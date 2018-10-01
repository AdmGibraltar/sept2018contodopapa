using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatSemana
    {
        public void ConsultaSemana(ref Semana semana, int Cal_Año, int Cal_Mes, Sesion sesion, ref List<Semana> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id_Emp"
                                          ,"@Cal_Año"  
                                          ,"@Cal_Mes"
                                          ,"@Id_Cd"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp.ToString()
                                       ,Cal_Año.ToString()
                                       ,Cal_Mes.ToString()
                                       ,sesion.Id_Cd_Ver
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSemana_Consulta2", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    semana = new Semana();
                    semana.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    semana.Id_Sem = dr.GetInt32(dr.GetOrdinal("Id_Sem"));
                    semana.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    semana.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    semana.Sem_FechaIni = dr.GetDateTime(dr.GetOrdinal("Sem_FechaIni"));
                    semana.Sem_FechaFin = dr.GetDateTime(dr.GetOrdinal("Sem_FechaFin"));
                    semana.Sem_Activo = dr.GetBoolean(dr.GetOrdinal("Sem_Activo"));

                    list.Add(semana);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSemanaRep(ref Semana semana, int Cal_Año, int Cal_Mes, Sesion sesion, ref List<Semana> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Cal_Año"  
                                          ,"@Cal_Mes"                                          
                                      };

                object[] Valores = {
                                       sesion.Id_Emp.ToString()
                                       ,sesion.Id_Cd_Ver
                                       ,Cal_Año.ToString()
                                       ,Cal_Mes.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spListadoSemanasMes", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    semana = new Semana();
                    //  semana.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    semana.Id_Sem = dr.GetInt32(dr.GetOrdinal("Id"));
                    semana.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    semana.Cal_Año = Cal_Año;   ///  dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    //  semana.Sem_FechaIni = dr.GetDateTime(dr.GetOrdinal("Sem_FechaIni"));
                    //  semana.Sem_FechaFin = dr.GetDateTime(dr.GetOrdinal("Sem_FechaFin"));
                    //  semana.Sem_Activo = dr.GetBoolean(dr.GetOrdinal("Sem_Activo"));

                    semana.Rango = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();

                    list.Add(semana);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSemana(ref List<Semana> semanas, string Conexion, ref int verificador, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp",
	                "@Id_Sem",	
                    "@Id_Cal",
                    "@Cal_Año",
	                "@Sem_FechaIni",
	                "@Sem_FechaFin",	                
	                "@Sem_Activo"
		        };

                SqlCommand sqlcmd = new SqlCommand();

                foreach (Semana semana in semanas)
                {
                    object[] Valores = {
                                    semana.Id_Emp,
                                    semana.Id_Sem,   
                                    semana.Id_Cal,
                                    semana.Cal_Año,
                                    semana.Sem_FechaIni,
                                    semana.Sem_FechaFin,                    
                                    semana.Sem_Activo
		                            };
                    sqlcmd = CapaDatos.GenerarSqlCommand(actualizar ? "spCatSemana_Modificar" : "spCatSemana_Insertar", ref verificador, Parametros, Valores);
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

        public void EliminarSemana(int Id_Cal, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { "@Id_Cal" };
                object[] Valores = { Id_Cal };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spCatSemana_Eliminar", verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        //Reporte cumplimiento de venta instalada
        public void ConsultaSemana(ref Semana semana, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd"
                                      };

                object[] Valores = {
                                       semana.Id_Emp,
                                       semana.Id_Cd//toma la fecha del periodo actual
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSemana_ConsultaPeriodoMensual", ref dr, parametros, Valores);//spCatSemanaRango_Consulta

                if (dr.HasRows)
                {
                    dr.Read();
                    semana.Periodo = dr.GetValue(dr.GetOrdinal("Periodo")).ToString();
                    verificador = 1;
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaSemanaMaxCalendario(DateTime InicioDocumento, ref DateTime FechaFinal, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] parametros = { "@InicioDoc" };

                object[] Valores = { InicioDocumento };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelMaxSemanaCalendario", ref dr, parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    FechaFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaFinal")));

                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioKey(int Anio, int Id_cd, string Conexion, List<Semana> list)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] parametros = { "@Anio", "Id_Cd" };
                object[] Valores = { Anio, Id_cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelCalendarioSemanasKey", ref dr, parametros, Valores);



                while (dr.Read())
                {
                    var semana = new Semana();

                    semana.Id_Sem = Convert.ToInt32(dr["Id_Sem"]);

                    semana.Cal_Año = Convert.ToInt32(dr["Cal_Año"]);
                    semana.Sem_FechaIni = Convert.ToDateTime(dr["Sem_FechaIni"]);
                    semana.Sem_FechaFin = Convert.ToDateTime(dr["Sem_FechaFin"]);
                    semana.Sem_Activo = Convert.ToBoolean(dr["Sem_Activo"]);
                    semana.Mes = Convert.ToInt32(dr["Mes"]);
                    semana.Id_SemxMes = Convert.ToInt32(dr["Id_SemxMes"]);

                    list.Add(semana);
                }
                dr.Close();


                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Captacion de pedidos de venta instalada
        public void ConsultaSemanaActual(ref Semana semana, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Fecha"
                                      };

                object[] Valores = {
                                       semana.Id_Emp,
                                       semana.Id_Cd,
                                       semana.Sem_FechaAct//.ToString("yyyy-MM-dd")
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSemanaRango_Consulta", ref dr, parametros, Valores);//

                if (dr.HasRows)
                {
                    dr.Read();
                    semana.Sem_FechaIni = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Sem_FechaIni")).ToString()) ? Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Sem_FechaIni"))) : DateTime.MinValue;
                    semana.Sem_FechaFin = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Sem_FechaFin")).ToString()) ? Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Sem_FechaFin"))) : DateTime.MinValue;
                    semana.Id_Sem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sem")));
                    verificador = 1;
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
