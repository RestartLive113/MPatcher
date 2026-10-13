MPatcher 0.3.9.16 Alpha for MachineCraft 0.248c

Русский
Если после 0.3.9.12 отсутствует вкладка OPTIONS -> MPatcher, обновитесь через установщик:
https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherInstaller.exe
Закройте MachineCraft, запустите MPatcherInstaller.exe, выберите папку уже установленной
игры, содержащую McnCraft.exe, и нажмите «Установить / Обновить» (Install / Update).
Вкладка в игре для этого не нужна. Предварительно удалять старый патчер не нужно.
Установщик скачивает канал Alpha, проверяет размер и SHA-256 пакета, сохраняет UserData
и машины. После установки запускайте игру обычным способом; специальный ярлык не нужен.
0.3.9.16 устраняет прежний общий лимит 30 секунд при скачивании пакета из игры.
Теперь ожидание начального прогресса и его остановки ограничено 60 секундами,
всё скачивание — 10 минутами. Каждые 10 секунд файл-лог показывает оценку числа
байтов по прогрессу и время передачи, после завершения — точное число байтов.
Проверка размера и SHA-256 перед запуском сохраняется.
Если обновление в 0.3.9.14/15 уже выдаёт timeout, обновитесь вручную указанным
выше установщиком. Старый запущенный updater не получает этот фикс автоматически.
Проверки таймеров и очистки пройдены; успешное обновление через каждое реальное
интернет-соединение не заявляется. Сетевые исправления 0.3.9.15 сохранены.
0.3.9.15 исправляет сетевую передачу целых расширенных размеров, смещений и углов
BOX/Capsule, расширенных смещений Coupler и сохранённой детализации форм.
Перед совместной игрой используйте 0.3.9.15 или новее на хосте и у всех участников:
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
is needed. 0.3.9.16 removes the old 30-second total in-game package download limit.
Initial-progress and idle waits are bounded at 60 seconds, with a 10-minute total limit.
File logs report estimated bytes from progress and timing every 10 seconds, with
the exact byte count after completion. Package size and SHA-256
checks remain required before launch. If an updater in 0.3.9.14/15 already reports
timeout, update manually with the installer above; the old running updater cannot
receive this fix itself. Deadline and cleanup fixtures passed; successful updates
over every live internet connection are not claimed. All 0.3.9.15 network fixes remain.
0.3.9.15 fixes transmission of expanded whole-number BOX/Capsule sizes,
offsets and angles, expanded Coupler offsets, and retained shape detail.
Use 0.3.9.15 or newer on the host and every participant before multiplayer:
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
0.3.9.16は、ゲーム内のパッケージ取得にあった合計30秒の制限を修正します。
進捗開始と進捗停止の待機は60秒、ダウンロード全体は10分までです。
10秒ごとに進捗から推定したバイト数と経過時間をログへ記録し、完了時に正確なバイト数を記録します。
起動前のサイズとSHA-256確認は維持されます。
0.3.9.14/15の更新でtimeoutが出る場合、上記インストーラーで手動更新してください。
実行中の古いアップデーターには、この修正を自動適用できません。
タイマーと後処理のテストは合格しました。すべての実際のインターネット接続での更新は未確認です。
0.3.9.15のネットワーク修正は維持されます。
0.3.9.15は、BOX/Capsuleの拡張された整数のサイズ・位置オフセット・角度、
Couplerの拡張オフセット、および保持された形状の詳細度のネットワーク送信を修正します。
マルチプレイでは、ホストと全参加者に0.3.9.15以降を使用してください。
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
