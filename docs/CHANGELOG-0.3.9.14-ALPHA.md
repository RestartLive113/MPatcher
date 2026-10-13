# MPatcher 0.3.9.14 Alpha

This release reduces patcher overhead while retaining physics calculations, input,
checkpoint cadence and network/save formats.

- Detailed Magnification physics observers now require `--mpatcher-magnification-diagnostics`; ordinary file logging remains enabled.
- The persistent Scale physics UI reads the native scene owner instead of searching all objects every gameplay frame.
- Inertia bookkeeping reuses records for the same physical body; native ID reuse remains guarded by exact managed identity.
- Zero/NaN force diagnostics skip an unused string allocation while retaining call accounting.
- Owner checkpoint encoding preallocates its buffer and calculates the same checksum without an intermediate full copy.
- Joint snapshots preallocate their existing lists while keeping the same native reads and float storage order.
- The tracker prefix uses its immutable `Build` string without repeated assembly/reflection/UTF-8 decoding. Constructor execution and failure behavior remain at the original point.

Scale R10, API1 and the OPTIONS tab fix are retained. The package contains the same
prepared game image and existing patch plans as the tested candidate. User scripts
and machines are not included or modified by this release.

The source compiler manifest records the required post-compile tracker optimization.
The optimizer refuses unrecognized decoder profiles and preserves existing tokens,
attributes, resources and unrelated method bodies.

Validation covers static IL/metadata equivalence, codecs, joint-capture values,
constructor behavior, actual Unity Mono tracker invocation and isolated healthy
startup. A reported host FPS increase is a specific observed scenario, not a universal
FPS claim. Multiplayer physics, arbitrary scripts and Proton/Wine still need their
own workload-specific checks.

Update through **OPTIONS → MPatcher → Update Channel → Alpha**, or download the
[Alpha bootstrap installer](https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe),
close MachineCraft, choose the existing game folder and select **Install / Update**.
No previous uninstall is needed. The installer preserves UserData and machine files.
