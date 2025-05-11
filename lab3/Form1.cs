using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var numberSystemType = new string[]
            {
            "(2)",
            "(8)",
            "(10)",
            "(16)",
            };

            comboBox1.DataSource = new List<string>(numberSystemType);
            comboBox2.DataSource = new List<string>(numberSystemType);
            comboBox3.DataSource = new List<string>(numberSystemType);
            var operations = new string[] { "+", "-", "*", "/" };
            comboBox4.DataSource = new List<string>(operations);

            button1.Click += (s, e) => Calculate();
        }

        private NumberSystemType GetSystemType(ComboBox comboBox)
        {
            NumberSystemType systemType;
            switch (comboBox.Text)
            {
                case "(10)":
                    systemType = NumberSystemType.dec;
                    break;
                case "(2)":
                    systemType = NumberSystemType.bin;
                    break;
                case "(8)":
                    systemType = NumberSystemType.oct;
                    break;
                case "(16)":
                    systemType = NumberSystemType.hex;
                    break;
                default:
                    systemType = NumberSystemType.dec;
                    break;
            }
            return systemType;
        }

        private void Calculate()
        {
            try
            {
                string firstValue = textBox1.Text;
                string secondValue = textBox2.Text;


                NumberSystemType firstType = GetSystemType(comboBox1);
                NumberSystemType secondType = GetSystemType(comboBox2);
                NumberSystemType resultType = GetSystemType(comboBox3);

                var firstSystemNumber = new SystemNum(firstValue, firstType);
                var secondSystemNumber = new SystemNum(secondValue, secondType);

                SystemNum sumLength;

                switch (comboBox4.Text)
                {
                    case "+":
                        sumLength = firstSystemNumber + secondSystemNumber;
                        break;
                    case "-":
                        sumLength = firstSystemNumber - secondSystemNumber;
                        break;
                    case "*":
                        sumLength = firstSystemNumber * secondSystemNumber;
                        break;
                    case "/":
                        sumLength = firstSystemNumber / secondSystemNumber;
                        break;
                    default:
                        sumLength = new SystemNum("0", NumberSystemType.dec);
                        break;
                }

                textBox3.Text = sumLength.To(resultType).GetValue().ToString();
            }
            catch (FormatException)
            {
                textBox3.Text = "Ошибка";
            }
        }
        private void ValueChanged(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
