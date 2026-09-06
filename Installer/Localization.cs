using System;
using System.Globalization;

namespace MachineCraftMPatcherInstaller
{
	internal enum InstallerLanguage
	{
		Russian,
		English,
		Japanese
	}

	internal static class InstallerText
	{
		internal static InstallerLanguage Language { get; private set; }

		internal static void UseSystemLanguage()
		{
			string code = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			if (string.Equals(code, "ru", StringComparison.OrdinalIgnoreCase))
				Language = InstallerLanguage.Russian;
			else if (string.Equals(code, "ja", StringComparison.OrdinalIgnoreCase))
				Language = InstallerLanguage.Japanese;
			else
				Language = InstallerLanguage.English;
		}

		internal static void SetLanguage(InstallerLanguage language)
		{
			Language = language;
		}

		internal static bool TrySetLanguageCode(string code)
		{
			if (string.Equals(code, "ru", StringComparison.OrdinalIgnoreCase))
				Language = InstallerLanguage.Russian;
			else if (string.Equals(code, "en", StringComparison.OrdinalIgnoreCase))
				Language = InstallerLanguage.English;
			else if (string.Equals(code, "ja", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(code, "jp", StringComparison.OrdinalIgnoreCase))
				Language = InstallerLanguage.Japanese;
			else
				return false;
			return true;
		}

		private static string Pick(string russian, string english, string japanese)
		{
			if (Language == InstallerLanguage.Russian)
				return russian;
			if (Language == InstallerLanguage.Japanese)
				return japanese;
			return english;
		}

		internal static string Explanation
		{
			get
			{
				return Pick(
					"Устанавливает патчи и исправления для MachineCraft 0.248c.\r\nПоддерживает чистую игру и уже установленный MPatcher; UserData не изменяется.",
					"Installs patches and fixes for MachineCraft 0.248c.\r\nSupports a clean game and an existing MPatcher; UserData is not modified.",
					"MachineCraft 0.248c向けのパッチと修正をインストールします。\r\nMpatcher未適応と適応済どちらも対応します。UserDataは変更しません。");
			}
		}

		internal static string PathLabel { get { return Pick("Папка MachineCraft:", "MachineCraft folder:", "MachineCraftフォルダー:"); } }
		internal static string Browse { get { return Pick("Обзор...", "Browse...", "参照..."); } }
		internal static string InstallOrUpdate { get { return Pick("Установить / Обновить", "Install / Update", "インストール / 更新"); } }
		internal static string UninstallPatcher { get { return Pick("Удалить патчер", "Uninstall MPatcher", "MPatcherを削除"); } }
		internal static string OpenLog { get { return Pick("Открыть лог", "Open log", "ログを開く"); } }
		internal static string Close { get { return Pick("Закрыть", "Close", "閉じる"); } }
		internal static string StatePrefix { get { return Pick("Состояние: ", "Status: ", "状態: "); } }
		internal static string CheckFailedPrefix { get { return Pick("ошибка проверки — ", "check failed — ", "確認エラー — "); } }
		internal static string LogPrefix { get { return Pick("Лог: ", "Log: ", "ログ: "); } }

		internal static string BrowseDescription
		{
			get { return Pick("Выберите папку MachineCraft, содержащую McnCraft.exe", "Select the MachineCraft folder containing McnCraft.exe", "McnCraft.exeを含むMachineCraftフォルダーを選択してください"); }
		}

		internal static string ExistingLoaderConfirmation
		{
			get
			{
				return Pick(
					"Найден существующий MPatcher. Он будет сохранён в резервную копию и заменён этой версией.",
					"An existing MPatcher was found. It will be backed up and replaced with this version.",
					"既存のMPatcherが見つかりました。バックアップを作成してからこのバージョンに置き換えます。");
			}
		}

		internal static string UninstallConfirmation
		{
			get
			{
				return Pick(
					"Удалить патчер? Если раньше был установлен другой MPatcher, будет восстановлена его предыдущая версия.",
					"Uninstall MPatcher? If another MPatcher was installed before it, that previous version will be restored.",
					"MPatcherを削除しますか？以前に別のMPatcherがインストールされていた場合は、そのバージョンを復元します。");
			}
		}

		internal static string LogNotCreated(string path)
		{
			return Pick("Лог ещё не создан: ", "The log has not been created yet: ", "ログはまだ作成されていません: ") + path;
		}

		internal static string GameFolderNotFound { get { return Pick("Папка MachineCraft не найдена", "MachineCraft folder not found", "MachineCraftフォルダーが見つかりません"); } }
		internal static string CurrentVersionInstalled(string version) { return Pick("Установлена текущая версия " + version, "Current version " + version + " is installed", "現在のバージョン " + version + " がインストールされています"); }
		internal static string UpdateAvailable { get { return Pick("Установлена другая версия MPatcher", "Another MPatcher version is installed", "別のバージョンのMPatcherがインストールされています"); } }
		internal static string ExistingMPatcherDetected { get { return Pick("Обнаружен существующий MPatcher", "Existing MPatcher detected", "既存のMPatcherが見つかりました"); } }
		internal static string CleanGameDetected { get { return Pick("Чистая игра без MPatcher", "Clean game without MPatcher", "MPatcherがないクリーンなゲームです"); } }

		internal static string OriginalBackupMissing(string path) { return Pick("Не найдена исходная резервная копия: ", "Original backup not found: ", "元のバックアップが見つかりません: ") + path; }
		internal static string EmbeddedHashMismatch(string hash) { return Pick("Хэш встроенной DLL не совпал: ", "Embedded DLL hash mismatch: ", "埋め込みDLLのハッシュが一致しません: ") + hash; }
		internal static string InstalledHashMismatch(string hash) { return Pick("Проверка установленной DLL не прошла: ", "Installed DLL verification failed: ", "インストール済みDLLの検証に失敗しました: ") + hash; }

		internal static string InstallSuccessTitle { get { return Pick("Установка завершена", "Installation complete", "インストール完了"); } }
		internal static string UninstallSuccessTitle { get { return Pick("Удаление завершено", "Uninstallation complete", "アンインストール完了"); } }
		internal static string InstallFailureTitle { get { return Pick("Установка не выполнена", "Installation failed", "インストール失敗"); } }
		internal static string UninstallFailureTitle { get { return Pick("Удаление не выполнено", "Uninstallation failed", "アンインストール失敗"); } }
		internal static string DownloadFailureTitle { get { return Pick("Не удалось получить MPatcher", "Could not obtain MPatcher", "MPatcherを取得できませんでした"); } }

		internal static string InstallSucceeded(string version, string root,
			int closedGameProcessCount, int forcedGameProcessCount)
		{
			string message = Pick(
				"MPatcher " + version + " успешно установлен.\r\n\r\nПапка игры:\r\n" + root
					+ "\r\n\r\nUserData и сохранённые машины не изменялись.\r\nТеперь можно запустить MachineCraft.",
				"MPatcher " + version + " was installed successfully.\r\n\r\nGame folder:\r\n" + root
					+ "\r\n\r\nUserData and saved machines were not modified.\r\nYou can now start MachineCraft.",
				"MPatcher " + version + " のインストールが正常に完了しました。\r\n\r\nゲームフォルダー:\r\n" + root
					+ "\r\n\r\nUserDataと保存したマシンは変更されていません。\r\nMachineCraftを起動できます。");
			return message + GameClosedSummary(closedGameProcessCount, forcedGameProcessCount);
		}

		internal static string InstallFailed(string details)
		{
			return Pick(
				"Не удалось установить MPatcher. Установка остановлена; если изменение файлов уже началось, установщик выполнил откат.\r\n\r\nПричина: " + details,
				"MPatcher could not be installed. Installation was stopped; if file changes had started, the installer rolled them back.\r\n\r\nReason: " + details,
				"MPatcherをインストールできませんでした。処理は中止され、ファイル変更が始まっていた場合はロールバックされました。\r\n\r\n理由: " + details);
		}

		internal static string UninstallSucceeded(string detail, string root,
			int closedGameProcessCount, int forcedGameProcessCount)
		{
			string message = Pick(
				"MPatcher успешно удалён.\r\n\r\n" + detail + "\r\n\r\nПапка игры:\r\n" + root
					+ "\r\n\r\nUserData и сохранённые машины не изменялись.",
				"MPatcher was uninstalled successfully.\r\n\r\n" + detail + "\r\n\r\nGame folder:\r\n" + root
					+ "\r\n\r\nUserData and saved machines were not modified.",
				"MPatcherのアンインストールが正常に完了しました。\r\n\r\n" + detail + "\r\n\r\nゲームフォルダー:\r\n" + root
					+ "\r\n\r\nUserDataと保存したマシンは変更されていません。");
			return message + GameClosedSummary(closedGameProcessCount, forcedGameProcessCount);
		}

		internal static string UninstallFailed(string details)
		{
			return Pick(
				"Не удалось удалить MPatcher. Операция остановлена, чтобы не повредить файлы игры.\r\n\r\nПричина: " + details,
				"MPatcher could not be uninstalled. The operation was stopped to protect the game files.\r\n\r\nReason: " + details,
				"MPatcherをアンインストールできませんでした。ゲームファイルを保護するため処理を中止しました。\r\n\r\n理由: " + details);
		}

		internal static string UnmanagedPayloadRemoved { get { return Pick("MPatcher удалён; игра возвращена в состояние без MPatcher.", "MPatcher was removed; the game is back to a clean state without MPatcher.", "MPatcherを削除し、MPatcherがない状態に戻しました。"); } }
		internal static string InstallManifestMissing { get { return Pick("Манифест нашей установки не найден; существующий MPatcher не изменён.", "This installer's manifest was not found; the existing MPatcher was not changed.", "このインストーラーのマニフェストが見つかりません。既存のMPatcherは変更されませんでした。"); } }
		internal static string ManifestReadFailed(string path) { return Pick("Не удалось прочитать ", "Could not read ", "読み取れませんでした: ") + path; }
		internal static string LoaderChanged(string hash) { return Pick("__Internal.dll изменён после установки; файл сохранён без изменений. SHA-256: ", "__Internal.dll changed after installation; the file was left unchanged. SHA-256: ", "インストール後に__Internal.dllが変更されています。ファイルは変更しませんでした。SHA-256: ") + hash; }
		internal static string PreviousBackupMissing { get { return Pick("Не найдена резервная копия исходного MPatcher.", "The original MPatcher backup was not found.", "元のMPatcherのバックアップが見つかりません。"); } }
		internal static string BackupDamaged(string hash) { return Pick("Резервная копия повреждена: ", "The backup is damaged: ", "バックアップが破損しています: ") + hash; }
		internal static string PreviousMPatcherRestored { get { return Pick("Текущий MPatcher удалён; предыдущая версия восстановлена.", "The current MPatcher was removed and the previous version was restored.", "現在のMPatcherを削除し、以前のバージョンを復元しました。"); } }
		internal static string CleanGameRestored { get { return Pick("MPatcher удалён; восстановлена чистая игра.", "MPatcher was removed and the clean game state was restored.", "MPatcherを削除し、クリーンなゲーム状態を復元しました。"); } }
		internal static string SelectGameFolder { get { return Pick("Выберите папку MachineCraft, содержащую McnCraft.exe.", "Select the MachineCraft folder containing McnCraft.exe.", "McnCraft.exeを含むMachineCraftフォルダーを選択してください。"); } }
		internal static string CloseSelectedGame { get { return Pick("Закройте выбранную копию MachineCraft перед установкой.", "Close the selected MachineCraft copy before continuing.", "続行する前に、選択したMachineCraftを終了してください。"); } }
		internal static string ClosingGameProcess(int processId)
		{
			return Pick(
				"MachineCraft запущен. Автоматически закрываю игру перед операцией (PID " + processId + ")...",
				"MachineCraft is running. Closing it automatically before the operation (PID " + processId + ")...",
				"MachineCraftが起動中です。処理前に自動終了します (PID " + processId + ")...");
		}
		internal static string GameProcessClosed(bool forced)
		{
			if (forced)
				return Pick("MachineCraft не ответил и был принудительно закрыт.", "MachineCraft did not exit and was force-closed.", "MachineCraftが終了しなかったため、強制終了しました。");
			return Pick("MachineCraft автоматически закрыт.", "MachineCraft was closed automatically.", "MachineCraftを自動的に終了しました。");
		}
		internal static string GameProcessPathUnavailable(int processId)
		{
			return Pick(
				"Обнаружен MachineCraft (PID " + processId + "), но установщик не смог безопасно определить его папку. Закройте игру вручную или запустите установщик от имени администратора.",
				"MachineCraft was detected (PID " + processId + "), but the installer could not safely identify its folder. Close the game manually or run the installer as administrator.",
				"MachineCraft (PID " + processId + ") を検出しましたが、フォルダーを安全に確認できませんでした。ゲームを手動で終了するか、インストーラーを管理者として実行してください。");
		}
		internal static string GameProcessCloseFailed(int processId, string details)
		{
			return Pick(
				"Не удалось автоматически закрыть MachineCraft (PID " + processId + "). Сохраните работу, закройте игру вручную и повторите попытку. Подробности: " + details,
				"Could not close MachineCraft automatically (PID " + processId + "). Save your work, close the game manually, and try again. Details: " + details,
				"MachineCraft (PID " + processId + ") を自動終了できませんでした。作業を保存し、ゲームを手動で終了してから再試行してください。詳細: " + details);
		}
		internal static string GameClosedSummary(int closedGameProcessCount, int forcedGameProcessCount)
		{
			if (closedGameProcessCount <= 0)
				return string.Empty;
			return "\r\n\r\n" + Pick(
				forcedGameProcessCount > 0
					? "Запущенный MachineCraft пришлось принудительно закрыть перед операцией."
					: "Запущенный MachineCraft был автоматически закрыт перед операцией.",
				forcedGameProcessCount > 0
					? "The running MachineCraft process had to be force-closed before the operation."
					: "The running MachineCraft process was closed automatically before the operation.",
				forcedGameProcessCount > 0
					? "起動中のMachineCraftは処理前に強制終了されました。"
					: "起動中のMachineCraftは処理前に自動終了しました。");
		}
		internal static string CrashWatchdogBusy { get { return Pick("Подождите, пока завершится формирование отчёта о краше, и повторите попытку.", "Wait for the crash report to finish, then try again.", "クラッシュレポートの作成が完了してから、もう一度お試しください。"); } }
		internal static string EmbeddedPayloadMissing(string resource) { return Pick("Встроенная DLL не найдена: ", "Embedded DLL not found: ", "埋め込みDLLが見つかりません: ") + resource; }
		internal static string UnsupportedManifest { get { return Pick("Неподдерживаемый формат манифеста установки.", "Unsupported installation manifest format.", "インストールマニフェストの形式がサポートされていません。"); } }

		internal static string BootstrapExplanation
		{
			get
			{
				return Pick(
					"Скачивает последнюю версию MPatcher с GitHub и устанавливает её в MachineCraft.\r\nТребуется подключение к интернету; UserData не изменяется.",
					"Downloads the latest MPatcher from GitHub and installs it into MachineCraft.\r\nAn internet connection is required; UserData is not modified.",
					"GitHubから最新のMPatcherをダウンロードしてMachineCraftにインストールします。\r\nインターネット接続が必要です。UserDataは変更しません。");
			}
		}

		internal static string BootstrapChecking
		{
			get { return Pick("проверка последней версии на GitHub...", "checking the latest GitHub release...", "GitHubの最新バージョンを確認しています..."); }
		}

		internal static string BootstrapDownloading(string version)
		{
			return Pick("загрузка и проверка MPatcher " + version + "...", "downloading and verifying MPatcher " + version + "...", "MPatcher " + version + " をダウンロードして確認しています...");
		}

		internal static string BootstrapNetworkState
		{
			get { return Pick("не удалось подключиться к GitHub", "could not connect to GitHub", "GitHubに接続できませんでした"); }
		}

		internal static string BootstrapNetworkError(string details)
		{
			return Pick(
				"Не удалось подключиться к GitHub и получить последнюю версию MPatcher. Проверьте подключение к интернету и повторите попытку. Ничего не было установлено.\r\n\r\nПодробности: " + details,
				"Could not connect to GitHub and obtain the latest MPatcher release. Check your internet connection and try again. Nothing was installed.\r\n\r\nDetails: " + details,
				"GitHubに接続して最新のMPatcherを取得できませんでした。インターネット接続を確認して、もう一度お試しください。何もインストールされていません。\r\n\r\n詳細: " + details);
		}

		internal static string BootstrapDownloadError(string details)
		{
			return Pick(
				"Не удалось безопасно получить последнюю версию MPatcher. Ничего не было установлено.\r\n\r\nПодробности: " + details,
				"Could not safely obtain the latest MPatcher release. Nothing was installed.\r\n\r\nDetails: " + details,
				"最新のMPatcherを安全に取得できませんでした。何もインストールされていません。\r\n\r\n詳細: " + details);
		}

		internal static string PackageReturnedExitCode(int exitCode)
		{
			return Pick(
				"Внутренний пакет завершился с кодом " + exitCode + " и не передал описание ошибки.",
				"The internal package exited with code " + exitCode + " without providing an error description.",
				"内部パッケージは終了コード " + exitCode + " で終了し、エラーの説明を返しませんでした。");
		}

		internal static string BootstrapOperationError(bool uninstall, string details, string logPath)
		{
			string action = uninstall
				? Pick("удалить", "uninstall", "アンインストール")
				: Pick("установить", "install", "インストール");
			return Pick(
				"Не удалось " + action + " MPatcher.\r\n\r\nПричина: " + details + "\r\n\r\nПодробный лог: " + logPath,
				"MPatcher could not be " + action + "ed.\r\n\r\nReason: " + details + "\r\n\r\nDetailed log: " + logPath,
				action + "に失敗しました。\r\n\r\n理由: " + details + "\r\n\r\n詳細ログ: " + logPath);
		}

		internal static string BootstrapUninstallSucceeded
		{
			get { return Pick("MPatcher удалён.", "MPatcher was uninstalled.", "MPatcherをアンインストールしました。"); }
		}

		internal static string BootstrapBusy
		{
			get { return Pick("Дождитесь завершения текущей проверки или установки.", "Wait for the current check or installation to finish.", "現在の確認またはインストールが完了するまでお待ちください。"); }
		}
	}
}
