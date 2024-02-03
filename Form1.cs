
using System;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Text;

namespace ReadPDF
{
    public partial class Form1 : Form
    {
        private string _progName = "Tax Return app";
        private string _saConfigFilePath;
        private string _ukDirectConfigFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateDefaultConfigFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                Encoding utf8WithBom = new UTF8Encoding(true);
                using (TextWriter sw = new StreamWriter(filePath, false, utf8WithBom))
                {
                    sw.WriteLine("AmountTypeEnum    SearchString    PreSearchString");
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private void InitConfigFile(string configDir)
        {
            if (string.IsNullOrEmpty(configDir))
            {
                DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                configDir = di.Parent.Parent.FullName;
            }
            _saConfigFilePath = Path.Combine(configDir, SaParser.ConfigFileName);
            _ukDirectConfigFilePath = Path.Combine(configDir, UkParser.ConfigFileName);

            CreateDefaultConfigFile(_saConfigFilePath);
            CreateDefaultConfigFile(_ukDirectConfigFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            InitConfigFile(config.AppSettings.Settings["ConfigDirectory"].Value);
            
            tbInputDirectory.Text = config.AppSettings.Settings["InputDirectory"].Value;
            tbOutputDirectory.Text = config.AppSettings.Settings["OutputDirectory"].Value;
            if (config.AppSettings.Settings["RadioSa"] != null)
            {
                bool isChecked = false;
                bool.TryParse(config.AppSettings.Settings["RadioSa"].Value, out isChecked);
                RadioSa.Checked = isChecked;
                bool.TryParse(config.AppSettings.Settings["RadioUk"].Value, out isChecked);
                RadioUk.Checked = isChecked;
            }
            if (!RadioSa.Checked && !RadioUk.Checked)
            {
                RadioUk.Checked = true;
            }

            this.Text = _progName;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["InputDirectory"].Value = tbInputDirectory.Text;
            config.AppSettings.Settings["OutputDirectory"].Value = tbOutputDirectory.Text;
            config.AppSettings.Settings["RadioUk"].Value = RadioUk.Checked.ToString();
            config.AppSettings.Settings["RadioSa"].Value = RadioSa.Checked.ToString();

            config.Save(ConfigurationSaveMode.Minimal);
            Close();
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                StatementParser parser;
                if (RadioUk.Checked)
                {
                    parser = new UkParser(tbInputDirectory.Text, GetOutputFilePath());
                }
                else
                {
                    parser = new SaParser(tbInputDirectory.Text, GetOutputFilePath());
                }
                parser.SetConfigEntries(tbConfigFilePath.Text);
                parser.ParseInputFolder();
                parser.WriteToCsv();

                if (MessageBoxInfoConfirm("Finished!\n\nWould you like to open the output folder in Explorer?"))
                {
                    System.Diagnostics.Process.Start("explorer.exe", tbOutputDirectory.Text);
                }
            }
        }

        private bool ValidateUserInput()
        {
            if (String.IsNullOrEmpty(tbInputDirectory.Text))
            {
                MessageBoxWarning("Please specify the input directory.\nThis is the folder that contains the cshtml files you want to extract translation phrases from");
            }
            else if (String.IsNullOrEmpty(tbOutputDirectory.Text))
            {
                MessageBoxWarning("Please specify the output directory.\nThis is the folder where this app will create the CSV file containing the translation phrases.");
            }
            else if (!Directory.Exists(tbInputDirectory.Text))
            {
                MessageBoxWarning("Cannot find input directory.");
            }
            else if (!Directory.Exists(tbOutputDirectory.Text))
            {
                MessageBoxWarning("Cannot find output directory.");
            }
            else if (!RadioUk.Checked && !RadioSa.Checked)
            {
                MessageBoxWarning("Please select a statement type.");
            }
            else
            {
                if (File.Exists(GetOutputFilePath()))
                {
                    if (MessageBoxWarningConfirm(GetOutputFileName() + " already exists in the target directory.\nAre you sure you want to overwrite this file?"))
                    {
                        try
                        {
                            File.Delete(GetOutputFilePath());
                        }
                        catch
                        {
                            MessageBoxWarning("Could not delete " + GetOutputFileName() + ". Please check it is not open in any other programs.");
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
                return true;
            }
            return false;
        }

        private string GetOutputFilePath()
        {
            return Path.Combine(tbOutputDirectory.Text, GetOutputFileName());
        }

        private string GetOutputFileName()
        {
            return RadioUk.Checked ? "UkOutput.csv" : "SaOutput.csv";
        }

        private void MessageBoxWarning(string message)
        {
            MessageBox.Show(message, _progName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool MessageBoxWarningConfirm(string message)
        {
            return MessageBox.Show(message, _progName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        }

        private bool MessageBoxInfoConfirm(string message)
        {
            return MessageBox.Show(message, _progName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }

        private void MessageBoxError(string message)
        {
            MessageBox.Show(message, _progName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MessageBoxInfo(string message)
        {
            MessageBox.Show(message, _progName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInputDirectoryBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbInputDirectory.Text = folderDlg.SelectedPath;
            }
        }

        private void btnOutputDirectoryBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbOutputDirectory.Text = folderDlg.SelectedPath;
            }
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            StatementParser parser;
            if (RadioUk.Checked)
            {
                parser = new UkParser(tbInputDirectory.Text, GetOutputFilePath());
            }
            else
            {
                parser = new SaParser(tbInputDirectory.Text, GetOutputFilePath());
            }
            parser.RenameToSimpleDateFormat();
        }

        private void RadioUk_CheckedChanged(object sender, EventArgs e)
        {
            tbConfigFilePath.Text = _ukDirectConfigFilePath;
        }

        private void RadioSa_CheckedChanged(object sender, EventArgs e)
        {
            tbConfigFilePath.Text = _saConfigFilePath;
        }

        private void btnConfigEdit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", tbConfigFilePath.Text);
        }
    }
}
