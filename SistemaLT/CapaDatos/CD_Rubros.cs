﻿using CapaEntidad;
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

        public int Registrar(Rubros obj)
        {
            string Mensaje;
            int idautogenerado = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("T_InsertarProv", oconexion);
                    cmd.Parameters.AddWithValue("IdRubro", obj.IdRubro);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                idautogenerado = 0;
            }
            return idautogenerado;
        }
    }
}
