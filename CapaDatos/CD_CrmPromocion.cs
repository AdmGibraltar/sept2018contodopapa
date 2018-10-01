using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmPromocion
    {
        public void ComboCds(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 2, sesion.Id_Emp, sesion.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboRik(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { 1, sesion.Id_Emp, cds, sesion.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboUen(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { 1, sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Combo", ref dr, Parametros, Valores);
                //spCatCRMUen_Combo
                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int uen, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, uen };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentosUen_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, segmento };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAreaSegmento_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSolucion(Sesion sesion, int area, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Area" };
                object[] Valores = { sesion.Id_Emp, area };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Sol" };
                object[] Valores = { sesion.Id_Emp, solucion };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatAplicacion_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatPromocion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",                                   
                                         "@Id_Ter",
                                         "@Id_Seg",
                                         "@Id_Uen",
                                         "@Id_Area",
                                         "@Id_Sol",
                                         "@Id_U",
                                         "@Id_Apl",
                                         "@Estatus",
                                         "@Clientes",
                                         "@Id_Rik"
                                        
                                      };
                object[] Valores = { sesion.Id_Emp, 
                                     promocion.Cds,
                                     promocion.Territorio == - 1 ? (int?)null : promocion.Territorio,
                                     promocion.Segmento== - 1 ? (int?)null : promocion.Segmento,
                                     promocion.Uen== - 1 ? (int?)null : promocion.Uen,
                                     promocion.Area== - 1 ? (int?)null : promocion.Area,
                                     promocion.Solucion== - 1 ? (int?)null : promocion.Solucion,
                                     sesion.Id_U,
                                     promocion.Aplicacion== - 1 ? (int?)null : promocion.Aplicacion,
                                     promocion.Estatus,
                                     promocion.Cliente == 0 ? (int?)null: promocion.Cliente,
                                     promocion.Id_Rik == "-1" ? (object)null : promocion.Id_Rik, 
                                     
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Consulta", ref dr, Parametros, Valores);
                int Avances;
                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    Avances = 0;
                    catPromociones = new CrmPromociones();
                    catPromociones.Ids = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.Cds = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    catPromociones.Representante = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    catPromociones.NombreCte = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    catPromociones.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catPromociones.Segmento = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    catPromociones.Cli_VPObservado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cli_VPObservado"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cli_VPObservado")));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    catPromociones.Analisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Analisis"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Analisis"))).ToString("dd/MM/yyyy");
                    catPromociones.Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Presentacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Negociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Negociacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Cierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cierre"))).ToString("dd/MM/yyyy");
                    catPromociones.Cancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? " " : (string)dr.GetValue(dr.GetOrdinal("Cancelacion"));
                    catPromociones.FechaCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))) ? " " : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Avances = (int)dr.GetValue(dr.GetOrdinal("Avances"));
                    catPromociones.Estatus = (int)dr.GetValue(dr.GetOrdinal("Estatus"));

                    Funciones funcion = new Funciones();
                    int mes_Actual = funcion.GetLocalDateTime(sesion.Minutos).Month;
                    int año_Actual = funcion.GetLocalDateTime(sesion.Minutos).Year;

                    //Analisis
                    if (catPromociones.Analisis != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Presentacion
                    if (catPromociones.Presentacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Negociacion
                    if (catPromociones.Negociacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Cierre
                    if (catPromociones.Cierre != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }                  
                    catPromociones.MesModificacion = Avances == 0 ? "--" : Avances.ToString();

                    catPromociones.VentaMensual = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VentaMensual"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaMensual")));
                    object idCrmProspectoObj=null;
                    int idCrmProspectoPos=dr.GetOrdinal("Id_CrmProspecto");
                    idCrmProspectoObj=dr.GetValue(idCrmProspectoPos);
                    catPromociones.Id_CrmProspecto = 0;
                    if (!Convert.IsDBNull(idCrmProspectoObj))
                    {
                        catPromociones.Id_CrmProspecto = Convert.ToInt16(idCrmProspectoObj);
                    }

                    try
                    {
                        catPromociones.VentaNoRepetitiva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Id_CrmProspecto")));
                    }
                    catch (Exception ex)
                    {
                        catPromociones.VentaNoRepetitiva = null;
                    }

                    object objId_Uen = dr.GetValue(dr.GetOrdinal("id_uen"));
                    catPromociones.Id_Uen = 0;
                    if (!Convert.IsDBNull(objId_Uen))
                    {
                        catPromociones.Id_Uen = (int)objId_Uen;
                    }
                    
                    object objCrmOp_EnValuacion=dr.GetValue(dr.GetOrdinal("CrmOp_EnValuacion"));
                    catPromociones.EnValuacion = false;
                    if (!Convert.IsDBNull(objCrmOp_EnValuacion))
                    {
                        catPromociones.EnValuacion = (bool)objCrmOp_EnValuacion;
                    }

                    object objCliente = dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.Cliente = 0;
                    if (!Convert.IsDBNull(objCliente))
                    {
                        catPromociones.Cliente = (int)objCliente;
                    }

                    object objId_Area = dr.GetValue(dr.GetOrdinal("ID_Area"));
                    catPromociones.Area = 0;
                    if (!Convert.IsDBNull(objId_Area))
                    {
                        catPromociones.Area = (int)objId_Area;
                    }

                    object objId_Sol = dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    catPromociones.Solucion = 0;
                    if (!Convert.IsDBNull(objId_Sol))
                    {
                        catPromociones.Solucion = (int)objId_Sol;
                    }

                    object objSeg_Descripcion = dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    catPromociones.Seg_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objSeg_Descripcion))
                    {
                        catPromociones.Seg_Descripcion = (string)objSeg_Descripcion;
                    }

                    object objArea_Descripcion = dr.GetValue(dr.GetOrdinal("Area_Descripcion"));
                    catPromociones.Area_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objArea_Descripcion))
                    {
                        catPromociones.Area_Descripcion = (string)objArea_Descripcion;
                    }

                    object objSol_Descripcion = dr.GetValue(dr.GetOrdinal("Sol_Descripcion"));
                    catPromociones.Sol_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objSol_Descripcion))
                    {
                        catPromociones.Sol_Descripcion = (string)objSol_Descripcion;
                    }

                    object objDim_Id_Uen = dr.GetValue(dr.GetOrdinal("Dim_Id_Uen"));
                    catPromociones.Dim_Id_Uen = 0;
                    if (!Convert.IsDBNull(objDim_Id_Uen))
                    {
                        catPromociones.Dim_Id_Uen = (int)objDim_Id_Uen;
                    }

                    object objDim_Id_Seg = dr.GetValue(dr.GetOrdinal("Dim_Id_Seg"));
                    catPromociones.Dim_Id_Seg = 0;
                    if (!Convert.IsDBNull(objDim_Id_Seg))
                    {
                        catPromociones.Dim_Id_Seg = (int)objDim_Id_Seg;
                    }

                    object objDim_Cantidad = dr.GetValue(dr.GetOrdinal("Dim_Cantidad"));
                    catPromociones.Dim_Cantidad = 0;
                    if (!Convert.IsDBNull(objDim_Cantidad))
                    {
                        catPromociones.Dim_Cantidad = (decimal)objDim_Cantidad;
                    }

                    object objDim_Descripcion = dr.GetValue(dr.GetOrdinal("Dim_Descripcion"));
                    catPromociones.Dim_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objDim_Descripcion))
                    {
                        catPromociones.Dim_Descripcion = (string)objDim_Descripcion;
                    }

                    object objValorPotencialTeorico = dr.GetValue(dr.GetOrdinal("CrmOp_ValorPotencialTeorico"));
                    catPromociones.ValorPotencialTeorico = 0.0f;
                    if (!Convert.IsDBNull(objValorPotencialTeorico))
                    {
                        catPromociones.ValorPotencialTeorico = (double)objValorPotencialTeorico;
                    }

                    object objVPM = dr.GetValue(dr.GetOrdinal("CrmOp_VPM"));
                    catPromociones.VentaPromedioMensualEsperada = 0.0M;
                    if (!Convert.IsDBNull(objVPM))
                    {
                        catPromociones.VentaPromedioMensualEsperada = (decimal)objVPM;
                    }

                    object objUenDescrip = dr.GetValue(dr.GetOrdinal("Uen_Descrip"));
                    if (!Convert.IsDBNull(objUenDescrip))
                    {
                        catPromociones.Uen_Descrip = (string)objUenDescrip;
                    }

                    object objTer_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    if (!Convert.IsDBNull(objTer_Nombre))
                    {
                        catPromociones.Ter_Nombre = (string)objTer_Nombre;
                    }

                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión a la fuente de datos
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="promocion"></param>
        /// <param name="List"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void ConsultaCatPromocion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> List, ICD_Contexto icdCtx)
        {
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",                                   
                                         "@Id_Ter",
                                         "@Id_Seg",
                                         "@Id_Uen",
                                         "@Id_Area",
                                         "@Id_Sol",
                                         "@Id_U",
                                         "@Id_Apl",
                                         "@Estatus",
                                         "@Clientes",
                                         "@Id_Rik"
                                        
                                      };
                object[] Valores = { sesion.Id_Emp, 
                                     promocion.Cds,
                                     promocion.Territorio == - 1 ? (int?)null : promocion.Territorio,
                                     promocion.Segmento== - 1 ? (int?)null : promocion.Segmento,
                                     promocion.Uen== - 1 ? (int?)null : promocion.Uen,
                                     promocion.Area== - 1 ? (int?)null : promocion.Area,
                                     promocion.Solucion== - 1 ? (int?)null : promocion.Solucion,
                                     sesion.Id_U,
                                     promocion.Aplicacion== - 1 ? (int?)null : promocion.Aplicacion,
                                     promocion.Estatus,
                                     promocion.Cliente == 0 ? (int?)null: promocion.Cliente,
                                     promocion.Id_Rik == "-1" ? (object)null : promocion.Id_Rik, 
                                     
                                   };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spCatCrmPromocion_Consulta", ref dr, Parametros, Valores, icdCtx);
                int Avances;
                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    Avances = 0;
                    catPromociones = new CrmPromociones();
                    catPromociones.Ids = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.Cds = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    catPromociones.Representante = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    catPromociones.NombreCte = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    catPromociones.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catPromociones.Segmento = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    catPromociones.Cli_VPObservado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cli_VPObservado"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cli_VPObservado")));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    catPromociones.Analisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Analisis"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Analisis"))).ToString("dd/MM/yyyy");
                    catPromociones.Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Presentacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Negociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Negociacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Cierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cierre"))).ToString("dd/MM/yyyy");
                    catPromociones.Cancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? " " : (string)dr.GetValue(dr.GetOrdinal("Cancelacion"));
                    catPromociones.FechaCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))) ? " " : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Avances = (int)dr.GetValue(dr.GetOrdinal("Avances"));
                    catPromociones.Estatus = (int)dr.GetValue(dr.GetOrdinal("Estatus"));

                    Funciones funcion = new Funciones();
                    int mes_Actual = funcion.GetLocalDateTime(sesion.Minutos).Month;
                    int año_Actual = funcion.GetLocalDateTime(sesion.Minutos).Year;

                    //Analisis
                    if (catPromociones.Analisis != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Presentacion
                    if (catPromociones.Presentacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Negociacion
                    if (catPromociones.Negociacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Cierre
                    if (catPromociones.Cierre != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    catPromociones.MesModificacion = Avances == 0 ? "--" : Avances.ToString();

                    catPromociones.VentaMensual = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VentaMensual"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaMensual")));
                    object idCrmProspectoObj = null;
                    int idCrmProspectoPos = dr.GetOrdinal("Id_CrmProspecto");
                    idCrmProspectoObj = dr.GetValue(idCrmProspectoPos);
                    catPromociones.Id_CrmProspecto = 0;
                    if (!Convert.IsDBNull(idCrmProspectoObj))
                    {
                        catPromociones.Id_CrmProspecto = Convert.ToInt16(idCrmProspectoObj);
                    }

                    try
                    {
                        catPromociones.VentaNoRepetitiva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Id_CrmProspecto")));
                    }
                    catch (Exception ex)
                    {
                        catPromociones.VentaNoRepetitiva = null;
                    }

                    object objId_Uen = dr.GetValue(dr.GetOrdinal("id_uen"));
                    catPromociones.Id_Uen = 0;
                    if (!Convert.IsDBNull(objId_Uen))
                    {
                        catPromociones.Id_Uen = (int)objId_Uen;
                    }

                    object objCrmOp_EnValuacion = dr.GetValue(dr.GetOrdinal("CrmOp_EnValuacion"));
                    catPromociones.EnValuacion = false;
                    if (!Convert.IsDBNull(objCrmOp_EnValuacion))
                    {
                        catPromociones.EnValuacion = (bool)objCrmOp_EnValuacion;
                    }

                    object objCliente = dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.Cliente = 0;
                    if (!Convert.IsDBNull(objCliente))
                    {
                        catPromociones.Cliente = (int)objCliente;
                    }

                    object objId_Area = dr.GetValue(dr.GetOrdinal("ID_Area"));
                    catPromociones.Area = 0;
                    if (!Convert.IsDBNull(objId_Area))
                    {
                        catPromociones.Area = (int)objId_Area;
                    }

                    object objId_Sol = dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    catPromociones.Solucion = 0;
                    if (!Convert.IsDBNull(objId_Sol))
                    {
                        catPromociones.Solucion = (int)objId_Sol;
                    }

                    object objSeg_Descripcion = dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    catPromociones.Seg_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objSeg_Descripcion))
                    {
                        catPromociones.Seg_Descripcion = (string)objSeg_Descripcion;
                    }

                    object objArea_Descripcion = dr.GetValue(dr.GetOrdinal("Area_Descripcion"));
                    catPromociones.Area_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objArea_Descripcion))
                    {
                        catPromociones.Area_Descripcion = (string)objArea_Descripcion;
                    }

                    object objSol_Descripcion = dr.GetValue(dr.GetOrdinal("Sol_Descripcion"));
                    catPromociones.Sol_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objSol_Descripcion))
                    {
                        catPromociones.Sol_Descripcion = (string)objSol_Descripcion;
                    }

                    object objDim_Id_Uen = dr.GetValue(dr.GetOrdinal("Dim_Id_Uen"));
                    catPromociones.Dim_Id_Uen = 0;
                    if (!Convert.IsDBNull(objDim_Id_Uen))
                    {
                        catPromociones.Dim_Id_Uen = (int)objDim_Id_Uen;
                    }

                    object objDim_Id_Seg = dr.GetValue(dr.GetOrdinal("Dim_Id_Seg"));
                    catPromociones.Dim_Id_Seg = 0;
                    if (!Convert.IsDBNull(objDim_Id_Seg))
                    {
                        catPromociones.Dim_Id_Seg = (int)objDim_Id_Seg;
                    }

                    object objDim_Cantidad = dr.GetValue(dr.GetOrdinal("Dim_Cantidad"));
                    catPromociones.Dim_Cantidad = 0;
                    if (!Convert.IsDBNull(objDim_Cantidad))
                    {
                        catPromociones.Dim_Cantidad = (decimal)objDim_Cantidad;
                    }

                    object objDim_Descripcion = dr.GetValue(dr.GetOrdinal("Dim_Descripcion"));
                    catPromociones.Dim_Descripcion = string.Empty;
                    if (!Convert.IsDBNull(objDim_Descripcion))
                    {
                        catPromociones.Dim_Descripcion = (string)objDim_Descripcion;
                    }

                    object objValorPotencialTeorico = dr.GetValue(dr.GetOrdinal("CrmOp_ValorPotencialTeorico"));
                    catPromociones.ValorPotencialTeorico = 0.0f;
                    if (!Convert.IsDBNull(objValorPotencialTeorico))
                    {
                        catPromociones.ValorPotencialTeorico = (double)objValorPotencialTeorico;
                    }

                    object objVPM = dr.GetValue(dr.GetOrdinal("CrmOp_VPM"));
                    catPromociones.VentaPromedioMensualEsperada = 0.0M;
                    if (!Convert.IsDBNull(objVPM))
                    {
                        catPromociones.VentaPromedioMensualEsperada = (decimal)objVPM;
                    }

                    object objUenDescrip = dr.GetValue(dr.GetOrdinal("Uen_Descrip"));
                    if (!Convert.IsDBNull(objUenDescrip))
                    {
                        catPromociones.Uen_Descrip = (string)objUenDescrip;
                    }

                    object objTer_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    if (!Convert.IsDBNull(objTer_Nombre))
                    {
                        catPromociones.Ter_Nombre = (string)objTer_Nombre;
                    }

                    List.Add(catPromociones);
                }
                dr.Close();
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarPromocion(Sesion sesion, int cd, int promocion, ref int validador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Op" };
                object[] Valores = { sesion.Id_Emp, cd, promocion };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Cancelar", ref validador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatClientes(Sesion sesion, int Id_Ter, int Id_UEN, int Id_Rik, int id_Seg, int idCliente, string nombreCliente, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {  "@Id_Emp", // saltillo - UEN, Segmento y territorio
                                         "@Id_Cd",  // mty - territorio y representante                          
                                         "@Cte_Nombre",
                                         "@Id_Ter",
                                         "@Id_UEN",//solo para saltillo
                                         "@Id_Seg",//solo para saltillo
                                         "@Id_Rik"
                                      };
                object[] Valores = {     sesion.Id_Emp,  
                                         sesion.Id_Cd_Ver,
                                         nombreCliente,
                                         Id_Ter,
                                         Id_UEN,//solo para saltillo
                                         id_Seg,//solo para saltillo
                                         Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_ConsultaClientes", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {                  
                    catPromociones = new CrmPromociones();
                    catPromociones.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.NombreCte = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    catPromociones.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catPromociones.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    catPromociones.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    catPromociones.Uen_Descrip = (string)dr.GetValue(dr.GetOrdinal("Uen_Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                DateTime date = new DateTime();
                date = Convert.ToDateTime("01/01/1980");
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",
                                         "@Id_Usu",
                                         "@Id_Op",       
                                         "@Id_UEN",          
                                         "@Id_Seg",           
                                         "@Id_Ter",            
                                         "@Id_Area",            
                                         "@Id_Sol", 
                                         "@List_Apl",
                                         "@Id_Apl",            
                                         "@Clientes",    
                                         "@Productos",
                                         "@VentaNoRepetitiva",
                                         "@Comentarios",
                                         "@Analisis",
                                         "@Presentacion",
                                         "@Negociacion",
                                         "@Cierre",                                         
                                         "@FechaCotizacion",
                                         "@VentaMensual",                                         
                                         "@MontoProyecto",  
                                         "@Estatus",
                                         "@Id_Op_Anterior",
                                         "@Cancelacion",
                                         "@Id_Causa",
                                         "@Competidor",
                                         "@ComentariosCancel",
                                         "@Id_Cam",
                                         "@Id_CrmProspecto",
                                         "@Dim_Id_Uen",
                                         "@Dim_Id_Seg",
                                         "@Dim_Cantidad",
                                         "@CrmOp_VPM"
                                      };
                object[] Valores = {   
                                        sesion.Id_Emp,  
                                        sesion.Id_Cd_Ver,
                                        sesion.Id_Rik,
                                        promocion.IdMax,
                                        promocion.Uen,
                                        promocion.Segmento,
                                        promocion.Territorio,
                                        promocion.Area,
                                        promocion.Solucion,
                                        aplicaciones,
                                        promocion.Aplicacion,
                                        promocion.Cliente,
                                        promocion.Productos,
                                        promocion.VentaNoRepetitiva,
                                        promocion.Comentarios,
                                        (promocion.Analisis > date) ? promocion.Analisis : date,
                                        (promocion.Presentacion> date) ? promocion.Presentacion : date,
                                        (promocion.Negociacion> date) ? promocion.Negociacion : date,
                                        (promocion.Cierre> date) ? promocion.Cierre : date,
                                        promocion.FechaCotizacion,
                                        promocion.VentaPromedio,
                                        promocion.ValorPotencialO,
                                        promocion.Estatus,
                                        promocion.Id_Op,
                                        (promocion.Cancelacion > date) ? promocion.Cancelacion : date,
                                        promocion.Id_Causa,
                                        promocion.Competidor,
                                        promocion.ComentariosCancel,
                                        promocion.Id_Cam,
                                        promocion.Id_CrmProspecto,
                                        promocion.Dim_Id_Uen,
                                        promocion.Dim_Id_Seg,
                                        promocion.Dim_Cantidad,
                                        promocion.CrmOp_VPM
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Insertar", ref validador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión transaccional de InsertarOportunidad.
        /// </summary>
        /// <param name="sesion">Sesión del operador</param>
        /// <param name="promocion"></param>
        /// <param name="validador"></param>
        /// <param name="aplicaciones"></param>
        /// <param name="idcCtx">Contexto de la conexión al repositorio</param>
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones, ICD_Contexto idcCtx)
        {
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)idcCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = null;
            sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
            sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;

            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                DateTime date = new DateTime();
                date = Convert.ToDateTime("01/01/1980");
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",
                                         "@Id_Usu",
                                         "@Id_Op",       
                                         "@Id_UEN",          
                                         "@Id_Seg",           
                                         "@Id_Ter",            
                                         "@Id_Area",            
                                         "@Id_Sol", 
                                         "@List_Apl",
                                         "@Id_Apl",            
                                         "@Clientes",    
                                         "@Productos",
                                         "@VentaNoRepetitiva",
                                         "@Comentarios",
                                         "@Analisis",
                                         "@Presentacion",
                                         "@Negociacion",
                                         "@Cierre",                                         
                                         "@FechaCotizacion",
                                         "@VentaMensual",                                         
                                         "@MontoProyecto",  
                                         "@Estatus",
                                         "@Id_Op_Anterior",
                                         "@Cancelacion",
                                         "@Id_Causa",
                                         "@Competidor",
                                         "@ComentariosCancel",
                                         "@Id_Cam",
                                         "@Id_CrmProspecto",
                                         "@Dim_Id_Uen",
                                         "@Dim_Id_Seg",
                                         "@Dim_Cantidad",
                                         "@CrmOp_VPM",
                                         "@CrmOp_TipoVenta",
                                         "@OrigenCRM"
                                      };
                object[] Valores = {   
                                        sesion.Id_Emp,  
                                        sesion.Id_Cd_Ver,
                                        sesion.Id_Rik,
                                        promocion.IdMax,
                                        promocion.Uen,
                                        promocion.Segmento,
                                        promocion.Territorio,
                                        promocion.Area,
                                        promocion.Solucion,
                                        aplicaciones,
                                        promocion.Aplicacion,
                                        promocion.Cliente,
                                        promocion.Productos,
                                        promocion.VentaNoRepetitiva,
                                        promocion.Comentarios,
                                        (promocion.Analisis > date) ? promocion.Analisis : date,
                                        (promocion.Presentacion> date) ? promocion.Presentacion : date,
                                        (promocion.Negociacion> date) ? promocion.Negociacion : date,
                                        (promocion.Cierre> date) ? promocion.Cierre : date,
                                        promocion.FechaCotizacion,
                                        promocion.VentaPromedio,
                                        promocion.ValorPotencialO,
                                        promocion.Estatus,
                                        promocion.Id_Op,
                                        (promocion.Cancelacion > date) ? promocion.Cancelacion : date,
                                        promocion.Id_Causa,
                                        promocion.Competidor,
                                        promocion.ComentariosCancel,
                                        promocion.Id_Cam,
                                        promocion.Id_CrmProspecto,
                                        promocion.Dim_Id_Uen,
                                        promocion.Dim_Id_Seg,
                                        promocion.Dim_Cantidad,
                                        promocion.CrmOp_VPM,
                                        promocion.Crm_TipoVenta,
                                        promocion.CrmOp_OrigenCRMII
                                   };

                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("spCatCrmPromocion_Insertar", ref validador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, CrmOportunidades registros, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Seg", 
                                          //"@Id_Cte", 
                                          "@Id_Op", 
                                          "@Id_Sol" 
                                      };
                object[] Valores = { 
                                       registros.Id_Emp, 
                                       registros.Id_Cd, 
                                       registros.Id_Seg, 
                                       //registros.Id_Cte, 
                                       registros.Id_Op, 
                                       registros.Id_Sol 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmentoProyecto", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura;
                //creamos tabla para guardar los datos
                DataTable dataTable;
                for (int x = 0; x < 4; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();
                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsEstructuraSegmento.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                            dataRow[i] = dr.GetValue(i);
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                        break;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Cnx);
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",  
                                         "@Id_Estruc",
                                         "@Id_Ter",
                                         "@Id_Cte",
                                         "@Id_Area",
                                         "@Id_Seg", 
                                         "@Id_UEN",
                                         "@Id_Apl", 
                                         "@Id_Sol",            
                                         "@Porcentaje",
                                         "@Estatus",
                                         "@Id_Op"
                                      };
                object[] Valores = {   
                                        registros.Id_Emp,  
                                        registros.Id_Cd,
                                        registros.Id_Estruc,
                                        registros.Id_Ter,
                                        registros.Id_Cte,
                                        registros.ID_Area,
                                        registros.Id_Seg,
                                        registros.Id_Uen, 
                                        registros.Id_Apl,
                                        registros.Id_Sol,
                                        registros.Porcentaje,
                                        registros.Activo,
                                        registros.Id_Op
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmentoProyecto_Modificar", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión transaccional de [ActualizaDimension]
        /// </summary>
        /// <param name="registros"></param>
        /// <param name="Cnx"></param>
        /// <param name="verificador"></param>
        /// <param name="icdCtx">Contexto de conexión al repositorio</param>
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador, ICD_Contexto icdCtx)
        {
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = null;

            try
            {
                //CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Cnx);
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",  
                                         "@Id_Estruc",
                                         "@Id_Ter",
                                         "@Id_Cte",
                                         "@Id_Area",
                                         "@Id_Seg", 
                                         "@Id_UEN",
                                         "@Id_Apl", 
                                         "@Id_Sol",            
                                         "@Porcentaje",
                                         "@Estatus",
                                         "@Id_Op"
                                      };
                object[] Valores = {   
                                        registros.Id_Emp,  
                                        registros.Id_Cd,
                                        registros.Id_Estruc,
                                        registros.Id_Ter,
                                        registros.Id_Cte,
                                        registros.ID_Area,
                                        registros.Id_Seg,
                                        registros.Id_Uen, 
                                        registros.Id_Apl,
                                        registros.Id_Sol,
                                        registros.Porcentaje,
                                        registros.Activo,
                                        registros.Id_Op
                                   };
                sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
                sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
                sqlcmd = CD_Datos.GenerarSqlCommand("spCRMEstructuraSegmentoProyecto_Modificar", ref verificador, Parametros, Valores, transaction.Connection, sqlcmd);
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CrmOportunidade ConsultarDetalle(int idEmp, int idCd, int idOp, string efConexion)
        {
            CrmOportunidade result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(efConexion))
            {
                var res = (from o in ctx.CrmOportunidades
                         where o.Id_Emp == idEmp && o.Id_Cd == idCd && o.Id_Op == idOp
                         select o).ToList();
                if (res.Count > 0)
                {
                    result = res[0];
                }
            }
            return result;
        }

        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="idEmp">int. Identificador de la empresa</param>
        /// <param name="idCd">int. Identificador del centro de distribución</param>
        /// <param name="promocion">CRMRegistroProyectos. Información a actualizar</param>
        /// <param name="conexionEF">string. Cadena de conexión compatible con entity framework</param>
        public void ActualizarPromocion(int idEmp, int idCd, CRMRegistroProyectos promocion, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var oportunidades = (from o in ctx.CrmOportunidades
                                     where o.Id_CrmProspecto == promocion.Id_CrmProspecto && o.Id_Emp == idEmp && o.Id_Cd == idCd && o.Id_Cte==promocion.Cliente && o.Id_Op==promocion.Id_Op
                                     select o).ToList();
                if (oportunidades.Count > 0)
                {
                    var oportunidad = oportunidades[0];
                    oportunidad.Id_Uen = promocion.Uen;
                    oportunidad.Id_Seg = promocion.Segmento;
                    oportunidad.ID_Area = promocion.Area;
                    oportunidad.Id_Sol = promocion.Solucion;
                    oportunidad.Id_Ter = promocion.Territorio;
                    oportunidad.VentaNoRepetitiva = promocion.VentaNoRepetitiva;
                    oportunidad.Id_Cte = promocion.Cliente;
                    oportunidad.Id_CrmProspecto = promocion.Id_CrmProspecto;

                    ctx.SaveChanges();
                }
            }
        }

        public void ActualizarPromocion(int idEmp, int idCd, CrmPromociones promocion, string conexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                
                var oportunidades = (from o in ctx.CrmOportunidades
                                     where o.Id_CrmProspecto == promocion.Id_CrmProspecto && o.Id_Emp == idEmp && o.Id_Cd == idCd && o.Id_Cte == promocion.Cliente && o.Id_Op == promocion.Id
                                     select o).ToList();
                if (oportunidades.Count > 0)
                {
                    var oportunidad = oportunidades[0];
                    oportunidad.Id_Uen = promocion.Uen;
                    oportunidad.Id_Seg = promocion.Segmento;
                    oportunidad.ID_Area = promocion.Area;
                    oportunidad.Id_Sol = promocion.Solucion;
                    oportunidad.Id_Ter = promocion.Territorio;
                    oportunidad.VentaNoRepetitiva = promocion.VentaNoRepetitiva;
                    oportunidad.Id_Cte = promocion.Cliente;
                    oportunidad.Id_CrmProspecto = promocion.Id_CrmProspecto;

                    ctx.SaveChanges();
                }
            }
        }

        public void ActualizarPromocion(int idEmp, int idCd, CrmPromociones promocion, ICD_Contexto contextoDatos)
        {
            sianwebmty_gEntities ctx = (contextoDatos as CD_ContextoDefault).Contexto;
            var oportunidades = (from o in ctx.CrmOportunidades
                                 where o.Id_CrmProspecto == promocion.Id_CrmProspecto && o.Id_Emp == idEmp && o.Id_Cd == idCd && o.Id_Cte == promocion.Cliente && o.Id_Op == promocion.Id
                                 select o).ToList();
            if (oportunidades.Count > 0)
            {
                var oportunidad = oportunidades[0];
                oportunidad.Id_Uen = promocion.Id_Uen;
                oportunidad.Id_Seg = promocion.Segmento;
                oportunidad.ID_Area = promocion.Area;
                oportunidad.Id_Sol = promocion.Solucion;
                oportunidad.Id_Ter = promocion.Id_Ter;
                oportunidad.VentaNoRepetitiva = promocion.VentaNoRepetitiva;
                oportunidad.Id_Cte = promocion.Cliente;
                oportunidad.Id_CrmProspecto = promocion.Id_CrmProspecto;
                oportunidad.CrmOp_EnValuacion = promocion.EnValuacion;
            }
        }

        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="idEmp">int. Identificador de la empresa</param>
        /// <param name="idCd">int. Identificador del centro de distribución</param>
        /// <param name="promocion">CRMRegistroProyectos. Información a actualizar</param>
        /// <param name="contextoDatos">string. Contexto de operación a la fuente de datos</param>
        public void ActualizarPromocion(int idEmp, int idCd, CRMRegistroProyectos promocion, ICD_Contexto contextoDatos)
        {
            sianwebmty_gEntities ctx = (contextoDatos as CD_ContextoDefault).Contexto;
            var oportunidades = (from o in ctx.CrmOportunidades
                                     where o.Id_CrmProspecto == promocion.Id_CrmProspecto && o.Id_Emp == idEmp && o.Id_Cd == idCd && o.Id_Cte == promocion.Cliente && o.Id_Op == promocion.Id_Op
                                     select o).ToList();
            if (oportunidades.Count > 0)
            {
                var oportunidad = oportunidades[0];
                oportunidad.Id_Uen = promocion.Uen;
                oportunidad.Id_Seg = promocion.Segmento;
                oportunidad.ID_Area = promocion.Area;
                oportunidad.Id_Sol = promocion.Solucion;
                oportunidad.Id_Ter = promocion.Territorio;
                oportunidad.VentaNoRepetitiva = promocion.VentaNoRepetitiva;
                oportunidad.Id_Cte = promocion.Cliente;
                oportunidad.Id_CrmProspecto = promocion.Id_CrmProspecto;
                oportunidad.Dim_Id_Uen = promocion.Dim_Id_Uen;
                oportunidad.Dim_Id_Seg = promocion.Dim_Id_Seg;
                oportunidad.Dim_Cantidad = promocion.Dim_Cantidad;
                oportunidad.CrmOp_VPM = promocion.CrmOp_VPM;
            }
        }

        public IEnumerable<CrmOportunidade> ConsultarPorClienteTerritorio(int idEmp, int idCd, int idCte, int idTer, string cadenaConexionEF)
        {
            IEnumerable<CrmOportunidade> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                resultado = (from op in ctx.CrmOportunidades
                                 where op.Id_Emp == idEmp && op.Id_Cd == idCd && op.Id_Cte == idCte && op.Id_Ter == idTer
                                 select op).ToList();
            }
            return resultado;
        }
    }
}
