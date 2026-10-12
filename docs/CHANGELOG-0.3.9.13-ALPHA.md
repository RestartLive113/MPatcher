# 0.3.9.13 Alpha — 2026-10-12

## Fix since 0.3.9.12

- Restored the native **OPTIONS → MPatcher** tab. The previous shared launcher filter hid this working tab along with the Build/Misc launcher. It now preserves the native Option-scene tab and its original callback; the Build/Misc behavior is unchanged.
- Added direct recovery instructions for users who cannot access the in-game updater because the tab is missing.

## Updating when the tab is missing

1. Download [MPatcherInstaller.exe from the Alpha release](https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe).
2. Close MachineCraft, run the installer and select the folder of the existing game installation.
3. Choose **Install / Update** and launch the game again. The installer preserves UserData and machine files; uninstalling the previous patcher is unnecessary.

The Windows bootstrap uses the Alpha update manifest directly, so the missing game tab cannot block this route. After updating, use **OPTIONS → MPatcher** for settings and future update-channel selection.

## Scope and checks

The source behavior change is limited to the launcher policy. Targeted policy fixtures, native Unity settings-tab lifecycle checks and isolated startup checks passed. Scale R10, the other patches and the installer transaction logic are unchanged. This update does not extend the physics, multiplayer, Undo/Redo or Proton/Wine acceptance of [0.3.9.12](CHANGELOG-0.3.9.12-ALPHA.md).

Stable 0.3.8 is unchanged. This release updates only the rolling Alpha channel.
