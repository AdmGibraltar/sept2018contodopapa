using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Pagina
    {
        int _clave;
        string _descripcion;
        string _url;
        private string _Path;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        public int Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

    }
}
