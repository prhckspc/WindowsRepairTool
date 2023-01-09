using System.Management.Automation;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Windows.ApplicationModel;
using System.Management.Automation.Language;

namespace WindowsRepairTool
{
    public partial class Form1 : Form
    {
        private int method = 1;
        private string done = "";
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
        }
        private static void Start_Info()
        {
            string msg = "This program starting the following repair methods: \n 1. DISM /Online /Cleanup-Image /RestoreHealth \n 2. sfc /scannow";
            string msgtitle = "Program Information";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(msg, msgtitle, buttons, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                return;
            }
            
        }

        private void repairButton_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync(this.method);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        

        private void infoButton_Click(object sender, EventArgs e)
        {
            Start_Info();
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int argument = (int)e.Argument;
            int check_Proc = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            if (argument == 1)
            {

                try
                {
                    
                    PowerShell ps = PowerShell.Create();
                    ps.AddCommand("Set-ExecutionPolicy");
                    ps.AddParameter("Scope", "CurrentUser");
                    ps.AddParameter("ExecutionPolicy", "Unrestricted");
                    ps.AddStatement();
                    ps.AddCommand("Repair-WindowsImage");
                    ps.AddParameter("RestoreHealth");
                    ps.AddParameter("Online");

                    ps.Streams.Progress.DataAdded += (sender, eventargs) =>
                    {
                        PSDataCollection<ProgressRecord> progressRecords = (PSDataCollection<ProgressRecord>)sender;
                        int percentage = progressRecords[eventargs.Index].PercentComplete;
                        worker.ReportProgress(percentage);
                    };

                    foreach (var obj in ps.Invoke())
                    {
                        if (obj != null)
                        {
                            Debug.WriteLine(obj);
                        }
                    }
                    this.done = "m1";
                    e.Cancel = true;
                    return;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("The program throwed exception: {0}", ex);
                }
            }

            else if(argument == 2)
            {
                try
                {
                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        { 
                            FileName = "powershell",
                            CreateNoWindow = false,
                            RedirectStandardOutput = false,
                            RedirectStandardInput = false,
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Arguments = "sfc /SCANNOW"
                        }
                    };
                    if (proc.Start())
                    {
                        worker.ReportProgress(50);
                        proc.WaitForExit();
                        if (proc.HasExited)
                        {
                            worker.ReportProgress(100);
                            e.Cancel = true;
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.method == 1)
            {
                this.progressBar1.Value = e.ProgressPercentage;
            }
            if (this.method == 2)
            {
                this.progressBar2.Value = e.ProgressPercentage;
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled && this.done == "m1" && this.method == 1)
            {
                this.method++;
                backgroundWorker1.RunWorkerAsync(this.method);
            }
            else
            {
                return;
            }
        }
    }
}

