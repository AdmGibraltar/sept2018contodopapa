using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Correos
    {
        public void Guardar(CapaEntidad.Correos correo, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Cor_CorreosSvtaAlm", "@Cor_CorreosAlmCob" };
                object[] Valores = { correo.Id_Emp, correo.Id_Cd, correo.Cor_CorreosSvtaAlm, correo.Cor_CorreosAlmCob };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAvisos_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(ref CapaEntidad.Correos correo, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { correo.Id_Emp, correo.Id_Cd};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAvisos_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (dr.GetValue(dr.GetOrdinal("Id_Tipo")).ToString() == "1")
                    {
                        correo.Cor_CorreosSvtaAlm = dr.GetValue(dr.GetOrdinal("Cor_Correos")).ToString();
                    }
                    else
                    {
                        correo.Cor_CorreosAlmCob = dr.GetValue(dr.GetOrdinal("Cor_Correos")).ToString();
                    }
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
