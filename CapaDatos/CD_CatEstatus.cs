using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatEstatus
    {
        public void Insertar(CapaEntidad.Estatus estatus, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Estatus", 
                                          "@Est_Descripcion", 
                                          "@Est_Estatus" 
                                      };
                object[] Valores = { 
                                       estatus.Id_Emp, 
                                       estatus.Id_Estatus,
                                       estatus.Es_Descripcion,
                                       estatus.Es_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEstatus_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(CapaEntidad.Estatus estatus, string Conexion, ref List<CapaEntidad.Estatus> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { estatus.Id_Emp};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEstatus_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    estatus = new Estatus();
                    estatus.Id_Estatus = (int)dr.GetValue(dr.GetOrdinal("Id_Est"));
                    estatus.Es_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Est_Descripcion"));
                    estatus.Es_Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Est_Activo")));
                    if (Convert.ToBoolean(estatus.Es_Estatus))
                    {
                        estatus.Es_EstatusStr = "Activo";
                    }
                    else
                    {
                        estatus.Es_EstatusStr = "Inactivo";
                    }
                    List.Add(estatus);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Borrar(Estatus estatus, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Est"
                                      };
                object[] Valores = { 
                                       estatus.Id_Emp, 
                                       estatus.Id_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatEstatus_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
