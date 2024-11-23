using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fakarny
{
    public partial class Validation : Form
    {
        public string Password;
        public FakarnyPage main_form;
        public string Key;
        int tries = 0;
        public bool valid = false;
        public Validation()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            Screen screen = Screen.PrimaryScreen;
            this.Location = new Point(screen.WorkingArea.Width / 2 - 156, screen.WorkingArea.Height / 2 - 172 / 2);
        }

        private void Add_Acc_Button_Click(object sender, EventArgs e)
        {
            Password = textBox1.Text;
            if (Key == Password)
            {
                valid = true;
                this.Close();
            }
            else
            {
                tries++;
                textBox1.Clear();
                if (tries < 3)
                {
                    MessageBox.Show($"You have {3 - tries} tries left before the Fakarny closes", "Wrong Password!");
                    return;
                }
                else
                {
                    Application.Exit();
                }
            }
            textBox1.Clear();
            this.Close();
        }
    }
}
