using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatSistemasPropietarios
    {
        public void ConsultaBanco(string Conexion, ref List<SistemasPropietarios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  };
                object[] Valores = {  };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSisPropietarios_Consulta", ref dr, Parametros, Valores);

                SistemasPropietarios sp;
                while (dr.Read())
                {
                    sp = new SistemasPropietarios();
                    sp.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Spo"));
                    sp.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Spo_Descripcion"));
                    sp.Factor = (float)dr.GetValue(dr.GetOrdinal("Spo_Factor"));
                    sp.Clase = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Spo_Clase")));
                    sp.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Spo_Activo")));
                    if (Convert.ToBoolean(sp.Estatus))
                    {
                        sp.EstatusStr = "Activo";
                    }
                    else
                    {
                        sp.EstatusStr = "Inactivo";
                    }
                    List.Add(sp);
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
