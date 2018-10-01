using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmPropuestaTecnica
    {
        public IEnumerable<CrmPropuestaTecnica> ConsultarDetallePropuestaTecnica(int idEmp, int idCd, int idCte, int idRik, int idVal, string cadenaConexionEF)
        {
            IEnumerable<CrmPropuestaTecnica> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                resultado = (from pt in ctx.CrmPropuestaTecnicas
                             join cop in ctx.CrmOportunidadesProductos
                             on new { 
                                 Id_Emp = pt.Id_Emp, Id_Cd = pt.Id_Cd, Id_Cte = pt.Id_Cte, Id_Prd=pt.Id_Prd 
                             } equals new { 
                                 Id_Emp = cop.Id_Emp, Id_Cd = cop.Id_Cd, Id_Cte = cop.Id_Cte, Id_Prd=cop.Id_Prd 
                             }
                             join cvo in ctx.CrmValuacionOportunidades
                             on new { 
                                 Id_Emp = cop.Id_Emp, Id_Cd = cop.Id_Cd, Id_Op = cop.Id_Op, Id_Cte = cop.Id_Cte, Id_Rik = cop.Id_Rik, Id_Val=pt.Id_Val 
                             } equals new { 
                                 Id_Emp = cvo.Id_Emp, Id_Cd = cvo.Id_Cd, Id_Op = cvo.Id_Op, Id_Cte = cvo.Id_Cte, Id_Rik = cvo.Id_Rik, Id_Val=cvo.Id_Val 
                             }
                             where cvo.Id_Emp==idEmp && cvo.Id_Cd==idCd && cvo.Id_Cte==idCte && cvo.Id_Rik==idRik && cvo.Id_Val==idVal
                             select pt).ToList().Select(cpt => { cpt.CatProductoSerializable = cpt.CatProducto; return cpt; }).ToList();
            }
            return resultado;
        }

        public IEnumerable<CrmPropuestaTecnica> ConsultarDetallePropuestaTecnica(int idEmp, int idCd, int idCte, int idRik, int idVal, ICD_Contexto icdCtx)
        {
            IEnumerable<CrmPropuestaTecnica> resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            resultado = (from pt in ctx.CrmPropuestaTecnicas
                         join cop in ctx.CrmOportunidadesProductos
                         on new { 
                             Id_Emp = pt.Id_Emp, Id_Cd = pt.Id_Cd, Id_Cte = pt.Id_Cte, Id_Prd = pt.Id_Prd 
                         } equals new { 
                             Id_Emp = cop.Id_Emp, Id_Cd = cop.Id_Cd, Id_Cte = cop.Id_Cte, Id_Prd = cop.Id_Prd 
                         }
                         join cvo in ctx.CrmValuacionOportunidades
                         on new { 
                             Id_Emp = cop.Id_Emp, Id_Cd = cop.Id_Cd, Id_Op = cop.Id_Op, Id_Cte = cop.Id_Cte, Id_Rik = cop.Id_Rik, Id_Val = pt.Id_Val 
                         } equals new { 
                             Id_Emp = cvo.Id_Emp, Id_Cd = cvo.Id_Cd, Id_Op = cvo.Id_Op, Id_Cte = cvo.Id_Cte, Id_Rik = cvo.Id_Rik, Id_Val = cvo.Id_Val 
                         }
                         where 
                            cvo.Id_Emp == idEmp && cvo.Id_Cd == idCd && cvo.Id_Cte == idCte && cvo.Id_Rik == idRik && cvo.Id_Val == idVal
                         select pt).ToList().Select(cpt => { cpt.CatProductoSerializable = cpt.CatProducto; return cpt; }).ToList();
            return resultado;
        }

        /// <summary>
        /// Actualiza los datos del detalle de una propuesta tecnica
        /// </summary>
        /// <param name="detalle">Conjunto de registros a actualizar en la tabla CrmPropuestaTecnica</param>
        /// <param name="contextoDatos">Contexto de la conexión a datos</param>
        public void Actualizar(int idEmp, int idCd, int idCte, int idVal, List<CrmPropuestaTecnica> detalle, ICD_Contexto contextoDatos)
        {
            sianwebmty_gEntities ctx = (contextoDatos as CD_ContextoDefault).Contexto;
            var entradasOriginales = (from pt in ctx.CrmPropuestaTecnicas
                                      where pt.Id_Emp==idEmp && pt.Id_Cd==idCd && pt.Id_Val==idVal && pt.Id_Cte==idCte
                                      select pt).ToList().Select(cpt=>
                                      {
                                          var datosAActualizar=(from d in detalle
                                                               where d.Id_Prd==cpt.Id_Prd
                                                               select d).ToList();
                                          if(datosAActualizar.Count>0)
                                          {
                                              cpt.CPT_ProductoActual=datosAActualizar[0].CPT_ProductoActual;
                                              cpt.CPT_SituacionActual=datosAActualizar[0].CPT_SituacionActual;
                                              cpt.CPT_VentajasKey=datosAActualizar[0].CPT_VentajasKey;
                                              cpt.CPT_RecursoImagenProductoActual=datosAActualizar[0].CPT_RecursoImagenProductoActual;
                                              cpt.CPT_RecursoImagenSolucionKey=datosAActualizar[0].CPT_RecursoImagenSolucionKey;
                                          }
                                          return cpt; 
                                      }).ToList();
        }

        /// <summary>
        /// Inserta una instancia de la entidad [CrmPropuestaTecnica]
        /// </summary>
        /// <param name="datos">Instancia de datos de la entidad [CrmPropuestaTecnica]</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato compatible de Entity Framework</param>
        public void Insertar(CrmPropuestaTecnica datos, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                ctx.CrmPropuestaTecnicas.Add(datos);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Inserta un conjunto de instancias de la entidad [CrmPropuestaTecnica].
        /// </summary>
        /// <param name="propuestaTecnicaDet">Conjunto de instancias de la entidad CrmPropuestaTecnica</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato compatible de Entity Framework</param>
        public void Insertar(IEnumerable<CrmPropuestaTecnica> propuestaTecnicaDet, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                ctx.CrmPropuestaTecnicas.AddRange(propuestaTecnicaDet);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Inserta un conjunto de instancias de la entidad [CrmPropuestaTecnica]. Esta versión es para necesidades transaccionales.
        /// </summary>
        /// <param name="propuestaTecnicaDet">Conjunto de instancias de la entidad CrmPropuestaTecnica</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio</param>
        public void Insertar(IEnumerable<CrmPropuestaTecnica> propuestaTecnicaDet, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((CD_ContextoDefault)icdCtx).Contexto;
            ctx.CrmPropuestaTecnicas.AddRange(propuestaTecnicaDet);
        }
    }
}
