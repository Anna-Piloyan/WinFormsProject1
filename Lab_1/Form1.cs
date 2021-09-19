using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class Form1 : Form
    {
        int timer_count = 10;      
        double[] petrol = { 31.98, 29.98, 28.48, 24.52, 20.36 };      
        double AmountGasStation { get; set; } = 0;
        double VolumeGas { get; set; } = 0;
        double AmountCafe { get; set; } = 0;
        double TotalToPay { get; set; } = 0;
        double EndShift { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
            textBox4.Text = "4,00";
            textBox6.Text = "5,40";
            textBox8.Text = "7,20";
            textBox10.Text = "4,40";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked)
            {
                textBox5.Text = "0";
                textBox5.ReadOnly = true;
            }
            if (checkBox1.Checked)
            {
                textBox5.ReadOnly = false;
                textBox5.Text = "1";
            }         
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked)
            {
                textBox7.Text = "0";
                textBox7.ReadOnly = true;
            }
            if (checkBox2.Checked)
            {
                textBox7.ReadOnly = false;
                textBox7.Text = "1";
            }          
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked)
            {
                textBox9.Text = "0";
                textBox9.ReadOnly = true;
            }
            if (checkBox3.Checked)
            {
                textBox9.ReadOnly = false;
                textBox9.Text = "1";
            }
          
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked)
            {
                textBox11.Text = "0";
                textBox11.ReadOnly = true;
            }
            if (checkBox4.Checked)
            {
                textBox11.ReadOnly = false;
                textBox11.Text = "1";
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            AmountCafe = (double.Parse(textBox4.Text) * double.Parse(textBox5.Text)) + (double.Parse(textBox6.Text) * double.Parse(textBox7.Text)) +
                (double.Parse(textBox8.Text) * double.Parse(textBox9.Text)) + (double.Parse(textBox10.Text) * double.Parse(textBox11.Text));
            label12.Text = $"{Math.Round(AmountCafe, 2):f2}";
            if (radioButton1.Checked)
            {
                AmountGasStation = petrol[comboBox1.SelectedIndex] * double.Parse(textBox2.Text);
                label11.Text = $"{Math.Round(AmountGasStation, 2):f2}";
                TotalToPay = AmountCafe + AmountGasStation;
            }
            if (radioButton2.Checked)
            {
                VolumeGas = double.Parse(textBox3.Text) / double.Parse(textBox1.Text);
                label11.Text = Math.Round(VolumeGas, 1).ToString();
                label9.Text = "litres";
                groupBox3.Text = "To Issue";
                TotalToPay = AmountCafe + double.Parse(textBox3.Text);
            }
            if((!radioButton1.Checked) && (!radioButton2.Checked))
                TotalToPay = AmountCafe;
            label13.Text = $"{Math.Round(TotalToPay, 2):f2}";
            EndShift += Math.Round(TotalToPay, 2);           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = $"{petrol[0]}";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = $"{petrol[1]}";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = $"{petrol[2]}";
            }
            if (comboBox1.SelectedIndex == 3)
            {
                textBox1.Text = $"{petrol[4]}";
            }
            if (comboBox1.SelectedIndex == 4)
            {
                textBox1.Text = $"{petrol[3]}";
            }
            textBox2.Text = "0";
            textBox3.Text = "0,00";           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                textBox3.ReadOnly = true;
                textBox2.ReadOnly = false;              
            }         
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = false;             
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DialogResult result;
            timer_count--;
            if (timer_count == 0)
            {
                timer1.Stop();
                result = MessageBox.Show(" Do you want to continue?", "New Customer Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    timer_count = 10;                  
                    return;
                }
                if (result == DialogResult.No)
                {
                    timer_count = 10;
                    foreach (Control ctrl in groupBox1.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            (ctrl as TextBox).Text = "";
                            (ctrl as TextBox).ReadOnly = true;
                        }
                        if (ctrl is ComboBox)
                        {
                            (ctrl as ComboBox).SelectedIndex = -1;                           
                        }
                        foreach (Control ctrl1 in groupBox6.Controls)
                        {
                            if (ctrl1 is RadioButton)
                            {
                                radioButton1.Checked = false;
                                radioButton2.Checked = false;
                            }
                        }
                    }
                    foreach (Control ctrl in groupBox2.Controls)
                    {
                        if (ctrl is CheckBox)
                        {
                            (ctrl as CheckBox).Checked = false;
                        }
                    }
                    label11.Text = "0,00";
                    label12.Text = "0,00";
                    label13.Text = "0,00";                   
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{Math.Round(EndShift, 2):f2}", "Total Cash", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]{1,2}$";
            Regex regex = new Regex(pattern);
            if (radioButton1.Checked)
            {
                try
                {
                    if (!regex.IsMatch(textBox2.Text))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please, enter digits and not more then 99 litres!");
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]{1,4}$";
            Regex regex = new Regex(pattern);
            if (radioButton2.Checked)
            {
                try
                {
                    if (!regex.IsMatch(textBox3.Text))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please, enter digits!");
                }
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'));
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'));
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'));
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'));
        }
    }
}
