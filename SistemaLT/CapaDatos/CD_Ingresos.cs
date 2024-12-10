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
                                FechaIngreso = Convert.ToDateTime(rdr["FechaIngreso"]),

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

    }
}
