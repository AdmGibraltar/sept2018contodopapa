using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapAplicacionProducto
    {
        /// <summary>
        /// Regresa el resultado de la consulta del repositorio CapAplicacionProducto dada la aplicación y el producto
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idApl">Identificador de la aplicación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapAplicacionProducto]</returns>
        public IEnumerable<CapAplicacionProducto> ConsultarPorAplicacionYProducto(int idEmp, int idApl, int idPrd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var aplProds = from ap in ctx.CapAplicacionProductoes
                           where ap.Id_Emp==idEmp && ap.Id_Apl==idApl && ap.Id_Prd==idPrd
                           select ap;
            return aplProds;
        }

        /// <summary>
        /// Regresa el resultado de la consulta al repositorio CapAplicacionProducto dado el identificador de producto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapAplicacionProducto]</returns>
        public IEnumerable<CapAplicacionProducto> ConsultarPorProducto(int idEmp, int idPrd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var productos = from ap in ctx.CapAplicacionProductoes
                            where ap.Id_Emp == idEmp && ap.Id_Prd == idPrd
                            select ap;
            return productos;
        }
    }
}
