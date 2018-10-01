using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RikUen
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Rik;
        int _Id_Uen;
        string _DescripcionUEN;
        
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
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        public string DescripcionUEN
        {
            get { return _DescripcionUEN; }
            set { _DescripcionUEN = value; }
        }
    }
}
