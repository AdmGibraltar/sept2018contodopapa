using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eClienteBuscar
    {

        private int _Cte;
        public int Cte
        {
            get { return _Cte; }
            set { _Cte = value; }
        }

        private string _RFC;
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }
                
        private string _NomComercial;
        public string NomComercial
        {
            get { return _NomComercial; }
            set { _NomComercial = value; }
        }

        private double _VPObservado;
        public double VPObservado
        {
            get { return _VPObservado; }
            set { _VPObservado = value; }
        }

        private int _UEN;
        public int UEN
        {
            get { return _UEN; }
            set { _UEN = value; }
        }

        private int _Segmento;
        public int Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }

        private Int32 _IdTer;
        public Int32 IdTer
        {
            get { return _IdTer; }
            set { _IdTer = value; }
        }

        private string _TerNombre;
        public string TerNombre
        {
            get { return _TerNombre; }
            set { _TerNombre = value; }
        }

        private Int32 _RegistrosEcontrados;
        public Int32 RegistrosEcontrados
        {
            get { return _RegistrosEcontrados; }
            set { _RegistrosEcontrados = value; }
        }

        //
    }
}
