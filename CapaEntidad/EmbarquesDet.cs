using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EmbarquesDet
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

        private int _Id_Emb;
        public int Id_Emb
        {
            get { return _Id_Emb; }
            set { _Id_Emb = value; }
        }

        private int _Id_Fac;
        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }

        private int _Id_EmbDet;
        public int Id_EmbDet
        {
            get { return _Id_EmbDet; }
            set { _Id_EmbDet = value; }
        }
    }
}
