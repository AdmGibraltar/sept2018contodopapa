using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_UsuarioRik
    {
        public List<eUsuarioRik> Lista(int Id_Emp, int Id_Cd, string Conexion)
        {
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMUsuarioRik_Lista", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    eUsuarioRik Obj = new eUsuarioRik();                   
                    Obj.Id_Emp= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    Obj.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    Obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    Obj.EsGerente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsGerente")));
                    lst.Add(Obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null; 
            }

            return lst;
        }

        //
    }
}
