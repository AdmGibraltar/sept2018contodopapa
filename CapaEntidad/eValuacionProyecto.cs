using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eValuacionProyecto
    {

        private List<eResultadosValuacionDetallado> _ValuacionProyecto = new List<eResultadosValuacionDetallado>();
        private List<eResultadoValuacionFlujo> _ValuacionFlujo = new List<eResultadoValuacionFlujo>();

        public List<eResultadosValuacionDetallado> ValuacionProyecto
        {
            get
            {
                return _ValuacionProyecto;
            }
            set
            {
                _ValuacionProyecto = value;
            }
        }

        public List<eResultadoValuacionFlujo> ValuacionFlujo
        {
            get
            {
                return _ValuacionFlujo;
            }
            set
            {
                _ValuacionFlujo = value;
            }
        }
        //
    }
}
