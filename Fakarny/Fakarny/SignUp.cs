using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Fakarny
{
    public partial class SignUp : Form
    {
        string Program_path, File_ext;

        public SignUp(string Program_path, string File_ext)
        {
            this.Program_path = Program_path;
            this.File_ext = File_ext;
            InitializeComponent();
        }


        public static string ComputeHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                string builder = string.Empty;
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder += bytes[i].ToString("x2");
                }
                return builder;
            }
        }

        private bool CheckViabilty(string s)
        {

            for (int i = 0; i < s.Length; i++)
            {
                if (!((s[i] < 58 && s[i] > 47) || (s[i] < 91 && s[i] > 64) || (s[i] < 123 && s[i] > 96) || s[i] == 95))
                {
                    return false;
                }
            }
            return true;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            Invalid_Fullname.Hide();
            Invalid_Username_Label.Hide();
            Invalid_Password.Hide();
            Password_Matching.Hide();
            Star1.Hide();
            Star2.Hide();
            Star3.Hide();
            Star4.Hide();
        }

        //FullName Space
        #region FullName
        private void Fullname_Enter(object sender, EventArgs e)
        {
            if (SignUp_Fullname_Textbox.Text == "" || SignUp_Fullname_Textbox.Text == "Full name")
            {
                SignUp_Fullname_Textbox.Text = "";
            }
        }

        private void Fullname_Changed(object sender, EventArgs e)
        {
            SignUp_Fullname_Textbox.ForeColor = Color.White;
        }

        private void Fullname_Leave(object sender, EventArgs e)
        {
            if (SignUp_Fullname_Textbox.Text == "" || SignUp_Fullname_Textbox.Text == "Full name")
            {

                SignUp_Fullname_Textbox.Text = "Full name";
                SignUp_Fullname_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }
        #endregion

        //Username Space
        #region UserName
        private void Username_Enter(object sender, EventArgs e)
        {
            if (SignUp_Username_Textbox.Text == "" || SignUp_Username_Textbox.Text == "Username")
            {
                SignUp_Username_Textbox.Text = "";
            }
        }

        private void Username_Changed(object sender, EventArgs e)
        {
            SignUp_Username_Textbox.ForeColor = Color.White;
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (SignUp_Username_Textbox.Text == "" || SignUp_Username_Textbox.Text == "Username")
            {

                SignUp_Username_Textbox.Text = "Username";
                SignUp_Username_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }
        #endregion

        //Password Space
        #region Password 
        private void Password_Enter(object sender, EventArgs e)
        {
            if (SignUp_Password_Textbox.Text == "" || SignUp_Password_Textbox.Text == "Password")
            {
                SignUp_Password_Textbox.Text = "";
            }
        }
        private void Password_Changed(object sender, EventArgs e)
        {
            SignUp_Password_Textbox.ForeColor = Color.White;
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (SignUp_Password_Textbox.Text == "" || SignUp_Password_Textbox.Text == "Password")
            {

                SignUp_Password_Textbox.Text = "Password";
                SignUp_Password_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }
        #endregion

        //Confirm Space
        #region Confirm
        private void Confirm_Changed(object sender, EventArgs e)
        {
            SignUp_Confirm_Password_Textbox.ForeColor = Color.White;
        }

        private void Confirm_Leave(object sender, EventArgs e)
        {
            if (SignUp_Confirm_Password_Textbox.Text == "" || SignUp_Confirm_Password_Textbox.Text == "Confirm Password")
            {

                SignUp_Confirm_Password_Textbox.Text = "Confirm Password";
                SignUp_Confirm_Password_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }

        private void Confirm_Enter(object sender, EventArgs e)
        {
            if (SignUp_Confirm_Password_Textbox.Text == "" || SignUp_Confirm_Password_Textbox.Text == "Confirm Password")
            {
                SignUp_Confirm_Password_Textbox.Text = "";
            }
        }
        #endregion

        //SignUp
        #region SignUp
        private void SignUp_Button_Click(object sender, EventArgs e)
        {
            bool fn = false;
            #region Fullname Check
            if (SignUp_Fullname_Textbox.Text == "" || SignUp_Fullname_Textbox.Text == "Full name")
            {
                Invalid_Fullname.Show();
                Star1.Show();
                SignUp_Fullname_Textbox.Text = "Full name";
            }
            else
            {
                Invalid_Fullname.Hide();
                Star1.Hide();
                fn = true;
            }
            #endregion
            string temp_username = ComputeHash(SignUp_Username_Textbox.Text);
            File_ext = Program_path + "\\data\\" + temp_username;
            #region Username Check
            bool un = false;
            if (SignUp_Username_Textbox.Text == "" || SignUp_Username_Textbox.Text == "Username")
            {
                Star2.Show();
                Invalid_Username_Label.Show();
                SignUp_Username_Textbox.Text = "Username";
                Invalid_Username_Label.Text = "Username Can't be empty";
            }
            else if (!CheckViabilty(SignUp_Username_Textbox.Text))
            {
                Star2.Show();
                Invalid_Username_Label.Show();
                SignUp_Username_Textbox.Text = "Username";
                Invalid_Username_Label.Text = "Username should only include numbers, letters and _";
            }
            else if (File.Exists(File_ext + ".txt"))
            {
                Star2.Show();
                Invalid_Username_Label.Show();
                SignUp_Username_Textbox.Text = "Username";
                Invalid_Username_Label.Text = "This account already exits";
            }
            else
            {
                un = true;
                Star2.Hide();
                Invalid_Username_Label.Hide();
            }
            #endregion
            #region Password Check
            bool pc = false;
            if (SignUp_Password_Textbox.Text == "")
            {
                Invalid_Password.Show();
                Star3.Show();
                SignUp_Password_Textbox.Text = "Password";
                Invalid_Password.Text = "Your password Can't be Empty";
            }
            else if (SignUp_Password_Textbox.Text == "Password")
            {
                Invalid_Password.Show();
                Star3.Show();
                SignUp_Password_Textbox.Text = "Password";
                Invalid_Password.Text = "Your password Can't be \" Password \"";
            }
            else if (SignUp_Password_Textbox.Text.Length < 6 || SignUp_Password_Textbox.Text.Length > 32)
            {
                Invalid_Password.Show();
                Star3.Show();
                SignUp_Password_Textbox.Text = "Password";
                Invalid_Password.Text = "Your password Can't be less than 6 chars or more than 32";
            }

            else
            {
                Invalid_Password.Hide();
                Star3.Hide();
                pc = true;
            }
            #endregion
            #region Confirmation
            bool con = false;
            if (!(SignUp_Confirm_Password_Textbox.Text == SignUp_Password_Textbox.Text))
            {
                Star4.Show();
                Password_Matching.Text = "Password must match";
                SignUp_Confirm_Password_Textbox.Text = "Confirm Password";
                Password_Matching.Show();
            }
            else
            {
                Star4.Hide();
                Password_Matching.Hide();
                con = true;
            }
            #endregion
            if (fn && un && pc && con)
            {
                if (!Directory.Exists(Program_path + "\\data"))
                {
                    Directory.CreateDirectory(Program_path + "\\data");
                }
                Directory.CreateDirectory(File_ext);
                using (StreamWriter sw = File.CreateText(File_ext + ".txt"))
                {
                    sw.Write(ComputeHash(SignUp_Password_Textbox.Text));
                }
                Directory.CreateDirectory(File_ext + "\\Info");
                using (StreamWriter sw = File.CreateText(File_ext + @"\Info\Info.txt"))
                {
                    sw.WriteLine(SignUp_Fullname_Textbox.Text);
                }
                MessageBox.Show("Welcome " + SignUp_Fullname_Textbox.Text, "Submmited");
                SignUp_Fullname_Textbox.Text = "Full name";
                SignUp_Username_Textbox.Text = "Username";
                SignUp_Password_Textbox.Text = "Password";
                SignUp_Confirm_Password_Textbox.Text = "Confirm Password";
                this.Close();
            }

        }

        private void Mouse_Hover(object sender, EventArgs e)
        {
            SignUp_Button.ForeColor = Color.FromArgb(36, 36, 36);
        }

        private void Mouse_LEAVE(object sender, EventArgs e)
        {
            SignUp_Button.ForeColor = Color.FromArgb(149, 149, 149);
        }

        #endregion

        //Have An Account
        #region LogIn
        private void Have_An_Account_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
