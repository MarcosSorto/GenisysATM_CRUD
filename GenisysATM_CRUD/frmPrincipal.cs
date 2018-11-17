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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

   


        /// <summary>
        /// instancia de frmClientes y lo muestra en la pantalla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes cliente = new frmClientes();
            cliente.ShowDialog();
        }
        /// <summary>
        /// Cierra el menú principal y cierra la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Está seguro que desa salir del programa?", "Control de cajeros automáticos", MessageBoxButtons.YesNo);

            if (resp == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Crea una instancia del formulario CRUD serviciosPublicos y lo muestra en
        /// pantalla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServicios_Click(object sender, EventArgs e)
        {
            frmServicioPublico nuevo = new frmServicioPublico();
            nuevo.ShowDialog();
        }
    }
}
