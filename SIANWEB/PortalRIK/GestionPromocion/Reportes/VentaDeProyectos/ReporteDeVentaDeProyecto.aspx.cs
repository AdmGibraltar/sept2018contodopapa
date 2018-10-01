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

namespace SIANWEB.PortalRIK.GestionPromocion.Reportes.VentaDeProyectos
{
    public partial class ReporteDeVentaDeProyecto
        : BaseServerPage
    {
        public string[] CurrentPath
        {
            get;
            set;
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

            CurrentPath = new List<string>() { "Gestion de la Promoción", "Reporte Venta de Proyectos" }.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 8;

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

        public void btnActualizar_Click(object sender, EventArgs e)
        {
            CN_ReporteVentaCRM cnReporteDeVenta = new CN_ReporteVentaCRM();
            string strAnyo = ddlAnyo.SelectedValue;
            int anyo = int.Parse(strAnyo);
            string strMes = ddlMes.SelectedValue;
            int mes = int.Parse(strMes);
            ViewModel.NombreRepresentanteElegido = "";
            using (IBusinessTransaction ibSianCentral = CN_FabricaTransaccionNegocios.ParaSIANCentral(EntidadSesion))
            {
                ibSianCentral.Begin();
                var fuente = cnReporteDeVenta.GenerarVistaGeneral(this.EntidadSesion, rbCDC.Checked ? 2 : 1, anyo, mes, chkPromedioTresMeses.Checked, ibSianCentral);

                reporteBlankSlate.Visible = false;
                dvContenedorDeReporte.Visible = true;
                tblReportePorCd.Visible = true;
                tblReportePorRik.Visible = false;
                tblReportePorProyecto.Visible = false;
                rptReporte.DataSource = fuente;
                rptReporte.DataBind();
            }
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
                CN_ReporteVentaCRM cnReporteDeVenta = new CN_ReporteVentaCRM();
                var fuente = cnReporteDeVenta.GenerarVistaRIK(this.EntidadSesion, anyo, mes, idCd, chkPromedioTresMeses.Checked, ibSianCentral);

                rptReportePorRIK.DataSource = fuente;
                rptReportePorRIK.DataBind();
            }
            tblReportePorCd.Visible = false;
            tblReportePorRik.Visible = true;
            tblReportePorProyecto.Visible = false;
        }

        public void lnkbtnRikNombre_Click(object sender, EventArgs e)
        {
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