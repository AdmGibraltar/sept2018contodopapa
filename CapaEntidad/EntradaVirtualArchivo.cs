using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaVirtualArchivo
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


        private int _Id_Env;
        public int Id_Env
        {
            get { return _Id_Env; }
            set { _Id_Env = value; }
        }

        private string _Archivo;
        public string Archivo
        {
            get { return _Archivo; }
            set { _Archivo = value; }
        }



    }
}
