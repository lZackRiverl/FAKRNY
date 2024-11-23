using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Fakarny.UserControls
{
    public partial class Edit_Account : UserControl
    {
        string Program_path, Key;
        public FakarnyPage main_form;
        Data data;

        public Edit_Account()
        {
            InitializeComponent();
        }

        public string Program_Path
        {
            get
            {
                return this.Program_path;
            }
            set
            {
                this.Program_path = value;
            }
        }
        public string key
        {
            get
            {
                return this.Key;
            }
            set
            {
                this.Key = value;
            }
        }
        private void Show_More_Button_Click(object sender, EventArgs e)
        {
            Move_Panel.Location = new Point(81, 560);
            panel17.Location = new Point(37, 413);
            panel17.Visible = true;
            show_less_bt.Visible = true;
            Show_Button.Visible = false;

        }

        private void Show_Less_Button_Click(object sender, EventArgs e)
        {
            Move_Panel.Location = new Point(81, 409);
            panel17.Location = new Point(37, 483);
            panel17.Visible = false;
            show_less_bt.Visible = false;
            Show_Button.Visible = true;
        }


        #region New_Name_Textbox Handling
        private void New_Name_Textbox_Enter(object sender, EventArgs e)
        {
            if (New_Name_Textbox.Text == "" || New_Name_Textbox.Text == "website or app name")
            {
                New_Name_Textbox.Text = "";
            }
        }
        private void New_Name_Textbox_Leave(object sender, EventArgs e)
        {
            if (New_Name_Textbox.Text == "" || New_Name_Textbox.Text == "website or app name")
            {
                New_Name_Textbox.Text = "website or app name";
                New_Name_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;
            }
        }
        private void New_Name_Textbox_Changed(object sender, EventArgs e)
        {
            New_Name_Textbox.ForeColor = Color.White;
        }
        #endregion

        #region New_User_Id_Textbox Handling
        private void New_User_Id_Textbox_Enter(object sender, EventArgs e)
        {
            if (New_User_Id_Textbox.Text == "" || New_User_Id_Textbox.Text == "username or email id")
            {
                New_User_Id_Textbox.Text = "";
            }
        }
        private void New_User_Id_Textbox_Changed(object sender, EventArgs e)
        {
            New_User_Id_Textbox.ForeColor = Color.White;
        }
        private void New_User_Id_Textbox_Leave(object sender, EventArgs e)
        {
            if (New_User_Id_Textbox.Text == "" || New_User_Id_Textbox.Text == "username or email id")
            {
                New_User_Id_Textbox.Text = "username or email id";
                New_User_Id_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;
            }
        }
        #endregion

        #region New_Password_Textbox Handling
        private void New_Password_Textbox_Enter(object sender, EventArgs e)
        {
            if (New_Password_Textbox.Text == "")
            {
                New_Password_Textbox.Text = "";
            }
        }
        private void New_Password_Textbox_Leave(object sender, EventArgs e)
        {
            if (New_Password_Textbox.Text == "")
            {

                New_Password_Textbox.Text = "";
                New_Password_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }
        private void New_Password_Textbox_Changed(object sender, EventArgs e)
        {
            New_Password_Textbox.ForeColor = Color.White;
        }
        #endregion

        #region New_Recovery_Email_Textbox Handling
        private void New_Recovery_Email_Textbox_Enter(object sender, EventArgs e)
        {
            if (New_Recovery_Email_Textbox.Text == "" || New_Recovery_Email_Textbox.Text == "Recovery Email")
            {
                New_Recovery_Email_Textbox.Text = "";
            }
        }
        private void New_Recovery_Email_Textbox_Leave(object sender, EventArgs e)
        {
            if (New_Recovery_Email_Textbox.Text == "" || New_Recovery_Email_Textbox.Text == "Recovery Email")
            {
                New_Recovery_Email_Textbox.Text = "Recovery Email";
                New_Recovery_Email_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;
            }
        }
        private void New_Recovery_Email_Textbox_Changed(object sender, EventArgs e)
        {
            New_Recovery_Email_Textbox.ForeColor = Color.White;
        }
        #endregion

        #region New_Phone_Textbox Handling
        private void New_Phone_Textbox_Changed(object sender, EventArgs e)
        {
            New_Phone_Textbox.ForeColor = Color.White;
        }
        private void New_Phone_Textbox_Enter(object sender, EventArgs e)
        {
            if (New_Phone_Textbox.Text == "" || New_Phone_Textbox.Text == "Phone Number")
            {
                New_Phone_Textbox.Text = "";
            }
        }
        private void New_Phone_Textbox_Leave(object sender, EventArgs e)
        {
            if (New_Phone_Textbox.Text == "" || New_Phone_Textbox.Text == "Phone Number")
            {
                New_Phone_Textbox.Text = "Phone Number";
                New_Phone_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;
            }
        }
        #endregion
        private void Save_Password_Button_Click(object sender, EventArgs e)
        {
            main_form.Validate_Password();

            Account_Saved.Hide();
            bool save = Check_Input();

            if (save)
            {
                Encryptor enc = new Encryptor(Key);
                string en = enc.Path_Safe_Encrypt(New_Name_Textbox.Text);
                enc.IVGenerate();
                using (StreamWriter sr = File.CreateText(Program_path + "\\" + en + ".txt"))
                {
                    sr.WriteLine(enc.IV);
                    sr.WriteLine(enc.Encrypt(New_User_Id_Textbox.Text));
                    sr.WriteLine(enc.Encrypt(New_Password_Textbox.Text));
                    sr.WriteLine(enc.Encrypt(New_Phone_Textbox.Text));
                    sr.WriteLine(enc.Encrypt(New_Recovery_Email_Textbox.Text));
                }
                New_Name_Textbox.Text = "website or app name";
                New_Name_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                New_User_Id_Textbox.Text = "username or email id";
                New_User_Id_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                New_Password_Textbox.Text = "";
                New_Password_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                New_Phone_Textbox.Text = "Phone Number";
                New_Phone_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                New_Recovery_Email_Textbox.Text = "Recovery Email";
                New_Recovery_Email_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                Account_Saved.Show();

                main_form.Load_Combobox();
            }
            else
            {
                //Make a Failed to save Panel and show it
            }
        }

        private bool Check_Input()
        {
            bool checker = true;

            //Check New Name TextBox
            if (New_Name_Textbox.Text == "" || New_Name_Textbox.Text == "website or app name")
            {
                invalid_name.Show();
                checker = false;
            }
            else
                invalid_name.Hide();

            //Check New User Id TextBox
            if (New_User_Id_Textbox.Text == "" || New_User_Id_Textbox.Text == "username or email id")
            {
                invalid_id.Show();
                checker = false;
            }
            else
                invalid_id.Hide();

            //Check New Password TextBox
            if (New_Password_Textbox.Text == "")
            {
                invalid_pass.Show();
                checker = false;
            }
            else
                invalid_pass.Hide();

            //Check New Phone TextBox
            if ((New_Phone_Textbox.Text.Length > 15 || New_Phone_Textbox.Text.Length < 10 && New_Phone_Textbox.Text.Length != 0) && New_Phone_Textbox.Visible == true)
            {
                invalid_phone.Show();
                checker = false;
            }
            else
            {
                invalid_phone.Hide();
                New_Phone_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
            }

            return checker;
        }

        private void Edit_Account_Load(object sender, EventArgs e)
        {

        }

        public Data Data_Set
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

    }

}
