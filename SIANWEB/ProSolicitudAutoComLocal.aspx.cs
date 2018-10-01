using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    class SolicitudAux
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private float _Costo;
        public float Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }
        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        private string _Autoriza;
        public string Autoriza
        {
            get { return _Autoriza; }
            set { _Autoriza = value; }
        }
    }

    public partial class ProSolicitudAutoComLocal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<SolicitudAux> list = new List<SolicitudAux>();
            rgSolicitud.DataSource = list;
            rgSolicitud.DataBind();
        }
    }
}