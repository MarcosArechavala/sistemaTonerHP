using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Rubros
    {
        private CD_Rubros objCapaDato = new CD_Rubros();
        public List<Rubros> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Rubros obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Codigo) || string.IsNullOrWhiteSpace(obj.Codigo))
            {
                Mensaje = "Ingresar Codigo";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj);
            }
            else
            {
                return 0;
            }
        }


    }

   
}
