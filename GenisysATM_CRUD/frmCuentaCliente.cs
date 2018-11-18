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
            // obtenemos la lista de todos los clientes de la tabla
            List<CuentaCliente> listaCuenta = CuentaCliente.LeerTodasCuentas(lstClientes.SelectedItem.ToString());
            // Limpiar el listView
            lstcuentas.Items.Clear();

            // Verificar si existen departamentos
            if (listaCuenta.Any())
            {
                listaCuenta.ForEach(Cliente => lstcuentas.Items.Add(Cliente.numero));
            }
            else
                lstcuentas.Items.Add("¡No hay cuentas!");
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
            txtNumero.Text = "";
            txtNumero.Focus();
            lstClientes.SelectedIndex = -1;
            lstClientes.Items.Clear();
            lstcuentas.Items.Clear();
            lstcuentas.SelectedIndex =- 1;
            //Volvemos a llenar las listas
            CargarDatos();
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
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

        private void lstcuentas_Click(object sender, EventArgs e)
        {
            //cargamos el dato de saldo para ser actualizado.
            CuentaCliente existe = CuentaCliente.ObtenerCliente(lstcuentas.SelectedItem.ToString());
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.numero != "")
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtNumero.Text = existe.numero;
                txtPin.Text = existe.pin;
                txtSaldo.Text = Convert.ToString(existe.saldo);
            }
            else
            {
                MessageBox.Show("Error en cargar datos");
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //verificamos que todo este lleno.
            if(txtNumero.Text==""||txtPin.Text=="" || txtSaldo.Text == "")
            {
                MessageBox.Show("Debe llenar los detalles de la cuenta", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase cuentaCliente
                CuentaCliente cuenta = new CuentaCliente();
                cuenta.nuevoNumero = txtNumero.Text;
                cuenta.numero = lstcuentas.SelectedItem.ToString();
                cuenta.pin = txtPin.Text;
                cuenta.saldo = Convert.ToDecimal(txtSaldo.Text);

                if (CuentaCliente.ActualizarCuenta(cuenta, lstClientes.SelectedItem.ToString()))
                {
                    MessageBox.Show("Cuenta Actualizada correctamente", "Control de cuentas", MessageBoxButtons.OK);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error duranté la actualización", "Control de Cuentas", MessageBoxButtons.OK);
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //verificamos que hayan seleccionado una cuenta
            if (lstcuentas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //Instanciamos de la claseCuentaCliete
                CuentaCliente cuenta = new CuentaCliente();
                cuenta.numero = lstcuentas.SelectedItem.ToString();

                if (CuentaCliente.EliminarCuenta(cuenta))
                {
                    MessageBox.Show("La cuenta se eliminó correctamente", "Control de cuentas", MessageBoxButtons.OK);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la eliminación");
                }
            }
        }
    }
}
