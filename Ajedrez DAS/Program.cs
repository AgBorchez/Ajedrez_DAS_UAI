using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BusinessEntities;
using Presentacion;

namespace Ajedrez_DAS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FormLogin frm = new FormLogin())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    {
                        Application.Run(new FormTablero(frm.logedUsers));
                    }
                }
            }
        }
    }
}
