using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaEspecial
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

        private int _Id_Fac;
        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }


        private string _Id_FacSerie;
        public string Id_FacSerie
        {
            get { return _Id_FacSerie; }
            set { _Id_FacSerie = value; }
        }



        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private DateTime _FacEsp_Fecha;
        public DateTime FacEsp_Fecha
        {
            get { return _FacEsp_Fecha; }
            set { _FacEsp_Fecha = value; }
        }

        private double _FacEsp_Importe;
        public double FacEsp_Importe
        {
            get { return _FacEsp_Importe; }
            set { _FacEsp_Importe = value; }
        }

        private double _FacEsp_SubTotal;
        public double FacEsp_SubTotal
        {
            get { return _FacEsp_SubTotal; }
            set { _FacEsp_SubTotal = value; }
        }

        private double _FacEsp_ImporteIva;
        public double FacEsp_ImporteIva
        {
            get { return _FacEsp_ImporteIva; }
            set { _FacEsp_ImporteIva = value; }
        }

        private double _FacEsp_Total;
        public double FacEsp_Total
        {
            get { return _FacEsp_Total; }
            set { _FacEsp_Total = value; }
        }
    }
}
