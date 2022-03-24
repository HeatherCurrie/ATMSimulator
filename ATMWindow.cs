using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMSimulator
{
    public partial class ATMWindow : Form
    {
        private int accountNum;
        private String state = "account";
        private Account[] account1;

        public ATMWindow(Account[] ac)
        {
            this.account1 = ac;
            InitializeComponent();
        }

        private void BtnKey1_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("1");
        }

        private void BtnKey2_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("2");
        }

        private void BtnKey3_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("3");
        }

        private void BtnKey4_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("4");
        }

        private void BtnKey5_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("5");
        }

        private void BtnKey6_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("6");
        }

        private void BtnKey7_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("7");
        }

        private void BtnKey8_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("8");
        }

        private void BtnKey9_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("9");
        }

        private void BtnKey0_Click(object sender, EventArgs e)
        {
            TxtInput.AppendText("0");
        }

        private void BtnKeyEnter_Click(object sender, EventArgs e)
        {
            LblInput.Text = "";
            if (state == "account" && TxtInput.TextLength == 6)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (account1[i].getAccountNum() == Convert.ToInt32(TxtInput.Text))
                    {
                        LblInstruction.Text = "Enter pin number";
                        LblInput.Text = "Account found";
                        accountNum = i;
                        state = "pin";
                    }
                }

                if (state == "account")
                {
                    LblInput.Text = "Invalid account number";
                }

            }

            else if (state == "account" && TxtInput.TextLength != 6)
            {
                LblInput.Text = "Account number must be 6 digits long";
            }

            else if (state == "pin" && TxtInput.TextLength == 4)
            {
                if (account1[accountNum].checkPin(Convert.ToInt32(TxtInput.Text)) == true)
                {
                    state = "balance";
                    LblInstruction.Text = "What would you like to do:";
                    lblMiddleLeft.Text = "View Balance";
                    lblBottomLeft.Text = "Take out money";
                    lblBottomRight.Text = "Exit";
                    LblInput.Text = "Correct pin entered";
                }

                if (state == "pin")
                {
                    LblInput.Text = "Invalid pin number";
                }
            }
            else if (state == "pin" && TxtInput.TextLength != 4)
            {
                LblInput.Text = "Pin must be 4 digits long";
            }

            else if (state == "balance" && TxtInput.TextLength == 1)
            {
                LblInstruction.Text = "What would you like to do:";
                lblMiddleLeft.Text = "View Balance";
                lblBottomLeft.Text = "Take out money";
                lblBottomRight.Text = "Exit";
            }

            if (state == "unique")
            {
                takeOutMoney(Convert.ToInt32(TxtInput.Text));
                state = "account";
            }

            TxtInput.Clear();
        }

        private void BtnKeyCancel_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("ATM Simulator created by Jonny Cormack, Heather Currie and Matthew Gallacher.", "About", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

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

        private void BtnMiddleLeft_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(20);
            }
            if (state == "balance")
            {
                lblBalance.Text = "Your balance is " + Convert.ToString(account1[accountNum].getBalance());
            }
        }

        private void btnBottomLeft_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(50);
            }

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

        private void btnTopLeft_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(10);
            }
        }

        private void takeOutMoney(int amount)
        {
            if (account1[accountNum].decrementBalance(amount) == true)
            {
                state = "account";
                lblBalance.Text = "Your balance is " + Convert.ToString(account1[accountNum].getBalance());
                clear();
            } else
            {
                LblInput.Text = "Insufficent funds - Enter again";
            } 
        }

        private void BtnTopRight_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                takeOutMoney(100);
            }
        }

        private void btnMiddleRight_Click(object sender, EventArgs e)
        {
            if (state == "takeOut")
            {
                LblInstruction.Text = "How much to take out:";
                state = "unique";
            }
        }

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
    }
}
