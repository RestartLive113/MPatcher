#!/usr/bin/env python3
"""Native Linux MPatcher installer. Python 3.10+, no third-party modules.

The downloaded Windows package is read as data, never executed. ECMA-335
ManifestResource tables provide the original native loader and crash watchdog.
"""
from __future__ import annotations

import argparse
import datetime as dt
import hashlib
import json
import locale
import os
from pathlib import Path
import queue
import re
import shutil
import struct
import sys
import tempfile
import threading
import time
import urllib.error
import urllib.parse
import urllib.request

VERSION = "0.1.2 Alpha"
RELEASES = "https://github.com/RestartLive113/MPatcher/releases/"
SOURCES = {"stable": RELEASES + "latest/download/MPatcherUpdate.ini",
           "alpha": RELEASES + "download/alpha/MPatcherUpdate.ini"}
MAX_PACKAGE = 64 * 1024 * 1024
GAME_SHA256 = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE"
RESOURCE_PREFIX = "MachineCraftMPatcherInstaller.Payload."
LOADER_RESOURCE = RESOURCE_PREFIX + "__Internal.dll"
WATCHDOG_RESOURCE = RESOURCE_PREFIX + "MPatcherCrashWatchdog.exe"
SCALE_MODULE_RESOURCE = RESOURCE_PREFIX + "MPatcher.ScaleRuntime.dll"
SCALE_MANIFEST_RESOURCE = RESOURCE_PREFIX + "MPatcher.ScaleRuntime.manifest.ini"
SCALE_MODULE_NAME = "MPatcher.ScaleRuntime"
SCALE_API_VERSION = "1"
SCALE_HASH_FIELDS = ("installedscalemodulesha256", "installedscalemodulemanifestsha256")
MODULE_DIRECTORIES = ("McnCraft_Data/MPatcher", "McnCraft_Data/MPatcher/Modules")
TARGETS = {"loader": "McnCraft_Data/Mono/__Internal.dll",
           "support": "McnCraft_Data/MPatcherFork",
           "watchdog": "McnCraft_Data/MPatcherFork/MPatcherCrashWatchdog.exe",
           "scale_module": "McnCraft_Data/MPatcher/Modules/MPatcher.ScaleRuntime.dll",
           "scale_manifest": "McnCraft_Data/MPatcher/Modules/MPatcher.ScaleRuntime.manifest.ini",
           "manifest": "McnCraft_Data/MPatcherFork.install.ini"}
TX_NAME = ".MPatcherLinux.transaction"
TEXT = {
    "title": ("MPatcher — установщик Linux", "MPatcher — Linux installer", "MPatcher — Linux インストーラー"),
    "folder": ("Папка MachineCraft", "MachineCraft folder", "MachineCraft フォルダー"),
    "browse": ("Обзор…", "Browse…", "参照…"),
    "channel": ("Канал обновлений", "Update channel", "更新チャンネル"),
    "language": ("Язык", "Language", "言語"),
    "install": ("Установить / обновить", "Install / update", "インストール / 更新"),
    "uninstall": ("Удалить патчер", "Uninstall patcher", "パッチャーを削除"),
    "recover": ("Восстановить после сбоя", "Recover interrupted operation", "中断した処理を復元"),
    "ready": ("Закройте игру. Выберите папку и канал.", "Close the game. Select its folder and channel.", "ゲームを終了し、フォルダーとチャンネルを選択してください。"),
    "runtime": ("Для Windows-версии игры через Proton/Wine. Игровая проверка Linux ещё требуется.", "For the Windows game through Proton/Wine. Linux gameplay testing is still required.", "Proton/Wine の Windows 版ゲーム用。Linux での動作確認が必要です。"),
    "network": ("Нет доступа к пакету GitHub. Проверьте интернет и повторите.", "Cannot access the GitHub package. Check your internet connection and retry.", "GitHub に接続できません。接続を確認して再試行してください。"),
    "invalid": ("Не удалось проверить пакет", "Package validation failed", "パッケージの検証に失敗しました"),
    "game": ("Нужна папка поддерживаемой Windows-версии MachineCraft (McnCraft.exe).", "Select the supported Windows MachineCraft folder (McnCraft.exe).", "対応する Windows 版 MachineCraft (McnCraft.exe) のフォルダーを選択してください。"),
    "running": ("Закройте MachineCraft и MPatcherCrashWatchdog перед операцией.", "Close MachineCraft and MPatcherCrashWatchdog before continuing.", "MachineCraft と MPatcherCrashWatchdog を終了してください。"),
    "root": ("Запускайте установщик обычным пользователем, без sudo.", "Run this installer as your normal user, without sudo.", "sudo を使わず通常のユーザーとして起動してください。"),
    "platform": ("Установщик предназначен для Linux.", "This installer requires Linux.", "このインストーラーは Linux 用です。"),
    "transaction": ("Обнаружена незавершённая операция. Нажмите «Восстановить после сбоя».", "An interrupted operation exists. Choose Recover interrupted operation.", "中断した処理があります。「中断した処理を復元」を選択してください。"),
    "backup": ("Резервная копия отсутствует или повреждена.", "The original backup is missing or damaged.", "元のバックアップが存在しないか破損しています。"),
    "changed": ("Файл патчера изменён извне или его принадлежность не подтверждена; операция остановлена.", "An MPatcher file was changed externally or ownership is unverified; operation stopped.", "MPatcher ファイルが外部で変更されたか所有情報を確認できないため、処理を中止しました。"),
    "manifest": ("Манифест установки отсутствует или недопустим.", "The installation manifest is missing or invalid.", "インストール情報が存在しないか無効です。"),
    "path": ("Небезопасный путь или символическая ссылка внутри служебных файлов.", "Unsafe path or symbolic link in installer-managed files.", "管理ファイルのパスまたはシンボリックリンクが安全ではありません。"),
    "proc": ("Не удалось проверить запущенные процессы через /proc.", "Cannot verify running processes through /proc.", "/proc で実行中のプロセスを確認できません。"),
    "done": ("Операция завершена. Журнал: ", "Operation completed. Log: ", "処理が完了しました。ログ: "),
    "error": ("Ошибка", "Error", "エラー"),
    "busy": ("Выполняется операция…", "Operation in progress…", "処理中…"),
    "confirm": ("Удалить MPatcher и восстановить предыдущий DLL, если он был?", "Remove MPatcher and restore the previous DLL, if one existed?", "MPatcher を削除して、存在する場合は以前の DLL を復元しますか？"),
    "tk": ("Для окна в Arch установите python и tk: sudo pacman -Syu python tk. Доступен также режим --install --game-dir PATH.", "For the Arch GUI install python and tk: sudo pacman -Syu python tk. CLI: --install --game-dir PATH.", "Arch の GUI: sudo pacman -Syu python tk。CLI: --install --game-dir PATH。"),
}


def tr(key, language="en"):
    return TEXT[key][{"ru": 0, "en": 1, "ja": 2, "jp": 2}.get(language, 1)]


class Failure(Exception):
    def __init__(self, key, detail=""):
        self.key, self.detail = key, str(detail)
        super().__init__(key + (": " + self.detail if self.detail else ""))

    def localized(self, language):
        return tr(self.key, language) + ("\n" + self.detail if self.detail else "")


def sha256(data):
    return hashlib.sha256(data).hexdigest().upper()


def file_hash(path):
    with Path(path).open("rb") as stream:
        digest = hashlib.sha256()
        for block in iter(lambda: stream.read(131072), b""):
            digest.update(block)
        return digest.hexdigest().upper()


def read_pairs(text, maximum=65536):
    if len(text.encode("utf-8")) > maximum:
        raise Failure("invalid", "manifest too large")
    values = {}
    for line in text.lstrip("\ufeff").splitlines():
        line = line.strip()
        if not line or line.startswith("#"):
            continue
        if "=" not in line:
            raise Failure("invalid", "invalid manifest line")
        key, value = (part.strip() for part in line.split("=", 1))
        key = key.lower()
        if not key or key in values:
            raise Failure("invalid", "duplicate or empty key")
        values[key] = value
    return values


def release_manifest(data, channel):
    try:
        fields = read_pairs(data.decode("utf-8-sig"))
        version = fields["version"]
        valid_version = r"[0-9]+\.[0-9]+\.[0-9]+" + (r"\.[0-9]+" if channel == "alpha" else "")
        if fields.get("format") != "1" or not re.fullmatch(valid_version, version):
            raise ValueError("invalid format/version")
        if any(int(piece) > 2147483647 for piece in version.split(".")):
            raise ValueError("version component out of range")
        tag = "alpha" if channel == "alpha" else "v" + version
        expected = RELEASES + "download/" + tag + "/MPatcherPackage.exe"
        if fields["installerurl"] != expected:
            raise ValueError("package URL is not allowed")
        if not re.fullmatch(r"[0-9a-fA-F]{64}", fields["installersha256"]):
            raise ValueError("invalid SHA-256")
        if not re.fullmatch(r"[0-9]+", fields["installerlength"]):
            raise ValueError("invalid length")
        length = int(fields["installerlength"])
        if not 0 < length <= MAX_PACKAGE:
            raise ValueError("package too large or empty")
        return {"version": version, "url": expected, "sha256": fields["installersha256"].upper(), "length": length}
    except (KeyError, ValueError, UnicodeError) as error:
        raise Failure("invalid", error) from error


class HTTPSRedirects(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        target = urllib.parse.urlsplit(newurl)
        if target.scheme != "https" or target.username or target.password:
            raise Failure("invalid", "unsafe download redirect")
        if target.hostname not in {"github.com", "release-assets.githubusercontent.com", "objects.githubusercontent.com"}:
            raise Failure("invalid", "unexpected download host")
        return super().redirect_request(req, fp, code, msg, headers, newurl)


def fetch(url, maximum):
    try:
        request = urllib.request.Request(url, headers={"User-Agent": "MPatcher-Linux/0.1.2", "Accept-Encoding": "identity"})
        with urllib.request.build_opener(HTTPSRedirects()).open(request, timeout=30) as response:
            chunks, length = [], 0
            deadline = time.monotonic() + 180
            while True:
                if time.monotonic() > deadline:
                    raise Failure("network", "download time limit exceeded")
                block = response.read(min(131072, maximum + 1 - length))
                if not block:
                    break
                length += len(block)
                if length > maximum:
                    raise Failure("invalid", "download exceeds allowed length")
                chunks.append(block)
            return b"".join(chunks)
    except (urllib.error.URLError, OSError, TimeoutError) as error:
        raise Failure("network", error) from error


class PE:
    """Bounded PE reader; no OS loader, reflection or package execution."""
    def __init__(self, data):
        self.data = data
        if self.part(0, 2) != b"MZ":
            raise Failure("invalid", "not a PE image")
        pe = self.number(0x3c, 4)
        if self.part(pe, 4) != b"PE\0\0":
            raise Failure("invalid", "invalid PE signature")
        self.machine = self.number(pe + 4, 2)
        count, optional_size = self.number(pe + 6, 2), self.number(pe + 20, 2)
        optional = pe + 24
        magic = self.number(optional, 2)
        if magic not in (0x10b, 0x20b) or count > 96:
            raise Failure("invalid", "unsupported PE header")
        directory_offset = 96 if magic == 0x10b else 112
        if optional_size < directory_offset + 15 * 8:
            raise Failure("invalid", "missing PE data directories")
        self.cli_rva = self.number(optional + directory_offset + 14 * 8, 4)
        self.sections = []
        sections = optional + optional_size
        for i in range(count):
            item = sections + i * 40
            self.part(item, 40)
            self.sections.append((self.number(item + 12, 4), self.number(item + 16, 4), self.number(item + 20, 4)))

    def part(self, offset, length):
        if offset < 0 or length < 0 or offset + length > len(self.data):
            raise Failure("invalid", "PE field outside image")
        return self.data[offset:offset + length]

    def number(self, offset, size):
        return int.from_bytes(self.part(offset, size), "little")

    def rva(self, address, size=1):
        for start, length, offset in self.sections:
            if start <= address and address + size <= start + length:
                result = offset + address - start
                self.part(result, size)
                return result
        raise Failure("invalid", "RVA outside raw section")

    def metadata(self):
        if not self.cli_rva:
            raise Failure("invalid", "package has no CLI header")
        cli = self.rva(self.cli_rva, 72)
        metadata = self.rva(self.number(cli + 8, 4), self.number(cli + 12, 4))
        if self.part(metadata, 4) != b"BSJB":
            raise Failure("invalid", "invalid CLI metadata")
        cursor = metadata + 16 + self.number(metadata + 12, 4)
        cursor = (cursor + 3) & ~3
        stream_count = self.number(cursor + 2, 2)
        cursor += 4
        streams = {}
        for _ in range(stream_count):
            offset, size = self.number(cursor, 4), self.number(cursor + 4, 4)
            name_start = cursor + 8
            end = self.data.find(b"\0", name_start, name_start + 32)
            if end < 0:
                raise Failure("invalid", "invalid metadata stream")
            name = self.part(name_start, end - name_start).decode("ascii")
            streams[name] = self.part(metadata + offset, size)
            cursor = (end + 4) & ~3
        tables = streams.get("#~", streams.get("#-"))
        strings = streams.get("#Strings")
        if tables is None or strings is None or len(tables) < 24:
            raise Failure("invalid", "metadata tables missing")
        return cli, tables, strings

    @staticmethod
    def table_layout(tables):
        heap = tables[6]
        valid = int.from_bytes(tables[8:16], "little")
        rows, position = [0] * 64, 24
        for index in range(64):
            if valid & (1 << index):
                if position + 4 > len(tables):
                    raise Failure("invalid", "truncated table counts")
                rows[index] = int.from_bytes(tables[position:position + 4], "little")
                position += 4
        # ECMA-335 II.22: rows preceding ManifestResource (table 40).
        def table(index):
            return 4 if rows[index] >= 65536 else 2

        def coded(bits, indices):
            return 4 if max(rows[index] for index in indices) >= (1 << (16 - bits)) else 2

        s, g, b = (4 if heap & mask else 2 for mask in (1, 2, 4))
        td = coded(2, (2, 1, 27))
        resolution = coded(2, (0, 26, 35, 1))
        constant = coded(2, (4, 8, 23))
        attribute = coded(5, (6, 4, 1, 2, 8, 9, 10, 0, 14, 23, 20, 17, 26, 27, 32, 35, 38, 39, 40, 42, 44, 43))
        attrtype = coded(3, (6, 10))
        sizes = [2+s+3*g, resolution+2*s, 4+2*s+td+table(4)+table(6), table(4), 2+s+b,
                 table(6), 8+s+b+table(8), table(8), 4+s, table(2)+td,
                 coded(3, (2, 1, 26, 6, 27))+s+b, 2+constant+b, attribute+attrtype+b,
                 coded(1, (4, 8))+b, 2+coded(2, (2, 6, 32))+b, 6+table(2), 4+table(4), b,
                 table(2)+table(20), table(20), 2+s+td, table(2)+table(23), table(23), 2+s+b,
                 2+table(6)+coded(1, (20, 23)), table(2)+2*coded(1, (6, 10)), s, b,
                 2+coded(1, (4, 6))+s+table(26), 4+table(4), 8, 4, 16+b+2*s,
                 4, 12, 12+2*b+2*s, 4+table(35), 12+table(35), 4+s+b,
                 8+2*s+coded(2, (38, 35, 39)), 8+s+coded(2, (38, 35, 39))]
        return rows, position, sizes, s, b

    def assembly_name(self):
        _, tables, strings = self.metadata()
        rows, position, sizes, s, b = self.table_layout(tables)
        if rows[32] != 1:
            raise Failure("invalid", "managed assembly identity missing")
        position += sum(rows[index] * sizes[index] for index in range(32))
        if position + sizes[32] > len(tables):
            raise Failure("invalid", "truncated assembly identity")
        name_index = int.from_bytes(tables[position + 16 + b:position + 16 + b + s], "little")
        end = strings.find(b"\0", name_index)
        if name_index >= len(strings) or end < 0:
            raise Failure("invalid", "invalid assembly name")
        return strings[name_index:end].decode("utf-8")

    def resources(self):
        cli, tables, strings = self.metadata()
        resource_size = self.number(cli + 28, 4)
        resource_base = self.rva(self.number(cli + 24, 4), resource_size)
        rows, position, sizes, s, _ = self.table_layout(tables)
        for index in range(40):
            position += rows[index] * sizes[index]
        result = {}
        size = sizes[40]
        for _ in range(rows[40]):
            if position + size > len(tables):
                raise Failure("invalid", "truncated resource table")
            offset = int.from_bytes(tables[position:position + 4], "little")
            name_index = int.from_bytes(tables[position + 8:position + 8 + s], "little")
            implementation = int.from_bytes(tables[position + 8 + s:position + size], "little")
            end = strings.find(b"\0", name_index)
            if name_index >= len(strings) or end < 0:
                raise Failure("invalid", "invalid resource name")
            name = strings[name_index:end].decode("utf-8")
            if name.startswith(RESOURCE_PREFIX):
                if name not in (LOADER_RESOURCE, WATCHDOG_RESOURCE, SCALE_MODULE_RESOURCE, SCALE_MANIFEST_RESOURCE):
                    raise Failure("invalid", "unknown payload resource")
                if implementation or name in result or offset + 4 > resource_size:
                    raise Failure("invalid", "invalid embedded resource")
                length = self.number(resource_base + offset, 4)
                if not length or offset + 4 + length > resource_size:
                    raise Failure("invalid", "invalid resource length")
                result[name] = self.part(resource_base + offset + 4, length)
            position += size
        if set(result) not in ({LOADER_RESOURCE, WATCHDOG_RESOURCE},
                              {LOADER_RESOURCE, WATCHDOG_RESOURCE, SCALE_MODULE_RESOURCE, SCALE_MANIFEST_RESOURCE}):
            raise Failure("invalid", "required package resources missing")
        return result


def validate_scale_payload(module, sidecar, version=None):
    try:
        if not module or not sidecar or len(module) > 16 * 1024 * 1024:
            raise Failure("invalid", "empty or oversized Scale module")
        fields = read_pairs(sidecar.decode("utf-8-sig"))
        if fields.get("hostapiversion") != SCALE_API_VERSION or fields.get("moduleassemblyname") != SCALE_MODULE_NAME:
            raise Failure("invalid", "Scale API or assembly name mismatch")
        if not re.fullmatch(r"[A-Fa-f0-9]{64}", fields.get("payloadsha256", "")) or fields["payloadsha256"].upper() != sha256(module):
            raise Failure("invalid", "Scale payload SHA-256 mismatch")
        if not re.fullmatch(r"[0-9]+\.[0-9]+\.[0-9]+(?:\.[0-9]+)?", fields.get("productversion", "")):
            raise Failure("invalid", "Scale product version invalid")
        if version is not None and fields["productversion"] != version:
            raise Failure("invalid", "Scale product version mismatch")
        image = PE(module)
        if not image.cli_rva or image.assembly_name() != SCALE_MODULE_NAME:
            raise Failure("invalid", "Scale module managed assembly identity mismatch")
        return fields
    except (UnicodeError, ValueError, IndexError) as error:
        raise Failure("invalid", error) from error


def payload_from_resources(version, resources):
    keys = set(resources)
    old = {LOADER_RESOURCE, WATCHDOG_RESOURCE}
    if keys not in (old, old | {SCALE_MODULE_RESOURCE, SCALE_MANIFEST_RESOURCE}):
        raise Failure("invalid", "invalid payload resource set")
    loader, watchdog = resources[LOADER_RESOURCE], resources[WATCHDOG_RESOURCE]
    image = PE(loader)
    if image.machine != 0x14c or image.cli_rva:
        raise Failure("invalid", "expected native x86 loader, not managed payload")
    PE(watchdog)
    if keys == old:
        return version, loader, watchdog
    module, sidecar = resources[SCALE_MODULE_RESOURCE], resources[SCALE_MANIFEST_RESOURCE]
    validate_scale_payload(module, sidecar, version)
    return version, loader, watchdog, module, sidecar


def download_payload(channel, log):
    log("DOWNLOAD_MANIFEST channel=" + channel)
    manifest = release_manifest(fetch(SOURCES[channel], 65536), channel)
    log("DOWNLOAD_PACKAGE version=" + manifest["version"])
    package = fetch(manifest["url"], manifest["length"])
    if len(package) != manifest["length"] or sha256(package) != manifest["sha256"]:
        raise Failure("invalid", "package length or SHA-256 mismatch")
    log("PACKAGE_VERIFIED sha256=" + manifest["sha256"])
    try:
        return payload_from_resources(manifest["version"], PE(package).resources())
    except (UnicodeError, ValueError, IndexError) as error:
        raise Failure("invalid", error) from error


def checked_path(root, relative):
    path = root / relative
    if path != root and root not in path.parents:
        raise Failure("path", relative)
    current = path
    while current != root:
        if current.is_symlink():
            raise Failure("path", str(current))
        current = current.parent
    if not path.resolve().is_relative_to(root):
        raise Failure("path", relative)
    return path


def validate_game(path):
    root = Path(path).expanduser().resolve()
    if not root.is_dir():
        raise Failure("game", str(root))
    for relative in ("McnCraft.exe", "McnCraft_Data/Mono", "McnCraft_Data/Managed/Assembly-CSharp.dll"):
        item = checked_path(root, relative)
        if not item.exists():
            raise Failure("game", relative)
    if file_hash(root / "McnCraft.exe") != GAME_SHA256:
        raise Failure("game", "unsupported McnCraft.exe SHA-256=" + file_hash(root / "McnCraft.exe"))
    for relative in (*TARGETS.values(), "McnCraft_Data/MPatcherForkBackup", "McnCraft_Data/" + TX_NAME, "MPatcherBackupHistory", "logs", "logs/MPatcherInstallerLinux.log", "Work"):
        checked_path(root, relative)
    return root


def ensure_game_stopped(proc=Path("/proc")):
    if not proc.is_dir():
        raise Failure("proc")
    try:
        processes = list(proc.iterdir())
    except OSError as error:
        raise Failure("proc", error) from error
    for process in processes:
        if not process.name.isdecimal():
            continue
        try:
            command = (process / "cmdline").read_bytes().replace(b"\0", b" ").lower()
            comm = (process / "comm").read_bytes().lower()
        except FileNotFoundError:
            continue
        except PermissionError as error:
            if process.stat().st_uid == os.getuid():
                raise Failure("proc", error) from error
            continue
        for executable in (b"mcncraft.exe", b"mpatchercrashwatchdog.exe"):
            if executable in command or executable in comm:
                raise Failure("running", "PID=" + process.name)


def parse_vdf(text):
    tokens = re.findall(r'"((?:\\.|[^"\\])*)"|([{}])|(//[^\n]*)', text)
    stack, root, pending = [], {}, None
    current = root
    for quoted, brace, comment in tokens:
        if comment:
            continue
        if brace == "{":
            if pending is None:
                raise ValueError("unexpected VDF brace")
            child = {}
            current[pending] = child
            stack.append(current)
            current, pending = child, None
        elif brace == "}":
            if not stack or pending is not None:
                raise ValueError("unbalanced VDF")
            current = stack.pop()
        else:
            value = re.sub(r'\\([\\"])', r'\1', quoted)
            if pending is None:
                pending = value
            else:
                current[pending], pending = value, None
    if stack or pending is not None:
        raise ValueError("incomplete VDF")
    return root


def discover_games(home=None):
    home = Path.home() if home is None else Path(home)
    roots = [home / entry for entry in (".local/share/Steam", ".steam/steam", ".steam/root", ".var/app/com.valvesoftware.Steam/.local/share/Steam")]
    libraries = {path.resolve() for path in roots if path.is_dir()}
    for steam in tuple(libraries):
        try:
            folders = parse_vdf((steam / "steamapps/libraryfolders.vdf").read_text(encoding="utf-8"))["libraryfolders"]
            for key, value in folders.items():
                if key.isdecimal():
                    folder = value.get("path") if isinstance(value, dict) else value
                    if folder:
                        libraries.add(Path(folder).expanduser().resolve())
        except (OSError, ValueError, KeyError):
            continue
    games = set()
    for library in libraries:
        try:
            app = parse_vdf((library / "steamapps/appmanifest_397100.acf").read_text(encoding="utf-8"))["AppState"]
            name = app["installdir"]
            if app.get("appid") != "397100" or not name or "/" in name or "\\" in name or name in (".", ".."):
                continue
            game = library / "steamapps/common" / name
        except (OSError, ValueError, KeyError):
            game = library / "steamapps/common/MachineCraft"
        if (game / "McnCraft.exe").is_file() and (game / "McnCraft_Data/Mono").is_dir():
            games.add(game.resolve())
    return sorted(games, key=str)


class Logger:
    def __init__(self, root=None, callback=None):
        base = root / "logs" if root else Path(os.environ.get("XDG_STATE_HOME", str(Path.home() / ".local/state"))) / "MPatcher/logs"
        base.mkdir(parents=True, exist_ok=True)
        self.path = base / "MPatcherInstallerLinux.log"
        self.callback = callback

    def __call__(self, message):
        line = dt.datetime.now(dt.timezone.utc).isoformat() + " " + message
        with self.path.open("a", encoding="utf-8") as output:
            output.write(line.replace("\n", " | ") + "\n")
        if self.callback:
            self.callback(message)


def atomic_write(path, data):
    descriptor, name = tempfile.mkstemp(prefix=".mpatcher-write-", dir=path.parent)
    temporary = Path(name)
    try:
        with os.fdopen(descriptor, "wb") as stream:
            stream.write(data)
            stream.flush()
            os.fsync(stream.fileno())
        os.replace(temporary, path)
        sync_directory(path.parent)
    finally:
        temporary.unlink(missing_ok=True)


def sync_directory(path):
    if sys.platform.startswith("linux"):
        descriptor = os.open(path, os.O_RDONLY | os.O_DIRECTORY)
        try:
            os.fsync(descriptor)
        finally:
            os.close(descriptor)


def remove_managed(path):
    if path.is_symlink():
        raise Failure("path", str(path))
    if path.is_dir():
        # Refuse symlinks anywhere in a directory before recursive cleanup.
        if any(item.is_symlink() for item in path.rglob("*")):
            raise Failure("path", str(path))
        shutil.rmtree(path)
    elif path.exists():
        path.unlink()


class Transaction:
    def __init__(self, root):
        self.root = root
        self.directory = checked_path(root, "McnCraft_Data/" + TX_NAME)
        try:
            self.directory.mkdir()
        except FileExistsError as error:
            raise Failure("transaction") from error
        self.journal = {"format": 1, "root": str(root), "status": "active", "entries": [], "work_created": False,
                        "relocations": [], "archive_created": False, "created_backups": [], "created_directories": []}
        self.save()

    def save(self):
        atomic_write(self.directory / "journal.json", json.dumps(self.journal).encode("utf-8"))

    def change(self, name, content):
        target = checked_path(self.root, TARGETS[name])
        if name in ("scale_module", "scale_manifest") and content is not None:
            self.create_module_directories()
        if name == "support" and target.exists():
            if not target.is_dir() or any(item.is_symlink() for item in target.rglob("*")):
                raise Failure("path", str(target))
        elif target.exists() and not target.is_file():
            raise Failure("path", str(target))
        entry = {"name": name, "had_old": target.exists()}
        self.journal["entries"].append(entry)
        self.save()
        backup = self.directory / ("old-" + name)
        if entry["had_old"]:
            os.replace(target, backup)
            sync_directory(target.parent)
            sync_directory(self.directory)
        if name == "support":
            target.mkdir()
            atomic_write(target / "MPatcherCrashWatchdog.exe", content)
        elif content is not None:
            atomic_write(target, content)

    def create_module_directories(self):
        for relative in MODULE_DIRECTORIES:
            directory = checked_path(self.root, relative)
            if directory.exists():
                if not directory.is_dir():
                    raise Failure("path", str(directory))
                continue
            self.journal["created_directories"].append(relative)
            self.save()
            directory.mkdir()
            sync_directory(directory.parent)

    def create_work(self):
        work = checked_path(self.root, "Work")
        if work.exists() and not work.is_dir():
            raise Failure("path", str(work))
        if not work.exists():
            self.journal["work_created"] = True
            self.save()
            work.mkdir()

    def relocate(self, source_relative, destination_relative):
        validate_relocation(source_relative, destination_relative)
        source = checked_path(self.root, source_relative)
        destination = checked_path(self.root, destination_relative)
        if not source.is_file() or destination.exists():
            raise Failure("backup", "relocation source/destination conflict")
        if not destination.parent.exists():
            self.journal["archive_created"] = True
            self.save()
            destination.parent.mkdir()
        self.journal["relocations"].append({"source": source_relative, "destination": destination_relative})
        self.save()
        os.replace(source, destination)
        sync_directory(source.parent)
        sync_directory(destination.parent)

    def create_backup(self, name, content):
        if not safe_backup_name(name) or not name.lower().startswith("__internal."):
            raise Failure("path", "invalid new backup name")
        path = checked_path(self.root, "McnCraft_Data/MPatcherForkBackup/" + name)
        if path.exists():
            raise Failure("backup", "new backup already exists")
        self.journal["created_backups"].append({"name": name, "sha256": sha256(content)})
        self.save()
        atomic_write(path, content)

    def commit(self):
        self.journal["status"] = "committed"
        self.save()
        remove_managed(self.directory)

    def rollback(self):
        for entry in reversed(self.journal["entries"]):
            target = checked_path(self.root, TARGETS[entry["name"]])
            backup = self.directory / ("old-" + entry["name"])
            if backup.exists():
                remove_managed(target)
                os.replace(backup, target)
                sync_directory(target.parent)
                sync_directory(self.directory)
            elif not entry["had_old"]:
                remove_managed(target)
        if self.journal["work_created"]:
            work = checked_path(self.root, "Work")
            if work.is_dir() and not any(work.iterdir()):
                work.rmdir()
        for move in reversed(self.journal.get("relocations", [])):
            source = checked_path(self.root, move["source"])
            destination = checked_path(self.root, move["destination"])
            if destination.exists():
                if source.exists():
                    raise Failure("transaction", "backup relocation rollback conflict")
                os.replace(destination, source)
                sync_directory(source.parent)
                sync_directory(destination.parent)
        if self.journal.get("archive_created", False):
            archive = checked_path(self.root, "MPatcherBackupHistory")
            if archive.is_dir() and not any(archive.iterdir()):
                archive.rmdir()
        for created in self.journal.get("created_backups", []):
            path = checked_path(self.root, "McnCraft_Data/MPatcherForkBackup/" + created["name"])
            if path.exists():
                if not path.is_file() or file_hash(path) != created["sha256"]:
                    raise Failure("transaction", "new backup changed during rollback")
                path.unlink()
                sync_directory(path.parent)
        for relative in reversed(self.journal.get("created_directories", [])):
            directory = checked_path(self.root, relative)
            if directory.is_dir() and not any(directory.iterdir()):
                directory.rmdir()
                sync_directory(directory.parent)
        remove_managed(self.directory)


def recover(root, log):
    directory = checked_path(root, "McnCraft_Data/" + TX_NAME)
    if not directory.exists():
        log("RECOVERY_NOT_NEEDED")
        return
    try:
        if any(path.is_symlink() for path in directory.rglob("*")):
            raise ValueError("symlink in transaction")
        journal = json.loads((directory / "journal.json").read_text(encoding="utf-8"))
        if journal["format"] != 1 or journal["root"] != str(root) or journal["status"] not in ("active", "committed"):
            raise ValueError("invalid transaction identity")
        names = [entry["name"] for entry in journal["entries"]]
        if len(names) != len(set(names)) or any(name not in TARGETS for name in names):
            raise ValueError("invalid transaction targets")
        if "support" in names and "watchdog" in names:
            raise ValueError("overlapping transaction targets")
        if not isinstance(journal["work_created"], bool) or any(not isinstance(entry["had_old"], bool) for entry in journal["entries"]):
            raise ValueError("invalid transaction flags")
        if not isinstance(journal.get("archive_created", False), bool) or not isinstance(journal.get("relocations", []), list):
            raise ValueError("invalid relocation journal")
        sources, destinations = set(), set()
        for move in journal.get("relocations", []):
            validate_relocation(move["source"], move["destination"])
            if move["source"] in sources or move["destination"] in destinations:
                raise ValueError("duplicate backup relocation")
            sources.add(move["source"])
            destinations.add(move["destination"])
        if not isinstance(journal.get("created_backups", []), list):
            raise ValueError("invalid created-backup journal")
        created_directories = journal.get("created_directories", [])
        if (not isinstance(created_directories, list)
                or any(not isinstance(name, str) or name not in MODULE_DIRECTORIES for name in created_directories)
                or len(created_directories) != len(set(created_directories))):
            raise ValueError("invalid module-directory journal")
        created_names = set()
        for created in journal.get("created_backups", []):
            if not safe_backup_name(created["name"]) or not created["name"].lower().startswith("__internal.") or not re.fullmatch(r"[A-F0-9]{64}", created["sha256"]):
                raise ValueError("invalid created-backup identity")
            if created["name"] in created_names:
                raise ValueError("duplicate created backup")
            created_names.add(created["name"])
        tx = object.__new__(Transaction)
        tx.root, tx.directory, tx.journal = root, directory, journal
        if journal["status"] == "committed":
            remove_managed(directory)
            log("RECOVERY_COMMITTED_CLEANUP_OK")
        else:
            tx.rollback()
            log("RECOVERY_ROLLBACK_OK")
    except (ValueError, KeyError, TypeError, OSError) as error:
        raise Failure("transaction", error) from error


def installed_manifest(root):
    path = checked_path(root, TARGETS["manifest"])
    if not path.is_file():
        raise Failure("manifest")
    values = read_pairs(path.read_text(encoding="utf-8-sig"))
    if values.get("format") not in ("1", "2") or values.get("originalexisted", "").lower() not in ("true", "false"):
        raise Failure("manifest")
    if not re.fullmatch(r"[A-Fa-f0-9]{64}", values.get("installedpayloadsha256", "")):
        raise Failure("manifest")
    scale_fields = [values.get(field, "") for field in SCALE_HASH_FIELDS]
    if any(scale_fields):
        if (not all(re.fullmatch(r"[A-Fa-f0-9]{64}", value) for value in scale_fields)
                or values.get("installedscalemoduleapiversion") != SCALE_API_VERSION
                or values.get("installedscalemodulename") != SCALE_MODULE_NAME):
            raise Failure("manifest", "invalid Scale ownership")
    elif values.get("installedscalemoduleapiversion") or values.get("installedscalemodulename"):
        raise Failure("manifest", "incomplete Scale ownership")
    if values["originalexisted"].lower() == "true":
        name = values.get("backupfilename", "")
        if not re.fullmatch(r"[A-Za-z0-9_.-]+", name) or name in (".", ".."):
            raise Failure("manifest", "invalid backup name")
        if not re.fullmatch(r"[A-Fa-f0-9]{64}", values.get("originalsha256", "")):
            raise Failure("manifest", "invalid backup hash")
    return values


def scale_ownership(root, manifest):
    """Only install-manifest identity proves existing canonical module ownership."""
    if manifest.get(SCALE_HASH_FIELDS[0]):
        return tuple(manifest[field].upper() for field in SCALE_HASH_FIELDS)
    # Manual test installs before the package schema recorded only module SHA/API.
    legacy_hash = manifest.get("scaleruntimepayloadsha256", "")
    legacy_api = manifest.get("scaleruntimeapiversion", "")
    if legacy_hash or legacy_api:
        if legacy_api != SCALE_API_VERSION or not re.fullmatch(r"[A-Fa-f0-9]{64}", legacy_hash):
            raise Failure("manifest", "invalid legacy Scale ownership")
        module = checked_path(root, TARGETS["scale_module"])
        sidecar = checked_path(root, TARGETS["scale_manifest"])
        if not module.is_file() or not sidecar.is_file() or file_hash(module) != legacy_hash.upper():
            raise Failure("changed", "legacy Scale module missing or modified")
        validate_scale_payload(module.read_bytes(), sidecar.read_bytes())
        return legacy_hash.upper(), file_hash(sidecar)
    return None


def check_owned_scale_files(root, ownership):
    if ownership is None:
        return
    for name, digest in zip(("scale_module", "scale_manifest"), ownership):
        path = checked_path(root, TARGETS[name])
        if path.exists() and (not path.is_file() or file_hash(path) != digest):
            raise Failure("changed", "Scale file modified: " + path.name)


def check_scale_install(root, manifest, module, sidecar):
    ownership = scale_ownership(root, manifest)
    check_owned_scale_files(root, ownership)
    if module is not None:
        validate_scale_payload(module, sidecar)
        if ownership is None:
            for name, data in (("scale_module", module), ("scale_manifest", sidecar)):
                path = checked_path(root, TARGETS[name])
                if path.exists():
                    raise Failure("changed", "unowned canonical Scale file: " + path.name)
    return ownership


def probe_installation(root):
    path = checked_path(root, TARGETS["manifest"])
    if not path.exists():
        return {"managed": False, "componentsIntact": False, "scaleModuleOwned": False}
    manifest = installed_manifest(root)
    components = {"loader": manifest["installedpayloadsha256"],
                  "watchdog": manifest.get("installedwatchdogsha256", "")}
    ownership = scale_ownership(root, manifest)
    if ownership is not None:
        components.update(zip(("scale_module", "scale_manifest"), ownership))
    matches = {}
    for name, digest in components.items():
        item = checked_path(root, TARGETS[name])
        matches[name] = bool(digest and item.is_file() and file_hash(item) == digest.upper())
    return {"managed": True, "componentsIntact": all(matches.values()),
            "scaleModuleOwned": ownership is not None, "components": matches}


def original_backup(root, manifest):
    if manifest["originalexisted"].lower() != "true":
        return None
    backup = checked_path(root, "McnCraft_Data/MPatcherForkBackup/" + manifest["backupfilename"])
    if not backup.is_file() or file_hash(backup) != manifest["originalsha256"].upper():
        raise Failure("backup")
    return backup.read_bytes()


def safe_backup_name(name):
    return isinstance(name, str) and bool(re.fullmatch(r"[A-Za-z0-9_.-]+", name)) and name.lower().startswith(("__internal.", "__internal-")) and name.lower().endswith(".dll")


def validate_relocation(source, destination):
    if not isinstance(source, str) or not isinstance(destination, str):
        raise Failure("path", "invalid backup relocation")
    origin, target = source.split("/"), destination.split("/")
    if len(origin) != 3 or origin[:2] != ["McnCraft_Data", "MPatcherForkBackup"] or not safe_backup_name(origin[-1]):
        raise Failure("path", "invalid backup relocation source")
    same_directory = len(target) == 3 and target[:2] == origin[:2]
    history_directory = len(target) == 2 and target[0] == "MPatcherBackupHistory"
    if not (same_directory or history_directory) or not safe_backup_name(target[-1]) or source == destination:
        raise Failure("path", "invalid backup relocation destination")


def known_backups(root, manifest, log):
    known = {}
    def remember(name, digest):
        if not safe_backup_name(name) or not re.fullmatch(r"[A-Fa-f0-9]{64}", digest or ""):
            return
        previous = known.get(name, digest.upper())
        known[name] = digest.upper() if previous == digest.upper() else ""
    if manifest.get("originalexisted", "").lower() == "true":
        remember(manifest["backupfilename"], manifest["originalsha256"])
    pattern = re.compile(r"(?:BACKUP_CREATED|EXTERNAL_LOADER_BACKUP_CREATED|BACKUP_VERIFIED) sha256=([0-9A-Fa-f]{64})(?: file=([A-Za-z0-9_.-]+))?$")
    for name in ("MPatcherInstaller.log", "MPatcherInstallerLinux.log"):
        path = checked_path(root, "logs/" + name)
        if not path.is_file():
            continue
        try:
            with path.open(encoding="utf-8-sig", errors="replace") as stream:
                for line in stream:
                    match = pattern.search(line.strip())
                    if match:
                        digest, filename = match.groups()
                        # 0.1.0 recorded no filename and always used this exact name.
                        if filename is None and name == "MPatcherInstallerLinux.log":
                            filename = "__Internal-" + digest.upper() + ".dll"
                        remember(filename, digest)
        except OSError as error:
            log("BACKUP_HISTORY_LOG_UNREADABLE file=" + name + " message=" + str(error))
    return known


def archive_destination(root, name):
    destination = "MPatcherBackupHistory/" + name
    suffix = 2
    while checked_path(root, destination).exists():
        destination = "MPatcherBackupHistory/" + name[:-4] + "." + str(suffix) + ".dll"
        suffix += 1
    return destination


def repair_backup_names(root, manifest, known, tx, log):
    if manifest["originalexisted"] == "True":
        current = manifest["backupfilename"]
        if not current.lower().startswith("__internal."):
            normalized = "__Internal.original." + manifest["originalsha256"].upper() + ".dll"
            relative = "McnCraft_Data/MPatcherForkBackup/" + normalized
            destination = checked_path(root, relative)
            if destination.exists():
                if not destination.is_file() or file_hash(destination) != manifest["originalsha256"].upper():
                    raise Failure("backup", "normalized backup name conflict")
                relative = archive_destination(root, current)
            tx.relocate("McnCraft_Data/MPatcherForkBackup/" + current, relative)
            manifest["backupfilename"] = normalized
            log("BACKUP_NAME_REPAIRED sha256=" + manifest["originalsha256"].upper() + " old=" + current + " file=" + normalized)
    current = manifest.get("backupfilename", "")
    for name, digest in known.items():
        if name == current or not digest:
            continue
        source = checked_path(root, "McnCraft_Data/MPatcherForkBackup/" + name)
        if not source.exists():
            continue
        if not source.is_file() or file_hash(source) != digest:
            log("BACKUP_HISTORY_RETAINED reason=hash-mismatch file=" + name)
            continue
        destination = archive_destination(root, name)
        tx.relocate("McnCraft_Data/MPatcherForkBackup/" + name, destination)
        log("BACKUP_HISTORY_ARCHIVED sha256=" + digest + " file=" + name + " destination=" + destination)
    if manifest["originalexisted"] == "True":
        original_backup(root, manifest)
        log("BACKUP_VERIFIED sha256=" + manifest["originalsha256"].upper() + " file=" + manifest["backupfilename"])


def install(root, channel, log):
    ensure_game_stopped()
    if checked_path(root, "McnCraft_Data/" + TX_NAME).exists():
        raise Failure("transaction")
    payload = download_payload(channel, log)
    if len(payload) not in (3, 5):
        raise Failure("invalid", "incomplete downloaded Scale payload")
    version, loader, watchdog = payload[:3]
    module, sidecar = payload[3:] if len(payload) == 5 else (None, None)
    if module is not None:
        validate_scale_payload(module, sidecar, version)
    ensure_game_stopped()
    root = validate_game(root)
    current = checked_path(root, TARGETS["loader"])
    manifest_path = checked_path(root, TARGETS["manifest"])
    manifest = installed_manifest(root) if manifest_path.exists() else {}
    module_ownership = check_scale_install(root, manifest, module, sidecar)
    known = known_backups(root, manifest, log)
    current_hash = file_hash(current) if current.exists() else ""
    managed_hashes = {sha256(loader), manifest.get("installedpayloadsha256", "").upper()}
    new_backup = None
    if manifest:
        original_backup(root, manifest)
    if current_hash and current_hash not in managed_hashes:
        directory = checked_path(root, "McnCraft_Data/MPatcherForkBackup")
        directory.mkdir(exist_ok=True)
        name = "__Internal.original." + current_hash + ".dll"
        backup = checked_path(root, "McnCraft_Data/MPatcherForkBackup/" + name)
        if backup.exists() and file_hash(backup) != current_hash:
            raise Failure("backup", name)
        if not backup.exists():
            new_backup = (name, current.read_bytes())
        manifest.update(originalexisted="True", originalsha256=current_hash, backupfilename=name)
    elif not manifest:
        # An unmanaged copy of exactly this loader is adopted, as on Windows.
        manifest.update(originalexisted="False", originalsha256="", backupfilename="")
    manifest["originalexisted"] = "True" if manifest["originalexisted"].lower() == "true" else "False"
    tx = Transaction(root)
    log("INSTALL_BEGIN version=" + version + " channel=" + channel)
    try:
        if new_backup:
            tx.create_backup(*new_backup)
        repair_backup_names(root, manifest, known, tx, log)
        fields = {"Format": "2", "ProductVersion": version, "InstalledPayloadSha256": sha256(loader),
                  "InstalledWatchdogSha256": sha256(watchdog), "InstalledUtc": dt.datetime.now(dt.timezone.utc).isoformat(),
                  "OriginalExisted": manifest["originalexisted"], "BackupFileName": manifest.get("backupfilename", ""),
                  "OriginalSha256": manifest.get("originalsha256", ""), "InstallerPlatform": "Linux", "UpdateChannel": channel}
        if module is not None:
            fields.update(InstalledScaleModuleSha256=sha256(module), InstalledScaleModuleManifestSha256=sha256(sidecar),
                          InstalledScaleModuleApiVersion=SCALE_API_VERSION, InstalledScaleModuleName=SCALE_MODULE_NAME)
        data = ("\n".join(key + "=" + value for key, value in fields.items()) + "\n").encode("utf-8")
        tx.change("loader", loader)
        tx.change("support", watchdog)
        if module is not None:
            tx.change("scale_module", module)
            tx.change("scale_manifest", sidecar)
        elif module_ownership is not None:
            tx.change("scale_module", None)
            tx.change("scale_manifest", None)
        tx.change("manifest", data)
        tx.create_work()
        if file_hash(current) != sha256(loader) or file_hash(root / TARGETS["watchdog"]) != sha256(watchdog):
            raise Failure("invalid", "installed SHA-256 mismatch")
        if module is not None and (file_hash(root / TARGETS["scale_module"]) != sha256(module)
                                   or file_hash(root / TARGETS["scale_manifest"]) != sha256(sidecar)):
            raise Failure("invalid", "installed Scale SHA-256 mismatch")
    except BaseException:
        tx.rollback()
        log("INSTALL_ROLLBACK_OK")
        raise
    tx.commit()
    cleanup_module_directories(root)
    log("INSTALL_OK version=" + version + " loaderSha256=" + sha256(loader) + " watchdogSha256=" + sha256(watchdog))
    log("SCALE_MODULE_INSTALLED api=1 sha256=" + sha256(module) if module is not None else "SCALE_MODULE_NOT_INCLUDED")
    log("LINUX_RUNTIME_PENDING networkAssist=not-installed updates=Linux-installer")


def uninstall(root, log):
    ensure_game_stopped()
    manifest = installed_manifest(root)
    original = original_backup(root, manifest)
    module_ownership = scale_ownership(root, manifest)
    check_owned_scale_files(root, module_ownership)
    loader = checked_path(root, TARGETS["loader"])
    if loader.exists() and file_hash(loader) != manifest["installedpayloadsha256"].upper():
        raise Failure("changed")
    tx = Transaction(root)
    log("UNINSTALL_BEGIN")
    try:
        tx.change("loader", original)
        tx.change("manifest", None)
        watchdog = checked_path(root, TARGETS["watchdog"])
        if watchdog.is_file() and file_hash(watchdog) == manifest.get("installedwatchdogsha256", "").upper():
            tx.change("watchdog", None)
        elif watchdog.exists():
            log("WATCHDOG_RETAINED reason=file-changed")
        if module_ownership is not None:
            tx.change("scale_module", None)
            tx.change("scale_manifest", None)
    except BaseException:
        tx.rollback()
        log("UNINSTALL_ROLLBACK_OK")
        raise
    tx.commit()
    support = root / TARGETS["support"]
    if support.is_dir() and not any(support.iterdir()):
        support.rmdir()
    cleanup_module_directories(root)
    log("UNINSTALL_OK previousLoaderRestored=" + str(original is not None))


def cleanup_module_directories(root):
    for relative in reversed(MODULE_DIRECTORIES):
        directory = checked_path(root, relative)
        if directory.is_dir() and not any(directory.iterdir()):
            directory.rmdir()


def run_operation(action, folder, channel, callback=None):
    if not sys.platform.startswith("linux"):
        raise Failure("platform")
    if os.geteuid() == 0:
        raise Failure("root")
    root = validate_game(folder)
    log = Logger(root, callback)
    log("START installer=" + VERSION + " action=" + action + " game=" + str(root) + " gameSha256=" + GAME_SHA256)
    try:
        if action == "install":
            install(root, channel, log)
        elif action == "uninstall":
            uninstall(root, log)
        elif action == "recover":
            ensure_game_stopped()
            recover(root, log)
    except BaseException as error:
        log("FAILED type=" + type(error).__name__ + " message=" + str(error))
        raise
    return log.path


def gui(args):
    try:
        import tkinter as tk
        from tkinter import filedialog, messagebox, ttk
        window = tk.Tk()
    except (ImportError, RuntimeError) as error:
        raise Failure("tk", error) from error
    except tk.TclError as error:
        raise Failure("tk", error) from error
    window.title("MPatcher Linux " + VERSION)
    window.minsize(780, 470)
    frame = ttk.Frame(window, padding=16)
    frame.pack(fill="both", expand=True)
    frame.columnconfigure(1, weight=1)
    frame.rowconfigure(6, weight=1)
    games = discover_games()
    folder = tk.StringVar(value=args.game_dir or (str(games[0]) if len(games) == 1 else ""))
    language = tk.StringVar(value=args.lang)
    channel = tk.StringVar(value=args.channel)
    status = tk.StringVar()
    labels = {}
    heading = ttk.Label(frame, font=("", 17))
    heading.grid(row=0, column=0, columnspan=3, sticky="w", pady=(0, 14))
    labels["folder"] = ttk.Label(frame)
    labels["folder"].grid(row=1, column=0, sticky="w", padx=(0, 12))
    selector = ttk.Combobox(frame, textvariable=folder, values=[str(game) for game in games])
    selector.grid(row=1, column=1, sticky="ew")
    def browse():
        chosen = filedialog.askdirectory(title=tr("folder", language.get()))
        if chosen:
            folder.set(chosen)
    labels["browse"] = ttk.Button(frame, command=browse)
    labels["browse"].grid(row=1, column=2, padx=(10, 0))
    labels["channel"] = ttk.Label(frame)
    labels["channel"].grid(row=2, column=0, sticky="w", pady=12)
    channels = ttk.Combobox(frame, textvariable=channel, values=("stable", "alpha"), state="readonly", width=14)
    channels.grid(row=2, column=1, sticky="w")
    languages = ttk.Combobox(frame, textvariable=language, values=("ru", "en", "ja"), state="readonly", width=7)
    languages.grid(row=2, column=2, sticky="e")
    controls = ttk.Frame(frame)
    controls.grid(row=3, column=0, columnspan=3, sticky="w", pady=6)
    runtime = ttk.Label(frame, wraplength=750)
    runtime.grid(row=4, column=0, columnspan=3, sticky="w", pady=(8, 4))
    ttk.Label(frame, textvariable=status, wraplength=750).grid(row=5, column=0, columnspan=3, sticky="w", pady=6)
    journal = tk.Text(frame, height=12, wrap="word", state="disabled")
    journal.grid(row=6, column=0, columnspan=3, sticky="nsew")
    events, busy = queue.Queue(), [False]
    def worker(action, selected, selected_channel):
        try:
            path = run_operation(action, selected, selected_channel, lambda line: events.put(("log", line)))
            events.put(("done", str(path)))
        except Exception as error:
            try:
                fallback = Logger()
                fallback("FAILED type=" + type(error).__name__ + " message=" + str(error))
                events.put(("log", "ERROR_LOG " + str(fallback.path)))
            except OSError:
                pass
            events.put(("error", error))
    def start(action):
        if busy[0] or not folder.get().strip():
            if not busy[0]:
                messagebox.showerror(tr("error", language.get()), tr("game", language.get()))
            return
        if action == "uninstall" and not messagebox.askyesno("MPatcher", tr("confirm", language.get())):
            return
        busy[0] = True
        status.set(tr("busy", language.get()))
        for widget in (*buttons, selector, channels, languages, labels["browse"]):
            widget.configure(state="disabled")
        threading.Thread(target=worker, args=(action, folder.get().strip(), channel.get()), daemon=False).start()
    buttons = []
    for key in ("install", "uninstall", "recover"):
        button = ttk.Button(controls, command=lambda action=key: start(action))
        button.pack(side="left", padx=(0, 10))
        labels[key] = button
        buttons.append(button)
    def translate(event=None):
        selected = language.get()
        heading.configure(text=tr("title", selected))
        for key, widget in labels.items():
            widget.configure(text=tr(key, selected))
        runtime.configure(text=tr("runtime", selected))
        status.set(tr("ready", selected))
    languages.bind("<<ComboboxSelected>>", translate)
    def poll():
        try:
            while True:
                kind, value = events.get_nowait()
                if kind == "log":
                    journal.configure(state="normal")
                    journal.insert("end", value + "\n")
                    journal.see("end")
                    journal.configure(state="disabled")
                else:
                    busy[0] = False
                    for widget in (*buttons, selector, labels["browse"]):
                        widget.configure(state="normal")
                    channels.configure(state="readonly")
                    languages.configure(state="readonly")
                    if kind == "done":
                        status.set(tr("done", language.get()) + value)
                    else:
                        details = value.localized(language.get()) if isinstance(value, Failure) else str(value)
                        status.set(tr("error", language.get()))
                        messagebox.showerror(tr("error", language.get()), details)
        except queue.Empty:
            pass
        window.after(100, poll)
    def close():
        if not busy[0]:
            window.destroy()
    window.protocol("WM_DELETE_WINDOW", close)
    translate()
    poll()
    window.mainloop()


def main(argv=None):
    language = (locale.getlocale()[0] or "en").split("_")[0]
    parser = argparse.ArgumentParser(description="MPatcher Linux installer (Arch, Steam/Proton or manual Wine folder)")
    parser.add_argument("--version", action="version", version="MPatcher Linux " + VERSION)
    parser.add_argument("--game-dir", help="folder containing McnCraft.exe")
    parser.add_argument("--channel", choices=("stable", "alpha"), default="stable")
    parser.add_argument("--lang", choices=("ru", "en", "ja", "jp"), default=language if language in ("ru", "ja") else "en")
    actions = parser.add_mutually_exclusive_group()
    for action in ("install", "uninstall", "recover", "list-games", "check"):
        actions.add_argument("--" + action, action="store_true")
    args = parser.parse_args(argv)
    try:
        if args.list_games:
            for root in discover_games():
                print(root)
            return 0
        if args.check:
            if not args.game_dir:
                parser.error("--check requires --game-dir")
            root = validate_game(args.game_dir)
            path = root / TARGETS["manifest"]
            print(json.dumps({"game": str(root), "gameSha256": GAME_SHA256,
                              "installation": installed_manifest(root) if path.exists() else None,
                              "health": probe_installation(root)}, ensure_ascii=False, indent=2))
            return 0
        action = next((item for item in ("install", "uninstall", "recover") if getattr(args, item)), None)
        if action:
            if not args.game_dir:
                games = discover_games()
                if len(games) != 1:
                    parser.error("specify --game-dir; cannot select one game unambiguously")
                args.game_dir = str(games[0])
            path = run_operation(action, args.game_dir, args.channel, print)
            print(tr("done", args.lang) + str(path))
        else:
            gui(args)
        return 0
    except (Failure, OSError, ValueError) as error:
        try:
            Logger()("FAILED type=" + type(error).__name__ + " message=" + str(error))
        except OSError:
            pass
        print(error.localized(args.lang) if isinstance(error, Failure) else str(error), file=sys.stderr)
        return 1


if __name__ == "__main__":
    sys.exit(main())
