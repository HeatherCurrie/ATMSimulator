using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMSimulator
{
    public partial class ATMWindow : Form
    {
        private int accountNum;
        private String state = "account";
        private Account[] account1;
        private Thread ATM2;

        /*
         * ATM Window Constructor
        */

        public ATMWindow(Account[] ac)
        {
            this.account1 = ac;
            InitializeComponent();
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey1_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("1");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey2_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("2");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey3_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("3");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey4_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("4");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey5_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("5");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey6_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("6");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey7_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("7");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey8_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("8");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey9_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("9");
        }

        /*
         * When button is clicked add corresponding number to input box
         */
        private void BtnKey0_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("0");
        }

        /*
         * When enter button is clicked
         */
        private void BtnKeyEnter_Click(object sender, EventArgs e)
        {
            LblInput.Text = "";

            // If state is account and input is correct length
            if (state == "account" && TxtInput.TextLength == 6)
            {
                // Search through accounts to check if it matches with ones initialised in program
                for (int i = 0; i < 3; i++)
                {
                    // If account number is equal to user input
                    if (account1[i].getAccountNum() == Convert.ToInt32(TxtInput.Text))
                    {
                        LblInstruction.Text = "Enter pin number";
                        LblInput.Text = "Account found";
                        accountNum = i;
                        state = "pin";
                    }
                }

                // If account not in program
                if (state == "account")
                {
                    LblInput.Text = "Invalid account number";
                }

            }

            // If account is incorrect length
            else if (state == "account" && TxtInput.TextLength != 6)
            {
                LblInput.Text = "Account number must be 6 digits long";
            }

            // If state is pin and user input length is correct
            else if (state == "pin" && TxtInput.TextLength == 4)
            {
                // Check if pin for that account number is the same as user input
                if (account1[accountNum].checkPin(Convert.ToInt32(TxtInput.Text)) == true)
                {
                    state = "balance";
                    LblInstruction.Text = "What would you like to do:";
                    lblMiddleLeft.Text = "View Balance";
                    lblBottomLeft.Text = "Take out money";
                    lblBottomRight.Text = "Exit";
                    LblInput.Text = "Correct pin entered";
                }

                // If pin is invalid
                if (state == "pin")
                {
                    LblInput.Text = "Invalid pin number";
                }
            }

            // If pen length is incorrect
            else if (state == "pin" && TxtInput.TextLength != 4)
            {
                LblInput.Text = "Pin must be 4 digits long";
            }

            // If state is balance
            else if (state == "balance")
            {
                LblInstruction.Text = "What would you like to do:";
                lblMiddleLeft.Text = "View Balance";
                lblBottomLeft.Text = "Take out money";
                lblBottomRight.Text = "Exit";
            }

            // If state is unique (User chooses to input unique amount)
            if (state == "unique")
            {
                // Take out their input from balance
                takeOutMoney(Convert.ToInt32(TxtInput.Text));
                state = "account";
            }

            TxtInput.Clear();
        }

        /*
         * When cancel button is clicked clear input box
         */
        private void BtnKeyCancel_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();
        }

        /*
         * When about is pressed in menu strip display message box
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("ATM Simulator created by Jonny Cormack, Heather Currie and Matthew Gallacher.", "About", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        /*
         * When bottom right button on ATM is clicked, exit the account
         */
        private void btnBottomRight_Click(object sender, EventArgs e)
        {
            state = "account";
            accountNum = 100;
            LblInput.Text = "Logged out account";
            LblInstruction.Text = "Enter account number:";
            lblBalance.Text = "";
            lblBottomLeft.Text = "";
            lblMiddleLeft.Text = "";
            lblBottomRight.Text = "";
            lblTopLeft.Text = "";
            lblTopRight.Text = "";
            lblMiddleRight.Text = "";
        }

        /*
         * When middle left button is pressed do function corresponding to ATMs state
         */
        private void BtnMiddleLeft_Click(object sender, EventArgs e)
        {
            // If state is takeOut, takeout 20 pounds
            if (state == "takeOut")
            {
                takeOutMoney(20);
            }
            // If state is balance display balance
            if (state == "balance")
            {
                lblBalance.Text = "Your balance is " + Convert.ToString(account1[accountNum].getBalance());
            }
        }

        /*
         * When bottom left button is clicked do function corresponding to ATMs state
         */
        private void btnBottomLeft_Click(object sender, EventArgs e)
        {
            // If state is takeOut take out 50 pounds
            if (state == "takeOut")
            {
                takeOutMoney(50);
            }

            // If state is balance, display different take out options
            else if (state == "balance")
            {
                lblBalance.Text = "Your balance is " + Convert.ToString(account1[accountNum].getBalance());
                lblTopLeft.Text = "£10";
                lblMiddleLeft.Text = "£20";
                lblBottomLeft.Text = "£50";
                lblTopRight.Text = "£100";
                lblMiddleRight.Text = "Unique Amount";
                state = "takeOut";
            }
        }

        /*
         * When top left button is clicked take out 10 pounds
         */
        private void btnTopLeft_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(10);
            }
        }

        /*
         * Take out money based on amount inputted
         * amount is amount provided by user
         */
        private void takeOutMoney(int amount)
        {
            // If funds are sufficient take out the money
            if (account1[accountNum].decrementBalance(amount) == true)
            {
                state = "account";
                lblBalance.Text = "Your balance is " + Convert.ToString(account1[accountNum].getBalance());
                clear();
            // If not enough funds, display
            } else
            {
                LblInput.Text = "Insufficent funds - Enter again";
            } 
        }

        /*
         * When top right button is clicked take out 100 pounds
         */
        private void BtnTopRight_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(100);
            }
        }

        /*
         * When middle right button is clicked take out unique amount
         */
        private void btnMiddleRight_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                LblInstruction.Text = "How much to take out:";
                state = "unique";
            }
        }

        /*
         * Function to clear ATM labels and log out account 
         */
        private void clear()
        {
            LblInput.Text = "Logged out account";
            LblInstruction.Text = "Enter account number:";
            lblBalance.Text = "";
            lblBottomLeft.Text = "";
            lblMiddleLeft.Text = "";
            lblBottomRight.Text = "";
            lblTopLeft.Text = "";
            lblTopRight.Text = "";
            lblMiddleRight.Text = "";
        }

        /*
         * When open a new ATM button is clicked, start thread
         */
        private void button1_Click(object sender, EventArgs e)
        {
            // Creates new thread named ATM2 which calls atmThread2()
            ATM2 = new Thread(new ThreadStart(atmThread2));
            // Start Thread
            ATM2.Start();
        }

        /*
         * Opens up new ATM window with new thread
         */
        private void atmThread2()
        {
            ATMWindow form = new ATMWindow(account1);
            form.ShowDialog();
        }


        /*
         * Run with data race
         */
        private void btnRace_Click(object sender, EventArgs e)
        {

        }

        /*
         * Run without data race
         */
        private void btnNonRace_Click(object sender, EventArgs e)
        {

        }
    }
}
