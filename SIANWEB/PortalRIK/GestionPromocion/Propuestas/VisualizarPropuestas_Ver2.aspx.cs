using System;
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
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

using System.Data;
using System.Data.SqlClient;

namespace SIANWEB.PortalRIK.GestionPromocion.Propuestas
{
    public partial class VisualizarPropuesta_Ver2 : System.Web.UI.Page
    {
        protected IEnumerable<CrmPropuestaTecnica> _DetallePropuesta = null;
        protected IEnumerable<CrmOportunidadesProducto> _DetallePropuestaEconomica = null;
        
        protected int? _idCte = null;
        protected int? _idVal = null;
        protected bool _parametrosConsultaDetalleValidos = false;
        
        public int? IdCte
        {
            get
            {
                if (_idCte == null)
                {
                    string strIdCte = Request["idCte"];
                    try
                    {
                        _idCte = int.Parse(strIdCte);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return _idCte;
            }
        }

        public int? IdVal
        {
            get
            {
                if (_idVal == null)
                {
                    string strIdVal = Request["idVal"];
                    try
                    {
                        _idVal = int.Parse(strIdVal);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return _idVal;
            }
        }


        /// <summary>
        /// Devuelve el detalle de la propuesta técnica
        /// </summary>
        public IEnumerable<CrmPropuestaTecnica> DetallePropuesta
        {
            get
            {
                if (_DetallePropuesta == null)
                {
                    if (_parametrosConsultaDetalleValidos)
                    {
                        CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                        //_DetallePropuesta = cnCrmPropuestaTecnica.ObtenerReportePropuestaTecnica(EntidadSesion, IdCte.Value, IdVal.Value, _ibt);
                    }
                }
                return _DetallePropuesta;
            }
        }

        /// <summary>
        /// Devuelve el detalle de la propuesta económica
        /// </summary>
        public IEnumerable<CrmOportunidadesProducto> DetallePropuestaEconomica
        {
            get
            {
                if (_DetallePropuestaEconomica == null)
                {
                    if (_parametrosConsultaDetalleValidos)
                    {
                        CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                        //_DetallePropuestaEconomica = cnCrmOportunidadesProductos.ObtenerPropuestaEconomica(EntidadSesion, IdVal.Value, IdCte.Value, _ibt);
                    }
                }
                return _DetallePropuestaEconomica;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            string strImp;
            strImp = Request["__EVENTARGUMENT"];
            try
            {
                if (strImp.ToString() == "3")
                {
                    hfImprimiendo.Value = "3";

                    pnlAciones.Visible = true;
                    pnlPropuesta.Visible = false;
                    pnlVisorDeReporte.Visible = true;

                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSetPTecnica());
                    ReportViewer1.LocalReport.DataSources.Add(dsPage1);

                    string sRep = Server.MapPath("../../Reportes/") + "rptPTETecnica.rdlc";

                    ReportViewer1.LocalReport.ReportPath = sRep;
                    ReportViewer1.LocalReport.Refresh();
                }

                if (strImp.ToString() == "2")
                {
                    hfImprimiendo.Value = "2";

                    pnlAciones.Visible = true;
                    pnlPropuesta.Visible = false;
                    pnlVisorDeReporte.Visible = true;

                    ReportViewer1.Reset();
                    ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1());
                    ReportViewer1.LocalReport.DataSources.Add(dsPage1);

                    string sRep = Server.MapPath("../../Reportes/") + "rptPTEProductos.rdlc";

                    ReportViewer1.LocalReport.ReportPath = sRep;
                    ReportViewer1.LocalReport.Refresh();
                }

                if (strImp.ToString() == "1")
                {
                    hfImprimiendo.Value = "1";

                    pnlAciones.Visible = true;
                    pnlPropuesta.Visible = false;
                    pnlVisorDeReporte.Visible = true;

                    ReportViewer1.Reset();
                    ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1());
                    ReportViewer1.LocalReport.DataSources.Add(dsPage1);

                    string sRep = Server.MapPath("../../Reportes/") + "rptPTEEncabezado.rdlc";

                    ReportViewer1.LocalReport.ReportPath = sRep;
                    ReportViewer1.LocalReport.Refresh();

                }
            }
            catch
            {
                hfImprimiendo.Value = "0";

                pnlAciones.Visible = true;
                pnlPropuesta.Visible = true;
                pnlVisorDeReporte.Visible = false;
            }

            //validar si la valuación se encuentra en un estado válido para la generación de las propuestas
            _parametrosConsultaDetalleValidos = IdCte != null && IdVal != null;
            if (!_parametrosConsultaDetalleValidos)
            {
                //falta: arrojar excepción
            }
            
        }


        public DataTable GetDataSet1()
        {
            //CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
            //var dpe = cnCrmOportunidadesProductos.ObtenerPropuestaEconomica(EntidadSesion, IdVal.Value, IdCte.Value, _ibt);

            DataTable dt = new DataTable();

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

            List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();

            CN_CrmPropuestaEconomica cn = new CN_CrmPropuestaEconomica();
            lst = cn.CRM_ObtenerPropuestaEconomica(0, 0, 0, 0, 0, EntidadSesion);

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
                row["Id_Cd"] = item.Prd_Presentacion ; // Presentacion
                row["Id_Op"] = item.COP_ConsumoMensual;//  .  //  item.COP_Cantidad; //CONSUMO MENSUAL(UNIDADES)
                row["Id_Cte"] = item.COP_ConsumoMensual; // .COP_ConsumoMensual; //CONSUMO MENSUAL(UNIDADES)
                row["Id_Rik"] = item.COP_DilucionAntecedente; //  .DilucionCompuesta; // DILUCION

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
                row["Id_Uen"] = string.Format("{0:C}", fConsumoMensual);  //element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual         // CONSUMO MENSUAL(L Diluidos);

                float.TryParse(item.COP_ConsumoMensual.ToString(), out fConsumoMensual);
                float fDilucion = 0;

                try
                {
                    float.TryParse(item.COP_DilucionAntecedente.ToString(), out  fDilucion);
                }
                catch
                {
                    fDilucion = 0;
                }

                float fConsumoMensual_LDiluidos = fConsumoMensual * (fDilucion + 1);

                row["Id_Seg"] = fConsumoMensual_LDiluidos;  // CONSUMO MENSUAL DILUIDOS

                // CALCULO DE COSTO EN USO
                bool bEsQuimico = false;
                //bEsQuimico = (bool)item.COP_EsQuimico;
                if (item.COP_EsQuimico == 1)
                {
                    bEsQuimico = true;
                }

                float bConsumoMensual = 0;
                float.TryParse(item.COP_ConsumoMensual.ToString(), out bConsumoMensual);

                /*float fCostoEnUso = 0;
                try {
                    float.TryParse(item.COP_ConsumoMensual.ToString() , out fCostoEnUso);
                } catch {
                    fCostoEnUso=0;
                }*/

                float fCostoEnUso = 0;
                fPrecio = 0;

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
                            float.TryParse(item.Prd_UniEmp.ToString(), out fUnidadesPresentacion);
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
                            }*/
                        }
                    }
                }

                row["Id_Area"] = string.Format("{0:C}", fCostoEnUso);
                row["Id_Sol"] = 0;
                row["Id_Apl"] = 0;
                row["Id_SubFam"] = 0;
                row["Id_Prd"] = item.Id_Prd;
                row["COP_Cantidad"] = 0;
                row["COP_Dilucion"] = 0;
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

            return dt;
        }


        public DataTable GetDataSetPTecnica()
        {
            DataTable dt = new DataTable();

            try
            {

                //CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                //_DetallePropuesta = cnCrmPropuestaTecnica.ObtenerReportePropuestaTecnica(EntidadSesion, IdCte.Value, IdVal.Value, _ibt);


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

                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();

                CN_CrmPropuestaEconomica cn = new CN_CrmPropuestaEconomica();
                lst  = cn.CRM_ObtenerPropuestaEconomica(0, 0, 0, 0, 0, EntidadSesion);


                foreach (var item in _DetallePropuesta)
                {
                    var row = dt.NewRow();
                    row["Descripcion"] = item.CPT_ProductoActual == null ? "" : item.CPT_ProductoActual.ToString();
                    row["Nombre"] = item.CPT_RecursoImagenProductoActual == null ? "" : item.CPT_RecursoImagenProductoActual.ToString();
                    row["Ruta"] = item.CPT_RecursoImagenSolucionKey == null ? "" : item.CPT_RecursoImagenSolucionKey.ToString();
                    row["ProductoSerializable"] = item.CPT_SituacionActual == null ? "" : item.CPT_SituacionActual.ToString();
                    row["ProductoActual"] = item.CPT_VentajasKey == null ? "" : item.CPT_VentajasKey.ToString();
                    row["ProductoActual2"] = item.CatProducto.Prd_Descripcion == null ? "" : item.CatProducto.Prd_Descripcion.ToString();
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
                    row["COP_Dilucion"] = 0;
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

        /// <summary>
        /// Regresa la sesión sensitiva al usuario en operación.
        /// </summary>
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
        
    }
}