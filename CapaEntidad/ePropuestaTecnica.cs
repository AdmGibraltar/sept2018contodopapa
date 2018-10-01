using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ePropuestaTecnica
    {
        private int Id_Emp { get; set; }
        private int Id_Cd { get; set; }
        private int Id_Cte { get; set; }
        private int Id_Val { get; set; }
        private int Id_Prd { get; set; }

        private string CPT_ProductoActual { get; set; }
        private string CPT_SituacionActual { get; set; }
        private string CPT_VentajasKey { get; set; }
        private string CPT_RecursoImagenProductoActual { get; set; }
        private string CPT_RecursoImagenSolucionKey { get; set; }

        private int Id_RecusoImagenProductoActual { get; set; }
        private int Id_RecusoImagensolucionKey { get; set; }

    }
}
