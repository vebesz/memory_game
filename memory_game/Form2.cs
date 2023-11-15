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
    public partial class NumbersDislplayForm : Form
    {
        public NumbersDislplayForm(int[] numbers)
        {
            this.Size = new Size(300, numbers.Length == 6 ? 300 : 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            int labelY = 20;
            foreach (int number in numbers)
            {
                Label numberLabel = new Label
                {
                    Text = number.ToString(),
                    Location = new Point(10, labelY),
                    Size = new Size(280, 25),
                    // set a bigger font size
                    Font = new Font("Arial", 16, FontStyle.Bold),
                };

                this.Controls.Add(numberLabel);
                labelY += 30;
            };
        }
    }
}
