using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CobProceso
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }


        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }


        private bool _SvtasAlm;
        public bool SvtasAlm
        {
            get { return _SvtasAlm; }
            set { _SvtasAlm = value; }
        }


        private bool _EmbAlm;
        public bool EmbAlm
        {
            get { return _EmbAlm; }
            set { _EmbAlm = value; }
        }

        private bool _EntAlm;
        public bool EntAlm
        {
            get { return _EntAlm; }
            set { _EntAlm = value; }
        }

        private bool _AlmCob;
        public bool AlmCob
        {
            get { return _AlmCob; }
            set { _AlmCob = value; }
        }

        private bool _RevCob;
        public bool RevCob
        {
            get { return _RevCob; }
            set { _RevCob = value; }
        }
    }
}
