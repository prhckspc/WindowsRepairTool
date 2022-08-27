using System.Management.Automation;
using System.Windows;
namespace WindowsRepairTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //The method should be static because it is not need things from the main classes
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
        //The method should be not static because it is need things from the main classes
        private void Start_Dism()
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 40;
        }

        private void repairButton_Click(object sender, EventArgs e)
        {
            Start_Dism();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            Start_Info();
        }
    }
}