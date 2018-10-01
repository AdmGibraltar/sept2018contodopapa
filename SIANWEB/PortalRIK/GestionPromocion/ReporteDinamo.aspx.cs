using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIANWEB.Core.UI;
using System.Globalization;
using CapaNegocios;
using CapaEntidad;
using System.Configuration;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class ReporteDinamo : BaseServerPage
    {
        public string[] CurrentPath
        {
            get;
            set;
        }

        public Sesion session
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

        private string Conexion
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionSIANCentralCRMDinamo"); }
        }

        public ReporteDinamoViewModel ViewModel
        {
            get
            {
                ReporteDinamoViewModel vm = ViewState["_viewmodelstate_"] as ReporteDinamoViewModel;
                _ViewModel = vm;
                if (vm == null)
                {
                    _ViewModel = new ReporteDinamoViewModel();
                    ViewState.Add("_viewmodelstate_", _ViewModel);
                }
                return _ViewModel;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EntidadSesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = Page.Request.Url.PathAndQuery; //pag[pag.Length - 1];
                Response.Redirect("~/login.aspx", true);
            }

            CurrentPath = new List<string>() { "Gestion de la Promoción", "Reporte DINAMO" }.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 7;

                ViewModel.AnoInicial = DateTime.Now.Year;
                ViewModel.AnyoElegido = ViewModel.AnoInicial;
                GenerarListadoAnos();

                //rptFiltroAnyos.DataSource = ViewModel.Anos;
                //rptFiltroAnyos.DataBind();

                ddlAnyo.DataSource = ViewModel.Anos;
                ddlAnyo.DataBind();

                GenerarListadoMeses();

                //rptFiltroMesesSP.DataSource = ViewModel.Meses;
                //rptFiltroMesesSP.DataBind();

                ddlMes.DataSource = ViewModel.Meses;
                ViewModel.MesElegido = "Enero";
                ddlMes.DataBind();

                ConfigurarDatosDeAtributosDeCliente();
            }
        }

        protected void ddlAnyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            int anyoElegido = int.Parse(ddl.SelectedValue);
            ViewModel.AnyoElegido = anyoElegido;
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            ViewModel.MesElegido = ddl.SelectedItem.Text;
        }

        protected void ConfigurarDatosDeAtributosDeCliente()
        {
            ddlAnyo.Attributes.Add("data-width", "fit");
            ddlMes.Attributes.Add("data-width", "fit");
        }

        public void btnActualizar_Click(object sender, EventArgs e)
        {
            //CN_ReporteDINAMO cnReporteDINAMO = new CN_ReporteDINAMO();
            

            string strAnyo = ddlAnyo.SelectedValue;
            int anyo = int.Parse(strAnyo);
            string strMes = ddlMes.SelectedValue;
            int mes = int.Parse(strMes);
            ViewModel.NombreRepresentanteElegido = "";

            bool bTrimestral = false;
            int iCNC = 0;

            bTrimestral = chkPromedioTresMeses.Checked ? true : false;
            iCNC = rbCDC.Checked ? 2 : 1;

            int iTipoRik = 0;

            if (rbAntiguedadRIKTodos.Checked)
            {
                iTipoRik = 1;
            }
            if (rbAntiguedadMenor8Meses.Checked)
            {
                iTipoRik = 2;
            }
            if (rbAntiguedadRIKMayorIgual8Meses.Checked)
            {
                iTipoRik = 3;
            }

            bool bListadoRiks = chkListadoRIKS.Checked;

            //using (IBusinessTransaction ibSianCentral = CN_FabricaTransaccionNegocios.ParaSIANCentral(EntidadSesion))
            //{
                //ibSianCentral.Begin();
                //var fuente = cnReporteDINAMO.GenerarVistaGeneral(this.EntidadSesion, rbCDC.Checked ? 2 : 1, anyo, mes, chkPromedioTresMeses.Checked, ibSianCentral);

            CN_CRMDinamo cnReporte_DINAMO = new CN_CRMDinamo();

            if (bListadoRiks == true)
            {
                var fuente = cnReporte_DINAMO.spRepCRMC_onsolidadoRik(
                    this.EntidadSesion,
                    iCNC,
                    iTipoRik,
                    anyo,
                    mes,
                    bTrimestral,
                    this.Conexion
                );
                //rptReporte.DataSource = fuente;
                rptReportePorRIK.DataSource = fuente;

                //tblReportePorRik.Visible = true;

                reporteBlankSlate.Visible = false;
                dvContenedorDeReporte.Visible = true;
                tblReportePorCd.Visible = false;
                tblReportePorRik.Visible = true;
                tblReportePorProyecto.Visible = false;
                rptReportePorRIK.DataBind();
            }

            if (bListadoRiks == false)
            {
                reporteBlankSlate.Visible = false;
                dvContenedorDeReporte.Visible = true;
                tblReportePorProyecto.Visible = false;

                tblReportePorCd.Visible = false;
                tblReportePorRik.Visible = false;

                if (bTrimestral == false)
                {
                    var fuente = cnReporte_DINAMO.VistaGeneral(
                    this.EntidadSesion,
                    iCNC,
                    iTipoRik,
                    anyo,
                    mes,
                    bTrimestral,
                    this.Conexion
                    );                   
                    rptReporte.DataSource = fuente;
                    tblReportePorCd.Visible = true;
                }

                if (bTrimestral == true)
                {
                    var fuente = cnReporte_DINAMO.VistaGeneralTrimestral(
                        this.EntidadSesion,
                        iCNC,
                        iTipoRik,
                        anyo,
                        mes,
                        bTrimestral,
                        this.Conexion
                    );
                    rptReporte.DataSource = fuente;
                    //tblReportePorRik.Visible = true;
                    tblReportePorCd.Visible = true;
                }                
                rptReporte.DataBind();
            }

            
                                            
            
            //}
        }

        public void lnkbtnCDNombre_Click(object sender, EventArgs e)
        {
            LinkButton _this = sender as LinkButton;
            string strAnyo = ddlAnyo.SelectedValue;
            int anyo = int.Parse(strAnyo);
            string strMes = ddlMes.SelectedValue;
            int mes = int.Parse(strMes);
            string strCd = _this.CommandArgument;
            int idCd = int.Parse(strCd);
            ViewModel.IdCdElegido = idCd;
            using (IBusinessTransaction ibSianCentral = CN_FabricaTransaccionNegocios.ParaSIANCentral(EntidadSesion))
            {
                ibSianCentral.Begin();
                CN_ReporteDINAMO cnReporteDINAMO = new CN_ReporteDINAMO();
                var fuente = cnReporteDINAMO.GenerarVistaRIK(this.EntidadSesion, anyo, mes, idCd, chkPromedioTresMeses.Checked, ibSianCentral);

                rptReportePorRIK.DataSource = fuente;
                rptReportePorRIK.DataBind();
            }
            tblReportePorCd.Visible = false;
            tblReportePorRik.Visible = true;
            tblReportePorProyecto.Visible = false;
        }

        public void lnkbtnRikNombre_Click(object sender, EventArgs e)
        {
            using (IBusinessTransaction ibSianCentral = CN_FabricaTransaccionNegocios.ParaSIANCentral(EntidadSesion))
            {
                ibSianCentral.Begin();
                LinkButton _this = sender as LinkButton;

                string strAnyo = ddlAnyo.SelectedValue;
                int anyo = int.Parse(strAnyo);
                string strMes = ddlMes.SelectedValue;
                int mes = int.Parse(strMes);
                int idCd = ViewModel.IdCdElegido.Value;//int.Parse(strCd);

                string strRik = _this.CommandArgument;
                int idRik = int.Parse(strRik);

                ViewModel.NombreRepresentanteElegido = _this.Text;

                CN_ReporteDINAMO cnReporteDINAMO = new CN_ReporteDINAMO();
                var fuente = cnReporteDINAMO.GenerarVistaProyectos/*MontoCorregido*/(EntidadSesion, anyo, mes, ViewModel.IdCdElegido.Value, idRik, chkPromedioTresMeses.Checked, ibSianCentral);
                rptReporteProyecto.DataSource = fuente;
                rptReporteProyecto.DataBind();

                tblReportePorCd.Visible = false;
                tblReportePorRik.Visible = false;
                tblReportePorProyecto.Visible = true;
            }

        }

        protected void GenerarListadoAnos()
        {
            List<SerializableListItem> anyos = new List<SerializableListItem>();
            for (int i = DateTime.Now.Year; i > _limiteInferiorAnyos; i--)
            {
                anyos.Add(new SerializableListItem(i.ToString(), i.ToString()));
            }
            ViewModel.Anos = anyos;
        }

        protected void GenerarListadoMeses()
        {
            var dtfMx = CultureInfo.CreateSpecificCulture("es-MX").DateTimeFormat;
            List<SerializableListItem> meses = new List<SerializableListItem>();
            for (int iMes = 1; iMes < 13; iMes++)
            {
                meses.Add(new SerializableListItem(dtfMx.GetMonthName(iMes), iMes.ToString()));
            }
            ViewModel.Meses = meses;
        }

        protected int _limiteInferiorAnyos = DateTime.Now.Year - 10;
        protected ReporteDinamoViewModel _ViewModel = null;

        [Serializable]
        public class SerializableListItem
        {
            public SerializableListItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public string Text
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }
        }

        [Serializable]
        public class ReporteDinamoViewModel
        {
            public ReporteDinamoViewModel()
            {
            }

            public int AnoInicial
            {
                get;
                set;
            }

            public IEnumerable<SerializableListItem> Anos
            {
                get;
                set;
            }

            public IEnumerable<SerializableListItem> Meses
            {
                get;
                set;
            }

            public int? AnyoElegido
            {
                get;
                set;
            }

            public string MesElegido
            {
                get;
                set;
            }

            public int? IdCdElegido
            {
                get;
                set;
            }

            public string NombreRepresentanteElegido
            {
                get;
                set;
            }
        }
    }
}