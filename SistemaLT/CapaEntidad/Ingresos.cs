using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
//    IdIngreso int (Auto) Clave P
//Idproducto  int Clave F
//CodigoId    Varchar(30) Productos que se quieran identificar con algun codigo al ingreso
//Cantidad int
//FechaIngreso    smalldatetime
//Observaciones   Varchar(50)
//IdUsuario int
//StockActual Int Stock antes del ingreso
//FechayHoraActualizacion smalldatetime
//TipoIngreso char (1)	(C) ompra/(D) evolucion/(O) tros
//IdProveedor int (el proveedor interno en caso de que no sea por compras)

    public class Ingresos
    {
        public int IdIngreso {  get; set; }
        public Productos oProductos { get; set; }
        public string CodigoId { get; set; }
        public int Cantidad { get; set; }
        public string Observaciones { get; set; }
        public int IdUsuario { get; set; }
        public int StockActual {  get; set; }
        public char TipoIngreso { get; set; }
        public Proveedores oProveedores { get; set; }
    }
}
