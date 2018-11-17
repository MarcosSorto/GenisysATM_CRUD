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

        /// <summary>
        /// inicializa y carga los datos en la lista con los servicios publicos
        /// disponibles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmServicioPublico_Load(object sender, EventArgs e)
        {
            
            Limpiar();
        }
        
        /// <summary>
        /// Se encarga de listar los servicios publicos
        /// disponibles en lstServicios
        /// </summary>
        public void cargarDatos()
        {

            // obtenemos la lista de todos los clientes de la tabla
            List<ServicioPublico> listaServicio = ServicioPublico.LeerTodos();

            // Limpiar el listView
            lstServicios.Items.Clear();

            // Verificar si existen departamentos
            if (listaServicio.Any())
            {
                listaServicio.ForEach(servicio => lstServicios.Items.Add(servicio.descripcion));
            }
            else
                lstServicios.Items.Add("¡No hay servicios!");
        }

        public void Limpiar()
        {
            txtDescripcion.Text = "";
            lstServicios.Items.Clear();
            cargarDatos();
            txtDescripcion.Focus();
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnAgregar.Enabled = true;
            cargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //verificamos que el usuario haya ingresado una descripción para guardar
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe de llenar la descrición", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase servicioPublico.
                ServicioPublico nuevo = new ServicioPublico();
                nuevo.descripcion = txtDescripcion.Text;
                if (ServicioPublico.InsertarServicio(nuevo))
                {
                    MessageBox.Show("Servicio registrado satifactoriamente", "Control de servicios públicos", MessageBoxButtons.OK);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error durante la inserción", "Control de servicios", MessageBoxButtons.OK);
                }
            }
        }

        private void grpOperaciones_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// carga los datos del servicio seleccionado en el 
        /// listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstServicios_Click(object sender, EventArgs e)
        {
            ServicioPublico existe = ServicioPublico.obtenerServicio(lstServicios.SelectedItem.ToString());
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.id != 0)
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtDescripcion.Text = existe.descripcion;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(lstServicios.SelectedIndex == -1)
            {
                MessageBox.Show("¡Debe seleecionar un servicio", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase servicioPublico.
                ServicioPublico actualizar = new ServicioPublico();
                actualizar.descripcionactual = lstServicios.SelectedItem.ToString();
                actualizar.descripcion = txtDescripcion.Text;
                if (ServicioPublico.ActualizarServicio(actualizar))
                {
                    MessageBox.Show("Servicio Actualizado satifactoriamente", "Control de servicios públicos", MessageBoxButtons.OK);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la actualización", "Control de servicios públicos", MessageBoxButtons.OK);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstServicios.SelectedIndex == -1)
            {
                MessageBox.Show("¡Debe seleccionar un servicio", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase servicioPublico.
                ServicioPublico Eliminar = new ServicioPublico();
                Eliminar.descripcion = txtDescripcion.Text;
                if (ServicioPublico.EliminarServicio(Eliminar))
                {
                    MessageBox.Show("Servicio Eliminado satifactoriamente", "Control de servicios públicos", MessageBoxButtons.OK);
                    Limpiar();
                }
            }
        }
    }
}
