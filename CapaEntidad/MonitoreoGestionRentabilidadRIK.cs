using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class MonitoreoGestionRentabilidadRIK
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

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }


        private string _Periodo;
        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }


        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }


        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        private int _Anio;
        public int Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private Decimal _VentaImporte;
        public Decimal VentaImporte
        {
            get { return _VentaImporte; }
            set { _VentaImporte = value; }
        }

        private Decimal _UtilidadBrutaImporte;
        public Decimal UtilidadBrutaImporte
        {
            get { return _UtilidadBrutaImporte; }
            set { _UtilidadBrutaImporte = value; }
        }


        private Decimal _UtilidadBrutaPorc;
        public Decimal UtilidadBrutaPorc
        {
            get { return _UtilidadBrutaPorc; }
            set { _UtilidadBrutaPorc = value; }
        }


        private Decimal _MetaUtilidadBrutaPorc;
        public Decimal MetaUtilidadBrutaPorc
        {
            get { return _MetaUtilidadBrutaPorc; }
            set { _MetaUtilidadBrutaPorc = value; }
        }


        private Decimal _MetaUtilidadBrutaImporte;
        public Decimal MetaUtilidadBrutaImporte
        {
            get { return _MetaUtilidadBrutaImporte; }
            set { _MetaUtilidadBrutaImporte = value; }
        }

        private Decimal _UtilidadBrutaProyectadaPorc;
        public Decimal UtilidadBrutaProyectadaPorc
        {
            get { return _UtilidadBrutaProyectadaPorc; }
            set { _UtilidadBrutaProyectadaPorc = value; }
        }

        private Decimal _UtilidadBrutaProyectadaImporte;
        public Decimal UtilidadBrutaProyectadaImporte
        {
            get { return _UtilidadBrutaProyectadaImporte; }
            set { _UtilidadBrutaProyectadaImporte = value; }
        }




    }
}
