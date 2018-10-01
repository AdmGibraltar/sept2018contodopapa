using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RelacionGestor
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        
        private string _Id_Cd;
        public string Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        
        private double? _Id_Cte;
        public double? Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        
        private double? _Id_Ter;
        public double? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _Cte_Nombre;
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        
        private string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private string _Cd_Nombre;
        

        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        private string _GUID;

        public string GUID
        {
            get { return _GUID; }
            set { _GUID = value; }
        }
    }
}
