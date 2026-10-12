MPatcher Linux インストーラー 0.1.2 Alpha — Arch Linux

先に Windows 版 MachineCraft (Steam app 397100, McnCraft.exe) をインストールし、
Steam/Proton または Wine で起動してください。インストーラー自体は Python 3.10+
で動作します。Wine/.NET は不要で、互換環境の prefix は変更しません。
ネイティブ Linux (ELF/.so) 版ゲームには対応していません。

Arch の GUI 用依存関係:
  sudo pacman -Syu python tk
アーカイブを展開し、ゲームと watchdog を終了してから起動します:
  sh MPatcherInstaller.sh --lang ja
インストーラーには sudo を使わないでください。ゲームのフォルダーと Stable/Alpha
を選択します。Steam のライブラリを自動検索します。Wine は手動選択できます。

CLI (tk 不要):
  sh MPatcherInstaller.sh --list-games
  sh MPatcherInstaller.sh --install --game-dir "/path/to/MachineCraft" --lang ja
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/path/to/MachineCraft" --lang ja
  sh MPatcherInstaller.sh --check --game-dir "/path/to/MachineCraft"
  sh MPatcherInstaller.sh --uninstall --game-dir "/path/to/MachineCraft" --lang ja
  sh MPatcherInstaller.sh --recover --game-dir "/path/to/MachineCraft" --lang ja

GitHub の公式 MPatcher パッケージを取得し、サイズと SHA-256 を検証します。
Windows インストーラーの EXE は実行せず、埋め込み DLL と watchdog を読み取ります。
接続や検証に失敗した場合、ゲームファイルは変更しません。オフライン版は含みません。
UserData、マシン、設定、既存バックアップは保持します。以前の DLL は
McnCraft_Data/MPatcherForkBackup に保存し、削除時に復元します。外部で変更された
loader の削除は中止し、変更された watchdog は保持します。MPatcherFork は
インストーラー用ディレクトリで、インストール時に再作成します。

中断した処理は --recover で復元します。完了までは
McnCraft_Data/.MPatcherLinux.transaction を削除しないでください。
ログ: <game>/logs/MPatcherInstallerLinux.log
初期エラー: ~/.local/state/MPatcher/logs (または $XDG_STATE_HOME)。

TEST の制限: 対応する McnCraft.exe の SHA-256 が必要です。Windows 用
Zapret/WinDivert サービスはインストールしません。Linux では Zapret - Legacy を
有効にしないでください。更新はこの Linux インストーラーを使ってください。
ゲーム内の更新ボタンは Windows パッケージを起動するため、Linux で未検証です。
Windows 上のファイルテストは完了していますが、Arch の GUI、Proton/Wine のゲーム、
watchdog、Unity Legacy の 2 クライアント、NTFS、Flatpak 権限、電源断は未検証です。

0.1.1 の wrong DLL 修正:
Linux インストーラー 0.1.0 の後にこのメッセージが出た場合、ゲームを終了し、
同じチャンネルで新しいアーカイブから再インストールしてください。例:
  sh MPatcherInstaller.sh --install --channel alpha --game-dir "/path/to/MachineCraft" --lang ja
検証済みの __Internal-<SHA>.dll バックアップを __Internal.original.<SHA>.dll
に変更します。記録と SHA-256 が一致する古いバックアップは、McnCraft_Data の
外側の MPatcherBackupHistory に移動します。削除と復元は保持し、失敗時は
変更を取り消します。不明または変更された DLL は移動しません。ゲームを
完全に再起動してください。エラーが続く場合は、最新の logs/MPatcher_*.log
の SCRIPT-DLL-COMPAT、MPATCHER_REJECT、RESULT wrongDll を確認してください。
Mono/Managed の有効な DLL は削除しないでください。Arch の実機動作は未確認です。

0.1.2: 外部 Scale モジュール
新しい Alpha は MPatcher.ScaleRuntime.dll と
MPatcher.ScaleRuntime.manifest.ini を McnCraft_Data/MPatcher/Modules に
インストールします。ゲームの変更前に API、managed アセンブリ名、バージョンと
SHA-256 を確認します。ローダー、モジュールの2ファイルとインストール情報は
同じ復元可能なトランザクションで更新します。--check は欠落または変更された
構成要素を報告します。モジュールを含まない旧 Stable/Alpha パッケージにも
対応し、切り替える際は所有情報とハッシュが一致する2ファイルのみ削除します。
他のモジュールは保持します。正規パスの不明なファイルや外部で変更された
所有ファイルがあれば、書き込み前に処理を中止します。旧手動インストールは
記録済み API1 と DLL ハッシュ、および検証済み sidecar が必要です。
アンインストールにもハッシュ一致が必要です。Arch/Proton/Wine の実機確認は
まだ行われていません。
