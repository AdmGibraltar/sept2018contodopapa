﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaModelo;
using CapaEntidad;
using Newtonsoft.Json;
using SIANWEB.Core.UI;
using Telerik.Web.UI;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

using System.Data;
using System.Data.SqlClient;


namespace SIANWEB.PortalRIK.GestionPromocion.Propuestas
{
    public partial class VisorReportesPropuestaTecnoEconomica : BaseServerPage
    {
        private int IdCliente;
        private int IdValuacion;
        protected IEnumerable<CrmPropuestaTecnica> _DetallePropuesta = null;
        protected IEnumerable<CrmOportunidadesProducto> _DetallePropuestaEconomica = null;
                
        protected void Page_Load(object sender, EventArgs e)
        {
            int IdRik;
            string IdCte;
            string IdVal;
            string IdOp;
            string IdTipoRep;
            int idTipoRep =0;
            
            Int32.TryParse(Request.QueryString["IdRik"].ToString(), out IdRik);                     

            IdTipoRep = Request.QueryString["idTipoRep"].ToString();
            IdCte = Request.QueryString["idCte"].ToString();
            IdVal = Request.QueryString["idVal"].ToString();
            IdOp = Request.QueryString["idOp"].ToString();

            Int32.TryParse(IdTipoRep, out idTipoRep);
            Int32.TryParse(IdCte , out IdCliente);
            Int32.TryParse(IdVal, out IdValuacion);

            if (!IsPostBack)
            {            
                string strImp;
                strImp = Request["__EVENTARGUMENT"];
                try
                {
                    if (idTipoRep == 3)
                    {                                     
                        ReportViewer1.Reset();
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSetPTecnica(IdRik));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTETecnica.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        ReportViewer1.LocalReport.Refresh();
                    }

                    if (idTipoRep == 2)
                    {                    
                        ReportViewer1.Reset();
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1(IdRik));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTEProductos.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        ReportViewer1.LocalReport.Refresh();
                    }

                    if (idTipoRep == 1)
                    {                    
                        ReportViewer1.Reset();
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1(IdRik));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTEEncabezado.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                catch
                { 
               
                }
            }
            
        }

        // PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA
        public DataTable GetDataSetPTecnica(int IdRik)
        {
            DataTable dt = new DataTable();

            try
            {

                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();

                //lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, EntidadSesion.Id_Rik, IdValuacion, EntidadSesion);
                lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, IdRik, IdValuacion, EntidadSesion);

                //CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                //_DetallePropuesta = cnCrmPropuestaTecnica.ObtenerReportePropuestaTecnica(EntidadSesion, IdCliente, IdValuacion, _ibt);
                
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Ruta");
                dt.Columns.Add("ProductoSerializable");
                dt.Columns.Add("ProductoActual");
                dt.Columns.Add("ProductoActual2");
                dt.Columns.Add("DilucionCompuesto");
                dt.Columns.Add("CostoEnUso");
                dt.Columns.Add("CapValProyectoDet");
                dt.Columns.Add("Context");
                dt.Columns.Add("Id_Emp");
                dt.Columns.Add("Id_Cd");
                dt.Columns.Add("Id_Op");
                dt.Columns.Add("Id_Cte");
                dt.Columns.Add("Id_Rik");
                dt.Columns.Add("Id_Uen");
                dt.Columns.Add("Id_Seg");
                dt.Columns.Add("Id_Area");
                dt.Columns.Add("Id_Sol");
                dt.Columns.Add("Id_Apl");
                dt.Columns.Add("Id_SubFam");
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("COP_Cantidad");
                dt.Columns.Add("COP_Dilucion");
                dt.Columns.Add("COP_EsQuimico");
                dt.Columns.Add("COP_CostoEnUso");
                dt.Columns.Add("COP_ConsumoMensual");
                dt.Columns.Add("COP_DilucionAntecedente");
                dt.Columns.Add("COP_DilucionConsecuente");
                dt.Columns.Add("CatSegmento");
                dt.Columns.Add("CatSolucion");
                dt.Columns.Add("CatUEN");
                dt.Columns.Add("CatProducto");
                dt.Columns.Add("CatArea");
                dt.Columns.Add("CatAplicacion");

                foreach (var item in lst)
                {
                
                    var row = dt.NewRow();
                    row["Descripcion"] = item.CPT_ProductoActual== null ? "" : item.CPT_ProductoActual.ToString();
                    row["Nombre"] = item.CPT_RecursoImagenProductoActual== null ? "" : item.CPT_RecursoImagenProductoActual.ToString();
                    row["Ruta"] = item.CPT_RecursoImagenSolucionKey == null ? "" : item.CPT_RecursoImagenSolucionKey.ToString();
                    row["ProductoSerializable"] = item.CPT_SituacionActual == null ? "" : item.CPT_SituacionActual.ToString();
                    row["ProductoActual"] = item.CPT_VentajasKey == null ? "" : item.CPT_VentajasKey.ToString();
                    row["ProductoActual2"] = item.Prd_Descripcion == null ? "" : item.Prd_Descripcion;
                    row["DilucionCompuesto"] = 0;
                    row["CostoEnUso"] = 0;
                    row["CapValProyectoDet"] = 0;
                    row["Context"] = 0;
                    row["Id_Emp"] = 0; //Precio
                    row["Id_Cd"] = 0; // Presentacion
                    row["Id_Op"] = 0; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Cte"] = 0; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Rik"] = 0; // DILUCION
                    row["Id_Uen"] = 0;  //element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual         // CONSUMO MENSUAL(L Diluidos);
                    row["Id_Seg"] = 0;  // CONSUMO MENSUAL DILUIDOS
                    row["Id_Area"] = 0;
                    row["Id_Sol"] = 0;
                    row["Id_Apl"] = 0;
                    row["Id_SubFam"] = 0;
                    row["Id_Prd"] = 0;
                    row["COP_Cantidad"] = 0;

                    if (item.AplDilucion == 1)
                    {
                        row["COP_Dilucion"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        row["COP_Dilucion"] = "N/A";
                    }                   


                    row["COP_EsQuimico"] = 0;
                    row["COP_CostoEnUso"] = 0;
                    row["COP_ConsumoMensual"] = 0;
                    row["COP_DilucionAntecedente"] = 0;
                    row["COP_DilucionConsecuente"] = 0;
                    row["CatSegmento"] = 0;
                    row["CatSolucion"] = 0;
                    row["CatUEN"] = 0;
                    row["CatProducto"] = 0;
                    row["CatArea"] = 0;
                    row["CatAplicacion"] = 0;
                    dt.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        // PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA - PROPUESTA TECNICA 
        public DataTable GetDataSet1(int IdRik)
        {
            DataTable dt = new DataTable();
            try
            {
                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();

                lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, IdRik, IdValuacion, EntidadSesion);
                //lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, EntidadSesion.Id_Rik, IdValuacion, EntidadSesion);
                
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Ruta");
                dt.Columns.Add("ProductoSerializable");
                dt.Columns.Add("ProductoActual");
                dt.Columns.Add("ProductoActual2");
                dt.Columns.Add("DilucionCompuesto");
                dt.Columns.Add("CostoEnUso");
                dt.Columns.Add("CapValProyectoDet");
                dt.Columns.Add("Context");
                dt.Columns.Add("Id_Emp");
                dt.Columns.Add("Id_Cd");
                dt.Columns.Add("Id_Op");
                dt.Columns.Add("Id_Cte");
                dt.Columns.Add("Id_Rik");
                dt.Columns.Add("Id_Uen");
                dt.Columns.Add("Id_Seg");
                dt.Columns.Add("Id_Area");
                dt.Columns.Add("Id_Sol");
                dt.Columns.Add("Id_Apl");
                dt.Columns.Add("Id_SubFam");
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("COP_Cantidad");
                dt.Columns.Add("COP_Dilucion");
                dt.Columns.Add("COP_EsQuimico");
                dt.Columns.Add("COP_CostoEnUso");
                dt.Columns.Add("COP_ConsumoMensual");
                dt.Columns.Add("COP_DilucionAntecedente");
                dt.Columns.Add("COP_DilucionConsecuente");
                dt.Columns.Add("CatSegmento");
                dt.Columns.Add("CatSolucion");
                dt.Columns.Add("CatUEN");
                dt.Columns.Add("CatProducto");
                dt.Columns.Add("CatArea");
                dt.Columns.Add("CatAplicacion");

                foreach (var item in lst)
                {
                    var row = dt.NewRow();
                    row["Descripcion"] = item.Prd_Descripcion;
                    row["Nombre"] = 0;
                    row["Ruta"] = 0;
                    row["ProductoSerializable"] = 0;
                    row["ProductoActual"] = 0;
                    row["ProductoActual2"] = 0;
                    row["DilucionCompuesto"] = 0;
                    row["CostoEnUso"] = 0;
                    row["CapValProyectoDet"] = 0;
                    row["Context"] = 0;
                    float fPrecio = 0;
                    float.TryParse(item.Vap_Precio.ToString(), out fPrecio);
                    row["Id_Emp"] = string.Format("{0:C}", fPrecio); //Precio
                    row["Id_Cd"] = item.Prd_Presentacion; // Presentacion
                    row["Id_Op"] = item.COP_ConsumoMensual; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Cte"] = item.ConsumoMensualLDiluidos; //CONSUMO MENSUAL(UNIDADES)

                    row["Id_Rik"] = 0; 

                    /*
                    float.TryParse(item.Vap_Precio.ToString(), out fPrecio);
                    float fCant = 0;
                    float.TryParse(item.COP_ConsumoMensual.ToString(), out fCant);
                    float fConsumoMensual = 0;
                    try
                    {
                        fConsumoMensual = fPrecio * fCant;
                    }
                    catch
                    {
                        fConsumoMensual = 0;
                    }
                    */
                    row["Id_Uen"] = string.Format("{0:C}", item.ConsumoMensualLDiluidos);  //element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual         // CONSUMO MENSUAL(L Diluidos);
/*
                    float.TryParse(item.COP_ConsumoMensual.ToString(), out fConsumoMensual);
                    float fDilucion = 0;

                    try
                    {
                        float.TryParse(item.COP_DilucionAntecedente.ToString(), out  fDilucion);
                    }
                    catch
                    {
                        fDilucion = 0;
                    }*/

                    //float fConsumoMensual_LDiluidos = fConsumoMensual * (fDilucion + 1);

                    row["Id_Seg"] = item.ConsumoMensualLDiluidos; // fConsumoMensual_LDiluidos;  // CONSUMO MENSUAL DILUIDOS

                    // CALCULO DE COSTO EN USO
                    //bool bEsQuimico = false;                    
                    //if (item.COP_EsQuimico==1)
                    //{
                    //                        bEsQuimico = true; 
                    //}

                    //float bConsumoMensual = 0;
                    //float.TryParse(item.COP_ConsumoMensual.ToString(), out bConsumoMensual);

                    /*float fCostoEnUso = 0;
                    try {
                        float.TryParse(item.COP_ConsumoMensual.ToString() , out fCostoEnUso);
                    } catch {
                        fCostoEnUso=0;
                    }*/

                    //float fCostoEnUso = 0;
                    //fPrecio = 0;

                    /*
                    if (bConsumoMensual > 0)
                    {
                        if (bEsQuimico == true)
                        {
                            if (item.COP_DilucionConsecuente.ToString() != "")
                            {
                                float fDilucionConsecuente = 0;
                                float.TryParse(item.COP_DilucionConsecuente.ToString(), out fDilucionConsecuente);

                                float.TryParse(item.Vap_Precio.ToString(), out fPrecio);
                                //var precio = dataItem.CapValProyectoDet.Vap_Precio;//ProductoActual.Prd_Pesos;

                                float fUnidadesPresentacion = 0;
                                float.TryParse(item. .ProductoSerializable.Prd_UniEmp.ToString(), out fUnidadesPresentacion);
                                //var unidadesPresentacion = dataItem.ProductoSerializable.Prd_UniEmp;

                                float fConsumoMensualEnLitrosDiluidos = 0;
                                fConsumoMensualEnLitrosDiluidos = (fUnidadesPresentacion * bConsumoMensual) * (fDilucionConsecuente + 1);
                                //var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * consumoMensual) * (parseInt(dilucionConsecuente) + 1));

                                if (fConsumoMensualEnLitrosDiluidos > 0)
                                {
                                    fCostoEnUso = (fConsumoMensual * fPrecio) / fConsumoMensualEnLitrosDiluidos;
                                }
                                /*if (consumoMensualEnLitrosDiluidos != 0.0)
                                {
                                    costoEnUso = (consumoMensual * precio) / consumoMensualEnLitrosDiluidos;
                                }*
                            }
                        }
                    }
                    */

                    row["Id_Area"] = string.Format("{0:C}",item.CostoEnUso);
                    row["Id_Sol"] = 0;
                    row["Id_Apl"] = 0;
                    row["Id_SubFam"] = 0;
                    row["Id_Prd"] = item.Id_Prd;
                    row["COP_Cantidad"] = 0;

                    if (item.AplDilucion == 1)
                    {
                        
                        row["COP_Dilucion"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        row["COP_Dilucion"] = "N/A";
                    }

                    row["COP_EsQuimico"] = 0;
                    row["COP_CostoEnUso"] = 0;
                    row["COP_ConsumoMensual"] = 0;
                    row["COP_DilucionAntecedente"] = 0;
                    row["COP_DilucionConsecuente"] = 0;
                    row["CatSegmento"] = 0;
                    row["CatSolucion"] = 0;
                    row["CatUEN"] = 0;
                    row["CatProducto"] = 0;
                    row["CatArea"] = 0;
                    row["CatAplicacion"] = 0;
                    dt.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        protected Sesion EntidadSesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        //
    }
}
