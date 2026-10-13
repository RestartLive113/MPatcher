# MPatcher 0.3.9.15 Alpha

Fixes BOX/Capsule parameters that looked correct to their owner but appeared with the wrong position, size or angle to other players.

The previous network capture required fractional SETUP metadata. Expanded whole-number values therefore stayed in the game's packed fields: a size of 1000 could arrive as 232, an offset of 10000 as -240, and an X/Z angle of 180 as -76. Offset overflow could also change later axes in the same packed word.

The exact structure capture now includes values that cannot round-trip through those fields, expanded Coupler offsets, and retained shape detail. SETUP-v3 carries the complete action arrays and optional detail metadata; the receiver restores them before generating the remote geometry. Existing save metadata and the Build grid remain unchanged.

Native-fit blocks keep their existing native packets. Fractional-only blocks without retained detail keep byte-identical SETUP-v2 packets. SETUP-v1 hundredths and SETUP-v2 thousandths remain readable. The native Photon sampling/application route is retained; Legacy precision motion keeps its existing owner/server relay.

**The host and every participant must update to 0.3.9.15. Alpha 0.3.9.14 and older readers do not understand SETUP-v3.**

Targeted validation passed 337 assertions: unsigned size and signed angle boundaries, all three offset lanes, expanded integer-only records, retained detail and Capsule family, exact arrays/fractions before geometry creation, Coupler initial poses, deterministic packets, old v2 bytes, malformed data, and transport guards. An optional 24-block reproduction fixture confirms that two BOX and one Coupler are selected without fractional metadata. The fixture itself, user machines and logs are excluded.

These are static and packet-level checks. Live two-client multiplayer acceptance remains open. Prior performance fixes, the required tracker post-compile optimization, Scale R10 and the OPTIONS tab repair are retained.

Update through **OPTIONS → MPatcher → Update Channel → Alpha**, or download the [Alpha installer](https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe), close MachineCraft, choose the existing game folder and select **Install / Update**. No previous uninstall is needed; UserData and machine files are preserved.
