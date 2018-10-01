using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CapAplicacionProducto
    {
        /// <summary>
        /// Regresa el conjunto de asociaciones entre una aplicación y un producto (en práctica solo es uno)
        /// </summary>
        /// <param name="s">Sesión del usuario en operación.</param>
        /// <param name="idApl">Identificador de la aplicación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CapAplicacionProducto]</returns>
        public IEnumerable<CapAplicacionProducto> ObtenerPorAplicacionYProducto(Sesion s, int idApl, int idPrd, IBusinessTransaction ibt)
        {
            CD_CapAplicacionProducto cdCapAplicacionProducto = new CD_CapAplicacionProducto();
            return cdCapAplicacionProducto.ConsultarPorAplicacionYProducto(s.Id_Emp, idApl, idPrd, ibt.DataContext);
        }

        /// <summary>
        /// Regresa el conjunto de asociaciones entre una aplicación y un producto (que en práctica solo debe ser uno) dado el proyecto para el cual se requiere dicho producto
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="ibt">Transacción a nivel de capa de negocio</param>
        /// <returns>IEnumerable[CapAplicacionProducto]</returns>
        public IEnumerable<CapAplicacionProducto> ObtenerPorProyectoYProducto(Sesion s, int idCte, int idOp, int idPrd, IBusinessTransaction ibt)
        {
            CD_CapAplicacionProducto cdCapAplicacionProducto = new CD_CapAplicacionProducto();
            CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
            var aplicaciones=cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(s, idCte, idOp, ibt);
            if (aplicaciones.Count() > 0)
            {
                var aplicacion = aplicaciones.First();
                return ObtenerPorAplicacionYProducto(s, aplicacion.Id_Apl, idPrd, ibt);
            }
            return null;
        }

        /// <summary>
        /// Regresa el conjunto de asociaciones entre una aplicación y los productos asociados (en práctica, el total de asociaciones debería ser 1).
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CapAplicacionProducto]</returns>
        public IEnumerable<CapAplicacionProducto> ObtenerPorProducto(Sesion s, int idPrd, IBusinessTransaction ibt)
        {
            CD_CapAplicacionProducto cdCapAplicacionProducto = new CD_CapAplicacionProducto();
            return cdCapAplicacionProducto.ConsultarPorProducto(s.Id_Emp, idPrd, ibt.DataContext);
        }
    }
}
