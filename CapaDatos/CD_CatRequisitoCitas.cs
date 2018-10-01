using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_CatRequisitoCitas
    {
        public void ConsultaRequisitosCita(RequisitoCita requisitoCita, string Conexion, ref List<RequisitoCita> List)
        {
            try
            {
                
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPreRequisitos_Todos", ref dr);
                while (dr.Read())
                {
                    /*
                    Id_CriterioCita,
		            IdPreRequisito
                    PreRequisito

                     * */
                    requisitoCita = new RequisitoCita();
                    //  requisitoCita.Id_CriterioCita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CriterioCita")));
                    requisitoCita.IdPreRequisito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdPreRequisito")));
                    requisitoCita.PreRequisito = dr.GetValue(dr.GetOrdinal("PreRequisito")).ToString();

                    List.Add(requisitoCita);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRequisitosCita(List<RequisitoCita> Listado, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                //  string Ssps;
                object[] Valores;
                SqlCommand sqlcmd;
                RequisitoCita Rrequi;
                Rrequi = Listado[0];
                string[] ParametroX = { 
                                        "@Id_CitaVisita", 
                                      };
                Valores = new object[] {
                    Rrequi.Id_CriterioCita,
                    };

                //  Ssps = "spRequisitosCitas_Limpia" + Rrequi.Id_CriterioCita.ToString();
                sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Limpia", ref verificador, ParametroX, Valores);
                    
                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                        "@IdPreRequisito",
                                        "@Secuencia",
                                      };
                int cont = 0;
                foreach (RequisitoCita Requi in Listado)
                {
                    cont++;
                    Valores = new object[] { 
                        Requi.Id_CriterioCita,
                        Requi.IdPreRequisito,
                        cont
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Alta", ref verificador, Parametros, Valores);
                }

                CapaDatos.CommitTrans();
                //  CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VerCalendario(string Conexion, int Emp, int Cd, int Usuario, ref int refer)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Usuario", 
                                      };
                object[] Valores = new object[] { 
                                        Emp,
                                        Cd,
                                        Usuario
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVerCalendario", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    refer = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tu")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoPrerequisitosCita_Todos(string Conexion, int Cita, ref List<RequisitoCita> Listado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                      };

                object[] Valores = new object[] { 
                        Convert.ToInt32(Cita)
                    };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosUnaCita_Consulta", ref dr, Parametros, Valores);

                RequisitoCita Comun = default(RequisitoCita);
                while (dr.Read())
                {
                    Comun = new RequisitoCita();
                    Comun.IdPreRequisito = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.PreRequisito = dr.GetString(dr.GetOrdinal("PreRequisito"));

                    Listado.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoRequisitos_Cita(string Conexion, string CitaVisita, ref List<RequisitoCita> list)
        {
            try
            { 
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                string[] Parametros = { 
                                        "@Id_CitaVisita", 
                                      };

                object[] Valores = new object[] { 
                        Convert.ToInt32(CitaVisita)
                    };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRequisitosCitas_Consulta", ref dr, Parametros, Valores);

                RequisitoCita Comun = default(RequisitoCita);
                while (dr.Read())
                {
                    Comun = new RequisitoCita();
                    Comun.IdPreRequisito = dr.GetInt32(dr.GetOrdinal("IdPreRequisito"));
                    Comun.PreRequisito = dr.GetString(dr.GetOrdinal("PreRequisito"));

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

    }
}
