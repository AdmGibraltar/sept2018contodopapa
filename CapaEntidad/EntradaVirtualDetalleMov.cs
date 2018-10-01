using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaVirtualDetalleMov
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


        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Id_Es;
        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }

        private int _Id_Tm;
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }


        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        private int _Cant;
        public int Cant
        {
            get { return _Cant; }
            set { _Cant = value; }
        }

    }
}
