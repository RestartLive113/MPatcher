# MPatcher 0.3.9.16 Alpha

Community continuation of MPatcher for MachineCraft 0.248c.

[Download Alpha](https://github.com/RestartLive113/MPatcher/releases/tag/alpha). Stable remains [0.3.8](https://github.com/RestartLive113/MPatcher/releases/latest).

## Install and update

**Missing the MPatcher tab in OPTIONS after installing 0.3.9.12?** Update without opening game settings:

1. Download [MPatcherInstaller.exe from Alpha](https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe).
2. Close MachineCraft, run the installer and choose the folder of your existing MachineCraft installation.
3. Select **Install / Update**, then launch the game normally. There is no need to remove the previous patcher; the installer preserves UserData and machine files.

The Windows bootstrap installer always retrieves the Alpha package and verifies its size and SHA-256. It supports both a clean game and an existing MPatcher installation. 0.3.9.13 restores **OPTIONS → MPatcher**; the unrelated Build/Misc entry keeps its previous behavior.

Once the tab is restored, select **OPTIONS → MPatcher → Update Channel → Alpha** for later updates. To return to Stable 0.3.8 on Windows, first use **Remove patcher** in the new Alpha installer, then install Stable. The old Stable installer cannot clean up the external Scale module, so a direct in-game channel switch is not the supported rollback route. New Scale detail metadata requires this Alpha reader; keep backups of important crafts before trying older versions.

The optional `MPatcherInstaller-Linux-0.1.2-Alpha.tar.gz` asset is TEST support for the updated package format. It uses native Python to install the Windows game package in a Steam/Proton or Wine directory; it is not a native Linux game build. Live Proton/Wine acceptance is still open. See the included language-specific README before using it.

## Package download timeout in 0.3.9.16

The in-game updater previously aborted a package download after 30 seconds even when data was still arriving. Package downloads now allow up to 60 seconds for initial activity and 60 seconds without increasing progress, with a 10-minute total limit. Polling uses the nonblocking progress value. File logs record estimated received bytes, expected size, progress and elapsed/idle time every 10 seconds; completed downloads report the exact byte count. Transfer state is cleaned up when the coroutine completes, fails or is disposed; package size and SHA-256 checks remain required before saving and launching it.

If an updater in 0.3.9.14 or 0.3.9.15 still reports `timeout`, use the manual installer steps above to obtain 0.3.9.16. The fix cannot change the updater already running in an older version.

The updater policy and cleanup fixtures pass, including continuous 45/180-second transfers, bounded monotonic progress estimates, first-byte/stall/total limits, retry isolation and manifest rejection checks. These fixtures do not establish successful download and installation over every live internet connection.

## Shape synchronization in 0.3.9.15

BOX and Capsule could appear in the wrong place or with the wrong size or angle to other players when their expanded parameters were whole numbers. The exact network extension previously selected only blocks with fractional metadata, leaving whole numbers to the game's narrower packed fields.

The new SETUP-v3 structure extension carries complete parameters for those blocks, expanded Coupler offsets, and retained shape detail. The receiving client restores them before generating the remote geometry. Native-fit blocks keep their existing packets; fractional-only records without retained detail keep the same SETUP-v2 bytes. Older SETUP-v1/v2 packets remain readable.

**Use 0.3.9.15 or newer on the host and every participant before using these shapes in multiplayer. 0.3.9.14 and older readers cannot read SETUP-v3.** Use the existing installer update procedure above.

Targeted checks passed 337 assertions, including an optional 24-block reproduction fixture, exact parameter restoration, packet validation, and old SETUP-v2 byte compatibility. These are code and packet checks; live two-client acceptance remains open. User machines and logs are not included in the source or package.

## Performance in 0.3.9.14

Routine physics observers now require the explicit `--mpatcher-magnification-diagnostics` opt-in. The Scale physics UI uses the native scene owner instead of searching the whole scene every gameplay frame. Force diagnostics and inertia bookkeeping allocate less memory; owner checkpoint encoding and joint capture also avoid unnecessary buffers. The tracker prefix no longer decodes its immutable scene string on every physical tick; type initialization remains at the original control point.

Physics formulas, input, network data formats and the 250 ms checkpoint cadence are preserved. Scale R10 and the repaired OPTIONS tab are retained. These changes remove measured patcher overhead. Actual FPS gain depends on the scene, machines and scripts; the reported host improvement is a scenario-specific observation.

When rebuilding the core, follow the required `CorePostCompileSteps` in `ReleaseSources/compiler-inputs.json` after metadata-name restoration. The source list alone does not apply the tracker optimization:

```powershell
pwsh tools/Optimize-TrackerSceneConstant.ps1 -InputAssemblyPath build/restored/MPatchrMain.dll -OutputAssemblyPath build/alpha/MPatchrMain.dll -ProofPath build/alpha/tracker-scene-proof.json
```

The tool checks the exact constant decoder and preserves existing metadata and every unrelated method. Unknown profiles require review.

## Scale

In Build, press **2** to switch between Sculpt and Scale. Select a mesh manually with a short left click. **Ctrl + left click** adds a mesh; **Ctrl + right click** removes it. Holding the left button outside the panel lets you inspect the selection with the camera. Typing in a field does not switch Build actions.

The initial factor is **1**, and Apply accepts 1. Scaling edits shape parameters and Coupler offsets without moving blocks on the Build grid. Box and Capsule retain their original detail when reduced, including after Apply, saving and loading. The Scale interface uses native game textures and English/Japanese according to the game language.

## Magnification physics

At **100%**, Scale physics is hidden and forced **OFF**, including previously saved settings. At another percentage it appears and stays OFF until enabled explicitly.

OFF uses the game's original physics calculations for Magnification while preserving the chosen visual size. Other independent MPatcher settings still apply. Rebuild the machine after changing this setting; an ordinary respawn does not reread it. ON remains experimental. Use the same new Alpha version on all peers when testing multiplayer.

## Validation and limits

The tested game build includes menu input cleanup, mutually exclusive Sculpt/Scale state in Preview, and NAT facilitator recovery for Unity Legacy. The user confirmed the 100% hidden/OFF behavior and its return after changing scale. Package, startup and targeted code checks are recorded separately from gameplay acceptance.

External WAN/NAT recovery, complete physics equivalence, mixed-size collisions, broad suspension behavior, live Undo/Redo, and Linux/Proton gameplay have not received universal acceptance. A reported JZZ30 bounce disappeared after the user moved Barrel blocks to Dummy 0; that does not prove a piston defect or its fix.

## Source and credits

This repository contains the source scope of this Alpha, including the external Scale module and packaging code. Game assemblies, user machines, local configuration, generated binaries and full reference assets are excluded. Building requires separately obtained matching patched-game references and the original loader/resource inputs; this source tree does not redistribute those dependencies.

The core files include the generated overrides actually passed to the tested compiler. They are a release source snapshot rather than the mutable development checkout: use [the compiler input list](ReleaseSources/compiler-inputs.json) when rebuilding, rather than regenerating and compiling a second copy of version attributes or capture hooks.

The new packet regression tool requires a .NET SDK, matching patched-game references and a separately obtained `dnlib.dll`. Supply the dependency paths explicitly:

```powershell
pwsh tools/Test-SetupPrecisionExpandedNetwork.ps1 -AssemblyPath build/alpha/MPatchrMain.dll -ReferenceDirectory path/to/patched-game-references -DnlibPath path/to/dnlib.dll
```

Reports are written under repository `analysis` by default. `-MachineFixturePath` optionally checks a separately supplied reproduction craft; no machine file is bundled.

The updater policy checks compile the same download deadline and cleanup helpers without Unity objects:

```powershell
dotnet run --project tools/MPatcherUpdaterChecks/MPatcherUpdaterChecks.csproj --configuration Release
```

`ReleaseSources/ScaleRuntimeVersion.cs` records the generated attributes of the exact tested module. The module builder generates those attributes itself; it is stored outside the normal ScaleRuntime source glob.

Original MPatcher: Dylan Grinberg (dylanpdx). Original installer credits also name Sanikku. This continuation and redistribution are made with permission from the original author. See [third-party notices](Installer/THIRD-PARTY-NOTICES.txt). Public signing uses recovered public-key metadata and is not an authentic signature by the original author.

[Changes in 0.3.9.16](docs/CHANGELOG-0.3.9.16-ALPHA.md). [Shape synchronization in 0.3.9.15](docs/CHANGELOG-0.3.9.15-ALPHA.md). [Performance changes in 0.3.9.14](docs/CHANGELOG-0.3.9.14-ALPHA.md). [OPTIONS hotfix](docs/CHANGELOG-0.3.9.13-ALPHA.md). [Previous Alpha changes](docs/CHANGELOG-0.3.9.12-ALPHA.md).
