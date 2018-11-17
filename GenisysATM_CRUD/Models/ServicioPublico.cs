using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Agregar los namespaces necesarios
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GenisysATM.Models
{
    class ServicioPublico
    {
        // Propiedades
        public int id { get; set; }
        public string descripcion { get; set; }
        public string descripcionactual { get; set; }

        // Constructores
        public ServicioPublico() { }

        // Métodos
        /// <summary>
        /// obtiene la información de un servicio publico 
        /// </summary>
        /// <returns>unServicio</returns>
        public static ServicioPublico obtenerServicio(string descripcion)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql;
            ServicioPublico resultado = new ServicioPublico();

            // Query SQL
            sql = @"SELECT *
                    FROM ATM.ServicioPublico
                    WHERE descripcion = @descripcion";

            SqlCommand cmd = conexion.EjecutarComando(sql);
            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 100).Value = descripcion;

                    rdr = cmd.ExecuteReader();
                }

                while (rdr.Read())
                {
                    resultado.id = rdr.GetInt32(0);
                    resultado.descripcion = rdr.GetString(1);
                }

                return resultado;
            }
            catch (SqlException)
            {
                return resultado;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        /// <summary>
        /// se encarga de leer todos los servicios que se encuentren
        /// almacenados en la tabla de servicios publicos
        /// </summary>
        /// <returns> lista de tipo SevicioPublico</returns>
        public static List<ServicioPublico> LeerTodos()
        {
            // Lista una de tipo  servicioPublico
            List<ServicioPublico> resultados = new List<ServicioPublico>();

            //instanciamos la conexion
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql = @"SELECT *
                    FROM ATM.ServicioPublico";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            try
            {
                // Establecer la conexión
                conexion.EstablecerConexion();

                // Ejecutar el query via un ExecuteReader
                SqlDataReader rdr = cmd.ExecuteReader();

                //Recorremos los elementos que se encuentra guardados
                // en la lista tipo ServicioPublico
                while (rdr.Read())
                {
                    ServicioPublico servi = new ServicioPublico();
                    // Asignar los valores de Reader al objeto Departamento
                    servi.id = rdr.GetInt32(0);
                    servi.descripcion = rdr.GetString(1);

                    // Agregar el Departamento a la List<Departamento>
                    resultados.Add(servi);
                }

                return resultados;
            }
            catch (SqlException)
            {
                return resultados;
            }
            finally
            {
                // Cerrar la conexión
                conexion.CerrarConexion();
            }
        }

        /// <summary>
        /// se encarga de capturar e insertar los datos de un nuevo
        /// servicio en la base de datos.
        /// </summary>
        /// <returns></returns>
        public static bool InsertarServicio(ServicioPublico elServicio)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_InsertarServicio");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = elServicio.descripcion;

            // intentamos insertar al nuevo ServicioPublico
            try
            {
                // establecemos la conexión
                conn.EstablecerConexion();

                // ejecutamos el comando
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace + "Detalles de la excepción");
                return false;
            }
            finally
            {
                conn.CerrarConexion();
            }
        }

        /// <summary>
        /// Se encarga de actualizar la información de un
        /// servicio seleccionado
        /// </summary>
        /// <param name="elServicio"></param>
        /// <returns>true si se actualizo correctamente, false si ocurrió un error</returns>
        public static bool ActualizarServicio(ServicioPublico elServicio)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_ActualizarServicio");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = elServicio.descripcionactual;

            cmd.Parameters.Add(new SqlParameter("@descripcionNueva", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcionNueva"].Value = elServicio.descripcion;

            // intentamos insertar al nuevo servicioPublico
            try
            {
                // establecemos la conexión
                conn.EstablecerConexion();

                // ejecutamos el comando
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace + "Detalles de la excepción");
                return false;
            }
            finally
            {
                conn.CerrarConexion();
            }
        }

     
        public static bool EliminarServicio(ServicioPublico elServicio)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_EliminarServicio");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = elServicio.descripcion;

            // intentamos insertar al nuevo servicioPublico
            try
            {
                // establecemos la conexión
                conn.EstablecerConexion();

                // ejecutamos el comando
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace + "Detalles de la excepción");
                return false;
            }
            finally
            {
                conn.CerrarConexion();
            }
        }
    }
}
