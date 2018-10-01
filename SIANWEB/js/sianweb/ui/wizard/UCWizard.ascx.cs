using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace SIANWEB.js.sianweb.ui.wizard
{
    public partial class UCWizard : System.Web.UI.UserControl
    {
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Establece el título del diálogo del wizard")]
        public string Title
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}