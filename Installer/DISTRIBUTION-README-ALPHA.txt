MPatcher 0.3.9.15 Alpha for MachineCraft 0.248c

Русский
Если после 0.3.9.12 отсутствует вкладка OPTIONS -> MPatcher, обновитесь через установщик:
https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe
Закройте MachineCraft, запустите MPatcherInstaller.exe, выберите папку уже установленной
игры, содержащую McnCraft.exe, и нажмите «Установить / Обновить» (Install / Update).
Вкладка в игре для этого не нужна. Предварительно удалять старый патчер не нужно.
Установщик скачивает канал Alpha, проверяет размер и SHA-256 пакета, сохраняет UserData
и машины. После установки запускайте игру обычным способом; специальный ярлык не нужен.
0.3.9.15 исправляет сетевую передачу целых расширенных размеров, смещений и углов
BOX/Capsule, расширенных смещений Coupler и сохранённой детализации форм.
Перед совместной игрой обновите хост и всех участников до 0.3.9.15:
0.3.9.14 и более старые версии не читают новые пакеты SETUP-v3.
Прежние SETUP-v1/v2 читаются, штатные значения сохраняют прежние пакеты.
Прошли 337 проверок кода/пакетов; живая двухклиентная приёмка ещё не завершена.
0.3.9.14 сокращает служебную нагрузку физической диагностики, поиска редактора,
наведения и снимков восстановления. Расчёты физики, формат данных и частота
сохранения восстановительных снимков сохранены. Подробная физическая диагностика:
--mpatcher-magnification-diagnostics. Исправленная OPTIONS -> MPatcher и Scale R10 сохранены.
Для следующих обновлений: OPTIONS -> MPatcher -> Update Channel -> Alpha.
Для возврата к Stable в Windows сначала выберите «Удалить патчер» (Remove patcher)
в новом Alpha-установщике, затем установите Stable 0.3.8. Старый Stable-установщик
не удаляет внешний модуль Scale; прямое переключение канала не является способом отката.
Перед тестированием и загрузкой машин в старых версиях сохраните копии важных крафтов.
Полная приёмка физики, внешнего NAT, Undo/Redo и Proton/Wine не заявляется.

English
Missing OPTIONS -> MPatcher after 0.3.9.12? Update without the game settings tab:
https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe
Close MachineCraft, run MPatcherInstaller.exe, choose your existing game folder
containing McnCraft.exe, and select Install / Update. No previous uninstall is needed.
The installer downloads Alpha, verifies package size and SHA-256, and preserves
UserData and machine files. Launch normally after installation; no special shortcut
is needed. 0.3.9.15 fixes transmission of expanded whole-number BOX/Capsule sizes,
offsets and angles, expanded Coupler offsets, and retained shape detail.
Update the host and every participant to 0.3.9.15 before multiplayer:
0.3.9.14 and older versions cannot read the new SETUP-v3 packets.
Old SETUP-v1/v2 packets remain readable; native-fit values keep existing packets.
337 code/packet assertions passed; live two-client acceptance remains open.
0.3.9.14 reduces physics-observer, editor-discovery, tracker-decoder and
recovery-snapshot overhead. Physics calculations, data formats and checkpoint cadence
are preserved. Detailed physics diagnostics: --mpatcher-magnification-diagnostics.
The repaired OPTIONS -> MPatcher tab and Scale R10 are retained.
For later updates: OPTIONS -> MPatcher -> Update Channel -> Alpha.
For Windows rollback, first use Remove patcher in the new Alpha installer, then
install Stable 0.3.8. The old Stable installer does not remove the external Scale
module; a direct in-game channel switch is not the supported rollback route.
Back up important crafts before testing or loading them in older versions.
Universal physics, external NAT, Undo/Redo and Proton/Wine acceptance is not claimed.

日本語
0.3.9.12以降でOPTIONS -> MPatcherが表示されない場合、設定タブなしで更新できます。
https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe
ゲームを終了し、MPatcherInstaller.exeを起動してください。McnCraft.exeを含む
既存のMachineCraftフォルダーを選び、「インストール / 更新」(Install / Update)を実行します。
以前のパッチャーを削除する必要はありません。インストーラーはAlphaを取得し、
サイズとSHA-256を確認します。更新後、通常の方法でゲームを起動してください。
Mpatcher未適応と適応済どちらも対応します。UserDataは変更しません。
0.3.9.15は、BOX/Capsuleの拡張された整数のサイズ・位置オフセット・角度、
Couplerの拡張オフセット、および保持された形状の詳細度のネットワーク送信を修正します。
マルチプレイ前に、ホストと全参加者を0.3.9.15へ更新してください。
0.3.9.14以前のバージョンは新しいSETUP-v3パケットを読み取れません。
既存のSETUP-v1/v2は読み取れます。標準範囲の値は従来のパケットを維持します。
コードとパケットの337項目の検証に合格しました。実際の2クライアントでの確認は未完了です。
0.3.9.14は物理診断、エディター検索、トラッカーの文字列処理、復元スナップショットの負荷を削減します。
物理計算、データ形式、スナップショット保存間隔は維持されます。
詳細な物理診断は --mpatcher-magnification-diagnostics で有効にできます。
修復されたOPTIONS -> MPatcherとScale R10は維持されます。
次回の更新はOPTIONS -> MPatcher -> Update Channel -> Alphaから行えます。
WindowsでStableへ戻す場合、新しいAlphaインストーラーの「パッチャーを削除」
(Remove patcher)で削除してからStable 0.3.8をインストールしてください。
古いStableインストーラーは外部Scaleモジュールを削除しないため、
ゲーム内のチャンネル切り替えだけでは安全に戻せません。
テスト前や古いバージョンで機体を読み込む前に、重要な機体をバックアップしてください。
物理挙動、外部NAT、Undo/Redo、Proton/Wineの全面的な動作確認は完了していません。
