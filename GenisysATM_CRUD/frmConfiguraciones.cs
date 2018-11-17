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
    public partial class frmConfiguraciones : Form
    {
        public frmConfiguraciones()
        {
            InitializeComponent();
        }

        /// <summary>
        /// cierra el formulario de configuración
        /// y regresa al menú principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Limpia ycarga los dtos iniciales en la lista de 
        /// configuraciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConfiguraciones_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        /// <summary>
        /// Se encarga de limpiar todos los datos despues una acción.
        /// </summary>
        public void limpiar()
        {
            txtNombres.Text = "";
            txtDescripcion.Text = "";
            txtValor.Text = "";
            lstconfiguracion.Items.Clear();
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            llenar();
            txtNombres.Focus();
        }

        /// <summary>
        /// obtiene todos las configuraciones que se encuantran en la tabla
        /// y los carga en la lista.
        /// </summary>
        public void llenar()
        {
            // obtenemos la lista de todos los clientes de la tabla
            List<Configuracion> listaConfiguracion = Configuracion.LeerTodos();

            // Limpiar el listView
            lstconfiguracion.Items.Clear();

            // Verificar si existen departamentos
            if (listaConfiguracion.Any())
            {
                listaConfiguracion.ForEach(config => lstconfiguracion.Items.Add(config.appKey));
            }
            else
                lstconfiguracion.Items.Add("¡No hay servicios!");
        }

        /// <summary>
        /// Se encarga de llamar al método de inserción
        /// para registrar una nueva configuración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNombres.Text=="" || txtDescripcion.Text=="" || txtValor.Text == "")
            {
                MessageBox.Show("Hay datos que no han sido llenados ¡Revise!", "Error en Ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la clase configuracion
                Configuracion nueva = new Configuracion();
                nueva.appKey = txtNombres.Text;
                nueva.descripcion = txtDescripcion.Text;
                nueva.valor = txtValor.Text;

                if (Configuracion.InsertarConfiguracion(nueva))
                {
                    MessageBox.Show("Configuración registrada correctamente", "Control de configuraciones", MessageBoxButtons.OK);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error en la inserción", "Control de configuraciones", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de una configuracion en particular
        /// y llama al método actualizar para guardar los cambios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(lstconfiguracion.SelectedIndex == -1){
                MessageBox.Show("Debe seleccionar una configuración", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la configuracion
                Configuracion actualizar = new Configuracion();
                actualizar.appKey = lstconfiguracion.SelectedItem.ToString();
                actualizar.nuevoappKey = txtNombres.Text;
                actualizar.valor = txtValor.Text;
                actualizar.descripcion = txtDescripcion.Text;

                if (Configuracion.ActualizarConfiguracion(actualizar))
                {
                    MessageBox.Show("Configuracion actualizada correctamente", "Control de configuraciones", MessageBoxButtons.OK);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la actualización", "Control de configuraciones", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// LLama al método eliminar configuración para
        /// el elemento seleccionado en la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstconfiguracion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una configuración", "Error en ingreso", MessageBoxButtons.OK);
            }
            else
            {
                //instanciamos de la configuracion
                Configuracion Eliminar = new Configuracion();
                Eliminar.appKey = lstconfiguracion.SelectedItem.ToString();

                if (Configuracion.EliminarConfiguracion(Eliminar))
                {
                    MessageBox.Show("Configuracion Eliminada correctamente", "Control de configuraciones", MessageBoxButtons.OK);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error durante la Eliminación", "Control de configuraciones", MessageBoxButtons.OK);
                }
            }
        }

        private void lstconfiguracion_Click(object sender, EventArgs e)
        {
            Configuracion existe = Configuracion.ObtenerConfigutacionnombre(lstconfiguracion.SelectedItem.ToString());
            //verificamos si el cliente esta registrado e inhabilitamos el boton guardar
            if (existe.id != 0)
            {
                // deshabilitamos el boton de guardar, dado que ya existe el cliente
                btnAgregar.Enabled = false;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                txtNombres.Text = existe.appKey;
                txtDescripcion.Text = existe.descripcion;
                txtValor.Text = existe.valor;
            }
        }
    }
}
