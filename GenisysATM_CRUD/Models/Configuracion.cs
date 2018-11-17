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
    class Configuracion
    {
        // Propiedades
        public int id { get; set; }
        public string appKey { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }

        public string nuevoappKey { get; set; }
        // Constructores
        public Configuracion() { }

        // Métodos
        /// <summary>
        /// obtiene el valor de una configuración
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ObtenerConfiguracion(string key)
        {
            string valor = "";
            SqlDataReader rdr;
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            SqlCommand cmd = conn.EjecutarComando(@"SELECT valor 
                                                    FROM ATM.Configuracion 
                                                    WHERE appKey = @key");

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@key", SqlDbType.NVarChar, 50).Value = key;

                    rdr = cmd.ExecuteReader();
                }

                while (rdr.Read())
                {
                    valor = rdr.GetString(0);
                }

                return valor;
            }
            catch (SqlException )
            {
                return "Clave no válida";
            }
            finally
            {
                conn.CerrarConexion();
            }
        }

        /// <summary>
        /// obtiene la información de una configuración, especificada
        /// por el nombre
        /// </summary>
        /// <param name="identidad"></param>
        /// <returns> objeto de tipo configuración</returns>
        public static Configuracion ObtenerConfigutacionnombre(string Nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql;
            Configuracion resultado = new Configuracion();

            // Query SQL
            sql = @"SELECT id,appKey,descripcion,valor
                    FROM ATM.Configuracion
                    WHERE appKey = @Nombre";

            SqlCommand cmd = conexion.EjecutarComando(sql);
            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@Nombre", SqlDbType.Char, 13).Value = Nombre;

                    rdr = cmd.ExecuteReader();
                }

                while (rdr.Read())
                {
                    resultado.id = Convert.ToInt32(rdr[0]);
                    resultado.appKey = rdr.GetString(1);
                    resultado.descripcion = rdr.GetString(2);
                    resultado.valor = rdr.GetString(3);
                    
                    


                    // Remover espacios en blanco

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
        /// obtiene una lista de todas las configuraciones que se encuetren
        /// disponibles en la tabla ATM.Configuracion
        /// </summary>
        /// <returns></returns>
        public static List<Configuracion> LeerTodos()
        {
            // Lista una de tipo de clientes
            List<Configuracion> resultados = new List<Configuracion>();

            //instanciamos la conexion
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql = @"SELECT id,appKey,valor,descripcion
                    FROM ATM.Configuracion
                    ";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            try
            {
                // Establecer la conexión
                conexion.EstablecerConexion();

                // Ejecutar el query via un ExecuteReader
                SqlDataReader rdr = cmd.ExecuteReader();

                //Recorremos los elementos que se encuentra guardados
                // en la lista tipo cliente
                while (rdr.Read())
                {
                    Configuracion config = new Configuracion();
                    // Asignar los valores de Reader al objeto Departamento
                    config.id = Convert.ToInt32(rdr[0]);
                    config.appKey = rdr.GetString(1);
                    config.valor = rdr.GetString(2);
                    config.descripcion = rdr.GetString(3);
                    

                    // Agregar el Departamento a la List<Departamento>
                    resultados.Add(config);
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
        /// se encarga de capturar y almacenar una nueva
        /// configuración en la base de datos.
        /// </summary>
        /// <param name="true si se insertó y false si ocurrió un error"></param>
        /// <returns></returns>
        public static bool InsertarConfiguracion(Configuracion laConfiguracion)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_InsertarConfiguracion");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NChar, 50));
            cmd.Parameters["@nombre"].Value = laConfiguracion.appKey;

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NChar, 200));
            cmd.Parameters["@descripcion"].Value = laConfiguracion.descripcion;

            cmd.Parameters.Add(new SqlParameter("@valor", SqlDbType.NChar, 50));
            cmd.Parameters["@valor"].Value = laConfiguracion.valor;

            // intentamos insertar la nueva configuración
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
        /// captura los datos de la configuración y los
        /// actualia en la base de datos
        /// </summary>
        /// <param name="laConfiguracion"></param>
        /// <returns></returns>
        public static bool ActualizarConfiguracion(Configuracion laConfiguracion)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_ActualizarConfiguracion");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NChar, 50));
            cmd.Parameters["@nombre"].Value = laConfiguracion.appKey;

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NChar, 200));
            cmd.Parameters["@descripcion"].Value = laConfiguracion.descripcion;

            cmd.Parameters.Add(new SqlParameter("@valor", SqlDbType.NChar, 50));
            cmd.Parameters["@valor"].Value = laConfiguracion.valor;

            cmd.Parameters.Add(new SqlParameter("@nombrenuevo", SqlDbType.NChar, 50));
            cmd.Parameters["@nombrenuevo"].Value = laConfiguracion.nuevoappKey;


            // intentamos actualizar la nueva configuración
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
        /// Elimina una configuracion en especifico
        /// </summary>
        /// <param name="elCliente"></param>
        /// <returns></returns>
        public static bool EliminarConfiguracion(Configuracion laConfiguracion)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_EliminarConfiguracion");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NChar, 50));
            cmd.Parameters["@nombre"].Value = laConfiguracion.appKey;

            // intentamos insertar al nuevo cliente
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
