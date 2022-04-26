using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatStat
{
    public partial class Form1 : Form
    {
        #region Переменные
        private double[] alfaInTable = new double[] { 0.01, 0.025, 0.05, 0.1, 0.25, 0.5, 0.75,
            0.9, 0.95, 0.975, 0.99, 0.995 }; // Индексы хи квадрат
        private double[,] hiQuad = new double[,] {
            { 1,  6.63490 , 5.02389 , 3.84146 , 2.70554 , 1.32330 , 0.45494 , 0.10153 , 0.01579 , 0.00393 ,0.00098 , 0.00016 , 0.00004},
            { 2,  9.21034 , 7.37776 , 5.99146 , 4.60517 , 2.77259 , 1.38629 , 0.57536 , 0.21072 , 0.10259 ,0.05064 , 0.02010 , 0.01003},
            { 3,  11.34487, 9.34840 , 7.81473 , 6.25139 , 4.10834 , 2.36597 , 1.21253 , 0.58437 , 0.35185 ,0.21580 , 0.11483 , 0.07172},
            { 4,  13.27670, 11.14329, 9.48773 , 7.77944 , 5.38527 , 3.35669 , 1.92256 , 1.06362 , 0.71072 ,0.48442 , 0.29711 , 0.20699},
            { 5,  15.08627, 12.83250, 11.07050, 9.23636 , 6.62568 , 4.35146 , 2.67460 , 1.61031 , 1.14548 ,0.83121 , 0.55430 , 0.41174},
            { 6,  16.81189, 14.44938, 12.59159, 10.64464, 7.84080 , 5.34812 , 3.45460 , 2.20413 , 1.63538 ,1.23734 , 0.87209 , 0.67573},
            { 7,  18.47531, 16.01276, 14.06714, 12.01704, 9.03715 , 6.34581 , 4.25485 , 2.83311 , 2.16735 ,1.68987 , 1.23904 , 0.98926},
            { 8,  20.09024, 17.53455, 15.50731, 13.36157, 10.21885, 7.34412 , 5.07064 , 3.48954 , 2.73264 ,2.17973 , 1.64650 , 1.34441},
            { 9,  21.66599, 19.02277, 16.91898, 14.68366, 11.38875, 8.34283 , 5.89883 , 4.16816 , 3.32511 ,2.70039 , 2.08790 , 1.73493},
            { 10, 23.20925, 20.48318, 18.30704, 15.98718, 12.54886, 9.34182 , 6.73720 , 4.86518 , 3.94030 ,3.24697 , 2.55821 , 2.15586},
            { 11, 24.72497, 21.92005, 19.67514, 17.27501, 13.70069, 10.34100, 7.58414 , 5.57778 , 4.57481 ,3.81575 , 3.05348 , 2.60322},
            { 12, 26.21697, 23.33666, 21.02607, 18.54935, 14.84540, 11.34032, 8.43842 , 6.30380 , 5.22603 ,4.40379 , 3.57057 , 3.07382},
            { 13, 27.68825, 24.73560, 22.36203, 19.81193, 15.98391, 12.33976, 9.29907 , 7.04150 , 5.89186 ,5.00875 , 4.10692 , 3.56503},
            { 14, 29.14124, 26.11895, 23.68479, 21.06414, 17.11693, 13.33927, 10.16531, 7.78953 , 6.57063 ,5.62873 , 4.66043 , 4.07467},
            { 15, 30.57791, 27.48839, 24.99579, 22.30713, 18.24509, 14.33886, 11.03654, 8.54676 , 7.26094 ,6.26214 , 5.22935 , 4.60092},
            { 16, 31.99993, 28.84535, 26.29623, 23.54183, 19.36886, 15.33850, 11.91222, 9.31224 , 7.96165 ,6.90766 , 5.81221 , 5.14221},
            { 17, 33.40866, 30.19101, 27.58711, 24.76904, 20.48868, 16.33818, 12.79193, 10.08519, 8.67176 ,7.56419 , 6.40776 , 5.69722},
            { 18, 34.80531, 31.52638, 28.86930, 25.98942, 21.60489, 17.33790, 13.67529, 10.86494, 9.39046 ,8.23075 , 7.01491 , 6.26480},
            { 19, 36.19087, 32.85233, 30.14353, 27.20357, 22.71781, 18.33765, 14.56200, 11.65091, 10.11701,8.90652 , 7.63273 , 6.84397},
            { 20, 37.56623, 34.16961, 31.41043, 28.41198, 23.82769, 19.33743, 15.45177, 12.44261, 10.85081,9.59078 , 8.26040 , 7.43384},
            { 21, 38.93217, 35.47888, 32.67057, 29.61509, 24.93478, 20.33723, 16.34438, 13.23960, 11.59131,10.28290, 8.89720 , 8.03365},
            { 22, 40.28936, 36.78071, 33.92444, 30.81328, 26.03927, 21.33705, 17.23962, 14.04149, 12.33801,10.98232, 9.54249 , 8.64272},
            { 23, 41.63840, 38.07563, 35.17246, 32.00690, 27.14134, 22.33688, 18.13730, 14.84796, 13.09051,11.68855, 10.19572, 9.26042},
            { 24, 42.97982, 39.36408, 36.41503, 33.19624, 28.24115, 23.33673, 19.03725, 15.65868, 13.84843,12.40115, 10.85636, 9.88623},
            { 25, 44.31410, 40.64647, 37.65248, 34.38159, 29.33885, 24.33659, 19.93934, 16.47341, 14.61141,13.11972, 11.52398, 10.51965},
            { 26, 45.64168, 41.92317, 38.88514, 35.56317, 30.43457, 25.33646, 20.84343, 17.29189, 15.37916,13.84391, 12.19815, 11.16024},
            { 27, 46.96294, 43.19451, 40.11327, 36.74122, 31.52841, 26.33634, 21.74941, 18.11390, 16.15140,14.57338, 12.87850, 11.80759},
            { 28, 48.27824, 44.46079, 41.33714, 37.91592, 32.62049, 27.33623, 22.65716, 18.93924, 16.92788,15.30786, 13.56471, 12.46134},
            { 29, 49.58788, 45.72229, 42.55697, 39.08747, 33.71091, 28.33613, 23.56659, 19.76774, 17.70837,16.04707, 14.25645, 13.12115},
            { 30, 50.89218, 46.97924, 43.77297, 40.25602, 34.79974, 29.33603, 24.47761, 20.59923, 18.49266,16.79077, 14.95346, 13.78672}}; // Таблица хи квадрат

        private double[] numbers;

        private int countOfNumbers; // колво чисел всего
        private double xMin; // минимальное число в списке
        private double xMax; // макс число 
        private double k; // k
        private double h; // h
        private double[] intervalNumbers; // Ni
        private double[] xAverage; // Массив с средними значениями
        private int countIntrevals = 0; // колво интервалов
        private double xWithDash; // икс с чертой
        private double sigma; // сигма
        private double[] nNakoplen; // n накопленные
        private double mediana; // Медиана

        private double gamma; // Гамма

        private double leftXChert; // Левая часть икс с чертой
        private double rightXChert; // Правая часть икс с чертой
        private double leftSChert; // Левая часть сигма с чертой
        private double rightSChert; // Правая часть сигма с чертой
        #endregion

        public Form1()
        {
            InitializeComponent();
            FillBoxAndLabels();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        #region Заполнение форм полей в форме
        private void FillBoxAndLabels()
        {
            CrearLabels();
            ComboBoxFill();
        }
        private void Inicialaze()
        {
            if (textBox1.Text == "")
                gamma = 0.95;

            else
                gamma = Math.Round(double.Parse(textBox1.Text), 2);


            var index = 0;
            if (textBox2.Text == "")
                index = 1;
            else
                index = int.Parse(textBox2.Text);

            ReadFile(index);
        }
        private void CrearLabels()
        {
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label12.Text = "";
            label15.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Inicialaze();
            MainProgram();
            dataGridView1.DataSource = GetDataTable(countIntrevals);
            FillTable();
            LabelFill();
            ChartFill();
        }


        #region Заполнение 
        private void LabelFill()
        {
            label3.Text = $"Икс с чертой = {xWithDash}";
            label4.Text = $"Сигма = {Math.Round(sigma, 3)}";
            label5.Text = $"Mo = {MZero()}";
            label6.Text = $"Me = {mediana}";
            label7.Text = $"A3 = {A3()}";
            label8.Text = $"Ek = {Ek()}";
            label9.Text = $"{leftXChert} < (икс с прямой чертой) < {rightXChert}";
            label10.Text = $"{leftSChert} < (сигма с прямой чертой) < {rightSChert}";
            label12.Text = String.Join(", ", numbers);
            label15.Text = tShtrih().ToString();
        }
        private void ComboBoxFill()
        {
            comboBox1.Items.Insert(0, "5");
            comboBox1.Items.Insert(1, "6");
            comboBox1.Items.Insert(2, "7");
            comboBox1.Items.Insert(3, "8");
            comboBox1.Items.Insert(4, "9");
            comboBox1.Items.Insert(5, "10");
            comboBox1.Items.Insert(6, "11");
            comboBox1.Items.Insert(7, "12");
            comboBox1.Items.Insert(8, "13");
            comboBox1.Items.Insert(9, "14");
            comboBox1.Items.Insert(10, "15");
            comboBox1.Items.Insert(11, "16");
            comboBox1.Items.Insert(12, "17");
            comboBox1.Items.Insert(13, "18");
            comboBox1.Items.Insert(14, "19");
            comboBox1.Items.Insert(15, "20");
            comboBox1.Items.Insert(16, "25");
            comboBox1.Items.Insert(17, "30");
            comboBox1.Items.Insert(18, "35");
            comboBox1.Items.Insert(19, "40");
            comboBox1.Items.Insert(20, "45");
        }
        private void ChartFill()
        {
            var b = xMin;
            for (int i = 0; i < countIntrevals; i++)
            {
                chart1.Series["Gistogram"].Points.AddXY($"{b}-{b + h}", $"{intervalNumbers[i]}");
                b += h;
            }

            chart1.Titles.Add("Гистограмма");
        }
        private void FillTable()
        {
            double b = xMin;
            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = b.ToString() + "-" + (b + h).ToString();
                b += h;
            }

            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = intervalNumbers[i];
            }

            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value = nNakoplen[i];
            }

            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[3].Value = xAverage[i];
            }

            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[4].Value = Math.Round((xAverage[i] - xWithDash), 2);
            }
            for (int i = 0; i < countIntrevals; i++)
            {
                dataGridView1.Rows[i].Cells[5].Value = Math.Round(Math.Pow(xAverage[i] - xWithDash, 2), 2);
            }


        }
        #endregion

        #region Ненужное
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        private DataTable GetDataTable(int count)
        {
            DataTable table = new DataTable();

            table.Columns.Add();
            table.Columns[0].ColumnName = "Промежутки";
            for (int i = 0; i < count; i++)
                table.Rows.Add();

            table.Columns.Add();
            table.Columns[1].ColumnName = "Ni";
            table.Columns.Add();
            table.Columns[2].ColumnName = "Накопленные";
            table.Columns.Add();
            table.Columns[3].ColumnName = "Xi(сред)";
            table.Columns.Add();
            table.Columns[4].ColumnName = "Xi(сред) - x(с чертой)";
            table.Columns.Add();
            table.Columns[5].ColumnName = "x^2";


            return table;
        }

        #region Главные висчитывающие функции
        private void MainProgram()
        {
            // Сортировка и нахождение min и max и нахождение n
            //countOfNumbers = numbers.Length;
            numbers = numbers.OrderBy(x => x).ToArray(); // Сортировка на всякий
            xMin = numbers[0];
            xMax = numbers[numbers.Length - 1];

            // Находим k
            k = Math.Floor(1 + (3.322 * Math.Log10(countOfNumbers)));
            //Находим шаг h
            h = Math.Round((xMax - xMin) / k);
            // Находим колво интервалов
            double a = xMin;
            while (true)
            {
                if (a <= xMax)
                {
                    a += h;
                    countIntrevals++;
                }
                else
                {
                    break;
                }
            }

            intervalNumbers = CalcIntervals();
            xAverage = new double[countIntrevals];
            xAverage = Averange();
            xWithDash = xDash();
            sigma = Math.Sqrt(SigmaPow(2));
            nNakoplen = nNak();
            mediana = Mediana();
            xWithPramyaCherta();
            SigmaPramQ();
        }
        private void SigmaPramQ()
        {
            double alfa1 = (1 - gamma) / 2;
            double alfa2 = (1 + gamma) / 2;

            int a = Array.IndexOf(alfaInTable, Math.Round(alfa1, 3));
            int b = Array.IndexOf(alfaInTable, Math.Round(alfa2, 3));

            double xiq1 = 1;
            double xiq2 = 1;

            for (int i = 0; i < hiQuad.GetLength(0); i++)
            {
                if (hiQuad[i, 0] == countOfNumbers - 1)
                {
                    xiq1 = hiQuad[i, a + 1];
                    xiq2 = hiQuad[i, b + 1];
                }
            }

            leftSChert = Math.Round(((countOfNumbers - 1) * Math.Pow(sigma, 2)) / Math.Round(xiq1, 2), 2);
            rightSChert = Math.Round(((countOfNumbers - 1) * Math.Pow(sigma, 2)) / Math.Round(xiq2, 2), 2);
        }
        private void xWithPramyaCherta()
        {
            var tSh = tShtrih();

            leftXChert = Math.Round(xWithDash - ((tSh * sigma) / Math.Sqrt(countOfNumbers)), 2);
            rightXChert = Math.Round(xWithDash + ((tSh * sigma) / Math.Sqrt(countOfNumbers)), 2);
        }
        private double tShtrih()
        {
            //int a = 0;
            //double t = 0;
            //switch (gamma)
            //{
            //    case 0.95:
            //        a = 1;
            //        break;
            //    case 0.99:
            //        a = 2;
            //        break;
            //    case 0.999:
            //        a = 3;
            //        break;
            //}
            //for (int i = 0; i < ty.GetLength(0); i++)
            //{
            //    if (ty[i, 0] == countOfNumbers)
            //        t = ty[i, a];
            //}
            double y = chart1.DataManipulator.Statistics.InverseTDistribution(gamma, countOfNumbers - 1);
            return y;
        }
        private double SigmaPow(int degree)
        {
            double sum = 0;
            for (int i = 0; i < countIntrevals; i++)
            {
                sum = sum + Math.Pow((xAverage[i] - xWithDash), degree) * intervalNumbers[i];
            }
            return sum / countOfNumbers;
        }
        private double xDash()
        {
            double a = 0;
            for (int i = 0; i < countIntrevals; i++)
            {
                a += xAverage[i] * intervalNumbers[i];
            }
            return Math.Round((a / countOfNumbers), 2);
        }
        private double[] Averange()
        {
            double a = xMin;
            double[] mas = new double[countIntrevals];
            for (int i = 0; i < countIntrevals; i++)
            {
                mas[i] = (a + (a + h)) / 2;
                a += h;
            }
            return mas;
        }
        private double[] CalcIntervals()
        {
            double a = xMin;
            double[] mas = new double[countIntrevals];
            for (int i = 0; i < countIntrevals; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (numbers[j] >= a && numbers[j] < a + h)
                    {
                        mas[i]++;
                    }
                }
                a += h;
            }
            return mas;
        }
        private double MZero()
        {
            double a = 1;
            if (Array.IndexOf(intervalNumbers, intervalNumbers.Max()) == 0)
            {
                a = intervalNumbers.Max() -
                intervalNumbers[Array.IndexOf(intervalNumbers, intervalNumbers.Max())];
            }
            else
            {

                a = intervalNumbers.Max() -
                    intervalNumbers[Array.IndexOf(intervalNumbers, intervalNumbers.Max()) - 1];
            }
            double b;
            if (intervalNumbers.Max() -
                intervalNumbers[Array.IndexOf(intervalNumbers, intervalNumbers.Max()) + 1] == 0)
            {
                b = intervalNumbers.Max() -
                intervalNumbers[Array.IndexOf(intervalNumbers, intervalNumbers.Max())];
            }
            else
            {
                b = intervalNumbers.Max() -
                intervalNumbers[Array.IndexOf(intervalNumbers, intervalNumbers.Max()) + 1];
            }

            var c = Math.Round(XZero() + (((a) / (a + b)) * h), 2);
            return c;
        }
        private double XZero()
        {
            double b = xMin;

            var a = Array.IndexOf(intervalNumbers, intervalNumbers.Max());

            for (int i = 0; i < a; i++)
            {
                b += h;
            }

            return b;
        }
        private double Mediana()
        {
            double a = 1;
            if (Array.IndexOf(intervalNumbers, intervalNumbers.Max()) == 0)
            {
                a = nNakoplen[Array.IndexOf(intervalNumbers, intervalNumbers.Max())];
            }
            else
            {
                a = nNakoplen[Array.IndexOf(intervalNumbers, intervalNumbers.Max()) - 1];
            }
            var b = intervalNumbers.Max();

            double sum = ((0.5 * countOfNumbers) - a) * h;
            sum = XZero() + (sum / b);

            return Math.Round(sum, 2);
        }
        private double[] nNak()
        {
            double a = intervalNumbers[0];
            double[] mas = new double[countIntrevals];
            mas[0] = a;
            for (int i = 1; i < countIntrevals; i++)
            {
                mas[i] = mas[i - 1] + intervalNumbers[i];
            }
            return mas;
        }
        private double A3()
        {
            var s = Math.Pow(sigma, 3);
            var m = SigmaPow(3);

            return Math.Round(m / s, 3) * -1;
        }
        private double Ek()
        {
            var s = Math.Pow(sigma, 4);
            var m = SigmaPow(4);

            return Math.Round((m / s) - 3, 3) * -1;
        }
        #endregion

        private void ReadFile(int index)
        {
            var lines = File.ReadAllLines($"C:\\Users\\ilia7\\Desktop\\Numbers.csv");

            if (countOfNumbers == 0)
                countOfNumbers = 30;

            numbers = new double[countOfNumbers];

            for (int i = 0; i < numbers.Length; i++)
            {
                var cells = lines[i].Split(',');
                numbers[i] = double.Parse(cells[index - 1]);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            countOfNumbers = int.Parse(comboBox1.SelectedItem.ToString());
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            dataGridView1.DataSource = GetDataTable(0);
            countOfNumbers = 0;
            countIntrevals = 0;
            numbers = new double[countOfNumbers];
            Controls.Clear();
            InitializeComponent();
            FillBoxAndLabels();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}