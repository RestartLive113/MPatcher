using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class BootstrapForm : Form
	{
		private readonly Uri manifestUri;
		private readonly Label titleLabel;
		private readonly Label explanationLabel;
		private readonly Label pathLabel;
		private readonly TextBox pathBox;
		private readonly Button browseButton;
		private readonly Label stateLabel;
		private readonly TextBox activityBox;
		private readonly Button installButton;
		private readonly Button uninstallButton;
		private readonly Button logButton;
		private readonly Button closeButton;
		private readonly ComboBox languageBox;
		private BootstrapManifest manifest;
		private bool busy;
		private string lastError;

		internal BootstrapForm(string initialPath, Uri manifestUri)
		{
			this.manifestUri = manifestUri;
			Text = BootstrapInfo.ProductName + " Installer";
			StartPosition = FormStartPosition.CenterScreen;
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = true;
			ClientSize = new Size(680, 365);
			Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

			titleLabel = new Label();
			titleLabel.Font = new Font(Font.FontFamily, 14F, FontStyle.Bold);
			titleLabel.AutoSize = true;
			titleLabel.Location = new Point(18, 16);
			Controls.Add(titleLabel);

			Label languageLabel = new Label();
			languageLabel.Text = "RU / EN / JP";
			languageLabel.AutoSize = true;
			languageLabel.Location = new Point(457, 23);
			Controls.Add(languageLabel);

			languageBox = new ComboBox();
			languageBox.DropDownStyle = ComboBoxStyle.DropDownList;
			languageBox.Items.AddRange(new object[] { "Русский", "English", "日本語" });
			languageBox.Location = new Point(542, 18);
			languageBox.Size = new Size(118, 25);
			languageBox.SelectedIndex = (int)InstallerText.Language;
			languageBox.SelectedIndexChanged += LanguageChanged;
			Controls.Add(languageBox);

			explanationLabel = new Label();
			explanationLabel.AutoSize = false;
			explanationLabel.Location = new Point(20, 52);
			explanationLabel.Size = new Size(640, 38);
			Controls.Add(explanationLabel);

			pathLabel = new Label();
			pathLabel.AutoSize = true;
			pathLabel.Location = new Point(20, 94);
			Controls.Add(pathLabel);

			pathBox = new TextBox();
			pathBox.Location = new Point(20, 114);
			pathBox.Size = new Size(548, 24);
			pathBox.Text = initialPath ?? string.Empty;
			Controls.Add(pathBox);

			browseButton = new Button();
			browseButton.Location = new Point(576, 112);
			browseButton.Size = new Size(84, 27);
			browseButton.Click += BrowseButtonClick;
			Controls.Add(browseButton);

			stateLabel = new Label();
			stateLabel.AutoSize = false;
			stateLabel.Location = new Point(20, 146);
			stateLabel.Size = new Size(640, 40);
			Controls.Add(stateLabel);

			activityBox = new TextBox();
			activityBox.Location = new Point(20, 184);
			activityBox.Size = new Size(640, 95);
			activityBox.Multiline = true;
			activityBox.ReadOnly = true;
			activityBox.ScrollBars = ScrollBars.Vertical;
			Controls.Add(activityBox);

			installButton = new Button();
			installButton.Location = new Point(20, 296);
			installButton.Size = new Size(180, 38);
			installButton.Click += InstallButtonClick;
			Controls.Add(installButton);

			uninstallButton = new Button();
			uninstallButton.Location = new Point(210, 296);
			uninstallButton.Size = new Size(180, 38);
			uninstallButton.Click += UninstallButtonClick;
			Controls.Add(uninstallButton);

			logButton = new Button();
			logButton.Location = new Point(400, 296);
			logButton.Size = new Size(120, 38);
			logButton.Click += LogButtonClick;
			Controls.Add(logButton);

			closeButton = new Button();
			closeButton.Location = new Point(530, 296);
			closeButton.Size = new Size(130, 38);
			closeButton.Click += delegate { Close(); };
			Controls.Add(closeButton);

			pathBox.TextChanged += delegate { RefreshState(); };
			Shown += delegate { BeginManifestCheck(null); };
			FormClosing += BootstrapFormClosing;
			ApplyLocalizedText();
			RefreshState();
		}

		private void LanguageChanged(object sender, EventArgs e)
		{
			if (languageBox.SelectedIndex < 0)
				return;
			InstallerText.SetLanguage((InstallerLanguage)languageBox.SelectedIndex);
			ApplyLocalizedText();
			RefreshState();
		}

		private void ApplyLocalizedText()
		{
			string version = manifest == null ? string.Empty : " " + manifest.VersionText;
			titleLabel.Text = BootstrapInfo.ProductName + version;
			Text = BootstrapInfo.ProductName + " Installer" + version;
			explanationLabel.Text = InstallerText.BootstrapExplanation;
			pathLabel.Text = InstallerText.PathLabel;
			browseButton.Text = InstallerText.Browse;
			installButton.Text = InstallerText.InstallOrUpdate;
			uninstallButton.Text = InstallerText.UninstallPatcher;
			logButton.Text = InstallerText.OpenLog;
			closeButton.Text = InstallerText.Close;
		}

		private void BrowseButtonClick(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.Description = InstallerText.BrowseDescription;
				dialog.ShowNewFolderButton = false;
				if (Directory.Exists(pathBox.Text))
					dialog.SelectedPath = pathBox.Text;
				if (dialog.ShowDialog(this) == DialogResult.OK)
					pathBox.Text = dialog.SelectedPath;
			}
		}

		private void InstallButtonClick(object sender, EventArgs e)
		{
			BeginManifestCheck(BootstrapAction.Install);
		}

		private void UninstallButtonClick(object sender, EventArgs e)
		{
			BeginManifestCheck(BootstrapAction.Uninstall);
		}

		private void BeginManifestCheck(BootstrapAction? nextAction)
		{
			if (busy)
				return;
			busy = true;
			lastError = string.Empty;
			manifest = null;
			activityBox.Clear();
			AppendActivity("BOOTSTRAP_CHECK_BEGIN url=" + manifestUri.AbsoluteUri);
			RefreshState();
			string gameRoot = pathBox.Text;

			BackgroundWorker worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.DoWork += delegate(object sender, DoWorkEventArgs e)
			{
				BackgroundWorker active = (BackgroundWorker)sender;
				e.Result = BootstrapClient.FetchManifest(manifestUri, gameRoot,
					delegate(string message) { active.ReportProgress(0, message); });
			};
			worker.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
			{
				AppendActivity(e.UserState as string);
			};
			worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				busy = false;
				if (e.Error != null)
				{
					ShowBootstrapError(e.Error, null);
					RefreshState();
					return;
				}
				manifest = (BootstrapManifest)e.Result;
				AppendActivity("BOOTSTRAP_READY version=" + manifest.VersionText);
				ApplyLocalizedText();
				RefreshState();
				if (nextAction.HasValue)
					BeginOperation(nextAction.Value);
			};
			worker.RunWorkerAsync();
		}

		private void BeginOperation(BootstrapAction action)
		{
			if (busy || manifest == null)
				return;
			BootstrapGameState state = BootstrapGameLocator.Probe(pathBox.Text);
			if (!state.ValidGame)
			{
				MessageBox.Show(this, InstallerText.SelectGameFolder, BootstrapInfo.ProductName,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (action == BootstrapAction.Install && state.LoaderExists && !state.ManagedInstall)
			{
				DialogResult choice = MessageBox.Show(this, InstallerText.ExistingLoaderConfirmation,
					BootstrapInfo.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (choice != DialogResult.OK)
					return;
			}
			if (action == BootstrapAction.Uninstall)
			{
				DialogResult choice = MessageBox.Show(this, InstallerText.UninstallConfirmation,
					BootstrapInfo.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (choice != DialogResult.OK)
					return;
			}

			string gameRoot = Path.GetFullPath(pathBox.Text.Trim());
			BootstrapManifest selectedManifest = manifest;
			InstallerLanguage selectedLanguage = InstallerText.Language;
			busy = true;
			lastError = string.Empty;
			RefreshState();
			BackgroundWorker worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.DoWork += delegate(object sender, DoWorkEventArgs e)
			{
				BackgroundWorker active = (BackgroundWorker)sender;
				e.Result = BootstrapClient.DownloadAndExecute(selectedManifest, gameRoot, action,
					selectedLanguage, delegate(string message) { active.ReportProgress(0, message); }, string.Empty);
			};
			worker.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
			{
				AppendActivity(e.UserState as string);
			};
			worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				busy = false;
				if (e.Error != null)
				{
					ShowBootstrapError(e.Error, action);
					RefreshState();
					return;
				}
				BootstrapOperationResult result = (BootstrapOperationResult)e.Result;
				string message = result.Message;
				if (string.IsNullOrWhiteSpace(message))
				{
					message = action == BootstrapAction.Install
						? InstallerText.InstallSucceeded(result.Version, gameRoot, 0, 0)
						: InstallerText.BootstrapUninstallSucceeded;
				}
				message += InstallerText.GameClosedSummary(result.ClosedGameProcessCount,
					result.ForcedGameProcessCount);
				string title = action == BootstrapAction.Install
					? InstallerText.InstallSuccessTitle : InstallerText.UninstallSuccessTitle;
				MessageBox.Show(this, message + "\r\n\r\n" + InstallerText.LogPrefix + result.LogPath,
					title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				RefreshState();
			};
			worker.RunWorkerAsync();
		}

		private void RefreshState()
		{
			BootstrapGameState state = BootstrapGameLocator.Probe(pathBox.Text);
			string description;
			if (busy)
				description = manifest == null ? InstallerText.BootstrapChecking : InstallerText.BootstrapDownloading(manifest.VersionText);
			else if (!string.IsNullOrEmpty(lastError))
				description = lastError;
			else if (!state.ValidGame)
				description = InstallerText.GameFolderNotFound;
			else if (manifest == null)
				description = InstallerText.BootstrapChecking;
			else if (state.ManagedInstall && string.Equals(state.InstalledVersion, manifest.VersionText, StringComparison.OrdinalIgnoreCase))
				description = InstallerText.CurrentVersionInstalled(manifest.VersionText);
			else if (state.ManagedInstall)
				description = InstallerText.UpdateAvailable;
			else if (state.LoaderExists)
				description = InstallerText.ExistingMPatcherDetected;
			else
				description = InstallerText.CleanGameDetected;
			stateLabel.Text = InstallerText.StatePrefix + description;
			installButton.Enabled = !busy && state.ValidGame;
			uninstallButton.Enabled = !busy && state.ValidGame && state.LoaderExists;
			browseButton.Enabled = !busy;
			languageBox.Enabled = !busy;
			closeButton.Enabled = !busy;
		}

		private void ShowBootstrapError(Exception error, BootstrapAction? action)
		{
			BootstrapClient.ReportFailure(pathBox.Text, error);
			string message;
			string title = InstallerText.DownloadFailureTitle;
			if (error is BootstrapNetworkException)
			{
				lastError = InstallerText.BootstrapNetworkState;
				message = InstallerText.BootstrapNetworkError(error.Message);
			}
			else if (error is BootstrapChildException)
			{
				BootstrapChildException child = (BootstrapChildException)error;
				bool uninstall = child.Action == BootstrapAction.Uninstall;
				title = uninstall ? InstallerText.UninstallFailureTitle : InstallerText.InstallFailureTitle;
				lastError = title;
				string logPath = string.IsNullOrWhiteSpace(child.LogPath)
					? BootstrapGameLocator.GetInstallerLogPath(pathBox.Text) : child.LogPath;
				message = InstallerText.BootstrapOperationError(uninstall, child.Details, logPath);
			}
			else if (error is GameProcessCloseException)
			{
				bool uninstall = action.HasValue && action.Value == BootstrapAction.Uninstall;
				title = uninstall ? InstallerText.UninstallFailureTitle : InstallerText.InstallFailureTitle;
				lastError = title;
				message = InstallerText.BootstrapOperationError(uninstall, error.Message,
					BootstrapGameLocator.GetInstallerLogPath(pathBox.Text));
			}
			else
			{
				lastError = InstallerText.CheckFailedPrefix + error.Message;
				message = InstallerText.BootstrapDownloadError(error.Message);
			}
			AppendActivity("BOOTSTRAP_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void AppendActivity(string message)
		{
			if (!string.IsNullOrEmpty(message))
				activityBox.AppendText(message + Environment.NewLine);
		}

		private void LogButtonClick(object sender, EventArgs e)
		{
			string path = BootstrapGameLocator.GetInstallerLogPath(pathBox.Text);
			if (!File.Exists(path))
			{
				MessageBox.Show(this, InstallerText.LogNotCreated(path), BootstrapInfo.ProductName,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
		}

		private void BootstrapFormClosing(object sender, FormClosingEventArgs e)
		{
			if (!busy)
				return;
			e.Cancel = true;
			MessageBox.Show(this, InstallerText.BootstrapBusy, BootstrapInfo.ProductName,
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
