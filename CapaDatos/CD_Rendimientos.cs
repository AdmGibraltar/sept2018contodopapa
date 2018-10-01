using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Rendimientos
    {

        public void InsertarRendimientos(Sesion sesion, string Conexion, string sessionID, ref Factura factura, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                Funciones funcion = new Funciones();
                CapaDatos.StartTrans();
              
                string[] Parametros = { 
	                                    "@Modo_Conexion" 
                                         ,"@Sucursal"
                                         ,"@usuario"
                                         ,"@Sesion"
                                         ,"@Id_Transaccion"
                                         ,"@Tipo_Transacción"
                                         ,"@Hora_Inicio"
                                         ,"@Hora_Fin"
                                        
                                      };
                object[] Valores = { 
                                         sesion.URL
                                        ,sesion.Id_Cd_Ver
                                        ,sesion.U_Nombre
                                        ,sessionID
                                        ,factura.Id_Fac
                                        ,"FACTURA"
                                        ,sesion.HoraInicio
                                        ,DateTime.Now
                                        

                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spInsertarRendimiento", ref verificador, Parametros, Valores);
             
                                                  
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarRendimientosRemisiones(Sesion sesion, string Conexion, string sessionID, ref int Id_Rem, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                Funciones funcion = new Funciones();
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Modo_Conexion" 
                                         ,"@Sucursal"
                                         ,"@usuario"
                                         ,"@Sesion"
                                         ,"@Id_Transaccion"
                                         ,"@Tipo_Transacción"
                                         ,"@Hora_Inicio"
                                         ,"@Hora_Fin"
                                        
                                      };
                object[] Valores = { 
                                         sesion.URL
                                        ,sesion.Id_Cd_Ver
                                        ,sesion.U_Nombre
                                        ,sessionID
                                        ,Id_Rem
                                        ,"REMISION"
                                        ,sesion.HoraInicio
                                        ,DateTime.Now
                                        

                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spInsertarRendimiento", ref verificador, Parametros, Valores);


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarRendimientosPedidos(Sesion sesion, string Conexion, string sessionID, ref Pedido pedido, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                Funciones funcion = new Funciones();
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Modo_Conexion" 
                                         ,"@Sucursal"
                                         ,"@usuario"
                                         ,"@Sesion"
                                         ,"@Id_Transaccion"
                                         ,"@Tipo_Transacción"
                                         ,"@Hora_Inicio"
                                         ,"@Hora_Fin"
                                        
                                      };
                object[] Valores = { 
                                         sesion.URL
                                        ,sesion.Id_Cd_Ver
                                        ,sesion.U_Nombre
                                        ,sessionID
                                        ,pedido.Id_Ped
                                        ,"PEDIDO"
                                        ,sesion.HoraInicio
                                        ,DateTime.Now
                                        

                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spInsertarRendimiento", ref verificador, Parametros, Valores);


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarRendimientosLogin(Sesion sesion, string Conexion, string sessionID, string actividad, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                Funciones funcion = new Funciones();
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Modo_Conexion" 
                                         ,"@Sucursal"
                                         ,"@usuario"
                                         ,"@Sesion"
                                         ,"@Id_Transaccion"
                                         ,"@Tipo_Transacción"
                                         ,"@Hora_Inicio"
                                         ,"@Hora_Fin"
                                        
                                      };
                object[] Valores = { 
                                         sesion.URL
                                        ,sesion.Id_Cd
                                        ,sesion.U_Nombre
                                        ,sessionID
                                        ,1
                                        ,actividad
                                        ,sesion.HoraInicio
                                        ,DateTime.Now
                                        

                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spInsertarRendimiento", ref verificador, Parametros, Valores);


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

    }
}
