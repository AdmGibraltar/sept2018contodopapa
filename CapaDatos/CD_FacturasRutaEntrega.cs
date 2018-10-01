using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_FacturasRutaEntrega
    {
        public void ConsultaFacturasRutaEntrega(Sesion sesion, FacturaEntregaRuta facturasfiltro, ref List<FacturaEntregaRuta> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Embarque",
                                          "@Filtro_Estatus",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin"
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp, 
                                       sesion.Id_Cd_Ver,
                                       facturasfiltro.Filtro_Embarque  == "" ? (object)null : facturasfiltro.Filtro_Embarque,
                                       facturasfiltro.Filtro_Estatus  == "" ? (object)null : facturasfiltro.Filtro_Estatus,                                      
                                       facturasfiltro.Filtro_FecIni  == "" ? (object)null : facturasfiltro.Filtro_FecIni,
                                       facturasfiltro.Filtro_FecFin  == "" ? (object)null : facturasfiltro.Filtro_FecFin 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaRutaEntrega_Consulta", ref dr, Parametros, Valores);

                FacturaEntregaRuta facturaEntrega;
                while (dr.Read())
                {
                    facturaEntrega = new FacturaEntregaRuta();                   
                    facturaEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Emb_Estatus"));                   
                    facturaEntrega.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    facturaEntrega.Cte_NomComercial1 = (string)dr.GetValue(dr.GetOrdinal("Usuario"));
                    facturaEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Emb_Fec"));
                    facturaEntrega.Id_Emb = (int)dr.GetValue(dr.GetOrdinal("Id_Emb"));
                    facturaEntrega.Dia = (int)dr.GetValue(dr.GetOrdinal("Dia"));
                    facturaEntrega.Chofer = (string)dr.GetValue(dr.GetOrdinal("Emb_Chofer"));
                    facturaEntrega.Camion = (string)dr.GetValue(dr.GetOrdinal("Emb_Camioneta"));  
                    List.Add(facturaEntrega);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarFacturaRutaEntrega(Sesion sesion, FacturaEntregaRuta facturas, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",                                         
                                         "@Id_Usu",                                        
                                         "@Id_Emb"
                                      };
                object[] Valores = {                                       
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver, 
                                       sesion.Id_U,
                                       facturas.Id_Emb                                       
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaRutaEntrega_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
