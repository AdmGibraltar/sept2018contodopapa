using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatPagoElectronicoCuenta
    {
        public void InsertarCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_PagElecCuenta",
                                        "@PagElecCuenta_CC",
                                        "@PagElecCuenta_Numero",
                                        "@PagElecCuenta_Descripcion",
                                        "@PagElecCuenta_SubCuenta",
                                        "@PagElecCuenta_SubSubCuenta",
                                        "@PagElecCuenta_CuentaPago",
                                        "@Flete",		 
	                                    "@NoInventariable", 
	                                    "@CompraLocal", 
	                                    "@Servicios", 
	                                    "@Otros",	 
	                                    "@Honorarios",	 
	                                    "@Arrendamientos"
                                      };

                object[] Valores = { 
                                       cuenta.Id_Emp,
                                       cuenta.Id_Cd,
                                       cuenta.Id_PagElecCuenta,
                                       cuenta.PagElecCuenta_CC,
                                       cuenta.PagElecCuenta_Numero,
                                       cuenta.PagElecCuenta_Descripcion,
                                       cuenta.PagElecCuenta_SubCuenta,
                                       cuenta.PagElecCuenta_SubSubCuenta,
                                       cuenta.PagElecCuenta_CuentaPago,
                                       cuenta.Flete,
                                       cuenta.NoInventariable,
                                       cuenta.CompraLocal,
                                       cuenta.Servicios,
                                       cuenta.Otros,
                                       cuenta.Honorarios,
                                       cuenta.Arrendamientos
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPagoElectronicoCuenta_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_PagElecCuenta",
                                        "@PagElecCuenta_CC",
                                        "@PagElecCuenta_Numero",
                                        "@PagElecCuenta_Descripcion",
                                        "@PagElecCuenta_SubCuenta",
                                        "@PagElecCuenta_SubSubCuenta",
                                        "@PagElecCuenta_CuentaPago",
                                        "@Flete",		 
	                                    "@NoInventariable", 
	                                    "@CompraLocal", 
	                                    "@Servicios", 
	                                    "@Otros",	 
	                                    "@Honorarios",	 
	                                    "@Arrendamientos"
                                      };

                object[] Valores = { 
                                       cuenta.Id_Emp,
                                       cuenta.Id_Cd,
                                       cuenta.Id_PagElecCuenta,
                                       cuenta.PagElecCuenta_CC,
                                       cuenta.PagElecCuenta_Numero,
                                       cuenta.PagElecCuenta_Descripcion,
                                       cuenta.PagElecCuenta_SubCuenta,
                                       cuenta.PagElecCuenta_SubSubCuenta,
                                       cuenta.PagElecCuenta_CuentaPago,
                                       cuenta.Flete,
                                       cuenta.NoInventariable,
                                       cuenta.CompraLocal,
                                       cuenta.Servicios,
                                       cuenta.Otros,
                                       cuenta.Honorarios,
                                       cuenta.Arrendamientos
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPagoElectronicoCuenta_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref List<PagoElectronicoCuenta> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Subtipo" };
                object[] Valores = { cuenta.Id_Emp, cuenta.Id_Cd, cuenta.Id_Subtipo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPagoElectronicoCuenta_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    cuenta = new PagoElectronicoCuenta();
                    cuenta.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cuenta.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cuenta.Id_PagElecCuenta = (int)dr.GetValue(dr.GetOrdinal("Id_PagElecCuenta"));
                    cuenta.PagElecCuenta_CC = dr["PagElecCuenta_CC"].ToString();
                    cuenta.PagElecCuenta_Descripcion = dr["PagElecCuenta_Descripcion"].ToString();
                    cuenta.PagElecCuenta_Numero = dr["PagElecCuenta_Numero"].ToString();
                    cuenta.PagElecCuenta_SubCuenta = dr["PagElecCuenta_SubCuenta"].ToString();
                    cuenta.PagElecCuenta_SubSubCuenta = dr["PagElecCuenta_SubSubCuenta"].ToString();
                    cuenta.PagElecCuenta_CuentaPago = dr["PagElecCuenta_CuentaPago"].ToString();
                    cuenta.Flete = (Boolean)dr.GetValue(dr.GetOrdinal("Flete"));
                    cuenta.NoInventariable = (Boolean)dr.GetValue(dr.GetOrdinal("NoInventariable"));
                    cuenta.CompraLocal = (Boolean)dr.GetValue(dr.GetOrdinal("CompraLocal"));
                    cuenta.Servicios = (Boolean)dr.GetValue(dr.GetOrdinal("Servicios"));
                    cuenta.Otros = (Boolean)dr.GetValue(dr.GetOrdinal("Otros"));
                    cuenta.Honorarios = (Boolean)dr.GetValue(dr.GetOrdinal("Honorarios"));
                    cuenta.Arrendamientos = (Boolean)dr.GetValue(dr.GetOrdinal("Arrendamientos"));

                    //Flete NoInventariable CompraLocal Servicios Otros Honorarios Arrendamientos
                    list.Add(cuenta);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuenta(PagoElectronicoCuenta cuenta, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_PagElecCuenta" };
                object[] Valores = { cuenta.Id_Emp, cuenta.Id_Cd, cuenta.Id_PagElecCuenta };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatPagoElectronicoCuenta_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    cuenta.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cuenta.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cuenta.Id_PagElecCuenta = (int)dr.GetValue(dr.GetOrdinal("Id_PagElecCuenta"));
                    cuenta.PagElecCuenta_CC = dr["PagElecCuenta_CC"].ToString();
                    cuenta.PagElecCuenta_Descripcion = dr["PagElecCuenta_Descripcion"].ToString();
                    cuenta.PagElecCuenta_Numero = dr["PagElecCuenta_Numero"].ToString();
                    cuenta.PagElecCuenta_SubCuenta = dr["PagElecCuenta_SubCuenta"].ToString();
                    cuenta.PagElecCuenta_SubSubCuenta = dr["PagElecCuenta_SubSubCuenta"].ToString();
                    cuenta.PagElecCuenta_CuentaPago = dr["PagElecCuenta_CuentaPago"].ToString();
                    cuenta.Flete = (Boolean)dr.GetValue(dr.GetOrdinal("Flete"));
                    cuenta.NoInventariable = (Boolean)dr.GetValue(dr.GetOrdinal("NoInventariable"));
                    cuenta.CompraLocal = (Boolean)dr.GetValue(dr.GetOrdinal("CompraLocal"));
                    cuenta.Servicios = (Boolean)dr.GetValue(dr.GetOrdinal("Servicios"));
                    cuenta.Otros = (Boolean)dr.GetValue(dr.GetOrdinal("Otros"));
                    cuenta.Honorarios = (Boolean)dr.GetValue(dr.GetOrdinal("Honorarios"));
                    cuenta.Arrendamientos = (Boolean)dr.GetValue(dr.GetOrdinal("Arrendamientos"));
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
