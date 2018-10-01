using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CatCalendarioControl
    {

        public void GuardarCalendario(ref List<CalendarioControl> Cal, ref int verificador, string Conexion, string estatus)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {


                if (Cal.Count > 0)
                {
                    string[] Parametros = {
			        "@Id_Emp",
	                "@Id_Cd",
                    "@Id_Acs",
                    "@Id_AcsVersion", 
	                "@Cal_Año"
		        };


                    SqlCommand sqlcmd = new SqlCommand();
                    object[] Valores = {
                                        Cal[0].Id_Emp,
                                        Cal[0].Id_Cd,                                        
                                        Cal[0].Id_Acs,
                                        Cal[0].Id_AcsVersion,
                                        Cal[0].Cal_Año,
		                            };


                    sqlcmd = CapaDatos.GenerarSqlCommand("spDelCatCalendarioControl", ref verificador, Parametros, Valores);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                    if (estatus == "A")
                    {
                        //
                        CapaDatos = new CapaDatos.CD_Datos(Conexion);
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysCalenda_DeleteCalendario", ref verificador, Parametros, Valores);
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    }

                    foreach (CalendarioControl calendario in Cal)
                    {
                        CapaDatos = new CapaDatos.CD_Datos(Conexion);
                        string[] Parametros2 = {
			        "@Id_Emp",
	                "@Id_Cd",
                    "@Id_Acs",
                    "@Id_AcsVersion", 
	                "@Cal_Año",
                      "@Semana",
                     "@IdProd",
                                                  "@Id_TG"};

                        object[] Val = {
                                        calendario.Id_Emp,
                                        calendario.Id_Cd,                                        
                                        calendario.Id_Acs,
                                        calendario.Id_AcsVersion,
                                        calendario.Cal_Año,
                                        calendario.Semana,
                                        calendario.IdProd,
                                        calendario.Id_TG
		                            };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spInsCatCalendarioControl", ref verificador, Parametros2, Val);
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Mismo propósito de [GuardarCalendario] (cualquiera que este sea) con la característica adicional de aceptar una transacción
        /// </summary>
        /// <param name="Cal">?</param>
        /// <param name="verificador">?</param>
        /// <param name="Conexion">?</param>
        /// <param name="dbTransaction">Transacción</param>
        public void GuardarCalendario(ref List<CalendarioControl> Cal, ref int verificador, string Conexion, IDbTransaction dbTransaction)
        {
            try
            {


                if (Cal.Count > 0)
                {
                    string[] Parametros = {
			        "@Id_Emp",
	                "@Id_Cd",
                    "@Id_Acs",
                    "@Id_AcsVersion", 
	                "@Cal_Año"
		        };


                    SqlCommand sqlcmd = new SqlCommand();
                    object[] Valores = {
                                        Cal[0].Id_Emp,
                                        Cal[0].Id_Cd,                                        
                                        Cal[0].Id_Acs,
                                        Cal[0].Id_AcsVersion,
                                        Cal[0].Cal_Año,
		                            };


                    sqlcmd = dbTransaction.Connection.CreateCommand() as SqlCommand;
                    sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)dbTransaction;
                    sqlcmd = CD_Datos.GenerarSqlCommand("spDelCatCalendarioControl", ref verificador, Parametros, Valores, dbTransaction.Connection, sqlcmd);
                    sqlcmd.Dispose();



                    foreach (CalendarioControl calendario in Cal)
                    {
                        string[] Parametros2 = {
			                                    "@Id_Emp",
	                                            "@Id_Cd",
                                                "@Id_Acs",
                                                "@Id_AcsVersion", 
	                                            "@Cal_Año",
                                                  "@Semana",
                                                 "@IdProd",
                                                  "@Id_TG"                         
                                                };

                        object[] Val = {
                                        calendario.Id_Emp,
                                        calendario.Id_Cd,                                        
                                        calendario.Id_Acs,
                                        calendario.Id_AcsVersion,
                                        calendario.Cal_Año,
                                        calendario.Semana,
                                        calendario.IdProd,
                                        calendario.Id_TG
		                            };

                        sqlcmd = dbTransaction.Connection.CreateCommand() as SqlCommand;
                        sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)dbTransaction;
                        sqlcmd = CD_Datos.GenerarSqlCommand("spInsCatCalendarioControl", ref verificador, Parametros2, Val, dbTransaction.Connection, sqlcmd);
                        sqlcmd.Dispose();
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultaCalendario(ref CalendarioControl Cal, string conexion, ref List<CalendarioControl> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);


                string[] parametros = { 
			                            "@Id_Emp",
	                                    "@Id_Cd",
                                        "@Id_Acs",
                                        "@Id_AcsVersion", 
	                                    "@Cal_Año"
                                      };

                object[] Valores = {
                                       Cal.Id_Emp,
                                       Cal.Id_Cd,
                                       Cal.Id_Acs,
                                       Cal.Id_AcsVersion,
                                       Cal.Cal_Año
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelCatCalendarioControl", ref dr, parametros, Valores);

                //Calendario _calendario = default(Calendario);
                while (dr.Read())
                {
                    var Calen = new CalendarioControl();
                    Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    Calen.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    Calen.Id_Acs = dr.GetInt32(dr.GetOrdinal("Id_Acs"));
                    Calen.Id_AcsVersion = dr.GetInt32(dr.GetOrdinal("Id_AcsVersion"));

                    Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    Calen.Semana = dr.GetInt32(dr.GetOrdinal("Semana"));
                    Calen.IdProd = dr.GetInt32(dr.GetOrdinal("IdProd"));
                    Calen.Id_TG = dr.GetInt32(dr.GetOrdinal("Id_TG"));
                    list.Add(Calen);
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