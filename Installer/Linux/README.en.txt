MPatcher Linux installer 0.1.2 Alpha — Arch Linux, RU/EN/JP GUI and CLI

Install the Windows version of MachineCraft first (Steam app 397100, McnCraft.exe).
Run the game through Steam/Proton or Wine. The installer itself uses native
Python 3.10+, reads package resources without executing the Windows installer,
and does not modify your compatibility prefix. Native ELF/.so game builds are
not supported.

Arch GUI prerequisites: sudo pacman -Syu python tk
Then extract the archive, close the game and its crash watchdog, and run:
  sh MPatcherInstaller.sh --lang en
Run the installer as your normal user, never with sudo. Select the game folder
and Stable/Alpha channel. Steam libraries, including Flatpak, are discovered;
Wine installations can be selected manually. Python alone is enough for CLI:
  sh MPatcherInstaller.sh --list-games
  sh MPatcherInstaller.sh --install --game-dir "/path/to/MachineCraft"
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/path/to/MachineCraft"
  sh MPatcherInstaller.sh --check --game-dir "/path/to/MachineCraft"
  sh MPatcherInstaller.sh --uninstall --game-dir "/path/to/MachineCraft"
  sh MPatcherInstaller.sh --recover --game-dir "/path/to/MachineCraft"

The online package comes from RestartLive113/MPatcher GitHub releases. Its size,
SHA-256, native loader type and resource layout are verified before installation.
No embedded offline fallback is used. Download failures leave game files intact.
UserData, machines, settings and existing backups are preserved. The previous
loader is stored in McnCraft_Data/MPatcherForkBackup. Format=2 install manifests
are compatible with Windows installer manifests. Removal restores the previous
loader; externally changed loaders are refused and changed watchdogs retained.
MPatcherFork is an installer service directory and is reset on installation;
do not store personal files there.

Recover an interrupted operation with --recover. Keep the transaction directory
McnCraft_Data/.MPatcherLinux.transaction until recovery finishes.
Log: <game>/logs/MPatcherInstallerLinux.log. Early errors use
$XDG_STATE_HOME/MPatcher/logs (default ~/.local/state/MPatcher/logs).

TEST limitations: the McnCraft.exe SHA-256 must match the supported Windows game.
Windows Zapret/WinDivert services are not installed; do not enable Zapret - Legacy
on Linux. Update through this Linux installer: the game's update button launches
a Windows package whose Linux behavior is unverified. Any running MachineCraft
or its watchdog blocks installation; the installer never closes or launches games.
File fixtures were tested on Windows. Arch GUI, Proton/Wine gameplay, watchdog,
two-client Unity Legacy, NTFS/network filesystems, Flatpak permissions and power
loss still require live verification.

0.1.1 wrong DLL fix:
If Linux installer 0.1.0 caused this message, close the game and reinstall from
this updated archive using the same channel, for example Alpha:
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/path/to/MachineCraft" --lang en
The verified current __Internal-<SHA>.dll backup is renamed to
__Internal.original.<SHA>.dll, matching the game's backup filter. Superseded
backups recorded in the install manifest/logs and matching SHA-256 are moved to
<game>/MPatcherBackupHistory outside McnCraft_Data. Restore/uninstall remains
available, and failed operations roll back these moves. Unknown or modified
DLLs remain untouched. Restart the game fully after installation. If the message
persists, provide the latest logs/MPatcher_*.log containing SCRIPT-DLL-COMPAT,
MPATCHER_REJECT and RESULT wrongDll. Do not delete active Mono/Managed DLLs.
The fix passed file fixtures and the original C# policy, not live Arch gameplay.

0.1.2 external Scale module:
The new Alpha package also installs the verified MPatcher.ScaleRuntime.dll and
MPatcher.ScaleRuntime.manifest.ini in McnCraft_Data/MPatcher/Modules. The module
API, managed assembly name, version and SHA-256 are checked before any game-file
change. The loader, module pair and install manifest update in one recoverable
transaction. --check reports missing or changed owned components. Older Stable
and Alpha packages without the module remain supported; switching to one removes
the owned unchanged module pair. Unknown other modules are preserved. Changed
owned files or untracked files at the canonical pair paths stop the operation.
Legacy manual installs require their recorded API1/module hash plus a verified
sidecar before adoption. Uninstall removes only the owned unchanged pair. Native
Linux game execution and Proton/Wine acceptance are still unverified.
