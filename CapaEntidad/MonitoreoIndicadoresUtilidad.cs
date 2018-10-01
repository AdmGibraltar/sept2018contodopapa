using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class MonitoreoIndicadoresUtilidad
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

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }


        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private Decimal _UtilidadBruta;
        public Decimal UtilidadBruta
        {
            get { return _UtilidadBruta; }
            set { _UtilidadBruta = value; }
        }


        private Decimal _MetaUtilidadBruta;
        public Decimal MetaUtilidadBruta
        {
            get { return _MetaUtilidadBruta; }
            set { _MetaUtilidadBruta = value; }
        }

        private Decimal _UtilidadBrutaGestion;
        public Decimal UtilidadBrutaGestion
        {
            get { return _UtilidadBrutaGestion; }
            set { _UtilidadBrutaGestion = value; }
        }




    }
}
