using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp25
{
    public partial class Form1 : Form
    {
        private Button movingButton;
        private Timer timer;
        private Random random;
        private int score;
        private int secondsLeft;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }
        private void InitializeGame()
        {
            movingButton = new Button();
            movingButton.BackColor = Color.Red;
            movingButton.Width = 50;
            movingButton.Height = 50;
            movingButton.Click += MovingButton_Click;
            Controls.Add(movingButton);

            timer = new Timer();
            timer.Interval = 700;
            timer.Tick += Timer_Tick;

            random = new Random();
            score = 0;
            secondsLeft = 30;
        }
        private void StartGame()
        {
            int x = random.Next(0, ClientSize.Width - movingButton.Width);
            int y = random.Next(0, ClientSize.Height - movingButton.Height);
            movingButton.Location = new Point(x, y);
            movingButton.Visible = true;
            timer.Start();
        }
        private void StopGame()
        {
            timer.Stop();
            movingButton.Visible = false;

            if (secondsLeft == 0)
            {
                string message = $"Гра закінчена!\nScore: {score}\n\nХочете зберегти свої очки?";
                DialogResult result = MessageBox.Show(message, "Гра закінчена", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveScore();
                }
            }
        }
        private void MovingButton_Click(object sender, EventArgs e)
        {
            if (secondsLeft > 0)
            {
                int width = random.Next(20, 100);
                int height = random.Next(20, 100);
                movingButton.Size = new Size(width, height);
                score++;
                label1.Text = $"Score: {score}";
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;
            label2.Text = $"Залишилось часу: {secondsLeft} секунд";

            if (secondsLeft == 0)
            {
                StopGame();
            }
            else
            {
                int x = random.Next(0, ClientSize.Width - movingButton.Width);
                int y = random.Next(0, ClientSize.Height - movingButton.Height);
                movingButton.Location = new Point(x, y);
            }
        }
        private void SaveScore()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt";
            saveFileDialog.Title = "Save Score";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "Scores.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
                {
                    writer.WriteLine($"Score: {score}");
                    writer.WriteLine($"Date: {DateTime.Now}");
                }

                MessageBox.Show("Очки були збережені.", "Save Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
