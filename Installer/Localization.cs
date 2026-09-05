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

		internal static string InstallSucceeded(string version)
		{
			return Pick(
				"MPatcher " + version + " установлен.\r\nЛоги и отчёты о крашах: logs",
				"MPatcher " + version + " was installed.\r\nLogs and crash reports: logs",
				"MPatcher " + version + " をインストールしました。\r\nログとクラッシュレポート: logs");
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
		internal static string CrashWatchdogBusy { get { return Pick("Подождите, пока завершится формирование отчёта о краше, и повторите попытку.", "Wait for the crash report to finish, then try again.", "クラッシュレポートの作成が完了してから、もう一度お試しください。"); } }
		internal static string EmbeddedPayloadMissing(string resource) { return Pick("Встроенная DLL не найдена: ", "Embedded DLL not found: ", "埋め込みDLLが見つかりません: ") + resource; }
		internal static string UnsupportedManifest { get { return Pick("Неподдерживаемый формат манифеста установки.", "Unsupported installation manifest format.", "インストールマニフェストの形式がサポートされていません。"); } }
	}
}
