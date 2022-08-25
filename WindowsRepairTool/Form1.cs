using System.Management.Automation;
namespace WindowsRepairTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void start_Info()
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult = DialogResult.OK;
            MessageBox.Show("This program starting the following repair methods: \n 1. DISM /Online /Cleanup-Image /RestoreHealth \n 2. sfc /scannow","Program Information");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            start_Info();
        }
    }
}