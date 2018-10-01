using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatAplicacion
    {
        public void Lista(Aplicacion aplicacion, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { aplicacion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    aplicacion = new Aplicacion();
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_Apl"));
                    aplicacion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    aplicacion.Id_Area = (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    aplicacion.Apl_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Apl_Descripcion"));
                    aplicacion.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    aplicacion.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    aplicacion.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));
                    aplicacion.Apl_Limpieza = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Limpieza")));
                    aplicacion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Activo")));
                    if (Convert.ToBoolean(aplicacion.Estatus))
                    {
                        aplicacion.EstatusStr = "Activo";
                    }
                    else
                    {
                        aplicacion.EstatusStr = "Inactivo";
                    }
                    List.Add(aplicacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicacionesSegmento_Consultar(int Id_Emp, int Id_Seg, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Seg" };
                object[] Valores = { Id_Emp, Id_Seg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMSegmentoAplicaciones_Conssultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Aplicacion aplicacion = new Aplicacion();
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_emp"));
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_Apl"));
                    aplicacion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Descripcion"))))
                        aplicacion.Apl_Descripcion = null;
                    else
                        aplicacion.Apl_Descripcion = dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Potencial"))))
                        aplicacion.Apl_Potencial = 0;
                    else
                        aplicacion.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Limpieza"))))
                        aplicacion.Apl_Limpieza = false;
                    else
                        aplicacion.Apl_Limpieza = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Limpieza")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Activo"))))
                        aplicacion.Estatus = false;
                    else
                        aplicacion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Activo")));

                    if (aplicacion.Estatus)
                    {
                        aplicacion.EstatusStr = "Activo";
                    }
                    else
                    {
                        aplicacion.EstatusStr = "Inactivo";
                    }
                    List.Add(aplicacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Apl",
                                          "@Id_Sol",
                                          "@Apl_Descripcion", 
                                          "@Apl_Potencial", 
                                          "@Apl_Limpieza",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       aplicacion.Id_Emp,
                                       aplicacion.Id_Apl,
                                       aplicacion.Id_Sol,
                                       aplicacion.Apl_Descripcion,
                                       aplicacion.Apl_Potencial,
                                       aplicacion.Apl_Limpieza,
                                       aplicacion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Apl",
                                          "@Id_Sol",
                                          "@Apl_Descripcion", 
                                          "@Apl_Potencial", 
                                          "@Apl_Limpieza",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       aplicacion.Id_Emp, 
                                       aplicacion.Id_Apl,
                                       aplicacion.Id_Sol,
                                       aplicacion.Apl_Descripcion,
                                       aplicacion.Apl_Potencial,
                                       aplicacion.Apl_Limpieza,
                                       aplicacion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta las aplicaciones disponibles para un proyecto
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idSol"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public IEnumerable<CatAplicacion> ConsultarPorEmpresaYSolucion(int idEmp, int idSol, string conexion)
        {
            IEnumerable<CatAplicacion> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                var res = (from ca in ctx.CatAplicacions
                           where ca.Id_Emp == idEmp && ca.Id_Sol == idSol && ca.Apl_Activo == true
                           select ca).ToList();
                result = res;
            }
            return result;
        }

        public IEnumerable<CatAplicacion> ConsultarPorEmpresaSolucionSegmento(int idEmp, int idCd, int idSol, int idSeg, int idOp, string conexion)
        {
            IEnumerable<CatAplicacion> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                var res = (from ca in ctx.CatAplicacions
                           join est in ctx.CRMEstructuraSegmentoProyectoes
                           on new { Id_Emp = ca.Id_Emp, Id_Apl = ca.Id_Apl } equals new { Id_Emp = est.Id_Emp, Id_Apl = est.Id_Aplic } into esps
                           from esp in esps.DefaultIfEmpty()
                           where ca.Id_Emp == idEmp && ca.Id_Sol == idSol && ca.Apl_Activo == true && esp.Id_Cd == idCd && esp.Id_Op == idOp
                           select new { CatAplicacion = ca, Porcentaje = esp.Porcentaje }).ToList();
                var ret = res.Select(ca => { ca.CatAplicacion.Porcentaje = ca.Porcentaje != null ? ca.Porcentaje.Value : 0.0D; return ca.CatAplicacion; }).ToList();
                result = ret;
            }
            return result;
        }

        /// <summary>
        /// Consulta y devuelve todas las aplicaciones usadas en proyectos activos.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idCte"></param>
        /// <param name="idUen"></param>
        /// <param name="idSeg"></param>
        /// <param name="idArea"></param>
        /// <param name="idSol"></param>
        /// <param name="conexionEF"></param>
        /// <returns></returns>
        public IEnumerable<CatAplicacion> ConsultarAplicacionesEnProyectos(int idEmp, int idCd, int idCte, int idUen, int idSeg, int idArea, int idSol, string conexionEF)
        {
            IEnumerable<CatAplicacion> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var aplicaciones = (from p in ctx.CrmOportunidades
                                    join a in ctx.CrmOportunidadesAplicacions
                                    on new { Id_Emp = p.Id_Emp, Id_Cd = p.Id_Cd, Id_Op = p.Id_Op } equals new { Id_Emp = a.Id_Emp, Id_Cd = a.Id_Cd, Id_Op = a.Id_Op }
                                    join apl in ctx.CatAplicacions
                                    on new { Id_Emp = a.Id_Emp, Id_Apl = a.Id_Apl } equals new { Id_Emp = apl.Id_Emp, Id_Apl = apl.Id_Apl }
                                    //where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte && p.Id_Uen == idUen && p.Id_Seg == idSeg && p.ID_Area == idArea && p.Id_Sol == idSol && p.Cierre == null && p.Cancelacion == null // SE CAMBIA Cancelacion x FechaCancelacion
                                    where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte && p.Id_Uen == idUen && p.Id_Seg == idSeg && p.ID_Area == idArea && p.Id_Sol == idSol && p.Cierre == null && p.FechaCancelacion == null
                                    select apl).Distinct().ToList();
                result = aplicaciones;
            }
            return result;
        }

        /// <summary>
        /// Consulta las aplicaciones asociadas a un proyecto activo
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idRik"></param>
        /// <param name="idCte"></param>
        /// <param name="idOp"></param>
        /// <param name="conexionEF"></param>
        /// <returns>IEnumerable<CatAplicacion></returns>
        public IEnumerable<CatAplicacion> ConsultarAplicacionesEnProyecto(int idEmp, int idCd, int idRik, int idCte, int idOp, string conexionEF)
        {
            IEnumerable<CatAplicacion> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var apps = (from oa in ctx.CrmOportunidadesAplicacions
                            join a in ctx.CatAplicacions
                            on new { Id_Emp = oa.Id_Emp, Id_Apl = oa.Id_Apl } equals new { Id_Emp = a.Id_Emp, Id_Apl = a.Id_Apl }
                            join p in ctx.CrmOportunidades
                            on new { Id_Emp = oa.Id_Emp, Id_Cd = oa.Id_Cd, Id_Op = oa.Id_Op } equals new { Id_Emp = p.Id_Emp, Id_Cd = p.Id_Cd, Id_Op = p.Id_Op }
                            where oa.Id_Emp == idEmp && oa.Id_Cd == idCd && oa.Id_Op == idOp && p.Id_Cte == idCte && p.Cierre == null && p.Cancelacion == null
                            select a).ToList();
                result = apps;
            }
            return result;
        }

        /// <summary>
        /// Consulta las aplicaciones asociadas a un proyecto activo
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idRik"></param>
        /// <param name="idCte"></param>
        /// <param name="idOp"></param>
        /// <param name="conexionEF"></param>
        /// <returns>IEnumerable<CatAplicacion></returns>
        public IEnumerable<CatAplicacion> ConsultarAplicacionesEnProyecto(int idEmp, int idCd, int idRik, int idCte, int idOp, ICD_Contexto icdCtx)
        {
            IEnumerable<CatAplicacion> result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;

            var apps = (from oa in ctx.CrmOportunidadesAplicacions
                        join a in ctx.CatAplicacions
                        on new { Id_Emp = oa.Id_Emp, Id_Apl = oa.Id_Apl } equals new { Id_Emp = a.Id_Emp, Id_Apl = a.Id_Apl }
                        join p in ctx.CrmOportunidades
                        on new { Id_Emp = oa.Id_Emp, Id_Cd = oa.Id_Cd, Id_Op = oa.Id_Op } equals new { Id_Emp = p.Id_Emp, Id_Cd = p.Id_Cd, Id_Op = p.Id_Op }
                        where oa.Id_Emp == idEmp && oa.Id_Cd == idCd && oa.Id_Op == idOp && p.Id_Cte == idCte && p.Cierre == null && p.Cancelacion == null
                        select a).ToList();
            result = apps;
            return result;
        }

        public CatAplicacion Consultar(int idEmp, int idApl, string cadenaConexionEF)
        {
            CatAplicacion resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var apls = (from a in ctx.CatAplicacions
                            where a.Id_Emp == idEmp && a.Id_Apl == idApl
                            select a).ToList();
                if (apls.Count > 0)
                {
                    resultado = apls[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve el resultado de la consulta al repositorio CatAplicacion
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idApl">Identificador de la aplicación</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CatAplicacion</returns>
        public CatAplicacion Consultar(int idEmp, int idApl, ICD_Contexto icdCtx)
        {
            CatAplicacion resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var apls = (from a in ctx.CatAplicacions
                        where a.Id_Emp == idEmp && a.Id_Apl == idApl
                        select a).ToList();
            if (apls.Count > 0)
            {
                resultado = apls[0];
            }
            return resultado;
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el repositorio CatAplicacion
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CatAplicacion]</returns>
        public IEnumerable<CatAplicacion> ConsultarTodas(int idEmp, ICD_Contexto icdCtx)
        {
            var ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var aplicaciones = from ap in ctx.CatAplicacions
                               where ap.Id_Emp==idEmp
                               select ap;
            return aplicaciones;
        }
    }
}
