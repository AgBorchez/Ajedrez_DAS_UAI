using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        BL_business_logic_layer.UserService _userBL = new BL_business_logic_layer.UserService();
        private BE_BusinessEntities.BE_User logedUser;

        public BE_BusinessEntities.BE_User LogedUser
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

            BE_BusinessEntities.BE_User RegisterUser = new BE_BusinessEntities.BE_User(username, password);

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

            BE_BusinessEntities.BE_User LoginUser = new BE_BusinessEntities.BE_User(username, password);

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
    }
}
