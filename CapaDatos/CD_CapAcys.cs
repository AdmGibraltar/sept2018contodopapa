using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapAcys
    {
        public void ConsultadeAcys_Rpt_Cumplimiento(int Id_Cd, int Id_Ter, int Id_Rep, int Anio_Ini, int Mes_Ini, int Anio_Fin, int Mes_Fin, ref DataTable dt, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Ter", "@Id_Rik ", "@Anio_Ini", "@Mes_Ini", "@Anio_Fin", "@Mes_Fin" };
                object[] Valores = { Id_Cd, Id_Ter, Id_Rep, Anio_Ini, Mes_Ini, Anio_Fin, Mes_Fin };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_CapAcys_Cumplimiento", ref dr, Parametros, Valores);


                dt.Load(dr);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcys_Rpt_Resumen(Acys acys, string Conexion, ref DataTable Dt)
        {
            string Fecha_Inicial = acys.Filtro_FecIni.Value.Month.ToString() + '-' + acys.Filtro_FecIni.Value.Day.ToString() + '-' + acys.Filtro_FecIni.Value.Year.ToString();
            string Fecha_Final = acys.Filtro_FecFin.Value.Month.ToString() + '-' + acys.Filtro_FecFin.Value.Day.ToString() + '-' + acys.Filtro_FecFin.Value.Year.ToString();

            if (acys.Id_Ter < 1)
            { acys.Id_Ter = 0; }
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",                                          
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,                                       
                                       Fecha_Inicial,
                                       Fecha_Final,
                                       acys.Filtro_usuario == ""? (object)null: acys.Filtro_usuario,    
                                       acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_CapAcys_Resumen", ref dr, Parametros, Valores);


                Dt.Load(dr);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcys_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Estatus",
                                          "@Filtro_FolIni",
                                          "@Filtro_FolFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Acs_Vencido",
                                          "@Id_Modalidad"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Filtro_Estatus == ""? (object)null: acys.Filtro_Estatus,
                                       acys.Filtro_FolIni == ""? (object)null: acys.Filtro_FolIni,
                                       acys.Filtro_FolFin == ""? (object)null: acys.Filtro_FolFin,
                                       acys.Filtro_FecIni,
                                       acys.Filtro_FecFin,
                                       acys.Filtro_usuario == ""? (object)null: acys.Filtro_usuario,
                                       acys.Id_Ter == -1 ? (object)null: acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte,
                                       acys.Acs_Vencido == ""? (object)null: acys.Acs_Vencido,
                                       acys.Id_Modalidad
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista", ref dr, Parametros, Valores);

                Acys a;
                while (dr.Read())
                {
                    a = new Acys();
                    a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    a.Acs_Version = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                    a.Acs_Estatus = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    a.Acs_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString());
                    a.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    a.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    a.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    a.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    a.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    a.Acs_FechaInicioDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")));
                    a.Acs_FechaFinDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")));
                    a.Acs_Vencido = dr.IsDBNull(dr.GetOrdinal("Acs_Vencido")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();
                    a.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();

                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcys_Resumen(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Estatus",
                                          "@Filtro_FolIni",
                                          "@Filtro_FolFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Acs_Vencido"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Filtro_Estatus == ""? (object)null: acys.Filtro_Estatus,
                                       acys.Filtro_FolIni == ""? (object)null: acys.Filtro_FolIni,
                                       acys.Filtro_FolFin == ""? (object)null: acys.Filtro_FolFin,
                                       acys.Filtro_FecIni,
                                       acys.Filtro_FecFin,
                                       acys.Filtro_usuario == ""? (object)null: acys.Filtro_usuario,
                                       acys.Id_Ter == -1 ? (object)null: acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte,
                                       acys.Acs_Vencido == ""? (object)null: acys.Acs_Vencido
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista", ref dr, Parametros, Valores);

                Acys a;
                while (dr.Read())
                {
                    a = new Acys();
                    a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    a.Acs_Version = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                    a.Acs_Estatus = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    a.Acs_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString());
                    a.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    a.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    a.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    a.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    a.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    a.Acs_FechaInicioDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")));
                    a.Acs_FechaFinDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")));
                    a.Acs_Vencido = dr.IsDBNull(dr.GetOrdinal("Acs_Vencido")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();

                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcysVersiones_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                                                               
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs                                      
                                      
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysVersiones_Lista", ref dr, Parametros, Valores);

                Acys a;
                while (dr.Read())
                {
                    a = new Acys();
                    a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    a.Acs_Version = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Version")));
                    a.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    a.FechaInicioVersion = dr.GetValue(dr.GetOrdinal("FechaInicioVersion")).ToString();
                    a.FechaFinVersion = dr.GetValue(dr.GetOrdinal("FechaFinVersion")).ToString();

                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Estatus(string p)
        {
            switch (p)
            {
                case "B":
                    return "Cancelado";
                case "C":
                    return "Capturado";
                case "I":
                    return "Impreso";
                case "A":
                    return "Autorizado";
                case "S":
                    return "Solicitado";
                case "R":
                    return "Rechazado";
                default:
                    return "";
            }
        }

        public void ConsultarAcys(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Ter == -1 ? (object)null: acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void actualizarEstatus(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion", 
                                          "@Acs_Estatus"                                          
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Acs_Estatus                                      
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_ActualizarEstatus", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarAcys(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion"                                                                             
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion                                   
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Autorizar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisFrecuencia",
                                          "@Acs_VisitaOtro",
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U",
                                          "@Acs_Modalidad",
                                          "@Id_CteDirEntrega",
                                            "@Acs_Sucursal",
                                            "@Acs_ParcialidadesSi",
                                            "@Acs_ParcialidadesNo",
                                            "@Acs_ConfirmacionPedidosSI",
                                            "@Acs_ConfirmacionPedidosnO",
                                            "@Acs_chkRecRevLunes",
                                            "@Acs_RecRevMartes",
                                            "@Acs_RecRevMiercoles",
                                            "@Acs_RecRevJueves",
                                            "@Acs_RecRevViernes",
                                            "@Acs_RecRevSabado",
                                            "@Acs_TimePicker1",
                                            "@Acs_TimePicker2",
                                            "@Acs_TimePicker3",
                                            "@Acs_TimePicker4",
                                            "@Acs_RecPersonaRecibe",
                                            "@Acs_RecPuesto",
                                            "@Acs_RecCitaMismoDia",
                                            "@Acs_RecCitaSinCita",
                                            "@Acs_RecCitaPrevia",
                                            "@Acs_RecCitaContacto",
                                            "@Acs_RecCitaTelefono",
                                            "@Acs_RecCitaDiasdeAnticipacion",
                                            "@Acs_RecAreaPropia",
                                            "@Acs_RecAreaPlaza",
                                            "@Acs_RecAreaCalle",
                                            "@Acs_RecAreaAvTransitada",
                                            "@Acs_RecEstCortesia",
                                            "@Acs_RecEstCosto",
                                            "@Acs_RecEstMonto",
                                            "@Acs_RecDocFactFranquiciaEnt",
                                            "@Acs_RecDocFactFranquiciaEntCop",
                                            "@Acs_RecDocFactFranquiciaRec",
                                            "@Acs_RecDocFactFranquiciaRecCop",
                                            "@Acs_RecDocFactKeyEnt",
                                            "@Acs_RecDocFactKeyEntCop",
                                            "@Acs_RecDocFactKeyRec",
                                            "@Acs_RecDocFactKeyRecCop",
                                            "@Acs_RecDocOrdCompraEnt",
                                            "@Acs_RecDocOrdCompraEntCop",
                                            "@Acs_RecDocOrdCompraRec",
                                            "@Acs_RecDocOrdCompraRecCop",
                                            "@Acs_RecDocOrdReposEnt",
                                            "@Acs_RecDocOrdReposEntCop",
                                            "@Acs_RecDocOrdReposRec",
                                            "@Acs_RecDocOrdReposRecCop",
                                            "@Acs_RecDocCopPedidoEnt",
                                            "@Acs_RecDocCopPedidoEntCop",
                                            "@Acs_RecDocCopPedidoRec",
                                            "@Acs_RecDocCopPedidoRecCop",
                                            "@ACS_RecDocRemisionEnt",
                                            "@ACS_RecDocRemisionEntCop",
                                            "@ACS_RecDocRemisionRec",
                                            "@ACS_RecDocRemisionRecCop",
                                            "@ACS_RecDocFolioEnt",
                                            "@ACS_RecDocFolioEntCop",
                                            "@ACS_RecDocFolioRec",
                                            "@ACS_RecDocFolioRecCop",
                                            "@ACS_RecDocContraRecEnt",
                                            "@ACS_RecDocContraRecEntCop",
                                            "@ACS_RecDocContraRecRec",
                                            "@ACS_RecDocContraRecRecCop",
                                            "@ACS_RecDocEntAlmacenEnt",
                                            "@ACS_RecDocEntAlmacenEntCop",
                                            "@ACS_RecDocEntAlmacenRec",
                                            "@ACS_RecDocEntAlmacenRecCop",
                                            "@ACS_RecDocSopServicioEnt",
                                            "@ACS_RecDocSopServicioEntCop",
                                            "@ACS_RecDocSopServicioRec",
                                            "@ACS_RecDocSopServicioRecCop",
                                            "@ACS_RecDocNomFirmaEnt",
                                            "@ACS_RecDocNomFirmaEntCop",
                                            "@ACS_RecDocNomFirmaoRec",
                                            "@ACS_RecDocNomFirmaRecCop",
                                            "@ACS_RecCitaEnt",
                                            "@ACS_RecCitaEntCop",
                                            "@ACS_RecCitaRec",
                                            "@ACS_RecCitaRecCop",
                                            "@ACS_RecOtroRec",
                                        "@ACS_chk62Lunes",
                                        "@ACS_chk62Martes",
                                        "@ACS_chk62Miercoles",
                                        "@ACS_chk62Jueves",
                                        "@ACS_chk62Viernes",
                                        "@ACS_chk62Sabado",
                                        "@ACS_RadTimePicker162",
                                        "@ACS_RadTimePicker262",
                                        "@ACS_RadTimePicker362",
                                        "@ACS_RadTimePicker462",
                                        "@ACS_txtRecPersonaRecibe62",
                                        "@ACS_txtRecPuesto62",
                                        "@ACS_Chk62Mismodia",
                                        "@ACS_Chk62Sincita",
                                        "@ACS_Chk62Previa",
                                        "@ACS_txt62CitaContacto",
                                        "@ACS_txt62CitaTelefono",
                                        "@ACS_txt62CitaDiasdeAnticipacion",
                                        "@ACS_chk62AreaPropia",
                                        "@ACS_chk62AreaPlaza",
                                        "@ACS_chk62AreaCalle",
                                        "@ACS_chk62AreaAvTransitada",
                                        "@ACS_chk62EstCortesia",
                                        "@ACS_chk62EstCosto",
                                        "@ACS_txt62EstMonto",
                                        "@ACS_txt62ClienteDireccion",
                                        "@ACS_txt62ClienteColonia",
                                        "@ACS_txt62ClienteMunicipio",
                                        "@ACS_txt62ClienteEstado",
                                        "@ACS_txt62ClienteCodPost",
                                        "@ACS_chk62DocFactFranquiciaEnt",
                                        "@ACS_txt62DocFactFranquiciaEntCop",
                                        "@ACS_chk62DocFactFranquiciaRec",
                                        "@ACS_txt62DocFactFranquiciaRecCop",
                                        "@ACS_chk62DocFactKeyEnt",
                                        "@ACS_txt62DocFactKeyEntCop",
                                        "@ACS_chk62DocFactKeyRec",
                                        "@ACS_txt62DocFactKeyRecCop",
                                        "@ACS_chk62DocOrdCompraEnt",
                                        "@ACS_txt62DocOrdCompraEntCop",
                                        "@ACS_chk62DocOrdCompraRec",
                                        "@ACS_txt62DocOrdCompraRecCop",
                                        "@ACS_chk62DocOrdReposEnt",
                                        "@ACS_txt62DocOrdReposEntCop",
                                        "@ACS_chk62DocOrdReposRec",
                                        "@ACS_txt62DocOrdReposRecCop",
                                        "@ACS_chk62DocCopPedidoEnt",
                                        "@ACS_txt62DocCopPedidoEntCop",
                                        "@ACS_chk62DocCopPedidoRec",
                                        "@ACS_txt62DocCopPedidoRecCop",
                                        "@ACS_chk62DocRemisionEnt",
                                        "@ACS_txt62DocRemisionEntCop",
                                        "@ACS_chk62DocRemisionRec",
                                        "@ACS_txt62DocRemisionRecCop",
                                        "@ACS_chk62DocFolioEnt",
                                        "@ACS_txt62DocFolioEntCop",
                                        "@ACS_chk62DocFolioRec",
                                        "@ACS_txt62DocFolioRecCop",
                                        "@ACS_chk62DocContraRecEnt",
                                        "@ACS_txt62DocContraRecEntCop",
                                        "@ACS_chk62DocContraRecRec",
                                        "@ACS_txt62DocContraRecRecCop",
                                        "@ACS_chk62DocEntAlmacenEnt",
                                        "@ACS_txt62DocEntAlmacenEntCop",
                                        "@ACS_chk62DocEntAlmacenRec",
                                        "@ACS_txt62DocEntAlmacenRecCop",
                                        "@ACS_chk62DocSopServicioEnt",
                                        "@ACS_txt62DocSopServicioEntCop",
                                        "@ACS_chk62DocSopServicioRec",
                                        "@ACS_txt62DocSopServicioRecCop",
                                        "@ACS_chk62DocNomFirmaEnt",
                                        "@ACS_txt62DocNomFirmaEntCop",
                                        "@ACS_chk62DocNomFirmaoRec",
                                        "@ACS_txt62DocNomFirmaRecCop",
                                        "@ACS_chk62CitaEnt",
                                        "@ACS_txt62CitaEntCop",
                                        "@ACS_chk62CitaRec",
                                        "@ACS_txt62CitaRecCop",
                                        "@ACS_chk63Lunes",
                                        "@ACS_chk63Martes",
                                        "@ACS_chk63Miercoles",
                                        "@ACS_chk63Jueves",
                                        "@ACS_chk63Viernes",
                                        "@ACS_chk63Sabado",
                                        "@ACS_Rad63TimePicker163",
                                        "@ACS_Rad63TimePicker263",
                                        "@ACS_Rad63TimePicker363",
                                        "@ACS_Rad63TimePicker463",
                                        "@ACS_txtRecPersonaRecibe63",
                                        "@ACS_txtRecPuesto63",
                                        "@ACS_Chk63Mismodia",
                                        "@ACS_Chk63Sincita",
                                        "@ACS_Chk63Previa",
                                        "@ACS_txt63CitaContacto",
                                        "@ACS_txt63CitaTelefono",
                                        "@ACS_txt63CitaDiasdeAnticipacion",
                                        "@ACS_chk63AreaPropia",
                                        "@ACS_chk63AreaPlaza",
                                        "@ACS_chk63AreaCalle",
                                        "@ACS_chk63AreaAvTransitada",
                                        "@ACS_chk63EstCortesia",
                                        "@ACS_chk63EstCosto",
                                        "@ACS_txt63EstMonto",
                                        "@ACS_txt63ClienteDireccion",
                                        "@ACS_txt63ClienteColonia",
                                        "@ACS_txt63ClienteMunicipio",
                                        "@ACS_txt63ClienteEstado",
                                        "@ACS_txt63ClienteCodPost",
                                        "@ACS_chk63DocFactFranquiciaEnt",
                                        "@ACS_txt63DocFactFranquiciaEntCop",
                                        "@ACS_chk63DocFactFranquiciaRec",
                                        "@ACS_txt63DocFactFranquiciaRecCop",
                                        "@ACS_chk63DocFactKeyEnt",
                                        "@ACS_txt63DocFactKeyEntCop",
                                        "@ACS_chk63DocFactKeyRec",
                                        "@ACS_txt63DocFactKeyRecCop",
                                        "@ACS_chk63DocOrdCompraEnt",
                                        "@ACS_txt63DocOrdCompraEntCop",
                                        "@ACS_chk63DocOrdCompraRec",
                                        "@ACS_txt63DocOrdCompraRecCop",
                                        "@ACS_chk63DocOrdReposEnt",
                                        "@ACS_txt63DocOrdReposEntCop",
                                        "@ACS_chk63DocOrdReposRec",
                                        "@ACS_txt63DocOrdReposRecCop",
                                        "@ACS_chk63DocCopPedidoEnt",
                                        "@ACS_txt63DocCopPedidoEntCop",
                                        "@ACS_chk63DocCopPedidoRec",
                                        "@ACS_txt63DocCopPedidoRecCop",
                                        "@ACS_chk63DocRemisionEnt",
                                        "@ACS_txt63DocRemisionEntCop",
                                        "@ACS_chk63DocRemisionRec",
                                        "@ACS_txt63DocRemisionRecCop",
                                        "@ACS_chk63DocFolioEnt",
                                        "@ACS_txt63DocFolioEntCop",
                                        "@ACS_chk63DocFolioRec",
                                        "@ACS_txt63DocFolioRecCop",
                                        "@ACS_chk63DocContraRecEnt",
                                        "@ACS_txt63DocContraRecEntCop",
                                        "@ACS_chk63DocContraRecRec",
                                        "@ACS_txt63DocContraRecRecCop",
                                        "@ACS_chk63DocEntAlmacenEnt",
                                        "@ACS_txt63DocEntAlmacenEntCop",
                                        "@ACS_chk63DocEntAlmacenRec",
                                        "@ACS_txt63DocEntAlmacenRecCop",
                                        "@ACS_chk63DocSopServicioEnt",
                                        "@ACS_txt63DocSopServicioEntCop",
                                        "@ACS_chk63DocSopServicioRec",
                                        "@ACS_txt63DocSopServicioRecCop",
                                        "@ACS_chk63DocNomFirmaEnt",
                                        "@ACS_txt63DocNomFirmaEntCop",
                                        "@ACS_chk63DocNomFirmaoRec",
                                        "@ACS_txt63DocNomFirmaRecCop",
                                        "@ACS_chk63CitaEnt",
                                        "@ACS_txt63CitaEntCop",
                                        "@ACS_chk63CitaRec",
                                        "@ACS_txt63CitaRecCop"
                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Vis_Frecuencia,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U,
                                        acys.Acs_Modalidad,
                                       acys.IdCte_DirEntrega,
                                        acys.Acs_Sucursal,
                                        acys.Acs_ParcialidadesSi,
                                        acys.Acs_ParcialidadesNo,
                                        acys.Acs_ConfirmacionPedidosSI,
                                        acys.Acs_ConfirmacionPedidosnO,
                                        acys.Acs_chkRecRevLunes,
                                        acys.Acs_RecRevMartes,
                                        acys.Acs_RecRevMiercoles,
                                        acys.Acs_RecRevJueves,
                                        acys.Acs_RecRevViernes,
                                        acys.Acs_RecRevSabado,
                                        acys.Acs_TimePicker1,
                                        acys.Acs_TimePicker2,
                                        acys.Acs_TimePicker3,
                                        acys.Acs_TimePicker4,
                                        acys.Acs_RecPersonaRecibe,
                                        acys.Acs_RecPuesto,
                                        acys.Acs_RecCitaMismoDia,
                                        acys.Acs_RecCitaSinCita,
                                        acys.Acs_RecCitaPrevia,
                                        acys.Acs_RecCitaContacto,
                                        acys.Acs_RecCitaTelefono,
                                        acys.Acs_RecCitaDiasdeAnticipacion,
                                        acys.Acs_RecAreaPropia,
                                        acys.Acs_RecAreaPlaza,
                                        acys.Acs_RecAreaCalle,
                                        acys.Acs_RecAreaAvTransitada,
                                        acys.Acs_RecEstCortesia,
                                        acys.Acs_RecEstCosto,
                                        acys.Acs_RecEstMonto,
                                        acys.Acs_RecDocFactFranquiciaEnt,
                                        acys.Acs_RecDocFactFranquiciaEntCop,
                                        acys.Acs_RecDocFactFranquiciaRec,
                                        acys.Acs_RecDocFactFranquiciaRecCop,
                                        acys.Acs_RecDocFactKeyEnt,
                                        acys.Acs_RecDocFactKeyEntCop,
                                        acys.Acs_RecDocFactKeyRec,
                                        acys.Acs_RecDocFactKeyRecCop,
                                        acys.Acs_RecDocOrdCompraEnt,
                                        acys.Acs_RecDocOrdCompraEntCop,
                                        acys.Acs_RecDocOrdCompraRec,
                                        acys.Acs_RecDocOrdCompraRecCop,
                                        acys.Acs_RecDocOrdReposEnt,
                                        acys.Acs_RecDocOrdReposEntCop,
                                        acys.Acs_RecDocOrdReposRec,
                                        acys.Acs_RecDocOrdReposRecCop,
                                        acys.Acs_RecDocCopPedidoEnt,
                                        acys.Acs_RecDocCopPedidoEntCop,
                                        acys.Acs_RecDocCopPedidoRec,
                                        acys.Acs_RecDocCopPedidoRecCop,
                                        acys.ACS_RecDocRemisionEnt,
                                        acys.ACS_RecDocRemisionEntCop,
                                        acys.ACS_RecDocRemisionRec,
                                        acys.ACS_RecDocRemisionRecCop,
                                        acys.ACS_RecDocFolioEnt,
                                        acys.ACS_RecDocFolioEntCop,
                                        acys.ACS_RecDocFolioRec,
                                        acys.ACS_RecDocFolioRecCop,
                                        acys.ACS_RecDocContraRecEnt,
                                        acys.ACS_RecDocContraRecEntCop,
                                        acys.ACS_RecDocContraRecRec,
                                        acys.ACS_RecDocContraRecRecCop,
                                        acys.ACS_RecDocEntAlmacenEnt,
                                        acys.ACS_RecDocEntAlmacenEntCop,
                                        acys.ACS_RecDocEntAlmacenRec,
                                        acys.ACS_RecDocEntAlmacenRecCop,
                                        acys.ACS_RecDocSopServicioEnt,
                                        acys.ACS_RecDocSopServicioEntCop,
                                        acys.ACS_RecDocSopServicioRec,
                                        acys.ACS_RecDocSopServicioRecCop,
                                        acys.ACS_RecDocNomFirmaEnt,
                                        acys.ACS_RecDocNomFirmaEntCop,
                                        acys.ACS_RecDocNomFirmaoRec,
                                        acys.ACS_RecDocNomFirmaRecCop,
                                        acys.ACS_RecCitaEnt,
                                        acys.ACS_RecCitaEntCop,
                                        acys.ACS_RecCitaRec,
                                        acys.ACS_RecCitaRecCop,
                                        acys.ACS_RecOtroRec,
                                    acys.ACS_chk62Lunes,
                                    acys.ACS_chk62Martes,
                                    acys.ACS_chk62Miercoles,
                                    acys.ACS_chk62Jueves,
                                    acys.ACS_chk62Viernes,
                                    acys.ACS_chk62Sabado,
                                    acys.ACS_RadTimePicker162,
                                    acys.ACS_RadTimePicker262,
                                    acys.ACS_RadTimePicker362,
                                    acys.ACS_RadTimePicker462,
                                    acys.ACS_txtRecPersonaRecibe62,
                                    acys.ACS_txtRecPuesto62,
                                    acys.ACS_Chk62Mismodia,
                                    acys.ACS_Chk62Sincita,
                                    acys.ACS_Chk62Previa,
                                    acys.ACS_txt62CitaContacto,
                                    acys.ACS_txt62CitaTelefono,
                                    acys.ACS_txt62CitaDiasdeAnticipacion,
                                    acys.ACS_chk62AreaPropia,
                                    acys.ACS_chk62AreaPlaza,
                                    acys.ACS_chk62AreaCalle,
                                    acys.ACS_chk62AreaAvTransitada,
                                    acys.ACS_chk62EstCortesia,
                                    acys.ACS_chk62EstCosto,
                                    acys.ACS_txt62EstMonto,
                                    acys.ACS_txt62ClienteDireccion,
                                    acys.ACS_txt62ClienteColonia,
                                    acys.ACS_txt62ClienteMunicipio,
                                    acys.ACS_txt62ClienteEstado,
                                    acys.ACS_txt62ClienteCodPost,
                                    acys.ACS_chk62DocFactFranquiciaEnt,
                                    acys.ACS_txt62DocFactFranquiciaEntCop,
                                    acys.ACS_chk62DocFactFranquiciaRec,
                                    acys.ACS_txt62DocFactFranquiciaRecCop,
                                    acys.ACS_chk62DocFactKeyEnt,
                                    acys.ACS_txt62DocFactKeyEntCop,
                                    acys.ACS_chk62DocFactKeyRec,
                                    acys.ACS_txt62DocFactKeyRecCop,
                                    acys.ACS_chk62DocOrdCompraEnt,
                                    acys.ACS_txt62DocOrdCompraEntCop,
                                    acys.ACS_chk62DocOrdCompraRec,
                                    acys.ACS_txt62DocOrdCompraRecCop,
                                    acys.ACS_chk62DocOrdReposEnt,
                                    acys.ACS_txt62DocOrdReposEntCop,
                                    acys.ACS_chk62DocOrdReposRec,
                                    acys.ACS_txt62DocOrdReposRecCop,
                                    acys.ACS_chk62DocCopPedidoEnt,
                                    acys.ACS_txt62DocCopPedidoEntCop,
                                    acys.ACS_chk62DocCopPedidoRec,
                                    acys.ACS_txt62DocCopPedidoRecCop,
                                    acys.ACS_chk62DocRemisionEnt,
                                    acys.ACS_txt62DocRemisionEntCop,
                                    acys.ACS_chk62DocRemisionRec,
                                    acys.ACS_txt62DocRemisionRecCop,
                                    acys.ACS_chk62DocFolioEnt,
                                    acys.ACS_txt62DocFolioEntCop,
                                    acys.ACS_chk62DocFolioRec,
                                    acys.ACS_txt62DocFolioRecCop,
                                    acys.ACS_chk62DocContraRecEnt,
                                    acys.ACS_txt62DocContraRecEntCop,
                                    acys.ACS_chk62DocContraRecRec,
                                    acys.ACS_txt62DocContraRecRecCop,
                                    acys.ACS_chk62DocEntAlmacenEnt,
                                    acys.ACS_txt62DocEntAlmacenEntCop,
                                    acys.ACS_chk62DocEntAlmacenRec,
                                    acys.ACS_txt62DocEntAlmacenRecCop,
                                    acys.ACS_chk62DocSopServicioEnt,
                                    acys.ACS_txt62DocSopServicioEntCop,
                                    acys.ACS_chk62DocSopServicioRec,
                                    acys.ACS_txt62DocSopServicioRecCop,
                                    acys.ACS_chk62DocNomFirmaEnt,
                                    acys.ACS_txt62DocNomFirmaEntCop,
                                    acys.ACS_chk62DocNomFirmaoRec,
                                    acys.ACS_txt62DocNomFirmaRecCop,
                                    acys.ACS_chk62CitaEnt,
                                    acys.ACS_txt62CitaEntCop,
                                    acys.ACS_chk62CitaRec,
                                    acys.ACS_txt62CitaRecCop,
                                    acys.ACS_chk63Lunes,
                                    acys.ACS_chk63Martes,
                                    acys.ACS_chk63Miercoles,
                                    acys.ACS_chk63Jueves,
                                    acys.ACS_chk63Viernes,
                                    acys.ACS_chk63Sabado,
                                    acys.ACS_Rad63TimePicker163,
                                    acys.ACS_Rad63TimePicker263,
                                    acys.ACS_Rad63TimePicker363,
                                    acys.ACS_Rad63TimePicker463,
                                    acys.ACS_txtRecPersonaRecibe63,
                                    acys.ACS_txtRecPuesto63,
                                    acys.ACS_Chk63Mismodia,
                                    acys.ACS_Chk63Sincita,
                                    acys.ACS_Chk63Previa,
                                    acys.ACS_txt63CitaContacto,
                                    acys.ACS_txt63CitaTelefono,
                                    acys.ACS_txt63CitaDiasdeAnticipacion,
                                    acys.ACS_chk63AreaPropia,
                                    acys.ACS_chk63AreaPlaza,
                                    acys.ACS_chk63AreaCalle,
                                    acys.ACS_chk63AreaAvTransitada,
                                    acys.ACS_chk63EstCortesia,
                                    acys.ACS_chk63EstCosto,
                                    acys.ACS_txt63EstMonto,
                                    acys.ACS_txt63ClienteDireccion,
                                    acys.ACS_txt63ClienteColonia,
                                    acys.ACS_txt63ClienteMunicipio,
                                    acys.ACS_txt63ClienteEstado,
                                    acys.ACS_txt63ClienteCodPost,
                                    acys.ACS_chk63DocFactFranquiciaEnt,
                                    acys.ACS_txt63DocFactFranquiciaEntCop,
                                    acys.ACS_chk63DocFactFranquiciaRec,
                                    acys.ACS_txt63DocFactFranquiciaRecCop,
                                    acys.ACS_chk63DocFactKeyEnt,
                                    acys.ACS_txt63DocFactKeyEntCop,
                                    acys.ACS_chk63DocFactKeyRec,
                                    acys.ACS_txt63DocFactKeyRecCop,
                                    acys.ACS_chk63DocOrdCompraEnt,
                                    acys.ACS_txt63DocOrdCompraEntCop,
                                    acys.ACS_chk63DocOrdCompraRec,
                                    acys.ACS_txt63DocOrdCompraRecCop,
                                    acys.ACS_chk63DocOrdReposEnt,
                                    acys.ACS_txt63DocOrdReposEntCop,
                                    acys.ACS_chk63DocOrdReposRec,
                                    acys.ACS_txt63DocOrdReposRecCop,
                                    acys.ACS_chk63DocCopPedidoEnt,
                                    acys.ACS_txt63DocCopPedidoEntCop,
                                    acys.ACS_chk63DocCopPedidoRec,
                                    acys.ACS_txt63DocCopPedidoRecCop,
                                    acys.ACS_chk63DocRemisionEnt,
                                    acys.ACS_txt63DocRemisionEntCop,
                                    acys.ACS_chk63DocRemisionRec,
                                    acys.ACS_txt63DocRemisionRecCop,
                                    acys.ACS_chk63DocFolioEnt,
                                    acys.ACS_txt63DocFolioEntCop,
                                    acys.ACS_chk63DocFolioRec,
                                    acys.ACS_txt63DocFolioRecCop,
                                    acys.ACS_chk63DocContraRecEnt,
                                    acys.ACS_txt63DocContraRecEntCop,
                                    acys.ACS_chk63DocContraRecRec,
                                    acys.ACS_txt63DocContraRecRecCop,
                                    acys.ACS_chk63DocEntAlmacenEnt,
                                    acys.ACS_txt63DocEntAlmacenEntCop,
                                    acys.ACS_chk63DocEntAlmacenRec,
                                    acys.ACS_txt63DocEntAlmacenRecCop,
                                    acys.ACS_chk63DocSopServicioEnt,
                                    acys.ACS_txt63DocSopServicioEntCop,
                                    acys.ACS_chk63DocSopServicioRec,
                                    acys.ACS_txt63DocSopServicioRecCop,
                                    acys.ACS_chk63DocNomFirmaEnt,
                                    acys.ACS_txt63DocNomFirmaEntCop,
                                    acys.ACS_chk63DocNomFirmaoRec,
                                    acys.ACS_txt63DocNomFirmaRecCop,
                                    acys.ACS_chk63CitaEnt,
                                    acys.ACS_txt63CitaEntCop,
                                    acys.ACS_chk63CitaRec,
                                    acys.ACS_txt63CitaRecCop
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Insertar", ref verificador, Parametros, Valores);

                acys.Id_Acs = verificador;

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_FechaInicio",
                            "@Acs_FechaFin",
                            "@Acs_CantTotal",
                            "@Id_TG"
                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
                            acys.Id_AcsVersion,
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,                            
                            list[x].Acys_FechaInicio,
                            list[x].Acys_FechaFin,
                            list[x].Acys_CantTotal,
                            list[x].Id_TG
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, Parametros, Valores);
                }
                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "Id_Acsversion", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni, a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores);
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoMensual, a.ServTecnicoRellenoMensualfechaIni, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores);
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores);
                    modificar = 0;
                }

                //Datos Garantia    
                int verificador5 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@FactorGarantia", "@UPrimaNeta", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, a.FactorGarantia, a.UPrimaNeta, DateTime.Now };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spInsCapAcysDatosGarantia", ref verificador5, Parametros, Valores);
                }

                // Calendario Fechas de Corte
                int verificador6 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@Mes", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    if (a.Fechas_Corte != null)
                        for (int i = 1; i <= 12; i++)
                        {
                            if (!a.Fechas_Corte.ContainsKey(i)) continue;
                            Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, i, a.Fechas_Corte[i] };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spInsCapAcysDatosGarantia_Fechas", ref verificador5, Parametros, Valores);
                        }
                }


                //Actualizar Calendario
                ActualizaControlCalendario(acys, ValoresCalendario, Conexion);

                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };

                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            acys.Id_AcsVersion,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                            eliminar++;
                        }
                    }
                }

                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        /// <summary>
        /// Crea una entrada en el repositorio de la entidad [CapAcys], así como entradas adicionales en los repositorios de las entidades de detalle del acys.
        /// </summary>
        /// <param name="acys"></param>
        /// <param name="list"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="verificador"></param>
        /// <param name="asesorias"></param>
        /// <param name="servicios"></param>
        /// <param name="serviciosMantenimiento"></param>
        /// <param name="listaGarantia"></param>
        /// <param name="ValoresCalendario"></param>
        /// <param name="idcCtx">Contexto de la conexión a la fuente de datos</param>
        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario, ICD_Contexto idcCtx)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            //SqlCommand sqlcmd = default(SqlCommand);
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)idcCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
            sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisFrecuencia",
                                          "@Acs_VisitaOtro",
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U",
                                          "@Acs_Modalidad",
                                          "@Id_CteDirEntrega",
                                            "@Acs_Sucursal",
                                            "@Acs_ParcialidadesSi",
                                            "@Acs_ParcialidadesNo",
                                            "@Acs_ConfirmacionPedidosSI",
                                            "@Acs_ConfirmacionPedidosnO",
                                            "@Acs_chkRecRevLunes",
                                            "@Acs_RecRevMartes",
                                            "@Acs_RecRevMiercoles",
                                            "@Acs_RecRevJueves",
                                            "@Acs_RecRevViernes",
                                            "@Acs_RecRevSabado",
                                            "@Acs_TimePicker1",
                                            "@Acs_TimePicker2",
                                            "@Acs_TimePicker3",
                                            "@Acs_TimePicker4",
                                            "@Acs_RecPersonaRecibe",
                                            "@Acs_RecPuesto",
                                            "@Acs_RecCitaMismoDia",
                                            "@Acs_RecCitaSinCita",
                                            "@Acs_RecCitaPrevia",
                                            "@Acs_RecCitaContacto",
                                            "@Acs_RecCitaTelefono",
                                            "@Acs_RecCitaDiasdeAnticipacion",
                                            "@Acs_RecAreaPropia",
                                            "@Acs_RecAreaPlaza",
                                            "@Acs_RecAreaCalle",
                                            "@Acs_RecAreaAvTransitada",
                                            "@Acs_RecEstCortesia",
                                            "@Acs_RecEstCosto",
                                            "@Acs_RecEstMonto",
                                            "@Acs_RecDocFactFranquiciaEnt",
                                            "@Acs_RecDocFactFranquiciaEntCop",
                                            "@Acs_RecDocFactFranquiciaRec",
                                            "@Acs_RecDocFactFranquiciaRecCop",
                                            "@Acs_RecDocFactKeyEnt",
                                            "@Acs_RecDocFactKeyEntCop",
                                            "@Acs_RecDocFactKeyRec",
                                            "@Acs_RecDocFactKeyRecCop",
                                            "@Acs_RecDocOrdCompraEnt",
                                            "@Acs_RecDocOrdCompraEntCop",
                                            "@Acs_RecDocOrdCompraRec",
                                            "@Acs_RecDocOrdCompraRecCop",
                                            "@Acs_RecDocOrdReposEnt",
                                            "@Acs_RecDocOrdReposEntCop",
                                            "@Acs_RecDocOrdReposRec",
                                            "@Acs_RecDocOrdReposRecCop",
                                            "@Acs_RecDocCopPedidoEnt",
                                            "@Acs_RecDocCopPedidoEntCop",
                                            "@Acs_RecDocCopPedidoRec",
                                            "@Acs_RecDocCopPedidoRecCop",
                                            "@ACS_RecDocRemisionEnt",
                                            "@ACS_RecDocRemisionEntCop",
                                            "@ACS_RecDocRemisionRec",
                                            "@ACS_RecDocRemisionRecCop",
                                            "@ACS_RecDocFolioEnt",
                                            "@ACS_RecDocFolioEntCop",
                                            "@ACS_RecDocFolioRec",
                                            "@ACS_RecDocFolioRecCop",
                                            "@ACS_RecDocContraRecEnt",
                                            "@ACS_RecDocContraRecEntCop",
                                            "@ACS_RecDocContraRecRec",
                                            "@ACS_RecDocContraRecRecCop",
                                            "@ACS_RecDocEntAlmacenEnt",
                                            "@ACS_RecDocEntAlmacenEntCop",
                                            "@ACS_RecDocEntAlmacenRec",
                                            "@ACS_RecDocEntAlmacenRecCop",
                                            "@ACS_RecDocSopServicioEnt",
                                            "@ACS_RecDocSopServicioEntCop",
                                            "@ACS_RecDocSopServicioRec",
                                            "@ACS_RecDocSopServicioRecCop",
                                            "@ACS_RecDocNomFirmaEnt",
                                            "@ACS_RecDocNomFirmaEntCop",
                                            "@ACS_RecDocNomFirmaoRec",
                                            "@ACS_RecDocNomFirmaRecCop",
                                            "@ACS_RecCitaEnt",
                                            "@ACS_RecCitaEntCop",
                                            "@ACS_RecCitaRec",
                                            "@ACS_RecCitaRecCop",
                                            "@ACS_RecOtroRec",
                                        "@ACS_chk62Lunes",
                                        "@ACS_chk62Martes",
                                        "@ACS_chk62Miercoles",
                                        "@ACS_chk62Jueves",
                                        "@ACS_chk62Viernes",
                                        "@ACS_chk62Sabado",
                                        "@ACS_RadTimePicker162",
                                        "@ACS_RadTimePicker262",
                                        "@ACS_RadTimePicker362",
                                        "@ACS_RadTimePicker462",
                                        "@ACS_txtRecPersonaRecibe62",
                                        "@ACS_txtRecPuesto62",
                                        "@ACS_Chk62Mismodia",
                                        "@ACS_Chk62Sincita",
                                        "@ACS_Chk62Previa",
                                        "@ACS_txt62CitaContacto",
                                        "@ACS_txt62CitaTelefono",
                                        "@ACS_txt62CitaDiasdeAnticipacion",
                                        "@ACS_chk62AreaPropia",
                                        "@ACS_chk62AreaPlaza",
                                        "@ACS_chk62AreaCalle",
                                        "@ACS_chk62AreaAvTransitada",
                                        "@ACS_chk62EstCortesia",
                                        "@ACS_chk62EstCosto",
                                        "@ACS_txt62EstMonto",
                                        "@ACS_txt62ClienteDireccion",
                                        "@ACS_txt62ClienteColonia",
                                        "@ACS_txt62ClienteMunicipio",
                                        "@ACS_txt62ClienteEstado",
                                        "@ACS_txt62ClienteCodPost",
                                        "@ACS_chk62DocFactFranquiciaEnt",
                                        "@ACS_txt62DocFactFranquiciaEntCop",
                                        "@ACS_chk62DocFactFranquiciaRec",
                                        "@ACS_txt62DocFactFranquiciaRecCop",
                                        "@ACS_chk62DocFactKeyEnt",
                                        "@ACS_txt62DocFactKeyEntCop",
                                        "@ACS_chk62DocFactKeyRec",
                                        "@ACS_txt62DocFactKeyRecCop",
                                        "@ACS_chk62DocOrdCompraEnt",
                                        "@ACS_txt62DocOrdCompraEntCop",
                                        "@ACS_chk62DocOrdCompraRec",
                                        "@ACS_txt62DocOrdCompraRecCop",
                                        "@ACS_chk62DocOrdReposEnt",
                                        "@ACS_txt62DocOrdReposEntCop",
                                        "@ACS_chk62DocOrdReposRec",
                                        "@ACS_txt62DocOrdReposRecCop",
                                        "@ACS_chk62DocCopPedidoEnt",
                                        "@ACS_txt62DocCopPedidoEntCop",
                                        "@ACS_chk62DocCopPedidoRec",
                                        "@ACS_txt62DocCopPedidoRecCop",
                                        "@ACS_chk62DocRemisionEnt",
                                        "@ACS_txt62DocRemisionEntCop",
                                        "@ACS_chk62DocRemisionRec",
                                        "@ACS_txt62DocRemisionRecCop",
                                        "@ACS_chk62DocFolioEnt",
                                        "@ACS_txt62DocFolioEntCop",
                                        "@ACS_chk62DocFolioRec",
                                        "@ACS_txt62DocFolioRecCop",
                                        "@ACS_chk62DocContraRecEnt",
                                        "@ACS_txt62DocContraRecEntCop",
                                        "@ACS_chk62DocContraRecRec",
                                        "@ACS_txt62DocContraRecRecCop",
                                        "@ACS_chk62DocEntAlmacenEnt",
                                        "@ACS_txt62DocEntAlmacenEntCop",
                                        "@ACS_chk62DocEntAlmacenRec",
                                        "@ACS_txt62DocEntAlmacenRecCop",
                                        "@ACS_chk62DocSopServicioEnt",
                                        "@ACS_txt62DocSopServicioEntCop",
                                        "@ACS_chk62DocSopServicioRec",
                                        "@ACS_txt62DocSopServicioRecCop",
                                        "@ACS_chk62DocNomFirmaEnt",
                                        "@ACS_txt62DocNomFirmaEntCop",
                                        "@ACS_chk62DocNomFirmaoRec",
                                        "@ACS_txt62DocNomFirmaRecCop",
                                        "@ACS_chk62CitaEnt",
                                        "@ACS_txt62CitaEntCop",
                                        "@ACS_chk62CitaRec",
                                        "@ACS_txt62CitaRecCop",
                                        "@ACS_chk63Lunes",
                                        "@ACS_chk63Martes",
                                        "@ACS_chk63Miercoles",
                                        "@ACS_chk63Jueves",
                                        "@ACS_chk63Viernes",
                                        "@ACS_chk63Sabado",
                                        "@ACS_Rad63TimePicker163",
                                        "@ACS_Rad63TimePicker263",
                                        "@ACS_Rad63TimePicker363",
                                        "@ACS_Rad63TimePicker463",
                                        "@ACS_txtRecPersonaRecibe63",
                                        "@ACS_txtRecPuesto63",
                                        "@ACS_Chk63Mismodia",
                                        "@ACS_Chk63Sincita",
                                        "@ACS_Chk63Previa",
                                        "@ACS_txt63CitaContacto",
                                        "@ACS_txt63CitaTelefono",
                                        "@ACS_txt63CitaDiasdeAnticipacion",
                                        "@ACS_chk63AreaPropia",
                                        "@ACS_chk63AreaPlaza",
                                        "@ACS_chk63AreaCalle",
                                        "@ACS_chk63AreaAvTransitada",
                                        "@ACS_chk63EstCortesia",
                                        "@ACS_chk63EstCosto",
                                        "@ACS_txt63EstMonto",
                                        "@ACS_txt63ClienteDireccion",
                                        "@ACS_txt63ClienteColonia",
                                        "@ACS_txt63ClienteMunicipio",
                                        "@ACS_txt63ClienteEstado",
                                        "@ACS_txt63ClienteCodPost",
                                        "@ACS_chk63DocFactFranquiciaEnt",
                                        "@ACS_txt63DocFactFranquiciaEntCop",
                                        "@ACS_chk63DocFactFranquiciaRec",
                                        "@ACS_txt63DocFactFranquiciaRecCop",
                                        "@ACS_chk63DocFactKeyEnt",
                                        "@ACS_txt63DocFactKeyEntCop",
                                        "@ACS_chk63DocFactKeyRec",
                                        "@ACS_txt63DocFactKeyRecCop",
                                        "@ACS_chk63DocOrdCompraEnt",
                                        "@ACS_txt63DocOrdCompraEntCop",
                                        "@ACS_chk63DocOrdCompraRec",
                                        "@ACS_txt63DocOrdCompraRecCop",
                                        "@ACS_chk63DocOrdReposEnt",
                                        "@ACS_txt63DocOrdReposEntCop",
                                        "@ACS_chk63DocOrdReposRec",
                                        "@ACS_txt63DocOrdReposRecCop",
                                        "@ACS_chk63DocCopPedidoEnt",
                                        "@ACS_txt63DocCopPedidoEntCop",
                                        "@ACS_chk63DocCopPedidoRec",
                                        "@ACS_txt63DocCopPedidoRecCop",
                                        "@ACS_chk63DocRemisionEnt",
                                        "@ACS_txt63DocRemisionEntCop",
                                        "@ACS_chk63DocRemisionRec",
                                        "@ACS_txt63DocRemisionRecCop",
                                        "@ACS_chk63DocFolioEnt",
                                        "@ACS_txt63DocFolioEntCop",
                                        "@ACS_chk63DocFolioRec",
                                        "@ACS_txt63DocFolioRecCop",
                                        "@ACS_chk63DocContraRecEnt",
                                        "@ACS_txt63DocContraRecEntCop",
                                        "@ACS_chk63DocContraRecRec",
                                        "@ACS_txt63DocContraRecRecCop",
                                        "@ACS_chk63DocEntAlmacenEnt",
                                        "@ACS_txt63DocEntAlmacenEntCop",
                                        "@ACS_chk63DocEntAlmacenRec",
                                        "@ACS_txt63DocEntAlmacenRecCop",
                                        "@ACS_chk63DocSopServicioEnt",
                                        "@ACS_txt63DocSopServicioEntCop",
                                        "@ACS_chk63DocSopServicioRec",
                                        "@ACS_txt63DocSopServicioRecCop",
                                        "@ACS_chk63DocNomFirmaEnt",
                                        "@ACS_txt63DocNomFirmaEntCop",
                                        "@ACS_chk63DocNomFirmaoRec",
                                        "@ACS_txt63DocNomFirmaRecCop",
                                        "@ACS_chk63CitaEnt",
                                        "@ACS_txt63CitaEntCop",
                                        "@ACS_chk63CitaRec",
                                        "@ACS_txt63CitaRecCop"

                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Vis_Frecuencia,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U,
                                       acys.Acs_Modalidad,
                                       acys.IdCte_DirEntrega,
                                        acys.Acs_Sucursal,
                                        acys.Acs_ParcialidadesSi,
                                        acys.Acs_ParcialidadesNo,
                                        acys.Acs_ConfirmacionPedidosSI,
                                        acys.Acs_ConfirmacionPedidosnO,
                                        acys.Acs_chkRecRevLunes,
                                        acys.Acs_RecRevMartes,
                                        acys.Acs_RecRevMiercoles,
                                        acys.Acs_RecRevJueves,
                                        acys.Acs_RecRevViernes,
                                        acys.Acs_RecRevSabado,
                                        acys.Acs_TimePicker1,
                                        acys.Acs_TimePicker2,
                                        acys.Acs_TimePicker3,
                                        acys.Acs_TimePicker4,
                                        acys.Acs_RecPersonaRecibe,
                                        acys.Acs_RecPuesto,
                                        acys.Acs_RecCitaMismoDia,
                                        acys.Acs_RecCitaSinCita,
                                        acys.Acs_RecCitaPrevia,
                                        acys.Acs_RecCitaContacto,
                                        acys.Acs_RecCitaTelefono,
                                        acys.Acs_RecCitaDiasdeAnticipacion,
                                        acys.Acs_RecAreaPropia,
                                        acys.Acs_RecAreaPlaza,
                                        acys.Acs_RecAreaCalle,
                                        acys.Acs_RecAreaAvTransitada,
                                        acys.Acs_RecEstCortesia,
                                        acys.Acs_RecEstCosto,
                                        acys.Acs_RecEstMonto,
                                        acys.Acs_RecDocFactFranquiciaEnt,
                                        acys.Acs_RecDocFactFranquiciaEntCop,
                                        acys.Acs_RecDocFactFranquiciaRec,
                                        acys.Acs_RecDocFactFranquiciaRecCop,
                                        acys.Acs_RecDocFactKeyEnt,
                                        acys.Acs_RecDocFactKeyEntCop,
                                        acys.Acs_RecDocFactKeyRec,
                                        acys.Acs_RecDocFactKeyRecCop,
                                        acys.Acs_RecDocOrdCompraEnt,
                                        acys.Acs_RecDocOrdCompraEntCop,
                                        acys.Acs_RecDocOrdCompraRec,
                                        acys.Acs_RecDocOrdCompraRecCop,
                                        acys.Acs_RecDocOrdReposEnt,
                                        acys.Acs_RecDocOrdReposEntCop,
                                        acys.Acs_RecDocOrdReposRec,
                                        acys.Acs_RecDocOrdReposRecCop,
                                        acys.Acs_RecDocCopPedidoEnt,
                                        acys.Acs_RecDocCopPedidoEntCop,
                                        acys.Acs_RecDocCopPedidoRec,
                                        acys.Acs_RecDocCopPedidoRecCop,
                                        acys.ACS_RecDocRemisionEnt,
                                        acys.ACS_RecDocRemisionEntCop,
                                        acys.ACS_RecDocRemisionRec,
                                        acys.ACS_RecDocRemisionRecCop,
                                        acys.ACS_RecDocFolioEnt,
                                        acys.ACS_RecDocFolioEntCop,
                                        acys.ACS_RecDocFolioRec,
                                        acys.ACS_RecDocFolioRecCop,
                                        acys.ACS_RecDocContraRecEnt,
                                        acys.ACS_RecDocContraRecEntCop,
                                        acys.ACS_RecDocContraRecRec,
                                        acys.ACS_RecDocContraRecRecCop,
                                        acys.ACS_RecDocEntAlmacenEnt,
                                        acys.ACS_RecDocEntAlmacenEntCop,
                                        acys.ACS_RecDocEntAlmacenRec,
                                        acys.ACS_RecDocEntAlmacenRecCop,
                                        acys.ACS_RecDocSopServicioEnt,
                                        acys.ACS_RecDocSopServicioEntCop,
                                        acys.ACS_RecDocSopServicioRec,
                                        acys.ACS_RecDocSopServicioRecCop,
                                        acys.ACS_RecDocNomFirmaEnt,
                                        acys.ACS_RecDocNomFirmaEntCop,
                                        acys.ACS_RecDocNomFirmaoRec,
                                        acys.ACS_RecDocNomFirmaRecCop,
                                        acys.ACS_RecCitaEnt,
                                        acys.ACS_RecCitaEntCop,
                                        acys.ACS_RecCitaRec,
                                        acys.ACS_RecCitaRecCop,
                                        acys.ACS_RecOtroRec,
                                    acys.ACS_chk62Lunes,
                                    acys.ACS_chk62Martes,
                                    acys.ACS_chk62Miercoles,
                                    acys.ACS_chk62Jueves,
                                    acys.ACS_chk62Viernes,
                                    acys.ACS_chk62Sabado,
                                    acys.ACS_RadTimePicker162,
                                    acys.ACS_RadTimePicker262,
                                    acys.ACS_RadTimePicker362,
                                    acys.ACS_RadTimePicker462,
                                    acys.ACS_txtRecPersonaRecibe62,
                                    acys.ACS_txtRecPuesto62,
                                    acys.ACS_Chk62Mismodia,
                                    acys.ACS_Chk62Sincita,
                                    acys.ACS_Chk62Previa,
                                    acys.ACS_txt62CitaContacto,
                                    acys.ACS_txt62CitaTelefono,
                                    acys.ACS_txt62CitaDiasdeAnticipacion,
                                    acys.ACS_chk62AreaPropia,
                                    acys.ACS_chk62AreaPlaza,
                                    acys.ACS_chk62AreaCalle,
                                    acys.ACS_chk62AreaAvTransitada,
                                    acys.ACS_chk62EstCortesia,
                                    acys.ACS_chk62EstCosto,
                                    acys.ACS_txt62EstMonto,
                                    acys.ACS_txt62ClienteDireccion,
                                    acys.ACS_txt62ClienteColonia,
                                    acys.ACS_txt62ClienteMunicipio,
                                    acys.ACS_txt62ClienteEstado,
                                    acys.ACS_txt62ClienteCodPost,
                                    acys.ACS_chk62DocFactFranquiciaEnt,
                                    acys.ACS_txt62DocFactFranquiciaEntCop,
                                    acys.ACS_chk62DocFactFranquiciaRec,
                                    acys.ACS_txt62DocFactFranquiciaRecCop,
                                    acys.ACS_chk62DocFactKeyEnt,
                                    acys.ACS_txt62DocFactKeyEntCop,
                                    acys.ACS_chk62DocFactKeyRec,
                                    acys.ACS_txt62DocFactKeyRecCop,
                                    acys.ACS_chk62DocOrdCompraEnt,
                                    acys.ACS_txt62DocOrdCompraEntCop,
                                    acys.ACS_chk62DocOrdCompraRec,
                                    acys.ACS_txt62DocOrdCompraRecCop,
                                    acys.ACS_chk62DocOrdReposEnt,
                                    acys.ACS_txt62DocOrdReposEntCop,
                                    acys.ACS_chk62DocOrdReposRec,
                                    acys.ACS_txt62DocOrdReposRecCop,
                                    acys.ACS_chk62DocCopPedidoEnt,
                                    acys.ACS_txt62DocCopPedidoEntCop,
                                    acys.ACS_chk62DocCopPedidoRec,
                                    acys.ACS_txt62DocCopPedidoRecCop,
                                    acys.ACS_chk62DocRemisionEnt,
                                    acys.ACS_txt62DocRemisionEntCop,
                                    acys.ACS_chk62DocRemisionRec,
                                    acys.ACS_txt62DocRemisionRecCop,
                                    acys.ACS_chk62DocFolioEnt,
                                    acys.ACS_txt62DocFolioEntCop,
                                    acys.ACS_chk62DocFolioRec,
                                    acys.ACS_txt62DocFolioRecCop,
                                    acys.ACS_chk62DocContraRecEnt,
                                    acys.ACS_txt62DocContraRecEntCop,
                                    acys.ACS_chk62DocContraRecRec,
                                    acys.ACS_txt62DocContraRecRecCop,
                                    acys.ACS_chk62DocEntAlmacenEnt,
                                    acys.ACS_txt62DocEntAlmacenEntCop,
                                    acys.ACS_chk62DocEntAlmacenRec,
                                    acys.ACS_txt62DocEntAlmacenRecCop,
                                    acys.ACS_chk62DocSopServicioEnt,
                                    acys.ACS_txt62DocSopServicioEntCop,
                                    acys.ACS_chk62DocSopServicioRec,
                                    acys.ACS_txt62DocSopServicioRecCop,
                                    acys.ACS_chk62DocNomFirmaEnt,
                                    acys.ACS_txt62DocNomFirmaEntCop,
                                    acys.ACS_chk62DocNomFirmaoRec,
                                    acys.ACS_txt62DocNomFirmaRecCop,
                                    acys.ACS_chk62CitaEnt,
                                    acys.ACS_txt62CitaEntCop,
                                    acys.ACS_chk62CitaRec,
                                    acys.ACS_txt62CitaRecCop,
                                    acys.ACS_chk63Lunes,
                                    acys.ACS_chk63Martes,
                                    acys.ACS_chk63Miercoles,
                                    acys.ACS_chk63Jueves,
                                    acys.ACS_chk63Viernes,
                                    acys.ACS_chk63Sabado,
                                    acys.ACS_Rad63TimePicker163,
                                    acys.ACS_Rad63TimePicker263,
                                    acys.ACS_Rad63TimePicker363,
                                    acys.ACS_Rad63TimePicker463,
                                    acys.ACS_txtRecPersonaRecibe63,
                                    acys.ACS_txtRecPuesto63,
                                    acys.ACS_Chk63Mismodia,
                                    acys.ACS_Chk63Sincita,
                                    acys.ACS_Chk63Previa,
                                    acys.ACS_txt63CitaContacto,
                                    acys.ACS_txt63CitaTelefono,
                                    acys.ACS_txt63CitaDiasdeAnticipacion,
                                    acys.ACS_chk63AreaPropia,
                                    acys.ACS_chk63AreaPlaza,
                                    acys.ACS_chk63AreaCalle,
                                    acys.ACS_chk63AreaAvTransitada,
                                    acys.ACS_chk63EstCortesia,
                                    acys.ACS_chk63EstCosto,
                                    acys.ACS_txt63EstMonto,
                                    acys.ACS_txt63ClienteDireccion,
                                    acys.ACS_txt63ClienteColonia,
                                    acys.ACS_txt63ClienteMunicipio,
                                    acys.ACS_txt63ClienteEstado,
                                    acys.ACS_txt63ClienteCodPost,
                                    acys.ACS_chk63DocFactFranquiciaEnt,
                                    acys.ACS_txt63DocFactFranquiciaEntCop,
                                    acys.ACS_chk63DocFactFranquiciaRec,
                                    acys.ACS_txt63DocFactFranquiciaRecCop,
                                    acys.ACS_chk63DocFactKeyEnt,
                                    acys.ACS_txt63DocFactKeyEntCop,
                                    acys.ACS_chk63DocFactKeyRec,
                                    acys.ACS_txt63DocFactKeyRecCop,
                                    acys.ACS_chk63DocOrdCompraEnt,
                                    acys.ACS_txt63DocOrdCompraEntCop,
                                    acys.ACS_chk63DocOrdCompraRec,
                                    acys.ACS_txt63DocOrdCompraRecCop,
                                    acys.ACS_chk63DocOrdReposEnt,
                                    acys.ACS_txt63DocOrdReposEntCop,
                                    acys.ACS_chk63DocOrdReposRec,
                                    acys.ACS_txt63DocOrdReposRecCop,
                                    acys.ACS_chk63DocCopPedidoEnt,
                                    acys.ACS_txt63DocCopPedidoEntCop,
                                    acys.ACS_chk63DocCopPedidoRec,
                                    acys.ACS_txt63DocCopPedidoRecCop,
                                    acys.ACS_chk63DocRemisionEnt,
                                    acys.ACS_txt63DocRemisionEntCop,
                                    acys.ACS_chk63DocRemisionRec,
                                    acys.ACS_txt63DocRemisionRecCop,
                                    acys.ACS_chk63DocFolioEnt,
                                    acys.ACS_txt63DocFolioEntCop,
                                    acys.ACS_chk63DocFolioRec,
                                    acys.ACS_txt63DocFolioRecCop,
                                    acys.ACS_chk63DocContraRecEnt,
                                    acys.ACS_txt63DocContraRecEntCop,
                                    acys.ACS_chk63DocContraRecRec,
                                    acys.ACS_txt63DocContraRecRecCop,
                                    acys.ACS_chk63DocEntAlmacenEnt,
                                    acys.ACS_txt63DocEntAlmacenEntCop,
                                    acys.ACS_chk63DocEntAlmacenRec,
                                    acys.ACS_txt63DocEntAlmacenRecCop,
                                    acys.ACS_chk63DocSopServicioEnt,
                                    acys.ACS_txt63DocSopServicioEntCop,
                                    acys.ACS_chk63DocSopServicioRec,
                                    acys.ACS_txt63DocSopServicioRecCop,
                                    acys.ACS_chk63DocNomFirmaEnt,
                                    acys.ACS_txt63DocNomFirmaEntCop,
                                    acys.ACS_chk63DocNomFirmaoRec,
                                    acys.ACS_txt63DocNomFirmaRecCop,
                                    acys.ACS_chk63CitaEnt,
                                    acys.ACS_txt63CitaEntCop,
                                    acys.ACS_chk63CitaRec,
                                    acys.ACS_txt63CitaRecCop
                                   };

                sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcys_Insertar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();
                acys.Id_Acs = verificador;

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_FechaInicio",
                            "@Acs_FechaFin",
                            "@Acs_CantTotal",
                            "@Id_TG",
                            "@Id_Ter" //RFH 22 Ene 2018

                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
                            acys.Id_AcsVersion,
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,                            
                            list[x].Acys_FechaInicio,
                            list[x].Acys_FechaFin,
                            list[x].Acys_CantTotal,
                            list[x].Id_TG,
                            acys.Id_Ter
                        };

                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                }

                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "Id_Acsversion", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni, a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoMensual, a.ServTecnicoRellenoMensualfechaIni, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                    modificar = 0;
                }

                //Datos Garantia    
                int verificador5 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@FactorGarantia", "@UPrimaNeta", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, a.FactorGarantia, a.UPrimaNeta, DateTime.Now };
                    sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spInsCapAcysDatosGarantia", ref verificador5, Parametros, Valores, transaction.Connection, sqlcmd);
                    sqlcmd.Dispose();
                }

                // Calendario Fechas de Corte
                int verificador6 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@Mes", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    if (a.Fechas_Corte != null)
                        for (int i = 1; i <= 12; i++)
                        {
                            Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, i, a.Fechas_Corte[i] };
                            sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                            sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                            sqlcmd = CD_Datos.GenerarSqlCommand("spInsCapAcysDatosGarantia_Fechas", ref verificador5, Parametros, Valores, transaction.Connection, sqlcmd);
                            sqlcmd.Dispose();
                        }
                }


                //Actualizar Calendario
                ActualizaControlCalendario(acys, ValoresCalendario, Conexion, transaction);

                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };

                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            acys.Id_AcsVersion,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                            sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                            sqlcmd = CD_Datos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias, transaction.Connection, sqlcmd);
                            sqlcmd.Dispose();
                            eliminar++;
                        }
                    }
                }

                if (verificador < 0)
                {
                    throw new Exception("Excepción levantada al correr CD_CapAcys.Insertar");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Excepción levantada al correr CD_CapAcys.Insertar", ex);
            }
            finally
            {
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        private void ActualizaControlCalendario(Acys acys, String ValoresCalendario, String Conexion)
        {
            int ver = 0;
            CD_CatCalendarioControl CD = new CD_CatCalendarioControl();
            List<CalendarioControl> listaCalendario = new List<CalendarioControl>();

            string[] items = ValoresCalendario.Split(',');

            foreach (string it in items)
            {
                if (it != "")
                {
                    CalendarioControl calItem = new CalendarioControl();
                    calItem.Id_Emp = acys.Id_Emp;
                    calItem.Id_Acs = acys.Id_Acs;
                    calItem.Id_AcsVersion = acys.Id_AcsVersion;
                    calItem.Id_Cd = acys.Id_Cd;
                    calItem.Cal_Año = DateTime.Now.Year;
                    calItem.Semana = Convert.ToInt32(it.Split('_')[0]);
                    calItem.IdProd = Convert.ToInt32(it.Split('_')[1]);
                    calItem.Id_TG = Convert.ToInt32(it.Split('_')[2]);

                    listaCalendario.Add(calItem);
                }
            }

            CD.GuardarCalendario(ref listaCalendario, ref ver, Conexion, "A");

        }

        /// <summary>
        /// Mismo propósito de [ActualizaControlCalendario(Acys, String, String)] (cualquiera que esta sea) con la característica adicional de aceptar una transacción
        /// </summary>
        /// <param name="acys">?</param>
        /// <param name="ValoresCalendario">?</param>
        /// <param name="Conexion">?</param>
        /// <param name="dbTransaccion">Transacción</param>
        private void ActualizaControlCalendario(Acys acys, String ValoresCalendario, String Conexion, IDbTransaction dbTransaccion)
        {
            int ver = 0;
            CD_CatCalendarioControl CD = new CD_CatCalendarioControl();
            List<CalendarioControl> listaCalendario = new List<CalendarioControl>();

            string[] items = ValoresCalendario.Split(',');

            foreach (string it in items)
            {
                if (it != "")
                {
                    CalendarioControl calItem = new CalendarioControl();
                    calItem.Id_Emp = acys.Id_Emp;
                    calItem.Id_Acs = acys.Id_Acs;
                    calItem.Id_AcsVersion = acys.Id_AcsVersion;
                    calItem.Id_Cd = acys.Id_Cd;
                    calItem.Cal_Año = DateTime.Now.Year;
                    calItem.Semana = Convert.ToInt32(it.Split('_')[0]);
                    calItem.IdProd = Convert.ToInt32(it.Split('_')[1]);
                    calItem.Id_TG = Convert.ToInt32(it.Split('_')[2]);

                    listaCalendario.Add(calItem);
                }
            }

            CD.GuardarCalendario(ref listaCalendario, ref ver, Conexion, dbTransaccion);
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, int? id_TV, List<int?> modalidadesGarantias, string cadenaConexionEF)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisFrecuencia",
                                          "@Acs_VisitaOtro",
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U",
                                          "@Acs_Modalidad",
                                          "@Id_CteDirEntrega",
"@Acs_Sucursal",
"@Acs_ParcialidadesSi",
"@Acs_ParcialidadesNo",
"@Acs_ConfirmacionPedidosSI",
"@Acs_ConfirmacionPedidosnO",
"@Acs_chkRecRevLunes",
"@Acs_RecRevMartes",
"@Acs_RecRevMiercoles",
"@Acs_RecRevJueves",
"@Acs_RecRevViernes",
"@Acs_RecRevSabado",
"@Acs_TimePicker1",
"@Acs_TimePicker2",
"@Acs_TimePicker3",
"@Acs_TimePicker4",
"@Acs_RecPersonaRecibe",
"@Acs_RecPuesto",
"@Acs_RecCitaMismoDia",
"@Acs_RecCitaSinCita",
"@Acs_RecCitaPrevia",
"@Acs_RecCitaContacto",
"@Acs_RecCitaTelefono",
"@Acs_RecCitaDiasdeAnticipacion",
"@Acs_RecAreaPropia",
"@Acs_RecAreaPlaza",
"@Acs_RecAreaCalle",
"@Acs_RecAreaAvTransitada",
"@Acs_RecEstCortesia",
"@Acs_RecEstCosto",
"@Acs_RecEstMonto",
"@Acs_RecDocFactFranquiciaEnt",
"@Acs_RecDocFactFranquiciaEntCop",
"@Acs_RecDocFactFranquiciaRec",
"@Acs_RecDocFactFranquiciaRecCop",
"@Acs_RecDocFactKeyEnt",
"@Acs_RecDocFactKeyEntCop",
"@Acs_RecDocFactKeyRec",
"@Acs_RecDocFactKeyRecCop",
"@Acs_RecDocOrdCompraEnt",
"@Acs_RecDocOrdCompraEntCop",
"@Acs_RecDocOrdCompraRec",
"@Acs_RecDocOrdCompraRecCop",
"@Acs_RecDocOrdReposEnt",
"@Acs_RecDocOrdReposEntCop",
"@Acs_RecDocOrdReposRec",
"@Acs_RecDocOrdReposRecCop",
"@Acs_RecDocCopPedidoEnt",
"@Acs_RecDocCopPedidoEntCop",
"@Acs_RecDocCopPedidoRec",
"@Acs_RecDocCopPedidoRecCop",
"@ACS_RecDocRemisionEnt",
"@ACS_RecDocRemisionEntCop",
"@ACS_RecDocRemisionRec",
"@ACS_RecDocRemisionRecCop",
"@ACS_RecDocFolioEnt",
"@ACS_RecDocFolioEntCop",
"@ACS_RecDocFolioRec",
"@ACS_RecDocFolioRecCop",
"@ACS_RecDocContraRecEnt",
"@ACS_RecDocContraRecEntCop",
"@ACS_RecDocContraRecRec",
"@ACS_RecDocContraRecRecCop",
"@ACS_RecDocEntAlmacenEnt",
"@ACS_RecDocEntAlmacenEntCop",
"@ACS_RecDocEntAlmacenRec",
"@ACS_RecDocEntAlmacenRecCop",
"@ACS_RecDocSopServicioEnt",
"@ACS_RecDocSopServicioEntCop",
"@ACS_RecDocSopServicioRec",
"@ACS_RecDocSopServicioRecCop",
"@ACS_RecDocNomFirmaEnt",
"@ACS_RecDocNomFirmaEntCop",
"@ACS_RecDocNomFirmaoRec",
"@ACS_RecDocNomFirmaRecCop",
"@ACS_RecCitaEnt",
"@ACS_RecCitaEntCop",
"@ACS_RecCitaRec",
"@ACS_RecCitaRecCop",
"@ACS_RecOtroRec",
"@ACS_chk62Lunes",
"@ACS_chk62Martes",
"@ACS_chk62Miercoles",
"@ACS_chk62Jueves",
"@ACS_chk62Viernes",
"@ACS_chk62Sabado",
"@ACS_RadTimePicker162",
"@ACS_RadTimePicker262",
"@ACS_RadTimePicker362",
"@ACS_RadTimePicker462",
"@ACS_txtRecPersonaRecibe62",
"@ACS_txtRecPuesto62",
"@ACS_Chk62Mismodia",
"@ACS_Chk62Sincita",
"@ACS_Chk62Previa",
"@ACS_txt62CitaContacto",
"@ACS_txt62CitaTelefono",
"@ACS_txt62CitaDiasdeAnticipacion",
"@ACS_chk62AreaPropia",
"@ACS_chk62AreaPlaza",
"@ACS_chk62AreaCalle",
"@ACS_chk62AreaAvTransitada",
"@ACS_chk62EstCortesia",
"@ACS_chk62EstCosto",
"@ACS_txt62EstMonto",
"@ACS_txt62ClienteDireccion",
"@ACS_txt62ClienteColonia",
"@ACS_txt62ClienteMunicipio",
"@ACS_txt62ClienteEstado",
"@ACS_txt62ClienteCodPost",
"@ACS_chk62DocFactFranquiciaEnt",
"@ACS_txt62DocFactFranquiciaEntCop",
"@ACS_chk62DocFactFranquiciaRec",
"@ACS_txt62DocFactFranquiciaRecCop",
"@ACS_chk62DocFactKeyEnt",
"@ACS_txt62DocFactKeyEntCop",
"@ACS_chk62DocFactKeyRec",
"@ACS_txt62DocFactKeyRecCop",
"@ACS_chk62DocOrdCompraEnt",
"@ACS_txt62DocOrdCompraEntCop",
"@ACS_chk62DocOrdCompraRec",
"@ACS_txt62DocOrdCompraRecCop",
"@ACS_chk62DocOrdReposEnt",
"@ACS_txt62DocOrdReposEntCop",
"@ACS_chk62DocOrdReposRec",
"@ACS_txt62DocOrdReposRecCop",
"@ACS_chk62DocCopPedidoEnt",
"@ACS_txt62DocCopPedidoEntCop",
"@ACS_chk62DocCopPedidoRec",
"@ACS_txt62DocCopPedidoRecCop",
"@ACS_chk62DocRemisionEnt",
"@ACS_txt62DocRemisionEntCop",
"@ACS_chk62DocRemisionRec",
"@ACS_txt62DocRemisionRecCop",
"@ACS_chk62DocFolioEnt",
"@ACS_txt62DocFolioEntCop",
"@ACS_chk62DocFolioRec",
"@ACS_txt62DocFolioRecCop",
"@ACS_chk62DocContraRecEnt",
"@ACS_txt62DocContraRecEntCop",
"@ACS_chk62DocContraRecRec",
"@ACS_txt62DocContraRecRecCop",
"@ACS_chk62DocEntAlmacenEnt",
"@ACS_txt62DocEntAlmacenEntCop",
"@ACS_chk62DocEntAlmacenRec",
"@ACS_txt62DocEntAlmacenRecCop",
"@ACS_chk62DocSopServicioEnt",
"@ACS_txt62DocSopServicioEntCop",
"@ACS_chk62DocSopServicioRec",
"@ACS_txt62DocSopServicioRecCop",
"@ACS_chk62DocNomFirmaEnt",
"@ACS_txt62DocNomFirmaEntCop",
"@ACS_chk62DocNomFirmaoRec",
"@ACS_txt62DocNomFirmaRecCop",
"@ACS_chk62CitaEnt",
"@ACS_txt62CitaEntCop",
"@ACS_chk62CitaRec",
"@ACS_txt62CitaRecCop",
"@ACS_chk63Lunes",
"@ACS_chk63Martes",
"@ACS_chk63Miercoles",
"@ACS_chk63Jueves",
"@ACS_chk63Viernes",
"@ACS_chk63Sabado",
"@ACS_Rad63TimePicker163",
"@ACS_Rad63TimePicker263",
"@ACS_Rad63TimePicker363",
"@ACS_Rad63TimePicker463",
"@ACS_txtRecPersonaRecibe63",
"@ACS_txtRecPuesto63",
"@ACS_Chk63Mismodia",
"@ACS_Chk63Sincita",
"@ACS_Chk63Previa",
"@ACS_txt63CitaContacto",
"@ACS_txt63CitaTelefono",
"@ACS_txt63CitaDiasdeAnticipacion",
"@ACS_chk63AreaPropia",
"@ACS_chk63AreaPlaza",
"@ACS_chk63AreaCalle",
"@ACS_chk63AreaAvTransitada",
"@ACS_chk63EstCortesia",
"@ACS_chk63EstCosto",
"@ACS_txt63EstMonto",
"@ACS_txt63ClienteDireccion",
"@ACS_txt63ClienteColonia",
"@ACS_txt63ClienteMunicipio",
"@ACS_txt63ClienteEstado",
"@ACS_txt63ClienteCodPost",
"@ACS_chk63DocFactFranquiciaEnt",
"@ACS_txt63DocFactFranquiciaEntCop",
"@ACS_chk63DocFactFranquiciaRec",
"@ACS_txt63DocFactFranquiciaRecCop",
"@ACS_chk63DocFactKeyEnt",
"@ACS_txt63DocFactKeyEntCop",
"@ACS_chk63DocFactKeyRec",
"@ACS_txt63DocFactKeyRecCop",
"@ACS_chk63DocOrdCompraEnt",
"@ACS_txt63DocOrdCompraEntCop",
"@ACS_chk63DocOrdCompraRec",
"@ACS_txt63DocOrdCompraRecCop",
"@ACS_chk63DocOrdReposEnt",
"@ACS_txt63DocOrdReposEntCop",
"@ACS_chk63DocOrdReposRec",
"@ACS_txt63DocOrdReposRecCop",
"@ACS_chk63DocCopPedidoEnt",
"@ACS_txt63DocCopPedidoEntCop",
"@ACS_chk63DocCopPedidoRec",
"@ACS_txt63DocCopPedidoRecCop",
"@ACS_chk63DocRemisionEnt",
"@ACS_txt63DocRemisionEntCop",
"@ACS_chk63DocRemisionRec",
"@ACS_txt63DocRemisionRecCop",
"@ACS_chk63DocFolioEnt",
"@ACS_txt63DocFolioEntCop",
"@ACS_chk63DocFolioRec",
"@ACS_txt63DocFolioRecCop",
"@ACS_chk63DocContraRecEnt",
"@ACS_txt63DocContraRecEntCop",
"@ACS_chk63DocContraRecRec",
"@ACS_txt63DocContraRecRecCop",
"@ACS_chk63DocEntAlmacenEnt",
"@ACS_txt63DocEntAlmacenEntCop",
"@ACS_chk63DocEntAlmacenRec",
"@ACS_txt63DocEntAlmacenRecCop",
"@ACS_chk63DocSopServicioEnt",
"@ACS_txt63DocSopServicioEntCop",
"@ACS_chk63DocSopServicioRec",
"@ACS_txt63DocSopServicioRecCop",
"@ACS_chk63DocNomFirmaEnt",
"@ACS_txt63DocNomFirmaEntCop",
"@ACS_chk63DocNomFirmaoRec",
"@ACS_txt63DocNomFirmaRecCop",
"@ACS_chk63CitaEnt",
"@ACS_txt63CitaEntCop",
"@ACS_chk63CitaRec",
"@ACS_txt63CitaRecCop"
                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Vis_Frecuencia,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U,
                                       acys.Acs_Modalidad,
                                       acys.IdCte_DirEntrega,
acys.Acs_Sucursal,
acys.Acs_ParcialidadesSi,
acys.Acs_ParcialidadesNo,
acys.Acs_ConfirmacionPedidosSI,
acys.Acs_ConfirmacionPedidosnO,
acys.Acs_chkRecRevLunes,
acys.Acs_RecRevMartes,
acys.Acs_RecRevMiercoles,
acys.Acs_RecRevJueves,
acys.Acs_RecRevViernes,
acys.Acs_RecRevSabado,
acys.Acs_TimePicker1,
acys.Acs_TimePicker2,
acys.Acs_TimePicker3,
acys.Acs_TimePicker4,
acys.Acs_RecPersonaRecibe,
acys.Acs_RecPuesto,
acys.Acs_RecCitaMismoDia,
acys.Acs_RecCitaSinCita,
acys.Acs_RecCitaPrevia,
acys.Acs_RecCitaContacto,
acys.Acs_RecCitaTelefono,
acys.Acs_RecCitaDiasdeAnticipacion,
acys.Acs_RecAreaPropia,
acys.Acs_RecAreaPlaza,
acys.Acs_RecAreaCalle,
acys.Acs_RecAreaAvTransitada,
acys.Acs_RecEstCortesia,
acys.Acs_RecEstCosto,
acys.Acs_RecEstMonto,
acys.Acs_RecDocFactFranquiciaEnt,
acys.Acs_RecDocFactFranquiciaEntCop,
acys.Acs_RecDocFactFranquiciaRec,
acys.Acs_RecDocFactFranquiciaRecCop,
acys.Acs_RecDocFactKeyEnt,
acys.Acs_RecDocFactKeyEntCop,
acys.Acs_RecDocFactKeyRec,
acys.Acs_RecDocFactKeyRecCop,
acys.Acs_RecDocOrdCompraEnt,
acys.Acs_RecDocOrdCompraEntCop,
acys.Acs_RecDocOrdCompraRec,
acys.Acs_RecDocOrdCompraRecCop,
acys.Acs_RecDocOrdReposEnt,
acys.Acs_RecDocOrdReposEntCop,
acys.Acs_RecDocOrdReposRec,
acys.Acs_RecDocOrdReposRecCop,
acys.Acs_RecDocCopPedidoEnt,
acys.Acs_RecDocCopPedidoEntCop,
acys.Acs_RecDocCopPedidoRec,
acys.Acs_RecDocCopPedidoRecCop,
acys.ACS_RecDocRemisionEnt,
acys.ACS_RecDocRemisionEntCop,
acys.ACS_RecDocRemisionRec,
acys.ACS_RecDocRemisionRecCop,
acys.ACS_RecDocFolioEnt,
acys.ACS_RecDocFolioEntCop,
acys.ACS_RecDocFolioRec,
acys.ACS_RecDocFolioRecCop,
acys.ACS_RecDocContraRecEnt,
acys.ACS_RecDocContraRecEntCop,
acys.ACS_RecDocContraRecRec,
acys.ACS_RecDocContraRecRecCop,
acys.ACS_RecDocEntAlmacenEnt,
acys.ACS_RecDocEntAlmacenEntCop,
acys.ACS_RecDocEntAlmacenRec,
acys.ACS_RecDocEntAlmacenRecCop,
acys.ACS_RecDocSopServicioEnt,
acys.ACS_RecDocSopServicioEntCop,
acys.ACS_RecDocSopServicioRec,
acys.ACS_RecDocSopServicioRecCop,
acys.ACS_RecDocNomFirmaEnt,
acys.ACS_RecDocNomFirmaEntCop,
acys.ACS_RecDocNomFirmaoRec,
acys.ACS_RecDocNomFirmaRecCop,
acys.ACS_RecCitaEnt,
acys.ACS_RecCitaEntCop,
acys.ACS_RecCitaRec,
acys.ACS_RecCitaRecCop,
acys.ACS_RecOtroRec,
acys.ACS_chk62Lunes,
acys.ACS_chk62Martes,
acys.ACS_chk62Miercoles,
acys.ACS_chk62Jueves,
acys.ACS_chk62Viernes,
acys.ACS_chk62Sabado,
acys.ACS_RadTimePicker162,
acys.ACS_RadTimePicker262,
acys.ACS_RadTimePicker362,
acys.ACS_RadTimePicker462,
acys.ACS_txtRecPersonaRecibe62,
acys.ACS_txtRecPuesto62,
acys.ACS_Chk62Mismodia,
acys.ACS_Chk62Sincita,
acys.ACS_Chk62Previa,
acys.ACS_txt62CitaContacto,
acys.ACS_txt62CitaTelefono,
acys.ACS_txt62CitaDiasdeAnticipacion,
acys.ACS_chk62AreaPropia,
acys.ACS_chk62AreaPlaza,
acys.ACS_chk62AreaCalle,
acys.ACS_chk62AreaAvTransitada,
acys.ACS_chk62EstCortesia,
acys.ACS_chk62EstCosto,
acys.ACS_txt62EstMonto,
acys.ACS_txt62ClienteDireccion,
acys.ACS_txt62ClienteColonia,
acys.ACS_txt62ClienteMunicipio,
acys.ACS_txt62ClienteEstado,
acys.ACS_txt62ClienteCodPost,
acys.ACS_chk62DocFactFranquiciaEnt,
acys.ACS_txt62DocFactFranquiciaEntCop,
acys.ACS_chk62DocFactFranquiciaRec,
acys.ACS_txt62DocFactFranquiciaRecCop,
acys.ACS_chk62DocFactKeyEnt,
acys.ACS_txt62DocFactKeyEntCop,
acys.ACS_chk62DocFactKeyRec,
acys.ACS_txt62DocFactKeyRecCop,
acys.ACS_chk62DocOrdCompraEnt,
acys.ACS_txt62DocOrdCompraEntCop,
acys.ACS_chk62DocOrdCompraRec,
acys.ACS_txt62DocOrdCompraRecCop,
acys.ACS_chk62DocOrdReposEnt,
acys.ACS_txt62DocOrdReposEntCop,
acys.ACS_chk62DocOrdReposRec,
acys.ACS_txt62DocOrdReposRecCop,
acys.ACS_chk62DocCopPedidoEnt,
acys.ACS_txt62DocCopPedidoEntCop,
acys.ACS_chk62DocCopPedidoRec,
acys.ACS_txt62DocCopPedidoRecCop,
acys.ACS_chk62DocRemisionEnt,
acys.ACS_txt62DocRemisionEntCop,
acys.ACS_chk62DocRemisionRec,
acys.ACS_txt62DocRemisionRecCop,
acys.ACS_chk62DocFolioEnt,
acys.ACS_txt62DocFolioEntCop,
acys.ACS_chk62DocFolioRec,
acys.ACS_txt62DocFolioRecCop,
acys.ACS_chk62DocContraRecEnt,
acys.ACS_txt62DocContraRecEntCop,
acys.ACS_chk62DocContraRecRec,
acys.ACS_txt62DocContraRecRecCop,
acys.ACS_chk62DocEntAlmacenEnt,
acys.ACS_txt62DocEntAlmacenEntCop,
acys.ACS_chk62DocEntAlmacenRec,
acys.ACS_txt62DocEntAlmacenRecCop,
acys.ACS_chk62DocSopServicioEnt,
acys.ACS_txt62DocSopServicioEntCop,
acys.ACS_chk62DocSopServicioRec,
acys.ACS_txt62DocSopServicioRecCop,
acys.ACS_chk62DocNomFirmaEnt,
acys.ACS_txt62DocNomFirmaEntCop,
acys.ACS_chk62DocNomFirmaoRec,
acys.ACS_txt62DocNomFirmaRecCop,
acys.ACS_chk62CitaEnt,
acys.ACS_txt62CitaEntCop,
acys.ACS_chk62CitaRec,
acys.ACS_txt62CitaRecCop,
acys.ACS_chk63Lunes,
acys.ACS_chk63Martes,
acys.ACS_chk63Miercoles,
acys.ACS_chk63Jueves,
acys.ACS_chk63Viernes,
acys.ACS_chk63Sabado,
acys.ACS_Rad63TimePicker163,
acys.ACS_Rad63TimePicker263,
acys.ACS_Rad63TimePicker363,
acys.ACS_Rad63TimePicker463,
acys.ACS_txtRecPersonaRecibe63,
acys.ACS_txtRecPuesto63,
acys.ACS_Chk63Mismodia,
acys.ACS_Chk63Sincita,
acys.ACS_Chk63Previa,
acys.ACS_txt63CitaContacto,
acys.ACS_txt63CitaTelefono,
acys.ACS_txt63CitaDiasdeAnticipacion,
acys.ACS_chk63AreaPropia,
acys.ACS_chk63AreaPlaza,
acys.ACS_chk63AreaCalle,
acys.ACS_chk63AreaAvTransitada,
acys.ACS_chk63EstCortesia,
acys.ACS_chk63EstCosto,
acys.ACS_txt63EstMonto,
acys.ACS_txt63ClienteDireccion,
acys.ACS_txt63ClienteColonia,
acys.ACS_txt63ClienteMunicipio,
acys.ACS_txt63ClienteEstado,
acys.ACS_txt63ClienteCodPost,
acys.ACS_chk63DocFactFranquiciaEnt,
acys.ACS_txt63DocFactFranquiciaEntCop,
acys.ACS_chk63DocFactFranquiciaRec,
acys.ACS_txt63DocFactFranquiciaRecCop,
acys.ACS_chk63DocFactKeyEnt,
acys.ACS_txt63DocFactKeyEntCop,
acys.ACS_chk63DocFactKeyRec,
acys.ACS_txt63DocFactKeyRecCop,
acys.ACS_chk63DocOrdCompraEnt,
acys.ACS_txt63DocOrdCompraEntCop,
acys.ACS_chk63DocOrdCompraRec,
acys.ACS_txt63DocOrdCompraRecCop,
acys.ACS_chk63DocOrdReposEnt,
acys.ACS_txt63DocOrdReposEntCop,
acys.ACS_chk63DocOrdReposRec,
acys.ACS_txt63DocOrdReposRecCop,
acys.ACS_chk63DocCopPedidoEnt,
acys.ACS_txt63DocCopPedidoEntCop,
acys.ACS_chk63DocCopPedidoRec,
acys.ACS_txt63DocCopPedidoRecCop,
acys.ACS_chk63DocRemisionEnt,
acys.ACS_txt63DocRemisionEntCop,
acys.ACS_chk63DocRemisionRec,
acys.ACS_txt63DocRemisionRecCop,
acys.ACS_chk63DocFolioEnt,
acys.ACS_txt63DocFolioEntCop,
acys.ACS_chk63DocFolioRec,
acys.ACS_txt63DocFolioRecCop,
acys.ACS_chk63DocContraRecEnt,
acys.ACS_txt63DocContraRecEntCop,
acys.ACS_chk63DocContraRecRec,
acys.ACS_txt63DocContraRecRecCop,
acys.ACS_chk63DocEntAlmacenEnt,
acys.ACS_txt63DocEntAlmacenEntCop,
acys.ACS_chk63DocEntAlmacenRec,
acys.ACS_txt63DocEntAlmacenRecCop,
acys.ACS_chk63DocSopServicioEnt,
acys.ACS_txt63DocSopServicioEntCop,
acys.ACS_chk63DocSopServicioRec,
acys.ACS_txt63DocSopServicioRecCop,
acys.ACS_chk63DocNomFirmaEnt,
acys.ACS_txt63DocNomFirmaEntCop,
acys.ACS_chk63DocNomFirmaoRec,
acys.ACS_txt63DocNomFirmaRecCop,
acys.ACS_chk63CitaEnt,
acys.ACS_txt63CitaEntCop,
acys.ACS_chk63CitaRec,
acys.ACS_txt63CitaRecCop


                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Insertar", ref verificador, Parametros, Valores);

                CD_CapAcysExt cdCAD = new CD_CapAcysExt(cadenaConexionEF);
                if (modalidadesGarantias.Count > 0)
                {
                    cdCAD.Insertar(acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, id_TV, modalidadesGarantias, CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                }
                else
                {
                    cdCAD.Insertar(acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, null, (int?)null, CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                }

                acys.Id_Acs = verificador;

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_FechaInicio",
                            "@Acs_FechaFin",
                            "@Acs_CantTotal",
                            "@Id_TG"
                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
                            acys.Id_AcsVersion,
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,                            
                            list[x].Acys_FechaInicio,
                            list[x].Acys_FechaFin,
                            list[x].Acys_CantTotal,
                            list[x].Id_TG
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_InsertarV1", ref verificador, Parametros, Valores);
                }

                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "Id_Acsversion", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni, a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores);
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoMensual, a.ServTecnicoRellenoMensualfechaIni, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores);
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores);
                    modificar = 0;
                }

                //Datos Garantia    
                int verificador5 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@FactorGarantia", "@UPrimaNeta", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, a.FactorGarantia, a.UPrimaNeta, a.FechaCorte };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spInsCapAcysDatosGarantia", ref verificador5, Parametros, Valores);
                }




                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion",
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };

                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            acys.Id_AcsVersion,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                            eliminar++;
                        }
                    }
                }

                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void Modificar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion", 
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisFrecuencia",
                                          "@Acs_VisitaOtro",                                          
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U",
                                          "@Acs_Modalidad",
                                          "@Id_CteDirEntrega",
"@Acs_Sucursal",
"@Acs_ParcialidadesSi",
"@Acs_ParcialidadesNo",
"@Acs_ConfirmacionPedidosSI",
"@Acs_ConfirmacionPedidosnO",
"@Acs_chkRecRevLunes",
"@Acs_RecRevMartes",
"@Acs_RecRevMiercoles",
"@Acs_RecRevJueves",
"@Acs_RecRevViernes",
"@Acs_RecRevSabado",
"@Acs_TimePicker1",
"@Acs_TimePicker2",
"@Acs_TimePicker3",
"@Acs_TimePicker4",
"@Acs_RecPersonaRecibe",
"@Acs_RecPuesto",
"@Acs_RecCitaMismoDia",
"@Acs_RecCitaSinCita",
"@Acs_RecCitaPrevia",
"@Acs_RecCitaContacto",
"@Acs_RecCitaTelefono",
"@Acs_RecCitaDiasdeAnticipacion",
"@Acs_RecAreaPropia",
"@Acs_RecAreaPlaza",
"@Acs_RecAreaCalle",
"@Acs_RecAreaAvTransitada",
"@Acs_RecEstCortesia",
"@Acs_RecEstCosto",
"@Acs_RecEstMonto",
"@Acs_RecDocFactFranquiciaEnt",
"@Acs_RecDocFactFranquiciaEntCop",
"@Acs_RecDocFactFranquiciaRec",
"@Acs_RecDocFactFranquiciaRecCop",
"@Acs_RecDocFactKeyEnt",
"@Acs_RecDocFactKeyEntCop",
"@Acs_RecDocFactKeyRec",
"@Acs_RecDocFactKeyRecCop",
"@Acs_RecDocOrdCompraEnt",
"@Acs_RecDocOrdCompraEntCop",
"@Acs_RecDocOrdCompraRec",
"@Acs_RecDocOrdCompraRecCop",
"@Acs_RecDocOrdReposEnt",
"@Acs_RecDocOrdReposEntCop",
"@Acs_RecDocOrdReposRec",
"@Acs_RecDocOrdReposRecCop",
"@Acs_RecDocCopPedidoEnt",
"@Acs_RecDocCopPedidoEntCop",
"@Acs_RecDocCopPedidoRec",
"@Acs_RecDocCopPedidoRecCop",
"@ACS_RecDocRemisionEnt",
"@ACS_RecDocRemisionEntCop",
"@ACS_RecDocRemisionRec",
"@ACS_RecDocRemisionRecCop",
"@ACS_RecDocFolioEnt",
"@ACS_RecDocFolioEntCop",
"@ACS_RecDocFolioRec",
"@ACS_RecDocFolioRecCop",
"@ACS_RecDocContraRecEnt",
"@ACS_RecDocContraRecEntCop",
"@ACS_RecDocContraRecRec",
"@ACS_RecDocContraRecRecCop",
"@ACS_RecDocEntAlmacenEnt",
"@ACS_RecDocEntAlmacenEntCop",
"@ACS_RecDocEntAlmacenRec",
"@ACS_RecDocEntAlmacenRecCop",
"@ACS_RecDocSopServicioEnt",
"@ACS_RecDocSopServicioEntCop",
"@ACS_RecDocSopServicioRec",
"@ACS_RecDocSopServicioRecCop",
"@ACS_RecDocNomFirmaEnt",
"@ACS_RecDocNomFirmaEntCop",
"@ACS_RecDocNomFirmaoRec",
"@ACS_RecDocNomFirmaRecCop",
"@ACS_RecCitaEnt",
"@ACS_RecCitaEntCop",
"@ACS_RecCitaRec",
"@ACS_RecCitaRecCop",
"@ACS_RecOtroRec",
"@ACS_chk62Lunes",
"@ACS_chk62Martes",
"@ACS_chk62Miercoles",
"@ACS_chk62Jueves",
"@ACS_chk62Viernes",
"@ACS_chk62Sabado",
"@ACS_RadTimePicker162",
"@ACS_RadTimePicker262",
"@ACS_RadTimePicker362",
"@ACS_RadTimePicker462",
"@ACS_txtRecPersonaRecibe62",
"@ACS_txtRecPuesto62",
"@ACS_Chk62Mismodia",
"@ACS_Chk62Sincita",
"@ACS_Chk62Previa",
"@ACS_txt62CitaContacto",
"@ACS_txt62CitaTelefono",
"@ACS_txt62CitaDiasdeAnticipacion",
"@ACS_chk62AreaPropia",
"@ACS_chk62AreaPlaza",
"@ACS_chk62AreaCalle",
"@ACS_chk62AreaAvTransitada",
"@ACS_chk62EstCortesia",
"@ACS_chk62EstCosto",
"@ACS_txt62EstMonto",
"@ACS_txt62ClienteDireccion",
"@ACS_txt62ClienteColonia",
"@ACS_txt62ClienteMunicipio",
"@ACS_txt62ClienteEstado",
"@ACS_txt62ClienteCodPost",
"@ACS_chk62DocFactFranquiciaEnt",
"@ACS_txt62DocFactFranquiciaEntCop",
"@ACS_chk62DocFactFranquiciaRec",
"@ACS_txt62DocFactFranquiciaRecCop",
"@ACS_chk62DocFactKeyEnt",
"@ACS_txt62DocFactKeyEntCop",
"@ACS_chk62DocFactKeyRec",
"@ACS_txt62DocFactKeyRecCop",
"@ACS_chk62DocOrdCompraEnt",
"@ACS_txt62DocOrdCompraEntCop",
"@ACS_chk62DocOrdCompraRec",
"@ACS_txt62DocOrdCompraRecCop",
"@ACS_chk62DocOrdReposEnt",
"@ACS_txt62DocOrdReposEntCop",
"@ACS_chk62DocOrdReposRec",
"@ACS_txt62DocOrdReposRecCop",
"@ACS_chk62DocCopPedidoEnt",
"@ACS_txt62DocCopPedidoEntCop",
"@ACS_chk62DocCopPedidoRec",
"@ACS_txt62DocCopPedidoRecCop",
"@ACS_chk62DocRemisionEnt",
"@ACS_txt62DocRemisionEntCop",
"@ACS_chk62DocRemisionRec",
"@ACS_txt62DocRemisionRecCop",
"@ACS_chk62DocFolioEnt",
"@ACS_txt62DocFolioEntCop",
"@ACS_chk62DocFolioRec",
"@ACS_txt62DocFolioRecCop",
"@ACS_chk62DocContraRecEnt",
"@ACS_txt62DocContraRecEntCop",
"@ACS_chk62DocContraRecRec",
"@ACS_txt62DocContraRecRecCop",
"@ACS_chk62DocEntAlmacenEnt",
"@ACS_txt62DocEntAlmacenEntCop",
"@ACS_chk62DocEntAlmacenRec",
"@ACS_txt62DocEntAlmacenRecCop",
"@ACS_chk62DocSopServicioEnt",
"@ACS_txt62DocSopServicioEntCop",
"@ACS_chk62DocSopServicioRec",
"@ACS_txt62DocSopServicioRecCop",
"@ACS_chk62DocNomFirmaEnt",
"@ACS_txt62DocNomFirmaEntCop",
"@ACS_chk62DocNomFirmaoRec",
"@ACS_txt62DocNomFirmaRecCop",
"@ACS_chk62CitaEnt",
"@ACS_txt62CitaEntCop",
"@ACS_chk62CitaRec",
"@ACS_txt62CitaRecCop",
"@ACS_chk63Lunes",
"@ACS_chk63Martes",
"@ACS_chk63Miercoles",
"@ACS_chk63Jueves",
"@ACS_chk63Viernes",
"@ACS_chk63Sabado",
"@ACS_Rad63TimePicker163",
"@ACS_Rad63TimePicker263",
"@ACS_Rad63TimePicker363",
"@ACS_Rad63TimePicker463",
"@ACS_txtRecPersonaRecibe63",
"@ACS_txtRecPuesto63",
"@ACS_Chk63Mismodia",
"@ACS_Chk63Sincita",
"@ACS_Chk63Previa",
"@ACS_txt63CitaContacto",
"@ACS_txt63CitaTelefono",
"@ACS_txt63CitaDiasdeAnticipacion",
"@ACS_chk63AreaPropia",
"@ACS_chk63AreaPlaza",
"@ACS_chk63AreaCalle",
"@ACS_chk63AreaAvTransitada",
"@ACS_chk63EstCortesia",
"@ACS_chk63EstCosto",
"@ACS_txt63EstMonto",
"@ACS_txt63ClienteDireccion",
"@ACS_txt63ClienteColonia",
"@ACS_txt63ClienteMunicipio",
"@ACS_txt63ClienteEstado",
"@ACS_txt63ClienteCodPost",
"@ACS_chk63DocFactFranquiciaEnt",
"@ACS_txt63DocFactFranquiciaEntCop",
"@ACS_chk63DocFactFranquiciaRec",
"@ACS_txt63DocFactFranquiciaRecCop",
"@ACS_chk63DocFactKeyEnt",
"@ACS_txt63DocFactKeyEntCop",
"@ACS_chk63DocFactKeyRec",
"@ACS_txt63DocFactKeyRecCop",
"@ACS_chk63DocOrdCompraEnt",
"@ACS_txt63DocOrdCompraEntCop",
"@ACS_chk63DocOrdCompraRec",
"@ACS_txt63DocOrdCompraRecCop",
"@ACS_chk63DocOrdReposEnt",
"@ACS_txt63DocOrdReposEntCop",
"@ACS_chk63DocOrdReposRec",
"@ACS_txt63DocOrdReposRecCop",
"@ACS_chk63DocCopPedidoEnt",
"@ACS_txt63DocCopPedidoEntCop",
"@ACS_chk63DocCopPedidoRec",
"@ACS_txt63DocCopPedidoRecCop",
"@ACS_chk63DocRemisionEnt",
"@ACS_txt63DocRemisionEntCop",
"@ACS_chk63DocRemisionRec",
"@ACS_txt63DocRemisionRecCop",
"@ACS_chk63DocFolioEnt",
"@ACS_txt63DocFolioEntCop",
"@ACS_chk63DocFolioRec",
"@ACS_txt63DocFolioRecCop",
"@ACS_chk63DocContraRecEnt",
"@ACS_txt63DocContraRecEntCop",
"@ACS_chk63DocContraRecRec",
"@ACS_txt63DocContraRecRecCop",
"@ACS_chk63DocEntAlmacenEnt",
"@ACS_txt63DocEntAlmacenEntCop",
"@ACS_chk63DocEntAlmacenRec",
"@ACS_txt63DocEntAlmacenRecCop",
"@ACS_chk63DocSopServicioEnt",
"@ACS_txt63DocSopServicioEntCop",
"@ACS_chk63DocSopServicioRec",
"@ACS_txt63DocSopServicioRecCop",
"@ACS_chk63DocNomFirmaEnt",
"@ACS_txt63DocNomFirmaEntCop",
"@ACS_chk63DocNomFirmaoRec",
"@ACS_txt63DocNomFirmaRecCop",
"@ACS_chk63CitaEnt",
"@ACS_txt63CitaEntCop",
"@ACS_chk63CitaRec",
"@ACS_txt63CitaRecCop"

                                      };




                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Vis_Frecuencia,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U,
                                       acys.Acs_Modalidad,
                                       acys.IdCte_DirEntrega,
acys.Acs_Sucursal,
acys.Acs_ParcialidadesSi,
acys.Acs_ParcialidadesNo,
acys.Acs_ConfirmacionPedidosSI,
acys.Acs_ConfirmacionPedidosnO,
acys.Acs_chkRecRevLunes,
acys.Acs_RecRevMartes,
acys.Acs_RecRevMiercoles,
acys.Acs_RecRevJueves,
acys.Acs_RecRevViernes,
acys.Acs_RecRevSabado,
acys.Acs_TimePicker1,
acys.Acs_TimePicker2,
acys.Acs_TimePicker3,
acys.Acs_TimePicker4,
acys.Acs_RecPersonaRecibe,
acys.Acs_RecPuesto,
acys.Acs_RecCitaMismoDia,
acys.Acs_RecCitaSinCita,
acys.Acs_RecCitaPrevia,
acys.Acs_RecCitaContacto,
acys.Acs_RecCitaTelefono,
acys.Acs_RecCitaDiasdeAnticipacion,
acys.Acs_RecAreaPropia,
acys.Acs_RecAreaPlaza,
acys.Acs_RecAreaCalle,
acys.Acs_RecAreaAvTransitada,
acys.Acs_RecEstCortesia,
acys.Acs_RecEstCosto,
acys.Acs_RecEstMonto,
acys.Acs_RecDocFactFranquiciaEnt,
acys.Acs_RecDocFactFranquiciaEntCop,
acys.Acs_RecDocFactFranquiciaRec,
acys.Acs_RecDocFactFranquiciaRecCop,
acys.Acs_RecDocFactKeyEnt,
acys.Acs_RecDocFactKeyEntCop,
acys.Acs_RecDocFactKeyRec,
acys.Acs_RecDocFactKeyRecCop,
acys.Acs_RecDocOrdCompraEnt,
acys.Acs_RecDocOrdCompraEntCop,
acys.Acs_RecDocOrdCompraRec,
acys.Acs_RecDocOrdCompraRecCop,
acys.Acs_RecDocOrdReposEnt,
acys.Acs_RecDocOrdReposEntCop,
acys.Acs_RecDocOrdReposRec,
acys.Acs_RecDocOrdReposRecCop,
acys.Acs_RecDocCopPedidoEnt,
acys.Acs_RecDocCopPedidoEntCop,
acys.Acs_RecDocCopPedidoRec,
acys.Acs_RecDocCopPedidoRecCop,
acys.ACS_RecDocRemisionEnt,
acys.ACS_RecDocRemisionEntCop,
acys.ACS_RecDocRemisionRec,
acys.ACS_RecDocRemisionRecCop,
acys.ACS_RecDocFolioEnt,
acys.ACS_RecDocFolioEntCop,
acys.ACS_RecDocFolioRec,
acys.ACS_RecDocFolioRecCop,
acys.ACS_RecDocContraRecEnt,
acys.ACS_RecDocContraRecEntCop,
acys.ACS_RecDocContraRecRec,
acys.ACS_RecDocContraRecRecCop,
acys.ACS_RecDocEntAlmacenEnt,
acys.ACS_RecDocEntAlmacenEntCop,
acys.ACS_RecDocEntAlmacenRec,
acys.ACS_RecDocEntAlmacenRecCop,
acys.ACS_RecDocSopServicioEnt,
acys.ACS_RecDocSopServicioEntCop,
acys.ACS_RecDocSopServicioRec,
acys.ACS_RecDocSopServicioRecCop,
acys.ACS_RecDocNomFirmaEnt,
acys.ACS_RecDocNomFirmaEntCop,
acys.ACS_RecDocNomFirmaoRec,
acys.ACS_RecDocNomFirmaRecCop,
acys.ACS_RecCitaEnt,
acys.ACS_RecCitaEntCop,
acys.ACS_RecCitaRec,
acys.ACS_RecCitaRecCop,
acys.ACS_RecOtroRec,
acys.ACS_chk62Lunes,
acys.ACS_chk62Martes,
acys.ACS_chk62Miercoles,
acys.ACS_chk62Jueves,
acys.ACS_chk62Viernes,
acys.ACS_chk62Sabado,
acys.ACS_RadTimePicker162,
acys.ACS_RadTimePicker262,
acys.ACS_RadTimePicker362,
acys.ACS_RadTimePicker462,
acys.ACS_txtRecPersonaRecibe62,
acys.ACS_txtRecPuesto62,
acys.ACS_Chk62Mismodia,
acys.ACS_Chk62Sincita,
acys.ACS_Chk62Previa,
acys.ACS_txt62CitaContacto,
acys.ACS_txt62CitaTelefono,
acys.ACS_txt62CitaDiasdeAnticipacion,
acys.ACS_chk62AreaPropia,
acys.ACS_chk62AreaPlaza,
acys.ACS_chk62AreaCalle,
acys.ACS_chk62AreaAvTransitada,
acys.ACS_chk62EstCortesia,
acys.ACS_chk62EstCosto,
acys.ACS_txt62EstMonto,
acys.ACS_txt62ClienteDireccion,
acys.ACS_txt62ClienteColonia,
acys.ACS_txt62ClienteMunicipio,
acys.ACS_txt62ClienteEstado,
acys.ACS_txt62ClienteCodPost,
acys.ACS_chk62DocFactFranquiciaEnt,
acys.ACS_txt62DocFactFranquiciaEntCop,
acys.ACS_chk62DocFactFranquiciaRec,
acys.ACS_txt62DocFactFranquiciaRecCop,
acys.ACS_chk62DocFactKeyEnt,
acys.ACS_txt62DocFactKeyEntCop,
acys.ACS_chk62DocFactKeyRec,
acys.ACS_txt62DocFactKeyRecCop,
acys.ACS_chk62DocOrdCompraEnt,
acys.ACS_txt62DocOrdCompraEntCop,
acys.ACS_chk62DocOrdCompraRec,
acys.ACS_txt62DocOrdCompraRecCop,
acys.ACS_chk62DocOrdReposEnt,
acys.ACS_txt62DocOrdReposEntCop,
acys.ACS_chk62DocOrdReposRec,
acys.ACS_txt62DocOrdReposRecCop,
acys.ACS_chk62DocCopPedidoEnt,
acys.ACS_txt62DocCopPedidoEntCop,
acys.ACS_chk62DocCopPedidoRec,
acys.ACS_txt62DocCopPedidoRecCop,
acys.ACS_chk62DocRemisionEnt,
acys.ACS_txt62DocRemisionEntCop,
acys.ACS_chk62DocRemisionRec,
acys.ACS_txt62DocRemisionRecCop,
acys.ACS_chk62DocFolioEnt,
acys.ACS_txt62DocFolioEntCop,
acys.ACS_chk62DocFolioRec,
acys.ACS_txt62DocFolioRecCop,
acys.ACS_chk62DocContraRecEnt,
acys.ACS_txt62DocContraRecEntCop,
acys.ACS_chk62DocContraRecRec,
acys.ACS_txt62DocContraRecRecCop,
acys.ACS_chk62DocEntAlmacenEnt,
acys.ACS_txt62DocEntAlmacenEntCop,
acys.ACS_chk62DocEntAlmacenRec,
acys.ACS_txt62DocEntAlmacenRecCop,
acys.ACS_chk62DocSopServicioEnt,
acys.ACS_txt62DocSopServicioEntCop,
acys.ACS_chk62DocSopServicioRec,
acys.ACS_txt62DocSopServicioRecCop,
acys.ACS_chk62DocNomFirmaEnt,
acys.ACS_txt62DocNomFirmaEntCop,
acys.ACS_chk62DocNomFirmaoRec,
acys.ACS_txt62DocNomFirmaRecCop,
acys.ACS_chk62CitaEnt,
acys.ACS_txt62CitaEntCop,
acys.ACS_chk62CitaRec,
acys.ACS_txt62CitaRecCop,
acys.ACS_chk63Lunes,
acys.ACS_chk63Martes,
acys.ACS_chk63Miercoles,
acys.ACS_chk63Jueves,
acys.ACS_chk63Viernes,
acys.ACS_chk63Sabado,
acys.ACS_Rad63TimePicker163,
acys.ACS_Rad63TimePicker263,
acys.ACS_Rad63TimePicker363,
acys.ACS_Rad63TimePicker463,
acys.ACS_txtRecPersonaRecibe63,
acys.ACS_txtRecPuesto63,
acys.ACS_Chk63Mismodia,
acys.ACS_Chk63Sincita,
acys.ACS_Chk63Previa,
acys.ACS_txt63CitaContacto,
acys.ACS_txt63CitaTelefono,
acys.ACS_txt63CitaDiasdeAnticipacion,
acys.ACS_chk63AreaPropia,
acys.ACS_chk63AreaPlaza,
acys.ACS_chk63AreaCalle,
acys.ACS_chk63AreaAvTransitada,
acys.ACS_chk63EstCortesia,
acys.ACS_chk63EstCosto,
acys.ACS_txt63EstMonto,
acys.ACS_txt63ClienteDireccion,
acys.ACS_txt63ClienteColonia,
acys.ACS_txt63ClienteMunicipio,
acys.ACS_txt63ClienteEstado,
acys.ACS_txt63ClienteCodPost,
acys.ACS_chk63DocFactFranquiciaEnt,
acys.ACS_txt63DocFactFranquiciaEntCop,
acys.ACS_chk63DocFactFranquiciaRec,
acys.ACS_txt63DocFactFranquiciaRecCop,
acys.ACS_chk63DocFactKeyEnt,
acys.ACS_txt63DocFactKeyEntCop,
acys.ACS_chk63DocFactKeyRec,
acys.ACS_txt63DocFactKeyRecCop,
acys.ACS_chk63DocOrdCompraEnt,
acys.ACS_txt63DocOrdCompraEntCop,
acys.ACS_chk63DocOrdCompraRec,
acys.ACS_txt63DocOrdCompraRecCop,
acys.ACS_chk63DocOrdReposEnt,
acys.ACS_txt63DocOrdReposEntCop,
acys.ACS_chk63DocOrdReposRec,
acys.ACS_txt63DocOrdReposRecCop,
acys.ACS_chk63DocCopPedidoEnt,
acys.ACS_txt63DocCopPedidoEntCop,
acys.ACS_chk63DocCopPedidoRec,
acys.ACS_txt63DocCopPedidoRecCop,
acys.ACS_chk63DocRemisionEnt,
acys.ACS_txt63DocRemisionEntCop,
acys.ACS_chk63DocRemisionRec,
acys.ACS_txt63DocRemisionRecCop,
acys.ACS_chk63DocFolioEnt,
acys.ACS_txt63DocFolioEntCop,
acys.ACS_chk63DocFolioRec,
acys.ACS_txt63DocFolioRecCop,
acys.ACS_chk63DocContraRecEnt,
acys.ACS_txt63DocContraRecEntCop,
acys.ACS_chk63DocContraRecRec,
acys.ACS_txt63DocContraRecRecCop,
acys.ACS_chk63DocEntAlmacenEnt,
acys.ACS_txt63DocEntAlmacenEntCop,
acys.ACS_chk63DocEntAlmacenRec,
acys.ACS_txt63DocEntAlmacenRecCop,
acys.ACS_chk63DocSopServicioEnt,
acys.ACS_txt63DocSopServicioEntCop,
acys.ACS_chk63DocSopServicioRec,
acys.ACS_txt63DocSopServicioRecCop,
acys.ACS_chk63DocNomFirmaEnt,
acys.ACS_txt63DocNomFirmaEntCop,
acys.ACS_chk63DocNomFirmaoRec,
acys.ACS_txt63DocNomFirmaRecCop,
acys.ACS_chk63CitaEnt,
acys.ACS_txt63CitaEntCop,
acys.ACS_chk63CitaRec,
acys.ACS_txt63CitaRecCop


                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Modificar", ref verificador, Parametros, Valores);

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion", 
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_UltSCpt",
                            "@Acs_UltACpt",
                            "@Acs_FechaInicio",
                            "@Acs_FechaFin",
                            "@Acs_CantTotal",
                            "@Id_TG"

                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
                            acys.Id_AcsVersion,
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,
                            list[x].Acys_UltSCtp == -1 ? (int?)null : list[x].Acys_UltSCtp,
                            list[x].Acys_UltACtp == -1 ? (int?)null : list[x].Acys_UltACtp,
                            list[x].Acys_FechaInicio,
                            list[x].Acys_FechaFin,
                            list[x].Acys_CantTotal,
                            list[x].Id_TG
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, Parametros, Valores);
                }


                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni, a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores);
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoMensual, a.ServTecnicoRellenoMensualfechaIni, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores);
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores);
                    modificar = 0;
                }


                //Datos Garantia    
                int verificador5 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@FactorGarantia", "@UPrimaNeta", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, a.FactorGarantia, a.UPrimaNeta, DateTime.Now };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spUpdCapAcysDatosGarantia", ref verificador5, Parametros, Valores);
                }

                // Calendario Fechas de Corte
                int verificador6 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_AcsVersion", "@Id_TG", "@Mes", "@FechaCorte" };
                foreach (AcysDatosGarantia a in listaGarantia)
                {
                    if (a.Fechas_Corte != null)
                        for (int i = 1; i <= 12; i++)
                        {
                            if (a.Fechas_Corte.ContainsKey(i))
                            {
                                Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, acys.Id_AcsVersion, a.Id_TG, i, a.Fechas_Corte[i] };
                                sqlcmd = CapaDatos.GenerarSqlCommand("spUpdCapAcysDatosGarantia_Fechas", ref verificador6, Parametros, Valores);
                            }
                        }
                }

                //Actualizar Calendario
                ActualizaControlCalendario(acys, ValoresCalendario, Conexion);
                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs", 
                            "@Id_AcsVersion", 
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };
                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            acys.Id_AcsVersion,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                            eliminar++;
                        }
                    }
                }
                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }


        public void Modificar_Log(List<Producto> List_ServiciosMantenimiento_Or, List<Producto> List_ServicioTec_Or, List<Asesoria> List_Asesoria_Or, DataTable dtAcuerdos_Or, Acys acys_Or, Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, string _Usuario, string _Pantalla)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Or_Id_Emp",
                                          "@Or_Id_Cd",
                                          "@Or_Id_Acs",
                                          "@Or_Id_AcsVersion", 
                                          "@Or_Id_Ter",
                                          "@Or_Id_Rik",
                                          "@Or_Id_Cte",
                                          "@Or_Cte_Nombre",
                                          "@Or_Acs_Fecha",
                                          "@Or_Acs_Contacto",
                                          "@Or_Acs_Puesto",
                                          "@Or_Acs_Telefono",
                                          "@Or_Acs_Correo",
                                          "@Or_Acs_Contacto2",
                                          "@Or_Acs_Telefono2",
                                          "@Or_Acs_Correo2",
                                          "@Or_Acs_Contacto3",
                                          "@Or_Acs_Telefono3",
                                          "@Or_Acs_Correo3",
                                          "@Or_Acs_Contacto4",
                                          "@Or_Acs_Telefono4",
                                          "@Or_Acs_Correo4",
                                          "@Or_Acs_Contacto5",
                                          "@Or_Acs_Telefono5",
                                          "@Or_Acs_Correo5",
                                          "@Or_Acs_Contacto6",
                                          "@Or_Acs_Telefono6",
                                          "@Or_Acs_Correo6",
                                          "@Or_Acs_Proveedor",
                                          "@Or_Acs_RutaEntrega",
                                          "@Or_Acs_RutaServicio",
                                          "@Or_Acs_ReqOrdenCompra",
                                          "@Or_Acs_VigenciaIni",
                                          "@Or_Acs_Semana",
                                          "@Or_Acs_ReqConfirmacion",
                                          "@Or_Acs_RecPedCorreo",
                                          "@Or_Acs_RecPedFax",
                                          "@Or_Acs_RecPedTel",
                                          "@Or_Acs_RecPedRep",
                                          "@Or_Acs_RecPedOtro",
                                          "@Or_Acs_RecPedOtroStr",
                                          "@Or_Acs_PedidoEncargadoEnviar",
                                          "@Or_Acs_PedidoPuesto",
                                          "@Or_Acs_PedidoTelefono",
                                          "@Or_Acs_PedidoEmail",
                                          "@Or_Acs_RecDocReposicion",
                                          "@Or_Acs_RecDocFolio",
                                          "@Or_Acs_RecDocOtro",
                                          "@Or_Acs_VisFrecuencia",
                                          "@Or_Acs_VisitaOtro",                                          
                                          "@Or_Acs_ReqServAsesoria",
                                          "@Or_Acs_ReqServTecnicoRelleno",
                                          "@Or_Acs_ReqServMantenimiento",
                                          "@Or_Acs_Notas",
                                          "@Or_Acs_ContactoRepVenta",
                                          "@Or_Acs_ContactoRepVentaTel",
                                          "@Or_Acs_ContactoRepVentaEmail",
                                          "@Or_Acs_ContactoRepServ",
                                          "@Or_Acs_ContactoRepServTel",
                                          "@Or_Acs_ContactoRepServEmail",
                                          "@Or_Acs_ContactoJefServ",
                                          "@Or_Acs_ContactoJefServTel",
                                          "@Or_Acs_ContactoJefServEmail",
                                          "@Or_Acs_ContactoAseServ",
                                          "@Or_Acs_ContactoAseServTel",
                                          "@Or_Acs_ContactoAseServEmail",
                                          "@Or_Acs_ContactoJefOper",
                                          "@Or_Acs_ContactoJefOperTel",
                                          "@Or_Acs_ContactoJefOperEmail",
                                          "@Or_Acs_ContactoCAlmRep",
                                          "@Or_Acs_ContactoCAlmRepTel",
                                          "@Or_Acs_ContactoCAlmRepEmail",
                                          "@Or_Acs_ContactoCServTec",
                                          "@Or_Acs_ContactoCServTecTel",
                                          "@Or_Acs_ContactoCServTecEmail",
                                          "@Or_Acs_ContactoCCreCob",
                                          "@Or_Acs_ContactoCCreCobTel",
                                          "@Or_Acs_ContactoCCreCobEmail",
                                          "@Or_Acs_FechaInicio",
                                          "@Or_Acs_FechaFin",
                                          "@Or_Id_U",
                                          "@Or_Acs_Modalidad",
                                          "@Or_Id_CteDirEntrega",
                                          //---
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion", 
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisFrecuencia",
                                          "@Acs_VisitaOtro",                                          
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U",
                                          "@Acs_Modalidad",
                                          "@Id_CteDirEntrega",                                          
                                          "@Usuario",
                                          "@Pantalla",             
                                          "@Descripcion",
                                          "@Codigo"
                                      };
                object[] Valores = {
                                       acys_Or.Id_Emp,
                                       acys_Or.Id_Cd ,
                                       acys_Or.Id_Acs,
                                       acys_Or.Id_AcsVersion,
                                       acys_Or.Id_Ter ,
                                       acys_Or.Id_Rik ,
                                       acys_Or.Id_Cte ,
                                       acys_Or.Cte_Nombre,
                                       acys_Or.Acs_Fecha,
                                       acys_Or.Acs_Contacto,
                                       acys_Or.Acs_Puesto,
                                       acys_Or.Acs_Telefono,
                                       acys_Or.Acs_Correo,
                                       acys_Or.Acs_Contacto2,
                                       acys_Or.Acs_Telefono2,
                                       acys_Or.Acs_Correo2,
                                       acys_Or.Acs_Contacto3,
                                       acys_Or.Acs_Telefono3,
                                       acys_Or.Acs_Correo3,
                                       acys_Or.Acs_Contacto4,
                                       acys_Or.Acs_Telefono4,
                                       acys_Or.Acs_Correo4,
                                       acys_Or.Acs_Contacto5,
                                       acys_Or.Acs_Telefono5,
                                       acys_Or.Acs_Correo5,
                                       acys_Or.Acs_Contacto6,
                                       acys_Or.Acs_Telefono6,
                                       acys_Or.Acs_Correo6,
                                       acys_Or.Acs_Proveedor,
                                       acys_Or.Acs_RutaEntrega,
                                       acys_Or.Acs_RutaServicio,
                                       acys_Or.Acs_ReqOrdenCompra,
                                       acys_Or.Acs_VigenciaIni,
                                       acys_Or.Acs_Semana,
                                       acys_Or.Acs_ReqConfirmacion,
                                       acys_Or.Acs_RecPedCorreo,
                                       acys_Or.Acs_RecPedFax,
                                       acys_Or.Acs_RecPedTel,
                                       acys_Or.Acs_RecPedRep,
                                       acys_Or.Acs_RecPedOtro,
                                       acys_Or.Acs_RecPedOtroStr,
                                       acys_Or.Acs_PedidoEncargadoEnviar,
                                       acys_Or.Acs_PedidoPuesto,
                                       acys_Or.Acs_PedidoTelefono,
                                       acys_Or.Acs_PedidoEmail,
                                       acys_Or.Acs_RecDocReposicion,
                                       acys_Or.Acs_RecDocFolio,
                                       acys_Or.Acs_RecDocOtro,
                                       acys_Or.Vis_Frecuencia,
                                       acys_Or.Acs_VisitaOtro,
                                       acys_Or.Acs_ReqServAsesoria,
                                       acys_Or.Acs_ReqServTecnicoRelleno,
                                       acys_Or.Acs_ReqServMantenimiento,
                                       acys_Or.Acs_Notas,
                                       acys_Or.Acs_ContactoRepVenta,
                                       acys_Or.Acs_ContactoRepVentaTel,
                                       acys_Or.Acs_ContactoRepVentaEmail,
                                       acys_Or.Acs_ContactoRepServ,
                                       acys_Or.Acs_ContactoRepServTel,
                                       acys_Or.Acs_ContactoRepServEmail,
                                       acys_Or.Acs_ContactoJefServ,
                                       acys_Or.Acs_ContactoJefServTel,
                                       acys_Or.Acs_ContactoJefServEmail,
                                       acys_Or.Acs_ContactoAseServ,
                                       acys_Or.Acs_ContactoAseServTel,
                                       acys_Or.Acs_ContactoAseServEmail,
                                       acys_Or.Acs_ContactoJefOper,
                                       acys_Or.Acs_ContactoJefOperTel,
                                       acys_Or.Acs_ContactoJefOperEmail,
                                       acys_Or.Acs_ContactoCAlmRep,
                                       acys_Or.Acs_ContactoCAlmRepTel,
                                       acys_Or.Acs_ContactoCAlmRepEmail,
                                       acys_Or.Acs_ContactoCServTec,
                                       acys_Or.Acs_ContactoCServTecTel,
                                       acys_Or.Acs_ContactoCServTecEmail,
                                       acys_Or.Acs_ContactoCCreCob,
                                       acys_Or.Acs_ContactoCCreCobTel,
                                       acys_Or.Acs_ContactoCCreCobEmail,
                                       acys_Or.Acs_FechaInicioDocumento,
                                       acys_Or.Acs_FechaFinDocumento,
                                       acys_Or.Id_U,
                                       acys_Or.Acs_Modalidad,
                                       acys_Or.IdCte_DirEntrega,
                                       //---
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Vis_Frecuencia,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U,
                                       acys.Acs_Modalidad,
                                       acys.IdCte_DirEntrega,
                                       _Usuario,
                                       _Pantalla,
                                       "",
                                       ""
                                      
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spLogs_Acys", ref verificador, Parametros, Valores);

                Parametros = new string[] {                     		
		                    "@Or_Id_Prd",  
                            "@Or_Acs_Cantidad",  
                            "@Or_Acs_Documento",  
                            "@Or_Acs_Sabado",  
                            "@Or_Acs_Viernes",  
                            "@Or_Acs_Jueves",  
                            "@Or_Acs_Miercoles", 
                            "@Or_Acs_Martes",  
                            "@Or_Acs_Lunes",  
                            "@Or_Acs_Frecuencia",
                            "@Or_Acs_Precio",
                            //---
                            "@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
                            "@Id_AcsVersion", 
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",                                          
                            "@Usuario",
                            "@Pantalla",
                            "@Descripcion",
                            "@Codigo"


                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    DataRow[] dr = dtAcuerdos_Or.Select("Id_Prd =" + list[x].Id_Prd);

                    if (dr.Count() > 0)
                    {


                        @Valores = new object[] {                            
                            dr[0][0] ,
                            dr[0][5],
                            dr[0][13],
                            dr[0][12],
                            dr[0][11],
                            dr[0][10],
                            dr[0][9],
                            dr[0][8],
                            dr[0][7],
                            dr[0][6],
                            dr[0][4],
                            //---
                            acys.Id_Emp,
                            acys.Id_Cd ,
                            x, 
                            acys.Id_Acs, 
                            acys.Id_AcsVersion,
                            list[x].Id_Prd, 
                            list[x].Acys_Cantidad, 
                            list[x].Acs_Doc, 
                            list[x].Acys_Sabado, 
                            list[x].Acys_Viernes, 
                            list[x].Acys_Jueves, 
                            list[x].Acys_Miercoles, 
                            list[x].Acys_Martes, 
                            list[x].Acys_Lunes, 
                            list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,
                            _Usuario,
                             _Pantalla,
                             "ID_Producto",
                             list[x].Id_Prd
                        };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spLogs_Acys_Det", ref verificador, Parametros, Valores);
                    }
                }


                //ASESORIAS
                int verificador2 = 0;

                Parametros = new string[] {"@Id_Acs",  
                                           "@Or_Id_Ase", 
                                           "@Or_Mensual", 
                                           "@Or_FechaInicioMensual",
                                           "@Or_Bimestral", 
                                           "@Or_FechaInicioBimestral", 
                                           "@Or_Trimestral", 
                                           "@Or_FechaInicioTrimestral", 
                                           "@Id_Ase", 
                                           "@Mensual", 
                                           "@FechaInicioMensual", 
                                           "@Bimestral", 
                                           "@FechaInicioBimestral", 
                                           "@Trimestral", 
                                           "@FechaInicioTrimestral", 
                                           "@Usuario",
                                           "@Pantalla",
                                           "@Descripcion",
                                           "@Codigo" };
                foreach (Asesoria a in asesorias)
                {
                    Asesoria asesoria_Or = List_Asesoria_Or.Where(x => x.Id_Ase == a.Id_Ase).FirstOrDefault();

                    Valores = new object[] { acys.Id_Acs, 
                                             asesoria_Or.Id_Ase, 
                                             asesoria_Or.Ase_ServAsesoriaMensual, 
                                             asesoria_Or.Ase_ServAsesoriaMensualfechaIni, 
                                             asesoria_Or.Ase_ServAsesoriaBimestral, 
                                             asesoria_Or.Ase_ServAsesoriaBimestralfechaIni, 
                                             asesoria_Or.Ase_ServAsesoriaTrimestral, 
                                             asesoria_Or.Ase_ServAsesoriaTrimestralfechaIni,
                                             a.Id_Ase, 
                                             a.Ase_ServAsesoriaMensual, 
                                             a.Ase_ServAsesoriaMensualfechaIni,
                                             a.Ase_ServAsesoriaBimestral, 
                                             a.Ase_ServAsesoriaBimestralfechaIni, 
                                             a.Ase_ServAsesoriaTrimestral, 
                                             a.Ase_ServAsesoriaTrimestralfechaIni,
                                             _Usuario,
                                             "Acuerdos Comerciales y Servicios - Servicio de Asesoria",
                                            "Id_Ase",
                                             a.Id_Ase                    
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spLogs_Asesoria", ref verificador2, Parametros, Valores);

                }

                //SERVICIOS RELLENO
                int verificador3 = 0;

                Parametros = new string[] {
                                            "@Or_Id_Prd", 
                                            "@Or_Revision", 
                                            "@Or_Mensual", 
                                            "@Or_FechaInicioMensual", 
                                            "@Or_Bimestral", 
                                            "@Or_FechaInicioBimestral", 
                                            "@Or_Trimestral", 
                                            "@Or_FechaInicioTrimestral", 
                                            //--
                                            "@Id_Acs",  
                                            "@Id_Prd", 
                                            "@Revision", 
                                            "@Mensual", 
                                            "@FechaInicioMensual", 
                                            "@Bimestral", 
                                            "@FechaInicioBimestral", 
                                            "@Trimestral", 
                                            "@FechaInicioTrimestral", 
                                            "@Usuario",
                                            "@Pantalla",
                                            "@Descripcion",
                                            "@Codigo"
                                             };
                foreach (Producto a in servicios)
                {
                    Producto Producto_Or = List_ServicioTec_Or.Where(x => x.Id_Prd == a.Id_Prd).FirstOrDefault();
                    Valores = new object[] {
                                            
                                            Producto_Or.Id_Prd, 
                                            Producto_Or.Prd_InvInicial, 
                                            Producto_Or.ServTecnicoRellenoMensual, 
                                            Producto_Or.ServTecnicoRellenoMensualfechaIni, 
                                            Producto_Or.ServTecnicoRellenoBimestral, 
                                            Producto_Or.ServTecnicoRellenoBimestralfechaIni,
                                            Producto_Or.ServTecnicoRellenoTrimestral,
                                            Producto_Or.ServTecnicoRellenoTrimestralfechaIni, 
                                            //---
                                            acys.Id_Acs, 
                                            a.Id_Prd,
                                            a.Prd_InvInicial, 
                                            a.ServTecnicoRellenoMensual, 
                                            a.ServTecnicoRellenoMensualfechaIni, 
                                            a.ServTecnicoRellenoBimestral, 
                                            a.ServTecnicoRellenoBimestralfechaIni,
                                            a.ServTecnicoRellenoTrimestral,
                                            a.ServTecnicoRellenoTrimestralfechaIni,
                                             _Usuario,
                                             "Acuerdos Comerciales y Servicios - Servicio Técnico",
                                            "Id_Prd",
                                             a.Id_Prd};
                    sqlcmd = CapaDatos.GenerarSqlCommand("spLogs_ServiciosTecnico", ref verificador3, Parametros, Valores);

                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;

                Parametros = new string[] {                                            
                                            "@Or_Id_Prd", 
                                            "@Or_Revision", 
                                            "@Or_Mensual", 
                                            "@Or_FechaInicioMensual", 
                                            "@Or_Bimestral", 
                                            "@Or_FechaInicioBimestral", 
                                            "@Or_Trimestral", 
                                            "@Or_FechaInicioTrimestral", 
                                            //----                                            
                                            "@Id_Acs",                                             
                                            "@Id_Prd", 
                                            "@Revision", 
                                            "@Mensual", 
                                            "@FechaInicioMensual", 
                                            "@Bimestral", 
                                            "@FechaInicioBimestral", 
                                            "@Trimestral", 
                                            "@FechaInicioTrimestral", 
                                            "@Usuario",
                                            "@Pantalla",
                                            "@Descripcion",
                                            "@Codigo" };

                foreach (Producto a in serviciosMantenimiento)
                {
                    Producto Producto_Or = List_ServiciosMantenimiento_Or.Where(x => x.Id_Prd == a.Id_Prd).FirstOrDefault();
                    Valores = new object[] { Producto_Or.Id_Prd, 
                                             Producto_Or.Prd_InvInicial, 
                                             Producto_Or.ServMantenimientoMensual, 
                                             Producto_Or.ServMantenimientoMensualfechaIni, 
                                             Producto_Or.ServMantenimientoBimestral, 
                                             Producto_Or.ServMantenimientoBimestralfechaIni, 
                                             Producto_Or.ServMantenimientoTrimestral, 
                                             Producto_Or.ServMantenimientoTrimestralfechaIni,
                                             acys.Id_Acs, 
                                             a.Id_Prd, 
                                             a.Prd_InvInicial, 
                                             a.ServMantenimientoMensual, 
                                             a.ServMantenimientoMensualfechaIni, 
                                             a.ServMantenimientoBimestral, 
                                             a.ServMantenimientoBimestralfechaIni, 
                                             a.ServMantenimientoTrimestral, 
                                             a.ServMantenimientoTrimestralfechaIni,
                                             _Usuario,
                                             "Acuerdos Comerciales y Servicios - Servicio Mantenimiento Preventivo/Revisión",
                                            "Id_Prd",
                                             a.Id_Prd                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spLogs_ServiciosMantenimiento", ref verificador4, Parametros, Valores);

                }


                //
                //int eliminar = 1;
                //Parametros = new string[] { 
                //            "@Id_Emp", 
                //            "@Id_Cd",  
                //            "@Id_Acs", 
                //            "@Id_AcsVersion", 
                //            "@Id_PrdOriginal",  
                //            "@Id_PrdEquivalente",  
                //            "@Eliminar"
                //    };
                //if (dt != null)
                //{
                //    for (int x = 0; x < dt.Rows.Count; x++)
                //    {
                //        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                //        {
                //            object[] ValoresEquivalencias = new object[] {                         
                //            acys.Id_Emp,
                //            acys.Id_Cd,
                //            acys.Id_Acs,
                //            acys.Id_AcsVersion,
                //            dt.Rows[x]["Id_Original"],
                //            dt.Rows[x]["Id_Similar"],
                //            eliminar
                //        };
                //            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                //            eliminar++;
                //        }
                //    }
                //}
                //if (verificador < 0)
                //{
                //    CapaDatos.RollBackTrans();
                //}
                //else
                //{
                CapaDatos.CommitTrans();
                //}
                verificador = 1;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void Cancelar(Acys acys, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);

            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                        
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs
                                     
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Baja", ref verificador, Parametros, Valores);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void CedVis_Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Funciones cdFuncs = new Funciones();

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion",
                                          "@Id_Cte"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion,
                                       acys.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysCedVis_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    acys = cdFuncs.GetEntity<Acys>(dr);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion" 
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    acys.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    acys.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    acys.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    acys.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));

                    acys.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Acs_NomComercial")).ToString();
                    acys.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    acys.Acs_FechaInicioDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")));
                    acys.Acs_FechaFinDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")));


                    acys.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    acys.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    acys.Acs_Telefono = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono")));
                    acys.Acs_Correo = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();

                    acys.ClienteDireccion = dr.GetValue(dr.GetOrdinal("ClienteDireccion")).ToString();
                    acys.ClienteColonia = dr.GetValue(dr.GetOrdinal("ClienteColonia")).ToString();
                    acys.ClienteMunicipio = dr.GetValue(dr.GetOrdinal("ClienteMunicipio")).ToString();
                    acys.ClienteEstado = dr.GetValue(dr.GetOrdinal("ClienteEstado")).ToString();
                    acys.ClienteRFC = dr.GetValue(dr.GetOrdinal("ClienteRFC")).ToString();
                    acys.ClienteCodPost = dr.GetValue(dr.GetOrdinal("ClienteCodPost")).ToString();
                    acys.CuentaCorporativa = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) > 1 && Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) != 10000) ? true : false;
                    acys.AddendaSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AddendaSi"))) > 0 ? true : false;

                    acys.DireccionEntrega = dr.GetValue(dr.GetOrdinal("DireccionEntrega")).ToString();
                    acys.ClienteColoniaE = dr.GetValue(dr.GetOrdinal("ClienteColoniaE")).ToString();
                    acys.ClienteMunicipioE = dr.GetValue(dr.GetOrdinal("ClienteMunicipioE")).ToString();
                    acys.ClienteCPE = dr.GetValue(dr.GetOrdinal("ClienteCPE")).ToString();
                    acys.ClienteEstadoE = dr.GetValue(dr.GetOrdinal("ClienteEstadoE")).ToString();


                    acys.Acs_Contacto2 = dr.GetValue(dr.GetOrdinal("Acs_Contacto2")).ToString();
                    acys.Acs_Telefono2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono2")));
                    acys.Acs_Correo2 = dr.GetValue(dr.GetOrdinal("Acs_Email2")).ToString();

                    acys.Acs_Contacto3 = dr.GetValue(dr.GetOrdinal("Acs_Contacto3")).ToString();
                    acys.Acs_Telefono3 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono3")));
                    acys.Acs_Correo3 = dr.GetValue(dr.GetOrdinal("Acs_email3")).ToString();

                    acys.Acs_Contacto4 = dr.GetValue(dr.GetOrdinal("Acs_Contacto4")).ToString();
                    acys.Acs_Telefono4 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono4")));
                    acys.Acs_Correo4 = dr.GetValue(dr.GetOrdinal("Acs_email4")).ToString();

                    acys.Acs_Contacto5 = dr.GetValue(dr.GetOrdinal("Acs_Contacto5")).ToString();
                    acys.Acs_Telefono5 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono5")));
                    acys.Acs_Correo5 = dr.GetValue(dr.GetOrdinal("Acs_email5")).ToString();

                    acys.Acs_Contacto6 = dr.GetValue(dr.GetOrdinal("Acs_Contacto6")).ToString();
                    acys.Acs_Telefono6 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono6")));
                    acys.Acs_Correo6 = dr.GetValue(dr.GetOrdinal("Acs_email6")).ToString();

                    acys.Acs_Proveedor = dr.GetValue(dr.GetOrdinal("Acs_NumPrv")).ToString();

                    acys.Acs_RutaServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta1")));
                    acys.Acs_RutaEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta2")));

                    acys.Acs_ReqOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    acys.Acs_VigenciaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")));
                    acys.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    acys.Acs_ReqConfirmacion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqConfirmacion")));

                    acys.Acs_RecPedCorreo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecCorreo")));
                    acys.Acs_RecPedFax = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFax")));
                    acys.Acs_RecPedTel = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecTelefono")));
                    acys.Acs_RecPedRep = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRepresentante")));
                    acys.Acs_RecPedOtro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOtro")));
                    acys.Acs_RecPedOtroStr = dr.GetValue(dr.GetOrdinal("Acs_RecOtroDesc")).ToString();

                    acys.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();


                    //VISITAS
                    acys.Vis_Frecuencia = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vis_Frecuencia")));
                    acys.Vis_Lunes = dr.GetBoolean(dr.GetOrdinal("Vis_Lunes"));
                    acys.Vis_Martes = dr.GetBoolean(dr.GetOrdinal("Vis_Martes"));
                    acys.Vis_Miercoles = dr.GetBoolean(dr.GetOrdinal("Vis_Miercoles"));
                    acys.Vis_Jueves = dr.GetBoolean(dr.GetOrdinal("Vis_Jueves"));
                    acys.Vis_Viernes = dr.GetBoolean(dr.GetOrdinal("Vis_Viernes"));
                    acys.Vis_Sabado = dr.GetBoolean(dr.GetOrdinal("Vis_Sabado"));
                    acys.Vis_HrAm1 = dr.GetValue(dr.GetOrdinal("Vis_HrAm1")).ToString();
                    acys.Vis_HrAm2 = dr.GetValue(dr.GetOrdinal("Vis_HrAm2")).ToString();
                    acys.Vis_HrPm1 = dr.GetValue(dr.GetOrdinal("Vis_HrPm1")).ToString();
                    acys.Vis_HrPm2 = dr.GetValue(dr.GetOrdinal("Vis_HrPm2")).ToString();

                    //RECEPCION DE PEDIDOS
                    acys.Rec_Semanas = dr.GetValue(dr.GetOrdinal("Rec_Semanas")).ToString();

                    acys.Rec_Lunes = dr.GetBoolean(dr.GetOrdinal("Rec_Lunes"));
                    acys.Rec_Martes = dr.GetBoolean(dr.GetOrdinal("Rec_Martes"));
                    acys.Rec_Miercoles = dr.GetBoolean(dr.GetOrdinal("Rec_Miercoles"));
                    acys.Rec_Jueves = dr.GetBoolean(dr.GetOrdinal("Rec_Jueves"));
                    acys.Rec_Viernes = dr.GetBoolean(dr.GetOrdinal("Rec_Viernes"));
                    acys.Rec_Sabado = dr.GetBoolean(dr.GetOrdinal("Rec_Sabado"));

                    acys.Rec_Confirmacion = dr.GetBoolean(dr.GetOrdinal("Rec_Confirmacion"));
                    acys.Rec_Correo = dr.GetBoolean(dr.GetOrdinal("Rec_Correo"));
                    acys.Rec_Fax = dr.GetBoolean(dr.GetOrdinal("Rec_Fax"));
                    acys.Rec_Telefono = dr.GetBoolean(dr.GetOrdinal("Rec_Telefono"));
                    acys.Rec_Representante = dr.GetBoolean(dr.GetOrdinal("Rec_Representante"));
                    acys.Rec_Otro = dr.GetBoolean(dr.GetOrdinal("Rec_Otro"));

                    acys.Rec_OtroStr = dr.GetValue(dr.GetOrdinal("Rec_OtroStr")).ToString();


                    acys.Acs_PedidoEncargadoEnviar = dr.GetValue(dr.GetOrdinal("Acs_PedidoEncargadoEnviar")).ToString();
                    acys.Acs_PedidoPuesto = dr.GetValue(dr.GetOrdinal("Acs_PedidoPuesto")).ToString();
                    acys.Acs_PedidoTelefono = dr.GetValue(dr.GetOrdinal("Acs_PedidoTelefono")).ToString();
                    acys.Acs_PedidoEmail = dr.GetValue(dr.GetOrdinal("Acs_PedidoEmail")).ToString();


                    acys.Acs_RecDocReposicion = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocReposicion"));
                    acys.Acs_RecDocFolio = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocFolio"));
                    acys.Acs_RecDocOtro = dr.GetValue(dr.GetOrdinal("Acs_RecDocOtro")).ToString();


                    acys.Acs_VisitaOtro = dr.GetValue(dr.GetOrdinal("Acs_VisitaOtro")).ToString();
                    acys.Acs_ReqServAsesoria = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServAsesoria"));
                    acys.Acs_ReqServTecnicoRelleno = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServTecnicoRelleno"));
                    acys.Acs_ReqServMantenimiento = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServMantenimiento"));

                    acys.Acs_Notas = dr.GetValue(dr.GetOrdinal("Acs_Notas")).ToString();

                    acys.Acs_ContactoRepVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVenta")));
                    acys.Acs_ContactoRepVentaTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaTel")).ToString();
                    acys.Acs_ContactoRepVentaEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaEmail")).ToString();

                    acys.Acs_ContactoRepServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServ")));
                    acys.Acs_ContactoRepServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServTel")).ToString();
                    acys.Acs_ContactoRepServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServEmail")).ToString();

                    acys.Acs_ContactoJefServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServ")));
                    acys.Acs_ContactoJefServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServTel")).ToString();
                    acys.Acs_ContactoJefServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServEmail")).ToString();

                    acys.Acs_ContactoAseServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServ")));
                    acys.Acs_ContactoAseServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServTel")).ToString();
                    acys.Acs_ContactoAseServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServEmail")).ToString();


                    acys.Acs_ContactoJefOper = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOper")));
                    acys.Acs_ContactoJefOperTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperTel")).ToString();
                    acys.Acs_ContactoJefOperEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperEmail")).ToString();

                    acys.Acs_ContactoCAlmRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRep")));
                    acys.Acs_ContactoCAlmRepTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepTel")).ToString();
                    acys.Acs_ContactoCAlmRepEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepEmail")).ToString();

                    acys.Acs_ContactoCServTec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTec")));
                    acys.Acs_ContactoCServTecTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecTel")).ToString();
                    acys.Acs_ContactoCServTecEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecEmail")).ToString();

                    acys.Acs_ContactoCCreCob = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCob")));
                    acys.Acs_ContactoCCreCobTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobTel")).ToString();
                    acys.Acs_ContactoCCreCobEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobEmail")).ToString();
                    acys.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString();
                    acys.Acs_Version = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Version")));
                    acys.Acs_RikNombre = dr.GetValue(dr.GetOrdinal("Acs_RikNombre")).ToString();
                    acys.IdCte_DirEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Id_CteDirEntrega")));



                    acys.Acs_Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_Sucursal")));
                    acys.Acs_ParcialidadesSi = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesSi")));
                    acys.Acs_ParcialidadesNo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ParcialidadesNo")));
                    acys.Acs_ConfirmacionPedidosSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosSI")));
                    acys.Acs_ConfirmacionPedidosnO = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ConfirmacionPedidosnO")));
                    acys.Acs_chkRecRevLunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_chkRecRevLunes")));
                    acys.Acs_RecRevMartes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMartes")));
                    acys.Acs_RecRevMiercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevMiercoles")));
                    acys.Acs_RecRevJueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevJueves")));
                    acys.Acs_RecRevViernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevViernes")));
                    acys.Acs_RecRevSabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecRevSabado")));
                    acys.Acs_TimePicker1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker1")));
                    acys.Acs_TimePicker2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker2")));
                    acys.Acs_TimePicker3 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker3")));
                    acys.Acs_TimePicker4 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_TimePicker4")));
                    acys.Acs_RecPersonaRecibe = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPersonaRecibe")));
                    acys.Acs_RecPuesto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecPuesto")));
                    acys.Acs_RecCitaMismoDia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaMismoDia")));
                    acys.Acs_RecCitaSinCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaSinCita")));
                    acys.Acs_RecCitaPrevia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaPrevia")));
                    acys.Acs_RecCitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaContacto")));
                    acys.Acs_RecCitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_RecCitaTelefono")));
                    acys.Acs_RecCitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecCitaDiasdeAnticipacion")));
                    acys.Acs_RecAreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPropia")));
                    acys.Acs_RecAreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaPlaza")));
                    acys.Acs_RecAreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaCalle")));
                    acys.Acs_RecAreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecAreaAvTransitada")));
                    acys.Acs_RecEstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCortesia")));
                    acys.Acs_RecEstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstCosto")));
                    acys.Acs_RecEstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecEstMonto")));
                    acys.Acs_RecDocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactFranquiciaEnt")));
                    acys.Acs_RecDocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactFranquiciaEntCop")));
                    acys.Acs_RecDocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactFranquiciaRec")));
                    acys.Acs_RecDocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactFranquiciaRecCop")));
                    acys.Acs_RecDocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEnt")));
                    acys.Acs_RecDocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyEntCop")));
                    acys.Acs_RecDocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRec")));
                    acys.Acs_RecDocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocFactKeyRecCop")));
                    acys.Acs_RecDocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEnt")));
                    acys.Acs_RecDocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraEntCop")));
                    acys.Acs_RecDocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRec")));
                    acys.Acs_RecDocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdCompraRecCop")));
                    acys.Acs_RecDocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEnt")));
                    acys.Acs_RecDocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposEntCop")));
                    acys.Acs_RecDocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRec")));
                    acys.Acs_RecDocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocOrdReposRecCop")));
                    acys.Acs_RecDocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEnt")));
                    acys.Acs_RecDocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoEntCop")));
                    acys.Acs_RecDocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRec")));
                    acys.Acs_RecDocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_RecDocCopPedidoRecCop")));
                    acys.ACS_RecDocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEnt")));
                    acys.ACS_RecDocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionEntCop")));
                    acys.ACS_RecDocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRec")));
                    acys.ACS_RecDocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocRemisionRecCop")));
                    acys.ACS_RecDocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEnt")));
                    acys.ACS_RecDocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioEntCop")));
                    acys.ACS_RecDocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRec")));
                    acys.ACS_RecDocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocFolioRecCop")));
                    acys.ACS_RecDocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEnt")));
                    acys.ACS_RecDocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecEntCop")));
                    acys.ACS_RecDocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRec")));
                    acys.ACS_RecDocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocContraRecRecCop")));
                    acys.ACS_RecDocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEnt")));
                    acys.ACS_RecDocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenEntCop")));
                    acys.ACS_RecDocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRec")));
                    acys.ACS_RecDocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocEntAlmacenRecCop")));
                    acys.ACS_RecDocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEnt")));
                    acys.ACS_RecDocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioEntCop")));
                    acys.ACS_RecDocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRec")));
                    acys.ACS_RecDocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocSopServicioRecCop")));
                    acys.ACS_RecDocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEnt")));
                    acys.ACS_RecDocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaEntCop")));
                    acys.ACS_RecDocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaoRec")));
                    acys.ACS_RecDocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecDocNomFirmaRecCop")));
                    acys.ACS_RecCitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEnt")));
                    acys.ACS_RecCitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaEntCop")));
                    acys.ACS_RecCitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRec")));
                    acys.ACS_RecCitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_RecCitaRecCop")));
                    acys.ACS_RecOtroRec = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RecOtro")));


                    acys.ACS_chk62Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Lunes")));
                    acys.ACS_chk62Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Martes")));
                    acys.ACS_chk62Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Miercoles")));
                    acys.ACS_chk62Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Jueves")));
                    acys.ACS_chk62Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Viernes")));
                    acys.ACS_chk62Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62Sabado")));
                    acys.ACS_RadTimePicker162 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker162")));
                    acys.ACS_RadTimePicker262 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker262")));
                    acys.ACS_RadTimePicker362 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker362")));
                    acys.ACS_RadTimePicker462 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_RadTimePicker462")));
                    acys.ACS_txtRecPersonaRecibe62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPersonaRecibe62")));
                    acys.ACS_txtRecPuesto62 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPuesto62")));
                    acys.ACS_Chk62Mismodia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Mismodia")));
                    acys.ACS_Chk62Sincita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Sincita")));
                    acys.ACS_Chk62Previa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk62Previa")));
                    acys.ACS_txt62CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaContacto")));
                    acys.ACS_txt62CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaTelefono")));
                    acys.ACS_txt62CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaDiasdeAnticipacion")));
                    acys.ACS_chk62AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPropia")));
                    acys.ACS_chk62AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaPlaza")));
                    acys.ACS_chk62AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaCalle")));
                    acys.ACS_chk62AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62AreaAvTransitada")));
                    acys.ACS_chk62EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCortesia")));
                    acys.ACS_chk62EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62EstCosto")));
                    acys.ACS_txt62EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62EstMonto")));
                    acys.ACS_txt62ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteDireccion")));
                    acys.ACS_txt62ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteColonia")));
                    acys.ACS_txt62ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteMunicipio")));
                    acys.ACS_txt62ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteEstado")));
                    acys.ACS_txt62ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt62ClienteCodPost")));
                    acys.ACS_chk62DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaEnt")));
                    acys.ACS_txt62DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaEntCop")));
                    acys.ACS_chk62DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactFranquiciaRec")));
                    acys.ACS_txt62DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactFranquiciaRecCop")));
                    acys.ACS_chk62DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyEnt")));
                    acys.ACS_txt62DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyEntCop")));
                    acys.ACS_chk62DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFactKeyRec")));
                    acys.ACS_txt62DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFactKeyRecCop")));
                    acys.ACS_chk62DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraEnt")));
                    acys.ACS_txt62DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraEntCop")));
                    acys.ACS_chk62DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdCompraRec")));
                    acys.ACS_txt62DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdCompraRecCop")));
                    acys.ACS_chk62DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposEnt")));
                    acys.ACS_txt62DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposEntCop")));
                    acys.ACS_chk62DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocOrdReposRec")));
                    acys.ACS_txt62DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocOrdReposRecCop")));
                    acys.ACS_chk62DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoEnt")));
                    acys.ACS_txt62DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoEntCop")));
                    acys.ACS_chk62DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocCopPedidoRec")));
                    acys.ACS_txt62DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocCopPedidoRecCop")));
                    acys.ACS_chk62DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionEnt")));
                    acys.ACS_txt62DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionEntCop")));
                    acys.ACS_chk62DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocRemisionRec")));
                    acys.ACS_txt62DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocRemisionRecCop")));
                    acys.ACS_chk62DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioEnt")));
                    acys.ACS_txt62DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioEntCop")));
                    acys.ACS_chk62DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocFolioRec")));
                    acys.ACS_txt62DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocFolioRecCop")));
                    acys.ACS_chk62DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecEnt")));
                    acys.ACS_txt62DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecEntCop")));
                    acys.ACS_chk62DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocContraRecRec")));
                    acys.ACS_txt62DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocContraRecRecCop")));
                    acys.ACS_chk62DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenEnt")));
                    acys.ACS_txt62DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenEntCop")));
                    acys.ACS_chk62DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocEntAlmacenRec")));
                    acys.ACS_txt62DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocEntAlmacenRecCop")));
                    acys.ACS_chk62DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioEnt")));
                    acys.ACS_txt62DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioEntCop")));
                    acys.ACS_chk62DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocSopServicioRec")));
                    acys.ACS_txt62DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocSopServicioRecCop")));
                    acys.ACS_chk62DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaEnt")));
                    acys.ACS_txt62DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaEntCop")));
                    acys.ACS_chk62DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62DocNomFirmaoRec")));
                    acys.ACS_txt62DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62DocNomFirmaRecCop")));
                    acys.ACS_chk62CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaEnt")));
                    acys.ACS_txt62CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaEntCop")));
                    acys.ACS_chk62CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk62CitaRec")));
                    acys.ACS_txt62CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt62CitaRecCop")));
                    acys.ACS_chk63Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Lunes")));
                    acys.ACS_chk63Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Martes")));
                    acys.ACS_chk63Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Miercoles")));
                    acys.ACS_chk63Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Jueves")));
                    acys.ACS_chk63Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Viernes")));
                    acys.ACS_chk63Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63Sabado")));
                    acys.ACS_Rad63TimePicker163 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker163")));
                    acys.ACS_Rad63TimePicker263 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker263")));
                    acys.ACS_Rad63TimePicker363 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker363")));
                    acys.ACS_Rad63TimePicker463 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_Rad63TimePicker463")));
                    acys.ACS_txtRecPersonaRecibe63 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPersonaRecibe63")));
                    acys.ACS_txtRecPuesto63 = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txtRecPuesto63")));
                    acys.ACS_Chk63Mismodia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk63Mismodia")));
                    acys.ACS_Chk63Sincita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk63Sincita")));
                    acys.ACS_Chk63Previa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_Chk63Previa")));
                    acys.ACS_txt63CitaContacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaContacto")));
                    acys.ACS_txt63CitaTelefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaTelefono")));
                    acys.ACS_txt63CitaDiasdeAnticipacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaDiasdeAnticipacion")));
                    acys.ACS_chk63AreaPropia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPropia")));
                    acys.ACS_chk63AreaPlaza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaPlaza")));
                    acys.ACS_chk63AreaCalle = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaCalle")));
                    acys.ACS_chk63AreaAvTransitada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63AreaAvTransitada")));
                    acys.ACS_chk63EstCortesia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCortesia")));
                    acys.ACS_chk63EstCosto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63EstCosto")));
                    acys.ACS_txt63EstMonto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63EstMonto")));
                    acys.ACS_txt63ClienteDireccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteDireccion")));
                    acys.ACS_txt63ClienteColonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteColonia")));
                    acys.ACS_txt63ClienteMunicipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteMunicipio")));
                    acys.ACS_txt63ClienteEstado = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteEstado")));
                    acys.ACS_txt63ClienteCodPost = Convert.ToString(dr.GetValue(dr.GetOrdinal("ACS_txt63ClienteCodPost")));
                    acys.ACS_chk63DocFactFranquiciaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaEnt")));
                    acys.ACS_txt63DocFactFranquiciaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaEntCop")));
                    acys.ACS_chk63DocFactFranquiciaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactFranquiciaRec")));
                    acys.ACS_txt63DocFactFranquiciaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactFranquiciaRecCop")));
                    acys.ACS_chk63DocFactKeyEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyEnt")));
                    acys.ACS_txt63DocFactKeyEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyEntCop")));
                    acys.ACS_chk63DocFactKeyRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFactKeyRec")));
                    acys.ACS_txt63DocFactKeyRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFactKeyRecCop")));
                    acys.ACS_chk63DocOrdCompraEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraEnt")));
                    acys.ACS_txt63DocOrdCompraEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraEntCop")));
                    acys.ACS_chk63DocOrdCompraRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdCompraRec")));
                    acys.ACS_txt63DocOrdCompraRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdCompraRecCop")));
                    acys.ACS_chk63DocOrdReposEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposEnt")));
                    acys.ACS_txt63DocOrdReposEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposEntCop")));
                    acys.ACS_chk63DocOrdReposRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocOrdReposRec")));
                    acys.ACS_txt63DocOrdReposRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocOrdReposRecCop")));
                    acys.ACS_chk63DocCopPedidoEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoEnt")));
                    acys.ACS_txt63DocCopPedidoEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoEntCop")));
                    acys.ACS_chk63DocCopPedidoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocCopPedidoRec")));
                    acys.ACS_txt63DocCopPedidoRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocCopPedidoRecCop")));
                    acys.ACS_chk63DocRemisionEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionEnt")));
                    acys.ACS_txt63DocRemisionEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionEntCop")));
                    acys.ACS_chk63DocRemisionRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocRemisionRec")));
                    acys.ACS_txt63DocRemisionRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocRemisionRecCop")));
                    acys.ACS_chk63DocFolioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioEnt")));
                    acys.ACS_txt63DocFolioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioEntCop")));
                    acys.ACS_chk63DocFolioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocFolioRec")));
                    acys.ACS_txt63DocFolioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocFolioRecCop")));
                    acys.ACS_chk63DocContraRecEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecEnt")));
                    acys.ACS_txt63DocContraRecEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecEntCop")));
                    acys.ACS_chk63DocContraRecRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocContraRecRec")));
                    acys.ACS_txt63DocContraRecRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocContraRecRecCop")));
                    acys.ACS_chk63DocEntAlmacenEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenEnt")));
                    acys.ACS_txt63DocEntAlmacenEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenEntCop")));
                    acys.ACS_chk63DocEntAlmacenRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocEntAlmacenRec")));
                    acys.ACS_txt63DocEntAlmacenRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocEntAlmacenRecCop")));
                    acys.ACS_chk63DocSopServicioEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioEnt")));
                    acys.ACS_txt63DocSopServicioEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioEntCop")));
                    acys.ACS_chk63DocSopServicioRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocSopServicioRec")));
                    acys.ACS_txt63DocSopServicioRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocSopServicioRecCop")));
                    acys.ACS_chk63DocNomFirmaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaEnt")));
                    acys.ACS_txt63DocNomFirmaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaEntCop")));
                    acys.ACS_chk63DocNomFirmaoRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63DocNomFirmaoRec")));
                    acys.ACS_txt63DocNomFirmaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63DocNomFirmaRecCop")));
                    acys.ACS_chk63CitaEnt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaEnt")));
                    acys.ACS_txt63CitaEntCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaEntCop")));
                    acys.ACS_chk63CitaRec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_chk63CitaRec")));
                    acys.ACS_txt63CitaRecCop = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_txt63CitaRecCop")));


                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet(Acys acys, ref System.Data.DataTable dtAcuerdos, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion" 
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                        dtAcuerdos.Rows.Add(new object[] {
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))), 
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString(),
                        dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString(),
                        Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio"))), 
                        Convert.ToInt32( dr.GetValue( dr.GetOrdinal("Acs_Cantidad") )==DBNull.Value ? 0: dr.GetValue( dr.GetOrdinal("Acs_Cantidad"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Martes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Sabado"))), 
                        dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString(),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString()),                         
                        Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio"))), 
                        Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CanTotal"))),
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt"))),
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")))

                    });

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // 18 Jul 2018 RFH
        // Acys 2
        public List<eAcysDet2> ConsultarDet2(int Id_Emp, int Id_Cd,int Id_Acs,int Id_AcsVersion, string Conexion)
        {
            List<eAcysDet2> Lst = new List<eAcysDet2>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion" 
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Acs,
                                       Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eAcysDet2 obj = new eAcysDet2(); 
                    
                    obj.Id_Prd=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))); 
                    obj.Prd_Descripcion=dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    obj.Prd_Presentacion= dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    obj.Uni_Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();
                    obj.Acs_Precio=Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio"))); 
                    obj.Acs_Cantidad=Convert.ToInt32( dr.GetValue( dr.GetOrdinal("Acs_Cantidad") )==DBNull.Value ? 0: dr.GetValue( dr.GetOrdinal("Acs_Cantidad")));
 
                    obj.Acs_Frecuencia=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));
                    obj.Acs_Lunes=Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))); 
                    obj.Acs_Martes=Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Martes")));
                    obj.Acs_Miercoles=Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Miercoles")));
                    obj.Acs_Jueves =Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))); 
                    obj.Acs_Viernes=Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Viernes")));
                    obj.Acs_Sabado=Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Sabado")));
                    obj.Acs_Documento=dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();                        

                    obj.Acs_ConsigFechaFin=dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio")).ToString();
                    obj.Acs_ConsigFechaFin=dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin")).ToString(); 
                    obj.Acs_CantTotal=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_CanTotal")));
                    obj.Acs_UltSCpt=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt"))); 
                    obj.Acs_UltACpt=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt")));
                    obj.Id_TG=Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));

                    Lst.Add(obj);                    
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
                //throw ex;
            }
            return Lst;
        }

        private string ObtieneNombreModalidad(string p)
        {
            if (p.Trim() == "A")
            {
                return "Visita del representante";
            }
            else if (p.Trim() == "B")
            {
                return "Confirmación Tel.";
            }
            else if (p.Trim() == "C")
            {
                return "Confirmación/Con consignación";
            }
            else if (p.Trim() == "D")
            {
                return "Orden Abierta/Con reposición";
            }
            else
            {
                return "";
            }
        }

        private string Str(string p)
        {
            if (p == "F")
                return "Factura";
            else
                return "Remisión";
        }

        public void Imprimir(Acys acys, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Acs",                                        
                                        "@Acs_Estatus"
                                      };
                object[] Valores = { 
                                        acys.Id_Emp,
                                        acys.Id_Cd,
                                        acys.Id_Acs,                                        
                                        acys.Acs_Estatus
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Imprimir", ref verificador, Parametros, Valores);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void ConsultarReemplazos(Acys acys, int Id_Prd, ref DataTable dt, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion" ,
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_ConsultarDisponible", ref dr, Parametros, Valores);
                Comun c;
                while (dr.Read())
                {
                    dt.Rows.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd_Equivalente"))),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible"))),
                        0
                        );

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEquivalencia(int Id_Prd, int Id_Prd_Original, string Id_Acys, int Id_AcysVersion, int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acys",
                                          "@Id_AcsVersion", 
                                          "@Id_PrdOriginal",
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Acys,
                                       Id_AcysVersion,
                                       Id_Prd_Original,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Modificar", ref dr, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAsesorias(Acys acys, string Conexion, ref List<Asesoria> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion", 

                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Consultar", ref dr, Parametros, Valores);
                Asesoria a;
                while (dr.Read())
                {
                    a = new Asesoria();
                    a.Id_Ase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ase")));
                    a.Ase_Descripcion = dr.GetValue(dr.GetOrdinal("Ase_Descripcion")).ToString();
                    a.Ase_ServAsesoriaMensual = dr.GetBoolean(dr.GetOrdinal("Mensual"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioMensual")))
                    {
                        a.Ase_ServAsesoriaMensualfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioMensual")));
                    }
                    a.Ase_ServAsesoriaBimestral = dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.Ase_ServAsesoriaBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }
                    a.Ase_ServAsesoriaTrimestral = dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.Ase_ServAsesoriaTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstBi(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@Id_Acs",
                                          "@Id_AcsVersion" 
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Cte,
                                       acys.Id_Ter,
                                       acys.Id_Acs == 0 ? (int?)null:acys.Id_Acs,
                                       acys.Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEstBi_Consultar", ref dr, Parametros, Valores);

                Producto a;
                while (dr.Read())
                {
                    a = new Producto();
                    a.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    a.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Prd_AgrupadoSpo")))
                    {
                        a.Prd_AgrupadoSpo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo")));
                    }
                    else
                    {
                        a.Prd_AgrupadoSpo = 0;
                    }

                    a.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    a.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Revision")));
                    a.ServTecnicoRellenoMensual = dr.GetBoolean(dr.GetOrdinal("Mensual"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioMensual")))
                    {
                        a.ServTecnicoRellenoMensualfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioMensual")));
                    }

                    a.ServTecnicoRellenoBimestral = dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.ServTecnicoRellenoBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }

                    a.ServTecnicoRellenoTrimestral = dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.ServTecnicoRellenoTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEnvio(ref Acys Acs, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_AcsVersion"
                                          
                                      };

                object[] Valores = { 
                                       Acs.Id_Emp, 
                                       Acs.Id_Cd, 
                                       Acs.Id_Acs,
                                       Acs.Id_AcsVersion
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcys_Envio", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Acs.Acs_Unique = dr.GetValue(dr.GetOrdinal("Acs_Unique")).ToString();
                    Acs.Acs_Solicitar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Solicitar")));
                    if (dr.GetValue(dr.GetOrdinal("Acs_Sustituye")) != DBNull.Value)
                    {
                        Acs.Acs_Sustituye = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sustituye")));
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaUltimaVersion(ref Acys Acs, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                          
                                          
                                      };

                object[] Valores = { 
                                       Acs.Id_Emp, 
                                       Acs.Id_Cd, 
                                       Acs.Id_Acs                                     
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysUltimaVersion_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Acs.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Acs.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Acs.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    Acs.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CapAcy ConsultarUltimaPorClienteYFecha(int idEmp, int idCd, int idCte, int idTer, DateTime fecha, string connexionEf)
        {
            CapAcy ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(connexionEf))
            {
                var res = ctx.spCapAcys_ConsultaCte(idEmp, idCd, idCte, idTer, fecha).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        public void ConsultaEstBiMantenimiento(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@Id_Acs",
                                          "@Id_AcsVersion", 
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Cte,
                                       acys.Id_Ter,
                                       acys.Id_Acs == 0 ? (int?)null:acys.Id_Acs,
                                       acys.Id_AcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEstBiMantenimiento_Consultar", ref dr, Parametros, Valores);

                Producto a;
                while (dr.Read())
                {
                    a = new Producto();
                    a.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    a.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Prd_AgrupadoSpo")))
                    {
                        a.Prd_AgrupadoSpo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo")));
                    }
                    else
                    {
                        a.Prd_AgrupadoSpo = 0;
                    }


                    a.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    a.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Revision")));
                    a.ServMantenimientoMensual = dr.GetBoolean(dr.GetOrdinal("Mensual"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioMensual")))
                    {
                        a.ServMantenimientoMensualfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioMensual")));
                    }

                    a.ServMantenimientoBimestral = dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.ServMantenimientoBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }

                    a.ServMantenimientoTrimestral = dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.ServMantenimientoTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteVentas_Consulta(RepVentasParams pParams, string pConexion, List<RepVentas> lRepVentas)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(pConexion);
                Funciones cdFunciones = new Funciones();

                string[] Parametros = { 
                                        "@Id_Emp",
		                                "@Id_Cd",
		                                "@Anio",
		                                "@Cliente",
		                                "@Tipo",
		                                "@NivelCliente",
		                                "@NivelProducto",
		                                "@Reporte",
		                                "@Id_U"
                                      };

                object[] Valores = { 
                                        pParams.Id_Emp,
                                        pParams.Id_Cd,
                                        pParams.Anio,
                                        pParams.Cliente,
                                        pParams.Tipo,
                                        pParams.NivelCliente,
                                        pParams.NivelProducto,
                                        pParams.Reporte,
                                        pParams.Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasAnuales", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    RepVentas rVentas = new RepVentas();
                    rVentas = cdFunciones.GetEntity<RepVentas>(dr);
                    lRepVentas.Add(rVentas);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AcysXCliente_Consulta(Acys pAcys, string pConexion, ref List<Acys> pLista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(pConexion);
                Funciones cdFunciones = new Funciones();

                string[] Parametros = { 
                                        "@Id_Emp",
		                                "@Id_Cd",
		                                "@Id_Cte",
                                        "@Id_Rik"
                                      };

                object[] Valores = { 
                                        pAcys.Id_Emp,
                                        pAcys.Id_Cd,
                                        pAcys.Id_Cte,
                                        pAcys.Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysXCliente_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Acys rAcys = new Acys();
                    rAcys = cdFunciones.GetEntity<Acys>(dr);
                    pLista.Add(rAcys);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta_Log(string Id_Acs, string Conexion, string Pantalla, ref List<Logs> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Acs",                                       
                                          "@Pantalla"    
                                      };
                object[] Valores = { 
                                       Id_Acs,
                                       Pantalla
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("splogs_Consulta", ref dr, Parametros, Valores);

                Logs c;
                while (dr.Read())
                {
                    c = new Logs();

                    c.Pantalla = dr["Pantalla"].ToString();
                    c.Id_Acs = Convert.ToInt32(dr["Id_Acs"]);
                    c.Campo = dr["Campo"].ToString();
                    c.Valor_Anterior = dr["Valor_Anterior"].ToString();
                    c.Valor_Actualizado = dr["Valor_Actualizado"].ToString();
                    c.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    c.Usuario = dr["Usuario"].ToString();
                    c.Descripcion = dr["Descripcion"].ToString();
                    c.Codigo = dr["Codigo"].ToString();

                    List.Add(c);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AcysDatosGarantia> DatosGarantia_Consulta(string pConexion, AcysDatosGarantia datosGarantia)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader drFechas = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(pConexion);
                Funciones cdFunciones = new Funciones();
                string[] Parametros = { 
                                        "@Id_Emp",
		                                "@Id_Cd",
		                                "@Id_Acs",
                                        "@Id_AcsVersion",
                                        "@Id_TG"
                                      };

                object[] Valores = { 
                                        datosGarantia.Id_Emp,
                                        datosGarantia.Id_Cd,
                                        datosGarantia.Id_Acs,
                                        datosGarantia.Id_AcsVersion,
                                        datosGarantia.Id_TG
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelCapAcysDatosGarantia", ref dr, Parametros, Valores);

                List<AcysDatosGarantia> listaDatosGar = new List<AcysDatosGarantia>();
                while (dr.Read())
                {
                    AcysDatosGarantia datosGar = new AcysDatosGarantia();
                    datosGar = cdFunciones.GetEntity<AcysDatosGarantia>(dr);
                    listaDatosGar.Add(datosGar);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


                foreach (AcysDatosGarantia a in listaDatosGar)
                {

                    CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(pConexion);

                    string[] ParametrosFechas = { 
                                        "@Id_Emp",
		                                "@Id_Cd",
		                                "@Id_Acs",
                                        "@Id_AcsVersion",
                                        "@Id_TG"
                                      };

                    object[] ValoresFechas = { 
                                        a.Id_Emp,
                                        a.Id_Cd,
                                        a.Id_Acs,
                                        a.Id_AcsVersion,
                                        a.Id_TG
                                   };

                    SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spSelCapAcysDatosGarantia_Fechas", ref drFechas, ParametrosFechas, ValoresFechas);

                    Dictionary<int, DateTime> FechasCorteDict = new Dictionary<int, DateTime>();
                    while (drFechas.Read())
                    {
                        int mes = Convert.ToInt32(drFechas["Mes"]);
                        DateTime fecha = Convert.ToDateTime(drFechas["FechaCorte"]);

                        FechasCorteDict.Add(mes, fecha);
                    }
                    if (FechasCorteDict.Count > 0) a.Fechas_Corte = FechasCorteDict;


                    CapaDatos.LimpiarSqlcommand(ref sqlcmd2);

                }




                return listaDatosGar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AcysDatosGarantia> DatosGarantia_Consulta_Remision(string pConexion, int remision, int idEmp, int idCd, int idCte, int idTer)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(pConexion);
                Funciones cdFunciones = new Funciones();
                string[] Parametros = { 
                                        "@Id_Rem",
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Ter",
                                      };

                object[] Valores = { 
                                        remision,
                                        idEmp,
                                        idCd,
                                        idCte,
                                        idTer
                                   };

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
        }

        /// <summary>
        /// Devuelve el resultado de consultar la entidad [CapAcys], dado el identificador del acys idAcys.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa asociado al centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución en el que se encuentra asociado el acys idAcys</param>
        /// <param name="idAcys">Identificador del acys de interés</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CapAcy. Instancia de la entidad [CapAcys] en caso de encontrar la coincidencia; null en caso contrario</returns>
        public CapAcy ConsultarPorId(int idEmp, int idCd, int idAcys, string cadenaConexionEF)
        {
            CapAcy resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var acys = (from a in ctx.CapAcys
                            where a.Id_Emp == idEmp && a.Id_Cd == idCd && a.Id_Acs == idAcys
                            select a).ToList();
                if (acys.Count > 0)
                {
                    resultado = acys[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza el valor del atributo [Id_Val] de la entidad [CapAcys] con el valor idVal, dado el identificador del acys idAcys.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa asociada al centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución asociado al acys idAcys</param>
        /// <param name="idAcys">Identificador del acys de interés</param>
        /// <param name="idVal">Identificador de la valuación a asociar al acys</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        public void ActualizarAtributoIdVal(int idEmp, int idCd, int idAcys, int idVal, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var acys = (from a in ctx.CapAcys
                            where a.Id_Emp == idEmp && a.Id_Cd == idCd && a.Id_Acs == idAcys
                            select a).ToList();
                if (acys.Count > 0)
                {
                    var maxVersionId = acys.Max(a => a.Acs_version);
                    var acysActual = (from a in acys
                                      where a.Acs_version == maxVersionId
                                      select a).ToList();
                    acysActual[0].Id_Val = idVal;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Actualiza el valor del atributo [Id_Val] de la entidad [CapAcys] con el valor idVal, dado el identificador del acys idAcys.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa asociada al centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución asociado al acys idAcys</param>
        /// <param name="idAcys">Identificador del acys de interés</param>
        /// <param name="idVal">Identificador de la valuación a asociar al acys</param>
        /// <param name="idcCtx">Contexto de conexión a la fuente de datos</param>
        public void ActualizarAtributoIdVal(int idEmp, int idCd, int idAcys, int idVal, ICD_Contexto idcCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)idcCtx).Contexto;
            var acys = (from a in ctx.CapAcys
                        where a.Id_Emp == idEmp && a.Id_Cd == idCd && a.Id_Acs == idAcys
                        select a).ToList();
            if (acys.Count > 0)
            {
                var maxVersionId = acys.Max(a => a.Acs_version);
                var acysActual = (from a in acys
                                  where a.Acs_version == maxVersionId
                                  select a).ToList();
                acysActual[0].Id_Val = idVal;
            }
        }

        /// <summary>
        /// Regresa el resultado de la consulta al repositorio CapAcys, condicionado por cliente y territorio. Se asume que se trabaja con la última versión del ACYS.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapAcys]</returns>
        public IEnumerable<CapAcy> ConsultarPorClienteYTerritorio(int idEmp, int idCd, int idCte, int idTerritorio, ICD_Contexto icdCtx)
        {
            try
            {

                sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
                var acys = from a in ctx.CapAcys
                           group a by a.Id_AcsVersion into g
                           select g.Where(ac => ac.Id_Emp == idEmp && ac.Id_Cd == idCd && ac.Id_Cte == idCte && ac.Id_Ter == idTerritorio && ac.Id_AcsVersion == g.Max(el => el.Id_AcsVersion));
                if (acys.Count() > 0)
                {
                    return acys.SelectMany(col => col);
                }
                return new List<CapAcy>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
