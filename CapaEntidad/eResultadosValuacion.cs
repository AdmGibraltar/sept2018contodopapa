using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eResultadosValuacion
    {
        private int _Folio;
        private string _Titulo;
        private string _Factor;
        private string _Monto;

        public int Folio
        {
            get { return _Folio; }
            set { _Folio = value; }
        }

        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }

        public string Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }

        public string Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        //
    }
}
