using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_SolConvenio
    {
        public void ProPrecioConv_SolicitudLista(SolConvenio conv, ref List<SolConvenio> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Sol",
                                        "@PC_NoConvenio",
                                        "@Sol_Estatus",
                                         "@Id_Cd"};
                object[] Valores = { conv.FiltroId_Sol == (int?) null ? (object) null: conv.FiltroId_Sol,
                                     conv.FiltroPc_NoConvenio == "" ? (object) null: conv.FiltroPc_NoConvenio,
                                     conv.FiltroSol_Estatus == "-1" ?(object) null: conv.FiltroSol_Estatus,
                                     conv.FiltroId_CD  == (int?) null ? (object) null: conv.FiltroId_CD };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudLista", ref dr, Parametros, Valores);

                SolConvenio c;
                while (dr.Read())
                {
                    c = new SolConvenio();
                    c.Id_Sol = Convert.ToInt32(dr["Id_Sol"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.Id_PC = Convert.ToInt32(dr["Id_PC"]);
                    c.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                    c.Sol_EstatusStr = dr["Sol_EstatusStr"].ToString();
                    c.Sol_IdUCreo = Convert.ToInt32(dr["Sol_IdUCreo"]);
                    c.Sol_UNombre = dr["Sol_UNombre"].ToString();
                    c.Sol_Fecha = Convert.ToDateTime(dr["Sol_Fecha"]);
                    c.CD_Nombre = dr["Cd_Nombre"].ToString();
                    c.Sol_Unique = dr["Sol_Unique"].ToString();
                    List.Add(c);

                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}
