using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DevolucionRemisionDet
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

        private int _Id_DevRem;
        public int Id_DevRem
        {
            get { return _Id_DevRem; }
            set { _Id_DevRem = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }


     

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }


        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }



        private int _Cant;
        public int Cant
        {
            get { return _Cant; }
            set { _Cant = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }


        private int _Id_Es;
        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }

    }
}
