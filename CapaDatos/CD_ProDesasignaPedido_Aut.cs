using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ProDesasignaPedido_Aut
    {
        public void DesasignaPedido_Aut(int Id_Emp, int Id_Cd, int Credito, ref int verificador, string Conexion)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;



                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Credito"
                                      };
                Valores = new object[]{ 
                                        Id_Emp,
                                        Id_Cd,
                                        Credito 
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void AsignacionPedido_Aut(int Id_Emp, int Id_Cd, DateTime Fecha, int Id_U, int Credito, ref int verificador, string Conexion)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;



                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Credito",
                                        "@FecAsig",
                                        "@UsrAsig"
                                      };
                Valores = new object[]{ 
                                        Id_Emp,
                                        Id_Cd,
                                        Credito == 2 ? (object)null: Credito ,
                                        Fecha,
                                        Id_U
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AsignacionPedido_CteTerr(CapaEntidad.Pedido ped, int Credito, ref int verificador, string Conexion)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;

                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Credito",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Id_Cte",
                                        "@Id_Ter"
                                      };

                Valores = new object[]{ 
                                        ped.Id_Emp,
                                        ped.Id_Cd,
                                        Credito == 2 ? (object)null:Credito,
                                        ped.FechaAsignacion,
                                        ped.Id_U,
                                        ped.Id_Cte,
                                        ped.Id_Ter
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void AsignacionPedido_Aut(int Id_Emp, int Id_Cd, DateTime Fecha, int Id_U, string Id_Ped, ref int verificador, string Conexion)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;



                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Id_Ped"
                                      };
                Valores = new object[]{ 
                                        Id_Emp,
                                        Id_Cd,
                                      
                                        Fecha,
                                        Id_U,
                                        Id_Ped
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DesasignacionPedido_Aut(int Id_Emp, int Id_Cd, string Id_Ped, ref int verificador, string Conexion)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;



                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ped" 
                                      };
                Valores = new object[]{ 
                                        Id_Emp,
                                        Id_Cd,
                                        Id_Ped 
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProDesAsignPrdxPed_Automatica", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
