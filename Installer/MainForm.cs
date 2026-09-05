using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class MainForm : Form
	{
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

		internal MainForm(string initialPath)
		{
			Text = PayloadInfo.ProductName + " " + PayloadInfo.Version;
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
			titleLabel.Text = PayloadInfo.ProductName + " " + PayloadInfo.Version;
			explanationLabel.Text = InstallerText.Explanation;
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

		private void RefreshState()
		{
			try
			{
				InstallState state = InstallerEngine.Probe(pathBox.Text);
				stateLabel.Text = InstallerText.StatePrefix + state.Description;
				installButton.Enabled = state.ValidGame;
				uninstallButton.Enabled = state.ValidGame && (state.ManagedInstall || state.CurrentPayload);
			}
			catch (Exception error)
			{
				stateLabel.Text = InstallerText.StatePrefix + InstallerText.CheckFailedPrefix + error.Message;
				installButton.Enabled = false;
				uninstallButton.Enabled = false;
			}
		}

		private void InstallButtonClick(object sender, EventArgs e)
		{
			InstallState state = InstallerEngine.Probe(pathBox.Text);
			if (state.LoaderExists && !state.ManagedInstall && !state.CurrentPayload)
			{
				DialogResult choice = MessageBox.Show(this, InstallerText.ExistingLoaderConfirmation,
					PayloadInfo.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (choice != DialogResult.OK)
					return;
			}
			RunOperation(delegate { return InstallerEngine.Install(pathBox.Text, AppendActivity); });
		}

		private void UninstallButtonClick(object sender, EventArgs e)
		{
			DialogResult choice = MessageBox.Show(this, InstallerText.UninstallConfirmation,
				PayloadInfo.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (choice != DialogResult.OK)
				return;
			RunOperation(delegate { return InstallerEngine.Uninstall(pathBox.Text, AppendActivity); });
		}

		private void RunOperation(Func<OperationResult> operation)
		{
			installButton.Enabled = false;
			uninstallButton.Enabled = false;
			UseWaitCursor = true;
			activityBox.Clear();
			try
			{
				OperationResult result = operation();
				MessageBox.Show(this, result.Message + (string.IsNullOrEmpty(result.LogPath) ? string.Empty : "\r\n\r\n" + InstallerText.LogPrefix + result.LogPath),
					PayloadInfo.ProductName, MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
			}
			finally
			{
				UseWaitCursor = false;
				RefreshState();
			}
		}

		private void AppendActivity(string message)
		{
			activityBox.AppendText(message + Environment.NewLine);
		}

		private void LogButtonClick(object sender, EventArgs e)
		{
			string path = InstallerEngine.GetInstallerLogPath(pathBox.Text);
			if (!File.Exists(path))
			{
				MessageBox.Show(this, InstallerText.LogNotCreated(path), PayloadInfo.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
		}
	}
}
