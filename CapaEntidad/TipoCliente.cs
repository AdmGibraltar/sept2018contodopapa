using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TipoCliente
    {
        protected int _id_TCte;
        protected string _tCte_Descripcion;
        protected string _tCte_ConCuentaCorporativa;
        protected string _tCte_Autorizadores;

        public int Id_TCte
        {
            get {return _id_TCte;}
            set {_id_TCte = value;}
        }

        public string TCte_Descripcion
        {
            get { return _tCte_Descripcion; }
            set { _tCte_Descripcion = value; }
        }

        public string TCte_ConCuentaCorporativa
        {
            get { return _tCte_ConCuentaCorporativa; }
            set { _tCte_ConCuentaCorporativa = value; }
        }

        public string TCte_Autorizadores
        {
            get { return _tCte_Autorizadores; }
            set { _tCte_Autorizadores = value; }
        }
    }
}
