using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Lista Generica 
// Utilizar en combos.

namespace CapaEntidad
{
    public class CrmLista
    {

        int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion= value; }
        }        
    }
}
