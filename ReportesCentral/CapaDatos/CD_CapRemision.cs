using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapRemision
    {             
       
        public void ConsultaEstadistica(Remision rem, string Conexion, ref List<Remision> listaRemision)
        {
            try
            {
                
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Ano", "@Mes", "@Id_Tm" };
                object[] Valores = { rem.Id_Emp, rem.Cal_Anio, rem.Cal_Mes, rem.Id_Tm};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepInvControlRemisiones", ref dr, Parametros, Valores);

                
                while (dr.Read())
                {

                    Remision remision = new Remision();
                    remision.Id_Emp = rem.Id_Emp;
                    remision.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    remision.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remision.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    remision.Est_Acumulado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Acumulado")));
                    remision.Est_Ene = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Ene")));
                    remision.Est_Feb = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Feb")));
                    remision.Est_Mar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Mar")));
                    remision.Est_Abr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Abr")));
                    remision.Est_May = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_May")));
                    remision.Est_Jun = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Jun")));
                    remision.Est_Jul = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Jul")));
                    remision.Est_Ago = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Ago")));
                    remision.Est_Sep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Sep")));
                    remision.Est_Oct = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Oct")));
                    remision.Est_Nov = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Nov")));
                    remision.Est_Dic = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Dic")));

                    remision.Est_CAcumulado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CAcumulado")));
                    remision.Est_CEne = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CEne")));
                    remision.Est_CFeb = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CFeb")));
                    remision.Est_CMar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CMar")));
                    remision.Est_CAbr = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CAbr")));
                    remision.Est_CMay = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CMay")));
                    remision.Est_CJun = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CJun")));
                    remision.Est_CJul = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CJul")));
                    remision.Est_CAgo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CAgo")));
                    remision.Est_CSep = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CSep")));
                    remision.Est_COct = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_COct")));
                    remision.Est_CNov = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CNov")));
                    remision.Est_CDic = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Est_CDic")));
                    remision.Est_Total = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Est_Total")));
                    remision.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    remision.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    remision.Total = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Total")));
                    remision.TotalCosto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("TotalCosto")));
                    remision.Vigente = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vigente")));
                    remision.Vencido = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vencido")));                 



                    listaRemision.Add(remision);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

       

       
    }
}
