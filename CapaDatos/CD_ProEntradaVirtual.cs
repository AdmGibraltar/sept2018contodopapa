using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ProEntradaVirtual
    {
        public void ConsultaVentanaProEntradaVirtual_ComboCliente(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion, ref string Cte_NomComercial)
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
        public void ConsultaVentanaProEntradaVirtual_ComboProducto(int Id_Emp, int Id_Cd, int Id_Prd, string Conexion, ref string Prd_Descripcion)
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
        public void InsertarVentanaEntradaVirtual(EntradaVirtual VentanaProEntradaVirtual, string Conexion, ref string verificador)
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
                                        "@Id_Env",
                                        "@Id_Cte",
                                        "@Env_CteNomComercial",
                                        "@Env_Fecha",
                                        "@Env_Estatus",
                                        "@Id_USolicita",
                                        "@Env_Credito",
                                        "@Env_Rentabilidad",
                                        "@Env_ImporteFacturar",
                                        "@Env_ComentariosSolicitante",
                                        "@Env_ComentariosAutoriza",
                                        "@Id_UAutorizo",                                       
                                        "@Id_Pvd"
                                        
                                      };
                object[] Valores = { 
                                        VentanaProEntradaVirtual.Id_Emp
                                        ,VentanaProEntradaVirtual.Id_Cd
                                        ,VentanaProEntradaVirtual.Id_Env                                        
                                        ,VentanaProEntradaVirtual.Id_Cte
                                        ,VentanaProEntradaVirtual.Env_CteNomComercial 
                                        ,VentanaProEntradaVirtual.Env_Fecha
                                        ,VentanaProEntradaVirtual.Env_Estatus
                                        ,VentanaProEntradaVirtual.Id_USolicita
                                        ,VentanaProEntradaVirtual.Env_Credito
                                        ,VentanaProEntradaVirtual.Env_Rentabilidad
                                        ,VentanaProEntradaVirtual.Env_ImporteFacturar
                                        ,VentanaProEntradaVirtual.Env_ComentariosSolicitante
                                        ,VentanaProEntradaVirtual.Env_ComentariosAutoriza
                                        ,VentanaProEntradaVirtual.Id_UAutorizo                                     
                                       ,VentanaProEntradaVirtual.Id_Pvd                                       
                                   };



                sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Insertar", ref verificador, Parametros, Valores);
                GUID = verificador;

                if (verificador == "")
                {
                    throw new Exception("Error al insertar datos en tabla de Entrada Virtual");
                }
            

                string[] ParametrosPro = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"//"@Id_Env"                                         
                                        ,"@Id_Prd"
                                        ,"@Env_Cantidad"
                                        ,"@Env_PreVta" 
                                        ,"@Env_Costo"
                                      };

                foreach (EntradaVirtualDet VentanaProEntradaVirtualDet in VentanaProEntradaVirtual.ListVentanaEntradaVirtualDet)
                {
                    object[] ValoresPro = { 
                                        VentanaProEntradaVirtualDet.Id_Emp
                                        ,VentanaProEntradaVirtualDet.Id_Cd
                                        ,GUID// VentanaProEntradaVirtualDet.Id_Env                                        
                                        ,VentanaProEntradaVirtualDet.Id_Prd                                       
                                        ,VentanaProEntradaVirtualDet.Env_Cantidad
                                        ,VentanaProEntradaVirtualDet.Env_PreVta
                                        ,VentanaProEntradaVirtualDet.Env_Costo
                                       
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDet_Insertar", ref verificador, ParametrosPro, ValoresPro);
                }
                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Entrada Virtual Pro");
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
        public void ModificarVentanaEntradaVirtual(EntradaVirtual VentanaProEntradaVirtual, string Conexion, ref string verificador)
        {
            //modificar un folio
            string GUID = "";
            SqlCommand sqlcmd = default(SqlCommand);
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Env",
                                        "@Id_Cte",
                                        "@Env_CteNomComercial",
                                        "@Env_Fecha",
                                        "@Env_Estatus",
                                        "@Id_USolicita",
                                        "@Env_Credito",
                                        "@Env_Rentabilidad",
                                        "@Env_ImporteFacturar",
                                        "@Env_ComentariosSolicitante",
                                        "@Env_ComentariosAutoriza" ,
                                        "@Id_UAutorizo",                                       
                                        "@Id_Pvd"
                                        
                                      };
                object[] Valores = { 
                                        VentanaProEntradaVirtual.Id_Emp
                                        ,VentanaProEntradaVirtual.Id_Cd
                                        ,VentanaProEntradaVirtual.Id_Env                                        
                                        ,VentanaProEntradaVirtual.Id_Cte
                                        ,VentanaProEntradaVirtual.Env_CteNomComercial 
                                        ,VentanaProEntradaVirtual.Env_Fecha
                                        ,VentanaProEntradaVirtual.Env_Estatus
                                        ,VentanaProEntradaVirtual.Id_USolicita
                                        ,VentanaProEntradaVirtual.Env_Credito
                                        ,VentanaProEntradaVirtual.Env_Rentabilidad
                                        ,VentanaProEntradaVirtual.Env_ImporteFacturar
                                       ,VentanaProEntradaVirtual.Env_ComentariosSolicitante
                                       ,VentanaProEntradaVirtual.Env_ComentariosAutoriza
                                       ,VentanaProEntradaVirtual.Id_UAutorizo                                       
                                       ,VentanaProEntradaVirtual.Id_Pvd
                                   };

                //sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Modificar", ref verificador, Parametros, Valores);
                GUID = verificador;

                if (verificador == "")
                {
                    throw new Exception("Error al modificar datos de folio en tabla de Precio Especial");
                    //return??
                }

                //borrar datos existentes en EntradaVirtualCte y EntradaVirtualDet
                string[] ParametrosDel = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Env"
                                      };
                object[] ValoresDel = {
                                        VentanaProEntradaVirtual.Id_Emp
                                        ,VentanaProEntradaVirtual.Id_Cd
                                        ,VentanaProEntradaVirtual.Id_Env
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDet_Eliminar", ref verificador, ParametrosDel, ValoresDel);
                if (verificador == "")
                {
                    throw new Exception("Error al borrar datos de folio en tabla de  Entrada Virtual Det");
                    //return??
                }

            
                string[] ParametrosPro = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@GUID"//"@Id_Env"                                         
                                        ,"@Id_Prd"
                                        ,"@Env_Cantidad"
                                        ,"@Env_PreVta"
                                        ,"@Env_Costo"
                                      };

                foreach (EntradaVirtualDet VentanaProEntradaVirtualDet in VentanaProEntradaVirtual.ListVentanaEntradaVirtualDet)
                {
                    object[] ValoresPro = { 
                                        VentanaProEntradaVirtualDet.Id_Emp
                                        ,VentanaProEntradaVirtualDet.Id_Cd
                                        ,GUID// VentanaProEntradaVirtualDet.Id_Env                                        
                                        ,VentanaProEntradaVirtualDet.Id_Prd                                       
                                        ,VentanaProEntradaVirtualDet.Env_Cantidad
                                        ,VentanaProEntradaVirtualDet.Env_PreVta
                                        ,VentanaProEntradaVirtualDet.Env_Costo
                                       
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDet_Insertar", ref verificador, ParametrosPro, ValoresPro);
                }

            
                    if (verificador == "")
                    {
                        throw new Exception("Error al insertar datos en tabla de Entrada Virtual Pro");
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentanaProEntradaVirtual_EmailP1", ref dr, Parametros, Valores);

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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentanaProEntradaVirtual_EmailP2", ref dr, Parametros, Valores);

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
        public void ConsultaVentanaProEntradaVirtual(ref EntradaVirtual VentanaProEntradaVirtual, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Env"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       folio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    VentanaProEntradaVirtual = new EntradaVirtual();

                    VentanaProEntradaVirtual.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    VentanaProEntradaVirtual.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    VentanaProEntradaVirtual.Id_Env = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Env")));
                    VentanaProEntradaVirtual.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    VentanaProEntradaVirtual.Env_CteNomComercial = dr.GetString(dr.GetOrdinal("Env_CteNomComercial"));
                    VentanaProEntradaVirtual.Env_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Env_Fecha")));
                    VentanaProEntradaVirtual.Env_Estatus =  dr.GetString(dr.GetOrdinal("Env_Estatus"));
                    VentanaProEntradaVirtual.Id_Pvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));

                    VentanaProEntradaVirtual.Id_UAutorizo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UAutorizo")));
                    VentanaProEntradaVirtual.Id_USolicita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_USolicita")));
                    VentanaProEntradaVirtual.Env_Credito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Env_Credito")));
                    VentanaProEntradaVirtual.Env_Rentabilidad = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Env_Rentabilidad")));
                    VentanaProEntradaVirtual.Env_ImporteFacturar = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Env_ImporteFacturar")));
                    VentanaProEntradaVirtual.Env_ComentariosSolicitante = dr.GetString(dr.GetOrdinal("Env_ComentariosSolicitante"));
                    VentanaProEntradaVirtual.Env_ComentariosAutoriza = dr.GetString(dr.GetOrdinal("Env_ComentariosAutoriza"));
                  
                 
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultaVentanaProEntradaVirtualDet(EntradaVirtual Ev, string Conexion, ref List<EntradaVirtualDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                       
                                        "@Id_Env"                                     
                                       
                                      };
                object[] Valores = { 
                                        Ev.Id_Emp,
                                        Ev.Id_Cd,                                        
                                        Ev.Id_Env==0? (object)null: Ev.Id_Env
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDet_Consulta", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    EntradaVirtualDet ev_prd = new EntradaVirtualDet();

                    ev_prd.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    ev_prd.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    ev_prd.Id_Env = (int)dr.GetValue(dr.GetOrdinal("Id_Env"));
                    ev_prd.Id_EvPro = a++;
                    ev_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));                  
                    ev_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    ev_prd.Env_Cantidad = (int)dr.GetValue(dr.GetOrdinal("Env_Cantidad"));
                    ev_prd.Env_PreVta = (decimal)dr.GetValue(dr.GetOrdinal("Env_PreVta"));
                    ev_prd.Env_Costo = (decimal)dr.GetValue(dr.GetOrdinal("Env_Costo"));  
                    List.Add(ev_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    
        
        public void ConsultaProAutEntradaVirtual_Lista(AutEntradaVirtual AutEntradaVirtual, string Conexion, ref List<AutEntradaVirtual> List)
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
                                       AutEntradaVirtual.Id_Emp, 
                                       AutEntradaVirtual.Id_Cd,
                                       AutEntradaVirtual.Folio1,
                                       AutEntradaVirtual.Folio2,
                                       AutEntradaVirtual.Fecha1,
                                       AutEntradaVirtual.Fecha2,
                                       AutEntradaVirtual.Estatus == "" ? (object)null: AutEntradaVirtual.Estatus,
                                       AutEntradaVirtual.Id_CteFiltro1,
                                       AutEntradaVirtual.Id_CteFiltro2
                                      // AutEntradaVirtual.Solicitud                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAutEntradaVirtual_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    AutEntradaVirtual = new AutEntradaVirtual();
                    AutEntradaVirtual.Id_Env = (int)dr.GetValue(dr.GetOrdinal("Id_Env"));
                    AutEntradaVirtual.Env_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Env_Fecha"));
                    AutEntradaVirtual.Env_Estatus = (string)dr.GetValue(dr.GetOrdinal("Env_Estatus"));
                    AutEntradaVirtual.Env_EstatusStr = Estatus((string)dr.GetValue(dr.GetOrdinal("Env_Estatus")));
                    AutEntradaVirtual.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    AutEntradaVirtual.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Env_CteNomComercial"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_Es")))
                    {
                        AutEntradaVirtual.Id_Es = (int)dr.GetValue(dr.GetOrdinal("Id_Es"));
                    }
                    //usar para columnas no requeridas: AutEntradaVirtual.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    List.Add(AutEntradaVirtual);
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

        public void ConsultaProAutEntradaVirtualVencido(ref int Vencido, int Id_Emp, int Id_Cd, int Id_Env, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Env"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Env
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAutEntradaVirtualVencido_Consulta", ref dr, Parametros, Valores);

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

        public void ConsultaProAutEntradaVirtual(ref EntradaVirtual ev, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Env_Unique",
                                        "@Id_Env"
                                      };
                object[] Valores = { 
                                       ev.Id_Emp, 
                                       ev.Id_Cd,
                                       ev.Env_Unique==""?(object)null: ev.Env_Unique,
                                       ev.Id_Env == 0? (object)null : ev.Id_Env
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    ev.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ev.Id_USolicita = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_USolicita")));                   
                    ev.Id_Env = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Env")).ToString());
                    ev.Env_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Env_Fecha")).ToString());
                    ev.Env_ComentariosSolicitante = dr.GetString(dr.GetOrdinal("Env_ComentariosSolicitante")).ToString();
                    ev.Env_ComentariosAutoriza = dr.GetString(dr.GetOrdinal("Env_ComentariosAutoriza")).ToString();
                    ev.Env_Credito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Env_Credito")));
                    ev.Env_Rentabilidad = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Env_Rentabilidad")));
                    ev.Env_ImporteFacturar = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Env_ImporteFacturar")));
                    ev.Id_Es = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es")));
                    ev.Id_Pvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    ev.Env_Estatus = dr.GetValue(dr.GetOrdinal("Env_Estatus")).ToString();


                   verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaProveedorSeleccionado(EntradaVirtual ape, string Conexion, ref int verificador, ref bool tieneProveedorNS)
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEntradaVirtualDetveedorSeleccionado_Consulta", ref dr, Parametros, Valores);

                int resultado = 0;

                if (dr.HasRows)
                {
                    dr.Read();
                    resultado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Result")));
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                tieneProveedorNS = resultado == 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarEntradaVirtual(EntradaVirtual ape, string Conexion, List<EntradaVirtualDet> List, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();

                SqlCommand sqlcmd = default(SqlCommand);
                /*
                string[] Parametros = { 
                    "@Id_Emp", 
                    "@Id_Cd", 
                    "@Id_Env" ,
                    "@Id_Prd",
                    "@Env_VolVta",
                    "@Env_PreVta",
                    "@Env_FecInicio",
                    "@Env_FecFin",
                    "@Env_PreEsp",
                    "@Env_FecApr",
                    "@Accion",
                    "@Env_Estatus"
                };

                object[] Valores;

                for (int x = 0; x < List.Count; x++)
                {
                    Valores = new object[] { 
                        ape.Id_Emp, 
                        ape.Id_Cd,
                        ape.Id_Env,
                        List[x].Id_Prd, 
                        List[x].Env_VolVta,
                        List[x].Env_PreVta,
                        List[x].Env_FecInicio,
                        List[x].Env_FecFin,
                        List[x].Env_PreEsp,
                        List[x].Env_FecAut,                       
                        List[x].Env_Estatus
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDet_Autorizar", ref verificador, Parametros, Valores);
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
                                        "@Id_Env",
                                        "@Env_NotaRes",
                                        "@Env_NumProveedor",
                                        "@Env_Convenio",
                                        "@Env_NumUsuario"
                                       
                                      };

                    Valores = new object[] { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd,
                                       ape.Id_Env,
                                       ape.Env_NotaResp,
                                       ape.Env_NumProveedor,
                                       ape.Env_Convenio,
                                       ape.Env_NumUsuario
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Autorizar", ref verificador, Parametros, Valores);

                }
                 */

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

        public void ConsultaProductoCliente(ref List<Clientes> List_cte, string Conexion, ref DateTime? Env_FecInicio, ref DateTime? Env_FecFin)
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
                        Env_FecInicio,
                        Env_FecFin,
                        List_cte[x].GenInt2
                    };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualCtePrd_Consulta", ref dr, Parametros, Valores);

                    if (dr.HasRows)
                    {
                        dr.Read();
                        List_cte[x].GenInt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Env")));
                        List_cte[x].GenBool = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ape")));
                        Env_FecInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Env_FecInicio")));
                        Env_FecFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Env_FecFin")));
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


        public void EntradaVirtualAgregarFolioEntrada(EntradaVirtual ape, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Env",
                                          "@Id_Es"
                                      };

                object[] Valores = { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd, 
                                       ape.Id_Env,
                                       ape.Id_Es

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_ModificarIdEs", ref  verificador,Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarEntradaSalida(EntradaSalida entradaSalida, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion, int Id_Env)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                int Id_Es = 0;
                string[] Parametros = { 
                                          "@Id_Emp",
	                                      "@Id_Cd",	
                                          "@Id_U",
	                                      "@Es_Naturaleza", 
	                                      "@Es_Fecha",
	                                      "@Id_Tm",
	                                      "@Id_Cte", 
	                                      "@Id_Pvd",
	                                      "@Es_Referencia",
	                                      "@Es_Notas",
	                                      "@Es_Subtotal",
	                                      "@Es_Iva",
	                                      "@Es_Total",
	                                      "@Es_Estatus",
                                          "@ManAut",
                                          "@Id_Ter"
                                          ,"@Es_CteCuentaNacional"
                                          ,"@Es_CteCuentaContNacional"
                                          ,"@Es_FechaReferencia"
                                      };
                object[] Valores = { 
                                        entradaSalida.Id_Emp,
                                        entradaSalida.Id_Cd,
                                        entradaSalida.Id_U,
                                        entradaSalida.Es_Naturaleza,
                                        entradaSalida.Es_Fecha,
                                        entradaSalida.Id_Tm,
                                        ((entradaSalida.Id_Cte==-1) ? (object)null: entradaSalida.Id_Cte),
                                        ((entradaSalida.Id_Pvd==-1) ? (object)null: entradaSalida.Id_Pvd), 
                                        entradaSalida.Es_Referencia,
                                        entradaSalida.Es_Notas,
                                        entradaSalida.Es_SubTotal,
                                        entradaSalida.Es_Iva,
                                        entradaSalida.Es_Total,
                                        entradaSalida.Es_Estatus
                                        ,1 //MANUAL
                                        ,entradaSalida.Id_Ter == -1 ? (object)null: entradaSalida.Id_Ter
                                        ,entradaSalida.Es_CteCuentaNacional
                                        ,entradaSalida.Es_CteCuentaContNacional
                                        ,entradaSalida.Es_FechaReferencia
                                   };
                //inserta una entradaSalida
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref Id_Es, Parametros, Valores);

                entradaSalida.Id_Es = Id_Es;

                EntradaVirtual evirtual = new EntradaVirtual();
                evirtual.Id_Emp = entradaSalida.Id_Emp;
                evirtual.Id_Cd = entradaSalida.Id_Cd;
                evirtual.Id_Env = Id_Env;
                evirtual.Id_Es = entradaSalida.Id_Es;

                int verificador = 0;
                this.EntradaVirtualAgregarFolioEntrada(evirtual, Conexion, ref verificador);

                Parametros = new string[]{
                                            "@Id_Emp",  
                                            "@Id_Cd",  
                                            "@Id_Es",  
                                            "@Id_EsDet",  
                                            "@EsDet_Naturaleza",  
                                            "@Id_Tm",
                                            "@Id_Ter",  
                                            "@Es_BuenEstado",  
                                            "@Id_Prd",  
                                            "@Es_Cantidad",  
                                            "@Es_Costo",  
                                            "@Afct_OrdCompra", 
                                            "@Afct_Precios", 
                                            "@Prd_AgrupadoSpo",  
                                            "@Id_Ref", 
                                            "@Id_Pvd", 
                                            "@Id_Cte", 
                                            "@Fecha"
		                                };
                int Id_EsDet = 0;
                foreach (EntradaSalidaDetalle detalle in listaDetalle)
                {
                    Valores = new object[]{
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                entradaSalida.Id_Es,
                                                Id_EsDet++,
                                                entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                entradaSalida.Id_Tm,
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afecta,
                                                strEmp != detalle.Id_Emp,
                                                detalle.Prd_AgrupadoSpo,
                                                entradaSalida.Es_Referencia
                                                ,entradaSalida.Id_Pvd
                                                ,entradaSalida.Id_Cte
                                                ,entradaSalida.Es_Fecha
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, Parametros, Valores);
                }



                Parametros = new string[]{
                                            "@Id_Emp",  
                                            "@Id_Cd",  
                                            "@Id_Env",  
                                            "@Id_Es",  
                                            "@Id_Tm",  
                                            "@Id_Prd",
                                            "@Cant",  
                                            "@Fecha",  
                                            "@Tipo"  
                                           
		                                };
                
                foreach (EntradaSalidaDetalle detalle in listaDetalle)
                {
                    Valores = new object[]{
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                Id_Env,
                                                entradaSalida.Id_Es,
                                                entradaSalida.Id_Tm,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                entradaSalida.Es_Fecha,
                                                entradaSalida.Es_Naturaleza                                               
                                                
                                                
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDetalleMov_Insertar", ref verificadorStr, Parametros, Valores);
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

      


        public void EnviarEntradaVirtual(EntradaVirtual ape, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Env",
                                          "@Env_Estatus"
                                      };

                object[] Valores = { 
                                       ape.Id_Emp, 
                                       ape.Id_Cd, 
                                       ape.Id_Env,
                                       ape.Env_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Enviar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEnvio(ref EntradaVirtual pe, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Env" 
                                          
                                      };

                object[] Valores = { 
                                       pe.Id_Emp, 
                                       pe.Id_Cd, 
                                       pe.Id_Env 
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Envio", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pe.Env_Unique = dr.GetValue(dr.GetOrdinal("Guid")).ToString();
                    pe.Env_Solicitar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_USolicita")));
                    
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(EntradaVirtual pe, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Env" 
                                      };

                object[] Valores = { 
                                       pe.Id_Emp, 
                                       pe.Id_Cd, 
                                       pe.Id_Env 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtual_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEntradaVirtualDetallemov(EntradaVirtual Ev, string Conexion, ref List<EntradaVirtualDetalleMov> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                       
                                        "@Id_Env"                                     
                                       
                                      };
                object[] Valores = { 
                                        Ev.Id_Emp,
                                        Ev.Id_Cd,                                        
                                        Ev.Id_Env
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualDetalleMov_Consulta", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    EntradaVirtualDetalleMov ev_prd = new EntradaVirtualDetalleMov();
                    ev_prd.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    ev_prd.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    ev_prd.Id_Env = (int)dr.GetValue(dr.GetOrdinal("Id_Env"));                    
                    ev_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    ev_prd.Id_Es = (int)dr.GetValue(dr.GetOrdinal("Id_Es"));
                    ev_prd.Id_Tm = (int)dr.GetValue(dr.GetOrdinal("Id_Tm"));
                    ev_prd.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha"));
                    ev_prd.Cant = (int)dr.GetValue(dr.GetOrdinal("cant"));
                    ev_prd.Tipo = (string)dr.GetValue(dr.GetOrdinal("tipo"));
                    List.Add(ev_prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEntradaVirtualSaldo(EntradaVirtual Ev, string Conexion, ref List<EntradaVirtualDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",                                       
                                        "@Id_Env"                                     
                                       
                                      };
                object[] Valores = { 
                                        Ev.Id_Emp,
                                        Ev.Id_Cd,                                        
                                        Ev.Id_Env
                                                                              
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProEntradaVirtualSaldo_Consulta", ref dr, Parametros, Valores);

                int a = 0;
                while (dr.Read())
                {
                    EntradaVirtualDet ev_prd = new EntradaVirtualDet();                   
                    ev_prd.Id_Env = (int)dr.GetValue(dr.GetOrdinal("Id_Env"));
                    ev_prd.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    ev_prd.Prd_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Prd_Descripcion"));
                    ev_prd.Env_Cantidad = (int)dr.GetValue(dr.GetOrdinal("Env_Cantidad"));
                    ev_prd.Env_Costo = (decimal)dr.GetValue(dr.GetOrdinal("Env_Costo")); 
                    ev_prd.Env_CantDevuelta = (int)dr.GetValue(dr.GetOrdinal("Env_CantDevuelta"));
                   
                    List.Add(ev_prd);
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
