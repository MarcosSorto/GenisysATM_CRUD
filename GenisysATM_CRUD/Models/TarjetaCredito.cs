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
    class TarjetaCredito
    {
        // Propiedades
        public int id { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public decimal limite { get; set; }
        public int idCliente { get; set; }

        public string nuevaDescripcion { get; set; } 

        // Métodos

            /// <summary>
            /// Obtiene las descripciones de una tarjeta en particular
            /// que pertenezca el cliente especificado en el parámetro Cliente.
            /// </summary>
            /// <param name="Tarjeta"></param>
            /// <param name="Cliente"></param>
            /// <returns></returns>
        public static TarjetaCredito ObtenerTarjetaCliente(string Tarjeta, string Cliente)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql;
            TarjetaCredito resultado = new TarjetaCredito();

            // Query SQL
            sql = @"DECLARE @codigoCliente INT
                  SET @codigoCliente =(SELECT id FROM ATM.Cliente WHERE nombres=@Cliente);
                  SELECT t.id,t.descripcion,t.monto,t.limite FROM ATM.TarjetaCredito AS t INNER JOIN ATM.CLiente as c
                  ON t.descripcion=@Tarjeta and t.idCliente=@codigoCliente;";

            SqlCommand cmd = conexion.EjecutarComando(sql);
            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@Cliente", SqlDbType.NVarChar, 100).Value = Cliente;
                    cmd.Parameters.Add("@Tarjeta", SqlDbType.NVarChar, 100).Value = Tarjeta;

                    rdr = cmd.ExecuteReader();
                }

                while (rdr.Read())
                {
                    resultado.id = rdr.GetInt32(0);
                    resultado.descripcion = rdr.GetString(1);
                    resultado.monto = rdr.GetDecimal(2);
                    resultado.limite = rdr.GetDecimal(3);

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
        /// Lista todas las tarjetas que un cliente tiene disponible.
        /// </summary>
        /// <returns></returns>
        public static List<TarjetaCredito> LeerTodos(string Cliente)
        {
            // Lista una de tipo de clientes
            List<TarjetaCredito> resultados = new List<TarjetaCredito>();

            //instanciamos la conexion
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");
            string sql = @"DECLARE @codigoCliente INT
                  SET @codigoCliente =(SELECT id FROM ATM.Cliente WHERE nombres=@Cliente);
                  SELECT t.id,t.descripcion,t.monto,t.limite FROM ATM.TarjetaCredito AS T
                  WHERE t.idCliente=@codigoCliente;";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            try
            {
                // Establecer la conexión
                conexion.EstablecerConexion();
                SqlDataReader rdr;
                using (cmd)
                {
                    cmd.Parameters.Add("@Cliente", SqlDbType.NVarChar, 100).Value = Cliente;
                    rdr = cmd.ExecuteReader();
                }

                //Recorremos los elementos que se encuentra guardados
                // en la lista tipo tarjetaCrédito
                while (rdr.Read())
                {
                    TarjetaCredito tarjeta = new TarjetaCredito();
                    // Asignar los valores de Reader al objeto TarjetaCrédito
                    tarjeta.id = rdr.GetInt32(0);
                    tarjeta.descripcion = rdr.GetString(1);
                    tarjeta.monto = rdr.GetDecimal(2);
                    tarjeta.limite = rdr.GetDecimal(3);

                    // Agregar el Departamento a la List<TarjetaDepartamento>
                    resultados.Add(tarjeta);
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
        /// Se encarga de almacenar una nueva tarjeta a un cliente
        /// especificado
        /// </summary>
        /// <param name="elCliente"></param>
        /// <returns></returns>
        public static bool InsertarTarjeta(string elCliente, TarjetaCredito laTarjeta)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_InsertarTarjeta");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = laTarjeta.descripcion;

            cmd.Parameters.Add(new SqlParameter("@monto", SqlDbType.Decimal));
            cmd.Parameters["@monto"].Value = laTarjeta.monto;

            cmd.Parameters.Add(new SqlParameter("@limite", SqlDbType.Decimal));
            cmd.Parameters["@limite"].Value = laTarjeta.limite;

            cmd.Parameters.Add(new SqlParameter("@cliente", SqlDbType.NVarChar, 100));
            cmd.Parameters["@cliente"].Value = elCliente;

            // intentamos insertar a la nueva Tarjeta
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
        /// Actualiza la información de la tarjeta de un cliente.
        /// </summary>
        /// <param name="elCliente"></param>
        /// <param name="laTarjeta"></param>
        /// <returns></returns>
        public static bool ActualizarTarjeta(string elCliente, TarjetaCredito laTarjeta)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_ActualizarTarjeta");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = laTarjeta.descripcion;

            cmd.Parameters.Add(new SqlParameter("@monto", SqlDbType.Decimal));
            cmd.Parameters["@monto"].Value = laTarjeta.monto;

            cmd.Parameters.Add(new SqlParameter("@limite", SqlDbType.Decimal));
            cmd.Parameters["@limite"].Value = laTarjeta.limite;

            cmd.Parameters.Add(new SqlParameter("@cliente", SqlDbType.NVarChar, 100));
            cmd.Parameters["@cliente"].Value = elCliente;

            cmd.Parameters.Add(new SqlParameter("@nuevaDescripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@nuevaDescripcion"].Value = laTarjeta.nuevaDescripcion;


            // intentamos Actualizar los datos de la Tarjeta
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
        /// Elimina la tarjeta de crédito de un cliente en particular.
        /// </summary>
        /// <param name="elCliente"></param>
        /// <param name="laTarjeta"></param>
        /// <returns></returns>
        public static bool EliminarTarjeta(string elCliente, TarjetaCredito laTarjeta)
        {

            Conexion conn = new Conexion(@"(local)\sqlexpress", "GenisysATM_V2");

            // enviamos y especificamos el comando a ejecutar
            SqlCommand cmd = conn.EjecutarComando("sp_EliminarTarjeta");
            cmd.CommandType = CommandType.StoredProcedure;

            // agregamos los parámetros que son requeridos

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 100));
            cmd.Parameters["@descripcion"].Value = laTarjeta.descripcion;

            cmd.Parameters.Add(new SqlParameter("@cliente", SqlDbType.NVarChar, 100));
            cmd.Parameters["@cliente"].Value = elCliente;
            // intentamos Eliminar la tarjeta
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
