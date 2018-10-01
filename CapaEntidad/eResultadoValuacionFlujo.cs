using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eResultadoValuacionFlujo
    {

        private int _Folio;
        private int _Anio;
        private string _FlujoAnual;
        private string _VPFlujos;

        public int Folio
        {
            get { 
                return _Folio;  
            }
            set { 
                _Folio = value;  
            }
        }

        public int Anio
        {
            get { 
                return _Anio;  
            }
            set { 
                _Anio= value;  
            }
        }

        public string FlujoAnual
        {
            get { 
                return _FlujoAnual;  
            }
            set { 
                _FlujoAnual= value;  
            }
        }

        public string VPFlujos
        {
            get { 
                return _VPFlujos;  
            }
            set { 
                _VPFlujos = value;  
            }
        }

        //

    }
}
