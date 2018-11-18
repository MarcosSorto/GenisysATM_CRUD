using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenisysATM.Models;

namespace GenisysATM_CRUD
{
    public partial class frmCuentaCliente : Form
    {
        public frmCuentaCliente()
        {
            InitializeComponent();
        }

        private void lstClientes_Click(object sender, EventArgs e)
        {

        }

        private void frmCuentaCliente_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lstClientes.SelectedIndex == -1 || txtPin.Text == "" || txtSaldo.Text == "")
            {
                MessageBox.Show("Debe de seleccionar un cliente y llenar todos los datos", "Error en ingreso", MessageBoxButtons.OK);

            }
            else
            {
                // Instanciamos de la clase cuentaCliente y gurdamos los datos.
                CuentaCliente nuevo = new CuentaCliente();
                nuevo.numero = txtNumero.Text;
                nuevo.pin = txtPin.Text;
                nuevo.saldo = Convert.ToDecimal(txtSaldo.Text);

                if(CuentaCliente.InsertarCuenta(nuevo, lstClientes.SelectedItem.ToString()))
                {
                    MessageBox.Show("La cuenta se Guardó correctamente", "Control de cuentas", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la inserción", "Control de cuentas", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// Limpia todos los datos y recarga las listas
        /// </summary>
        public void Limpiar()
        {
            txtPin.Text = "";
            txtSaldo.Text = "";
            txtSaldo.Focus();
            lstClientes.SelectedIndex = -1;
            lstClientes.Items.Clear();
            //Volvemos a llenar las listas
            CargarDatos();
        }

        public void CargarDatos()
        {
            // obtenemos la lista de todos los clientes de la tabla
            List<Cliente> listaCliente = Cliente.LeerTodos();

            // Limpiar el listView
            lstClientes.Items.Clear();

            // Verificar si existen departamentos
            if (listaCliente.Any())
            {
                listaCliente.ForEach(Cliente => lstClientes.Items.Add(Cliente.nombres));
            }
            else
                lstClientes.Items.Add("¡No hay clientes!");
        }

        private void grpOperaciones_Enter(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
