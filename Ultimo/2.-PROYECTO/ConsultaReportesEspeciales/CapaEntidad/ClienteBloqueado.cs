using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class  ClienteBloqueado
    {
        private int _Id_Cte;
        private string _Cte_NomComercial;
        private int _Cte_Facturacion;

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        public int Cte_Facturacion
        {
            get { return _Cte_Facturacion; }
            set { _Cte_Facturacion = value; }
        }
    }
}
