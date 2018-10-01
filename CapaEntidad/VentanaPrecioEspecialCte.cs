using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class VentanaPrecioEspecialCte
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ape;
        int _Id_ApeCte;
        int _Id_Cte;
        string _Cte_NomComercial;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Ape
        {
            get { return _Id_Ape; }
            set { _Id_Ape = value; }
        }
        public int Id_ApeCte
        {
            get { return _Id_ApeCte; }
            set { _Id_ApeCte = value; }
        }
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
    }
}
