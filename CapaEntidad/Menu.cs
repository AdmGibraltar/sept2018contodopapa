using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
  public  class Menu
    {
        private int _Sm_Cve;
        private string _Sm_Sm_Cve;
        private int _Sm_Ord;
        private string _Sm_Desc;
        private string _Sm_HRef;
        
        private int _Sm_Ofi;

        public int Sm_Cve
        {
            get { return _Sm_Cve; }
            set { _Sm_Cve = value; }
        }

        public string Sm_Sm_Cve
        {
            get { return _Sm_Sm_Cve; }
            set { _Sm_Sm_Cve = value; }
        }
        public int Sm_Ord
        {
            get { return _Sm_Ord; }
            set { _Sm_Ord = value; }
        }
        public string Sm_Desc
        {
            get { return _Sm_Desc; }
            set { _Sm_Desc = value; }
        }
        public string Sm_HRef
        {
            get { return _Sm_HRef; }
            set { _Sm_HRef = value; }
        }
        public int Sm_Nivel
        {
            get { return _Sm_Ord; }
            set { _Sm_Ord = value; }
        }
        public int Sm_Ofi
        {
            get { return _Sm_Ofi; }
            set { _Sm_Ofi = value; }
        }
    }
}
