using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Rastreo
    {
        public void Lista(Rastreo rastreo, List<Rastreo> list, string Conexion, int tipoBusqueda)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Doc_Tipo", "@Id_SerieDoc", "@Id_Doc", "@Fac_FolioFiscal", "@tipoBusqueda" };
                object[] Valores = { rastreo.Id_Emp, rastreo.Id_Cd, rastreo.Ras_TipoDoc, rastreo.Ras_SerieDoc, rastreo.Ras_Doc, rastreo.Ras_FolioFiscal, tipoBusqueda };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRastreo_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    rastreo = new Rastreo();
                    rastreo.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    rastreo.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    rastreo.Cd_Externo = dr.GetValue(dr.GetOrdinal("Cd_Externo")).ToString();
                    rastreo.Doc_TipoMov = dr.GetValue(dr.GetOrdinal("Doc_TipoMov")).ToString();
                    rastreo.Id_Doc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Doc")));
                    rastreo.Doc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Doc_Fecha")));
                    rastreo.Doc_Importe = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Doc_Importe")));
                    rastreo.Doc_Estatus = dr.GetValue(dr.GetOrdinal("Doc_Estatus")).ToString();
                    rastreo.Doc_EstatusStr = Estatus(dr.GetValue(dr.GetOrdinal("Doc_Estatus")).ToString());
                    list.Add(rastreo);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LogDocumento(Rastreo rastreo, List<Rastreo> list, string Conexion, int tipoBusqueda)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros;
                object[] Valores;

                if (tipoBusqueda == 1)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                    Valores = new object[] { rastreo.Id_Emp, rastreo.Id_Cd, rastreo.Ras_Doc };
                }
                else
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Fac", "@Fac_FolioFiscal" };
                    Valores = new object[] { rastreo.Id_Emp, rastreo.Id_Cd, rastreo.Ras_Doc, rastreo.Ras_FolioFiscal };
                }

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("FacturaBitacoraCobDetalle_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    rastreo = new Rastreo();
                    rastreo.U_Actividad = dr.GetValue(dr.GetOrdinal("Actividad")).ToString();
                    rastreo.U_Nombre = dr.GetValue(dr.GetOrdinal("Usuario")).ToString();                   
                    rastreo.Doc_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha")));
                    rastreo.Id_Relacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Relacion")));                   
                    list.Add(rastreo);
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
            switch (p.ToUpper())
            {
                case "C": return "Capturado";
                case "I": return "Impreso";
                case "B": return "Baja";
                default: return "";
            }
        }
    }
}
