using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CrmProspecto
    {
        public IEnumerable<CrmProspecto> ObtenerProspectos(int idEmp, int idCd, int idRik, string conexion)
        {
            List<CrmProspecto> res = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                res = (from p in ctx.CrmProspectoes
                       where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik
                       select p).ToList();
                res.ForEach(p => {
                    p.Cte_Calle = p.CatCliente.Cte_Calle; 
                    p.Cte_Contacto = p.CatCliente.Cte_Contacto; 
                    p.Cte_Email = p.CatCliente.Cte_Email; 
                    p.Cte_NomComercial = p.CatCliente.Cte_NomComercial; 
                    p.Cte_Telefono = p.CatCliente.Cte_NomComercial;
                    p.Cte_Rfc = p.CatCliente.Cte_Rfc;
                });
                //res = ctx.spCrmProspecto_Consultar(idEmp, idCd, idRik).ToList();
            }
            return res;
        }

        public IEnumerable<CrmProspecto> ObtenerTodosProspectos(int idEmp, int idCd, string conexion)
        {
            List<CrmProspecto> res = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                res = (from p in ctx.CrmProspectoes
                       where p.Id_Emp == idEmp && p.Id_Cd == idCd
                       select p).ToList();
                res.ForEach(p =>
                {
                    p.Cte_Calle = p.CatCliente.Cte_Calle;
                    p.Cte_Contacto = p.CatCliente.Cte_Contacto;
                    p.Cte_Email = p.CatCliente.Cte_Email;
                    p.Cte_NomComercial = p.CatCliente.Cte_NomComercial;
                    p.Cte_Telefono = p.CatCliente.Cte_NomComercial;
                    p.Cte_Rfc = p.CatCliente.Cte_Rfc;
                });
                //res = ctx.spCrmProspecto_Consultar(idEmp, idCd, idRik).ToList();
            }
            return res;
        }

        /// <summary>
        /// Devuelve la consulta de todos los prospectos.
        /// </summary>
        /// <param name="idEmp">Identificador de empresa</param>
        /// <param name="idCd">Identificador de centro de distribución</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable<CrmProspecto></returns>
        public IEnumerable<CrmProspecto> ObtenerTodosProspectos(int idEmp, int idCd, ICD_Contexto icdCtx)
        {
            List<CrmProspecto> res = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            res = (from p in ctx.CrmProspectoes
                   where p.Id_Emp == idEmp && p.Id_Cd == idCd
                   select p).ToList();
            res.ForEach(p =>
            {
                p.Cte_Calle = p.CatCliente.Cte_Calle;
                p.Cte_Contacto = p.CatCliente.Cte_Contacto;
                p.Cte_Email = p.CatCliente.Cte_Email;
                p.Cte_NomComercial = p.CatCliente.Cte_NomComercial;
                p.Cte_Telefono = p.CatCliente.Cte_NomComercial;
                p.Cte_Rfc = p.CatCliente.Cte_Rfc;
            });
            return res;
        }

        public IEnumerable<CrmProspecto> ObtenerProspectosComoClientes(int idEmp, int idCd, string conexion)
        {
            List<CrmProspecto> res = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                res = (from p in ctx.CrmProspectoes
                       where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_CrmTipoCliente==2
                       select p).ToList();
                res.ForEach(p =>
                {
                    p.Cte_Calle = p.CatCliente.Cte_Calle;
                    p.Cte_Contacto = p.CatCliente.Cte_Contacto;
                    p.Cte_Email = p.CatCliente.Cte_Email;
                    p.Cte_NomComercial = p.CatCliente.Cte_NomComercial;
                    p.Cte_Telefono = p.CatCliente.Cte_NomComercial;
                });

                var clientes = from c in ctx.CatClientes
                               where c.Id_Emp==idEmp && c.Id_Cd==idCd
                               select c;
                //res = ctx.spCrmProspecto_Consultar(idEmp, idCd, idRik).ToList();
            }
            return res;
        }

        public CrmProspecto ObtenerProspecto(int idEmp, int idCd, int idRik, int idCrmProspecto, string conexion)
        {
            CrmProspecto result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                var res = (from p in ctx.CrmProspectoes
                           where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_CrmProspecto == idCrmProspecto
                           select p).ToList();
                if (res.Count > 0)
                {
                    result = res[0];
                    result.Cte_Calle = result.CatCliente.Cte_Calle;
                    result.Cte_Contacto = result.CatCliente.Cte_Contacto;
                    result.Cte_Email = result.CatCliente.Cte_Email;
                    result.Cte_NomComercial = result.CatCliente.Cte_NomComercial;
                    result.Cte_Telefono = result.CatCliente.Cte_Telefono;
                    result.Territorios = result.CatCliente.CatClienteDets.Select(ccd => ccd.Id_Ter.Value).ToArray();
                    result.Cte_Rfc = result.CatCliente.Cte_Rfc;
                }
            }
            return result;
        }

        //protected CrmProspecto ObtenerProspecto(int idEmp, int idCd, int idRik, int idCrmProspecto, sianwebmty_gEntities ctx)
        //{
        //    CrmProspecto result = null;
        //    var res = (from p in ctx.CrmProspectoes
        //               where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_CrmProspecto == idCrmProspecto
        //               select p).ToList();
        //    if (res.Count > 0)
        //    {
        //        result = res[0];
        //    }
        //    return result;
        //}

        public CrmProspecto InsertarProspecto(CrmProspecto prospecto, string conexion)
        {
            CrmProspecto result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                result = ctx.CrmProspectoes.Add(prospecto);
                ctx.SaveChanges();
            }
            return result;
        }

        public CrmProspecto InsertarProspecto(CrmProspecto prospecto, ICD_Contexto icdCtx)
        {
            CrmProspecto result = null;
            sianwebmty_gEntities ctx=((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            result = ctx.CrmProspectoes.Add(prospecto);
            return result;
        }

        public void ActualizarProspecto(int idEmp, int idCd, CrmProspecto prospecto, string conexion)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexion))
            {
                var ctes = (from c in ctx.CatClientes
                            where 
                            c.Id_Emp == prospecto.Id_Emp && 
                            c.Id_Cd == prospecto.Id_Cd && 
                            //c.Id_Rik ==prospecto.Id_Rik && 
                            c.Id_Cte == prospecto.Id_Cte 
                            select c).ToList();
                if (ctes.Count > 0)
                {
                    ctes[0].Cte_NomComercial = prospecto.Cte_NomComercial;
                    ctes[0].Cte_Contacto = prospecto.Cte_Contacto;
                    ctes[0].Cte_Email = prospecto.Cte_Email;
                    ctes[0].Cte_Calle = prospecto.Cte_Calle;
                    ctes[0].Cte_Telefono = prospecto.Cte_Telefono;
                    ctes[0].Cte_Rfc = prospecto.Cte_Rfc;

                    var prospectos = (from p in ctx.CrmProspectoes
                                      where p.Id_Emp == prospecto.Id_Emp && p.Id_Cd == prospecto.Id_Cd && p.Id_CrmProspecto == prospecto.Id_CrmProspecto && p.Id_Cte == prospecto.Id_Cte && p.Id_Rik == prospecto.Id_Rik
                                      select p).ToList();
                    if (prospectos.Count > 0)
                    {
                        prospectos[0].Id_Ter_Temporal = prospecto.Id_Ter_Temporal;
                    }
                    
                    ctx.SaveChanges();
                }
            }
        }

        public int Eliminar_Prospecto(int idCrmProspecto, int idEmp, int idCd, int idRik, int idCte, string conexionEF, string conexion)
        {
            int iEstatus = 0;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                bool Eliminar = true;
                var CrmOpo = (from O in ctx.CrmOportunidades
                                  where O.Id_Emp == idEmp && O.Id_Cd == idCd && O.Id_Cte == idCte 
                                  select O).ToList();

                if (CrmOpo.Count > 0)
                {
                    // Si existen Oportunidade no eliminar.
                    Eliminar = false;
                }
                if (Eliminar)
                {
                    var prospectos = (from p in ctx.CrmProspectoes
                                      where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_CrmProspecto == idCrmProspecto && p.Id_Cte == idCte
                                      select p).ToList();

                    if (prospectos.Count > 0)
                    {
                        try
                        {
                            ctx.CrmProspectoes.Remove(prospectos[0]);
                            // CRM - No van a eliminar los clientes
                            //CD_CatCliente cdCatCliente = new CD_CatCliente();
                            //cdCatCliente.EliminarCliente(idEmp, idCd, idCte, ctx);
                            ctx.SaveChanges();
                            iEstatus = 1;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            return iEstatus;
        }

        public void EliminarProspecto(int idCrmProspecto, int idEmp, int idCd, int idRik, int idCte, string conexionEF, string conexion)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                bool Eliminar = true;
                var CrmOpo = (from O in ctx.CrmOportunidades
                                  where O.Id_Emp == idEmp && O.Id_Cd == idCd && O.Id_Cte == idCte 
                                  select O).ToList();

                if (CrmOpo.Count > 0)
                {
                    // Si existen Oportunidade no eliminar.
                    Eliminar = false;
                }

                if (Eliminar)
                {
                    var prospectos = (from p in ctx.CrmProspectoes
                                      where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_CrmProspecto == idCrmProspecto && p.Id_Cte == idCte
                                      select p).ToList();

                    if (prospectos.Count > 0)
                    {
                        try
                        {
                            ctx.CrmProspectoes.Remove(prospectos[0]);
                            // CRM - No van a eliminar los clientes
                            //CD_CatCliente cdCatCliente = new CD_CatCliente();
                            //cdCatCliente.EliminarCliente(idEmp, idCd, idCte, ctx);
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

        public int ConsultarTipoCliente(int idEmp, int idCd, int idRik, int idCte, string conexionEF)
        {
            int result = 2;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var prospectos = (from p in ctx.CrmProspectoes
                                  where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_Cte == idCte
                                  select p).ToList();
                if (prospectos.Count > 0)
                {
                    result = prospectos[0].Id_CrmTipoCliente;
                }
            }
            return result;
        }

        /// <summary>
        /// Devuelve el resultado de la consulta al repositorio CrmTipoCliente
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>int</returns>
        public int ConsultarTipoCliente(int idEmp, int idCd, int idRik, int idCte, ICD_Contexto icdCtx)
        {
            int result = 2;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var prospectos = (from p in ctx.CrmProspectoes
                              where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Rik == idRik && p.Id_Cte == idCte
                              select p).ToList();
            if (prospectos.Count > 0)
            {
                result = prospectos[0].Id_CrmTipoCliente;
            }
            return result;
        }

        /// <summary>
        /// Actualiza el atributo [Id_CrmTipoCliente] del repositorio de la entidad [CrmProspecto]
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución en el que se encuentra asociado el cliente idCte</param>
        /// <param name="idCte">Identificador del cliente de interés</param>
        /// <param name="val">Valor a actualiza del atributo [Id_CrmTipoCliente]</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        public void ActualizarCampoTipoCliente(int idEmp, int idCd, int idCte, int val, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var prospectos = (from p in ctx.CrmProspectoes
                                  where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte
                                  select p).ToList();
                if (prospectos.Count > 0)
                {
                    prospectos[0].Id_CrmTipoCliente = val;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Actualiza el atributo [Id_CrmTipoCliente] del repositorio de la entidad [CrmProspecto]
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución en el que se encuentra asociado el cliente idCte</param>
        /// <param name="idCte">Identificador del cliente de interés</param>
        /// <param name="val">Valor a actualiza del atributo [Id_CrmTipoCliente]</param>
        /// <param name="conexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        public void ActualizarCampoTipoCliente(int idEmp, int idCd, int idCte, int val, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)(icdCtx)).Contexto;
            var prospectos = (from p in ctx.CrmProspectoes
                              where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Cte == idCte
                              select p).ToList();
            if (prospectos.Count > 0)
            {
                prospectos[0].Id_CrmTipoCliente = val;
            }
        }

        public int ConsultarById(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Cte, string Conexion)
        {
            int Result = 0;
            try
            {
                CapaEntidad.Crm_Prospecto CRMp = new CapaEntidad.Crm_Prospecto();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik", "@Id_Cte"};
                object[] Valores = { Id_Emp, Id_Cd, Id_Rik, Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmProspecto_Consulta", ref dr, Parametros, Valores);

                while (dr.Read()) {
                    Result = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CrmProspecto")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                Result = -1;
            }

            return Result; 
        }
        //
    }
}
