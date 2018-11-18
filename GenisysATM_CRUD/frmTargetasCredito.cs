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
    public partial class frmTargetasCredito : Form
    {
        public frmTargetasCredito()
        {
            InitializeComponent();
        }

        /// <summary>
        /// cierra el módulo de tarjetas de crédito y regresa
        ///al menú principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTargetasCredito_Load(object sender, EventArgs e)
        {
            Limpiar();
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
        /// <summary>
        /// Se encarga de limpiar el contenido de los dato y recargar las
        /// listas de informaión.
        /// </summary>
        public void Limpiar()
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtdescripcion.Text = "";
            txtmonto.Text = "";
            txtlimite.Text = "";
            lstClientes.Items.Clear();
            lstTarjetasClientes.Items.Clear();
            CargarDatos();
            txtdescripcion.Focus();
        }

        private void lstClientes_Click(object sender, EventArgs e)
        {
            // obtenemos la lista de todos los clientes de la tabla
            List<TarjetaCredito> listaTarjeta = TarjetaCredito.LeerTodos(lstClientes.SelectedItem.ToString());

            // Limpiar el listView
            lstTarjetasClientes.Items.Clear();

            // Verificar si existen departamentos
            if (listaTarjeta.Any())
            {
                listaTarjeta.ForEach(tarjeta => lstTarjetasClientes.Items.Add(tarjeta.descripcion));
            }
            else
                lstTarjetasClientes.Items.Add("¡Cliente sin Tarjetas!");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtmonto.Text=="" || txtdescripcion.Text=="" || txtlimite.Text=="" || lstClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente y llenar todo los dato de la tarjeta", "Error en Ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase tarjetaCredito
                TarjetaCredito nueva = new TarjetaCredito();
                nueva.monto = Convert.ToDecimal(txtmonto.Text);
                nueva.limite =Convert.ToDecimal(txtlimite.Text);
                nueva.descripcion = txtdescripcion.Text;
                MessageBox.Show(nueva.monto.ToString());

                if (TarjetaCredito.InsertarTarjeta(lstClientes.SelectedItem.ToString(), nueva))
                {
                    MessageBox.Show("Tarjeta Registrada correctamente", "Control de Tarjetas de crédito", MessageBoxButtons.OK);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la inserción", "Control de tarjetas de crédito", MessageBoxButtons.OK);
                }
                 
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //instanciamos de la clase tarjetaCredito
            TarjetaCredito nueva = new TarjetaCredito();
            nueva.monto = Convert.ToDecimal(txtmonto.Text);
            nueva.limite = Convert.ToDecimal(txtlimite.Text);
            nueva.descripcion = lstTarjetasClientes.SelectedItem.ToString();
            nueva.nuevaDescripcion = txtdescripcion.Text;

            if (TarjetaCredito.ActualizarTarjeta(lstClientes.SelectedItem.ToString(), nueva))
            {
                MessageBox.Show("Tarjeta Actualizada  correctamente", "Control de Tarjetas de crédito", MessageBoxButtons.OK);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Ocurrió un error durante la Actualización", "Control de tarjetas de crédito", MessageBoxButtons.OK);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //instanciamos de la clase tarjetaCredito
            TarjetaCredito nueva = new TarjetaCredito();

            nueva.descripcion = lstTarjetasClientes.SelectedItem.ToString();

            if (TarjetaCredito.EliminarTarjeta(lstClientes.SelectedItem.ToString(),nueva))
            {
                MessageBox.Show("Tarjeta Eliminada correctamente", "Control de Tarjetas de crédito", MessageBoxButtons.OK);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Ocurrió un error durante la Eliminación", "Control de tarjetas de crédito", MessageBoxButtons.OK);
            }
        }

        private void lstTarjetasClientes_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;

            TarjetaCredito existe = TarjetaCredito.ObtenerTarjetaCliente(lstTarjetasClientes.SelectedItem.ToString(), lstClientes.SelectedItem.ToString());
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.id != 0)
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtdescripcion.Text = existe.descripcion;
                txtmonto.Text = Convert.ToString(existe.monto);
                txtlimite.Text = Convert.ToString(existe.limite);

            }

        }
    }
}
