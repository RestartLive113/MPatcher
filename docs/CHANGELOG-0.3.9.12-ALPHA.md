# 0.3.9.12 Alpha — 2026-10-12

## Changes since public Alpha 0.3.9.3

- Added Scale alongside Sculpt in Build with native UI textures, manual mesh selection, Ctrl add/remove, camera inspection, and English/Japanese text matching the game language.
- Added Coupler-descendant branch selection using native Group/Merge connections; preview stays near the clicked detail. Scale and Sculpt no longer remain active together in native Preview.
- Made factor 1 the initial value and allowed Apply at 1. Removed automatic selection on entering Scale.
- Retained the original Box/Capsule detail when shrinking, through Apply and craft save/load. Fixed rounded Box carrier visibility and zero-height Capsule preview.
- Fixed false pose-restoration failures after fractional scaling and expanded Box positional offsets to ±10000.
- Separated Scale/UI into an external module for faster independent development updates. The package installs the matching module and manifest alongside the prepared core.
- Fixed an orphan loading-screen component that could leave the main menu dark and unclickable after a UI creation failure. The cause of a historical indefinite “not responding” startup is still unconfirmed.
- Added independent Unity Legacy NAT facilitator health/recovery for the `NATTargetNotConnected` case, without resetting the game server or disconnecting current players. External two-client WAN acceptance remains open.
- Changed Magnification Scale physics OFF to use native Magnification physics calculations. Independent MPatcher settings remain active; rebuild the machine after switching.
- At 100%, Scale physics is hidden and forced OFF, even when not visible or when an older enabled value was saved. At other percentages it appears without automatically enabling itself. The user confirmed this behavior in the test game.
- Added optional Linux installer 0.1.2 TEST support for the external module package and its recoverable transaction. This is package/file validation, not live Proton/Wine acceptance.

## Acceptance boundaries

Exact startup, package/resource hashes, installer transaction fixtures and targeted code checks are distinct from full gameplay acceptance. Magnification ON remains experimental. Mixed-size collisions, general suspension/physics equivalence, comprehensive live Undo/Redo, WAN NAT recovery and Linux/Proton gameplay still require broader testing. Peers should use the same new Alpha version because the physics-OFF wire marker now has a new meaning.

The user withdrew the `1H_Jack_1` hat mismatch report after finding a missed mirroring step; no scaler-math change was made for that report. JZZ30 on Hakkone stopped bouncing after Barrel blocks were changed to Dummy 0; the original piston attribution was not confirmed.

Stable 0.3.8 is unchanged. This release updates the rolling Alpha channel only.

For Windows rollback, remove MPatcher with the new Alpha installer before installing old Stable 0.3.8. The old Stable installer does not remove the external Scale module.
