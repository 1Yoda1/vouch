using System;
using System.Windows.Forms;

namespace Vouchers
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (normaBox.Text == "")
                return;
            var result = MessageBox.Show(@"Ты уверен что хочешь изменить норму топлива на " + normaBox.Text, @"Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            var manager = new INIManager(Environment.CurrentDirectory + @"\settings.ini");
            manager.WritePrivateString("main", "averageConsumption", normaBox.Text);
            norma.Text = manager.GetPrivateString("main", "averageConsumption");
            normaBox.Text = "";
            Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var manager = new INIManager(Environment.CurrentDirectory + @"\settings.ini");
            norma.Text = manager.GetPrivateString("main", "averageConsumption");
        }
    }
}
