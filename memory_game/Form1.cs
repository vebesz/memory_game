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
        GroupBox groupNumbers, groupTime, groupDigits;
        RadioButton rb6Numbers, rb9Numbers;
        RadioButton rb5Seconds, rb10Seconds, rb20Seconds;
        RadioButton rb1Digit, rb2Digits;
        Button btnStartGame;
        Timer displayTimer;
        NumbersDislplayForm numbersDislplayForm;

        public StartGameForm()
        {
            this.Size = new System.Drawing.Size(260, 500); // Méret beállítása


            // Csoportok létrehozása
            groupNumbers = new GroupBox { Text = "Számok száma", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(200, 100) };
            groupTime = new GroupBox { Text = "Megjelenítési idő", Location = new System.Drawing.Point(20, 130), Size = new System.Drawing.Size(200, 100) };
            groupDigits = new GroupBox { Text = "Számjegyek száma", Location = new System.Drawing.Point(20, 240), Size = new System.Drawing.Size(200, 100) };

            // Számok száma rádiógombok
            rb6Numbers = new RadioButton { Text = "6 szám", Location = new System.Drawing.Point(10, 20) };
            rb9Numbers = new RadioButton { Text = "9 szám", Location = new System.Drawing.Point(10, 40) };
            rb6Numbers.CheckedChanged += new EventHandler(RbNumbers_CheckedChanged);
            rb9Numbers.CheckedChanged += new EventHandler(RbNumbers_CheckedChanged);

            // Megjelenítési idő rádiógombok
            rb5Seconds = new RadioButton { Text = "5 másodperc", Location = new System.Drawing.Point(10, 20) };
            rb10Seconds = new RadioButton { Text = "10 másodperc", Location = new System.Drawing.Point(10, 40) };
            rb20Seconds = new RadioButton { Text = "20 másodperc", Location = new System.Drawing.Point(10, 60) };

            // Számjegyek száma rádiógombok
            rb1Digit = new RadioButton { Text = "1 jegyű", Location = new System.Drawing.Point(10, 20) };
            rb2Digits = new RadioButton { Text = "2 jegyű", Location = new System.Drawing.Point(10, 40) };

            // Rádiógombok hozzáadása a csoportokhoz
            groupNumbers.Controls.Add(rb6Numbers);
            groupNumbers.Controls.Add(rb9Numbers);
            groupTime.Controls.Add(rb5Seconds);
            groupTime.Controls.Add(rb10Seconds);
            groupTime.Controls.Add(rb20Seconds);
            groupDigits.Controls.Add(rb1Digit);
            groupDigits.Controls.Add(rb2Digits);

            // Csoportok hozzáadása a formhoz
            Controls.Add(groupNumbers);
            Controls.Add(groupTime);
            Controls.Add(groupDigits);


            btnStartGame = new Button
            {
                Text = "Játék indítása",
                Location = new System.Drawing.Point(20, 350),
                Size = new System.Drawing.Size(200, 30)
            };
            btnStartGame.Click += new EventHandler(StartGame_Click);

            Controls.Add(btnStartGame);

            displayTimer = new Timer();
            displayTimer.Tick += new EventHandler(DisplayTimer_Tick);
        }
        
        private void StartGame_Click(object sender, EventArgs e)
            {
            // input ellenőrzés
            if ((!rb6Numbers.Checked && !rb9Numbers.Checked) || (!rb5Seconds.Checked && !rb10Seconds.Checked && !rb20Seconds.Checked) || (!rb1Digit.Checked && !rb2Digits.Checked
                ))
            {
                MessageBox.Show("Nincs minden input megadva!");
                return;
            }

            // random számok generálása
            int numberCount = rb6Numbers.Checked ? 6 : 9;
            int showTime = rb5Seconds.Checked ? 5 : rb10Seconds.Checked ? 10 : 20;
            int[] numbers = new int[numberCount];
            Random rnd = new Random();
            for (int i = 0; i < numberCount; i++)
            {
            numbers[i] = rnd.Next(1, rb1Digit.Checked?10:100);
            }
            // Számok megjelenítése
            numbersDislplayForm = new NumbersDislplayForm(numbers);
            numbersDislplayForm.Show();
            displayTimer.Interval = showTime * 1000; // Idő átváltása ezredmásodpercre
            displayTimer.Start();
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            displayTimer.Stop();
            if (numbersDislplayForm != null && !numbersDislplayForm.IsDisposed)
            {
                numbersDislplayForm.Close();
            }
        }

        private void RbNumbers_CheckedChanged(object sender, EventArgs e)
        {
            if (rb6Numbers.Checked)
            {
                rb20Seconds.Enabled = true;
                rb5Seconds.Enabled = false;
            }
            if (rb9Numbers.Checked)
            {
                rb20Seconds.Enabled = false;
                rb5Seconds.Enabled = true;
            }
        }
    }
       
}


