using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatAcreedor
    {
        public void InsertarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_Acr",
                                        "@Acr_Tipo",
                                        "@Acr_Nombre",
                                        "@Acr_Calle",
                                        "@Acr_Numero",
                                        "@Acr_NumInterior",
                                        "@Acr_CP",
                                        "@Acr_Colonia",
                                        "@Acr_Municipio",
                                        "@Acr_Estado",
                                        "@Acr_RFC",
                                        "@Acr_Telefono",
                                        "@Acr_Correo",
                                        "@Acr_CondPago",
                                        "@Acr_Banco",
                                        "@Acr_Cuenta",
                                        "@Acr_Estatus"
                                      };

                object[] Valores = { 
                                       acreedor.Id_Emp,
                                       acreedor.Id_Cd,
                                       acreedor.Id_Acr,
                                       acreedor.Acr_Tipo,
                                       acreedor.Acr_Nombre,
                                       acreedor.Acr_Calle,
                                       acreedor.Acr_Numero,
                                       acreedor.Acr_NumInterior,
                                       acreedor.Acr_CP,
                                       acreedor.Acr_Colonia,
                                       acreedor.Acr_Municipio,
                                       acreedor.Acr_Estado,
                                       acreedor.Acr_RFC,
                                       acreedor.Acr_Telefono,
                                       acreedor.Acr_Correo,
                                       acreedor.Acr_CondPago,
                                       acreedor.Acr_Banco,
                                       acreedor.Acr_Cuenta,
                                       acreedor.Acr_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedor_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_Acr",
                                        "@Acr_Tipo",
                                        "@Acr_Nombre",
                                        "@Acr_Calle",
                                        "@Acr_Numero",
                                        "@Acr_NumInterior",
                                        "@Acr_CP",
                                        "@Acr_Colonia",
                                        "@Acr_Municipio",
                                        "@Acr_Estado",
                                        "@Acr_RFC",
                                        "@Acr_Telefono",
                                        "@Acr_Correo",
                                        "@Acr_CondPago",
                                        "@Acr_Banco",
                                        "@Acr_Cuenta",
                                        "@Acr_Estatus"
                                      };

                object[] Valores = { 
                                       acreedor.Id_Emp,
                                       acreedor.Id_Cd,
                                       acreedor.Id_Acr,
                                       acreedor.Acr_Tipo,
                                       acreedor.Acr_Nombre,
                                       acreedor.Acr_Calle,
                                       acreedor.Acr_Numero,
                                       acreedor.Acr_NumInterior,
                                       acreedor.Acr_CP,
                                       acreedor.Acr_Colonia,
                                       acreedor.Acr_Municipio,
                                       acreedor.Acr_Estado,
                                       acreedor.Acr_RFC,
                                       acreedor.Acr_Telefono,
                                       acreedor.Acr_Correo,
                                       acreedor.Acr_CondPago,
                                       acreedor.Acr_Banco,
                                       acreedor.Acr_Cuenta,
                                       acreedor.Acr_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedor_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_Acr",
                                        "@Acr_NumeroGenerado"
                                      };

                object[] Valores = { 
                                       acreedor.Id_Emp,
                                       acreedor.Id_Cd,
                                       acreedor.Id_Acr,
                                       acreedor.Acr_NumeroGenerado
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedor_Autorizar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAcreedor(Acreedor acreedor, string Conexion, ref List<Acreedor> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd","@Acr_Nombre" };
                object[] Valores = { acreedor.Id_Emp, acreedor.Id_Cd,acreedor.Acr_Nombre };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedor_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    acreedor = new Acreedor();
                    acreedor.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    acreedor.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    acreedor.Id_Acr = (int)dr.GetValue(dr.GetOrdinal("Id_Acr"));
                    acreedor.Acr_Tipo = (int)dr.GetValue(dr.GetOrdinal("Acr_Tipo"));
                    acreedor.Acr_Nombre = dr["Acr_Nombre"].ToString();
                    acreedor.Acr_Calle = dr["Acr_Calle"].ToString();
                    acreedor.Acr_Numero = dr["Acr_Numero"].ToString();
                    acreedor.Acr_NumInterior = dr["Acr_NumInterior"].ToString();
                    acreedor.Acr_CP = dr["Acr_CP"].ToString();
                    acreedor.Acr_Colonia = dr["Acr_Colonia"].ToString();
                    acreedor.Acr_Municipio = dr["Acr_Municipio"].ToString();
                    acreedor.Acr_Estado = dr["Acr_Estado"].ToString();
                    acreedor.Acr_RFC = dr["Acr_RFC"].ToString();
                    acreedor.Acr_Telefono = dr["Acr_Telefono"].ToString();
                    acreedor.Acr_Correo = dr["Acr_Correo"].ToString();
                    acreedor.Acr_Contacto = dr["Acr_Contacto"].ToString();
                    acreedor.Acr_CondPago = (int)dr.GetValue(dr.GetOrdinal("Acr_CondPago"));
                    acreedor.Acr_Clave = dr["Acr_Clave"].ToString();
                    acreedor.Acr_Autorizado = Boolean.Parse(dr["Acr_Autorizado"].ToString());
                    acreedor.Acr_Banco = dr["Acr_Banco"].ToString();
                    acreedor.Acr_Cuenta = dr["Acr_Cuenta"].ToString();
                    acreedor.Acr_Estatus = Boolean.Parse(dr["Acr_Estatus"].ToString());
                    acreedor.Acr_EstatusDescripcion = dr["Acr_EstatusDescripcion"].ToString() ;

 

                    list.Add(acreedor);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAcreedor(Acreedor acreedor, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Acr", "@Acr_Estatus" };
                object[] Valores = { acreedor.Id_Emp, acreedor.Id_Cd, acreedor.Id_Acr,acreedor.Acr_Estatus };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedor_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    acreedor.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    acreedor.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    acreedor.Id_Acr = (int)dr.GetValue(dr.GetOrdinal("Id_Acr"));
                    acreedor.Acr_Tipo = (int)dr.GetValue(dr.GetOrdinal("Acr_Tipo"));
                    acreedor.Acr_Nombre = dr["Acr_Nombre"].ToString();
                    acreedor.Acr_Calle = dr["Acr_Calle"].ToString();
                    acreedor.Acr_Numero = dr["Acr_Numero"].ToString();
                    acreedor.Acr_NumInterior = dr["Acr_NumInterior"].ToString();
                    acreedor.Acr_CP = dr["Acr_CP"].ToString();
                    acreedor.Acr_Colonia = dr["Acr_Colonia"].ToString();
                    acreedor.Acr_Municipio = dr["Acr_Municipio"].ToString();
                    acreedor.Acr_Estado = dr["Acr_Estado"].ToString();
                    acreedor.Acr_RFC = dr["Acr_RFC"].ToString();
                    acreedor.Acr_Telefono = dr["Acr_Telefono"].ToString();
                    acreedor.Acr_Correo = dr["Acr_Correo"].ToString();
                    acreedor.Acr_Contacto = dr["Acr_Contacto"].ToString();
                    acreedor.Acr_CondPago = (int)dr.GetValue(dr.GetOrdinal("Acr_CondPago"));
                    acreedor.Acr_Clave = dr["Acr_Clave"].ToString();
                    acreedor.Acr_Autorizado = Boolean.Parse(dr["Acr_Autorizado"].ToString());
                    acreedor.Acr_NumeroGenerado = dr["Acr_NumeroGenerado"].ToString();
                    acreedor.Acr_Banco = dr["Acr_Banco"].ToString();
                    acreedor.Acr_Cuenta = dr["Acr_Cuenta"].ToString();
                    acreedor.Acr_Estatus = Boolean.Parse(dr["Acr_Estatus"].ToString());
                    acreedor.Acr_EstatusDescripcion = dr["Acr_EstatusDescripcion"].ToString();

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidaRFC(int Id_Acr, string Acr_RFC, string Conexion)
        {
            bool Result = false;
            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    Result = (bool)oDB.spExecScalar(
                        "spCatAcreedor_ValidaRFC",
                        new SqlParameter("@Id_Acr", Id_Acr),
                        new SqlParameter("@Acr_RFC", Acr_RFC)
                    );

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

            return Result;
        }

        //jfcv 26oct2016 consulta por numero generado  punto 4
        public void ConsultaAcreedorPorNumero(Acreedor acreedor, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Acr_NumeroGenerado" };
                object[] Valores = { acreedor.Id_Emp, acreedor.Id_Cd, acreedor.Acr_NumeroGenerado };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAcreedorPorNumero_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    acreedor.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    acreedor.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    acreedor.Id_Acr = (int)dr.GetValue(dr.GetOrdinal("Id_Acr"));
                    acreedor.Acr_Tipo = (int)dr.GetValue(dr.GetOrdinal("Acr_Tipo"));
                    acreedor.Acr_Nombre = dr["Acr_Nombre"].ToString();
                    acreedor.Acr_Calle = dr["Acr_Calle"].ToString();
                    acreedor.Acr_Numero = dr["Acr_Numero"].ToString();
                    acreedor.Acr_NumInterior = dr["Acr_NumInterior"].ToString();
                    acreedor.Acr_CP = dr["Acr_CP"].ToString();
                    acreedor.Acr_Colonia = dr["Acr_Colonia"].ToString();
                    acreedor.Acr_Municipio = dr["Acr_Municipio"].ToString();
                    acreedor.Acr_Estado = dr["Acr_Estado"].ToString();
                    acreedor.Acr_RFC = dr["Acr_RFC"].ToString();
                    acreedor.Acr_Telefono = dr["Acr_Telefono"].ToString();
                    acreedor.Acr_Correo = dr["Acr_Correo"].ToString();
                    acreedor.Acr_Contacto = dr["Acr_Contacto"].ToString();
                    acreedor.Acr_CondPago = (int)dr.GetValue(dr.GetOrdinal("Acr_CondPago"));
                    acreedor.Acr_Clave = dr["Acr_Clave"].ToString();
                    acreedor.Acr_Autorizado = Boolean.Parse(dr["Acr_Autorizado"].ToString());
                    acreedor.Acr_NumeroGenerado = dr["Acr_NumeroGenerado"].ToString();
                    acreedor.Acr_Banco = dr["Acr_Banco"].ToString();
                    acreedor.Acr_Cuenta = dr["Acr_Cuenta"].ToString();
                   
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
