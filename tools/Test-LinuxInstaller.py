#!/usr/bin/env python3
"""File/package fixtures. These do not launch MachineCraft, Wine or Proton."""
import argparse
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import subprocess
import sys
import tempfile
import unittest
from unittest import mock

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location("mpatcher_linux", ROOT / "Installer/Linux/mpatcher_linux.py")
m = importlib.util.module_from_spec(spec)
spec.loader.exec_module(m)
PACKAGES = {}
MODULE_PAYLOAD = None
FIXTURE_EXE = b"MachineCraft installer fixture, not executable"
POLICY_PROBE = None


def native_backup_policy(root):
    result = subprocess.run([str(POLICY_PROBE), str(root / "McnCraft_Data")], capture_output=True, text=True, encoding="utf-8", timeout=15)
    if result.returncode:
        raise RuntimeError(result.stderr)
    lines = result.stdout.splitlines()
    return lines[0], lines[1:]


class InstallerTests(unittest.TestCase):
    def setUp(self):
        self.temporary = tempfile.TemporaryDirectory(prefix="case-", dir=WORK)
        self.addCleanup(self.temporary.cleanup)
        # Keep the .NET Framework native-policy probe below its MAX_PATH limit;
        # spaces and Japanese path characters are still exercised.
        self.root = Path(self.temporary.name) / "MC 日本語"
        (self.root / "McnCraft_Data/Mono").mkdir(parents=True)
        (self.root / "McnCraft_Data/Managed").mkdir()
        (self.root / "McnCraft_Data/Managed/Assembly-CSharp.dll").write_bytes(b"untouched game assembly")
        (self.root / "McnCraft.exe").write_bytes(FIXTURE_EXE)
        (self.root / "UserData/Machine").mkdir(parents=True)
        self.user = self.root / "UserData/Machine/keep.mcbd"
        self.user.write_bytes(b"unchanged user machine")
        (self.root / "settings.ini").write_bytes(b"unchanged settings")
        self.logs = []
        self.addCleanup(mock.patch.stopall)
        mock.patch.object(m, "GAME_SHA256", m.sha256(FIXTURE_EXE)).start()
        mock.patch.object(m, "ensure_game_stopped").start()
        self.payload = PACKAGES["stable"]
        mock.patch.object(m, "download_payload", side_effect=lambda channel, log: PACKAGES[channel]["payload"]).start()

    def install(self, channel="stable"):
        m.install(m.validate_game(self.root), channel, self.logs.append)
        self.assertEqual(self.user.read_bytes(), b"unchanged user machine")
        self.assertEqual((self.root / "settings.ini").read_bytes(), b"unchanged settings")
        self.assertEqual((self.root / "McnCraft_Data/Managed/Assembly-CSharp.dll").read_bytes(), b"untouched game assembly")

    def test_clean_install_remove(self):
        self.install()
        self.assertTrue((self.root / "Work").is_dir())
        self.assertEqual(m.installed_manifest(self.root)["productversion"], "0.3.8")
        m.uninstall(self.root, self.logs.append)
        self.assertFalse((self.root / m.TARGETS["loader"]).exists())
        self.assertFalse((self.root / m.TARGETS["manifest"]).exists())
        self.assertTrue(self.user.exists())

    def test_existing_patcher_restore(self):
        original = b"old third-party loader"
        (self.root / m.TARGETS["loader"]).write_bytes(original)
        self.install()
        manifest = m.installed_manifest(self.root)
        self.assertTrue(manifest["backupfilename"].startswith("__Internal."))
        self.assertEqual(m.original_backup(self.root, manifest), original)
        m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), original)

    def seed_legacy_linux_install(self):
        original = b"original before Linux 0.1.0"
        digest = m.sha256(original)
        name = "__Internal-" + digest + ".dll"
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        backup.mkdir()
        (backup / name).write_bytes(original)
        loader = PACKAGES["alpha"]["payload"][1]
        (self.root / m.TARGETS["loader"]).write_bytes(loader)
        text = "Format=2\nProductVersion=0.3.9.3\nInstalledPayloadSha256=" + m.sha256(loader) + "\nOriginalExisted=True\nOriginalSha256=" + digest + "\nBackupFileName=" + name + "\nInstallerPlatform=Linux\n"
        (self.root / m.TARGETS["manifest"]).write_text(text)
        return name, original

    def test_linux_010_backup_name_migration(self):
        name, original = self.seed_legacy_linux_install()
        self.install("alpha")
        manifest = m.installed_manifest(self.root)
        self.assertTrue(manifest["backupfilename"].startswith("__Internal."))
        self.assertFalse((self.root / "McnCraft_Data/MPatcherForkBackup" / name).exists())
        self.assertEqual(m.original_backup(self.root, manifest), original)
        m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), original)

    def test_native_game_policy_reproduces_and_accepts_fix(self):
        if POLICY_PROBE is None:
            self.skipTest("C# policy probe requires the Windows .NET compiler")
        name, original = self.seed_legacy_linux_install()
        path = self.root / "McnCraft_Data/MPatcherForkBackup" / name
        reason, files = native_backup_policy(self.root)
        self.assertEqual(reason, "no-verified-backup")
        self.assertIn(str(path), files)
        self.install("alpha")
        reason, files = native_backup_policy(self.root)
        self.assertEqual(reason, "verified-installer-backup")
        new_path = self.root / "McnCraft_Data/MPatcherForkBackup" / m.installed_manifest(self.root)["backupfilename"]
        self.assertNotIn(str(new_path), files)
        self.assertEqual(new_path.read_bytes(), original)

    def test_native_game_policy_accepts_new_backup(self):
        if POLICY_PROBE is None:
            self.skipTest("C# policy probe requires the Windows .NET compiler")
        (self.root / m.TARGETS["loader"]).write_bytes(b"existing MPatcher")
        self.install("alpha")
        self.assertEqual(native_backup_policy(self.root)[0], "verified-installer-backup")

    def test_migration_duplicate_backup_preserved_outside_scan(self):
        name, original = self.seed_legacy_linux_install()
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        normalized = "__Internal.original." + m.sha256(original) + ".dll"
        (backup / normalized).write_bytes(original)
        self.install("alpha")
        self.assertEqual((backup / normalized).read_bytes(), original)
        self.assertEqual((self.root / "MPatcherBackupHistory" / name).read_bytes(), original)
        self.assertFalse((backup / name).exists())

    def test_migration_collision_refused(self):
        name, original = self.seed_legacy_linux_install()
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        normalized = "__Internal.original." + m.sha256(original) + ".dll"
        (backup / normalized).write_bytes(b"unrelated conflicting data")
        before = (self.root / m.TARGETS["manifest"]).read_bytes()
        with self.assertRaises(m.Failure):
            m.install(self.root, "alpha", self.logs.append)
        self.assertEqual((self.root / m.TARGETS["manifest"]).read_bytes(), before)
        self.assertEqual((backup / name).read_bytes(), original)
        self.assertEqual((backup / normalized).read_bytes(), b"unrelated conflicting data")

    def test_recorded_legacy_history_archived_unknown_retained(self):
        current, original = self.seed_legacy_linux_install()
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        older = b"previous Linux original backup"
        old_name = "__Internal-" + m.sha256(older) + ".dll"
        (backup / old_name).write_bytes(older)
        (backup / "unknown.dll").write_bytes(b"unknown DLL")
        (self.root / "logs").mkdir()
        (self.root / "logs/MPatcherInstallerLinux.log").write_text("2026-10-01T00:00:00 BACKUP_VERIFIED sha256=" + m.sha256(older) + "\n")
        archive = self.root / "MPatcherBackupHistory"
        archive.mkdir()
        (archive / old_name).write_bytes(b"preserve archive collision")
        self.install("alpha")
        self.assertFalse((backup / old_name).exists())
        self.assertEqual((archive / (old_name[:-4] + ".2.dll")).read_bytes(), older)
        self.assertEqual((archive / old_name).read_bytes(), b"preserve archive collision")
        self.assertEqual((backup / "unknown.dll").read_bytes(), b"unknown DLL")
        if POLICY_PROBE is not None:
            self.assertIn(str(backup / "unknown.dll"), native_backup_policy(self.root)[1])

    def test_windows_recorded_history_hash_mismatch_retained(self):
        self.install()
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        backup.mkdir(exist_ok=True)
        name = "__Internal.previous.dll"
        (backup / name).write_bytes(b"modified historical backup")
        (self.root / "logs").mkdir()
        (self.root / "logs/MPatcherInstaller.log").write_text("2026-10-01 00:00:00 BACKUP_CREATED sha256=" + "0" * 64 + " file=" + name + "\n")
        self.install("alpha")
        self.assertEqual((backup / name).read_bytes(), b"modified historical backup")
        self.assertFalse((self.root / "MPatcherBackupHistory").exists())

    def test_migration_rollback_restores_names_and_manifest(self):
        name, original = self.seed_legacy_linux_install()
        before = (self.root / m.TARGETS["manifest"]).read_bytes()
        with mock.patch.object(m.Transaction, "create_work", side_effect=OSError("forced migration failure")), self.assertRaises(OSError):
            m.install(self.root, "alpha", self.logs.append)
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        self.assertEqual((backup / name).read_bytes(), original)
        self.assertEqual(len(list(backup.glob("*.dll"))), 1)
        self.assertEqual((self.root / m.TARGETS["manifest"]).read_bytes(), before)

    def test_external_loader_failure_removes_new_unrecorded_backup(self):
        (self.root / m.TARGETS["loader"]).write_bytes(b"original existing loader")
        self.install()
        previous = m.installed_manifest(self.root)
        loader = self.root / m.TARGETS["loader"]
        loader.write_bytes(b"external replacement before failing update")
        with mock.patch.object(m.Transaction, "create_work", side_effect=OSError("forced archive failure")), self.assertRaises(OSError):
            m.install(self.root, "alpha", self.logs.append)
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        self.assertEqual([path.name for path in backup.glob("*.dll")], [previous["backupfilename"]])
        self.assertEqual(loader.read_bytes(), b"external replacement before failing update")
        self.assertEqual(m.installed_manifest(self.root), previous)

    def test_relocation_interruption_recovers_history(self):
        name, original = self.seed_legacy_linux_install()
        tx = m.Transaction(self.root)
        tx.relocate("McnCraft_Data/MPatcherForkBackup/" + name, "MPatcherBackupHistory/" + name)
        m.recover(self.root, self.logs.append)
        self.assertEqual((self.root / "McnCraft_Data/MPatcherForkBackup" / name).read_bytes(), original)
        self.assertFalse((self.root / "MPatcherBackupHistory").exists())

    def test_recovery_010_journal_remains_supported(self):
        tx = m.Transaction(self.root)
        tx.change("loader", b"legacy interrupted loader")
        for key in ("relocations", "archive_created", "created_backups"):
            tx.journal.pop(key)
        tx.save()
        m.recover(self.root, self.logs.append)
        self.assertFalse((self.root / m.TARGETS["loader"]).exists())

    def test_stable_alpha_stable_keeps_original(self):
        original = b"original before both channels"
        (self.root / m.TARGETS["loader"]).write_bytes(original)
        for channel in ("stable", "alpha", "stable"):
            self.install(channel)
            self.assertEqual(m.original_backup(self.root, m.installed_manifest(self.root)), original)
        m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), original)

    def test_windows_manifest_upgrade(self):
        original = b"previous Windows patcher"
        backup = self.root / "McnCraft_Data/MPatcherForkBackup"
        backup.mkdir()
        (backup / "__Internal.original.dll").write_bytes(original)
        old = b"installed by Windows installer"
        (self.root / m.TARGETS["loader"]).write_bytes(old)
        text = "Format=2\nProductVersion=0.3.7\nInstalledPayloadSha256=" + m.sha256(old) + "\nOriginalExisted=True\nOriginalSha256=" + m.sha256(original) + "\nBackupFileName=__Internal.original.dll\n"
        (self.root / m.TARGETS["manifest"]).write_text(text)
        self.install()
        m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), original)

    def test_support_directory_self_heal(self):
        support = self.root / m.TARGETS["support"]
        (support / "MPatcherCrashWatchdog.exe").mkdir(parents=True)
        (support / "obsolete.bin").write_bytes(b"obsolete service cache")
        self.install()
        self.assertTrue((support / "MPatcherCrashWatchdog.exe").is_file())
        self.assertFalse((support / "obsolete.bin").exists())

    def test_install_failure_rollback(self):
        self.install()
        before_loader = (self.root / m.TARGETS["loader"]).read_bytes()
        before_manifest = (self.root / m.TARGETS["manifest"]).read_bytes()
        support = self.root / m.TARGETS["support"]
        (support / "preserve.bin").write_bytes(b"previous service folder")
        original = m.Transaction.create_work
        def fail(tx):
            original(tx)
            raise OSError("forced final-stage failure")
        with mock.patch.object(m.Transaction, "create_work", fail), self.assertRaises(OSError):
            m.install(self.root, "alpha", self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), before_loader)
        self.assertEqual((self.root / m.TARGETS["manifest"]).read_bytes(), before_manifest)
        self.assertEqual((support / "preserve.bin").read_bytes(), b"previous service folder")
        self.assertFalse((self.root / "McnCraft_Data" / m.TX_NAME).exists())

    def test_clean_failure_removes_new_work(self):
        original = m.Transaction.create_work
        def fail(tx):
            original(tx)
            raise OSError("failure after creating Work")
        with mock.patch.object(m.Transaction, "create_work", fail), self.assertRaises(OSError):
            m.install(self.root, "stable", self.logs.append)
        self.assertFalse((self.root / "Work").exists())
        self.assertFalse((self.root / m.TARGETS["loader"]).exists())
        self.assertFalse((self.root / m.TARGETS["support"]).exists())

    def test_uninstall_failure_rollback(self):
        self.install()
        previous = (self.root / m.TARGETS["manifest"]).read_bytes()
        original = m.Transaction.change
        def fail(tx, name, content):
            original(tx, name, content)
            if name == "watchdog":
                raise OSError("forced uninstall failure")
        with mock.patch.object(m.Transaction, "change", fail), self.assertRaises(OSError):
            m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["manifest"]).read_bytes(), previous)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), self.payload["payload"][1])
        self.assertTrue((self.root / m.TARGETS["watchdog"]).is_file())

    def test_changed_loader_refused(self):
        self.install()
        loader = self.root / m.TARGETS["loader"]
        loader.write_bytes(b"external replacement")
        with self.assertRaises(m.Failure) as caught:
            m.uninstall(self.root, self.logs.append)
        self.assertEqual(caught.exception.key, "changed")
        self.assertEqual(loader.read_bytes(), b"external replacement")

    def test_external_loader_update_restores_external(self):
        self.install()
        loader = self.root / m.TARGETS["loader"]
        loader.write_bytes(b"external replacement")
        self.install("alpha")
        m.uninstall(self.root, self.logs.append)
        self.assertEqual(loader.read_bytes(), b"external replacement")

    def test_changed_watchdog_retained(self):
        self.install()
        watchdog = self.root / m.TARGETS["watchdog"]
        watchdog.write_bytes(b"external watchdog")
        m.uninstall(self.root, self.logs.append)
        self.assertEqual(watchdog.read_bytes(), b"external watchdog")

    def test_damaged_backup_refused(self):
        (self.root / m.TARGETS["loader"]).write_bytes(b"old loader")
        self.install()
        manifest = m.installed_manifest(self.root)
        backup = self.root / "McnCraft_Data/MPatcherForkBackup" / manifest["backupfilename"]
        backup.write_bytes(b"damaged")
        with self.assertRaises(m.Failure):
            m.uninstall(self.root, self.logs.append)
        with self.assertRaises(m.Failure):
            m.install(self.root, "alpha", self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), self.payload["payload"][1])

    def test_missing_backup_refused(self):
        (self.root / m.TARGETS["loader"]).write_bytes(b"old loader")
        self.install()
        manifest = m.installed_manifest(self.root)
        (self.root / "McnCraft_Data/MPatcherForkBackup" / manifest["backupfilename"]).unlink()
        with self.assertRaises(m.Failure):
            m.uninstall(self.root, self.logs.append)

    def test_no_manifest_uninstall_refused(self):
        (self.root / m.TARGETS["loader"]).write_bytes(b"unmanaged patcher")
        with self.assertRaises(m.Failure):
            m.uninstall(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), b"unmanaged patcher")

    def test_manifest_backup_traversal_refused(self):
        self.install()
        path = self.root / m.TARGETS["manifest"]
        text = path.read_text().replace("OriginalExisted=False", "OriginalExisted=True").replace("BackupFileName=", "BackupFileName=../outside.dll")
        path.write_text(text)
        with self.assertRaises(m.Failure):
            m.uninstall(self.root, self.logs.append)

    def test_offline_leaves_game_unchanged(self):
        with mock.patch.object(m, "download_payload", side_effect=m.Failure("network")), self.assertRaises(m.Failure):
            m.install(self.root, "stable", self.logs.append)
        self.assertFalse((self.root / m.TARGETS["loader"]).exists())
        self.assertFalse((self.root / "McnCraft_Data/MPatcherForkBackup").exists())
        self.assertEqual(self.user.read_bytes(), b"unchanged user machine")

    def test_interrupted_transaction_recovery(self):
        self.install()
        before = {name: (self.root / m.TARGETS[name]).read_bytes() for name in ("loader", "watchdog", "manifest")}
        tx = m.Transaction(self.root)
        tx.change("loader", b"partial loader")
        tx.change("support", b"partial watchdog")
        tx.change("manifest", b"partial manifest")
        m.recover(self.root, self.logs.append)
        for name, data in before.items():
            self.assertEqual((self.root / m.TARGETS[name]).read_bytes(), data)

    def test_abrupt_process_exit_recovery(self):
        self.install()
        before = (self.root / m.TARGETS["loader"]).read_bytes()
        code = "import importlib.util,sys,os;from pathlib import Path;s=importlib.util.spec_from_file_location('i',sys.argv[1]);m=importlib.util.module_from_spec(s);s.loader.exec_module(m);tx=m.Transaction(Path(sys.argv[2]));tx.change('loader',b'interrupted child loader');tx.change('support',b'interrupted child watchdog');os._exit(99)"
        result = subprocess.run([sys.executable, "-c", code, str(ROOT / "Installer/Linux/mpatcher_linux.py"), str(self.root)], capture_output=True, timeout=30)
        self.assertEqual(result.returncode, 99, result.stderr)
        m.recover(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), before)

    def test_interrupted_install_blocks_update(self):
        m.Transaction(self.root)
        with self.assertRaises(m.Failure) as caught:
            self.install()
        self.assertEqual(caught.exception.key, "transaction")
        m.recover(self.root, self.logs.append)

    def test_committed_recovery_keeps_new_loader(self):
        tx = m.Transaction(self.root)
        tx.change("loader", b"committed loader")
        tx.journal["status"] = "committed"
        tx.save()
        m.recover(self.root, self.logs.append)
        self.assertEqual((self.root / m.TARGETS["loader"]).read_bytes(), b"committed loader")

    def test_recovery_rejects_unsafe_journal(self):
        tx = m.Transaction(self.root)
        tx.journal["entries"] = [{"name": "../UserData", "had_old": False}]
        tx.save()
        with self.assertRaises(m.Failure):
            m.recover(self.root, self.logs.append)
        self.assertTrue(self.user.exists())

    def test_unsupported_game_refused(self):
        (self.root / "McnCraft.exe").write_bytes(b"unknown game")
        with self.assertRaises(m.Failure) as caught:
            m.validate_game(self.root)
        self.assertEqual(caught.exception.key, "game")

    def test_steam_extra_library_discovery(self):
        home = Path(self.temporary.name) / "home"
        steam = home / ".var/app/com.valvesoftware.Steam/.local/share/Steam"
        (steam / "steamapps").mkdir(parents=True)
        library = Path(self.temporary.name) / "other library 日本語"
        game = library / "steamapps/common/MachineCraft"
        (game / "McnCraft_Data/Mono").mkdir(parents=True)
        (game / "McnCraft.exe").write_bytes(FIXTURE_EXE)
        escaped = str(library).replace("\\", "\\\\")
        (steam / "steamapps/libraryfolders.vdf").write_text('// comment\n"libraryfolders" { "1" { "path" "' + escaped + '" } }', encoding="utf-8")
        (library / "steamapps/appmanifest_397100.acf").write_text('"AppState" { "appid" "397100" "installdir" "MachineCraft" }')
        self.assertEqual(m.discover_games(home), [game.resolve()])

    def test_steam_traversal_refused(self):
        home = Path(self.temporary.name) / "home"
        steam = home / ".local/share/Steam"
        (steam / "steamapps").mkdir(parents=True)
        (steam / "steamapps/appmanifest_397100.acf").write_text('"AppState" { "appid" "397100" "installdir" "../../outside" }')
        self.assertEqual(m.discover_games(home), [])

    def test_symlink_refused(self):
        outside = Path(self.temporary.name) / "outside"
        outside.mkdir()
        try:
            (self.root / "McnCraft_Data/MPatcherForkBackup").symlink_to(outside, target_is_directory=True)
        except OSError:
            self.skipTest("Windows symlink privilege unavailable; run this case on Linux")
        with self.assertRaises(m.Failure):
            m.validate_game(self.root)


class ContractTests(unittest.TestCase):
    def test_real_stable_alpha_resource_hashes(self):
        expected = {"stable": ("71094801BC6A759123BDC473E6ED55EA5A3B07FC112F9363362C9C7D395C0D0A", "EB1023D0DEAEB83FDE204C35A3D6A2D33695CD839C4AC5099A80E90D6C2D10E0"),
                    "alpha": ("A136005DF71F104325B99392D0407F179D01BF952FC4EAD28D44F4EE0837851B", "6A9C4ABD4C61257D2A74D497B0CC84B91BA5E7DF130B0400BC3201694755630E")}
        for channel, package in PACKAGES.items():
            self.assertEqual(tuple(m.sha256(item) for item in package["payload"][1:]), expected[channel])


class ScaleModuleTests(unittest.TestCase):
    setUp = InstallerTests.setUp
    install = InstallerTests.install

    def module_payload(self):
        self.payload = {"payload": MODULE_PAYLOAD}
        PACKAGES["candidate"] = self.payload
        return MODULE_PAYLOAD

    def install_module(self):
        self.module_payload()
        self.install("candidate")

    def canonical_paths(self):
        return self.root / m.TARGETS["scale_module"], self.root / m.TARGETS["scale_manifest"]

    def test_module_clean_install_probe_remove(self):
        self.install_module()
        module, sidecar = self.canonical_paths()
        self.assertEqual(module.read_bytes(), MODULE_PAYLOAD[3])
        self.assertEqual(sidecar.read_bytes(), MODULE_PAYLOAD[4])
        self.assertTrue(m.probe_installation(self.root)["componentsIntact"])
        fields = m.installed_manifest(self.root)
        self.assertEqual(fields["installedscalemoduleapiversion"], "1")
        self.assertEqual(fields["installedscalemodulename"], "MPatcher.ScaleRuntime")
        m.uninstall(self.root, self.logs.append)
        self.assertFalse(module.exists())
        self.assertFalse(sidecar.exists())
        self.assertFalse((self.root / "McnCraft_Data/MPatcher").exists())

    def test_old_alpha_to_new_module_upgrade(self):
        self.install("alpha")
        self.install_module()
        self.assertTrue(m.probe_installation(self.root)["componentsIntact"])

    def test_module_to_old_stable_removes_owned_pair(self):
        self.install_module()
        self.install("stable")
        self.assertFalse(any(path.exists() for path in self.canonical_paths()))
        self.assertFalse(m.probe_installation(self.root)["scaleModuleOwned"])

    def test_module_upgrade_replaces_owned_pair(self):
        self.install_module()
        self.install_module()
        self.assertTrue(m.probe_installation(self.root)["componentsIntact"])

    def test_missing_pair_repaired_and_probe_detects(self):
        for missing in ("scale_module", "scale_manifest"):
            self.install_module()
            (self.root / m.TARGETS[missing]).unlink()
            self.assertFalse(m.probe_installation(self.root)["componentsIntact"])
            self.install_module()
            self.assertTrue(m.probe_installation(self.root)["componentsIntact"])

    def test_unknown_canonical_pair_refused_without_mutation(self):
        self.module_payload()
        module, sidecar = self.canonical_paths()
        module.parent.mkdir(parents=True)
        module.write_bytes(MODULE_PAYLOAD[3])
        sidecar.write_bytes(MODULE_PAYLOAD[4])
        before = {path.name: path.read_bytes() for path in (module, sidecar)}
        with self.assertRaises(m.Failure):
            m.install(self.root, "candidate", self.logs.append)
        self.assertEqual(before, {path.name: path.read_bytes() for path in (module, sidecar)})
        self.assertFalse((self.root / m.TARGETS["loader"]).exists())
        self.assertFalse((self.root / m.TARGETS["manifest"]).exists())
        self.assertFalse((self.root / "McnCraft_Data" / m.TX_NAME).exists())

    def test_unknown_other_module_preserved(self):
        self.install_module()
        sentinel = self.canonical_paths()[0].parent / "Unrelated.dll"
        sentinel.write_bytes(b"unrelated user module")
        self.install_module()
        m.uninstall(self.root, self.logs.append)
        self.assertEqual(sentinel.read_bytes(), b"unrelated user module")

    def test_changed_pair_refuses_update_and_uninstall_before_writes(self):
        self.install_module()
        module, sidecar = self.canonical_paths()
        for modified in (module, sidecar):
            original = modified.read_bytes()
            modified.write_bytes(b"externally changed")
            before = {name: (self.root / m.TARGETS[name]).read_bytes()
                      for name in ("loader", "manifest", "scale_module", "scale_manifest")}
            for operation in (lambda: m.install(self.root, "candidate", self.logs.append),
                              lambda: m.uninstall(self.root, self.logs.append)):
                with self.assertRaises(m.Failure):
                    operation()
                self.assertEqual(before, {name: (self.root / m.TARGETS[name]).read_bytes() for name in before})
            modified.write_bytes(original)

    def test_legacy_manual_manifest_adopted_only_with_proof(self):
        self.install_module()
        path = self.root / m.TARGETS["manifest"]
        lines = [line for line in path.read_text().splitlines() if not line.startswith("InstalledScaleModule")]
        lines += ["ScaleRuntimeApiVersion=1", "ScaleRuntimePayloadSha256=" + m.sha256(MODULE_PAYLOAD[3])]
        path.write_text("\n".join(lines) + "\n")
        self.install_module()
        self.assertTrue(m.probe_installation(self.root)["componentsIntact"])
        self.assertIn("installedscalemodulemanifestsha256", m.installed_manifest(self.root))

    def test_legacy_wrong_proof_refused(self):
        self.install_module()
        path = self.root / m.TARGETS["manifest"]
        lines = [line for line in path.read_text().splitlines() if not line.startswith("InstalledScaleModule")]
        lines += ["ScaleRuntimeApiVersion=1", "ScaleRuntimePayloadSha256=" + "0" * 64]
        path.write_text("\n".join(lines) + "\n")
        before = path.read_bytes()
        with self.assertRaises(m.Failure):
            m.install(self.root, "candidate", self.logs.append)
        self.assertEqual(path.read_bytes(), before)

    def test_module_install_rollback_each_stage(self):
        self.install("alpha")
        before = {name: (self.root / m.TARGETS[name]).read_bytes() for name in ("loader", "manifest", "watchdog")}
        self.module_payload()
        original = m.Transaction.change
        for failing_stage in ("scale_module", "scale_manifest", "manifest"):
            def fail(tx, name, content):
                original(tx, name, content)
                if name == failing_stage:
                    raise OSError("injected module-stage failure")
            with mock.patch.object(m.Transaction, "change", fail), self.assertRaises(OSError):
                m.install(self.root, "candidate", self.logs.append)
            self.assertEqual(before, {name: (self.root / m.TARGETS[name]).read_bytes() for name in before})
            self.assertFalse(any(path.exists() for path in self.canonical_paths()))
            self.assertFalse((self.root / "McnCraft_Data/MPatcher").exists())

    def test_module_upgrade_rollback_restores_pair(self):
        self.install_module()
        before = {name: (self.root / m.TARGETS[name]).read_bytes()
                  for name in ("loader", "manifest", "watchdog", "scale_module", "scale_manifest")}
        with mock.patch.object(m.Transaction, "create_work", side_effect=OSError("injected final failure")), self.assertRaises(OSError):
            m.install(self.root, "candidate", self.logs.append)
        self.assertEqual(before, {name: (self.root / m.TARGETS[name]).read_bytes() for name in before})

    def test_module_interruption_recover_old_pair(self):
        self.install_module()
        before = {name: (self.root / m.TARGETS[name]).read_bytes() for name in ("loader", "scale_module", "scale_manifest")}
        tx = m.Transaction(self.root)
        for name in before:
            tx.change(name, b"interrupted replacement")
        m.recover(self.root, self.logs.append)
        self.assertEqual(before, {name: (self.root / m.TARGETS[name]).read_bytes() for name in before})

    def test_module_interruption_recover_new_directories(self):
        tx = m.Transaction(self.root)
        tx.change("scale_module", MODULE_PAYLOAD[3])
        m.recover(self.root, self.logs.append)
        self.assertFalse((self.root / "McnCraft_Data/MPatcher").exists())

    def test_module_uninstall_rollback_restores_pair_and_loader(self):
        self.install_module()
        before = {name: (self.root / m.TARGETS[name]).read_bytes()
                  for name in ("loader", "manifest", "scale_module", "scale_manifest")}
        original = m.Transaction.change
        def fail(tx, name, content):
            original(tx, name, content)
            if name == "scale_manifest":
                raise OSError("injected uninstall failure")
        with mock.patch.object(m.Transaction, "change", fail), self.assertRaises(OSError):
            m.uninstall(self.root, self.logs.append)
        self.assertEqual(before, {name: (self.root / m.TARGETS[name]).read_bytes() for name in before})

    def test_module_sidecar_api_name_hash_and_version_rejected(self):
        for old, new in ((b"HostApiVersion=1", b"HostApiVersion=2"),
                         (b"ModuleAssemblyName=MPatcher.ScaleRuntime", b"ModuleAssemblyName=Other"),
                         (b"PayloadSha256=", b"PayloadSha256=0"),
                         (b"ProductVersion=" + MODULE_PAYLOAD[0].encode("ascii"), b"ProductVersion=0.0.0.1")):
            with self.subTest(replacement=new), self.assertRaises(m.Failure):
                self.assertNotEqual(MODULE_PAYLOAD[4].replace(old, new), MODULE_PAYLOAD[4])
                m.validate_scale_payload(MODULE_PAYLOAD[3], MODULE_PAYLOAD[4].replace(old, new), MODULE_PAYLOAD[0])

    def test_module_payload_partial_and_unknown_resources_rejected(self):
        resources = {m.LOADER_RESOURCE: MODULE_PAYLOAD[1], m.WATCHDOG_RESOURCE: MODULE_PAYLOAD[2],
                     m.SCALE_MODULE_RESOURCE: MODULE_PAYLOAD[3], m.SCALE_MANIFEST_RESOURCE: MODULE_PAYLOAD[4]}
        self.assertEqual(m.payload_from_resources(MODULE_PAYLOAD[0], resources), MODULE_PAYLOAD)
        for omitted in (m.SCALE_MODULE_RESOURCE, m.SCALE_MANIFEST_RESOURCE):
            with self.assertRaises(m.Failure):
                m.payload_from_resources(MODULE_PAYLOAD[0], {name: data for name, data in resources.items() if name != omitted})
        resources[m.RESOURCE_PREFIX + "Unknown.dll"] = b"unknown"
        with self.assertRaises(m.Failure):
            m.payload_from_resources(MODULE_PAYLOAD[0], resources)

    def test_module_actual_assembly_name_checked(self):
        # A different valid managed PE cannot masquerade behind the sidecar name.
        wrong = MODULE_PAYLOAD[2]
        original_hash = m.sha256(MODULE_PAYLOAD[3]).encode("ascii")
        sidecar = MODULE_PAYLOAD[4].replace(original_hash, m.sha256(wrong).encode("ascii"))
        with self.assertRaises(m.Failure):
            m.validate_scale_payload(wrong, sidecar, MODULE_PAYLOAD[0])

    def test_incomplete_authoritative_ownership_refused(self):
        self.install_module()
        path = self.root / m.TARGETS["manifest"]
        path.write_text("\n".join(line for line in path.read_text().splitlines()
                                  if not line.startswith("InstalledScaleModuleManifestSha256=")) + "\n")
        before = path.read_bytes()
        with self.assertRaises(m.Failure):
            m.install(self.root, "candidate", self.logs.append)
        self.assertEqual(path.read_bytes(), before)

    def test_module_symlink_refused(self):
        outside = Path(self.temporary.name) / "outside-modules"
        outside.mkdir()
        (self.root / "McnCraft_Data/MPatcher").mkdir()
        try:
            (self.root / "McnCraft_Data/MPatcher/Modules").symlink_to(outside, target_is_directory=True)
        except OSError:
            self.skipTest("Windows symlink privilege unavailable; run this case on Linux")
        with self.assertRaises(m.Failure):
            m.validate_game(self.root)

    def test_module_invalid_recovery_directory_refused(self):
        tx = m.Transaction(self.root)
        tx.journal["created_directories"] = ["UserData"]
        tx.save()
        with self.assertRaises(m.Failure):
            m.recover(self.root, self.logs.append)


class AdditionalContractTests(unittest.TestCase):

    def test_duplicate_key_rejected(self):
        with self.assertRaises(m.Failure):
            m.release_manifest(PACKAGES["stable"]["manifest"] + b"\nInstallerLength=1", "stable")

    def test_manifest_url_and_version_rejections(self):
        source = PACKAGES["stable"]["manifest"]
        for old, new in ((b"https://github.com/RestartLive113", b"http://github.com/RestartLive113"),
                         (b"RestartLive113/MPatcher", b"other/repo"), (b"MPatcherPackage.exe", b"MPatcherPackage.exe?x=1"),
                         (b"Version=0.3.8", b"Version=../escape"), (b"Format=1", b"Format=2"),
                         (b"Version=0.3.8", b"Version=0.3.8.0"), (b"InstallerLength=19257344", b"InstallerLength=999999999")):
            with self.subTest(replacement=new), self.assertRaises(m.Failure):
                m.release_manifest(source.replace(old, new), "stable")

    def test_bad_package_hash_rejected(self):
        original = m.download_payload
        # The function may be patched only in InstallerTests, whose cleanup stops it.
        with mock.patch.object(m, "fetch", side_effect=[PACKAGES["stable"]["manifest"], b"tampered package"]), self.assertRaises(m.Failure):
            original("stable", lambda line: None)

    def test_real_download_pipeline_without_execution(self):
        for channel, package in PACKAGES.items():
            with mock.patch.object(m, "fetch", side_effect=[package["manifest"], package["package"]]):
                payload = m.download_payload(channel, lambda line: None)
                self.assertEqual(payload, package["payload"])

    def test_truncated_images_rejected(self):
        package = PACKAGES["stable"]["package"]
        for length in (0, 2, 60, 200, 1000, 5000):
            with self.subTest(length=length), self.assertRaises(m.Failure):
                m.PE(package[:length]).resources()

    def test_synthetic_linux_proc_guard(self):
        # Run the original guard separately from the operation fixtures.
        with tempfile.TemporaryDirectory(dir=WORK) as temp:
            proc = Path(temp)
            (proc / "12").mkdir()
            (proc / "12/cmdline").write_bytes(b"/usr/bin/wine\0Z:\\game\\McnCraft.exe\0")
            (proc / "12/comm").write_bytes(b"McnCraft.exe\n")
            with self.assertRaises(m.Failure) as caught:
                m.ensure_game_stopped(proc)
            self.assertEqual(caught.exception.key, "running")

    def test_missing_proc_refused(self):
        with self.assertRaises(m.Failure):
            m.ensure_game_stopped(WORK / "missing-proc")

    def test_root_and_windows_mutation_refused(self):
        with mock.patch.object(m.sys, "platform", "win32"), self.assertRaises(m.Failure) as caught:
            m.run_operation("install", "unused", "stable")
        self.assertEqual(caught.exception.key, "platform")
        with mock.patch.object(m.sys, "platform", "linux"), mock.patch.object(m.os, "geteuid", return_value=0, create=True), self.assertRaises(m.Failure) as caught:
            m.run_operation("install", "unused", "stable")
        self.assertEqual(caught.exception.key, "root")

    def test_all_localizations_present(self):
        for key in m.TEXT:
            for language in ("ru", "en", "ja"):
                self.assertTrue(m.tr(key, language))

    def test_logs_persist(self):
        with tempfile.TemporaryDirectory(dir=WORK) as temp:
            root = Path(temp)
            logger = m.Logger(root)
            logger("FIXTURE_EVENT")
            self.assertIn("FIXTURE_EVENT", logger.path.read_text())


def main():
    global WORK, POLICY_PROBE, MODULE_PAYLOAD
    parser = argparse.ArgumentParser()
    parser.add_argument("--package-root", type=Path, default=ROOT)
    parser.add_argument("--output", type=Path, default=ROOT / "logs/installer-linux")
    parser.add_argument("--candidate-package-dir", type=Path)
    parser.add_argument("--module-directory", type=Path,
                        default=ROOT / "build/scale-runtime-20261012-r10-preview-state")
    args = parser.parse_args()
    WORK = args.output.resolve()
    WORK.mkdir(parents=True, exist_ok=True)
    compiler = Path(os.environ.get("WINDIR", "C:/Windows")) / "Microsoft.NET/Framework/v4.0.30319/csc.exe"
    policy_source = args.package_root / "CustomPatches/InstallerBackupDllPolicy.cs"
    if sys.platform == "win32" and compiler.is_file():
        probe_source = WORK / "ScriptBackupPolicyProbe.cs"
        probe_source.write_text('using System; using System.IO; using System.Text; using MPatcherFork.CustomPatches; internal static class Program { static int Main(string[] args) { Console.OutputEncoding = new UTF8Encoding(false); string reason; string[] files = Directory.GetFiles(args[0], "*.dll", SearchOption.AllDirectories); string[] filtered = InstallerBackupDllPolicy.Filter(args[0], files, out reason); Console.WriteLine(reason); foreach (string file in filtered) Console.WriteLine(file); return 0; } }', encoding="utf-8")
        POLICY_PROBE = WORK / "ScriptBackupPolicyProbe.exe"
        subprocess.run([str(compiler), "/nologo", "/target:exe", "/out:" + str(POLICY_PROBE), str(probe_source), str(policy_source)], check=True, capture_output=True, timeout=30)
    for channel, folder in (("stable", "MPatcher-v0.3.8"), ("alpha", "MPatcher-v0.3.9.3-Alpha")):
        root = args.package_root / "dist" / folder
        package = (root / "MPatcherPackage.exe").read_bytes()
        manifest = (root / "MPatcherUpdate.ini").read_bytes()
        values = m.release_manifest(manifest, channel)
        if len(package) != values["length"] or m.sha256(package) != values["sha256"]:
            raise RuntimeError("reference package does not match manifest: " + folder)
        resources = m.PE(package).resources()
        PACKAGES[channel] = {"manifest": manifest, "package": package,
                             "payload": (values["version"], resources[m.LOADER_RESOURCE], resources[m.WATCHDOG_RESOURCE])}
    if args.candidate_package_dir:
        package = (args.candidate_package_dir / "MPatcherPackage.exe").read_bytes()
        manifest = (args.candidate_package_dir / "MPatcherUpdate.ini").read_bytes()
        values = m.release_manifest(manifest, "alpha")
        if len(package) != values["length"] or m.sha256(package) != values["sha256"]:
            raise RuntimeError("candidate package does not match manifest")
        MODULE_PAYLOAD = m.payload_from_resources(values["version"], m.PE(package).resources())
        if len(MODULE_PAYLOAD) != 5:
            raise RuntimeError("candidate package lacks required Scale module")
        candidate_report = {"version": values["version"], "packageSha256": m.sha256(package),
                            "loaderSha256": m.sha256(MODULE_PAYLOAD[1]), "moduleSha256": m.sha256(MODULE_PAYLOAD[3]),
                            "moduleManifestSha256": m.sha256(MODULE_PAYLOAD[4]), "realPackageExtraction": True}
    else:
        module = (args.module_directory / "MPatcher.ScaleRuntime.dll").read_bytes()
        sidecar = (args.module_directory / "MPatcher.ScaleRuntime.manifest.ini").read_bytes()
        fields = m.validate_scale_payload(module, sidecar)
        MODULE_PAYLOAD = (fields["productversion"], PACKAGES["alpha"]["payload"][1], PACKAGES["alpha"]["payload"][2], module, sidecar)
        candidate_report = {"version": fields["productversion"], "moduleSha256": m.sha256(module),
                            "moduleManifestSha256": m.sha256(sidecar), "realPackageExtraction": False}
    suite = unittest.defaultTestLoader.loadTestsFromModule(sys.modules[__name__])
    with (WORK / "fixtures.txt").open("w", encoding="utf-8") as stream:
        result = unittest.TextTestRunner(stream=stream, verbosity=2).run(suite)
    report = {"tests": result.testsRun, "failures": len(result.failures), "errors": len(result.errors), "skipped": result.skipped,
              "passed": result.wasSuccessful(), "host": sys.platform, "runtimeAcceptance": False,
              "installerVersion": m.VERSION, "nativePolicyProbe": POLICY_PROBE is not None,
              "policySourceSha256": m.file_hash(policy_source),
              "sourceSha256": m.file_hash(ROOT / "Installer/Linux/mpatcher_linux.py"),
              "scaleModuleCandidate": candidate_report,
              "packages": {key: {"version": value["payload"][0], "packageSha256": m.sha256(value["package"]),
                                 "loaderSha256": m.sha256(value["payload"][1]), "watchdogSha256": m.sha256(value["payload"][2])}
                           for key, value in PACKAGES.items() if "package" in value}}
    (WORK / "report.json").write_text(json.dumps(report, indent=2), encoding="utf-8")
    print(json.dumps(report, indent=2))
    if not result.wasSuccessful():
        print((WORK / "fixtures.txt").read_text(encoding="utf-8"))
    return 0 if result.wasSuccessful() else 1


if __name__ == "__main__":
    sys.exit(main())
