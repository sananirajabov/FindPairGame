using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class form : Form
    {

        private Button[] buttons = null;
        private List<int> listPartOne = null;
        private List<int> listPartTwo = null;
        private List<int> list = null;
        private Color[] colors = null;
        private Button firstButton = null;
        private Button secondButton = null;
        private string firstValue = "";
        private string secondValue = "";
        private int count = 0;
        private int colorIndex = 0;
        private int order = 0;
        private const int ME = 0;
        private const int YOU = 1;
        private static int scoreOfMe = 0;
        private static int scoreOfYou = 0;
        private static int win = 0;

        public form()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            listPartOne = new List<int>();
            listPartTwo = new List<int>();
            list = new List<int>();

            lblScoreOfMe.Text = lblScoreOfYou.Text = "0";
            lblOrderOfMe.ForeColor = Color.Red;
            lblOrderOfMe.Text = "X";

            buttons = new Button[] {
                button1, button2, button3, button4, button5, button6,
                button7, button8, button9, button10, button11, button12,
                button13, button14, button15, button16, button17, button18,
                button19, button20, button21, button22, button23, button24,
                button25, button26, button27, button28, button29, button30,
                button31, button32, button33, button34, button35, button36
            };

            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].MouseClick += new MouseEventHandler(buttonCLick);
                buttons[i].BackgroundImage = Properties.Resources.poker;
            }

            colors = new Color[] {
                Color.Aqua, Color.Crimson, Color.Beige, Color.Brown, Color.Blue, Color.BurlyWood,
                Color.Chocolate, Color.Yellow, Color.Orange, Color.Green, Color.LightBlue, Color.Pink,
                Color.Red, Color.SeaGreen, Color.Gray, Color.GreenYellow, Color.Gold, Color.DarkViolet
            };
        }

        private void form_Load(object sender, EventArgs e)
        {
            createRandomCards();
        }

        private void createRandomCards()
        {
            Random random = new Random();

            for(int i = 0; i < 18; i++)
            {
                int randomValue = random.Next(1, 19);

                while (listPartOne.Contains(randomValue))
                {
                    randomValue = random.Next(1, 19);
                }

                listPartOne.Add(randomValue);
            }

            for (int i = 0; i < 18; i++)
            {
                int randomValue = random.Next(1, 19);

                while (listPartTwo.Contains(randomValue))
                {
                    randomValue = random.Next(1, 19);
                }

                listPartTwo.Add(randomValue);
            }

            for (int i = 0; i < listPartOne.Count; i++)
            {
                list.Add(listPartOne[i]);
                list.Add(listPartTwo[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                buttons[i].Tag = list[i].ToString();
            }
        }

        private void buttonCLick(object sender, MouseEventArgs e)
        {
            if (count == 0)
            {
                firstButton = new Button();
                firstButton = sender as Button;
                firstButton.Enabled = false;
                firstValue = firstButton.Tag.ToString();
                openCard(firstButton).Text = firstValue;
                count++;
            }
            else
            {
                secondButton = new Button();
                secondButton = sender as Button;
                secondValue = secondButton.Tag.ToString();

                if (firstValue.Equals(secondValue))
                {
                    keepCardsOpenAndAddScore(firstButton, secondButton, firstValue, order);
                    winner(win++);
                }
                else
                {
                    closeCard(firstButton);
                    
                    if(order == ME)
                    {
                        order = YOU;
                        lblOrderOfYou.ForeColor = Color.Red;
                        lblOrderOfYou.Text = "X";
                        lblOrderOfMe.Text = "";
                    }
                    else
                    {
                        order = ME;
                        lblOrderOfMe.ForeColor = Color.Red;
                        lblOrderOfMe.Text = "X";
                        lblOrderOfYou.Text = "";
                    }
                }

                firstButton.Enabled = true;
                firstButton = secondButton = null;
                count = 0;
            }
            
            
        }

        private Button openCard(Button button)
        {
            button.BackgroundImage = null;
            return button;
        }

        private void closeCard(Button button)
        {
            button.BackgroundImage = Properties.Resources.poker;
            button.Text = "";
        }

        private void winner(int win)
        {
            if(win == 17)
            {
                if (scoreOfMe > scoreOfYou)
                {
                    MessageBox.Show("Me Win");
                }
                else if (scoreOfMe < scoreOfYou)
                {
                    MessageBox.Show("You Win");
                }
                else
                {
                    MessageBox.Show("Nobody Win");
                }
            }
        }

        private void keepCardsOpenAndAddScore(Button firstButton, Button secondButton, string value, int order)
        {
            firstButton.BackgroundImage = secondButton.BackgroundImage = null;
            firstButton.BackColor = colors[colorIndex];
            secondButton.BackColor = colors[colorIndex];
            firstButton.Text = value;
            secondButton.Text = value;
            firstButton.Enabled = false;
            secondButton.Enabled = false;

            if (order == ME)
            {
                scoreOfMe += 5;
                lblScoreOfMe.Text = scoreOfMe.ToString();
            }
            else
            {
                scoreOfYou += 5;
                lblScoreOfYou.Text = scoreOfYou.ToString();
            }

            colorIndex++;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
