using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCriterioCitas
    {
        public void ConsultaCriteriosCita(CriterioCita criterioCita, string Conexion, ref List<CriterioCita> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Todos", ref dr);


                while (dr.Read())
                {
                    /*
        Id_CriterioCita,
		Id_Cliente,
        Cliente
		Id_Frecuencia,
		Frecuencia,
		Id_TipoVisita,
		TipoVisita,
		Id_RSC,
		U_Nombre */
                    criterioCita = new CriterioCita();
                    criterioCita.Id_CriterioCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CriterioCita")));
                    criterioCita.Id_Cliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cliente")));
                    criterioCita.Cliente= dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    criterioCita.Id_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Frecuencia")));
                    criterioCita.Frecuencia= dr.GetValue(dr.GetOrdinal("Frecuencia")).ToString();
                    criterioCita.Id_TipoVisita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TipoVisita")));
                    criterioCita.TipoVisita = dr.GetValue(dr.GetOrdinal("TipoVisita")).ToString();
                    criterioCita.Id_RSC = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RSC")));
                    criterioCita.RSC = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    criterioCita.FechaInicial = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicial")).ToString());

                    List.Add(criterioCita);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cliente", 
                                        "@Id_Frecuencia", 
                                        "@Id_TipoVisita", 
                                        "@Id_RSC",
                                        "@TienePreRequi",
                                      };
                object[] Valores = { 
                                        Cita.Id_Cliente
                                        ,Cita.Id_Frecuencia
                                        ,Cita.Id_TipoVisita
                                        ,Cita.Id_RSC
                                        ,Cita.TienePreRequi
                                   };
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Alta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verificador = dr.GetInt32(dr.GetOrdinal("Id_CriterioCita"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraPaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Pagina",
                                      };
                object[] Valores = { 
                                        1
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtienePaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdSecuencia"))) == 0)
                    {
                        PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("txt"))) + "<div >"  + Environment.NewLine;
                    }
                    else
                    {
                        PagHTML = PagHTML + Convert.ToString(dr.GetValue(dr.GetOrdinal("txt"))) + Environment.NewLine;
                    }
                }

                PagHTML = PagHTML + "</div>";

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignaPaginaAyudaPorId(int IdOpcion, string Conexion, string PagHTML, ref int Ret) 
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@idPagina",
                                        "@pagHTML"
                                      };
                object[] Valores = { 
                                        IdOpcion,
                                        PagHTML
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAsignaPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Ret = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grabo")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ObtienePaginaAyudaPorId(string IdPagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@idPagina",
                                      };
                object[] Valores = { 
                                        IdPagASPX
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPaginaAyudaId", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("hlp")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtienePaginaAyuda(string PagASPX, string Conexion, ref string PagHTML)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@aspx",
                                      };
                object[] Valores = { 
                                        PagASPX
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    PagHTML = Convert.ToString(dr.GetValue(dr.GetOrdinal("hlp")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneModulo(string PagHTML, string Conexion, ref string Modulo) 
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@html",
                                      };
                object[] Valores = { 
                                        PagHTML
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spModuloPaginaAyuda", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Modulo = Convert.ToString(dr.GetValue(dr.GetOrdinal("modulo")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EsDiaFestivo(string Conexion, string Fecha, ref int resul)//spEsDiaFestivo 
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Fecha",
                                      };
                object[] Valores = { 
                                        Fecha
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEsDiaFestivo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    resul = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsDia")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListadoPrerequisitosCita_Todos(string Conexion, string sp, string cita, ref System.Collections.Generic.List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        cita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    list.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListadoPrerequisitos_Todos(string Conexion, string sp, ref System.Collections.Generic.List<Comun> list)
        {
            try
            { 
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    list.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Cliente", 
                                        "@Id_Frecuencia", 
                                        "@Id_TipoVisita", 
                                        "@Id_RSC",
                                        "@TienePreRequi",
                                      };
                object[] Valores = { 
                                        Cita.Id_Cliente
                                        ,Cita.Id_Frecuencia
                                        ,Cita.Id_TipoVisita
                                        ,Cita.Id_RSC
                                        ,Cita.TienePreRequi
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_Alta", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarFechaCriteriosCita(CriterioCita Cita, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        //  "@Id_CriterioCita", 
                                        "@Id_Cliente", 
                                        "@FechaInicial", 
                                        "@Duracion", 
                                        "@UserID",
                                      };
                object[] Valores = { 
                                        //  Cita.Id_CriterioCita
                                        Cita.Id_Cliente
                                        ,Cita.FechaInicial
                                        ,23
                                        ,Cita.Usuario
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCriteriosCitas_AsignarFecha", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneTipoVisitaCte(int Id_Emp, int Id_Cd, int cliente, string Conexion, ref string visita)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@IdCliente",
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cd
                                        ,cliente
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spObtieneTipoVisitaCliente", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    visita = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_TipoVisita")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosEmpresaCita(int Id_CitaVisita, ref string Empr, ref string Conta, ref string Fromz, ref string Toz, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        Id_CitaVisita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDatosCita_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Empr = dr.GetString(dr.GetOrdinal("Cliente"));
                    Conta = dr.GetString(dr.GetOrdinal("Cte_Contacto")); 
                    Fromz = dr.GetString(dr.GetOrdinal("U_Correo"));
                    Toz = dr.GetString(dr.GetOrdinal("Cte_Email"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosCriterioCita(int Id_CitaVisita, ref string txtRSC, string Conexion)
        {
            try
            { 
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_CitaVisita",
                                      };
                object[] Valores = { 
                                        Id_CitaVisita
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spDatosCita_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    txtRSC = dr.GetString(dr.GetOrdinal("U_Nombre"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaMotivoModificacion(int Id_CitaVisita, string Motivo, string Conexion)
        {
            try
            {
                int verificador=0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_CitaVisita", 
                                        "@Motivo",
                                      };
                object[] Valores = {    Id_CitaVisita
                                        ,Motivo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCambioVisita_AgregarALog", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaModificacion(int Id_CitaVisita, string Conexion)
        {
            try
            {
                int verificador = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_CitaVisita",
                                      };
                object[] Valores = {    Id_CitaVisita
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCambioVisita_CancelaModificacion", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
