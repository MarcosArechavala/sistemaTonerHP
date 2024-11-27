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
    }
}
