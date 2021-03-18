using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        private int TasksCount { get; set; }
        private int WorkersCount { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void createMatrixButton_Click(object sender, EventArgs e)
        {
            try
            {
                TasksCount = Int32.Parse(tasksTextBox.Text);
                WorkersCount = Int32.Parse(workersTextBox.Text);

                if(WorkersCount >= TasksCount)
                {
                    throw new ArgumentException("Кількість задач повина бути більше ніж кількість працівників.");
                }

                var calculateButton = new Button();
                calculateButton.Name = "calculateButton";
                calculateButton.Text = "Підрахувати";
                calculateButton.Location = new Point(391, 26);
                calculateButton.Size = new Size(86, 39);
                calculateButton.Click += new EventHandler(calculateButton_Click);
                Controls.Add(calculateButton);

                for (int i = 0; i < WorkersCount; i++)
                {
                    for (int j = 0; j < TasksCount; j++)
                    {
                        var effTextBox = new TextBox()
                        {
                            Name = i.ToString() + j.ToString(),
                            Location = new Point(j * 75 + 30, i * 50 + 100),
                            Size = new Size(50, 10)
                        };
                        Controls.Add(effTextBox);
                    }
                }
                Controls["createMatrixButton"].Enabled = false;
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(FormatException ex)
            {
                MessageBox.Show("Кількість задач та працівників повина бути більшою за 0.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Щось пішло не так.");
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new List<List<int>>();
                try
                {
                    for (int i = 0; i < WorkersCount; i++)
                    {
                        var list = new List<int>();
                        for (int j = 0; j < TasksCount; j++)
                        {
                            list.Add(Int32.Parse(Controls[i.ToString() + j.ToString()].Text));
                        }
                        data.Add(list);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Ефективність виконання задачі повинна бути числом.");
                }

                var result = LutsenkoAlgorithm.Handle(data);
                for (int i = 0; i < result.Count; i++)
                {
                    for (int j = 0; j < (result.Sum(x => x.Count) / data.Count); j++)
                    {
                        Controls[result[i][0].ToString() + result[i][1].ToString()].BackColor = Color.LimeGreen;
                    }
                }

                Controls["calculateButton"].Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Щось пішло не так.");
            }
        }
    }
}
