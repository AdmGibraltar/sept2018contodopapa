using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using CapaModelo;
using CapaDatos;

namespace CapaDatos
{
    public class CD_ProPrecioEspecial
    {
        public void ConsultaVentanaPrecioEspecial_ComboCliente(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion, ref string Cte_NomComercial)
        {
            /* para consultar nombre del cliente en el combo dentro del grid */
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Cte"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecial_ComboProducto(int Id_Emp, int Id_Cd, int Id_Prd, string Conexion, ref string Prd_Descripcion)
        {
            /* para consultar nombre del producto en el combo dentro del grid */
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Cd_Ver",
                                        "@Id_Prd"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Cd,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarVentanaPrecioEspecial(PrecioEspecial VentanaPrecioEspecial, string Conexion, ref string verificador)
        {
            string GUID = "";
            SqlCommand sqlcmd = default(SqlCommand);
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ape",
                                        "@Ape_Fecha",
                                        "@Ape_Estatus",
                                        "@Ape_Sustituye",
                                        "@Ape_Sustituida",
                                        "@Ape_Tipo",
                                        "@Ape_Solicitar",
                                        "@Ape_Nota",
                                        "@Id_U",
                                        "@Ape_Naturaleza",
                                        "@Ape_NumProveedor",
                                        "@Ape_Convenio",
                                        "@Ape_NumUsuario"

                                        
                                      };
                object[] Valores = { 
                                        VentanaPrecioEspecial.Id_Emp
                                        ,VentanaPrecioEspecial.Id_Cd
                                        ,VentanaPrecioEspecial.Id_Ape
                                        ,VentanaPrecioEspecial.Ape_Fecha
                                        ,VentanaPrecioEspecial.Ape_Estatus
                                        ,VentanaPrecioEspecial.Ape_Sustituye == 0 ? (object)null : VentanaPrecioEspecial.Ape_Sustituye
                                        ,VentanaPrecioEspecial.Ape_Sustituida == 0 ? (object)null : VentanaPrecioEspecial.Ape_Sustituida
                                        ,VentanaPrecioEspecial.Ape_Tipo == -1 ? (object)null : VentanaPrecioEspecial.Ape_Tipo
                                        ,VentanaPrecioEspecial.Ape_Solicitar == -1 ? (object)null : VentanaPrecioEspecial.Ape_Solicitar
                                        ,VentanaPrecioEspecial.Ape_Nota,
                                        VentanaPrecioEspecial.Id_U ,
                                       VentanaPrecioEspecial.Ape_Naturaleza,
                                       VentanaPrecioEspecial.Ape_NumProveedor,
                                       VentanaPrecioEspecial.Ape_Convenio,
                                       VentanaPrecioEspecial.Ape_NumUsuario
                                   };



                sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Insertar", ref verificador, Parametros, Valores);
                GUID = verificador;

                if (verificador == "")
                {
                    throw new Exception("Error al insertar datos en tabla de Precio Especial");
                }



                string[] ParametrosCte = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"//@Id_Ape"
                                        ,"@Id_ApeCte"
                                        ,"@Id_Cte"
                                      };

                foreach (VentanaPrecioEspecialCte VentanaPrecioEspecialCte in VentanaPrecioEspecial.ListVentanaPrecioEspecialCte)
                {
                    object[] ValoresCte = { 
                                        VentanaPrecioEspecialCte.Id_Emp
                                        ,VentanaPrecioEspecialCte.Id_Cd
                                        ,GUID//VentanaPrecioEspecialCte.Id_Ape
                                        ,VentanaPrecioEspecialCte.Id_ApeCte
                                        ,VentanaPrecioEspecialCte.Id_Cte
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialCte_Insertar", ref verificador, ParametrosCte, ValoresCte);

                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Precio Especial Cliente");
                    }
                }


                string[] ParametrosPro = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"//"@Id_Ape"
                                         
                                        ,"@Id_Prd"
                                        ,"@Id_Mon"
                                        ,"@Ape_VolVta"
                                        ,"@Ape_PreVta"
                                        ,"@Ape_FecInicio"
                                        ,"@Ape_FecFin"
                                        ,"@Ape_PreEsp"
                                      };

                foreach (VentanaPrecioEspecialPro VentanaPrecioEspecialPro in VentanaPrecioEspecial.ListVentanaPrecioEspecialPro)
                {
                    object[] ValoresPro = { 
                                        VentanaPrecioEspecialPro.Id_Emp
                                        ,VentanaPrecioEspecialPro.Id_Cd
                                        ,GUID// VentanaPrecioEspecialPro.Id_Ape
                                        
                                        ,VentanaPrecioEspecialPro.Id_Prd
                                        ,VentanaPrecioEspecialPro.Id_Mon
                                        ,VentanaPrecioEspecialPro.Ape_VolVta
                                        ,VentanaPrecioEspecialPro.Ape_PreVta
                                        ,VentanaPrecioEspecialPro.Ape_FecInicio
                                        ,VentanaPrecioEspecialPro.Ape_FecFin
                                        ,VentanaPrecioEspecialPro.Ape_PreEsp
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialPro_Insertar", ref verificador, ParametrosPro, ValoresPro);

                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Precio Especial Pro");
                    }
                }
                verificador = GUID;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ModificarVentanaPrecioEspecial(PrecioEspecial VentanaPrecioEspecial, string Conexion, ref string verificador)
        {
            //modificar un folio
            string GUID = "";
            SqlCommand sqlcmd = default(SqlCommand);
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ape"
                                        ,"@Ape_Fecha"
                                        ,"@Ape_Estatus"
                                        ,"@Ape_Sustituye"
                                        ,"@Ape_Sustituida"
                                        ,"@Ape_Tipo"
                                        ,"@Ape_Solicitar"
                                        ,"@Ape_Nota",
                                        "@Id_U" ,
                                        "@Ape_NumProveedor",
                                        "@Ape_Convenio",
                                        "@Ape_NumUsuario"
                                      };
                object[] Valores = { 
                                        VentanaPrecioEspecial.Id_Emp
                                        ,VentanaPrecioEspecial.Id_Cd
                                        ,VentanaPrecioEspecial.Id_Ape
                                        ,VentanaPrecioEspecial.Ape_Fecha
                                        ,VentanaPrecioEspecial.Ape_Estatus
                                        ,VentanaPrecioEspecial.Ape_Sustituye == 0 ? (object)null : VentanaPrecioEspecial.Ape_Sustituye
                                        ,VentanaPrecioEspecial.Ape_Sustituida == 0 ? (object)null : VentanaPrecioEspecial.Ape_Sustituida
                                        ,VentanaPrecioEspecial.Ape_Tipo == -1 ? (object)null : VentanaPrecioEspecial.Ape_Tipo
                                        ,VentanaPrecioEspecial.Ape_Solicitar == -1 ? (object)null : VentanaPrecioEspecial.Ape_Solicitar
                                        ,VentanaPrecioEspecial.Ape_Nota,
                                        VentanaPrecioEspecial.Id_U,
                                        VentanaPrecioEspecial.Ape_NumProveedor,
                                        VentanaPrecioEspecial.Ape_Convenio,
                                        VentanaPrecioEspecial.Ape_NumUsuario
                                   };

                //sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Modificar", ref verificador, Parametros, Valores);
                GUID = verificador;

                if (verificador == "")
                {
                    throw new Exception("Error al modificar datos de folio en tabla de Precio Especial");
                    //return??
                }

                //borrar datos existentes en PrecioEspecialCte y PrecioEspecialPro
                string[] ParametrosDel = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ape"
                                      };
                object[] ValoresDel = {
                                        VentanaPrecioEspecial.Id_Emp
                                        ,VentanaPrecioEspecial.Id_Cd
                                        ,VentanaPrecioEspecial.Id_Ape
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialCtePro_Eliminar", ref verificador, ParametrosDel, ValoresDel);
                if (verificador == "")
                {
                    throw new Exception("Error al borrar datos de folio en tabla de Precio Especial Cliente, y Precio Especial Pro");
                    //return??
                }


                string[] ParametrosCte = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"
                                        ,"@Id_ApeCte"
                                        ,"@Id_Cte"
                                      };

                foreach (VentanaPrecioEspecialCte VentanaPrecioEspecialCte in VentanaPrecioEspecial.ListVentanaPrecioEspecialCte)
                {
                    object[] ValoresCte = { 
                                        VentanaPrecioEspecialCte.Id_Emp
                                        ,VentanaPrecioEspecialCte.Id_Cd
                                        ,GUID
                                        ,VentanaPrecioEspecialCte.Id_ApeCte
                                        ,VentanaPrecioEspecialCte.Id_Cte
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialCte_Insertar", ref verificador, ParametrosCte, ValoresCte);
                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Precio Especial Cliente");
                        //return??
                    }
                }


                string[] ParametrosPro = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"
                                        
                                        ,"@Id_Prd"
                                        ,"@Id_Mon"
                                        ,"@Ape_VolVta"
                                        ,"@Ape_PreVta"
                                        ,"@Ape_FecInicio"
                                        ,"@Ape_FecFin"
                                        ,"@Ape_PreEsp"
                                      };

                foreach (VentanaPrecioEspecialPro VentanaPrecioEspecialPro in VentanaPrecioEspecial.ListVentanaPrecioEspecialPro)
                {
                    object[] ValoresPro = { 
                                        VentanaPrecioEspecialPro.Id_Emp
                                        ,VentanaPrecioEspecialPro.Id_Cd
                                        ,GUID
                                       
                                        ,VentanaPrecioEspecialPro.Id_Prd
                                        ,VentanaPrecioEspecialPro.Id_Mon
                                        ,VentanaPrecioEspecialPro.Ape_VolVta
                                        ,VentanaPrecioEspecialPro.Ape_PreVta
                                        ,VentanaPrecioEspecialPro.Ape_FecInicio
                                        ,VentanaPrecioEspecialPro.Ape_FecFin
                                        ,VentanaPrecioEspecialPro.Ape_PreEsp
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialPro_Insertar", ref verificador, ParametrosPro, ValoresPro);
                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Precio Especial Pro");
                        //return??
                    }
                }

                verificador = GUID;
                CapaDatos.CommitTrans();

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }
        public void ConsultarEmailsPt1(int Id_Emp, int Id_Cd, string Conexion, ref string pipelist)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentanaPrecioEspecial_EmailP1", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pipelist = (string)dr.GetValue(dr.GetOrdinal("Conf_Valor"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEmailsPt2(int Id_Emp, int Id_Cd, string Conexion, string Email, ref string nombre, ref int? id)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Email"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Email
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentanaPrecioEspecial_EmailP2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    nombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    id = (int?)dr.GetValue(dr.GetOrdinal("Id_U"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecial(ref PrecioEspecial VentanaPrecioEspecial, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Ape"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       folio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    VentanaPrecioEspecial = new PrecioEspecial();

                    VentanaPrecioEspecial.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    VentanaPrecioEspecial.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    VentanaPrecioEspecial.Id_Ape = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")));
                    VentanaPrecioEspecial.Ape_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_Fecha")));
                    VentanaPrecioEspecial.Ape_Estatus = dr.GetString(dr.GetOrdinal("Ape_Estatus"));
                    VentanaPrecioEspecial.Ape_Sustituye = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_Sustituye"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Ape_Sustituye"));
                    VentanaPrecioEspecial.Ape_Sustituida = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_Sustituida"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Ape_Sustituida"));
                    VentanaPrecioEspecial.Ape_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ape_Tipo")));
                    VentanaPrecioEspecial.Ape_Solicitar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ape_Solicitar")));
                    VentanaPrecioEspecial.Ape_Nota = dr.GetString(dr.GetOrdinal("Ape_Nota"));
                    VentanaPrecioEspecial.Ape_NotaResp = dr.GetString(dr.GetOrdinal("Ape_NotaResp"));
                    VentanaPrecioEspecial.Ape_NumProveedor = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_NumProveedor"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Ape_NumProveedor"));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_Convenio")))) VentanaPrecioEspecial.Ape_Convenio = string.Empty; else VentanaPrecioEspecial.Ape_Convenio = dr.GetValue(dr.GetOrdinal("Ape_Convenio")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_NumUsuario")))) VentanaPrecioEspecial.Ape_NumUsuario = string.Empty; else VentanaPrecioEspecial.Ape_NumUsuario = dr.GetValue(dr.GetOrdinal("Ape_NumUsuario")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecialCte(PrecioEspecial ape, string Conexion, ref List<VentanaPrecioEspecialCte> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Ape_Unique",
                                        "@Id_Ape"
                                      };
                object[] Valores = { 
                                        ape.Id_Emp,
                                        ape.Id_Cd,
                                        ape.Ape_Unique==""?(object)null:ape.Ape_Unique,
                                        ape.Id_Ape==0?(object)null:ape.Id_Ape
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialCte_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    VentanaPrecioEspecialCte VentanaPrecioEspecialCte = new VentanaPrecioEspecialCte();

                    VentanaPrecioEspecialCte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    VentanaPrecioEspecialCte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    VentanaPrecioEspecialCte.Id_Ape = (int)dr.GetValue(dr.GetOrdinal("Id_Ape"));
                    VentanaPrecioEspecialCte.Id_ApeCte = (int)dr.GetValue(dr.GetOrdinal("Id_ApeCte"));
                    VentanaPrecioEspecialCte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    VentanaPrecioEspecialCte.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    List.Add(VentanaPrecioEspecialCte);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecialPro(PrecioEspecial ape, string Conexion, ref List<VentanaPrecioEspecialPro> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Ape_Unique",
                                        "@Id_Ape",
                                        "@FecActual",
                                        "@Accion"
                                      };
                object[] Valores = { 
                                        ape.Id_Emp,
                                        ape.Id_Cd,
                                        ape.Ape_Unique == "" ? (object)null: ape.Ape_Unique,
                                        ape.Id_Ape==0? (object)null: ape.Id_Ape,
                                        ape.Ape_Fecha,
                                        ape.Accion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialPro_Consulta", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    VentanaPrecioEspecialPro ape_prd = new VentanaPrecioEspecialPro();

                    ape_prd.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    ape_prd.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    ape_prd.Id_Ape = (int)dr.GetValue(dr.GetOrdinal("Id_Ape"));
                    ape_prd.Id_ApePro = a++;
                    ape_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    ape_prd.Id_Mon = (int)dr.GetValue(dr.GetOrdinal("Id_Mon"));
                    ape_prd.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    ape_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    ape_prd.Ape_VolVta = (int)dr.GetValue(dr.GetOrdinal("Ape_VolVta"));
                    ape_prd.Ape_PreVta = (double)dr.GetValue(dr.GetOrdinal("Ape_PreVta"));
                    ape_prd.Ape_FecInicio = (DateTime)dr.GetValue(dr.GetOrdinal("Ape_FecInicio"));
                    ape_prd.Ape_FecFin = (DateTime)dr.GetValue(dr.GetOrdinal("Ape_FecFin"));
                    ape_prd.Ape_PreEsp = (double)dr.GetValue(dr.GetOrdinal("Ape_PreEsp"));
                    ape_prd.Ape_PreAAA = (double)dr.GetValue(dr.GetOrdinal("Prd_pesos"));
                    ape_prd.Ape_FecAut = dr.GetValue(dr.GetOrdinal("Ape_FecApr")) == DBNull.Value ? Convert.ToDateTime("01/01/2000") : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecApr")));

                    ape_prd.Ape_Estatus = dr.GetValue(dr.GetOrdinal("Ape_Estatus")).ToString();
                    List.Add(ape_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PrecioEspecialProductoCliente_Consulta(ref VentanaPrecioEspecialPro precioEspecialPro, string Conexion, int id_Emp, int id_Cd, int id_Cli, int id_Prd)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cli",
                                        "@Id_Prd"
                                        //"@Id_Mon"
                                      };
                object[] Valores = { 
                                        id_Emp,
                                        id_Cd,
                                        id_Cli,
                                        id_Prd
                                        //id_Mon
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioEspecialProductoCliente_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();

                    precioEspecialPro = new VentanaPrecioEspecialPro();
                    precioEspecialPro.Id_Ape = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")));
                    precioEspecialPro.Ape_FecInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecInicio")));
                    precioEspecialPro.Ape_FecFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecFin")));
                    precioEspecialPro.Ape_PreEsp = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ape_PreEsp")));
                    precioEspecialPro.Ape_PreVta = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ape_PreVta")));
                    precioEspecialPro.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PrecioEspecialSolicitudesVencidas_Consulta(ref List<VentanaPrecioEspecialPro> lista, string Conexion, int id_Emp, int id_Cd, int id_Cli, int id_Prd/*, int id_Mon*/)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cli",
                                        "@Id_Prd"
                                        //"@Id_Mon"
                                      };
                object[] Valores = { 
                                        id_Emp,
                                        id_Cd,
                                        id_Cli,
                                        id_Prd
                                        //id_Mon
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioEspecialSolicitudesVencidas_Consulta", ref dr, Parametros, Valores);

                lista = new List<VentanaPrecioEspecialPro>();
                while (dr.Read())
                {
                    VentanaPrecioEspecialPro precioEspecialPro = new VentanaPrecioEspecialPro();
                    precioEspecialPro.Id_Ape = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")));
                    precioEspecialPro.Ape_FecInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecInicio")));
                    precioEspecialPro.Ape_FecFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecFin")));
                    lista.Add(precioEspecialPro);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProAutPrecioEspecial_Lista(AutPrecioEspecial AutPrecioEspecial, string Conexion, ref List<AutPrecioEspecial> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Folio1",
                                        "@Folio2",
                                        "@Fecha1",
                                        "@Fecha2",
                                        "@Estatus",
                                        "@Id_Cte1",
                                        "@Id_Cte2"
                                      //  "@Solicitud"
                                      };
                object[] Valores = { 
                                       AutPrecioEspecial.Id_Emp, 
                                       AutPrecioEspecial.Id_Cd,
                                       AutPrecioEspecial.Folio1,
                                       AutPrecioEspecial.Folio2,
                                       AutPrecioEspecial.Fecha1,
                                       AutPrecioEspecial.Fecha2,
                                       AutPrecioEspecial.Estatus == "" ? (object)null: AutPrecioEspecial.Estatus,
                                       AutPrecioEspecial.Id_CteFiltro1,
                                       AutPrecioEspecial.Id_CteFiltro2
                                      // AutPrecioEspecial.Solicitud                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAutPrecioEspecial_Lista", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    AutPrecioEspecial = new AutPrecioEspecial();
                    AutPrecioEspecial.Id_Ape = (int)dr.GetValue(dr.GetOrdinal("Id_Ape"));
                    AutPrecioEspecial.Ape_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ape_Fecha"));
                    AutPrecioEspecial.Ape_Estatus = (string)dr.GetValue(dr.GetOrdinal("Ape_Estatus"));
                    AutPrecioEspecial.Ape_EstatusStr = Estatus((string)dr.GetValue(dr.GetOrdinal("Ape_Estatus")));
                    AutPrecioEspecial.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    AutPrecioEspecial.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    //usar para columnas no requeridas: AutPrecioEspecial.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    List.Add(AutPrecioEspecial);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Estatus(string p)
        {
            switch (p)
            {
                case "C": return "Pendiente de solicitar";
                case "S": return "Solicitada";
                case "A": return "Autorizado";
                case "P": return "Parcialmente autorizada";
                case "R": return "Rechazada";
                case "B": return "Cancelada";
                default: return "";
            }
        }

        public void ConsultaProAutPrecioEspecialVencido(ref int Vencido, int Id_Emp, int Id_Cd, int Id_Ape, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Ape"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Ape
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAutPrecioEspecialVencido_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Vencido = (int)dr.GetValue(dr.GetOrdinal("Vencido"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProAutPrecioEspecial(ref PrecioEspecial ape, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Ape_Unique",
                                        "@Id_Ape"
                                      };
                object[] Valores = { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd,
                                       ape.Ape_Unique==""?(object)null: ape.Ape_Unique,
                                       ape.Id_Ape == 0? (object)null : ape.Id_Ape
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    ape.Ape_Naturaleza = dr.GetValue(dr.GetOrdinal("Ape_Naturaleza")).ToString();
                    ape.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    ape.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    ape.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    ape.Id_Ape = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")).ToString());
                    ape.Ape_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_Fecha")).ToString());
                    ape.Ape_Nota = dr.GetValue(dr.GetOrdinal("Ape_Nota")).ToString();
                    ape.Ape_NotaResp = dr.GetValue(dr.GetOrdinal("Ape_NotaResp")).ToString();
                    ape.Ape_NumProveedor = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_NumProveedor"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Ape_NumProveedor"));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_Convenio")))) ape.Ape_Convenio = string.Empty; else ape.Ape_Convenio = dr.GetValue(dr.GetOrdinal("Ape_Convenio")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ape_NumUsuario")))) ape.Ape_NumUsuario = string.Empty; else ape.Ape_NumUsuario = dr.GetValue(dr.GetOrdinal("Ape_NumUsuario")).ToString();
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaProveedorSeleccionado(PrecioEspecial ape, string Conexion, ref int verificador, ref bool tieneProveedorNS)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                         "@Id_Cte"
                                        
                                      };
                object[] Valores = { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd,
                                       ape.Id_Cte
                                     
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioEspecialProveedorSeleccionado_Consulta", ref dr, Parametros, Valores);

                int resultado  = 0 ;

                if (dr.HasRows)
                {
                    dr.Read();
                    resultado =  Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Result")));
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                 tieneProveedorNS  =   resultado == 1 ? true: false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarPrecioEspecial(PrecioEspecial ape, string Conexion, List<VentanaPrecioEspecialPro> List, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                SqlCommand sqlcmd = default(SqlCommand);

                string[] Parametros = { 
                    "@Id_Emp", 
                    "@Id_Cd", 
                    "@Id_Ape" ,
                    "@Id_Prd",
                    "@Ape_VolVta",
                    "@Ape_PreVta",
                    "@Ape_FecInicio",
                    "@Ape_FecFin",
                    "@Ape_PreEsp",
                    "@Ape_FecApr",
                    "@Accion",
                    "@Ape_Estatus"
                };

                object[] Valores;

                for (int x = 0; x < List.Count; x++)
                {
                    Valores = new object[] { 
                        ape.Id_Emp, 
                        ape.Id_Cd,
                        ape.Id_Ape,
                        List[x].Id_Prd, 
                        List[x].Ape_VolVta,
                        List[x].Ape_PreVta,
                        List[x].Ape_FecInicio,
                        List[x].Ape_FecFin,
                        List[x].Ape_PreEsp,
                        List[x].Ape_FecAut,
                        ape.Accion,
                        List[x].Ape_Estatus
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialPro_Autorizar", ref verificador, Parametros, Valores);
                    if (verificador != 1)
                    {
                        break;
                    }
                }

                if (verificador == 1)
                {
                    Parametros = new string[] {
                                       "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Ape",
                                        "@Ape_NotaRes",
                                        "@Ape_NumProveedor",
                                        "@Ape_Convenio",
                                        "@Ape_NumUsuario"
                                       
                                      };

                    Valores = new object[] { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd,
                                       ape.Id_Ape,
                                       ape.Ape_NotaResp,
                                       ape.Ape_NumProveedor,
                                       ape.Ape_Convenio,
                                       ape.Ape_NumUsuario
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Autorizar", ref verificador, Parametros, Valores);

                }

                if (verificador == 1)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaProductoCliente(ref List<Clientes> List_cte, string Conexion, ref DateTime? Ape_FecInicio, ref DateTime? Ape_FecFin)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = default(CD_Datos);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Prd",
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@SolSustituye"
                                      };

                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);

                for (int x = 0; x < List_cte.Count; x++)
                {
                    CapaDatos = new CapaDatos.CD_Datos(Conexion);
                    dr = null;

                    Valores = new object[] { 
                        List_cte[x].Id_Emp,
                        List_cte[x].Id_Cd,
                        List_cte[x].Id_Cte,
                        List_cte[x].GenInt, 
                        Ape_FecInicio,
                        Ape_FecFin,
                        List_cte[x].GenInt2
                    };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecialCtePrd_Consulta", ref dr, Parametros, Valores);

                    if (dr.HasRows)
                    {
                        dr.Read();
                        List_cte[x].GenInt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")));
                        List_cte[x].GenBool = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ape")));
                        Ape_FecInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecInicio")));
                        Ape_FecFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecFin")));
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                        break;
                    }
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizaProveedor(PrecioEspecial pe, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            CapaDatos.StartTrans();
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ape"
                                        ,"@Ape_NumProveedor"
                                        ,"@Ape_Convenio"
                                        ,"@Ape_NumUsuario"
                                      };
                object[] Valores = { 
                                        pe.Id_Emp
                                        ,pe.Id_Cd
                                        ,pe.Id_Ape
                                        ,pe.Ape_NumProveedor
                                        ,pe.Ape_Convenio
                                        ,pe.Ape_NumUsuario
                                   };

                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioEspecialActualizaProveedor", ref verificador, Parametros, Valores);



                if (verificador == 1)
                {
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void EnviarPrecioEspecial(PrecioEspecial ape, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ape",
                                          "@Ape_Estatus"
                                      };

                object[] Valores = { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd, 
                                       ape.Id_Ape,
                                       ape.Ape_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Enviar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEnvio(ref PrecioEspecial pe, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ape" 
                                          
                                      };

                object[] Valores = { 
                                       pe.Id_Emp, 
                                       pe.Id_Cd, 
                                       pe.Id_Ape 
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Envio", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pe.Ape_Unique = dr.GetValue(dr.GetOrdinal("Ape_Unique")).ToString();
                    pe.Ape_Solicitar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ape_Solicitar")));
                    if (dr.GetValue(dr.GetOrdinal("Ape_Sustituye")) != DBNull.Value)
                    {
                        pe.Ape_Sustituye = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ape_Sustituye")));
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDatosEmail(ref PrecioEspecial pe, int Id_Ape, Sesion sesion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;

                string[] Parametros = {"@Id_Emp", "@Id_Cd", "@Id_Ape" };
                object[] Valores = {sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Ape};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProductoPrecio_ConsultaDatosMail", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    pe.Ape_Estatus = dr["Ape_Estatus"].ToString();
                    pe.Ape_NotaResp = dr["Ape_NotaResp"].ToString();
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
     

        public void Eliminar(PrecioEspecial pe, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ape" 
                                      };

                object[] Valores = { 
                                       pe.Id_Emp, 
                                       pe.Id_Cd, 
                                       pe.Id_Ape 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioEspecial_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión.
        /// </summary>
        /// <param name="precioEspecialPro"></param>
        /// <param name="icdCtx"></param>
        /// <param name="id_Emp"></param>
        /// <param name="id_Cd"></param>
        /// <param name="id_Cli"></param>
        /// <param name="id_Prd"></param>
        public void PrecioEspecialProductoCliente_Consulta(ref VentanaPrecioEspecialPro precioEspecialPro, ICD_Contexto icdCtx, int id_Emp, int id_Cd, int id_Cli, int id_Prd)
        {
            try
            {
                SqlCommand sqlcmd = null;

                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cli",
                                        "@Id_Prd"
                                        //"@Id_Mon"
                                      };
                object[] Valores = { 
                                        id_Emp,
                                        id_Cd,
                                        id_Cli,
                                        id_Prd
                                        //id_Mon
                                   };

                sqlcmd = CD_Datos.GenerarSqlCommand("spPrecioEspecialProductoCliente_Consulta", ref dr, Parametros, Valores, icdCtx);

                if (dr.HasRows)
                {
                    dr.Read();

                    precioEspecialPro = new VentanaPrecioEspecialPro();
                    precioEspecialPro.Id_Ape = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ape")));
                    precioEspecialPro.Ape_FecInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecInicio")));
                    precioEspecialPro.Ape_FecFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ape_FecFin")));
                    precioEspecialPro.Ape_PreEsp = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ape_PreEsp")));
                    precioEspecialPro.Ape_PreVta = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ape_PreVta")));
                    precioEspecialPro.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                }

                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
