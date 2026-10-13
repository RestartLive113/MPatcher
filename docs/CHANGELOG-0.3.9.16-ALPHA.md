# MPatcher 0.3.9.16 Alpha

Fixes in-game package downloads that failed with `timeout` after 30 seconds while the transfer was still progressing.

The package downloader now waits up to 60 seconds for initial download activity or for increasing progress after a stall, with a 600-second total limit. Polling uses the nonblocking `WWW.progress` value and refreshes the clock after sampling it. Estimated received bytes derived from progress reset only the idle deadline; byte/size getters are not polled during the transfer. The file logger records estimated bytes, expected size, progress and elapsed/idle time every 10 seconds, and identifies first-byte, stall or total timeout failures. Completed downloads report their exact byte count.

The transfer scope removes request state when the download coroutine finishes, throws or is disposed. Retries start with fresh timing and byte state. Existing package size, SHA-256, save and launch guards remain required; timeout failures do not launch an incomplete package.

Fake-clock and cleanup fixtures cover continuous 45/180-second downloads, finite/clamped/monotonic progress estimates, initial activity and stalled-transfer boundaries, the 600-second total limit despite traffic, retry isolation, clock rollback, disposal/completion/exceptions, and production/Alpha manifest validation. These are targeted code checks, not acceptance of every live internet route or a completed in-game self-update.

The BOX/Capsule/Coupler network fixes and SETUP-v3 compatibility from 0.3.9.15 remain included. Multiplayer peers still need 0.3.9.15 or newer. Performance fixes, Scale R10 and the required tracker post-compile step are retained.

An older running updater still has its old timeout. If 0.3.9.14/15 reports `timeout`, download the [Alpha installer](https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe), close MachineCraft, choose the existing game folder and select **Install / Update**. No previous uninstall is needed; UserData and machine files are preserved.
