using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Ingresos
    {
        public List<Ingresos> Listar()
        {
            List<Ingresos> lista = new List<Ingresos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT i.IdIngreso,");
                    sb.AppendLine("p.IdProveedor, p.RazonSocial,");
                    sb.AppendLine("prod.IdProducto, prod.Detalle, prod.StockActual,");
                    sb.AppendLine("i.CodigoId,i.Cantidad,i.Observaciones,i.IdUsuario,i.TipoIngreso,i.FechaIngreso");
                    sb.AppendLine("FROM Tonner_Ingresos i ");
                    sb.AppendLine("inner join Tonner_Proveedor p on p.IdProveedor = i.IdProveedor");
                    sb.AppendLine("inner join Tonner_Productos prod on prod.IdProducto = i.IdProducto ");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using(SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(new Ingresos()
                            {
                                oProveedores = new Proveedores() { IdProveedor = Convert.ToInt32(rdr["IdProveedor"]), RazonSocial = rdr["RazonSocial"].ToString() },
                                oProductos = new Productos() { IdProducto = Convert.ToInt32(rdr["IdProducto"]), Detalle = rdr["Detalle"].ToString() },
                                oStockActual = new Productos() { IdProducto = Convert.ToInt32(rdr["IdProducto"]), StockActual = Convert.ToInt32(rdr["StockActual"]) },
                                CodigoId = rdr["CodigoId"].ToString(),
                                Cantidad = Convert.ToInt32(rdr["Cantidad"]),
                                Observaciones = rdr["Observaciones"].ToString(),
                                TipoIngreso = Convert.ToChar(rdr["TipoIngreso"]),

                            });
                        }
                    }
                }
            }catch
            {
                lista = new List<Ingresos>();
            }
            return lista;
        }


        public int Registrar(Ingresos obj)
        {
            
            int idautogenerado = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("T_InsertarIngresos", oconexion);
                cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedores.IdProveedor);
                cmd.Parameters.AddWithValue("IdProducto", obj.oProductos.IdProducto);
                cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                cmd.Parameters.AddWithValue("CodigoId", obj.CodigoId);
                cmd.Parameters.AddWithValue("Observaciones", obj.Observaciones);
                cmd.Parameters.AddWithValue("TipoIngreso", obj.TipoIngreso);
                cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                cmd.Parameters.AddWithValue("FechaIngreso", obj.FechaIngreso);
                cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                cmd.ExecuteNonQuery();

                idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                //Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            return idautogenerado;
        }

        public bool Editar(Ingresos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("T_ModificarProductos", oconexion);
                    cmd.Parameters.AddWithValue("IdIngresos", obj.IdIngreso);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedores.IdProveedor);
                    cmd.Parameters.AddWithValue("IdTipo", obj.oProductos.IdProducto);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("CodigoId", obj.CodigoId);
                    cmd.Parameters.AddWithValue("Observaciones", obj.Observaciones);
                    cmd.Parameters.AddWithValue("TipoIngreso", obj.CodigoId);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("FechaIngreso", obj.FechaIngreso);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

        }
    }
}
