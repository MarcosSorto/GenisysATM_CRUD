using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Agregar los namespaces necesarios
using System.Data;
using System.Data.SqlClient;

namespace GenisysATM.Models
{
    class Cliente
    {
        // Propiedades para la clase
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string identidad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }

        // Métodos
        /// <summary>
        /// Obtiene un cliente desde la tabla ATM.Cliente
        /// </summary>
        /// <param name="identidad">La identidad del cliente (13 caracteres)</param>
        /// <returns>Un objeto de tipo Cliente.</returns>
        public static Cliente ObtenerCliente(string identidad)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql;
            Cliente resultado = new Cliente();

            // Query SQL
            sql = @"SELECT *
                    FROM ATM.Cliente
                    WHERE identidad = @identidad";

            SqlCommand cmd = conexion.EjecutarComando(sql);
            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@identidad", SqlDbType.Char, 13).Value = identidad;

                    rdr = cmd.ExecuteReader();
                }

                while (rdr.Read())
                {
                    resultado.id = rdr.GetInt32(0);
                    resultado.nombres = rdr.GetString(1);
                    resultado.apellidos = rdr.GetString(2);
                    resultado.identidad = rdr.GetString(3);
                    resultado.direccion = rdr.GetString(4);
                    resultado.telefono = rdr.GetString(5);
                    resultado.celular = rdr.GetString(6);

                    // Remover espacios en blanco
     
                }

                return resultado;
            }
            catch (SqlException )
            {
                return resultado;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public static Cliente ObtenerClienteNombre(string Nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql;
            Cliente resultado = new Cliente();

            // Query SQL
            sql = @"SELECT *
                    FROM ATM.Cliente
                    WHERE nombres = @Nombre";

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
                    resultado.id = rdr.GetInt32(0);
                    resultado.nombres = rdr.GetString(1);
                    resultado.apellidos = rdr.GetString(2);
                    resultado.identidad = rdr.GetString(3);
                    resultado.direccion = rdr.GetString(4);
                    resultado.telefono = rdr.GetString(5);
                    resultado.celular = rdr.GetString(6);

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
        /// lista todos los cliente que se encuentran almacenados en la tabla ATM.cliente
        /// </summary>
        /// <returns></returns>
        public static List<Cliente> LeerTodos()
        {
            // Lista una de tipo de clientes
            List<Cliente> resultados = new List<Cliente>();

            //instanciamos la conexion
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql = @"SELECT identidad, nombres
                    FROM ATM.Cliente";

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
                    Cliente cli= new Cliente();
                    // Asignar los valores de Reader al objeto Cliente
                    cli.identidad= rdr.GetString(0);
                    cli.nombres = rdr.GetString(1);

                    // Agregar el Cliente a la List<Cliete>
                    resultados.Add(cli);
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
        /// se encarga de guardar un nuevo cliente en la base de datos, recibe 
        /// como parametros un objeto de tipo cliente.
        /// </summary>
        /// <returns>true: si se insertó correctamente false: si ocurrió un error</returns>
        public static bool InsertarCliente(Cliente elCliente)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_InsertarCliente");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@identidad", SqlDbType.Char, 13));
            cmd.Parameters["@identidad"].Value = elCliente.identidad;

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar, 100));
            cmd.Parameters["@nombre"].Value = elCliente.nombres;

            cmd.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar, 100));
            cmd.Parameters["@apellido"].Value = elCliente.apellidos;

            cmd.Parameters.Add(new SqlParameter("@direccion", SqlDbType.NVarChar, 2000));
            cmd.Parameters["@direccion"].Value = elCliente.direccion;

            cmd.Parameters.Add(new SqlParameter("@telefono", SqlDbType.Char, 9));
            cmd.Parameters["@telefono"].Value = elCliente.telefono;

            cmd.Parameters.Add(new SqlParameter("@celular", SqlDbType.Char, 9));
            cmd.Parameters["@celular"].Value = elCliente.celular;

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
        
        /// <summary>
        /// Actualiza los datos de un cliente en particular
        /// </summary>
        /// <param name="elcliente"></param>
        /// <returns></returns>
        public static bool ActualizarCliente(Cliente elCliente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_ActualizarCliente");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@identidad", SqlDbType.Char, 13));
            cmd.Parameters["@identidad"].Value = elCliente.identidad;

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar, 100));
            cmd.Parameters["@nombre"].Value = elCliente.nombres;

            cmd.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar, 100));
            cmd.Parameters["@apellido"].Value = elCliente.apellidos;

            cmd.Parameters.Add(new SqlParameter("@direccion", SqlDbType.NVarChar, 2000));
            cmd.Parameters["@direccion"].Value = elCliente.direccion;

            cmd.Parameters.Add(new SqlParameter("@telefono", SqlDbType.Char, 9));
            cmd.Parameters["@telefono"].Value = elCliente.telefono;

            cmd.Parameters.Add(new SqlParameter("@celular", SqlDbType.Char, 9));
            cmd.Parameters["@celular"].Value = elCliente.celular;

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

        public static bool EliminarCliente(Cliente elCliente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_EliminarCliente");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@identidad", SqlDbType.Char, 13));
            cmd.Parameters["@identidad"].Value = elCliente.identidad;

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar, 100));
            cmd.Parameters["@nombre"].Value = elCliente.nombres;

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
