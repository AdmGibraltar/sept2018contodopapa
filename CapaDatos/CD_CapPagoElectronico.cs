using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapPagoElectronico
    {
        public void InsertarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {
                //JFCV 05 abr 2016
                //oDB.BeginTransaction();
                try
                {
                    verificador = (int)oDB.spExecScalar(
                        "spCapPagoElectronico_Insertar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                        new SqlParameter("@Id_PagElecTipo", pagoElectronico.Id_PagElecTipo),
                        new SqlParameter("@Id_PagElecSubTipo", pagoElectronico.Id_PagElecSubTipo),
                        new SqlParameter("@Id_PagElecCuenta", pagoElectronico.Id_PagElecCuenta),
                        new SqlParameter("@Id_AcrCheque", pagoElectronico.Id_AcrCheque),
                        new SqlParameter("@Id_Acr", pagoElectronico.Id_Acr),
                        new SqlParameter("@PagElec_Solicitante", pagoElectronico.PagElec_Solicitante),
                        new SqlParameter("@PagElec_FechaRequiere", pagoElectronico.PagElec_FechaRequiere),
                        new SqlParameter("@PagElec_Cuenta", pagoElectronico.PagElec_Cuenta),
                        new SqlParameter("@PagElec_Cc", pagoElectronico.PagElec_Cc),
                        new SqlParameter("@PagElec_SubCuenta", pagoElectronico.PagElec_SubCuenta),
                        new SqlParameter("@PagElec_SubSubCuenta", pagoElectronico.PagElec_SubSubCuenta),
                        new SqlParameter("@PagElec_CuentaPago", pagoElectronico.PagElec_CuentaPago),
                        new SqlParameter("@PagElec_Numero", pagoElectronico.PagElec_Numero),
                        new SqlParameter("@PagElec_Importe", pagoElectronico.PagElec_Importe),
                        new SqlParameter("@PagElec_Observaciones", pagoElectronico.PagElec_Observaciones),
                        new SqlParameter("@PagElec_Xml", (System.Data.SqlTypes.SqlXml)pagoElectronico.PagElec_Xml),
                        new SqlParameter("@PagElec_Pdf", (byte[])pagoElectronico.PagElec_Pdf),
                        new SqlParameter("@PagElec_IdU", pagoElectronico.PagElec_IdU),
                        new SqlParameter("@PagElec_FechaRegistro", pagoElectronico.PagElec_FechaRegistro),
                        new SqlParameter("@PagElec_Soporte", pagoElectronico.PagElec_Soporte),
                        new SqlParameter("@PagElec_Soporte_Nombre", pagoElectronico.PagElec_Soporte_Nombre),
                        new SqlParameter("@PagElec_Soporte_Tipo", pagoElectronico.PagElec_Soporte_Tipo),
                        new SqlParameter("@PagElec_FechaSalida", pagoElectronico.PagElec_FechaSalida),
                        new SqlParameter("@PagElec_Destino", pagoElectronico.PagElec_Destino),
                        new SqlParameter("@PagElec_SoporteImporte", pagoElectronico.PagElec_SoporteImporte)

                    );

                    //JFCV para evitar inconsistencia, al mandar insertar y el id ya existe , genera un nuevo valor para el id 
                    // ese valor lo guardo para que lo tenga al insertar el detalle 
                    if (verificador != 0)
                    {
                        pagoElectronico.Id_PagElec = verificador;
                    }

                    for (int CR = 0; CR <= (pagoElectronico.PagElecArchivo.Count - 1); CR++)
                    {
                        oDB.spExecNonQuery(
                            "spCapPagoElectronicoArchivos_Insertar",
                            new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                            new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                            new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                            new SqlParameter("@Partida", CR + 1),
                            new SqlParameter("@PDFStream", SqlDbType.Image) { Value = pagoElectronico.PagElecArchivo[CR].PagElec_PDFStream },
                            new SqlParameter("@XMLStream", SqlDbType.Image) { Value = pagoElectronico.PagElecArchivo[CR].PagElec_XMLStream },
                            new SqlParameter("@FechaFactura", pagoElectronico.PagElecArchivo[CR].FechaFactura),
                            new SqlParameter("@Serie", pagoElectronico.PagElecArchivo[CR].Serie),
                            new SqlParameter("@Folio", pagoElectronico.PagElecArchivo[CR].Folio),
                            new SqlParameter("@Importe", pagoElectronico.PagElecArchivo[CR].Importe),
                            new SqlParameter("@PagElec_Cuenta", pagoElectronico.PagElecArchivo[CR].PagElec_Cuenta),
                            new SqlParameter("@PagElec_Cc", pagoElectronico.PagElecArchivo[CR].PagElec_Cc),
                            new SqlParameter("@PagElec_Numero", pagoElectronico.PagElecArchivo[CR].PagElec_Numero),
                            new SqlParameter("@PagElec_SubCuenta", pagoElectronico.PagElecArchivo[CR].PagElec_SubCuenta),
                            new SqlParameter("@PagElec_SubSubCuenta", pagoElectronico.PagElecArchivo[CR].PagElec_SubSubCuenta),
                            new SqlParameter("@PagElec_CuentaPago", pagoElectronico.PagElecArchivo[CR].PagElec_CuentaPago),
                            new SqlParameter("@PagElec_Observaciones", pagoElectronico.PagElecArchivo[CR].PagElec_Observaciones),
                            new SqlParameter("@PagElec_RFC", pagoElectronico.PagElecArchivo[CR].PagElec_RFC),
                            new SqlParameter("@PagElec_Id_PagElecCuenta", pagoElectronico.PagElecArchivo[CR].Id_PagElecCuenta),

                            //JFCV 02 feb 2016 agregar campos de control 
                            new SqlParameter("@PagElec_UUID", pagoElectronico.PagElecArchivo[CR].PagElec_UUID),
                            new SqlParameter("@PagElec_Subtotal", pagoElectronico.PagElecArchivo[CR].PagElec_Subtotal),
                            new SqlParameter("@PagElec_Iva", pagoElectronico.PagElecArchivo[CR].PagElec_Iva),
                            new SqlParameter("@PagElec_ImpRetenido", pagoElectronico.PagElecArchivo[CR].PagElec_ImpRetenido),
                            new SqlParameter("@PagElec_IvaRetenido", pagoElectronico.PagElecArchivo[CR].PagElec_IvaRetenido)

                        );
                    }

                    //JFCV 05 abr 2016
                    //oDB.Commit();
                }
                catch (Exception ex)
                {
                    verificador = 0;
                    //JFCV 05 abr 2016
                    // oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }
         

        public void ModificarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {
                oDB.BeginTransaction();

                try
                {

                    verificador = (int)oDB.spExecScalar("spCapPagoElectronicoArchivos_Eliminar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                );
                
               


                
                    verificador = (int)oDB.spExecScalar(
                        "spCapPagoElectronico_Modificar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                        new SqlParameter("@Id_PagElecTipo", pagoElectronico.Id_PagElecTipo),
                        new SqlParameter("@Id_PagElecSubTipo", pagoElectronico.Id_PagElecSubTipo),
                        new SqlParameter("@Id_PagElecCuenta", pagoElectronico.Id_PagElecCuenta),
                        new SqlParameter("@Id_AcrCheque", pagoElectronico.Id_AcrCheque),
                        new SqlParameter("@Id_Acr", pagoElectronico.Id_Acr),
                        new SqlParameter("@PagElec_Solicitante", pagoElectronico.PagElec_Solicitante),
                        new SqlParameter("@PagElec_FechaRequiere", pagoElectronico.PagElec_FechaRequiere),
                        new SqlParameter("@PagElec_Cuenta", pagoElectronico.PagElec_Cuenta),
                        new SqlParameter("@PagElec_Cc", pagoElectronico.PagElec_Cc),
                        new SqlParameter("@PagElec_SubCuenta", pagoElectronico.PagElec_SubCuenta),
                        new SqlParameter("@PagElec_SubSubCuenta", pagoElectronico.PagElec_SubSubCuenta),
                        new SqlParameter("@PagElec_CuentaPago", pagoElectronico.PagElec_CuentaPago),
                        new SqlParameter("@PagElec_Numero", pagoElectronico.PagElec_Numero),
                        new SqlParameter("@PagElec_Importe", pagoElectronico.PagElec_Importe),
                        new SqlParameter("@PagElec_Observaciones", pagoElectronico.PagElec_Observaciones),
                        new SqlParameter("@PagElec_Xml", (System.Data.SqlTypes.SqlXml)pagoElectronico.PagElec_Xml),
                        new SqlParameter("@PagElec_Pdf", (byte[])pagoElectronico.PagElec_Pdf),
                        new SqlParameter("@PagElec_IdU", pagoElectronico.PagElec_IdU),
                        new SqlParameter("@PagElec_FechaRegistro", pagoElectronico.PagElec_FechaRegistro),
                        new SqlParameter("@PagElec_Soporte", pagoElectronico.PagElec_Soporte),
                        new SqlParameter("@PagElec_Soporte_Nombre", pagoElectronico.PagElec_Soporte_Nombre),
                        new SqlParameter("@PagElec_Soporte_Tipo", pagoElectronico.PagElec_Soporte_Tipo),
                        new SqlParameter("@PagElec_FechaSalida", pagoElectronico.PagElec_FechaSalida),
                        new SqlParameter("@PagElec_Destino", pagoElectronico.PagElec_Destino),
                        new SqlParameter("@PagElec_SoporteImporte", pagoElectronico.PagElec_SoporteImporte)
                    );

                    for (int CR = 0; CR <= (pagoElectronico.PagElecArchivo.Count - 1); CR++)
                    {
                        oDB.spExecNonQuery(
                            "spCapPagoElectronicoArchivos_Insertar",
                            new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                            new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                            new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                            new SqlParameter("@Partida", CR + 1),
                            new SqlParameter("@PDFStream", SqlDbType.Image) { Value = pagoElectronico.PagElecArchivo[CR].PagElec_PDFStream },
                            new SqlParameter("@XMLStream", SqlDbType.Image) { Value = pagoElectronico.PagElecArchivo[CR].PagElec_XMLStream },
                            new SqlParameter("@FechaFactura", pagoElectronico.PagElecArchivo[CR].FechaFactura),
                            new SqlParameter("@Serie", pagoElectronico.PagElecArchivo[CR].Serie),
                            new SqlParameter("@Folio", pagoElectronico.PagElecArchivo[CR].Folio),
                            new SqlParameter("@Importe", pagoElectronico.PagElecArchivo[CR].Importe),
                            new SqlParameter("@PagElec_Cuenta", pagoElectronico.PagElecArchivo[CR].PagElec_Cuenta),
                            new SqlParameter("@PagElec_Cc", pagoElectronico.PagElecArchivo[CR].PagElec_Cc),
                            new SqlParameter("@PagElec_Numero", pagoElectronico.PagElecArchivo[CR].PagElec_Numero),
                            new SqlParameter("@PagElec_SubCuenta", pagoElectronico.PagElecArchivo[CR].PagElec_SubCuenta),
                            new SqlParameter("@PagElec_SubSubCuenta", pagoElectronico.PagElecArchivo[CR].PagElec_SubSubCuenta),
                            new SqlParameter("@PagElec_CuentaPago", pagoElectronico.PagElecArchivo[CR].PagElec_CuentaPago),
                            new SqlParameter("@PagElec_Observaciones", pagoElectronico.PagElecArchivo[CR].PagElec_Observaciones),
                            new SqlParameter("@PagElec_RFC", pagoElectronico.PagElecArchivo[CR].PagElec_RFC),
                            new SqlParameter("@PagElec_Id_PagElecCuenta", pagoElectronico.PagElecArchivo[CR].Id_PagElecCuenta),
                            //JFCV 02 feb 2016 agregar campos de control 
                            new SqlParameter("@PagElec_UUID", pagoElectronico.PagElecArchivo[CR].PagElec_UUID),
                            new SqlParameter("@PagElec_Subtotal", pagoElectronico.PagElecArchivo[CR].PagElec_Subtotal),
                            new SqlParameter("@PagElec_Iva", pagoElectronico.PagElecArchivo[CR].PagElec_Iva),
                            new SqlParameter("@PagElec_ImpRetenido", pagoElectronico.PagElecArchivo[CR].PagElec_ImpRetenido),
                            new SqlParameter("@PagElec_IvaRetenido", pagoElectronico.PagElecArchivo[CR].PagElec_IvaRetenido)
                        );
                    }

                    oDB.Commit();
                }
                catch (Exception ex)
                {
                    verificador = 0;
                    oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }

        public void EliminarPagoElectronicoArchivos(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {
                oDB.BeginTransaction();
                try
                {
                    verificador = (int)oDB.spExecScalar("spCapPagoElectronicoArchivos_Eliminar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                );

                    oDB.Commit();

                }
                catch (Exception ex)
                {
                    verificador = 0;
                    oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }

 

        public void AutorizarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            dbAccess oDB = new dbAccess(Conexion);
            try
            {
                oDB.BeginTransaction();

                AutorizarPagoElectronico(pagoElectronico, ref verificador, ref oDB);

                oDB.Commit();
            }
            catch (Exception ex)
            {
                oDB.RollBack();
                throw ex;
            }
            finally
            {
                oDB.Dispose();
            }
        }

        public void AutorizarPagoElectronico(PagoElectronico pagoElectronico, ref int verificador, ref dbAccess dbCNX)
        {
            try
            {
                verificador = (int)dbCNX.spExecScalar(
                        "spCapPagoElectronico_Autorizar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                        new SqlParameter("@PagElec_NumeroReferencia", pagoElectronico.PagElec_NumeroReferencia)
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        //JFCV 18 dic 2015 agregar rechazo 
        public void RechazarPagoElectronico(PagoElectronico pagoElectronico, ref int verificador, ref dbAccess dbCNX)
        {
            try
            {
                verificador = (int)dbCNX.spExecScalar(
                        "spCapPagoElectronico_Rechazar",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec),
                        new SqlParameter("@PagElec_MotivoRechazo", pagoElectronico.PagElec_MotivoRechazo)
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void CancelarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_PagElec"
                                      };

                object[] Valores = { 
                                       pagoElectronico.Id_Emp,
                                       pagoElectronico.Id_Cd,
                                       pagoElectronico.Id_PagElec
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoElectronico_Cancelar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void ConsultaPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref List<PagoElectronico> list)
        {
            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    if (pagoElectronico.Id_PagElec==0)
                    {
                        pagoElectronico.Id_PagElec = -1;
                    }
                    if (pagoElectronico.Id_PagElecSubTipo == 0)
                    {
                        pagoElectronico.Id_PagElecSubTipo = -1;
                    }
                    

                    DataSet DS = oDB.spExecDataSet(
                        "spCapPagoElectronico_Lista",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_Acr", pagoElectronico.Id_Acr_Filtro == -1 ? (object)null : pagoElectronico.Id_Acr_Filtro),
                        new SqlParameter("@Id_PagElecTipo", pagoElectronico.Id_PagElecTipo_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecTipo_Filtro),
                        new SqlParameter("@Id_PagElecCuenta", pagoElectronico.Id_PagElecCuenta_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecCuenta_Filtro),
                        new SqlParameter("@Id_PagElecEstatus", pagoElectronico.Id_PagElecEstatus_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecEstatus_Filtro),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec == -1 ? (object)null : pagoElectronico.Id_PagElec),
                        new SqlParameter("@Id_PagElecSubTipo",pagoElectronico.Id_PagElecSubTipo == -1 ? (object)null : pagoElectronico.Id_PagElecSubTipo)
                        //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                    );

                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        pagoElectronico = new PagoElectronico();
                        pagoElectronico.Id_Emp = (int)DR["Id_Emp"];
                        pagoElectronico.Id_Cd = (int)DR["Id_Cd"];
                        pagoElectronico.Id_PagElec = (int)DR["Id_PagElec"];
                        pagoElectronico.Id_PagElecTipo = (int)DR["Id_PagElecTipo"];
                        pagoElectronico.Id_PagElecSubTipo = (int)DR["Id_PagElecSubTipo"];
                        pagoElectronico.PagElecTipo_Descrpcion = DR["PagElecTipo_Descrpcion"].ToString();
                        pagoElectronico.Id_PagElecCuenta = (int)DR["Id_PagElecCuenta"];
                        pagoElectronico.pagElecCuenta_Descripcion = DR["pagElecCuenta_Descripcion"].ToString();
                        pagoElectronico.Id_AcrCheque = (int)DR["Id_AcrCheque"];
                        pagoElectronico.AcrCheque_Nombre = DR["AcrCheque_Nombre"].ToString();
                        pagoElectronico.Id_Acr = (int)DR["Id_Acr"];
                        pagoElectronico.Acr_Nombre = DR["Acr_Nombre"].ToString();
                        pagoElectronico.PagElec_Solicitante = DR["PagElec_Solicitante"].ToString();
                        pagoElectronico.PagElec_FechaRequiere = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_Cuenta = DR["PagElec_Cuenta"].ToString();
                        pagoElectronico.PagElec_Cc = DR["PagElec_Cc"].ToString();
                        pagoElectronico.PagElec_Numero = DR["PagElec_Numero"].ToString();
                        pagoElectronico.PagElec_SubCuenta = DR["PagElec_SubCuenta"].ToString();
                        pagoElectronico.PagElec_SubSubCuenta = DR["PagElec_SubSubCuenta"].ToString();
                        pagoElectronico.PagElec_CuentaPago = DR["PagElec_CuentaPago"].ToString();
                        pagoElectronico.PagElec_Importe = decimal.Parse(DR["PagElec_Importe"].ToString());
                        pagoElectronico.PagElec_Observaciones = DR["PagElec_Observaciones"].ToString();
                        pagoElectronico.PagElec_Autorizado = Boolean.Parse(DR["PagElec_Autorizado"].ToString());
                        pagoElectronico.Id_PagElecEstatus = (int)DR["Id_PagElecEstatus"];
                        pagoElectronico.PagElecEstatus_Descripcion = DR["PagElecEstatus_Descripcion"].ToString();
                        pagoElectronico.PagElec_Xml = DR["PagElec_Xml"].ToString();
                        pagoElectronico.PagElec_Pdf = DR["PagElec_Pdf"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Pdf"]);
                        pagoElectronico.PagElec_Soporte = DR["PagElec_Soporte"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Soporte"]);
                        pagoElectronico.Acr_NumeroGenerado = DR["Acr_NumeroGenerado"].ToString();
                        //JFCV 03nov2015 agregue la fecha de salida
                        pagoElectronico.PagElec_FechaSalida = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_FechaUltMod = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        //JFCV 17 Dic 2015 Agregar Destino y Motivo rechazo
                        pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                        pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString();
                        //JFCV 08nov2016
                        pagoElectronico.PagElec_SubTipoDescripcion = DR["PagElec_SubTipoDescripcion"].ToString();

                         DataSet DSFile = oDB.spExecDataSet(
                            "spCapPagoElectronicoArchivos_Listasinpdf",
                            new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                            new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                            new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                        );

                         decimal? importe = 0;

                        if (DSFile != null && DSFile.Tables.Count > 0 && DSFile.Tables[0].Rows.Count > 0)
                        {
                           
                            foreach (DataRow DRFile in DSFile.Tables[0].Rows)
                            {
                                pagoElectronico.PagElecArchivo.Add(
                                    new PagoElectronicoArchivo(
                                        (int?)pagoElectronico.Id_Emp,
                                        (int?)pagoElectronico.Id_Cd,
                                        (int?)pagoElectronico.Id_PagElec,
                                        (DRFile["Partida"].GetType().Name == "DBNull"?(int?)null:(int?)DRFile["Partida"]),
                                        (DRFile["PDFStream"].GetType().Name == "DBNull"?(byte[])null:(byte[])DRFile["PDFStream"]),
                                        (DRFile["XMLStream"].GetType().Name == "DBNull"?(byte[])null:(byte[])DRFile["XMLStream"]),
                                        (DRFile["FechaFactura"].GetType().Name == "DBNull"?(DateTime?)null:(DateTime?)DRFile["FechaFactura"]),
                                        (DRFile["Serie"].GetType().Name == "DBNull"?(string)null:(string)DRFile["Serie"]),
                                        (DRFile["Folio"].GetType().Name == "DBNull"?(string)null:(string)DRFile["Folio"]),
                                        (DRFile["Importe"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["Importe"]),
                                        (DRFile["PagElec_Cc"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cc"]),
                                        (DRFile["PagElec_Numero"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Numero"]),
                                        (DRFile["PagElec_Cuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                        (DRFile["PagElec_SubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                        (DRFile["PagElec_SubSubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                        (DRFile["PagElec_CuentaPago"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                        (DRFile["PagElec_Observaciones"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                        (DRFile["PagElec_Id_PagElecCuenta"].GetType().Name == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                        (DRFile["PagElec_RFC"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                        //JFCV  02 feb 2016 
                                        (DRFile["PagElec_UUID"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                        (DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                        (DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                        (DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                        (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"]),
                                        ("Con Comprobante")
                                    )
                                );
                                if (DRFile["Importe"].GetType().Name != "DBNull" )
                                {
                                    importe = importe + (decimal?)DRFile["Importe"];
                                }
                            }
                        }
                        else
                        {
                            pagoElectronico.PagElecArchivo = new List<PagoElectronicoArchivo>();
                        }

                        //JFCV 8 NOV 2016 INICIO agregar el archivo de soporte al listado de comprobantes 
                        //AGREGAR UNA COLUMNA QUE SEA CON COMPROBANTE O SIN COMPROBANTE 
                        if (pagoElectronico.PagElec_Pdf != null)
                        {
                            pagoElectronico.PagElecArchivo.Add(
                                    new PagoElectronicoArchivo(
                                        (int?)pagoElectronico.Id_Emp,
                                        (int?)pagoElectronico.Id_Cd,
                                        (int?)pagoElectronico.Id_PagElec,
                                        9999, //partida
                                        pagoElectronico.PagElec_Pdf,
                                        (byte[])null, //XML
                                        pagoElectronico.PagElec_FechaRequiere,
                                        ((string)null), //Serie
                                        ((string)null), //Folio
                                        (pagoElectronico.PagElec_Importe - importe),
                                        (pagoElectronico.PagElec_Cc),
                                        (pagoElectronico.PagElec_Numero), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                         (pagoElectronico.PagElec_Cuenta),
                                        (pagoElectronico.PagElec_SubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                        (pagoElectronico.PagElec_SubSubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                        (pagoElectronico.PagElec_CuentaPago), // == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                        (pagoElectronico.PagElec_Observaciones), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                        (pagoElectronico.Id_PagElecCuenta), // == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                        (""), // == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                        (""), // "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                         (pagoElectronico.PagElec_Importe - importe), //(DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                         (0), //(DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                        (0), //(DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                        (0) // (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"])
                                         ,("Sin Comprobante")
                                        )
                                        );

                        }
                        //JFCV 8 NOV 2016 FIN agregar el archivo de soporte al listado de comprobantes 

                        list.Add(pagoElectronico);
                    }
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
        }

     

       public void ConsultaPagoElectronicoAdmin(PagoElectronico pagoElectronico, string Conexion, ref List<PagoElectronico> list)
       {


            try
               {
                   if (pagoElectronico.Id_PagElec == 0)
                   {
                       pagoElectronico.Id_PagElec = -1;
                   }
                   if (pagoElectronico.Id_PagElecSubTipo == 0)
                   {
                       pagoElectronico.Id_PagElecSubTipo = -1;
                   }

                SqlDataReader DR = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Acr" ,"@Id_PagElecTipo", "@Id_PagElecCuenta", "@Id_PagElecEstatus", "@Id_PagElec","@Id_PagElecSubTipo" };
                object[] Valores = { pagoElectronico.Id_Emp, pagoElectronico.Id_Cd, 
                                       pagoElectronico.Id_Acr_Filtro == -1 ? (object)null : pagoElectronico.Id_Acr_Filtro
                                   ,pagoElectronico.Id_PagElecTipo_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecTipo_Filtro
                                   ,pagoElectronico.Id_PagElecCuenta_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecCuenta_Filtro
                                   ,pagoElectronico.Id_PagElecEstatus_Filtro == -1 ? (object)null : pagoElectronico.Id_PagElecEstatus_Filtro
                                   , pagoElectronico.Id_PagElec == -1 ? (object)null : pagoElectronico.Id_PagElec
                                   ,pagoElectronico.Id_PagElecSubTipo == -1 ? (object)null : pagoElectronico.Id_PagElecSubTipo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoElectronico_Lista", ref DR, Parametros, Valores);

                while (DR.Read())
                {
   
                   //foreach (DataRow DR in DS.Tables[0].Rows)
                   //{
                       pagoElectronico = new PagoElectronico();
                       pagoElectronico.Id_Emp = (int)DR["Id_Emp"];
                       pagoElectronico.Id_Cd = (int)DR["Id_Cd"];
                       pagoElectronico.Id_PagElec = (int)DR["Id_PagElec"];
                       pagoElectronico.Id_PagElecTipo = (int)DR["Id_PagElecTipo"];
                       pagoElectronico.Id_PagElecSubTipo = (int)DR["Id_PagElecSubTipo"];
                       pagoElectronico.PagElecTipo_Descrpcion = DR["PagElecTipo_Descrpcion"].ToString();
                       pagoElectronico.Id_PagElecCuenta = (int)DR["Id_PagElecCuenta"];
                       pagoElectronico.pagElecCuenta_Descripcion = DR["pagElecCuenta_Descripcion"].ToString();
                       pagoElectronico.Id_AcrCheque = (int)DR["Id_AcrCheque"];
                       pagoElectronico.AcrCheque_Nombre = DR["AcrCheque_Nombre"].ToString();
                       pagoElectronico.Id_Acr = (int)DR["Id_Acr"];
                       pagoElectronico.Acr_Nombre = DR["Acr_Nombre"].ToString();
                       pagoElectronico.PagElec_Solicitante = DR["PagElec_Solicitante"].ToString();
                       pagoElectronico.PagElec_FechaRequiere = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                       pagoElectronico.PagElec_Cuenta = DR["PagElec_Cuenta"].ToString();
                       pagoElectronico.PagElec_Cc = DR["PagElec_Cc"].ToString();
                       pagoElectronico.PagElec_Numero = DR["PagElec_Numero"].ToString();
                       pagoElectronico.PagElec_SubCuenta = DR["PagElec_SubCuenta"].ToString();
                       pagoElectronico.PagElec_SubSubCuenta = DR["PagElec_SubSubCuenta"].ToString();
                       pagoElectronico.PagElec_CuentaPago = DR["PagElec_CuentaPago"].ToString();
                       pagoElectronico.PagElec_Importe = decimal.Parse(DR["PagElec_Importe"].ToString());
                       pagoElectronico.PagElec_Observaciones = DR["PagElec_Observaciones"].ToString();
                       pagoElectronico.PagElec_Autorizado = Boolean.Parse(DR["PagElec_Autorizado"].ToString());
                       pagoElectronico.Id_PagElecEstatus = (int)DR["Id_PagElecEstatus"];
                       pagoElectronico.PagElecEstatus_Descripcion = DR["PagElecEstatus_Descripcion"].ToString();
                       pagoElectronico.PagElec_Xml = DR["PagElec_Xml"].ToString();
                       pagoElectronico.PagElec_Pdf = DR["PagElec_Pdf"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Pdf"]);
                       pagoElectronico.PagElec_Soporte = DR["PagElec_Soporte"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Soporte"]);
                       pagoElectronico.Acr_NumeroGenerado = DR["Acr_NumeroGenerado"].ToString();
                       //JFCV 03nov2015 agregue la fecha de salida
                       pagoElectronico.PagElec_FechaSalida = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                       pagoElectronico.PagElec_FechaUltMod = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                       //JFCV 17 Dic 2015 Agregar Destino y Motivo rechazo
                       pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                       pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString();
                       //JFCV 08nov2016
                       pagoElectronico.PagElec_SubTipoDescripcion = DR["PagElec_SubTipoDescripcion"].ToString();

                      

                       decimal? importe = 0;
                       pagoElectronico.PagElecArchivo = new List<PagoElectronicoArchivo>();
                      

                       //JFCV 8 NOV 2016 INICIO agregar el archivo de soporte al listado de comprobantes 
                       //AGREGAR UNA COLUMNA QUE SEA CON COMPROBANTE O SIN COMPROBANTE 
                       if (pagoElectronico.PagElec_Pdf != null)
                       {
                           pagoElectronico.PagElecArchivo.Add(
                                   new PagoElectronicoArchivo(
                                       (int?)pagoElectronico.Id_Emp,
                                       (int?)pagoElectronico.Id_Cd,
                                       (int?)pagoElectronico.Id_PagElec,
                                       9999, //partida
                                       pagoElectronico.PagElec_Pdf,
                                       (byte[])null, //XML
                                       pagoElectronico.PagElec_FechaRequiere,
                                       ((string)null), //Serie
                                       ((string)null), //Folio
                                       (pagoElectronico.PagElec_Importe - importe),
                                       (pagoElectronico.PagElec_Cc),
                                       (pagoElectronico.PagElec_Numero), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                        (pagoElectronico.PagElec_Cuenta),
                                       (pagoElectronico.PagElec_SubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                       (pagoElectronico.PagElec_SubSubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                       (pagoElectronico.PagElec_CuentaPago), // == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                       (pagoElectronico.PagElec_Observaciones), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                       (pagoElectronico.Id_PagElecCuenta), // == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                       (""), // == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                       (""), // "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                        (pagoElectronico.PagElec_Importe - importe), //(DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                        (0), //(DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                       (0), //(DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                       (0) // (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"])
                                        , ("Sin Comprobante")
                                       )
                                       );

                       }
                       //JFCV 8 NOV 2016 FIN agregar el archivo de soporte al listado de comprobantes 

                       list.Add(pagoElectronico);
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
               
           }
     



        public void ConsultaPagoElectronico(PagoElectronico pagoElectronico, string Conexion)
        {
            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    DataSet DS = oDB.spExecDataSet(
                        "spCapPagoElectronico_Consulta",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                    );

                   
                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        pagoElectronico.Id_Emp = (int)DR["Id_Emp"];
                        pagoElectronico.Id_Cd = (int)DR["Id_Cd"];
                        pagoElectronico.Id_PagElec = (int)DR["Id_PagElec"];
                        pagoElectronico.Id_PagElecTipo = (int)DR["Id_PagElecTipo"];
                        pagoElectronico.Id_PagElecSubTipo = (int)DR["Id_PagElecSubTipo"];
                        pagoElectronico.PagElecTipo_Descrpcion = DR["PagElecTipo_Descrpcion"].ToString();
                        pagoElectronico.Id_PagElecCuenta = (int)DR["Id_PagElecCuenta"];
                        pagoElectronico.pagElecCuenta_Descripcion = DR["pagElecCuenta_Descripcion"].ToString();
                        pagoElectronico.Id_AcrCheque = (int)DR["Id_AcrCheque"];
                        pagoElectronico.AcrCheque_Nombre = DR["AcrCheque_Nombre"].ToString();
                        pagoElectronico.Id_Acr = (int)DR["Id_Acr"];
                        pagoElectronico.Acr_Nombre = DR["Acr_Nombre"].ToString();
                        pagoElectronico.PagElec_Solicitante = DR["PagElec_Solicitante"].ToString();
                        pagoElectronico.PagElec_FechaRequiere = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_Cuenta = DR["PagElec_Cuenta"].ToString();
                        pagoElectronico.PagElec_Cc = DR["PagElec_Cc"].ToString();
                        pagoElectronico.PagElec_Numero = DR["PagElec_Numero"].ToString();
                        pagoElectronico.PagElec_SubCuenta = DR["PagElec_SubCuenta"].ToString();
                        pagoElectronico.PagElec_SubSubCuenta = DR["PagElec_SubSubCuenta"].ToString();
                        pagoElectronico.PagElec_CuentaPago = DR["PagElec_CuentaPago"].ToString();
                        pagoElectronico.PagElec_Importe = decimal.Parse(DR["PagElec_Importe"].ToString());
                        pagoElectronico.PagElec_Observaciones = DR["PagElec_Observaciones"].ToString();
                        pagoElectronico.PagElec_Autorizado = Boolean.Parse(DR["PagElec_Autorizado"].ToString());
                        pagoElectronico.Id_PagElecEstatus = (int)DR["Id_PagElecEstatus"];
                        pagoElectronico.PagElecEstatus_Descripcion = DR["PagElecEstatus_Descripcion"].ToString();
                        pagoElectronico.PagElec_Xml = DR["PagElec_Xml"].ToString();
                        pagoElectronico.PagElec_Pdf = DR["PagElec_Pdf"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Pdf"]);
                        pagoElectronico.PagElec_Soporte = DR["PagElec_Soporte"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Soporte"]);
                        pagoElectronico.PagElec_Soporte_Nombre = DR["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(DR["PagElec_Soporte_Nombre"]);
                        pagoElectronico.PagElec_Soporte_Tipo = DR["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(DR["PagElec_Soporte_Tipo"]);
                        //JFCV 03nov2015 agregue la fecha de salida
                        pagoElectronico.PagElec_FechaSalida = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_FechaUltMod = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        //JFCV 17 Dic 2015 Agregar Destino y Motivo rechazo
                        pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                        pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString();
                        pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                        pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString(); 
                        pagoElectronico.Id_GV = DR["Id_GV"] == System.DBNull.Value ? 0 : (int)(DR["Id_GV"]);
                        //pagoElectronico.PagElec_FechaRegreso = DateTime.Parse(DR["GV_FechaRegreso"].ToString());
                        pagoElectronico.PagElec_FechaRegreso = DR["GV_FechaRegreso"] == System.DBNull.Value ? DateTime.Parse(DR["PagElec_FechaRequiere"].ToString()) : (DateTime)(DR["GV_FechaRegreso"]);
                        //JFCV 04 feb 2016
                        pagoElectronico.PagElec_SoporteImporte = decimal.Parse(DR["PagElec_SoporteImporte"].ToString());
                      

                        DataSet DSFile = oDB.spExecDataSet(
                            "spCapPagoElectronicoArchivos_Lista",
                            new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                            new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                            new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                        );

                         decimal? importe = 0;
                         int? partida = 0;

                        if (DSFile != null && DSFile.Tables.Count > 0 && DSFile.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DRFile in DSFile.Tables[0].Rows)
                            {
                                pagoElectronico.PagElecArchivo.Add(
                                    new PagoElectronicoArchivo(
                                        (int?)pagoElectronico.Id_Emp,
                                        (int?)pagoElectronico.Id_Cd,
                                        (int?)pagoElectronico.Id_PagElec,
                                        (DRFile["Partida"].GetType().Name == "DBNull" ? (int?)null : (int?)DRFile["Partida"]),
                                        (DRFile["PDFStream"].GetType().Name == "DBNull" ? (byte[])null : (byte[])DRFile["PDFStream"]),
                                        (DRFile["XMLStream"].GetType().Name == "DBNull" ? (byte[])null : (byte[])DRFile["XMLStream"]),
                                        (DRFile["FechaFactura"].GetType().Name == "DBNull" ? (DateTime?)null : (DateTime?)DRFile["FechaFactura"]),
                                        (DRFile["Serie"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["Serie"]),
                                        (DRFile["Folio"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["Folio"]),
                                        (DRFile["Importe"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["Importe"]),
                                        (DRFile["PagElec_Cuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                        (DRFile["PagElec_Cc"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cc"]),
                                        (DRFile["PagElec_Numero"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Numero"]),
                                        (DRFile["PagElec_SubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                        (DRFile["PagElec_SubSubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                        (DRFile["PagElec_CuentaPago"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                        (DRFile["PagElec_Observaciones"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                        (DRFile["PagElec_Id_PagElecCuenta"].GetType().Name == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                        (DRFile["PagElec_RFC"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                    //JFCV  02 feb 2016 
                                        (DRFile["PagElec_UUID"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                        (DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                        (DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                        (DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                        (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"]),
                                        ("Con Comprobante")
                                    )
                                );
                                 if (DRFile["Importe"].GetType().Name != "DBNull" )
                                {
                                    importe = importe + (decimal?)DRFile["Importe"];
                                    partida ++;
                                }
                            }
                        }
                        else
                        {
                            pagoElectronico.PagElecArchivo = new List<PagoElectronicoArchivo>();
                        }

                        //JFCV 8 NOV 2016 INICIO agregar el archivo de soporte al listado de comprobantes 
                        //AGREGAR UNA COLUMNA QUE SEA CON COMPROBANTE O SIN COMPROBANTE 
                        if (pagoElectronico.PagElec_Soporte != null)
                        {
                            //jfcv 20 dic agregar el soporte del archivo en el campo de pdf 
                            pagoElectronico.PagElec_Pdf = pagoElectronico.PagElec_Soporte;
                            partida++;
                            pagoElectronico.PagElecArchivo.Add(
                                    new PagoElectronicoArchivo(
                                        (int?)pagoElectronico.Id_Emp,
                                        (int?)pagoElectronico.Id_Cd,
                                        (int?)pagoElectronico.Id_PagElec,
                                        partida, //partida
                                        pagoElectronico.PagElec_Pdf,
                                        (byte[])null, //XML
                                        pagoElectronico.PagElec_FechaRequiere,
                                        ((string)null), //Serie
                                        ((string)null), //Folio
                                        (pagoElectronico.PagElec_Importe - importe),
                                        (pagoElectronico.PagElec_Cuenta),
                                        (pagoElectronico.PagElec_Cc),
                                        (pagoElectronico.PagElec_Numero), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                        (pagoElectronico.PagElec_SubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                        (pagoElectronico.PagElec_SubSubCuenta), // == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                        (pagoElectronico.PagElec_CuentaPago), // == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                        (pagoElectronico.PagElec_Observaciones), // == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                        (pagoElectronico.Id_PagElecCuenta), // == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                        (""), // == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                        (""), // "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                         (pagoElectronico.PagElec_Importe - importe), //(DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                         (0), //(DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                        (0), //(DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                        (0) // (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"])
                                         ,("Sin Comprobante")
                                        )
                                        );
                        }
                        //JFCV 8 NOV 2016 FIN agregar el archivo de soporte al listado de comprobantes 
                        


                    }
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
        }

        //jfcv 17 enero 2016 agregue porque al autorizar se revolvia con los archivos reales y el de soporte 
        public void ConsultaPagoElectronicoAutorizacion(PagoElectronico pagoElectronico, string Conexion)
        {
            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    DataSet DS = oDB.spExecDataSet(
                        "spCapPagoElectronico_Consulta",
                        new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                        new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                        new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        pagoElectronico.Id_Emp = (int)DR["Id_Emp"];
                        pagoElectronico.Id_Cd = (int)DR["Id_Cd"];
                        pagoElectronico.Id_PagElec = (int)DR["Id_PagElec"];
                        pagoElectronico.Id_PagElecTipo = (int)DR["Id_PagElecTipo"];
                        pagoElectronico.Id_PagElecSubTipo = (int)DR["Id_PagElecSubTipo"];
                        pagoElectronico.PagElecTipo_Descrpcion = DR["PagElecTipo_Descrpcion"].ToString();
                        pagoElectronico.Id_PagElecCuenta = (int)DR["Id_PagElecCuenta"];
                        pagoElectronico.pagElecCuenta_Descripcion = DR["pagElecCuenta_Descripcion"].ToString();
                        pagoElectronico.Id_AcrCheque = (int)DR["Id_AcrCheque"];
                        pagoElectronico.AcrCheque_Nombre = DR["AcrCheque_Nombre"].ToString();
                        pagoElectronico.Id_Acr = (int)DR["Id_Acr"];
                        pagoElectronico.Acr_Nombre = DR["Acr_Nombre"].ToString();
                        pagoElectronico.PagElec_Solicitante = DR["PagElec_Solicitante"].ToString();
                        pagoElectronico.PagElec_FechaRequiere = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_Cuenta = DR["PagElec_Cuenta"].ToString();
                        pagoElectronico.PagElec_Cc = DR["PagElec_Cc"].ToString();
                        pagoElectronico.PagElec_Numero = DR["PagElec_Numero"].ToString();
                        pagoElectronico.PagElec_SubCuenta = DR["PagElec_SubCuenta"].ToString();
                        pagoElectronico.PagElec_SubSubCuenta = DR["PagElec_SubSubCuenta"].ToString();
                        pagoElectronico.PagElec_CuentaPago = DR["PagElec_CuentaPago"].ToString();
                        pagoElectronico.PagElec_Importe = decimal.Parse(DR["PagElec_Importe"].ToString());
                        pagoElectronico.PagElec_Observaciones = DR["PagElec_Observaciones"].ToString();
                        pagoElectronico.PagElec_Autorizado = Boolean.Parse(DR["PagElec_Autorizado"].ToString());
                        pagoElectronico.Id_PagElecEstatus = (int)DR["Id_PagElecEstatus"];
                        pagoElectronico.PagElecEstatus_Descripcion = DR["PagElecEstatus_Descripcion"].ToString();
                        pagoElectronico.PagElec_Xml = DR["PagElec_Xml"].ToString();
                        pagoElectronico.PagElec_Pdf = DR["PagElec_Pdf"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Pdf"]);
                        pagoElectronico.PagElec_Soporte = DR["PagElec_Soporte"] == System.DBNull.Value ? null : (byte[])(DR["PagElec_Soporte"]);
                        pagoElectronico.PagElec_Soporte_Nombre = DR["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(DR["PagElec_Soporte_Nombre"]);
                        pagoElectronico.PagElec_Soporte_Tipo = DR["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(DR["PagElec_Soporte_Tipo"]);
                        //JFCV 03nov2015 agregue la fecha de salida
                        pagoElectronico.PagElec_FechaSalida = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        pagoElectronico.PagElec_FechaUltMod = DateTime.Parse(DR["PagElec_FechaRequiere"].ToString());
                        //JFCV 17 Dic 2015 Agregar Destino y Motivo rechazo
                        pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                        pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString();
                        pagoElectronico.PagElec_Destino = DR["PagElec_Destino"].ToString();
                        pagoElectronico.PagElec_MotivoRechazo = DR["PagElec_MotivoRechazo"].ToString();
                        pagoElectronico.Id_GV = DR["Id_GV"] == System.DBNull.Value ? 0 : (int)(DR["Id_GV"]);
                        //pagoElectronico.PagElec_FechaRegreso = DateTime.Parse(DR["GV_FechaRegreso"].ToString());
                        pagoElectronico.PagElec_FechaRegreso = DR["GV_FechaRegreso"] == System.DBNull.Value ? DateTime.Parse(DR["PagElec_FechaRequiere"].ToString()) : (DateTime)(DR["GV_FechaRegreso"]);
                        //JFCV 04 feb 2016
                        pagoElectronico.PagElec_SoporteImporte = decimal.Parse(DR["PagElec_SoporteImporte"].ToString());


                        DataSet DSFile = oDB.spExecDataSet(
                            "spCapPagoElectronicoArchivos_Lista",
                            new SqlParameter("@Id_Emp", pagoElectronico.Id_Emp),
                            new SqlParameter("@Id_Cd", pagoElectronico.Id_Cd),
                            new SqlParameter("@Id_PagElec", pagoElectronico.Id_PagElec)
                        );

                        decimal? importe = 0;
                        int? partida = 0;

                        if (DSFile != null && DSFile.Tables.Count > 0 && DSFile.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow DRFile in DSFile.Tables[0].Rows)
                            {
                                pagoElectronico.PagElecArchivo.Add(
                                    new PagoElectronicoArchivo(
                                        (int?)pagoElectronico.Id_Emp,
                                        (int?)pagoElectronico.Id_Cd,
                                        (int?)pagoElectronico.Id_PagElec,
                                        (DRFile["Partida"].GetType().Name == "DBNull" ? (int?)null : (int?)DRFile["Partida"]),
                                        (DRFile["PDFStream"].GetType().Name == "DBNull" ? (byte[])null : (byte[])DRFile["PDFStream"]),
                                        (DRFile["XMLStream"].GetType().Name == "DBNull" ? (byte[])null : (byte[])DRFile["XMLStream"]),
                                        (DRFile["FechaFactura"].GetType().Name == "DBNull" ? (DateTime?)null : (DateTime?)DRFile["FechaFactura"]),
                                        (DRFile["Serie"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["Serie"]),
                                        (DRFile["Folio"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["Folio"]),
                                        (DRFile["Importe"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["Importe"]),
                                        (DRFile["PagElec_Cuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cuenta"]),
                                        (DRFile["PagElec_Cc"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Cc"]),
                                        (DRFile["PagElec_Numero"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Numero"]),
                                        (DRFile["PagElec_SubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubCuenta"]),
                                        (DRFile["PagElec_SubSubCuenta"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_SubSubCuenta"]),
                                        (DRFile["PagElec_CuentaPago"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_CuentaPago"]),
                                        (DRFile["PagElec_Observaciones"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_Observaciones"]),
                                        (DRFile["PagElec_Id_PagElecCuenta"].GetType().Name == "DBNull" ? (int?)null : (int?)DRFile["PagElec_Id_PagElecCuenta"]),
                                        (DRFile["PagElec_RFC"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_RFC"]),
                                    //JFCV  02 feb 2016 
                                        (DRFile["PagElec_UUID"].GetType().Name == "DBNull" ? (string)null : (string)DRFile["PagElec_UUID"]),
                                        (DRFile["PagElec_Subtotal"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Subtotal"]),
                                        (DRFile["PagElec_Iva"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_Iva"]),
                                        (DRFile["PagElec_ImpRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_ImpRetenido"]),
                                        (DRFile["PagElec_IvaRetenido"].GetType().Name == "DBNull" ? (decimal?)null : (decimal?)DRFile["PagElec_IvaRetenido"]),
                                        ("Con Comprobante")
                                    )
                                );
                                if (DRFile["Importe"].GetType().Name != "DBNull")
                                {
                                    importe = importe + (decimal?)DRFile["Importe"];
                                    partida++;
                                }
                            }
                        }
                        else
                        {
                            pagoElectronico.PagElecArchivo = new List<PagoElectronicoArchivo>();
                        }
                    }
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
        }


        public string ConsultaEmpRFC(int id_Emp, string Conexion)
        {
            string Result = null;

            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    Result = (string)oDB.spExecScalar(
                        "spCatEmpresa_ConsultaRFC",
                        new SqlParameter("@Id_Emp", id_Emp)
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

        //JFCV 13 ene 2016 cambiar estatus pago electrónico 
        public void CambiarEstatusPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_PagElec",
                                        "@Id_Estatus"
                                      };

                object[] Valores = { 
                                       pagoElectronico.Id_Emp,
                                       pagoElectronico.Id_Cd,
                                       pagoElectronico.Id_PagElec,
                                       pagoElectronico.Id_PagElecEstatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoElectronico_CambiarEstatus", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
