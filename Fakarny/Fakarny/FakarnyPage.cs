using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace Fakarny
{
    public partial class FakarnyPage : Form
    {
        string Program_path, Key;
        string[] Names_before_Clean, Names;
        Validation validation = new Validation();
        Data data;
        public FakarnyPage(string Program_path, string Key)
        {
            this.Program_path = Program_path;
            this.Key = Key;
            InitializeComponent();
            edit_Account1.Program_Path = Program_path;
            edit_Account1.key = Key;
            edit_Account1.main_form = this;
            add_Account1.Program_Path = Program_path;
            add_Account1.key = Key;
            add_Account1.main_form = this;
            validation.Key = Key;
            validation.main_form = this;
        }
        private void FakarnyPage_Load(object sender, EventArgs e)
        {
            add_Account1.Hide();
            edit_Account1.Show();
            edit_Account1.BringToFront();
            Load_Combobox();
        }

        private void View_Contents_Button_Click(object sender, EventArgs e)
        {
            if (Show_Name_Combobox.SelectedIndex >= 0 && Validate_Password())//&&Validate_Password()
            {
                add_Account1.Data_Set = data;
                add_Account1.Enter_data();
                edit_Account1.Hide();
                add_Account1.Show();
                add_Account1.BringToFront();
            }
        }

        private void Add_Acc_Button_Click_1(object sender, EventArgs e)
        {
            add_Account1.Hide();
            edit_Account1.Show();
            edit_Account1.BringToFront();
        }

        private void Show_Name_Combobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string temp;
            Encryptor enc = new Encryptor(Key);
            if (Show_Name_Combobox.SelectedIndex >= Names_before_Clean.Length || Show_Name_Combobox.SelectedIndex < 0)
            {
                //exception
            }
            temp = Names_before_Clean[Show_Name_Combobox.SelectedIndex];
            using (StreamReader sr = new StreamReader(temp))
            {
                data.IV = enc.IV = sr.ReadLine();
                Name_Label.Text = data.Site_Name = Show_Name_Combobox.SelectedItem.ToString();
                User_Id_Label.Text = data.User_Name = enc.Decrypt(sr.ReadLine());
                data.Password = enc.Decrypt(sr.ReadLine());
                data.Phone = enc.Decrypt(sr.ReadLine());
                data.Recovery_Email = enc.Decrypt(sr.ReadLine());
            }
            add_Account1.Data_Set = data;
            edit_Account1.Data_Set = data;
        }

        private void Copy_Username_Button_Click(object sender, EventArgs e)
        {
            if (data.User_Name != null && Validate_Password())
                Clipboard.SetDataObject(data.User_Name);
        }

        private void Copy_Password_Button_Click(object sender, EventArgs e)
        {
            if (data.Password != null && Validate_Password())//&& Validate_Password()
                Clipboard.SetDataObject(data.Password);
        }


        //Currently Deactivated Button
        private void button1_Click(object sender, EventArgs e)
        {
            Encryptor enc = new Encryptor(Key);

            if (File.Exists(Program_path + "\\" + enc.Path_Safe_Encrypt(Search_Textbox.Text) + ".txt"))
            {
                data.Site_Name = Search_Textbox.Text;
                Name_Label.Text = Search_Textbox.Text;
                StreamReader sr = new StreamReader(Program_path + "\\" + enc.Path_Safe_Encrypt(Search_Textbox.Text) + ".txt");
                data.IV = sr.ReadLine();
                enc.IV = data.IV;
                data.User_Name = enc.Decrypt(sr.ReadLine());
                data.Password = enc.Decrypt(sr.ReadLine());
                User_Id_Label.Text = data.User_Name;
                data.Phone = enc.Decrypt(sr.ReadLine());
                data.Recovery_Email = enc.Decrypt(sr.ReadLine());
                sr.Close();
                edit_Account1.Data_Set = data;
                add_Account1.Data_Set = data;
                Show_Name_Combobox.Text = "";
                Show_Name_Combobox.SelectedText = Search_Textbox.Text;

                int i = 0;
                while (i < Show_Name_Combobox.Items.Count)
                {
                    if (Show_Name_Combobox.Items[i].ToString().Equals(Search_Textbox.Text))
                    {
                        Show_Name_Combobox.SelectedIndex = i;
                    }
                    i++;
                }



            }
            else
            {
                MessageBox.Show("This account doesn't exsist", "Error");
            }
        }
        //

        public void Load_Combobox()
        {
            string temp;
            Names_before_Clean = Directory.GetFiles(Program_path, "*.txt");
            Names = new string[Names_before_Clean.Length];
            Encryptor enc = new Encryptor(Key);
            for (int i = 0; i < Names_before_Clean.Length; i++)
            {
                temp = Names_before_Clean[i].Substring(Program_path.Length + 1);
                temp = temp.Remove(temp.Length - 4);
                Names[i] = enc.Path_Safe_Decrypt(temp);
            }
            Show_Name_Combobox.Items.Clear();
            Show_Name_Combobox.Items.AddRange(Names);
        }

        private void Delete_Account_Button_Click(object sender, EventArgs e)
        {
            if (Show_Name_Combobox.Text == "" || Show_Name_Combobox.Text == "Choose Your Account From Here")
            {
                MessageBox.Show("No Selected Account", "");
            }
            else
            {
                DialogResult

                resalt = MessageBox.Show("Do you want to delete this account ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (resalt == DialogResult.OK)
                {
                    Encryptor enc = new Encryptor(Key);



                    File.Delete(Program_path + "\\" + enc.Path_Safe_Encrypt(Show_Name_Combobox.Text) + ".txt");
                    Show_Name_Combobox.Items.RemoveAt(Show_Name_Combobox.SelectedIndex);

                    Show_Name_Combobox.Text = "Choose Your Account From Here";
                    Search_Textbox.Text = "Search Account";
                    Name_Label.Text = "App Name";
                    User_Id_Label.Text = "Username";

                    Clipboard.Clear();
                    add_Account1.Hide();
                    edit_Account1.Show();
                    edit_Account1.BringToFront();

                    MessageBox.Show("Account deleted successfully");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Encryptor enc = new Encryptor(Key);

            if (File.Exists(Program_path + "\\" + enc.Path_Safe_Encrypt(Search_Textbox.Text) + ".txt"))
            {
                data.Site_Name = Search_Textbox.Text;
                Name_Label.Text = Search_Textbox.Text;
                StreamReader sr = new StreamReader(Program_path + "\\" + enc.Path_Safe_Encrypt(Search_Textbox.Text) + ".txt");
                data.IV = sr.ReadLine();
                enc.IV = data.IV;
                data.User_Name = enc.Decrypt(sr.ReadLine());
                data.Password = enc.Decrypt(sr.ReadLine());
                User_Id_Label.Text = data.User_Name;
                data.Phone = enc.Decrypt(sr.ReadLine());
                data.Recovery_Email = enc.Decrypt(sr.ReadLine());
                sr.Close();
                edit_Account1.Data_Set = data;
                add_Account1.Data_Set = data;
                Show_Name_Combobox.Text = "";
                Show_Name_Combobox.SelectedText = Search_Textbox.Text;

                int i = 0;
                while (i < Show_Name_Combobox.Items.Count)
                {
                    if (Show_Name_Combobox.Items[i].ToString().Equals(Search_Textbox.Text))
                    {
                        Show_Name_Combobox.SelectedIndex = i;
                    }
                    i++;
                }
            }
        }
        private void Search_Textbox_Enter(object sender, EventArgs e)
        {
            if (Search_Textbox.Text == "" || Search_Textbox.Text == "Search Account")
            {
                Search_Textbox.Text = "";
            }
        }
        private void Search_Textbox_Leave(object sender, EventArgs e)
        {
            if (Search_Textbox.Text == "" || Search_Textbox.Text == "Search Account")
            {

                Search_Textbox.Text = "Search Account";
                Search_Textbox.ForeColor = Color.FromArgb(149, 149, 149);
                return;

            }
        }

        private void Search_Textbox_TextChanged(object sender, EventArgs e)
        {
            Search_Textbox.ForeColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi there! Let's explore FAKARNY - فكرني, We'll keep it fun and simple, promise! :D" +
                "\r\n\r\n1. What is FAKARNY - فكرني?" +
                "\r\nFAKARNY - فكرني is a helpful and secure app that stores all your account info, like usernames and passwords, safely encrypted and organized." +
                "\r\n\r\n2. Getting Started" +
                "\r\nIf you're new, create an account with a unique username and strong password. Already have an account? Just log in with your username and password!" +
                "\r\n\r\n3. Main Interface\r\nYou'll see two main sections:" +
                "\r\nLeft Panel: Search for your accounts, view details, and use buttons to delete, or copy usernames and passwords." +
                "\r\nRight Panel: Add or edit accounts by filling in the info and saving your changes." +
                "\r\n\r\n4. Keeping Your Accounts Safe" +
                "\r\nWhen you copy a password, you'll need to enter your FAKARNY - فكرني password for extra security. Don't worry; you have three tries to get it right. If you enter the wrong password three times, the app will shut down to protect your data." +
                "\r\n\r\n5. Need Help?" +
                "\r\nWe're here for you! Reach out to our support team if you have questions or need assistance." +
                "\r\nThat's it! Now you're ready to use FAKARNY - فكرني like a pro. Happy organizing! 🌟", "FAKARNY - فكرني Manual");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zack River:                +201205330792\n" +
                            "Tarek Ashraf:            +201016971706\n" +
                            "Anas Mahmoud:      +201147647816\n" +
                            "Minna Khaled:          +201033988119\n" +
                            "Abdelrahman Ehab: +201116023154\n",
                            "Contact US");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Zack-River/FAKRNY");
        }

        public bool Validate_Password()
        {
            validation.ShowDialog();

            if (validation.valid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void add_Account1_Load(object sender, EventArgs e)
        {

        }
    }
    #region Encryptor
    public class Encryptor
    {
        private byte[] iv = { 196, 105, 10, 83, 65, 63, 165, 46, 109, 166, 141, 17, 94, 109, 146, 235 };
        private byte[] KeyByte;
        private byte[] Encrypted;
        private Aes aes = Aes.Create();

        public Encryptor(string Key)
        {
            for (int i = Key.Length; i < 32; i++)
            {
                Key += '0';
            }
            this.KeyByte = Encoding.UTF8.GetBytes(Key);
            aes.Mode = CipherMode.CBC;
        }

        public Encryptor(string Key, string IV)
        {
            this.iv = Convert.FromBase64String(IV);
            for (int i = Key.Length; i < 32; i++)
            {
                Key += '0';
            }
            this.KeyByte = Encoding.UTF8.GetBytes(Key);
            aes.Mode = CipherMode.CBC;
        }

        public string Encrypt(string plaintext)
        {
            using (ICryptoTransform encryptor = aes.CreateEncryptor(KeyByte, iv))
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                Encrypted = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                return Convert.ToBase64String(Encrypted);
            }
        }

        public string Decrypt(string Encrypted)
        {
            byte[] encbytes = Convert.FromBase64String(Encrypted);
            using (ICryptoTransform decryptor = aes.CreateDecryptor(KeyByte, iv))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encbytes, 0, encbytes.Length);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedText;
            }
        }

        public string Key
        {
            get
            {
                return Convert.ToBase64String(KeyByte);
            }

            set
            {
                for (int i = value.Length; i < 32; i++)
                {
                    value += '0';
                }
                KeyByte = Encoding.UTF8.GetBytes(value);
            }
        }

        public void IVGenerate()
        {
            aes.GenerateIV();
            IV = Convert.ToBase64String(aes.IV);
        }
        public string IV
        {
            get
            {
                return Convert.ToBase64String(iv);
            }

            set
            {
                Array.Copy(Convert.FromBase64String(value), iv, 16);
            }
        }


        private void Make_Safe(ref string s)
        {
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '/')
                    sb[i] = '_';
            }
            s = sb.ToString();
        }

        private string Make_UnSafe(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '_')
                    sb[i] = '/';
            }
            s = sb.ToString();
            return s;
        }

        public string Path_Safe_Encrypt(string plaintext)
        {
            using (ICryptoTransform encryptor = aes.CreateEncryptor(KeyByte, iv))
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                Encrypted = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                string s = Convert.ToBase64String(Encrypted);
                Make_Safe(ref s);
                return s;
            }
        }
        public string Path_Safe_Decrypt(string Encrypted)
        {
            Encrypted = Make_UnSafe(Encrypted);
            byte[] encbytes = Convert.FromBase64String(Encrypted);
            using (ICryptoTransform decryptor = aes.CreateDecryptor(KeyByte, iv))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encbytes, 0, encbytes.Length);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedText;
            }
        }
    }
    #endregion
    public struct Data
    {
        private string Iv, Site_name, User_name, password, phone, Recovery_email;

        public string IV
        {
            get { return Iv; }
            set { Iv = value; }
        }
        public string Site_Name
        {
            get { return Site_name; }
            set { Site_name = value; }
        }
        public string User_Name
        {
            get { return User_name; }
            set { User_name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Recovery_Email
        {
            get { return Recovery_email; }
            set { Recovery_email = value; }
        }
    }
}
