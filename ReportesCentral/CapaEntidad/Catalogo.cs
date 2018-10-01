using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Catalogo
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id;
        string _IdStr;

        public string IdStr
        {
            get { return _IdStr; }
            set { _IdStr = value; }
        }
        string _Tabla;
        string _Columna;
        private bool _IsStr;

        public bool IsStr
        {
            get { return _IsStr; }
            set { _IsStr = value; }
        }

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
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
        public string Tabla
        {
            get { return _Tabla; }
            set { _Tabla = value; }
        }
        public string Columna
        {
            get { return _Columna; }
            set { _Columna = value; }
        }

        
    }
}
