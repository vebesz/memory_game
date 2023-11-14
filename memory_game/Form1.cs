using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public partial class StartGameForm : Form
    {
        private ComboBox cmbNumberCount;
        private ComboBox cmbDisplayTime;
        private ComboBox cmbNumberDigits;
        private Button btnStartGame;

        public StartGameForm()
        {
            InitializeComponent();

            // Számok számának beállítása
            NumericUpDown numericUpDownNumbers = new NumericUpDown();
            numericUpDownNumbers.Minimum = 6;
            numericUpDownNumbers.Maximum = 9;
            numericUpDownNumbers.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(numericUpDownNumbers);

            // Megjelenítési idő beállítása
            NumericUpDown numericUpDownTime = new NumericUpDown();
            numericUpDownTime.Minimum = 5;
            numericUpDownTime.Maximum = 20;
            numericUpDownTime.Location = new System.Drawing.Point(20, 50);
            this.Controls.Add(numericUpDownTime);

            // Jegyek számának beállítása
            NumericUpDown numericUpDownDigits = new NumericUpDown();
            numericUpDownDigits.Minimum = 1;
            numericUpDownDigits.Maximum = 2;
            numericUpDownDigits.Location = new System.Drawing.Point(20, 80);
            this.Controls.Add(numericUpDownDigits);

            // Indítógomb
            Button startButton = new Button();
            startButton.Text = "Játék indítása";
            startButton.Location = new System.Drawing.Point(20, 110);
            startButton.Click += new EventHandler(StartButton_Click);
            this.Controls.Add(startButton);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            // Itt lehet kezelni a játék indítását a kiválasztott beállításokkal
            MessageBox.Show("A játék elindult!");
        }
    }

}
