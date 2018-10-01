using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatProveedores
    {

        public void ConsultaProveedores(Proveedores prv, string Conexion, ref List<Proveedores> List, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Centro", "@Empresa", "bdCentral" };
                object[] Valores = { prv.Centro, prv.Empresa, bdCentral };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProveedores_Consulta", ref dr, Parametros, Valores);

                Proveedores Prv = default(Proveedores);
                while (dr.Read())
                {
                    try
                    {
                        Prv = new Proveedores();
                        Prv.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                        Prv.Empresa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                        Prv.Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Descripcion")));
                        Prv.Calle = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Calle")));
                        Prv.Numero = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Numero")));
                        Prv.CP = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Cp")));
                        Prv.Colonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Colonia")));
                        Prv.Municipio = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Municipio")));
                        Prv.Telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Tel")));
                        Prv.RFC = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Rfc")));
                        Prv.Fax = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Fax")));
                        Prv.Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Mail")));
                        Prv.Estado = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Estado")));
                        Prv.Contacto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Contacto")));
                        Prv.Pais = Convert.ToString(dr.GetValue(dr.GetOrdinal("Pvd_Pais")));
                        Prv.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Pvd_Activo")));
                        Prv.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pvd_Tipo")));
                        Prv.Habilitar = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Pvd_Habilitar")));

                        if (Convert.ToBoolean(Prv.Estatus))
                        {
                            Prv.EstatusStr = "Activo";
                        }
                        else
                        {
                            Prv.EstatusStr = "Inactivo";
                        }
                        List.Add(Prv);
                    }
                    catch
                    { }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProveedores(Proveedores prv, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Pvd",
                                          "@Id_Emp",
                                          "@Pvd_Descripcion",
                                          "@Pvd_Calle",
                                          "@Pvd_Numero",
                                          "@Pvd_Cp",                                          	                                    
                                          "@Pvd_Colonia",                                                                                	                                    
                                          "@Pvd_Municipio",
	                                      "@Pvd_Tel",
	                                      "@Pvd_Rfc",
	                                      "@Pvd_Fax",
	                                      "@Pvd_Mail",
	                                      "@Pvd_Estado",
	                                      "@Pvd_Contacto",	                                      
	                                      "@Pvd_Pais",
	                                      "@Pvd_Activo",
                                          "@Id_Tpvd"
                                          

                                      };
                object[] Valores = { 
                                       prv.Id,
                                       prv.Empresa,
                                       prv.Descripcion,
                                       prv.Calle, 
                                       prv.Numero,
                                       prv.CP,
                                       prv.Colonia,
                                       prv.Municipio,
                                       prv.Telefono,
                                       prv.RFC,
                                       prv.Fax,
                                       prv.Correo,
                                       prv.Estado,
                                       prv.Contacto,                                      
                                       prv.Pais,
                                       prv.Estatus,
                                       prv.Tipo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProveedores_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProveedores(Proveedores prv, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Pvd",
                                          "@Id_Emp",
                                          "@Pvd_Descripcion",
                                          "@Pvd_Calle",
                                          "@Pvd_Numero",
                                          "@Pvd_Cp",                                          	                                    
                                          "@Pvd_Colonia",                                                                                	                                    
                                          "@Pvd_Municipio",
	                                      "@Pvd_Tel",
	                                      "@Pvd_Rfc",
	                                      "@Pvd_Fax",
	                                      "@Pvd_Mail",
	                                      "@Pvd_Estado",
	                                      "@Pvd_Contacto",	                                      
	                                      "@Pvd_Pais",
	                                      "@Pvd_Activo",
                                          "@Id_Tpvd"
                                      };
                object[] Valores = { 
                                       prv.Id,
                                       prv.Empresa,
                                       prv.Descripcion,
                                       prv.Calle, 
                                       prv.Numero,
                                       prv.CP,
                                       prv.Colonia,
                                       prv.Municipio,
                                       prv.Telefono,
                                       prv.RFC,
                                       prv.Fax,
                                       prv.Correo,
                                       prv.Estado,
                                       prv.Contacto,                                      
                                       prv.Pais,
                                       prv.Estatus,
                                       prv.Tipo

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProveedores_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaTMov(Movimientos mov, string Conexion, ref List<Movimientos> List, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Empresa", "@Id_TProv", "@Tm_NatMov", "bdCentral", "@Id_Cd" };
                object[] Valores = { mov.Id_Emp, mov.Id, mov.NatMov, bdCentral, mov.Id_Cd };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spMovimientosxProveedor_Consulta", ref dr, Parametros, Valores);

                Movimientos Mov = default(Movimientos);


                while (dr.Read())
                {

                    Mov = new Movimientos();
                    Mov.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    Mov.Nombre = dr.GetValue(dr.GetOrdinal("Tm_Nombre")).ToString();

                    List.Add(Mov);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarClavePorTipo(ProveedorInternoTipo mov, string Conexion, ref List<ProveedorInternoTipo> List, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Empresa",  "bdCentral" };
                object[] Valores = { mov.Id_Emp, bdCentral };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapProveedorClavexTipo_Consulta", ref dr, Parametros, Valores);
                ProveedorInternoTipo Mov = default(ProveedorInternoTipo);
                while (dr.Read())
                {

                    Mov = new ProveedorInternoTipo();
                    Mov.Id_Pvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    Mov.Id_Tpvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tpvd")).ToString());
                    Mov.Tpvd_Valida = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tpvd_Valida")).ToString());
                    Mov.Tpvd_Descripcion = dr.GetValue(dr.GetOrdinal("Tpvd_Descripcion")).ToString();

                    List.Add(Mov);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
