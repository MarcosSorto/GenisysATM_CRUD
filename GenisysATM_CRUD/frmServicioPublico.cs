using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenisysATM_CRUD
{
    public partial class frmServicioPublico : Form
    {
        public frmServicioPublico()
        {
            InitializeComponent();
        }

        /// <summary>
        /// cierra el módulo de servicios y 
        /// retorna al menú principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
