using System.Diagnostics;

namespace host_example_gui
{
    public partial class Form1 : Form
    {
        private readonly Rainway.HostExample.Core core;

        private void AddLogLine(string line)
        {
            Invoke(() =>
            {
                textBoxLogs.Text += line + Environment.NewLine;
            });
        }

        public Form1()
        {
            InitializeComponent();
            core = new Rainway.HostExample.Core((line) => AddLogLine(line));
            RecomputeHint();
        }

        private void RecomputeHint()
        {
            if (core.Connected) {
                linkLabelWebDemo.Text = "Connected! Enter the above Peer ID in the Web Demo to connect here.";
                linkLabelWebDemo.LinkArea = new LinkArea(linkLabelWebDemo.Text.IndexOf("Web Demo"), "Web Demo".Length);
            } else {
                linkLabelWebDemo.Text = "Enter your Rainway API key and click \"Connect\" to start.";
                linkLabelWebDemo.LinkArea = new LinkArea(0, 0);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://hub.rainway.com/keys") { UseShellExecute = true });
        }

        private void linkLabelWebDemo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://webdemo.rainway.com") { UseShellExecute = true });
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonConnect.Enabled = !string.IsNullOrWhiteSpace(textBoxApiKey.Text);
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            var apiKey = textBoxApiKey.Text;
            if (apiKey == null || !apiKey.StartsWith("pk_"))
            {
                AddLogLine("Error: Your Rainway API key should start with \"pk_\". If you haven't yet, click \"Manage API keys\" to generate a valid Rainway API key in the Hub.");
                return;
            }
            buttonConnect.Enabled = false;
            linkLabelWebDemo.Text = "Connecting...";
            await core.Start(apiKey);
            textBoxPeerId.Text = core.PeerId?.ToString() ?? "(failed to get Peer ID)";
            groupBoxSettings.Enabled = true;
            buttonDisconnect.Enabled = true;
            RecomputeHint();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            groupBoxSettings.Enabled = false;
            buttonDisconnect.Enabled = false;
            textBoxPeerId.Text = "";
            core.Stop();
            buttonConnect.Enabled = true;
            RecomputeHint();
        }

        private void checkBoxMouse_CheckedChanged(object sender, EventArgs e)
        {
            RecomputeInputLevel();
        }

        private void checkBoxKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            RecomputeInputLevel();
        }

        private void RecomputeInputLevel()
        {
            core.SetInputLevel(checkBoxMouse.Checked, checkBoxKeyboard.Checked, true);
        }

        private void checkBoxAcceptIncoming_CheckedChanged(object sender, EventArgs e)
        {
            core.AcceptIncoming = checkBoxAcceptIncoming.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}