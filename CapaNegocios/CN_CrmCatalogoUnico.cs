using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CrmCatalogoUnico
    {
        public IEnumerable<CrmCatalogoUnico> ObtenerPorConfiguracionDeProyecto(Sesion s, int idCte, int idOp)
        {
            CD_CrmCatalogoUnico cdCrmCatalogoUnico = new CD_CrmCatalogoUnico();
            var prods = cdCrmCatalogoUnico.ConsultarPorConfiguracionDeProyecto(s.Id_Emp, s.Id_Cd, idCte, idOp, s.Emp_Cnx_EF);
            return prods;
        }

        /// <summary>
        /// Regresa el conjunto de productos del catálogo único que coinciden con el identificador idPrd
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto de interés</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CrmCatalogoUnico]</returns>
        public IEnumerable<CrmCatalogoUnico> ObtenerPorProducto(Sesion s, int idPrd, IBusinessTransaction ibt)
        {
            CD_CrmCatalogoUnico cdCrmCatalogoUnico = new CD_CrmCatalogoUnico();
            return cdCrmCatalogoUnico.ConsultarPorProducto(s.Id_Emp, s.Id_Cd, idPrd, ibt.DataContext);
        }

        /// <summary>
        /// Regresa el unico producto del catálogo único asociado al proyecto [idOp] (debido a que el proyecto se encuentra asociado a solo una aplicación) que coinciden con el identificador idPrd
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <param name="idPrd">Identificador del producto de interés</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CrmCatalogoUnico]</returns>
        public CrmCatalogoUnico ObtenerPorProductoYProyecto(Sesion s, int idCte, int idOp, int idPrd, IBusinessTransaction ibt)
        {
            CD_CrmCatalogoUnico cdCrmCatalogoUnico = new CD_CrmCatalogoUnico();
            //Obteniendo la aplicacion asociada con el proyecto. El identificador del sistema de la aplicación ayudará a rastrear la ruta configurada del producto en el catálogo único
            CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
            var aplicaciones=cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(s, idCte, idOp, ibt);
            if (aplicaciones.Count() > 0)
            {
                var aplicacion = aplicaciones.First();
                var catalogoUnico = cdCrmCatalogoUnico.Consultar(s.Id_Emp, aplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Id_Uen,
                                    aplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Id_Seg,
                                    aplicacion.CatAplicacion.CatSolucion.CatArea.Id_Area,
                                    aplicacion.CatAplicacion.CatSolucion.Id_Sol,
                                    aplicacion.CatAplicacion.Id_Apl,
                                    0,
                                    idPrd,
                                    ibt.DataContext);
                if(catalogoUnico.Count()>0)
                {
                    var productoCU=catalogoUnico.First();
                    return productoCU;
                }
            }
            return null;
        }
    }
}
