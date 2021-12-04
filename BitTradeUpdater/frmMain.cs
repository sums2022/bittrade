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
        const string serverUri = "https://github.com/sums2022/bittrade/raw/main/";

        string projPath = "";
        string releaseVer = "";
        string remoteVer = "";
        string localVer = "";

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
            if (project != "")
            {
                UpdateProjectInfo(project);
            }
        }

        private void UpdateProjectInfo(string project)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(updateXml);

            tbDescript.Text = "";

            XmlNode xn = xdoc.SelectSingleNode("//update[@appID='" + project + "']");
            if (xn == null) return;

            tbDescript.Text = xn.SelectSingleNode("description").InnerText;

            projPath = xn.SelectSingleNode("projPath").InnerText;
            releaseVer = xn.SelectSingleNode("releaseVersion").InnerText;
            remoteVer = xn.SelectSingleNode("remoteVersion").InnerText;
            localVer = GetFlutterVersion(projPath);
            lbRelease.Text = releaseVer;
            lbRemote.Text = remoteVer;
            lbLocal.Text = localVer;
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

            string projPath = xnode["projPath"].InnerText;
            string apkFile = Path.Combine(projPath, "build/app/outputs/apk/release/app-release.apk");
            if (!File.Exists(apkFile))
            {
                MessageBox.Show("Cannot make project " + project);
                return;
            }

            this.remoteVer = this.localVer;
            lbRemote.Text = this.remoteVer;
            string dstFile = string.Format("{0}/{1}_{2}.apk", project, project, remoteVer);
            File.Copy(apkFile, dstFile, true);

            // Update XML
            xnode["remoteVersion"].InnerText = remoteVer;
            xnode["url"].InnerText = serverUri + dstFile.Replace('\\', '/');
            xnode["description"].InnerText = tbDescript.Text;
            xnode["sha256"].InnerText = CalcSha256(dstFile);

            xdoc.Save(updateXml);
        }

        private string CalcSha256(string fname)
        {
            string sha256;
            using (Stream s = new FileStream(fname, FileMode.Open))
            {
                byte[] hash = SHA256.Create().ComputeHash(s);
                sha256 = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
            }
            return sha256;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string project = lbProject.Text;
            if (project != "")
            {
                MakeProject(project);
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
                comment = string.Format("-m \"{0}_{1}\"", lbProject.Text, lbRemote.Text);
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

        private void BuildProject(string project)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(updateXml);

            XmlNode xnode = xdoc.SelectSingleNode("//update[@appID='" + project + "']");
            if (xnode == null)
            {
                MessageBox.Show("Cannot build project " + project);
                return;
            }

            string projPath = xnode["projPath"].InnerText;
            localVer = GetFlutterVersion(projPath, true);
            lbLocal.Text = localVer;

            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = string.Format("/C echo BUILD Version={0} & cd \"{1}\" & flutter build apk & pause",
                this.localVer, projPath);
            Info.FileName = "cmd.exe";
            Info.CreateNoWindow = true;
            Process.Start(Info);
        }

        private String GetFlutterVersion(string projPath, bool inc = false)
        {
            string pubspec = Path.Combine(projPath, "pubspec.yaml");
            StringBuilder sb = new StringBuilder();
            String line;
            String ver = "";
            using (StreamReader sr = new StreamReader(pubspec))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string vv = CheckVersion(line, inc);
                    if (vv!="")
                    {
                        ver = vv;
                        if (!inc) return ver.Replace('+', '.');
                        line = "version: " + ver; 
                    }
                    sb.AppendLine(line);
                }
            }
            File.WriteAllText("ver.txt", sb.ToString());
            File.Copy("ver.txt", pubspec, true);

            return ver.Replace('+', '.');
        }

        private string CheckVersion(string line, bool inc=false)
        {
            if (line.Length < 10) return "";
            if (line.Substring(0, 8) != "version:") return "";

            string[] vers = line.Substring(8).Split(new char[] { '.', '+' });
            int nver = 0;
            for (int i=0; i<vers.Length; ++i)
            {
                nver += int.Parse(vers[i]);
                if (i < 2) nver *= 10;
                else if (i == 2) nver *= 100;
            }

            if (inc) nver += 1;
            string strVer = string.Format("{0}.{1}.{2}+{3}",
                nver / 10000, nver / 1000 % 10, nver / 100 % 10, nver % 100);

            return strVer;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string[] lines = tbDescript.Lines;
            tbDescript.Text = lbRemote.Text + ":\r\n";
            for (int i = 1; i < lines.Length; ++i)
            {
                string ln = lines[i].Trim();
                if (ln != "") tbDescript.Text += ln + "\r\n";
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string project = lbProject.Text;
            if (project != "")
            {
                BuildProject(project);
            }
        }
    }
}
