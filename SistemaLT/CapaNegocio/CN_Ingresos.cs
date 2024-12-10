using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Ingresos
    {
        // Método para verificar si la cadena es alfanumérica
        private bool IsAlphanumeric(string input)
        {
            // Expresión regular que permite solo letras y números
            return Regex.IsMatch(input, "^[a-zA-Z0-9]*$");
        }

        private CD_Ingresos objCapaDato = new CD_Ingresos();

        public List<Ingresos> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Ingresos objeto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objeto.oProveedores.IdProveedor == 0)
            {
                Mensaje = "Ingresar proveedor o razon social";
            }

            else if (objeto.oProductos.IdProducto == 0)
            {
                Mensaje = "Ingresar producto";
            }

            else if (string.IsNullOrEmpty(objeto.CodigoId) || string.IsNullOrWhiteSpace(objeto.CodigoId))
            {
                Mensaje = "Ingresar codigo";
            }
            else if (!IsAlphanumeric(objeto.Observaciones))
            {
                Mensaje = "Las observaciones solo pueden contener letras y números.";
            }
            else if (objeto.IdUsuario == 0)
            {
                Mensaje = "Ingresar usuario";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(objeto);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Ingresos objeto, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (objeto.oProveedores.IdProveedor == 0)
            {
                Mensaje = "Ingresar proveedor o razon social";
            }

            else if (objeto.oProductos.IdProducto == 0)
            {
                Mensaje = "Ingresar producto";
            }

            else if (string.IsNullOrEmpty(objeto.CodigoId) || string.IsNullOrWhiteSpace(objeto.CodigoId))
            {
                Mensaje = "Ingresar codigo";
            }

            else if (string.IsNullOrEmpty(objeto.Observaciones) || string.IsNullOrWhiteSpace(objeto.Observaciones))
            {
                Mensaje = "Ingresar codigo";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(objeto, out Mensaje);
            }
            else
            {
                return false;
            }




        }
    }
}
