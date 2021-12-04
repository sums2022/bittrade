using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BitTradeUpdater
{
    public partial class frmMain : Form
    {
        const string updateXml = "update.xml";
        const string serverUri = "https://github.com/sibuzu/NetBackUpdate/raw/main/";
        public frmMain()
        {
            InitializeComponent();
            LoadProject();
        }

        private void LoadProject()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(updateXml);

            lbProject.Items.Clear();
            foreach (XmlNode xn in xdoc.SelectNodes("//update"))
            {
                lbProject.Items.Add(xn.Attributes["appID"].InnerText);
            }
        }

        private void lbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string project = lbProject.Text;
            if (project !="")
            {
                UpdateProjectInfo(project);
            }
        }

        private void UpdateProjectInfo(string project)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(updateXml);

            lbCurrent.Text = "";
            lbNew.Text = "";
            tbDescript.Text = "";

            XmlNode xn = xdoc.SelectSingleNode("//update[@appID='" + project + "']");
            if (xn == null) return;

            lbCurrent.Text = xn.SelectSingleNode("version").InnerText;
            tbDescript.Text = xn.SelectSingleNode("description").InnerText;

            XmlNode xn2 = xn.SelectSingleNode("files");
            string exepath = xn2.Attributes["dir"].InnerText;
            string exename = Path.Combine(exepath, xn.SelectSingleNode("fileName").InnerText);
            if (File.Exists(exename))
            {
                string newVer = GetVersion(exename);
                lbNew.Text = newVer;
            }
        }

        private static string GetVersion(string exename)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(exename);
            Version ver = new Version(versionInfo.ProductVersion);
            DateTime buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * ver.Build)); // days since 1 January 2000
            return string.Format("v{0:yy.MM.dd}.{1:00000}", buildDateTime, ver.Revision);
        }

        private void MakeProject(string project)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(updateXml);

            XmlNode xnode = xdoc.SelectSingleNode("//update[@appID='" + project + "']");
            if (xnode == null)
            { 
                MessageBox.Show("Cannot make project " + project);
                return;
            }

            string ver = lbNew.Text;
            string tmppath = "tmp\\" + project;
            string zipfile = Path.Combine(project, project + "_" + ver + ".zip");
            Directory.CreateDirectory(project);
            if (Directory.Exists(tmppath))
               Directory.Delete(tmppath, true);

            XmlNode xn2 = xnode.SelectSingleNode("files");
            string exepath = xn2.Attributes["dir"].InnerText;
            foreach (XmlNode xn3 in xn2.SelectNodes("file"))
            {
                string fname = xn3.InnerText;
                string src = Path.Combine(exepath, fname);
                string dst = Path.Combine(tmppath, fname);
                string p = Path.GetDirectoryName(dst);
                Directory.CreateDirectory(p);
                File.Copy(src, dst);
            }
            if (File.Exists(zipfile))
                File.Delete(zipfile);
           
            // Update XML
            xnode["version"].InnerText = ver;
            xnode["url"].InnerText = serverUri + zipfile.Replace('\\', '/');
            xnode["description"].InnerText = tbDescript.Text;
            if (xnode.SelectSingleNode("md5") == null)
            {
                xnode.AppendChild(xdoc.CreateElement("md5"));
            }
            xnode["md5"].InnerText = CalcMD5(zipfile);

            xdoc.Save(updateXml);
            MessageBox.Show(string.Format("Project {0}_{1} is ready to submit", project, ver));
        }

        private string CalcMD5(string fname)
        {
            string md5;
            using (Stream s = new FileStream(fname, FileMode.Open))
            {
                byte[] hash = MD5.Create().ComputeHash(s);
                md5 = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
            }
            return md5;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string project = lbProject.Text;
            if (project != "")
            {
                // MakeProject(project);
                SummitProject();
            }
        }

        private void SummitProject()
        {
            // string comment = tbDescript.Text;
            string comment = "";
            string argument = "/C choice /C Y /N /D Y /T 4 & git add -A & git commit {0} & git pull & git push";

            if (tbDescript.Lines.Length == 0)
            {
                comment = string.Format("-m \"{0}_{1}\"", lbProject.Text, lbNew.Text);
            }
            else
            {
                foreach (string line in tbDescript.Lines)
                {
                    comment += string.Format("-m \"{0}\" ", line);
                }
            }

            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = String.Format(argument, comment);
            // Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            // MessageBox.Show(string.Format("Project {0} has been submit", comment));
        }

        private void BuildProject()
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C git pull";
            Info.FileName = "cmd.exe";
            Info.CreateNoWindow = true;
            Process.Start(Info);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string[] lines = tbDescript.Lines;
            tbDescript.Text = lbNew.Text + ":\r\n";
            for (int i = 1; i < lines.Length; ++i)
            {
                string ln = lines[i].Trim();
                if (ln != "") tbDescript.Text += ln + "\r\n";
            }
        }

        private static StringBuilder cmdOutput = null;
        Process p;
        StreamWriter SW;

        private void btnBuild_Click(object sender, EventArgs e)
        {
            BuildProject();
        }

        private static void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                cmdOutput.Append(Environment.NewLine + outLine.Data);
            }
        }
   }
}
