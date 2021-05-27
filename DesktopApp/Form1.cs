using DesktopApp.Enum;

using Kursach.Algorithms;
using Kursach.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
                ValidateInputData();

                CreateCalculateLutsenkoButton();

                CreateCalculateMansouryButton();

                CreateCalculateGreedyButton();

                CreateRandomButton();

                CreateMatrixControls();

                DisableInputButtons();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Кількість задач та працівників, а також ефективність повинні бути більшими за 0.");
            }
            catch (Exception)
            {
                MessageBox.Show("Щось пішло не так.");
            }
        }

        private void calculateLutsenkoButton_Click(object sender, EventArgs e)
        {
            ProcessAlgorithm(Algorithm.Lutsenko);
        }

        private void calculateMansouryButton_Click(object sender, EventArgs e)
        {
            ProcessAlgorithm(Algorithm.Mansoury);
        }

        private void calculateGreedyButton_Click(object sender, EventArgs e)
        {
            ProcessAlgorithm(Algorithm.Greedy);
        }

        private void ProcessAlgorithm(Algorithm algorithm)
        {
            try
            {
                var data = new List<List<int>>();

                for (int i = 0; i < WorkersCount; i++)
                {
                    var list = new List<int>();
                    for (int j = 0; j < TasksCount; j++)
                    {
                        list.Add(Int32.Parse(Controls[i.ToString() + " " + j.ToString()].Text));
                    }
                    data.Add(list);
                }
                var dataCopy = data.Select(x => x.ToList()).ToList();

                IAlgorithm algorithmHandler = null;

                switch (algorithm)
                {
                    case Algorithm.Lutsenko:
                        algorithmHandler = new LutsenkoAlgorithm();
                        break;
                    case Algorithm.Mansoury:
                        algorithmHandler = new ElMansouryAlgorithm();
                        break;
                    case Algorithm.Greedy:
                        algorithmHandler = new GreedyAlgorithm();
                        break;
                }

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var result = algorithmHandler.Handle(data);

                stopwatch.Stop();

                int cf = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    Controls[result[i][0].ToString() + " " + result[i][1].ToString()].BackColor = Color.LimeGreen;
                    cf += dataCopy[result[i][0]][result[i][1]];
                }

                CreateCfControls(cf);

                DisableOutputButtons();

                CreateTimeControl(stopwatch.Elapsed);

                ActiveControl = null;
            }
            catch (FormatException)
            {
                MessageBox.Show("Ефективність виконання задачі повинна бути числом.");
            }
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            for (int i = 0; i < WorkersCount; i++)
            {
                for (int j = 0; j < TasksCount; j++)
                {
                    Controls[i.ToString() + " " + j.ToString()].Text = random.Next(1, 20).ToString();
                }
            }
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    int[][] fileData = File.ReadAllLines(fileDialog.FileName)
                       .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                       .ToArray();

                    var workersCount = fileData.Length;
                    var tasksCount = fileData.GroupBy(row => row.Length).Single().Key;

                    SetTasksAndWorkersCount(workersCount, tasksCount);

                    ValidateInputData();

                    CreateMatrixControls();

                    DisableInputButtons();

                    CreateCalculateLutsenkoButton();

                    CreateCalculateGreedyButton();

                    CreateCalculateMansouryButton();

                    for (int i = 0; i < workersCount; i++)
                    {
                        for (int j = 0; j < tasksCount; j++)
                        {
                            Controls[i.ToString() + " " + j.ToString()].Text = fileData[i][j].ToString();
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Кількість задач та працівників, а також ефективність повинні бути більшими за 0.");
            }
            catch (Exception)
            {
                MessageBox.Show("Щось пішло не так.");
            }
        }

        private void ValidateInputData()
        {
            TasksCount = Int32.Parse(tasksTextBox.Text);
            WorkersCount = Int32.Parse(workersTextBox.Text);

            if (WorkersCount >= TasksCount)
            {
                throw new ArgumentException("Кількість задач повина бути більше ніж кількість працівників.");
            }
        }

        private void CreateCalculateLutsenkoButton()
        {
            var calculateButton = new Button()
            {
                Name = "calculateLutsenkoButton",
                Text = "Підрахувати Луценко",
                Location = new Point(506, 26),
                Size = new Size(86, 39)
            };
            calculateButton.Click += new EventHandler(calculateLutsenkoButton_Click);
            Controls.Add(calculateButton);
        }

        private void CreateCalculateMansouryButton()
        {
            var calculateButton = new Button()
            {
                Name = "calculateMansouryButton",
                Text = "Підрахувати Ель-Мансурі",
                Location = new Point(622, 26),
                Size = new Size(86, 39)
            };
            calculateButton.Click += new EventHandler(calculateMansouryButton_Click);
            Controls.Add(calculateButton);
        }

        private void CreateCalculateGreedyButton()
        {
            var calculateButton = new Button()
            {
                Name = "calculateGreedyButton",
                Text = "Підрахувати Жадібний",
                Location = new Point(738, 26),
                Size = new Size(86, 39)
            };
            calculateButton.Click += new EventHandler(calculateGreedyButton_Click);
            Controls.Add(calculateButton);
        }

        private void CreateRandomButton()
        {
            var randomButton = new Button()
            {
                Name = "randomButton",
                Text = "Random",
                Location = new Point(854, 26),
                Size = new Size(86, 39)
            };
            randomButton.Click += new EventHandler(randomButton_Click);
            Controls.Add(randomButton);
        }

        private void CreateTimeControl(TimeSpan time)
        {
            var timeLabel = new Label()
            {
                Name = "timeLabel",
                Text = "Time:",
                Location = new Point(950, 28),
                Size = new Size(40, 20)
            };

            var timeTextBox = new TextBox()
            {
                Name = "timeLabel",
                Text = time.ToString(),
                Location = new Point(993, 26),
                Size = new Size(100, 20)
            };
            Controls.Add(timeLabel);
            Controls.Add(timeTextBox);
        }

        private void CreateMatrixControls()
        {
            for (int i = 0; i < TasksCount; i++)
            {
                var label = new Label()
                {
                    Text = $"Задача {i + 1}",
                    Location = new Point(i * 75 + 52, 98),
                    Size = new Size(75, 20)
                };
                Controls.Add(label);
            }

            for (int i = 0; i < WorkersCount; i++)
            {
                var label = new Label()
                {
                    Text = $"Вик. {i + 1}",
                    Location = new Point(8, i * 50 + 128),
                    Size = new Size(46, 20)
                };
                Controls.Add(label);
                for (int j = 0; j < TasksCount; j++)
                {
                    var effTextBox = new TextBox()
                    {
                        Name = i.ToString() + " " + j.ToString(),
                        Location = new Point(j * 75 + 54, i * 50 + 125),
                        Size = new Size(50, 10)
                    };
                    Controls.Add(effTextBox);
                }
            }
        }

        private void CreateCfControls(int cf)
        {
            var cfLabel = new Label()
            {
                Text = "ЦФ:",
                Location = new Point(10, WorkersCount * 50 + 127),
                Size = new Size(40, 20)
            };
            Controls.Add(cfLabel);

            var cfTextBox = new TextBox()
            {
                Name = "resultTextBox",
                Location = new Point(54, WorkersCount * 50 + 125),
                Size = new Size(50, 10),
                Text = cf.ToString(),
                BackColor = Color.LightYellow
            };
            Controls.Add(cfTextBox);
        }

        private void SetTasksAndWorkersCount(int workersCount, int tasksCount)
        {
            workersTextBox.Text = workersCount.ToString();
            tasksTextBox.Text = tasksCount.ToString();
        }

        private void DisableInputButtons()
        {
            Controls["createMatrixButton"].Enabled = false;
            Controls["fileButton"].Enabled = false;
            ActiveControl = null;
        }

        private void DisableOutputButtons()
        {
            Controls["calculateLutsenkoButton"].Enabled = false;
            Controls["calculateMansouryButton"].Enabled = false;
            Controls["calculateGreedyButton"].Enabled = false;
            if (Controls.ContainsKey("randomButton"))
            {
                Controls["randomButton"].Enabled = false;
            }
        }
    }
}
