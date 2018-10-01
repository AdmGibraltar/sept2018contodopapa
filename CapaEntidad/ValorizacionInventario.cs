using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ValorizacionInventario
    {
        int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        string _Id_Ptp;
        public string Id_Ptp
        {
            get { return _Id_Ptp; }
            set { _Id_Ptp = value; }
        }
        string _Id_Spo;
        public string Id_Spo
        {
            get { return _Id_Spo; }
            set { _Id_Spo = value; }
        }
        string _Id_PrdStr;
        public string Id_PrdStr
        {
            get { return _Id_PrdStr; }
            set { _Id_PrdStr = value; }
        }
        string _TipoPrecioStr;
        public string TipoPrecioStr
        {
            get { return _TipoPrecioStr; }
            set { _TipoPrecioStr = value; }
        }
        string _Orden;
        public string Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }        
    }
}
