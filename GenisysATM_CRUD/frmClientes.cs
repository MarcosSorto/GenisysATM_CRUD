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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }


        private void lstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// retorna al menú principal y cierra el módulo de clientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// se encarga de enviar los datos capturados para ser guardados en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // verificamos si los campos están llenos
            if (txtIdentidad.Text == "" || txtNombres.Text == "" || txtApellidos.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "" || txtCelular.Text == "")
            {
                MessageBox.Show("Aun hay datos que aun no se han llenado, ¡Revise!", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase cliente
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.identidad = txtIdentidad.Text;
                nuevoCliente.nombres = txtNombres.Text;
                nuevoCliente.apellidos = txtApellidos.Text;
                nuevoCliente.direccion = txtDireccion.Text;
                nuevoCliente.telefono = txtTelefono.Text;
                nuevoCliente.celular = txtCelular.Text;

                // mandamos los datos al método insertarCliente y verificamos su resultado
                if (Cliente.InsertarCliente(nuevoCliente))
                {
                    MessageBox.Show("El Cliente ha sido registrado satifactoriamente", "Control de Clientes", MessageBoxButtons.OK);

                    //limpiamos los datos
                    LimpiarDatos();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error en la inserción de los datos", "Control de Clientes", MessageBoxButtons.OK);
                }

            }


        }
        /// <summary>
        /// se encarga de limpiar los datos contenidos en los textBox.
        /// </summary>
        public void LimpiarDatos()
        {
            txtIdentidad.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            lstClientes.Refresh();
            lstClientes.SelectedIndex = -1;
            lstClientes.DataSource = null;
            cargarDatos();
            txtIdentidad.Focus();
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        /// <summary>
        /// llama a obtener cliente con el objetivo
        /// de llenar los doatos en pantalla en el caso que el cliente
        /// exista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIdentidad_Leave(object sender, EventArgs e)
        {

            Cliente existe = Cliente.ObtenerCliente(txtIdentidad.Text);
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.id != 0)
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtNombres.Text = existe.nombres;
                txtApellidos.Text = existe.apellidos;
                txtDireccion.Text = existe.direccion;
                txtTelefono.Text = existe.telefono;
                txtCelular.Text = existe.celular;
            }
        }

        /// <summary>
        /// inicializa los companentes del formuario,
        /// llama a la función cargar datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmClientes_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }


        /// <summary>
        /// Llama al método actualizar cliente y gurada los cambios
        /// en el cliente especificado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // verificamos si los campos están llenos
            if (txtIdentidad.Text == "" || txtNombres.Text == "" || txtApellidos.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "" || txtCelular.Text == "")
            {
                MessageBox.Show("Aun hay datos que aun no se han llenado, ¡Revise!", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase cliente
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.identidad = txtIdentidad.Text;
                nuevoCliente.nombres = txtNombres.Text;
                nuevoCliente.apellidos = txtApellidos.Text;
                nuevoCliente.direccion = txtDireccion.Text;
                nuevoCliente.telefono = txtTelefono.Text;
                nuevoCliente.celular = txtCelular.Text;

                // mandamos los datos al método insertarCliente y verificamos su resultado
                if (Cliente.ActualizarCliente(nuevoCliente))
                {
                    MessageBox.Show("El Cliente ha sido actualizado satifactoriamente", "Control de Clientes", MessageBoxButtons.OK);

                    //limpiamos los datos
                    LimpiarDatos();
                    btnAgregar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error en la actualización de los datos", "Control de Clientes", MessageBoxButtons.OK);
                }

            }
        }

        /// <summary>
        /// Llama al método eliminar de la clase clientes y
        /// elimina al cliente especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">identidad del cliente</param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // verificamos si los campos están llenos
            if (lstClientes.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado un cliente en la lista o no ha especificado una identidad", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase cliente
                Cliente EliminarCliente = new Cliente();
                EliminarCliente.nombres = txtNombres.Text;
                EliminarCliente.identidad = txtIdentidad.Text;

                // mandamos los datos al método insertarCliente y verificamos su resultado
                if (Cliente.EliminarCliente(EliminarCliente))
                {
                    MessageBox.Show("El Cliente ha sido eliminado satifactoriamente", "Control de Clientes", MessageBoxButtons.OK);

                    //limpiamos los datos
                    LimpiarDatos();
                    btnAgregar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error en la actualización de los datos", "Control de Clientes", MessageBoxButtons.OK);
                }

            }
        }

        /// <summary>
        /// Se encaga de cargar los datos en la lista de los
        /// clientes.
        /// </summary>
        public void cargarDatos()
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
        /// carga los datos del cliente seleccionado en la
        /// lista de los clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstClientes_Click(object sender, EventArgs e)
        {
            Cliente existe = Cliente.ObtenerClienteNombre(lstClientes.SelectedItem.ToString());
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.id != 0)
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtNombres.Text = existe.nombres;
                txtApellidos.Text = existe.apellidos;
                txtDireccion.Text = existe.direccion;
                txtTelefono.Text = existe.telefono;
                txtCelular.Text = existe.celular;
                txtIdentidad.Text = existe.identidad;
            }
        }
    }
}
