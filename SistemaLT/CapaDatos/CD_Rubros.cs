using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.Design;

namespace CapaDatos
{
    public class CD_Rubros
    {
        public List<Rubros> Listar()
        {
            List<Rubros> lista = new List<Rubros>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IdRubro, Rubro, Codigo, Activo, IdUsuario FROM Tonner_Rubros";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(new Rubros()
                            {
                                IdRubro = Convert.ToInt32(rdr["Idrubro"]),
                                Rubro= rdr["Rubro"].ToString(),
                                Codigo = rdr["Codigo"].ToString(),
                                Activo = Convert.ToBoolean(rdr["Activo"]),
                                IdUsuario = Convert.ToInt32(rdr["IdUsuario"])
                            });
                        }
                    }
                    
                }
            } catch
            {
                lista = new List<Rubros>();
            }
            return lista;
        }
    }
}
