using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_EntSalSolicitud
    {
        public void CapEntSalSolicitud_ConsultaLista(EntSalSolicitud es, ref List<EntSalSolicitud> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cd",
                                        "@Id_ESolIni",
                                        "@Id_ESolFin",
                                        "@ESol_FechaIni" ,
                                        "@ESol_FechaFin",
                                        "@ESol_Estatus"};
                object[] Valores = {
                                     es.Id_Cd ,
                                     es.Id_ESolIni == (int?) null ? (object) null: es.Id_ESolIni,
                                     es.Id_ESolFin == (int?) null ? (object) null: es.Id_ESolFin,
                                     es.ESol_FechaIni == (DateTime?) null ? (object) null : es.ESol_FechaIni,
                                     es.ESol_FechaFin == (DateTime?) null ? (object) null : es.ESol_FechaFin,
                                     es.ESol_Estatus == "" ? (object) null: es.ESol_Estatus
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSalSolicitud_ConsultaLista", ref dr, Parametros, Valores);

                EntSalSolicitud e;
                while (dr.Read())
                {
                    e = new EntSalSolicitud();
                    e.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    e.Id_ESol = Convert.ToInt32(dr["Id_ESol"]);
                    e.ESol_Unique = dr["ESol_Unique"].ToString();
                    e.ESol_EstatusStr = dr["ESol_EstatusStr"].ToString();
                    e.Id_TmStr = dr["Id_TmStr"].ToString();
                    e.Id_UCreoStr = dr["Id_UCreoStr"].ToString();
                    e.Id_EsStr = dr["Id_EsStr"].ToString();
                    e.ESol_Fecha = Convert.ToDateTime(dr["ESol_Fecha"]);
                    e.Id_UEnviar = Convert.ToInt32(dr["Id_UEnviar"]);
                    e.Id_UCC = Convert.ToInt32(dr["Id_UCC"]);
                    e.ESol_Total = Convert.ToDouble(dr["Esol_Total"]);
                    List.Add(e);
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_ConsultaConsecutivo(Sesion sesion, ref int Id_ESol)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { sesion.Id_Cd_Ver };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSalSolicitud_ConsultaConsecutivo", ref dr, Parametros, Valores);

                dr.Read();
                Id_ESol = Convert.ToInt32(dr["Id_ESol"]);
                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_Insertar(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = {      "@Id_Emp",    
                                             "@Id_Cd",    
                                             "@ESol_Unique",    
                                             "@ESol_Naturaleza",      
                                             "@Id_Tm",    
                                             "@Id_Cte",    
                                             "@Id_Pvd", 
                                             "@Id_Ter",
                                             "@ESol_Referencia",  
                                             "@Id_UEnviar" ,
                                             "@Id_UCC", 
                                             "@ESol_Notas",   
                                             "@ESol_CteCuentaNacional",
                                             "@ESol_CteCuentaContNacional",
                                             "@ESol_FechaReferencia",
                                             "@ESol_Subtotal", 
                                             "@ESol_Impuesto",    
                                             "@ESol_Total",    
                                             "@Id_UCreo",
                                             "@ESol_Fecha"
 
                                      };
                object[] Valores = {
                                       es.Id_Emp ,
                                       es.Id_Cd,
                                       es.ESol_Unique ,
                                       es.ESol_Naturaleza,
                                       es.Id_Tm,
                                       ((es.Id_Cte==-1) ? (object)null: es.Id_Cte),
                                       ((es.Id_Pvd==-1) ? (object)null: es.Id_Pvd),
                                        es.Id_Ter == -1 ? (object)null: es.Id_Ter,
                                       es.ESol_Referencia,
                                       es.Id_UEnviar,
                                       es.Id_UCC,
                                       es.ESol_Notas,
                                       es.ESol_CteCuentaNacional,
                                       es.ESol_CteCuentaContNacional,
                                       es.ESol_FechaReferencia,
                                       es.ESol_Subtotal,
                                       es.ESol_Impuesto,
                                       es.ESol_Total,
                                       es.Id_UCreo,
                                       es.ESol_Fecha
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudInsertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitudDet_Insertar(int Id_ESol, EntSalSolicitud es, List<EntSalSolicitudDet> List, ref int Verificador, string Conexion)
        {
            try
            {
                string[] Parametros = {  
                                      	"@Id_Emp",  
	                                    "@Id_Cd",  
	                                    "@Id_ESol" ,  
	                                    "@Id_ESolD",  
	                                    "@ESol_Naturaleza",  
	                                    "@Id_Ter",  
	                                    "@Id_Prd",
	                                    "@ESol_Cantidad",
	                                    "@ESol_EsCosto",
	                                    "@ESol_BuenEstado" ,
	                                    "@ESol_AfectaOrdCom"
                                      
                                      };
                int Id_ESolD = 1;
                foreach (EntSalSolicitudDet e in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);

                    object[] Valores = {
                                           e.Id_Emp ,
                                           e.Id_Cd ,
                                           Id_ESol,
                                           Id_ESolD ++,
                                           es.ESol_Naturaleza ,
                                           e.Id_Ter==-1?(object)null:e.Id_Ter,
                                           e.Id_Prd,
                                           e.ESol_Cantidad ,
                                           e.ESol_EsCosto ,
                                           e.ESol_BuenEstado ,
                                           e.Afecta
                                       };
                    SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudDetInsertar", ref Verificador, Parametros, Valores);

                    cd_datos.LimpiarSqlcommand(ref sqlcmd);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_Cancelar(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = {"@Id_Cd",
                                       "@Id_ESol"};
                object[] Valores = {
                                       es.Id_Cd ,
                                       es.Id_ESol 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCapEntSal_SolicitudCancelar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_CorreoCreo(int Id_Cd, int Id_ESol, string Url, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd",
                                        "@Id_ESol",
                                        "@Url"};

                object[] Valores = {  Id_Cd,
                                      Id_ESol,
                                      Url};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCapEntSal_SolicitudEnviar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_Consulta(string ESol_Unique, ref EntSalSolicitud es, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@ESol_Unique" };
                object[] Valores = { ESol_Unique };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntsal_SolicitudConsulta", ref dr, Parametros, Valores);


                if (dr.Read())
                {
                    es.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    es.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    es.Id_ESol = Convert.ToInt32(dr["Id_ESol"]);
                    es.ESol_Naturaleza = Convert.ToInt32(dr["ESol_Naturaleza"]);
                    es.Id_Tm = Convert.ToInt32(dr["Id_Tm"]);
                    es.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    es.Id_Pvd = Convert.ToInt32(dr["Id_Pvd"]);
                    es.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                    es.ESol_Referencia = dr["ESol_Referencia"].ToString();
                    es.Id_Rem = Convert.ToInt32(dr["Id_Rem"]);
                    es.Id_Fac = Convert.ToInt32(dr["Id_Fac"]);
                    es.Id_UEnviar = Convert.ToInt32(dr["Id_UEnviar"]);
                    es.Id_UCC = Convert.ToInt32(dr["Id_UCC"]);
                    es.ESol_Notas = dr["ESol_Notas"].ToString();
                    es.ESol_CteCuentaNacional = Convert.ToInt32(dr["ESol_CteCuentaNacional"]);
                    es.ESol_CteCuentaContNacional = Convert.ToInt32(dr["ESol_CteCuentaContNacional"]);
                    es.ESol_FechaReferencia = dr["ESol_FechaReferencia"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["ESol_FechaReferencia"]);
                    es.ESol_Subtotal = Convert.ToDouble(dr["ESol_Subtotal"]);
                    es.ESol_Impuesto = Convert.ToDouble(dr["ESol_Impuesto"]);
                    es.ESol_Total = Convert.ToDouble(dr["ESol_Total"]);
                    es.ESol_Fecha = Convert.ToDateTime(dr["ESol_Fecha"]);
                    es.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    es.Ter_Nombre = dr["Ter_Nombre"].ToString();
                    es.ESol_Estatus = dr["ESol_Estatus"].ToString();
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_ConsultaDet(string ESol_Unique, ref List<EntSalSolicitudDet> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@ESol_Unique" };
                object[] Valores = { ESol_Unique };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntsal_SolicitudConsultaDet", ref dr, Parametros, Valores);

                EntSalSolicitudDet e;
                while (dr.Read())
                {
                    e = new EntSalSolicitudDet();
                    e.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    e.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    e.Id_ESol = dr.GetInt32(dr.GetOrdinal("Id_ESol"));
                    e.Id_ESolD = dr.GetInt32(dr.GetOrdinal("Id_ESolD"));
                    e.Id_EsDetStr = Guid.NewGuid().ToString();
                    e.ESol_Naturaleza = Convert.ToInt32(dr["ESol_Naturaleza"]);
                    e.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Ter"));
                    e.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    e.Id_Prd = dr.GetInt32(dr.GetOrdinal("Id_Prd"));
                    e.Prd_AgrupadoSpo = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    e.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    e.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    e.Prd_Unidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unine"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Unine"));
                    e.Presentacion = e.Prd_Presentacion;
                    e.ESol_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ESol_Cantidad"))) ? 0 : dr.GetInt32(dr.GetOrdinal("ESol_Cantidad"));
                    e.ESol_EsCosto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ESol_EsCosto"))) ? 0 : dr.GetDouble(dr.GetOrdinal("ESol_EsCosto"));
                    e.ESol_BuenEstado = dr.GetBoolean(dr.GetOrdinal("ESol_BuenEstado"));
                    e.Afct_OrdCompra = dr.GetBoolean(dr.GetOrdinal("ESol_AfectaOrdCom"));
                    e.Importe = Convert.ToDouble(dr["Importe"]);

                    List.Add(e);

                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_EliminarDet(int Id_Cd, int Id_ESol, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_ESol" };
                object[] Valores = { Id_Cd, Id_ESol };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntsal_SolicitudEliminaDet", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_ModificarEstatus(int Id_Cd, int Id_ESol, string ESol_Estatus, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd",
                                        "@Id_ESol",
                                        "@ESol_Estatus"
                                      };
                object[] Valores = {
                                       Id_Cd ,
                                       Id_ESol ,
                                       ESol_Estatus 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudModificaEstatus", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CapEntSalSolicitud_CorreoAtendio(int Id_Cd, int Id_ESol, string Url, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd",
                                        "@Id_ESol",
                                        "@Url"};

                object[] Valores = {  Id_Cd,
                                      Id_ESol,
                                      Url};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCapEntSal_SolicitudAtender", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSalSolicitud_Autorizo(int Id_Cd, int Id_ESol, int Id_Es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {"@Id_Cd",
                                       "@Id_ESol",
                                      "@Id_Es"};
                object[] Valores = { Id_Cd,
                                     Id_ESol ,
                                     Id_Es };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudAutorizar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void GuardarEntradaSalida(ref EntradaSalida entradaSalida, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                int Id_Es = 0;
                string[] Parametros = { 
                                          "@Id_Emp",
	                                      "@Id_Cd",	
                                          "@Id_U",
	                                      "@Es_Naturaleza", 
	                                      "@Es_Fecha",
	                                      "@Id_Tm",
	                                      "@Id_Cte", 
	                                      "@Id_Pvd",
	                                      "@Es_Referencia",
	                                      "@Es_Notas",
	                                      "@Es_Subtotal",
	                                      "@Es_Iva",
	                                      "@Es_Total",
	                                      "@Es_Estatus",
                                          "@ManAut",
                                          "@Id_Ter"
                                          ,"@Es_CteCuentaNacional"
                                          ,"@Es_CteCuentaContNacional"
                                          ,"@Es_FechaReferencia"
                                      };
                object[] Valores = { 
                                        entradaSalida.Id_Emp,
                                        entradaSalida.Id_Cd,
                                        entradaSalida.Id_U,
                                        entradaSalida.Es_Naturaleza,
                                        entradaSalida.Es_Fecha,
                                        entradaSalida.Id_Tm,
                                        ((entradaSalida.Id_Cte==-1) ? (object)null: entradaSalida.Id_Cte),
                                        ((entradaSalida.Id_Pvd==-1) ? (object)null: entradaSalida.Id_Pvd), 
                                        entradaSalida.Es_Referencia,
                                        entradaSalida.Es_Notas,
                                        entradaSalida.Es_SubTotal,
                                        entradaSalida.Es_Iva,
                                        entradaSalida.Es_Total,
                                        entradaSalida.Es_Estatus
                                        ,1 //MANUAL
                                        ,entradaSalida.Id_Ter == -1 ? (object)null: entradaSalida.Id_Ter
                                        ,entradaSalida.Es_CteCuentaNacional
                                        ,entradaSalida.Es_CteCuentaContNacional
                                        ,entradaSalida.Es_FechaReferencia
                                   };
                //inserta una entradaSalida
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref Id_Es, Parametros, Valores);

                entradaSalida.Id_Es = Id_Es;

                Parametros = new string[]{
                                            "@Id_Emp",  
                                            "@Id_Cd",  
                                            "@Id_Es",  
                                            "@Id_EsDet",  
                                            "@EsDet_Naturaleza",  
                                            "@Id_Tm",
                                            "@Id_Ter",  
                                            "@Es_BuenEstado",  
                                            "@Id_Prd",  
                                            "@Es_Cantidad",  
                                            "@Es_Costo",  
                                            "@Afct_OrdCompra", 
                                            "@Afct_Precios", 
                                            "@Prd_AgrupadoSpo",  
                                            "@Id_Ref", 
                                            "@Id_Pvd", 
                                            "@Id_Cte", 
                                            "@Fecha"
		                                };
                int Id_EsDet = 0;
                foreach (EntradaSalidaDetalle detalle in listaDetalle)
                {
                    Valores = new object[]{
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                entradaSalida.Id_Es,
                                                Id_EsDet++,
                                                entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                entradaSalida.Id_Tm,
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afecta,
                                                strEmp != detalle.Id_Emp,
                                                detalle.Prd_AgrupadoSpo,
                                                entradaSalida.Es_Referencia
                                                ,entradaSalida.Id_Pvd
                                                ,entradaSalida.Id_Cte
                                                ,entradaSalida.Es_Fecha
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, Parametros, Valores);
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
        public void CapEntSalSolicitud_ConsultaFolio(string ESol_Unique, ref int Id_ESol, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@ESol_Unique" };
                object[] Valores = { ESol_Unique };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_ConsultaFolio", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Id_ESol = Convert.ToInt32(dr["Id_Esol"]);
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CapEntSolicitud_ConsultaDatosEnvio(int Id_Cd, int Id_ESol, ref EntSalSolicitud es, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Cd",
                                          "@Id_ESol"
                                      };
                object[] Valores = {
                                       Id_Cd,
                                       Id_ESol
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudConsultaDatosEnvio", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    es.ESol_CorreoDest = dr["ESol_CorreoDest"].ToString();
                    es.ESol_CorreoCC = dr["ESol_CorreoCC"].ToString();
                    es.ESol_Unique = dr["ESol_Unique"].ToString();
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CapEntSalSolicitud_ValidarMonto(EntSalSolicitud es, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = {"@Id_Emp",
                                       "@Id_Cd",
                                       "@Id_Tm"};
                object[] Valores = {
                                       es.Id_Emp,
                                       es.Id_Cd ,
                                       es.Id_Tm
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapEntSal_SolicitudValidar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
