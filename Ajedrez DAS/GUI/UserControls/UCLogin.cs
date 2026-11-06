using System;
using System.Windows.Forms;
using BL_business_logic_layer;
using BE_BusinessEntities;

namespace Ajedrez_DAS.GUI.UserControls
{
    public delegate void Login();
    public partial class UCLogin : UserControl
    {
        public event Login Login;
        public UCLogin()
        {
            InitializeComponent();
        }

        UserService _userBL = new UserService();
        private BE_User logedUser;

        public BE_User LogedUser
        {
            get { return logedUser; }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE_User RegisterUser = new BE_User(username, password);

            try
            {
                _userBL.Register(RegisterUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE_User LoginUser = new BE_User(username, password);

            try
            {
                logedUser = _userBL.Login(LoginUser);
                Login?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ResumeGame(string name)
        {
            this.textBox1.Text = name;
            this.buttonRegister.Enabled = false;
            this.textBox1.Enabled = false;
        }
    }
}
