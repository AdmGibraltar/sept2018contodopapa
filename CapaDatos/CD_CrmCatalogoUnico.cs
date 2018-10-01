using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmCatalogoUnico
    {
        public IEnumerable<CrmCatalogoUnico> ConsultarPorConfiguracionDeProyecto(int idEmp, int idCd, int idCte, int idOp, string cadenaDeConexionEF)
        {
            IEnumerable<CrmCatalogoUnico> ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                Func<CrmCatalogoUnico, CrmCatalogoUnico> sel = cu =>
                                 {
                                     cu.DescripcionProducto = string.Format("{0}", cu.CatProducto.Prd_Descripcion, cu.CatUEN.Uen_Descripcion, cu.CatSegmento.Seg_Descripcion, cu.CatArea.Area_Descripcion, cu.CatSolucion.Sol_Descripcion, cu.CatAplicacion.Apl_Descripcion);
                                     return cu;
                                 };
                var productosEnOportunidad = (from p in ctx.CrmOportunidadesProductos
                                              where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Op == idOp && p.Id_Cte == idCte
                                              select p.Id_Prd).ToList();
                var productos = (from p in ctx.CrmCatalogoUnicoes
                                 join o in ctx.CrmOportunidades
                                 on new { Id_Emp = p.Id_Emp, /*Id_Uen = p.Id_Uen, Id_Seg = p.Id_Seg, Id_Area = p.Id_Area, Id_Sol = p.Id_Sol*/ } equals new { Id_Emp = o.Id_Emp, /*Id_Uen = o.Id_Uen.Value, Id_Seg = o.Id_Seg.Value, Id_Area = o.ID_Area.Value, Id_Sol = o.Id_Sol.Value*/ }
                                 join app in ctx.CrmOportunidadesAplicacions on new { Id_Emp = o.Id_Emp, Id_Cd = o.Id_Cd, Id_Op = o.Id_Op, Id_Apl = p.Id_Apl } equals new { Id_Emp = app.Id_Emp, Id_Cd = app.Id_Cd, Id_Op = app.Id_Op, Id_Apl = app.Id_Apl }
                                 where o.Id_Op == idOp && o.Id_Cte == idCte && o.Id_Emp == idEmp && o.Id_Cd == idCd && !productosEnOportunidad.Contains(p.Id_Prd)
                                 select p).Select(sel).ToList();
                ret = productos;
            }
            return ret;
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el repositorio CrmCatalogoUnico.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CrmCatalogoUnico]</returns>
        public IEnumerable<CrmCatalogoUnico> ConsultarPorProducto(int idEmp, int idCd, int idPrd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var productos = from o in ctx.CrmCatalogoUnicoes
                            where o.Id_Emp==idEmp
                            && o.Id_Prd==idPrd
                            select o;
            return productos;
        }

        /// <summary>
        /// Devuelve elresultado de la consulta sobre el repositorio CrmCatalogUnico condicionado por empresa
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="icdCtx">Contexto de conexión a la base de datos</param>
        /// <returns>IEnumerable[CrmCatalogoUnico]</returns>
        public IEnumerable<CrmCatalogoUnico> Consultar(int idEmp, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var catalogo = from p in ctx.CrmCatalogoUnicoes
                           where p.Id_Emp==idEmp
                           select p;
            return catalogo;
        }

        /// <summary>
        /// Devuelve elresultado de la consulta sobre el repositorio CrmCatalogUnico condicionado por la ruta de oferta del producto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="icdCtx">Contexto de conexión a la base de datos</param>
        /// <returns>IEnumerable[CrmCatalogoUnico]</returns>
        public IEnumerable<CrmCatalogoUnico> Consultar(int idEmp, int idUen, int idSeg, int idArea, int idSol, int idApl, int idSubFam, int idPrd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var catalogo = from p in ctx.CrmCatalogoUnicoes
                           where p.Id_Emp == idEmp
                           && p.Id_Uen==idUen
                           && p.Id_Seg==idSeg
                           && p.Id_Area==idArea
                           && p.Id_Sol==idSol
                           && p.Id_Apl==idApl
                           && p.Id_SubFam==idSubFam
                           && p.Id_Prd==idPrd
                           select p;
            return catalogo;
        }
    }
}
