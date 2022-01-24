using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Vouchers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private readonly string _dataFile = Environment.CurrentDirectory + @"\data.xml";
        private readonly string _settingsFile = Environment.CurrentDirectory + @"\settings.ini";
        private readonly string _logFile = Environment.CurrentDirectory + @"\vouchers.log";
        private void button1_Click(object sender, EventArgs e)
        {
            INIManager manager = new INIManager(_settingsFile);
            var averageConsumption = manager.GetPrivateString("main", "averageConsumption");
            
            var data = new List<uint>();
            byte probeg, prihod;

            var date = checkBox1.Checked ? DateTime.Now.ToString().Split(' ')[0] : dateTimePicker1.Value.ToString().Split(' ')[0];
            
            try {
                probeg = byte.Parse(probegBox.Text);
                prihod = byte.Parse(prihodBox.Text);
            }
            catch (Exception){ return; }

            using (StreamReader sr = new StreamReader(_logFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(uint.Parse(line));
                }
            }
            
            uint nowMileage = data[3] + probeg;
            double consumption = Math.Round((double)probeg * int.Parse(averageConsumption) / 100);
            uint remainder = data[0] + data[1] - data[2];
            
            using (StreamWriter sw = new StreamWriter(_logFile))
            {
                sw.Write(remainder + "\n" + prihod + "\n" + consumption + "\n" + nowMileage);
            }
            
            remainderLabel.Text = remainder.ToString();
            consumptionLabel.Text = consumption.ToString();
            mileageLabel.Text = nowMileage.ToString();
            prihodBox.Text = ""; probegBox.Text = "";

            XMLManager.AddNote(_dataFile, date, remainder.ToString(), probeg.ToString(), prihod.ToString(), nowMileage.ToString(), consumption.ToString());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            prihodBox.Text = ""; probegBox.Text = "";
            var form2 = new Form2();
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            a.Visible = false;
            dateTimePicker1.Visible = false;
            if(!File.Exists(_dataFile))
                Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                a.Visible = true;
                dateTimePicker1.Visible = true;
            }
            else
            {
                a.Visible = false;
                dateTimePicker1.Visible = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form3 = new Form3();
            form3.Show();
        }
    }
}

