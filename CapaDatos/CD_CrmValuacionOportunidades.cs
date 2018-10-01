using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaModelo;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CrmValuacionOportunidades
    {

        public eValuacionProyecto ObtenerPorValuacion_Detallado(int Id_Emp, int Id_Cd, int Id_Op, int Id_Val, ref int verificador, string Conexion,
            double CuentasPorCobrar_,
            double Inventario_,
            double InversionActivosFijos_,
            double FinanciamientoProveedores_,
            double TasaIncrementoCostoCapital_,
            double VigenciaACYS_,
            double FleteCD_,
            double GastosServirCliente_,
            double ISRyPTU_,
            double Cetes_,
            double ManoObraProyectos_)
        {

            eValuacionProyecto Res = new eValuacionProyecto();
            List<eResultadosValuacionDetallado> lst_ValuacionProyecto = new List<eResultadosValuacionDetallado>();
            List<eResultadoValuacionFlujo> lst_eResultadoValuacionFlujo= new List<eResultadoValuacionFlujo>();
                 
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@OpcionResult",
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Op",
                                        "@Id_Val",
                                                                                
                                        "@CuentasPorCobrar_",
                                        "@Inventario_",
                                        "@InversionActivosFijos_",
                                        "@FinanciamientoProveedores_",
                                        "@TasaIncrementoCostoCapital_",
                                        "@VigenciaACYS_",
                                        "@FleteCD_",
                                        "@GastosServirCliente_",
                                        "@ISRyPTU_",
                                        "@Cetes_",
                                        "@ManoObraProyectos_" };

                object[] Valores = { 2, Id_Emp, Id_Cd, Id_Op, Id_Val, 
                                       CuentasPorCobrar_,
                                       Inventario_,
                                        InversionActivosFijos_,
                                        FinanciamientoProveedores_,
                                        TasaIncrementoCostoCapital_,
                                        VigenciaACYS_,
                                        FleteCD_,
                                        GastosServirCliente_,
                                        ISRyPTU_,
                                        Cetes_,
                                        ManoObraProyectos_ };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ObtenerProyectosPorRik_CalcularResultado", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    var Obj = new eResultadosValuacionDetallado();
                    Obj.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Folio")));
                    Obj.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Tipo")));
                    Obj.Titulo = dr.GetValue(dr.GetOrdinal("Titulo")).ToString();
                    Obj.Factor = dr.GetValue(dr.GetOrdinal("Factor")).ToString();
                    Obj.Monto = dr.GetValue(dr.GetOrdinal("Monto")).ToString();
                    lst_ValuacionProyecto.Add(Obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst_ValuacionProyecto = null;
                //throw ex;
                //Res = null;            
            }


            try {                
            
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@OpcionResult",
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Op",
                                        "@Id_Val",

                                        "@CuentasPorCobrar_",
                                        "@Inventario_",
                                        "@InversionActivosFijos_",
                                        "@FinanciamientoProveedores_",
                                        "@TasaIncrementoCostoCapital_",
                                        "@VigenciaACYS_",
                                        "@FleteCD_",
                                        "@GastosServirCliente_",
                                        "@ISRyPTU_",
                                        "@Cetes_",
                                        "@ManoObraProyectos_" };

                object[] Valores = { 5, Id_Emp, Id_Cd, Id_Op, Id_Val,
                                       CuentasPorCobrar_,
                                       Inventario_,
                                        InversionActivosFijos_,
                                        FinanciamientoProveedores_,
                                        TasaIncrementoCostoCapital_,
                                        VigenciaACYS_,
                                        FleteCD_,
                                        GastosServirCliente_,
                                        ISRyPTU_,
                                        Cetes_,
                                        ManoObraProyectos_ };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ObtenerProyectosPorRik_CalcularResultado", ref dr2, Parametros, Valores);
                while (dr2.Read())
                {
                    var Obj = new eResultadoValuacionFlujo();
                    Obj.Folio = Convert.ToInt32(dr2.GetValue(dr2.GetOrdinal("Folio")));
                    Obj.Anio = Convert.ToInt32(dr2.GetValue(dr2.GetOrdinal("Anio")));
                    Obj.FlujoAnual = dr2.GetValue(dr2.GetOrdinal("FlujoAnual")).ToString();
                    Obj.VPFlujos = dr2.GetValue(dr2.GetOrdinal("VPFlujos")).ToString(); 
                    lst_eResultadoValuacionFlujo.Add(Obj);
                }
                
                dr2.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst_eResultadoValuacionFlujo = null;
                //throw ex;
                //Res = null;            
            }

            Res.ValuacionProyecto = lst_ValuacionProyecto;
            Res.ValuacionFlujo = lst_eResultadoValuacionFlujo;
            return Res;
        }

        /// <summary>
        /// Persiste la asociación entre proyectos y una valuación
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idOps"></param>
        /// <param name="conexionEF"></param>
        public void Insertar(int idEmp, int idCd, int idCte, int idVal, int idRik, int[] idOps, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                foreach (var idOp in idOps)
                {
                    ctx.CrmValuacionOportunidades.Add(new CrmValuacionOportunidade() { Id_Emp=idEmp, Id_Cd=idCd, Id_Cte=idCte, Id_Val=idVal, Id_Rik=idRik, Id_Op=idOp });
                }
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Elimina todos los proyectos asociados a una valuación
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idOps">Conjunto de proyectos a desasociar</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos, en formato compatible con EF</param>
        public void Eliminar(int idEmp, int idCd, int idCte, int idVal, int idRik, int[] idOps, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var oportunidades = (from op in ctx.CrmValuacionOportunidades
                                     where idOps.Contains(op.Id_Op) && op.Id_Emp == idEmp && op.Id_Cd == idCd && op.Id_Cte == idCte && op.Id_Val == idVal && op.Id_Rik == idRik
                                     select op).ToList();
                if (oportunidades.Count > 0)
                {
                    ctx.CrmValuacionOportunidades.RemoveRange(oportunidades);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Elimina todos los proyectos asociados a una valuación
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos, en formato compatible con EF</param>
        public void Eliminar(int idEmp, int idCd, int idCte, int idVal, int idRik, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var oportunidades = (from op in ctx.CrmValuacionOportunidades
                                     where op.Id_Emp == idEmp && op.Id_Cd == idCd && op.Id_Cte == idCte && op.Id_Val == idVal && op.Id_Rik == idRik
                                     select op).ToList();
                if (oportunidades.Count > 0)
                {
                    ctx.CrmValuacionOportunidades.RemoveRange(oportunidades);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Elimina todos los proyectos asociados a una valuación. Esta versión no promueve las operaciones en la base de datos, con la finalidad de controlar la operación como una unidad transaccional.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio de datos</param>
        public void Eliminar(int idEmp, int idCd, int idCte, int idVal, int idRik, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var oportunidades = (from op in ctx.CrmValuacionOportunidades
                                 where op.Id_Emp == idEmp && op.Id_Cd == idCd && op.Id_Cte == idCte && op.Id_Val == idVal && op.Id_Rik == idRik
                                 select op).ToList();
            if (oportunidades.Count > 0)
            {
                ctx.CrmValuacionOportunidades.RemoveRange(oportunidades);
            }
        }

        /// <summary>
        /// Elimina todos los proyectos asociados a una valuación. Esta versión no promueve las operaciones en la base de datos, con la finalidad de controlar la operación como una unidad transaccional.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="ctx">Contexto de operaciones en la fuente de datos</param>
        internal void Eliminar(int idEmp, int idCd, int idCte, int idVal, int idRik, sianwebmty_gEntities ctx)
        {
            var oportunidades = (from op in ctx.CrmValuacionOportunidades
                                 where op.Id_Emp == idEmp && op.Id_Cd == idCd && op.Id_Cte == idCte && op.Id_Val == idVal && op.Id_Rik == idRik
                                 select op).ToList();
            if (oportunidades.Count > 0)
            {
                ctx.CrmValuacionOportunidades.RemoveRange(oportunidades);
            }
        }

        /// <summary>
        /// Devuelve el conjunto de proyectos asociados a una valuación.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idCte">Cliente del centro de distribución idCd</param>
        /// <param name="idRik">Representante que maneja la cuenta idCte</param>
        /// <param name="idValuacion">Valuación del cliente idCte</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CrmValuacionOportunidade[]</returns>
        public IEnumerable<CrmValuacionOportunidade> ConsultarPorValuacion(int idEmp, int idCd, int idCte, int idRik, int idValuacion, string conexionEF)
        {
            IEnumerable<CrmValuacionOportunidade> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var proyectos = (from vp in ctx.CrmValuacionOportunidades
                                 where vp.Id_Emp == idEmp && vp.Id_Cd == idCd && vp.Id_Cte == idCte && vp.Id_Rik == idRik && vp.Id_Val == idValuacion
                                 select vp).ToList();
                result = proyectos;
            }
            return result;
        }

        public List<eCrmValuacionOportunidades> ConsultarPorValuacion_(int idEmp, int idCd, int idCte, int idRik, int idValuacion, string conexionEF)
        {
            List<eCrmValuacionOportunidades> Lst = new List<eCrmValuacionOportunidades>();
            /*
            IEnumerable<CrmValuacionOportunidade> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var proyectos = (from vp in ctx.CrmValuacionOportunidades
                                 where vp.Id_Emp == idEmp && vp.Id_Cd == idCd && vp.Id_Cte == idCte && vp.Id_Rik == idRik && vp.Id_Val == idValuacion
                                 select vp).ToList();
                result = proyectos;
            }
            */
            return Lst;
        }
        

        /// <summary>
        /// Devuelve el conjunto de proyectos asociados a una valuación.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idCte">Cliente del centro de distribución idCd</param>
        /// <param name="idRik">Representante que maneja la cuenta idCte</param>
        /// <param name="idValuacion">Valuación del cliente idCte</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CrmValuacionOportunidade[]</returns>
        public IEnumerable<CrmValuacionOportunidade> ConsultarPorValuacion(int idEmp, int idCd, int idCte, int idRik, int idValuacion, ICD_Contexto icdCtx)
        {
            IEnumerable<CrmValuacionOportunidade> result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;

            var proyectos = (from vp in ctx.CrmValuacionOportunidades
                             where vp.Id_Emp == idEmp && vp.Id_Cd == idCd && vp.Id_Cte == idCte && vp.Id_Rik == idRik && vp.Id_Val == idValuacion
                             select vp).ToList();
            result = proyectos;

            return result;
        }

        /// <summary>
        /// Devuelve el conjunto de proyectos asociados a una valuación.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idRik">Representante que maneja la cuenta idCte</param>
        /// <param name="idValuacion">Valuación del cliente idCte</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CrmValuacionOportunidade[]</returns>
        public IEnumerable<CrmValuacionOportunidade> ConsultarPorValuacion(int idEmp, int idCd, int idRik, int idValuacion, ICD_Contexto icdCtx)
        {
            IEnumerable<CrmValuacionOportunidade> result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;

            var proyectos = (from vp in ctx.CrmValuacionOportunidades
                             where vp.Id_Emp == idEmp && vp.Id_Cd == idCd && vp.Id_Rik == idRik && vp.Id_Val == idValuacion
                             select vp).ToList();
            result = proyectos;

            return result;
        }

        public CrmValuacionOportunidade ConsultarPorProyecto(int idEmp, int idCd, int idCte, int idOp, string conexionEF)
        {
            CrmValuacionOportunidade result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var vals = (from vo in ctx.CrmValuacionOportunidades
                            where vo.Id_Emp == idEmp && vo.Id_Cd == idCd && vo.Id_Op == idOp && vo.Id_Cte == idCte
                            select vo).ToList().Select(cvo => { cvo.CapValProyectoSerializable = cvo.CapValProyecto; return cvo; }).ToList();
                if (vals.Count > 0)
                {
                    result = vals[0];
                }
            }
            return result;
        }

        /// <summary>
        /// Regresa el conjunto de asociaciones de una valuación y sus proyectos
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución perteneciente a la empresa idEmp</param>
        /// <param name="idCte">Identificador del cliente asociado al centro de distribución idCd</param>
        /// <param name="idOp">Identificador del proyecto asociado al cliente idCte</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CrmValuacionOportunidade</returns>
        public CrmValuacionOportunidade ConsultarPorProyecto(int idEmp, int idCd, int idCte, int idOp, ICD_Contexto icdCtx)
        {
            CrmValuacionOportunidade result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;

            // RFH 
            // TODO: Error

            var vals = (from vo in ctx.CrmValuacionOportunidades
                        where vo.Id_Emp == idEmp && vo.Id_Cd == idCd && vo.Id_Op == idOp && vo.Id_Cte == idCte
                        select vo).ToList().Select(cvo => { 
                            cvo.CapValProyectoSerializable = cvo.CapValProyecto; 
                            return cvo; 
                        }).ToList().Where(vo=>vo.CapValuacionGlobalCliente==null).ToList();

            var val1 = (from vo in ctx.CrmValuacionOportunidades
                        where vo.Id_Emp == idEmp && vo.Id_Cd == idCd && vo.Id_Op == idOp && vo.Id_Cte == idCte
                        select vo).ToList();

            //.Select(cvo =>
            //            {
            //                cvo.CapValProyectoSerializable = cvo.CapValProyecto;
            //                return cvo;
            //            }).ToList().Where(vo => vo.CapValuacionGlobalCliente == null).ToList();
            

            var valsLocal = (from vo in ctx.CrmValuacionOportunidades.Local
                        where vo.Id_Emp == idEmp && vo.Id_Cd == idCd && vo.Id_Op == idOp && vo.Id_Cte == idCte
                        select vo).ToList().Select(cvo => { 
                             cvo.CapValProyectoSerializable = cvo.CapValProyecto; 
                            return cvo; 
                        }).ToList().Where(vo=>vo.CapValuacionGlobalCliente==null).ToList();

            vals.AddRange(valsLocal);
            if (vals.Count > 0)
            {
                result = vals[0];
            }
            return result;
        }

        /// <summary>
        /// Inserta un registro en la entidad [CrmValuacionOportunidades]
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idCte">Identificador del cliente asociado al centro de distribución idCd</param>
        /// <param name="idVal">Identificador de la valuación perteneciente al cliente idCte</param>
        /// <param name="idRik">Identificador del representante creador de la valuación idVal</param>
        /// <param name="idOps">Conjunto de identificadores de proyectos a asociar a la valuación idVal</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void Insertar(int idEmp, int idCd, int idCte, int idVal, int idRik, int[] idOps, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            foreach (var idOp in idOps)
            {
                var entry = new CrmValuacionOportunidade() { Id_Emp = idEmp, Id_Cd = idCd, Id_Cte = idCte, Id_Val = idVal, Id_Rik = idRik, Id_Op = idOp };
                entry.Context = ctx;
                ctx.CrmValuacionOportunidades.Add(entry);
                var proyectos = (from p in ctx.CrmOportunidades
                                 where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte && p.Id_Op == idOp
                                 select p).ToList();
                if (proyectos.Count > 0)
                {
                    var p = proyectos[0];
                    p.CrmOp_EnValuacion = true;
                }
            }
        }

        internal void Insertar(int idEmp, int idCd, int idCte, int idVal, int idRik, int[] idOps, sianwebmty_gEntities ctx)
        {
            foreach (var idOp in idOps)
            {
                ctx.CrmValuacionOportunidades.Add(new CrmValuacionOportunidade() { Id_Emp = idEmp, Id_Cd = idCd, Id_Cte = idCte, Id_Val = idVal, Id_Rik = idRik, Id_Op = idOp });
                var proyectos = (from p in ctx.CrmOportunidades
                                 where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte && p.Id_Op == idOp
                                 select p).ToList();
                if (proyectos.Count > 0)
                {
                    var p = proyectos[0];
                    p.CrmOp_EnValuacion = true;
                }
            }
        }
    }
}
