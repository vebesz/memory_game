using System;
using System.Drawing;
using System.Windows.Forms;

namespace memory_game
{
    public partial class Form3 : Form
    {
        Label resultLabel;
        Button restartButton;
        Button exitButton;

        public Form3(int correctGuesses, int totalGuesses)
        {
            InitializeComponent();

            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Játék vége";

            resultLabel = new Label
            {
                Text = $"Gratulálok! {correctGuesses} számot találtál el {totalGuesses}-ból.",
                AutoSize = true,
                Location = new Point(50, 20),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            this.Controls.Add(resultLabel);

            restartButton = new Button
            {
                Text = "Újrakezdés",
                Size = new Size(100, 30),
                Location = new Point(50, 100),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            restartButton.Click += RestartButton_Click;
            this.Controls.Add(restartButton);

            exitButton = new Button
            {
                Text = "Kilépés",
                Size = new Size(100, 30),
                Location = new Point(200, 100),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // Itt implementálhatod az újrakezdés logikáját
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
