﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Globalization;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatTerritorios
    {
        public void ObtenerRepPendientesAct(string Conexion, int Id_Emp, int Id_Cd, ref DataTable DT)
        {
            DataSet ds = new DataSet();
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd"
                                      };
                object[] Valores = { 
                                            Id_Emp,
                                            Id_Cd
                                       };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("RepresentantePendientesActualizar", ref ds, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                ds.DataSetName = "RepresentantesDS";
                DataTable D = new DataTable();
                D = ds.Tables[0];
                D.TableName = "Representantes";
                DT = D;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaTerritorios(Territorios territorio, string Conexion, ref List<Territorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { territorio.Id_Emp, territorio.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    territorio.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    territorio.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    territorio.Id_TerNuevo = 0;
                    territorio.Id_TerAnt = (int)dr.GetValue(dr.GetOrdinal("Id_TerAnt"));
                    territorio.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    territorio.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    territorio.Uen_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Uen_Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    territorio.Rik_Nombre = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    territorio.Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Seg"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    territorio.Seg_Nombre = (string)dr.GetValue(dr.GetOrdinal("Seg_Nombre"));
                    territorio.Id_TipoCliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoCliente"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_TipoCliente"));
                    territorio.TipoCliente_Nombre = Convert.IsDBNull((string)dr.GetValue(dr.GetOrdinal("TipoCliente_Nombre"))) ? string.Empty : (string)dr.GetValue(dr.GetOrdinal("TipoCliente_Nombre"));
                    territorio.Id_Local = Convert.IsDBNull((string)dr.GetValue(dr.GetOrdinal("Id_Local"))) ? string.Empty : (string)dr.GetValue(dr.GetOrdinal("Id_Local"));
                    territorio.Id_TipoRepresentante = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoRepresentante"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_TipoRepresentante"));
                    territorio.TipoRepresentante_Nombre = Convert.IsDBNull((string)dr.GetValue(dr.GetOrdinal("TipoRepresentante_Nombre"))) ? string.Empty : (string)dr.GetValue(dr.GetOrdinal("TipoRepresentante_Nombre"));
                    territorio.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ter_Activo")));
                    territorio.Consecutivo = 0;

                    if (Convert.ToBoolean(territorio.Estatus))
                    {
                        territorio.EstatusStr = "Activo";
                    }
                    else
                    {
                        territorio.EstatusStr = "Inactivo";
                    }
                    List.Add(territorio);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTerritorios(Territorios territorio, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
	                                    "@Ter_Nombre", 
	                                    "@Id_Uen", 
	                                    "@Id_Rik", 
	                                    "@Id_Seg",
                                        "@Id_TipoCliente",
                                        "@Id_Local",
                                        "@Id_TipoRepresentante",
	                                    "@Ter_Activo",
                                        "@Cve_Terr"
                                      };
                object[] Valores = { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        territorio.Descripcion,
                                        territorio.Id_Uen == -1 ? (object)null : territorio.Id_Uen,
                                        territorio.Id_Rik == -1 ? (object)null : territorio.Id_Rik,
                                        territorio.Id_Seg == -1 ? (object)null : territorio.Id_Seg,
                                        territorio.Id_TipoCliente == -1 ? (object)null : territorio.Id_TipoCliente,
                                        territorio.Id_Local ==string.Empty ? (object)null : territorio.Id_Local,
                                        territorio.Id_TipoRepresentante == -1 ? (object)null : territorio.Id_TipoRepresentante,
                                        territorio.Estatus,
                                        territorio.Cve_Terr
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTerritorios(Territorios territorio, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
	                                    "@Ter_Nombre", 
	                                    "@Id_Uen", 
	                                    "@Id_Rik", 
	                                    "@Id_Seg",
                                        "@Id_TipoCliente",
                                        "@Id_Local",
                                        "@Id_TipoRepresentante",
	                                    "@Ter_Activo"
                                      };
                object[] Valores = { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        territorio.Descripcion,
                                        territorio.Id_Uen== -1 ? (object)null : territorio.Id_Uen,
                                        territorio.Id_Rik == -1 ? (object)null : territorio.Id_Rik,
                                        territorio.Id_Seg == -1 ? (object)null : territorio.Id_Seg,
                                        territorio.Id_TipoCliente == -1 ? (object)null : territorio.Id_TipoCliente,                                        
                                        territorio.Id_Local == string.Empty ? (object)null : territorio.Id_Local,
                                        territorio.Id_TipoRepresentante == -1 ? (object)null : territorio.Id_TipoRepresentante,
                                        territorio.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTerritoriosActID(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            if (dt.Rows.Count == 0) return;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Ter",
                                        "@Id_TerNuevo",
                                        "@Ter_Nombre", 
                                        "@Id_Uen", 
                                        "@Id_Rik", 
                                        "@Id_Seg",
                                        "@Id_TipoCliente",
                                        "@Id_Local",
                                        "@Id_TipoRepresentante"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] {
                                        dt.Rows[x]["Id_Emp"],
                                        dt.Rows[x]["Id_Cd"],
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Id_TerNuevo"],
                                        dt.Rows[x]["Descripcion"],
                                        dt.Rows[x]["Id_Uen"].ToString() == "-1" ? (object)null : dt.Rows[x]["Id_Uen"],
                                        dt.Rows[x]["Id_Rik"].ToString() == "-1" ? (object)null : dt.Rows[x]["Id_Rik"],
                                        dt.Rows[x]["Id_Seg"].ToString() == "-1" ? (object)null : dt.Rows[x]["Id_Seg"],
                                        dt.Rows[x]["Id_TipoCliente"],
                                        dt.Rows[x]["Id_Local"] == " " ? (object)null : dt.Rows[x]["Id_Local"],
                                        dt.Rows[x]["Id_TipoRepresentante"]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_ModificarActID", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }

        public void ConsultaTerritoriosDet(TerritorioDet territorio, string Conexion, ref DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { territorio.Id_Emp, territorio.Id_Cd, territorio.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Consulta", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] 
                    { 
                        dr.GetValue(dr.GetOrdinal("Det_Anyo")), 
                        dr.GetValue(dr.GetOrdinal("Det_Mes")),
                        ci.TextInfo.ToTitleCase(ci.DateTimeFormat.GetMonthName((int)dr.GetValue(dr.GetOrdinal("Det_Mes")))),
                        dr.GetValue(dr.GetOrdinal("Det_Presupuesto")) 
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
                                        "@Id_TerDet",
	                                    "@Det_Anyo", 
	                                    "@Det_Mes", 
	                                    "@Det_Presupuesto",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        x,
                                        dt.Rows[x]["Anyo"],
                                        dt.Rows[x]["Mes"],
                                        dt.Rows[x]["Presupuesto"],
                                        1
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Insertar", ref verificador, Parametros, Valores);

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
                                        "@Id_TerDet",
	                                    "@Det_Anyo", 
	                                    "@Det_Mes", 
	                                    "@Det_Presupuesto", 
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null; ;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        x,
                                        dt.Rows[x]["Anyo"],
                                        dt.Rows[x]["Mes"],
                                        dt.Rows[x]["Presupuesto"],
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Insertar", ref verificador, Parametros, Valores);

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritorios(ref Territorios catterr, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Consultar", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;

                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Uen_Descripcion = dr.GetValue(dr.GetOrdinal("Uen_Descripcion")).ToString();
                    catterr.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    catterr.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    catterr.Id_Uen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Uen"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    catterr.Id_TipoCliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoCliente"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoCliente")));
                    catterr.Id_TipoRepresentante = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoRepresentante"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoRepresentante")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosCombo(ref Territorios catterr, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikTerr_Combo", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    catterr.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritorio(ref Territorios catterr, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                catterr = new Territorios();
                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    catterr.Descripcion = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 27 Jun 2018
        
        public List<eTerritorio> ObtenerTerritorios_PorRik(int Id_Emp, int Id_Cd, int Id_Rik, string Conexion)
        {
            List<eTerritorio> lst = new List<eTerritorio>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik" };
                object[] Valores = { Id_Emp, 
                                       Id_Cd, 
                                       Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_PorRik", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eTerritorio obj = new eTerritorio();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));

                    obj.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    obj.Id_Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    obj.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));

                    obj.Ter_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ter_Activo")));

                    obj.Id_TipoCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoCliente")));
                    obj.Cve_Terr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cve_Terr")));

                    lst.Add(obj);
                }
                
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                return lst;
            }

            return lst;
        }

        // 28 Jun 2018

        public List<eTerritorio> ObtenerTerritorios_PorClienteYRik(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Rik, string Conexion)
        {
            List<eTerritorio> lst = new List<eTerritorio>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik" };
                object[] Valores = { Id_Emp, 
                                       Id_Cd, 
                                       Id_Cte, 
                                       Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_PorClienteYRik", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eTerritorio obj = new eTerritorio();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));

                    obj.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    obj.Id_Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    obj.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));

                    obj.Ter_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ter_Activo")));

                    obj.Id_TipoCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoCliente")));
                    obj.Cve_Terr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cve_Terr")));

                    lst.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                return lst;
            }

            return lst;
        }

        /// <summary>
        /// Consulta los territorios asociados al RIK. Este procedimiento es utilizado en el diálogo "Nuevo Proyecto para Prospecto" en la vista de prospecto  y en el diálogo "Nuevo Proyecto" en la vista de Proyectos, dentro del módulo "Portal del RIK", en la sección "Gestión de la Promoción".
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribucion</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="cadenaDeConexionEF">Cadena de conexión a la base de datos, compatible con Entity Framework</param>
        /// <returns>Conjunto de territorios asociados a un representante</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorRik(int idEmp, int idCd, int idRik, string cadenaDeConexionEF)
        {
            IEnumerable<CatTerritorio> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var res = (from t in ctx.CatTerritorios
                           where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik && t.Ter_Activo.Value
                           select t).ToList().Select(ct =>
                           {
                               ct.CatSegmentoSerializable = ct.CatSegmento;
                               ct.CatUENSerializable = ct.CatUEN;
                               return ct;
                           }).ToList();
                result = res;
            }
            return result;
        }

        /// <summary>
        /// Consulta los territorios asociados al RIK. Este procedimiento es utilizado en el diálogo "Nuevo Proyecto para Prospecto" en la vista de prospecto  y en el diálogo "Nuevo Proyecto" en la vista de Proyectos, dentro del módulo "Portal del RIK", en la sección "Gestión de la Promoción".
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribucion</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="cadenaDeConexionEF">Cadena de conexión a la base de datos, compatible con Entity Framework</param>
        /// <returns>Conjunto de territorios asociados a un representante</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorRik(int idEmp, int idCd, int idRik, ICD_Contexto icdCtx)
        {
            IEnumerable<CatTerritorio> result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var res = (from t in ctx.CatTerritorios
                       where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik && t.Ter_Activo.Value
                       select t).ToList().Select(ct =>
                       {
                           ct.CatSegmentoSerializable = ct.CatSegmento;
                           ct.CatUENSerializable = ct.CatUEN;
                           return ct;
                       });
            result = res;
            return result;
        }

        /// <summary>
        /// Consulta los territorios asociados al segmento asociado al RIK especificado. Este procedimiento es utilizado en el diálogo "Nuevo Proyecto para Prospecto" en la vista de prospecto  y en el diálogo "Nuevo Proyecto" en la vista de Proyectos, dentro del módulo "Portal del RIK", en la sección "Gestión de la Promoción".
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribucion</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idSeg">Identificador del segmento</param>
        /// <param name="cadenaDeConexionEF">Cadena de conexión a la base de datos, compatible con Entity Framework</param>
        /// <returns>Conjunto de territorios asociados a un representante</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorRikYSegmento(int idEmp, int idCd, int idRik, int idSeg, string cadenaDeConexionEF)
        {
            IEnumerable<CatTerritorio> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var res = (from t in ctx.CatTerritorios
                           where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik && t.Id_Seg == idSeg && t.Ter_Activo.Value
                           select t).ToList();
                result = res;
            }
            return result;
        }

        /// <summary>
        /// Regresa el conjunto de instancias de la entidad [CatTerritorio] asociadas al cliente idCte sin importar en el estado en el que se encuentren.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idCte">Identificador del cliente del cual se interesa extraer los territorios asociados</param>
        /// <param name="cadenaDeConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>IEnumerable[CatTerritorio]. Conjunto de instancias de la entidad [CatTerritorio].</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorCliente(int idEmp, int idCd, int idCte, string cadenaDeConexionEF)
        {
            IEnumerable<CatTerritorio> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var territorios = (from ccd in ctx.CatClienteDets
                                   where ccd.Id_Emp == idEmp && ccd.Id_Cd == idCd && ccd.Id_Cte == idCte
                                   select ccd.CatTerritorio).ToList().Select(ct =>
                                   {
                                       ct.CatSegmentoSerializable = ct.CatSegmento;
                                       ct.CatUENSerializable = ct.CatUEN;
                                       return ct;
                                   }).ToList();
                resultado = territorios;
            }
            return resultado;
        }

        /// <summary>
        /// Regresa el conjunto de instancias de la entidad [CatTerritorio] asociadas al cliente idCte y al territorio del RIK idrim; se consultan los territorios sin importar en el estado en el que se encuentren.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idCte">Identificador del cliente del cual se interesa extraer los territorios asociados</param>
        /// <param name="idRik">Identificador del representante del cual maneja al cliente en los territorios asociados en ámbos</param>
        /// <param name="cadenaDeConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>IEnumerable[CatTerritorio]. Conjunto de instancias de la entidad [CatTerritorio].</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorClienteYRIK(int idEmp, int idCd, int idCte, int idRik, string cadenaDeConexionEF)
        {
            IEnumerable<CatTerritorio> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var territorios = (from ccd in ctx.CatClienteDets
                                   where ccd.Id_Emp == idEmp && ccd.Id_Cd == idCd && ccd.Id_Cte == idCte && ccd.CatTerritorio.Id_Rik == idRik
                                   select ccd.CatTerritorio).ToList().Select(ct =>
                                   {
                                       ct.CatSegmentoSerializable = ct.CatSegmento;
                                       ct.CatUENSerializable = ct.CatUEN;
                                       return ct;
                                   }).ToList();
                resultado = territorios;
            }
            return resultado;
        }

        public List<CatTerritorio> ConsultarTerritorios_PorClienteYRIK(int idEmp, int idCd, int idCte, int idRik, string strConn)
        {
            List<CatTerritorio> Lst = new List<CatTerritorio>();

            /*
            IEnumerable<CatTerritorio> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var territorios = (from ccd in ctx.CatClienteDets
                                   where ccd.Id_Emp == idEmp && ccd.Id_Cd == idCd && ccd.Id_Cte == idCte && ccd.CatTerritorio.Id_Rik == idRik
                                   select ccd.CatTerritorio).ToList().Select(ct =>
                                   {
                                       ct.CatSegmentoSerializable = ct.CatSegmento;
                                       ct.CatUENSerializable = ct.CatUEN;
                                       return ct;
                                   }).ToList();
                resultado = territorios;
            }
            return resultado;
             */
            /*
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConn);
                Funciones cdFunciones = new Funciones();
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik" };
                object[] Valores = { idEmp, idCd, idCte, idRik };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelCapAcysDatosGarantia_PorRemision", ref dr, Parametros, Valores);

                List<AcysDatosGarantia> listaDatosGar = new List<AcysDatosGarantia>();
                while (dr.Read())
                {
                    AcysDatosGarantia datosGar = new AcysDatosGarantia();
                    datosGar = cdFunciones.GetEntity<AcysDatosGarantia>(dr);
                    listaDatosGar.Add(datosGar);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                return listaDatosGar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
            return Lst;
        }


        /// <summary>
        /// Regresa el conjunto de instancias de la entidad [CatTerritorio] asociadas al cliente idCte y al territorio del RIK idrim; se consultan los territorios sin importar en el estado en el que se encuentren.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idCte">Identificador del cliente del cual se interesa extraer los territorios asociados</param>
        /// <param name="idRik">Identificador del representante del cual maneja al cliente en los territorios asociados en ámbos</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CatTerritorio]. Conjunto de instancias de la entidad [CatTerritorio].</returns>
        public IEnumerable<CatTerritorio> ConsultarTerritoriosPorClienteYRIK(int idEmp, int idCd, int idCte, int idRik, ICD_Contexto icdCtx)
        {
            IEnumerable<CatTerritorio> resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var territorios = (from ccd in ctx.CatClienteDets
                               where ccd.Id_Emp == idEmp && ccd.Id_Cd == idCd && ccd.Id_Cte == idCte && ccd.CatTerritorio.Id_Rik == idRik
                               select ccd.CatTerritorio).ToList().Select(ct =>
                               {
                                   ct.CatSegmentoSerializable = ct.CatSegmento;
                                   ct.CatUENSerializable = ct.CatUEN;
                                   return ct;
                               });
            resultado = territorios;
            return resultado;
        }




        public void ConsultaAutorizacionPendienteTerritorio(int ClaveTerritorio, ref ModelAutorizacionTerritorios SolicitudDatos, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                string[] Parametros = { "@IdAutorizacion", "@ClaveTerritorio ", "@IdRepresentante", "@Territorio", "@Activo", "@IdUSolicita", "@Accion" };
                object[] Valores = { 0, ClaveTerritorio, 0, "", false, 0, 3 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref dr, Parametros, Valores);

                SolicitudDatos = new CapaEntidad.ModelAutorizacionTerritorios();

                if (dr.HasRows)
                {

                    //  dr.Read();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    SolicitudDatos.IdAutorizacion = Convert.IsDBNull(dt.Rows[0]["IdAutorizacion"]) ? 0 : Convert.ToInt64(dt.Rows[0]["IdAutorizacion"].ToString());
                    SolicitudDatos.IdRepresentante = Convert.IsDBNull(dt.Rows[0]["IdRepresentante"]) ? 0 : Convert.ToInt32(dt.Rows[0]["IdRepresentante"].ToString());
                    SolicitudDatos.Territorio = Convert.IsDBNull(dt.Rows[0]["Territorio"]) ? "" : dt.Rows[0]["Territorio"].ToString();
                    SolicitudDatos.Activo = bool.Parse(dt.Rows[0]["Activo"].ToString());
                }
                else
                {
                    SolicitudDatos.IdAutorizacion = 0;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void ConsultaTerritoriosAutorizacionPendientes(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@IdCiudad",
                                        "@IdEmpresa",
	                                    "@Accion"
                                      };
                object[] Valores = { IdCiudad, IdEmpresa, 1 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_AutorizacionCambiosTerritorio ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreSolicitante = (string)dr.GetValue(dr.GetOrdinal("Solicita"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));


                    List.Add(Solicitud);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosAutorizacionAprobadas(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@IdCiudad",
                                        "@IdEmpresa",
	                                    "@Accion"
                                      };
                object[] Valores = { IdCiudad, IdEmpresa, 2 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_AutorizacionCambiosTerritorio ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreAprobador = (string)dr.GetValue(dr.GetOrdinal("Autorizo"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));


                    List.Add(Solicitud);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosAutorizacionRechazar(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@IdCiudad",
                                        "@IdEmpresa",
	                                    "@Accion"
                                      };
                object[] Valores = { IdCiudad, IdEmpresa, 3 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_AutorizacionCambiosTerritorio ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreAprobador = (string)dr.GetValue(dr.GetOrdinal("Rechazo"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));


                    List.Add(Solicitud);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarAutorizacionTerritorios(CapaEntidad.ModelAutorizacionTerritorios DatosAutorizacion, ref int Respuesta, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@IdAutorizacion",
                                        "@ClaveTerritorio",
	                                    "@IdRepresentante", 
	                                    "@Territorio", 
	                                    "@Activo", 
                                        "@IdUSolicita",
	                                    "@Accion"
                                      };

                if (DatosAutorizacion.IdAutorizacion == 0)
                {
                    //Nuevo


                    CapaEntidad.AutorizacionTerritorio NuevaSolicitud = new CapaEntidad.AutorizacionTerritorio();

                    NuevaSolicitud.IdRepresentante = DatosAutorizacion.IdRepresentante;
                    NuevaSolicitud.ClaveTerritorio = DatosAutorizacion.ClaveTerritorio;
                    NuevaSolicitud.Territorio = DatosAutorizacion.Territorio;
                    NuevaSolicitud.Activo = DatosAutorizacion.Activo;
                    //Estatus: 1 Pendiente | 2 Autorizado | 3 Rechazado
                    // NuevaSolicitud.Estatus = 1;
                    NuevaSolicitud.IdUSolicita = DatosAutorizacion.IdUSolicita;
                    NuevaSolicitud.FechaSolicitud = System.DateTime.Now;
                    object[] Valores = null;
                    Valores = new object[] { 
                                        NuevaSolicitud.IdAutorizacion,
                                        NuevaSolicitud.ClaveTerritorio,
                                        NuevaSolicitud.IdRepresentante,
                                        NuevaSolicitud.Territorio,
                                        NuevaSolicitud.Activo,
                                        NuevaSolicitud.IdUSolicita,
                                        1
                                   };
                    SqlCommand sqlcmd = null;

                    sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref Respuesta, Parametros, Valores);

                    CapaDatos.CommitTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                }
                else
                {
                    //Editar


                    CapaEntidad.AutorizacionTerritorio NuevaSolicitud = new CapaEntidad.AutorizacionTerritorio();

                    NuevaSolicitud.IdRepresentante = DatosAutorizacion.IdRepresentante;
                    NuevaSolicitud.Territorio = DatosAutorizacion.Territorio;
                    NuevaSolicitud.Activo = DatosAutorizacion.Activo;
                    //Estatus: 1 Pendiente | 2 Autorizado | 3 Rechazado
                    // NuevaSolicitud.Estatus = 1;
                    NuevaSolicitud.IdUSolicita = DatosAutorizacion.IdUSolicita;
                    NuevaSolicitud.FechaSolicitud = System.DateTime.Now;
                    object[] Valores = null;
                    Valores = new object[] { 
                                        NuevaSolicitud.IdAutorizacion,
                                        NuevaSolicitud.ClaveTerritorio,
                                        NuevaSolicitud.IdRepresentante,
                                        NuevaSolicitud.Territorio,
                                        NuevaSolicitud.Activo,
                                        2,
                                        NuevaSolicitud.IdUSolicita
                                   };
                    SqlCommand sqlcmd = null;

                    sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref Respuesta, Parametros, Valores);

                    CapaDatos.CommitTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    Respuesta = 1;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
