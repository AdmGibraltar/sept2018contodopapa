using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace CapaEntidad
{
    public class Amortizacion
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

        private int _Id_Amo;
        public int Id_Amo
        {
            get { return _Id_Amo; }
            set { _Id_Amo = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Amo_AnioInicio;
        public int Amo_AnioInicio
        {
            get { return _Amo_AnioInicio; }
            set { _Amo_AnioInicio = value; }
        }

        private int _Amo_AnioFin;
        public int Amo_AnioFin
        {
            get { return _Amo_AnioFin; }
            set { _Amo_AnioFin = value; }
        }

        private int _Amo_MesInicio;
        public int Amo_MesInicio
        {
            get { return _Amo_MesInicio; }
            set { _Amo_MesInicio = value; }
        }

        private int _Amo_MesFin;
        public int Amo_MesFin
        {
            get { return _Amo_MesFin; }
            set { _Amo_MesFin = value; }
        }

        private int _Amo_MesesAmortiza;
        public int Amo_MesesAmortiza
        {
            get { return _Amo_MesesAmortiza; }
            set { _Amo_MesesAmortiza = value; }
        }

        private float _Amo_Costo;
        public float Amo_Costo
        {
            get { return _Amo_Costo; }
            set { _Amo_Costo = value; }
        }

        private int _Amo_Cant;
        public int Amo_Cant
        {
            get { return _Amo_Cant; }
            set { _Amo_Cant = value; }
        }
    }
}
