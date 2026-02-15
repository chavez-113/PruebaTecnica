using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.DataAccess.Repositories
{
    public class ScriptDataBase
    {
        // Clientes
        public static string Cliente_Insertar = "dbo.SP_Cliente_Crear";
        public static string Cliente_Listar = "dbo.SP_Clientes_Listar";
        public static string Cliente_Actualizar = "dbo.SP_Cliente_Actualizar";
        public static string Cliente_Buscar = "dbo.SP_Cliente_BuscarId";

        // Producto
        public const string Producto_Insertar = "dbo.SP_Producto_Crear";
        public const string Producto_Listar = "dbo.SP_Producto_Listar";
        public const string Producto_Buscar = "dbo.SP_Producto_Buscar";
        public const string Producto_Actualizar = "dbo.SP_Producto_Actualizar";
        public const string Producto_ActualizarExistencia = "dbo.SP_Producto_ActualizarExistencia";

        // Orden
        public const string Orden_Crear = "dbo.SP_Orden_Crear";
        public const string Orden_ActualizarTotales = "dbo.SP_Orden_ActualizarTotales";
        public const string Orden_ObtenerConDetalles = "dbo.SP_Orden_ObtenerConDetalles";

        // DetalleOrden
        public const string DetalleOrden_Crear = "dbo.SP_DetalleOrden_Crear";

    }
}
