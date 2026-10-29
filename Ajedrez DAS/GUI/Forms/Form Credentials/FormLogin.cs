using Ajedrez_DAS.GUI.Forms;
using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ajedrez_DAS
{
    public partial class FormLogin : Form
    {
        public List<BE_User> logedUsers = null;
        public FormLogin()
        {
            InitializeComponent();
            ucLogin1.Login += UpdateFormState;
            ucLogin2.Login += UpdateFormState;
        }
         
       private void UpdateFormState()
        {
            if (ucLogin1.LogedUser != null) 
            {
                label1.Text = ucLogin1.LogedUser.Username;
                label1.Visible = true;
                pictureBoxPlayer1.Visible = true;
                ucLogin1.Visible = false;
            }
            if (ucLogin2.LogedUser != null) 
            {
                label2.Text = ucLogin2.LogedUser.Username;
                pictureBoxPlayer2.Visible = true;
                label2.Visible = true;
                ucLogin2.Visible = false;
            }
            if(ucLogin1.LogedUser != null && ucLogin2.LogedUser != null)
            {
                buttonCambiarColores.Visible = true;
                buttonJugar.Visible = true;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBoxPlayer1.BackColor == Color.White) 
            {
                pictureBoxPlayer1.BackColor = Color.Black;
                pictureBoxPlayer2.BackColor = Color.White;
            }
            else
            {
                pictureBoxPlayer2.BackColor = Color.Black;
                pictureBoxPlayer1.BackColor = Color.White;
            }
        }

        private void buttonJugar_Click(object sender, EventArgs e)
        {
            ucLogin1.LogedUser.SetColor(BE_GameColor.White);
            ucLogin2.LogedUser.SetColor(BE_GameColor.Black);

            List<BE_User> users = new List<BE_User> 
            {
                ucLogin1.LogedUser,
                ucLogin2.LogedUser,
            };

            this.logedUsers = users;
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormRegisteredPlayers frm = new FormRegisteredPlayers();
            frm.ShowDialog();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            FormGameHistory frm = new FormGameHistory(true);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormGameHistory frm = new FormGameHistory(false);
            frm.ShowDialog();
        }
    }
}
