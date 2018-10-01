using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
namespace CapaDatos
{
   public class CD_Convenio
    {
       public void ConsultaListaConvenios(Convenio conv, ref List<Convenio> ListUtil, ref List<Convenio> ListNoUtil, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = {
                                         "@TipoFiltro",
                                         "@Vencido",
                                         "@Id_Cat",
                                         "@Filtro",
                                         "@Id_Cd"};
               object [] Valores = {
                                       conv.Filtro_TipoFiltro,
                                       conv.Filtro_Vencido,
                                       conv.Filtro_Id_Cat,
                                       conv.Filtro_Valor,
                                       conv.Filtro_Id_Cd 
                                   };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecConvenio_ConsultaLista", ref dr, Parametros, Valores);

               Convenio c;
               while (dr.Read())
               {
                   c = new Convenio();
                   c.Id_PC = Convert.ToInt32 (dr["Id_PC"]);
                   c.PC_NoConvenio = dr["PC_NoConvenio"].ToString().Trim();
                   c.PC_Nombre = dr["PC_Nombre"].ToString().Trim();
                   c.Cat_DescCorta = dr["Cat_DescCorta"].ToString().Trim();
                   c.Estatus = dr["Estatus"].ToString().Trim();
                   c.PC_FechaCreo = Convert.ToDateTime (dr["PC_FechaCreo"]);
                   c.PCD_Usar = Convert.ToBoolean(dr["PCD_Usar"]);

                   if (Convert.ToInt32 (dr["Utilizado"]) == 0)
                   {
                       ListNoUtil.Add(c);
                   }
                   else 
                   {
                       ListUtil.Add(c);
                   }
               }

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConsecutivo(int Id_Cat, ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = {"@Id_Cat"};
               object[] Valores = {
                                      Id_Cat 
                                   };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatConvCategoria_ConsultaConscutivo", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   conv.Cat_Consecutivo = Convert.ToInt32(dr["Cat_Consecutivo"]);
                   conv.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarConvenio(Convenio conv, List<ConvenioDet> List, ref int Verificador,ref int Id_PC, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = {  "@Pc_NoConvenio",
                                        "@PC_Nombre",
                                        "@Id_Cat" ,
                                        "@Id_ULider",
                                        "@Id_UEjecutivo" ,
                                        "@PC_Margen",
                                        "@PC_Notas",
                                        "@PC_Consecutivo",
                                        "@Id_PCAnterior",
                                        "@Id_U"};
               object[] Valores = {
                                      conv.PC_NoConvenio,
                                      conv.PC_Nombre,
                                      conv.Id_Cat,
                                      conv.Id_ULider,
                                      conv.Id_UEjecutivo,
                                      conv.PC_Margen,
                                      conv.PC_Notas,
                                      conv.Cat_Consecutivo ,
                                      conv.Id_PCAnterior,
                                      conv.Id_U
                                  };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenio_Insertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

               if (Verificador > 0)
               {
                   Id_PC = Verificador;


                   string[] Parametros2 = { "@Id_Pc",
                                            "@Id_Prd",
                                            "@PCD_ClaveProv",
                                            "@PCD_PrecioVtaMin",
                                            "@PCD_PrecioVtaMax",
                                            "@PCD_PrecioAAAEsp",
                                            "@PCD_PrecioAAAEspA",
                                            "@PCD_PrecioAAAEspC",
                                            "@PCD_CantidadMax",
                                            "@Id_Moneda",
                                            "@PCD_FechaInicio",
                                            "@PCD_FechaFin",
                                            "@PCD_FechaInicioA",
                                            "@PCD_FechaFinA",
                                            "@PCD_FechaInicioC",
                                            "@PCD_FechaFinC",
                                            "@PCD_CatDesp" };
                   foreach (ConvenioDet  c in List)
                   {
                       CD_Datos cd_datos2 = new CD_Datos(Conexion);
                       SqlCommand sqlcmd2 = null;
                       object[] Valores2 = {
                                            Id_PC ,
                                           c.Id_Prd, 
                                           c.PCD_ClaveProv , 
                                           c.PCD_PrecioVtaMin ,
                                           c.PCD_PrecioVtaMax, 
                                           c.PCD_PrecioAAAEsp  == (double?)null ? (object) null: c.PCD_PrecioAAAEsp  ,
                                           c.PCD_PrecioAAAEspA == (double?)null ? (object) null: c.PCD_PrecioAAAEspA ,
                                           c.PCD_PrecioAAAEspC == (double?)null ? (object) null: c.PCD_PrecioAAAEspC ,
                                           c.PCD_CantidadMax, 
                                           c.Id_Moneda,
                                           c.PCD_FechaInicio == (DateTime?)  null ?(object) null : c.PCD_FechaInicio,
                                           c.PCD_FechaFin    == (DateTime?)  null ?(object) null : c.PCD_FechaFin,
                                           c.PCD_FechaInicioA == (DateTime?)  null ?(object) null : c.PCD_FechaInicioA,
                                           c.PCD_FechaFinA == (DateTime?)  null ?(object) null : c.PCD_FechaFinA,
                                           c.PCD_FechaInicioC == (DateTime?)  null ?(object) null : c.PCD_FechaInicioC,
                                           c.PCD_FechaFinC == (DateTime?)  null ?(object) null : c.PCD_FechaFinC,
                                           c.PCD_CatDesp };

                       sqlcmd2 = cd_datos2.GenerarSqlCommand("spProPrecioConvenioDet_Insertar", ref Verificador, Parametros2, Valores2);
                       cd_datos2.LimpiarSqlcommand(ref sqlcmd2);

                   }

 
               }

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void BajaConvenio(int Id_PC, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = { "Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_Baja", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConvenio(int Id_PC, ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;
               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenio_Consulta", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   conv.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                   conv.PC_Nombre = dr["Pc_Nombre"].ToString();
                   conv.Id_Cat = Convert.ToInt32(dr["Id_Cat"]);
                   conv.Id_ULider = Convert.ToInt32(dr["Id_ULider"]);
                   conv.Id_UEjecutivo = Convert.ToInt32(dr["Id_UEjecutivo"]);
                   conv.PC_Margen = Convert.ToDouble(dr["Pc_Margen"]);
                   conv.PC_Notas = dr["PC_Notas"].ToString();
                   conv.PC_FechaMod = Convert.ToDateTime(dr["PC_FechaMod"]);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaConvenioDet(int Id_PC, ref List<ConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;
               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenioDet_Consulta", ref dr, Parametros, Valores);

               ConvenioDet c;
               while (dr.Read())
               {
                   c = new ConvenioDet();
                   c.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                   c.PCD_ClaveProv = dr["PCD_ClaveProv"].ToString();
                   c.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                   c.PCD_PrecioVtaMin = Convert.ToDouble (dr["PCD_PrecioVtaMin"]);
                   c.PCD_PrecioVtaMax = Convert.ToDouble(dr["PCD_PrecioVtaMax"]);
                   //c.PCD_PrecioAAAEsp = Convert.ToDouble(dr["PCD_PrecioAAAEsp"]);
                   c.PCD_CantidadMax = Convert.ToInt32(dr["PCD_CantidadMax"]);
                   c.Id_Moneda = Convert.ToInt32(dr["Id_Moneda"]);
                   c.Id_MonedaStr = dr["Id_MonedaStr"].ToString();
                   //c.PCD_FechaInicio = Convert.ToDateTime(dr["PCD_FechaInicio"]);
                   //c.PCD_FechaFin = Convert.ToDateTime(dr["PCD_FechaFin"]);
                   c.PCD_CatDesp = dr["PCD_CatDesp"].ToString();
                   //Anterior:la fecha fin es menor a la fecha actual
                   c.PCD_PrecioAAAEspA = dr["PCD_PrecioAAAEspA"].ToString() == "" ? (Double?)null :  Convert.ToDouble(dr["PCD_PrecioAAAEspA"]) ;
                   c.PCD_FechaInicioA = dr["PCD_FechaInicioA"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaInicioA"]);
                   c.PCD_FechaFinA = dr["PCD_FechaFinA"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaFinA"]); 
                  
                   //Actual:La fecha inicio es menor a la fecha actual y la fecha fin es mayor a la fecha actual
                   c.PCD_PrecioAAAEsp = dr["PCD_PrecioAAAEsp"].ToString() == "" ? (Double?)null : Convert.ToDouble(dr["PCD_PrecioAAAEsp"]);
                   c.PCD_FechaInicio = dr["PCD_FechaInicio"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaInicio"]);
                   c.PCD_FechaFin = dr["PCD_FechaFin"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaFin"]); 
                  
                   //Futuro: La fecha inicio es mayor a la fecha actual 
                   c.PCD_PrecioAAAEspC = dr["PCD_PrecioAAAEspC"].ToString() == "" ? (Double?)null : Convert.ToDouble(dr["PCD_PrecioAAAEspC"]);
                   c.PCD_FechaInicioC = dr["PCD_FechaInicioC"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaInicioC"]);
                   c.PCD_FechaFinC = dr["PCD_FechaFinC"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["PCD_FechaFinC"]);

                   c.PCD_FechaFinVer = Convert.ToDateTime(dr["PCD_FechaFinVer"]);

                   List.Add(c);

                 
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ModificaConvenio(Convenio conv, ref int Verificador, string Conexion)
       {
           try
           {
                 CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = {  "@Id_PC",
                                        "@PC_Nombre",
                                        "@Id_ULider",
                                        "@Id_UEjecutivo",
                                        "@PC_Margen",
                                        "@PC_Notas",
                                        "@Id_U"};

               object[] Valores = { conv.Id_PC ,
                                    conv.PC_Nombre ,
                                    conv.Id_ULider,
                                    conv.Id_UEjecutivo,
                                    conv.PC_Margen,
                                    conv.PC_Notas,
                                    conv.Id_U};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenio_Modificar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarConvenioDet(int Id_PC, List<ConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {
               string[] Parametros = {     "@Id_Pc",
                                            "@Id_Prd",
                                            "@PCD_ClaveProv",
                                            "@PCD_PrecioVtaMin",
                                            "@PCD_PrecioVtaMax",
                                            "@PCD_PrecioAAAEsp",
                                            "@PCD_PrecioAAAEspA",
                                            "@PCD_PrecioAAAEspC",
                                            "@PCD_CantidadMax",
                                            "@Id_Moneda",
                                            "@PCD_FechaInicio",
                                            "@PCD_FechaFin",
                                            "@PCD_FechaInicioA",
                                            "@PCD_FechaFinA",
                                            "@PCD_FechaInicioC",
                                            "@PCD_FechaFinC",
                                            "@PCD_CatDesp" };
                   foreach (ConvenioDet  c in List)
                   {
                       CD_Datos cd_datos = new CD_Datos(Conexion);
                       SqlCommand sqlcmd = null;
                       object[] Valores = {
                                           Id_PC ,
                                           c.Id_Prd, 
                                           c.PCD_ClaveProv , 
                                           c.PCD_PrecioVtaMin ,
                                           c.PCD_PrecioVtaMax, 
                                           c.PCD_PrecioAAAEsp  == (double?)null ? (object) null: c.PCD_PrecioAAAEsp  ,
                                           c.PCD_PrecioAAAEspA == (double?)null ? (object) null: c.PCD_PrecioAAAEspA ,
                                           c.PCD_PrecioAAAEspC == (double?)null ? (object) null: c.PCD_PrecioAAAEspC ,
                                           c.PCD_CantidadMax, 
                                           c.Id_Moneda,
                                           c.PCD_FechaInicio == (DateTime?)  null ?(object) null : c.PCD_FechaInicio,
                                           c.PCD_FechaFin    == (DateTime?)  null ?(object) null : c.PCD_FechaFin,
                                           c.PCD_FechaInicioA == (DateTime?)  null ?(object) null : c.PCD_FechaInicioA,
                                           c.PCD_FechaFinA == (DateTime?)  null ?(object) null : c.PCD_FechaFinA,
                                           c.PCD_FechaInicioC == (DateTime?)  null ?(object) null : c.PCD_FechaInicioC,
                                           c.PCD_FechaFinC == (DateTime?)  null ?(object) null : c.PCD_FechaFinC,
                                           c.PCD_CatDesp };

                       sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenioDet_Insertar", ref Verificador, Parametros, Valores);
                       cd_datos.LimpiarSqlcommand(ref sqlcmd);

                   }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaProConvSucursal(int Id_PC, ref List<Convenio> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;
               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpProConvSucursal_Consulta", ref dr, Parametros, Valores);

               Convenio conv;
               while (dr.Read())
               {
                   conv = new Convenio();
                   conv.Id_PC = Id_PC;
                   conv.Id_Tipo = Convert.ToInt32(dr["Id_Tipo"]);
                   conv.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                   conv.Cd_Nombre = dr["Cd_Nombre"].ToString();
                   conv.PCD_Ver = Convert.ToBoolean(dr["PCD_Ver"]);
                   conv.PCD_Usar = Convert.ToBoolean(dr["PCD_Usar"]);
                   List.Add(conv);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void InsertarProConvSucursal(List<Convenio> List, ref int Verificador, string Conexion)
       {
           try
           {
     
               string[] Parametros = {  "@Id_PC",
                                        "@Id_Cd",
                                        "@PCD_Ver",
                                        "@PCD_Usar"
                                     };
               foreach (Convenio c in List)
               {
                   CD_Datos cd_datos = new CD_Datos(Conexion);
                   SqlCommand sqlcmd = null;
                   object[] Valores = {
                                          c.Id_PC ,
                                          c.Id_Cd ,
                                          c.PCD_Ver,
                                          c.PCD_Usar 
                                          };

                   sqlcmd = cd_datos.GenerarSqlCommand("spProConvSucursal_Insertar", ref Verificador, Parametros, Valores);
                   cd_datos.LimpiarSqlcommand(ref sqlcmd);

               }


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
           
       }
       public void ConsultaUsuariosEspeciales(ref Convenio conv, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProcPrecioConv_UsuEspecial_Consulta", ref dr);

               if (dr.Read())
               {
                   conv.Pue_Admin1 = Convert.ToInt32(dr["Pue_Admin1"]);
                   conv.Pue_Admin2 = Convert.ToInt32(dr["Pue_Admin2"]);
                   conv.Pue_CteMacola = Convert.ToInt32(dr["Pue_CteMacola"]);
                   conv.Pue_CteIntranet = Convert.ToInt32(dr["Pue_CteIntranet"]);
                   conv.Pue_EqComodato  = Convert.ToInt32(dr["Pue_EqComodato"]);
                   conv.Pue_VerTodo = Convert.ToInt32(dr["Pue_VerTodo"]);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ModificaUsuariosEspeciales(Convenio conv,ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               string[] Parametros = {  "@Id_Cd",
                                        "@Pue_Admin1",
                                        "@Pue_Admin2",
                                        "@Pue_CteMacola",
                                        "@Pue_CteIntranet",
                                        "@Pue_EqComodato",
                                        "@Pue_VerTodo"};
               object[] Valores = {
                                      conv.Id_Cd ,
                                      conv.Pue_Admin1 ,
                                      conv.Pue_Admin2 ,
                                      conv.Pue_CteMacola ,
                                      conv.Pue_CteIntranet ,
                                      conv.Pue_EqComodato ,
                                      conv.Pue_VerTodo
                                      
                                  };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_UsuEspecial_Modificar", ref Verificador , Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConsultaPermisosSucursal(int Id_U, ref List<Convenio> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;
               string[] Parametros = { "@Id_U" };
               object[] Valores = { Id_U };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatUsuarioCDI_Consulta", ref dr, Parametros, Valores);

               Convenio conv;
               while (dr.Read())
               {
                   conv = new Convenio();
                   conv.Id_U = Id_U;
                   conv.Id_Tipo = Convert.ToInt32(dr["Id_Tipo"]);
                   conv.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                   conv.Cd_Nombre = dr["Cd_Nombre"].ToString();
                   conv.Seleccionado = Convert.ToBoolean(dr["Seleccionado"]);
                   List.Add(conv);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void InsertarPermisosSucursal(List<Convenio> List, ref int Verificador, string Conexion)
       {
           try
           {

               string[] Parametros = {  "@Id_Cd",
                                        "@Id_U",
                                        "@Seleccionado"
                                     };
               foreach (Convenio c in List)
               {
                   CD_Datos cd_datos = new CD_Datos(Conexion);
                   SqlCommand sqlcmd = null;
                   object[] Valores = {
                                          c.Id_Cd ,
                                          c.Id_U ,
                                          c.Seleccionado
                                          };

                   sqlcmd = cd_datos.GenerarSqlCommand("spCatUsuarioCDI_Insertar", ref Verificador, Parametros, Valores);
                   cd_datos.LimpiarSqlcommand(ref sqlcmd);

               }


           }
           catch (Exception ex)
           {

               throw ex;
           }

       }
       public void ConsultaConsecutivoSolicitud(ref int Id_Sol, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_Solicitud_Consecutivo", ref dr);

               if (dr.Read())
               {
                   Id_Sol = Convert.ToInt32(dr["Id_Sol"]);
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConsultaCapturaUsuario(string Cat_DescCorta, ref string Cat_CapturaUsuario, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;
               string[] Parametros = { "@Cat_DescCorta" };
               object[] Valores = { Cat_DescCorta };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatConvCategoria_CapturaUsuario", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   Cat_CapturaUsuario = dr["Cat_CapturaUsuario"].ToString().Trim();
               }
               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_Insertar(SolConvenio sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               string[] Parametros = {  "@Id_Cd",
                                        "@Id_PC",
                                        "@Id_U",
                                        "@Sol_UNombre",
                                        "@Sol_UCorreo"};
               object[] Valores = {
                                      sol.Id_Cd ,
                                      sol.Id_PC,
                                      sol.Id_U,
                                      sol.Sol_UNombre ,
                                      sol.Sol_UCorreo
                                  };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_Solicitud_Insertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_InsertarDet(int Id_Sol,SolConvenio sol,List<SolConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {
               string[] Parametros = {  "@Id_Sol",
                                        "@Id_PC",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Sol_CteNombre",
                                        "@Sol_UsuFinal",
                                        "@Id_Ter",
                                         "@SolTer_Nombre"};

               foreach (SolConvenioDet s in List)
               {
                   CD_Datos cd_datos = new CD_Datos(Conexion);
                   SqlCommand sqlcmd = null;
                   object[] Valores = {
                                            Id_Sol,
                                            sol.Id_PC ,
                                            sol.Id_Cd,
                                            s.Id_Cte,
                                            s.Sol_CteNombre ,
                                            s.Sol_UsuFinal,
                                            s.Id_Ter ,
                                            s.SolTer_Nombre
                                      };


                   sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_Solicitud_InsertarDet", ref Verificador, Parametros, Valores);
                   cd_datos.LimpiarSqlcommand(ref sqlcmd);

               }

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSolicitud_Consulta(int Id_Sol, ref SolConvenio sol, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = { "@Id_Sol" };
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudConsulta", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   sol.CD_Nombre = dr["Cd_Nombre"].ToString();
                   sol.Sol_UNombre = dr["Sol_UNombre"].ToString();
                   sol.Sol_UCorreo = dr["Sol_UCorreo"].ToString();
                   sol.Sol_Fecha = Convert.ToDateTime(dr["Sol_Fecha"]);
                   sol.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                   sol.PC_Nombre = dr["PC_Nombre"].ToString();
                   sol.Cat_DescCorta = dr["Cat_DescCorta"].ToString().Trim(); 

               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
          
       }
       public void ConvenioSolicitud_ConsultaAt(string Sol_Unique, ref SolConvenio sol, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = { "@Sol_Unique" };
               object[] Valores = { Sol_Unique };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudConsultaAt", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   sol.Id_Sol = Convert.ToInt32(dr["Id_Sol"]);
                   sol.CD_Nombre = dr["Cd_Nombre"].ToString();
                   sol.Sol_UNombre = dr["Sol_UNombre"].ToString();
                   sol.Sol_UCorreo = dr["Sol_UCorreo"].ToString();
                   sol.Sol_Estatus = dr["Sol_Estatus"].ToString();
                   sol.Sol_Fecha = Convert.ToDateTime(dr["Sol_Fecha"]);
                   sol.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                   sol.PC_Nombre = dr["PC_Nombre"].ToString();
                   sol.Cat_DescCorta = dr["Cat_DescCorta"].ToString().Trim();
                

               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }

       }
       public void ConvenioSolicitud_ConsultaDet(int Id_Sol, ref  List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = { "@Id_Sol" };
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudConsultaDet", ref dr, Parametros, Valores);

               SolConvenioDet sol;
               while  (dr.Read())
               {
                   sol = new SolConvenioDet();
                   sol.Id_Unique = Guid.NewGuid().ToString() ;
                   sol.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                   sol.Sol_CteNombre = dr["Sol_CteNombre"].ToString();
                   sol.Sol_UsuFinal = dr["Sol_UsuFinal"].ToString();
                   sol.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                   sol.SolTer_Nombre = dr["SolTer_Nombre"].ToString();
                   List.Add(sol);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }

       }
       public void ConvenioSolicitud_Cancelar(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               string[] Parametros = {"@Id_Sol"};
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_Solicitud_Cancelar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_Modificar(SolConvenio sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = {  "@Id_Sol",
                                        "@Sol_IdUCreo",
                                        "@Sol_UNombre" ,
                                        "@Sol_UCorreo" };
               object[] Valores = {   sol.Id_Sol,
                                      sol.Id_U,
                                      sol.Sol_UNombre,
                                      sol.Sol_UCorreo };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_Solicitud_Modificar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioSolicitud_ConsultaDetAt(int Id_Sol, ref  List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = { "@Id_Sol" };
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudConsultaDetAt", ref dr, Parametros, Valores);

               SolConvenioDet sol;
               while (dr.Read())
               {
                   sol = new SolConvenioDet();
                   sol.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                   sol.Sol_CteNombre = dr["Sol_CteNombre"].ToString();
                   sol.Sol_UsuFinal = dr["Sol_UsuFinal"].ToString();
                   sol.SolD_Estatus = dr["SolD_Estatus"].ToString();
                   sol.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                   sol.SolTer_Nombre = dr["SolTer_Nombre"].ToString();
                   List.Add(sol);
               }

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }

       }
       public void ConvenioSolicitud_Atender(int Id_Sol, List<SolConvenioDet> List, ref int Verificador, string Conexion)
       {
           try
           {

               string[] Parametros = {  "@Id_Sol",
                                        "@Id_Cte",
                                        "@SolD_Estatus"
                                     };
               foreach (SolConvenioDet s in List)
               {
                   CD_Datos cd_datos = new CD_Datos(Conexion);

                   object[] Valores = {s.Id_Sol ,
                                       s.Id_Cte ,
                                      s.SolD_Estatus };

                   SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_SolicitudDetAtender", ref Verificador, Parametros, Valores);

                   cd_datos.LimpiarSqlcommand(ref sqlcmd);
 
               }

               if (Verificador == -1)
               {
                   string[] Parametros2 = { "@Id_Sol" };
                   object[] Valores2 = { Id_Sol };

                   CD_Datos cd_datos2 = new CD_Datos(Conexion);

                   SqlCommand sqlcmd2 = cd_datos2.GenerarSqlCommand("spProPrecioConv_SolicitudAtender", ref Verificador, Parametros2, Valores2);

                   cd_datos2.LimpiarSqlcommand(ref sqlcmd2); 
               }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenio_ConsultaVinculados(int Id_PC, int Id_CD, ref List<SolConvenioDet> List, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = {"@Id_PC",
                                       "@Id_Cd"};
               object[] Valores = {Id_PC,
                                   Id_CD};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_ConsultaClientes", ref dr, Parametros, Valores);

               SolConvenioDet s;

               while (dr.Read())
               {
                   s = new SolConvenioDet();
                   s.Id_Sol = Convert.ToInt32(dr["Id_Sol"]);
                   s.Id_PC = Convert.ToInt32(dr["Id_PC"]);
                   s.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                   s.Sol_CteNombre = dr["Sol_CteNombre"].ToString();
                   s.Sol_UsuFinal = dr["Sol_UsuFinal"].ToString();
                   s.CDI = dr["CDI"].ToString();
                   s.Sol_UNombre = dr["Sol_UNombre"].ToString();
                   s.SolTer_Nombre = dr["SolTer_Nombre"].ToString();
                   List.Add(s);
               }

               dr.Close();
              
               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenio_DesvinculaUno (SolConvenioDet sol , ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = { "@Id_Sol",
                                       "@Id_PC",
                                       "@Id_Cte" };
               object[] Valores = {sol.Id_Sol, 
                                   sol.Id_PC ,
                                  sol.Id_Cte };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_DesvinculaUno", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }

       }
       public void ConvenioSustituyo_ActualizaClientes(int Id_PC, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = {"Id_PC" };
               object[] Valores = { Id_PC};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenio_SustituyeClientes", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenion_ConsultaVinculadosTodos(int Id_Cd, ref DataTable dt, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               DataSet ds = null;

               string[] Parametros = { "@Id_Cd" };
               object[] Valores = { Id_Cd };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConv_ConsultaClientesTodos", ref ds, Parametros, Valores);

               dt = ds.Tables[0];

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void Convenio_ConsultaListaDet(Convenio conv, ref DataTable dt, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               DataSet ds = null;
               string[] Parametros = {
                                         "@TipoFiltro",
                                         "@Vencido",
                                         "@Id_Cat",
                                         "@Filtro",
                                         "@Id_Cd"};
               object [] Valores = {
                                       conv.Filtro_TipoFiltro,
                                       conv.Filtro_Vencido,
                                       conv.Filtro_Id_Cat,
                                       conv.Filtro_Valor,
                                       conv.Filtro_Id_Cd 
                                   };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecConvenio_ConsultaListaDet", ref ds, Parametros, Valores);

               dt = ds.Tables[0];

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       public void Convenio_ConsultaPrecio(ConvenioDet conv, ref ConvenioDet convdet, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = {
                                        "@Id_Emp",  
                                        "@Id_Cd" ,
                                        "@Id_Cte" ,
                                        "@Id_Prd",
                                     };
               object[] Valores = {
                                      conv.Id_Emp,
                                      conv.Id_Cd,
                                      conv.Id_Cte,
                                      conv.Id_Prd
                                  };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProPrecioConvenio_ConsultaPrecio", ref dr, Parametros, Valores);

               while (dr.Read())
               {
                   convdet.Id_PC = Convert.ToInt32(dr["Id_PC"]);
                   convdet.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                   convdet.PC_NoConvenio = dr["PC_NoConvenio"].ToString();
                   convdet.PC_Nombre = dr["PC_Nombre"].ToString();
                   convdet.PCD_PrecioAAAEsp = Convert.ToDouble(dr["PCD_PrecioAAAEsp"]);
                   convdet.PCD_PrecioVtaMin = Convert.ToDouble(dr["PCD_PrecioVtaMin"]);
                   convdet.PCD_PrecioVtaMax = Convert.ToDouble(dr["PCD_PrecioVtaMax"]);
                   convdet.PCD_FechaInicio = Convert.ToDateTime(dr["PCD_FechaInicio"]);
                   convdet.PCD_FechaFin = Convert.ToDateTime(dr["PCD_FechaFin"]);
                   convdet.Id_Moneda = Convert.ToInt32(dr["Id_Moneda"]);
               }

               dr.Close();

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       #region Correos
       public void ConvenioCreo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               int Verificador = 0;

               string[] Parametros = { "@Id_PC" };
               object[] Valores = {Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_CorreoCreo",ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConvenioModifico_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               int Verificador = 0;

               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_CorreoModifico", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSustituyo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               int Verificador = 0;

               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_CorreoSustituyo", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioCancelo_EnviarCorreo(int Id_PC, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               int Verificador = 0;

               string[] Parametros = { "@Id_PC" };
               object[] Valores = { Id_PC };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_CorreoCanceloConv", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSolicitud_EnviarCorreoCreoSol(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               string[] Parametros = { "@Id_Sol" };
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConv_CorreoCreoSolicitud", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ConvenioSolicitud_EnviarCorreoAtendio(int Id_Sol, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               string[] Parametros = { "@Id_Sol" };
               object[] Valores = { Id_Sol };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConv_CorreoAtendioSolicitud", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {

               throw ex;
           }

       }
       public void ConvenioDesvinculo_EnviarCorreo(SolConvenioDet sol, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               int Verificador = 0;
               string[] Parametros = {  "@Id_Sol",
                                        "@Id_PC",
                                        "@Id_Cte",
                                       "@U_Nombre"};
               object[] Valores = {sol.Id_Sol ,
                                   sol.Id_PC ,
                                  sol.Id_Cte ,
                                  sol.U_Nombre};
               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spPrecioConvenio_CorreoCancelo", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       #endregion



      
    }
}
