using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapValProyectoDet
    {
        /// <summary>
        /// Inserta un conjunto de entradas en el repositorio CapValProyectoDet.
        /// </summary>
        /// <param name="entradas">Conjunto de entradas a insertar</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void Insertar(IEnumerable<CapValProyectoDet> entradas, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            ctx.CapValProyectoDets.AddRange(entradas);
        }

        /// <summary>
        /// Regresa la consulta de la entidad [CapValProyectoDet] mediante la llave [Id_Vap].
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuacion</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>IEnumerable<CapValProyectoDet>. Resultado de la consulta a la entidad [CapValProyectoDet]</returns>
        public IEnumerable<CapValProyectoDet> ConsultarPorCapValProyectoId(int idEmp, int idCd, int idVal, string cadenaConexionEF)
        {
            IEnumerable<CapValProyectoDet> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var dets = (from d in ctx.CapValProyectoDets
                            where d.Id_Emp == idEmp && d.Id_Cd == idCd && d.Id_Vap == idVal
                            select d).ToList();
                resultado = dets;
            }
            return resultado;
        }

        public CapaEntidad.eResultadoValuacion Consultar_ResultadoValuacion(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Vap, ICD_Contexto icdCtx)
        {
            CapaEntidad.eResultadoValuacion rv = new CapaEntidad.eResultadoValuacion();

            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
                        
            var CapValProyecto = (from cvp in ctx.CapValProyectoes
                where cvp.Id_Emp == Id_Emp && cvp.Id_Cd == Id_Cd && cvp.Id_Cte == Id_Cte && cvp.Id_Vap == Id_Vap
                select cvp).ToList();
            
            if (CapValProyecto.Count()>0) {
                rv.UtilidadRemanente = (double) CapValProyecto[0].Vap_UtilidadRemanente;
                rv.ValorPresenteNeto = (double)CapValProyecto[0].Vap_ValorPresenteNeto;            
            } else {
                rv.UtilidadRemanente = 0;
                rv.ValorPresenteNeto= 0;            
            }
            
            return rv;
        }
        
        /// <summary>
        /// Regresa la consulta de la entidad [CapValProyectoDet] mediante la llave [Id_Vap].
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuacion</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable<CapValProyectoDet>. Resultado de la consulta a la entidad [CapValProyectoDet]</returns>
        public IEnumerable<CapValProyectoDet> ConsultarPorCapValProyectoId(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            IEnumerable<CapValProyectoDet> resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var dets = (from d in ctx.CapValProyectoDets
                        where d.Id_Emp == idEmp && d.Id_Cd == idCd && d.Id_Vap == idVal
                        select d).ToList();
            resultado = dets;
            return resultado;
        }

        /// <summary>
        /// Actualiza los atributos que marcan la autorización del detalle de una valuación.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVap">Identificador de la valuación a la que pertenece este detalle</param>
        /// <param name="idVapDet">Identificador del detalle</param>
        /// <param name="estatus">Estado del detalle</param>
        /// <param name="fechaAutorizacion">Fecha en la que se marcó la actualización del detalle</param>
        /// <param name="idAutorizador">Identificador del usuario que actualizó el detalle</param>
        /// <param name="icdCtx">Contexto de conexión a la base de datos</param>
        public void ActualizarAtributosDeAutorizacion(int idEmp, int idCd, int idVap, int idVapDet, string estatus, DateTime fechaAutorizacion, int idAutorizador, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var dets = from d in ctx.CapValProyectoDets
                       where d.Id_Emp == idEmp && d.Id_Cd == idCd && d.Id_Vap == idVap && d.Id_VapDet == idVapDet
                       select d;
            if (dets.Count() > 0)
            {
                var det = dets.First();
                det.Det_Estatus = estatus;
                det.Det_FecAut = fechaAutorizacion;
                det.Det_Autorizo = idAutorizador;
            }
        }
    }
}
