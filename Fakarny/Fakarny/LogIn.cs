using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Fakarny
{
    public partial class Login_Form : Form
    {
        string Program_path, PassWord;
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_UserName_TextBox_Click(object sender, EventArgs e)
        {
            Login_UserName_TextBox.Select();
        }

        private void Login_Password_TextBox_Click(object sender, EventArgs e)
        {
            Login_Password_TextBox.Select();
        }

        private void Login_Button_MouseEnter(object sender, EventArgs e)
        {
            Login_Button.ForeColor = Color.FromArgb(38, 38, 38);
        }

        private void Login_Button_MouseLeave(object sender, EventArgs e)
        {
            Login_Button.ForeColor = Color.FromArgb(149, 149, 149);
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            PassWord = Login_Password_TextBox.Text;
            string Input_Username = SignUp.ComputeHash(Login_UserName_TextBox.Text);
            string Input_Password = SignUp.ComputeHash(PassWord);
            string filePath = Program_path + "\\data\\" + Input_Username + ".txt";

            if (File.Exists(Program_path + "\\data\\" + Input_Username + ".txt") && File.ReadAllText(filePath) == Input_Password)
            {
                Invalid_Username_Label.Hide();
                Login_Username_Error_Panel.Visible = false;
                Wrong_Password_Label.Hide();
                Login_Password_Error_Panel.Visible = false;
                Login_UserName_TextBox.Text = "Username";
                Login_Password_TextBox.Text = "Password";
                Login_UserName_TextBox.ForeColor = Color.FromArgb(149, 149, 149);
                Login_Password_TextBox.ForeColor = Color.FromArgb(149, 149, 149);
                FakarnyPage fk = new FakarnyPage(Program_path + "\\data\\" + Input_Username, PassWord);
                this.Hide();
                fk.ShowDialog();
                this.Close();
            }
            else
            {
                Invalid_Username_Label.Show();
                Login_Username_Error_Panel.Visible = true;
                Login_UserName_TextBox.Focus();
                Login_UserName_TextBox.Text = "";

                Wrong_Password_Label.Show();
                Login_Password_Error_Panel.Visible = true;
                Login_Password_TextBox.Focus();
                Login_Password_TextBox.Text = "";
            }
        }
        private void Leave_User(object sender, EventArgs e)
        {
            if (Login_UserName_TextBox.Text == "" || Login_UserName_TextBox.Text == "Username")
            {

                Login_UserName_TextBox.Text = "Username";
                Login_UserName_TextBox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
            Login_Username_Error_Panel.Visible = false;
        }

        private void Enter_User(object sender, EventArgs e)
        {
            if (Login_UserName_TextBox.Text == "" || Login_UserName_TextBox.Text == "Username")
            {
                Login_UserName_TextBox.Text = "";
            }
        }
        private void User_Changed(object sender, EventArgs e)
        {
            Login_UserName_TextBox.ForeColor = Color.White;
        }

        private void Pass_Changed(object sender, EventArgs e)
        {
            if (!(Login_Password_TextBox.Text == "") || !(Login_Password_TextBox.Text == "Password"))
                Login_Password_TextBox.ForeColor = Color.White;
        }

        private void Pass_Leave(object sender, EventArgs e)
        {
            if (Login_Password_TextBox.Text == "" || Login_Password_TextBox.Text == "Password")
            {
                this.Login_Password_TextBox.PasswordChar = '\0';
                Login_Password_TextBox.Text = "Password";
                Login_Password_TextBox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
            Login_Password_Error_Panel.Visible = false;
        }

        private void Pass_Enter(object sender, EventArgs e)
        {
            if (Login_Password_TextBox.Text == "" || Login_Password_TextBox.Text == "Password")
            {
                this.Login_Password_TextBox.PasswordChar = '*';
                Login_Password_TextBox.Text = "";
            }
        }

        private void Create_New_Acount_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signUp = new SignUp(Program_path, "1");
            this.Hide();
            signUp.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            Program_path = Directory.GetCurrentDirectory();
        }

    }
}
