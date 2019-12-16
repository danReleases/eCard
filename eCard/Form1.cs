using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eCard
{
    public partial class Form1 : Form
    {
        int cardSelected;
        bool noSelection = true;

        Boolean[] deck = new Boolean[] { true, true, true, true, true };
        Boolean[] oppDeck = new Boolean[] { true, true, true, true, true };

        static String[] mains = new String[] { "King", "Slave", "Citizen" };

        Boolean emperorTurn = true;
        int roundCount = 1;
        Boolean roundChange = false;

        int[] winCounts = new int[] { 0, 0 };

        public Form1()
        {
            InitializeComponent();
            this.Text = "eCard";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (deck[0] == true)
            {
                cardSelected = 0;
                noSelection = false;
                String turnCard;
                if (emperorTurn)
                {
                    turnCard = mains[0];
                }
                else
                {
                    turnCard = mains[1];
                }

                if (!roundChange)
                {
                    label3.Text = "You have selected: " + turnCard;
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (deck[1] == true)
            {
                cardSelected = 1;
                noSelection = false;
                if (!roundChange)
                {
                    label3.Text = "You have selected: " + mains[2];
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (deck[2] == true)
            {
                cardSelected = 2;
                noSelection = false;
                if (!roundChange)
                {
                    label3.Text = "You have selected: " + mains[2];
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (deck[3] == true)
            {
                cardSelected = 3;
                noSelection = false;
                if (!roundChange)
                {
                    label3.Text = "You have selected: " + mains[2];
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (deck[4] == true)
            {
                cardSelected = 4;
                noSelection = false;
                if (!roundChange)
                {
                    label3.Text = "You have selected: " + mains[2];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (noSelection)
            {
                label3.Text = "Please Select a Card.";
            }

            else
            {

                switch (cardSelected)
                {
                    case 0:
                        if (emperorTurn)
                        {
                            pictureBox2.Image = eCard.Properties.Resources.emperor;
                        }
                        else
                        {
                            pictureBox2.Image = eCard.Properties.Resources.slave;
                        }
                        pictureBox2.Visible = true;
                        pictureBox3.Visible = false;
                        deck[0] = false;
                        break;
                    case 1:
                        pictureBox2.Image = eCard.Properties.Resources.citizen;
                        pictureBox2.Visible = true;
                        pictureBox4.Visible = false;
                        deck[1] = false;
                        break;
                    case 2:
                        pictureBox2.Image = eCard.Properties.Resources.citizen;
                        pictureBox2.Visible = true;
                        pictureBox5.Visible = false;
                        deck[2] = false;
                        break;
                    case 3:
                        pictureBox2.Image = eCard.Properties.Resources.citizen;
                        pictureBox2.Visible = true;
                        pictureBox6.Visible = false;
                        deck[3] = false;
                        break;
                    case 4:
                        pictureBox2.Image = eCard.Properties.Resources.citizen;
                        pictureBox2.Visible = true;
                        pictureBox7.Visible = false;
                        deck[4] = false;
                        break;
                }

                int oppCard = opponentPlay();
                gameCheck(cardSelected, oppCard);
                noSelection = true;
            }
        }

        private int opponentPlay()
        {
            Random r = new Random();
            int dn;
            while (true)
            {
                dn = r.Next(0, 5);
                if (oppDeck[dn] == true)
                {
                    break;
                }
            }

            if (dn == 0)
            {
                if (emperorTurn)
                {
                    pictureBox1.Image = eCard.Properties.Resources.slave;
                }
                else
                {
                    pictureBox1.Image = eCard.Properties.Resources.emperor;
                }
            }
            else
            {
                pictureBox1.Image = eCard.Properties.Resources.citizen;
            }

            pictureBox1.Visible = true;
            oppDeck[dn] = false;
            return dn;
        }

        private void gameCheck(int player, int opp)
        {

            if (player == 0)
            {
                if (opp == 0)
                {
                    nextMatch(!emperorTurn);
                }
                else
                {
                    nextMatch(emperorTurn);
                }
            }
            else
            {
                if (opp == 0)
                {
                    nextMatch(emperorTurn);
                }

                else
                {
                    //Citizen vs Citzien
                }
            }
        }

        private void nextMatch(Boolean playerWon)
        {

            if (playerWon)
            {
                if (emperorTurn)
                {
                    winCounts[0] += 1;
                }
                else
                {
                    winCounts[0] += 3;
                }

                label1.Text = "You: " + (winCounts[0]);
                label3.Text = "You won this match!";
            }
            else
            {
                if (emperorTurn)
                {
                    winCounts[1] += 3;
                }
                else
                {
                    winCounts[1] += 1;
                }
                label2.Text = "Opponent: " + (winCounts[1]);
                label3.Text = "You lost this match!";
            }

            roundCount++;
            roundChange = true;

            if (roundCount == 4 || roundCount == 7 || roundCount == 10)
            {
                button2.Text = "Next Round" + Environment.NewLine + "(Deck change)";
            }
            else
            {
                button2.Text = "Next Match";
            }

            if (roundCount > 12)
            {
                endGame();
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button2.Focus();
            }
        }

        private void resetCards()
        {
            noSelection = true;
            deck = new Boolean[] { true, true, true, true, true };
            oppDeck = new Boolean[] { true, true, true, true, true };

            if (roundCount == 4 || roundCount == 10)
            {
                pictureBox3.Image = eCard.Properties.Resources.slave;
            }
            else if (roundCount == 7)
            {
                pictureBox3.Image = eCard.Properties.Resources.emperor;
            }

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;

            button1.Enabled = true;
            button2.Enabled = false;

            label3.Text = "No Selection";
            roundChange = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text = "Round " + roundCount + "/12";

            if (roundCount == 4 || roundCount == 7 || roundCount == 10)
            {
                emperorTurn = !emperorTurn;
            }
            resetCards();
        }

        private void endGame()
        {
            button1.Enabled = false;
            button2.Enabled = false;

            if (winCounts[0] > winCounts[1])
            {
                pictureBox8.Image = eCard.Properties.Resources.kaijiWin;
                pictureBox9.Image = eCard.Properties.Resources.tonegawaLose;

                label3.Text = "You Win!";
            }
            else if (winCounts[0] < winCounts[1])
            {
                pictureBox8.Image = eCard.Properties.Resources.kaijiLose;
                pictureBox9.Image = eCard.Properties.Resources.tonegawaWin;

                label3.Text = "You Lose!";
            }
            else
            {
                pictureBox8.Image = eCard.Properties.Resources.kaijiLose;
                pictureBox9.Image = eCard.Properties.Resources.tonegawaLose;

                label3.Text = "It's a Draw!";
            }

            pictureBox8.Visible = true;
            pictureBox9.Visible = true;

        }


        private void s(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start("http://tobakumokushirokukaiji.wikia.com/wiki/E_Card");
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (deck[0] == true)
            {
                pictureBox3.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Cursor = Cursors.Default;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (deck[1] == true &&  (roundChange!=true))
            {
                pictureBox4.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Cursor = Cursors.Default;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            if (deck[2] == true && (roundChange != true))
            {
                pictureBox5.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Cursor = Cursors.Default;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            if (deck[3] == true && (roundChange != true))
            {
                pictureBox6.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Cursor = Cursors.Default;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            if (deck[4] == true && (roundChange != true))
            {
                pictureBox7.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Cursor = Cursors.Default;
        }
    }
}
