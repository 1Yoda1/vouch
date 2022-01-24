using System;
using System.Windows.Forms;

namespace Vouchers
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private readonly string dataFile = Environment.CurrentDirectory + @"\data.xml";

        private void Form3_Load(object sender, EventArgs e)
        {
            remainderLabel.Text = "";
            probegLabel.Text = "";
            prihodLabel.Text = "";
            mileageLabel.Text = "";
            consumptionLabel.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString().Split(' ')[0];
            try
            {
                var dataArray = XMLManager.SelectNote(dataFile, date);
                remainderLabel.Text = dataArray[0];
                probegLabel.Text = dataArray[1];
                prihodLabel.Text = dataArray[2];
                mileageLabel.Text = dataArray[3];
                consumptionLabel.Text = dataArray[4];
            }
            catch (Exception)
            {
                MessageBox.Show(@"Данных за это число не найдено!",@"Ошибка");
            }
        }
    }
}
