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
        List<Card> cards = new List<Card>();
        Timer hideTimer;
        private Button firstRevealedCard = null;
        private Button secondRevealedCard = null;
        private Timer checkTimer;
        private int maxGuesses;
        private int currentGuesses;
        private int successGuesses;
        
        private bool started = false;


        public NumbersDislplayForm(int[] numbers, int showTime)
        {
            InitializeComponent();

            Console.WriteLine(numbers.Length);

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.maxGuesses = numbers.Length;
            this.currentGuesses = 0;

            // számok elrejtésének timere
            hideTimer = new Timer();
            hideTimer.Interval = showTime * 1000;
            hideTimer.Tick += new EventHandler(HideTimer_Tick);
            hideTimer.Start();

          

            // ellenőrzés időzítő
            checkTimer = new Timer();
            checkTimer.Interval = 2000; // 2 másodperc
            checkTimer.Tick += new EventHandler(CheckTimer_Tick);


            // kártyák megkeverése
            var allNumbers = numbers.Concat(numbers).OrderBy(x => Guid.NewGuid()).ToArray();


            int rows = numbers.Length; // Sorok száma
            int cols = 6; // Mindig 2 oszlop, mivel minden szám kétszer jelenik meg
            int buttonWidth = 120;
            int buttonHeight = 120;
            int spacing = 10;
            int startX = 10;
            int startY = 10;

            //this.Size = new Size(300, 1000);
            this.Size = new Size(cols * (buttonWidth + spacing) + 20, rows * (buttonHeight + spacing) + 20);
            // újraméretezhető
            this.FormBorderStyle = FormBorderStyle.Sizable;
            
            foreach (int number in allNumbers)
            {
                cards.Add(new Card(number));
            }


            int index = 0;
            foreach (Card card in cards)
            {
                int row = index / cols;
                int col = index % cols;
                Button cardButton = new Button
                {
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Location = new Point(startX + col * (buttonWidth + spacing), startY + row * (buttonHeight + spacing)),
                    Tag = card,
                    Text = card.Number.ToString(), // Kezdetben jelenítsd meg a számot
                    Font = new Font("Arial", 24, FontStyle.Bold),
                    BackColor = Color.LightGray
                };
                cardButton.Click += new EventHandler(CardButton_Click);

                this.Controls.Add(cardButton);
                index++;
            }
            this.Size = new Size(cols * (buttonWidth + spacing) + 20, 300);

        }

        // számok elrejtése X idő után
        private void HideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.Text = "?"; // Szöveg cseréje kérdőjelre
                    started = true;
                }
            }
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            if (!started) return; // Ha még nem kezdődött el a játék, ne csináljon semmit

            Button clickedButton = sender as Button;
            Card card = clickedButton.Tag as Card;

            if (!card.IsMatched && clickedButton != firstRevealedCard)
            {
                if (firstRevealedCard == null)
                {
                    firstRevealedCard = clickedButton;
                    card.Reveal();
                    clickedButton.Text = card.Number.ToString();
                }
                else if (secondRevealedCard == null)
                {
                    secondRevealedCard = clickedButton;
                    card.Reveal();
                    clickedButton.Text = card.Number.ToString();
                    checkTimer.Start(); // Indítja a timer-t az összehasonlításhoz

                    currentGuesses++;

                    // ha elfogytak a tippek
                    if (currentGuesses >= maxGuesses)
                    {
                        // záró képernyő mutatása
                        Form3 form3 = new Form3(maxGuesses, successGuesses);
                        form3.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            checkTimer.Stop();

            Card firstCard = firstRevealedCard.Tag as Card;
            Card secondCard = secondRevealedCard.Tag as Card;

            if (firstCard.Number == secondCard.Number)
            {
                // Egyezik
                firstRevealedCard.ForeColor = Color.Green;
                secondRevealedCard.ForeColor = Color.Green;
                firstCard.IsMatched = true;
                secondCard.IsMatched = true;
                successGuesses++;
            }
            else
            {
                // Nem egyezik, fordítsd vissza őket
                firstRevealedCard.Text = "?";
                secondRevealedCard.Text = "?";
                firstCard.Hide();
                secondCard.Hide();
            }

            firstRevealedCard = null;
            secondRevealedCard = null;
        }

    }

    public class Card
    {
        public int Number { get; private set; }
        public bool IsRevealed { get; set; }
        public bool IsMatched { get; set; }

        public Card(int number)
        {
            Number = number;
            IsRevealed = true;
            IsMatched = false;
        }

        public void Reveal()
        {
            IsRevealed = true;
        }

        public void Hide()
        {
            IsRevealed = false;
        }
    }
}
