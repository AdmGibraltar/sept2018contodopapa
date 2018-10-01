using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RemisionTotal
    {
        private int _Id_Cd;
        private double _Total;
        private double _TotalImporte;
        private double _TotalVencido;
        private double _TotalVigente;



        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }


        public double Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public double TotalImporte
        {
            get { return _TotalImporte; }
            set { _TotalImporte = value; }
        }


        public double TotalVencido
        {
            get { return _TotalVencido; }
            set { _TotalVencido = value; }
        }

        public double TotalVigente
        {
            get { return _TotalVigente; }
            set { _TotalVigente = value; }
        }

       
    }
}
