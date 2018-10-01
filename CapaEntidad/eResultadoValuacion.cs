using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eResultadoValuacion
    {
        private double _UtilidadRemanente;
        private double _ValorPresenteNeto;

        public double UtilidadRemanente
        {
            get { 
                return _UtilidadRemanente; 
            }    
            set { 
                _UtilidadRemanente = value; 
            }            
        }

        public double ValorPresenteNeto
        {
            get
            {
                return _ValorPresenteNeto;
            }
            set
            {
                _ValorPresenteNeto = value;
            }
        }

        public bool EsPositiva
        {
            get
            {
                return _UtilidadRemanente > 0 && _ValorPresenteNeto > 0;
            }
        }
        //
    }
}
