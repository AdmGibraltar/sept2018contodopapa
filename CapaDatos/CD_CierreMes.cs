using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CierreMes
    {
        public void Cierre(int Id_Emp, int Id_Cd, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();

                SqlCommand sqlcmd;

                //string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_prd", "@inventario", "@ordenado", "@asignado", "@consultas" };
                //object[] Valores = { Id_Emp, Id_Cd, null, 1, 1, 1, 0 };

                //sqlcmd = CapaDatos.GenerarSqlCommand("ReconstruirInventario", ref verificador, Parametros, Valores);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd" };
                object[] Valores = new object[] { Id_Emp, Id_Cd };

                //Evalua si hay siguiente periodo, inicializa el inventario, cancela saldos menores, inicializa pronostico de cierre
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMensual", ref verificador, Parametros, Valores);

                //historial de acys
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_AcysHistorico", ref verificador, Parametros, Valores);

                //borra los historiales (anteriores a 1 año)
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_Historicos", ref verificador, Parametros, Valores);

                //extiende 2 meses mas los acys que vencen dentro de 2 meses
                //sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_Acys", ref verificador, Parametros, Valores);

                //venta neta importe unidades
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_VentaNetaUniImp", ref verificador, Parametros, Valores);

                //venta neta
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_VentaNetaCosto", ref verificador, Parametros, Valores);

                //EstBi
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_UniBiFacturada", ref verificador, Parametros, Valores);

                //spProCierreMes_EstRemision
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_EstRemision", ref verificador, Parametros, Valores);


                sqlcmd = CapaDatos.GenerarSqlCommand("spInsertaCOB12", ref verificador, Parametros, Valores);


                // sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_Inversion", ref verificador, Parametros, Valores);

                //actualiza la tabla de amortizacion
                //sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_Amortizacion", ref verificador, Parametros, Valores);

                //actualiza la tabla de amortizacion anticipada
                //sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_AmortAnticipada", ref verificador, Parametros, Valores);

                //Cambia el periodo
                sqlcmd = CapaDatos.GenerarSqlCommand("spProCierreMes_CambiaPeriodo", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void CierreGrid(Sesion sesion, ref  List<PronCierre> listProCierre)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Rik"
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp, 
                                       sesion.Id_Cd,
                                       sesion.Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPronosticoCierre_Consulta", ref dr, Parametros, Valores);

                PronCierre proCierre;
                while (dr.Read())
                {
                    proCierre = new PronCierre();
                    proCierre.Id_ProCierre = (int)dr.GetValue(dr.GetOrdinal("Id_ProCierre"));
                    proCierre.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    proCierre.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    proCierre.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    proCierre.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    proCierre.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    proCierre.Autorizado = dr.GetValue(dr.GetOrdinal("Estatus")).ToString().ToUpper() == "A" ? true : false;
                    proCierre.Pron_Actual = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Pron_Actual")).ToString()) ? (double)dr.GetValue(dr.GetOrdinal("Pron_Actual")) : 0.00;
                    proCierre.Pron_Anterior = (double)dr.GetValue(dr.GetOrdinal("Pron_Anterior"));
                    listProCierre.Add(proCierre);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarPronosticoCierre(PronCierre pronCierre, Sesion sesion, ref int validador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rik",
                                          "@Id_ProCierre", 
                                          "@Id_Ter", 
                                          "@Id_Cte", 
                                          "@Pron_Anterior", 
                                          "@Pron_Actual"
                                      };
                object[] Valores = { 
                                       pronCierre.Id_Emp,
                                       pronCierre.Id_Cd,
                                       pronCierre.Id_Rik,
                                       pronCierre.Id_ProCierre,
                                       pronCierre.Id_Ter,
                                       pronCierre.Id_Cte,
                                       pronCierre.Pron_Anterior,
                                       pronCierre.Pron_Actual
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepPronosticoCierre_Modificar", ref validador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean RemisionesPendientes(int Id_Emp, int Id_Cd, string Conexion, ref string RemisionesPend)
        {

            SqlDataReader dr = null;
            Boolean res = false;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd"
                                      };
            object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd
                                   };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCierre_RemisionesPendientes", ref dr, Parametros, Valores);

            while (dr.Read())
            {
                res = true;

                RemisionesPend += (dr[0].ToString() + " ");
            }


            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            return res;
        }


    }
}
