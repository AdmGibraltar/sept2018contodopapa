using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatPosicion
    {
        public void Lista(Posicion posicion, string Conexion, ref List<Posicion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { posicion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPosicion_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    posicion = new Posicion();
                    posicion.Id_Pos = (int)dr.GetValue(dr.GetOrdinal("Id_Pos"));
                    posicion.Pos_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Area_Descripcion"));
                    posicion.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    posicion.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    posicion.Id_Est = (int)dr.GetValue(dr.GetOrdinal("Id_Est"));
                    posicion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Area_Activo")));
                    if (Convert.ToBoolean(posicion.Estatus))
                    {
                        posicion.EstatusStr = "Activo";
                    }
                    else
                    {
                        posicion.EstatusStr = "Inactivo";
                    }
                    List.Add(posicion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Posicion posicion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Pos", 
                                          "@Pos_Descripcion", 
                                          "@Id_Seg", 
                                          "@Id_Est",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       posicion.Id_Emp, 
                                       posicion.Id_Pos,
                                       posicion.Pos_Descripcion,
                                       posicion.Id_Seg,
                                       posicion.Id_Est,
                                       posicion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPosicion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Posicion posicion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Pos", 
                                          "@Pos_Descripcion", 
                                          "@Id_Seg", 
                                          "@Id_Est",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       posicion.Id_Emp, 
                                       posicion.Id_Pos,
                                       posicion.Pos_Descripcion,
                                       posicion.Id_Seg,
                                       posicion.Id_Est,
                                       posicion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPosicion_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
