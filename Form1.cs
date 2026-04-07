using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_mortgage_calculator
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void HighlightResults()
        {
            // 貸款總額用淺藍色，總還款用淺綠色
            textBox6.BackColor = Color.AliceBlue;
            textBox10.BackColor = Color.Honeydew;

            // 讓文字靠右對齊 (符合金融習慣)
            foreach (var tb in new[] { textBox6, textBox7, textBox8, textBox9, textBox10 })
            {
                tb.TextAlign = HorizontalAlignment.Right;
                tb.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
        }
        private void SetModernStyle(Control ctrl)
        {
            ctrl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, 15, 15));
        }

        private void RefreshUI()
        {
            // 調用美化
            SetModernStyle(button1);
            HighlightResults();

            // 如果總利息超過貸款本金的一半，將總利息文字變紅 (警示)
            if (double.Parse(textBox9.Text) > (double.Parse(textBox6.Text) / 2))
            {
                textBox9.ForeColor = Color.Red;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool is1Valid = double.TryParse(textBox1.Text, out double txt1);
            bool is2Valid = double.TryParse(textBox2.Text, out double txt2);
            bool is3Valid = double.TryParse(textBox3.Text, out double txt3);
            bool is4Valid = double.TryParse(textBox4.Text, out double txt4);
            bool is5Valid = double.TryParse(textBox5.Text, out double txt5);

            // 驗證身高輸入
            if (is1Valid)
            {
                if (txt1 <= 0)
                {
                    MessageBox.Show("房屋總價(Total House Price)必須大於0", "房屋總價(Total House Price)錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的房屋總價(Total House Price)", "房屋總價(Total House Price)錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 驗證體重輸入
            if (is2Valid)
            {
                if (txt2 <= 0 || txt2 > 100)
                {
                    MessageBox.Show("自備款比例(Down Payment %)單位為百分比", "自備款比例(Down Payment %)錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的自備款比例(Down Payment %)", "自備款比例(Down Payment %)錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (is3Valid)
            {
                if (txt3 <= 0 || txt3 > 100)
                {
                    MessageBox.Show("貸款利率(Annual Interest Rate)單位為百分比", "貸款利率(Annual Interest Rate)錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的貸款利率(Annual Interest Rate)", "貸款利率(Annual Interest Rate)錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 驗證體重輸入
            if (is4Valid)
            {
                if (txt4 <= 0)
                {
                    MessageBox.Show("貸款年限(Loan Term)必須大於零", "貸款年限(Loan Term)錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的貸款年限(Loan Term)", "貸款年限(Loan Term)錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (is5Valid)
            {
                if (txt5 < 0)
                {
                    MessageBox.Show("寬限期(Grace Period)必須大於零。", "寬限期(Grace Period)錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的寬限期(Grace Period)", "寬限期(Grace Period)錯誤",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double h = double.Parse(textBox1.Text);
            double p = double.Parse(textBox2.Text);
            double a = double.Parse(textBox3.Text);
            double l = double.Parse(textBox4.Text);
            double g = double.Parse(textBox5.Text);
            double P = h * (1 - p * 0.01); // 貸款總額
            double r = a / 12 / 100;               // 月利率
            double n = l * 12;                         // 總期數
            double G = g * 12;                        // 寬限期期數

            // 1. 每月應繳 (以寬限期後為準)
            double monthlyPayment;
            if (n == g) // 極端情況：全寬限期
                monthlyPayment = P * r;
            else
                monthlyPayment = P * (r * Math.Pow(1 + r, n - g)) / (Math.Pow(1 + r, n - g) - 1);

            // 2. 首期利息與本金
            double firstMonthInterest = P * r;
            double firstMonthPrincipal = (g > 0) ? 0 : monthlyPayment - firstMonthInterest;

            // 3. 總利息
            double totalInterest = (P * r * g) + (monthlyPayment * (n - g)) - P;

            // 4. 總還款額
            double totalAmount = P + totalInterest;

            textBox6.Text = P.ToString("N2");              // 貸款總額
            textBox7.Text = monthlyPayment.ToString("N2");   // 每月應繳
            textBox8.Text = firstMonthInterest.ToString("N2"); // 首期利息
            textBox9.Text = totalInterest.ToString("N2");    // 總利息
            textBox10.Text = totalAmount.ToString("N2");      // 總還款額

            RefreshUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
