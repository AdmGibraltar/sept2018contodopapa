using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProGenPoliza_Compaq
    {
        /*Fecha Facturas*/
        DateTime FechaFactura;
        DateTime Fac_Fecha;
        int Id_Fac;
        int Id_Cte;
        double Fac_Importe;
        double Fac_SubTotal;
        double Fac_ImporteIva;
        double Costo;

        /*Fechas Periodo*/
        int Id_Cal;
        DateTime Cal_FechaIni;
        DateTime Cal_FechaFin;
        bool Cal_Actual;
       
        /*Filtro*/
        int _filtroTipo;
        int _filtroCmbCentro;
        int _filtroAnhio;
        int _filtroMes;
        string _filtroCuenta1;
        string _filtroCuenta2;
        string _filtroCuenta3;

        /*Fecha Facturas*/
        public DateTime FechaFactura1
        {
            get { return FechaFactura; }
            set { FechaFactura = value; }
        }
        public DateTime Fac_Fecha1
        {
            get { return Fac_Fecha; }
            set { Fac_Fecha = value; }
        }
        public int Id_Fac1
        {
            get { return Id_Fac; }
            set { Id_Fac = value; }
        }
        public int Id_Cte1
        {
            get { return Id_Cte; }
            set { Id_Cte = value; }
        }
        public double Fac_Importe1
        {
            get { return Fac_Importe; }
            set { Fac_Importe = value; }
        }
        public double Fac_SubTotal1
        {
            get { return Fac_SubTotal; }
            set { Fac_SubTotal = value; }
        }
        public double Fac_ImporteIva1
        {
            get { return Fac_ImporteIva; }
            set { Fac_ImporteIva = value; }
        }
        public double Costo1
        {
            get { return Costo; }
            set { Costo = value; }
        }

        /*Fechas Periodo*/
        public int Id_Cal1
        {
            get { return Id_Cal; }
            set { Id_Cal = value; }
        }
        public DateTime Cal_FechaIni1
        {
            get { return Cal_FechaIni; }
            set { Cal_FechaIni = value; }
        }
        public DateTime Cal_FechaFin1
        {
            get { return Cal_FechaFin; }
            set { Cal_FechaFin = value; }
        }
        public bool Cal_Actual1
        {
            get { return Cal_Actual; }
            set { Cal_Actual = value; }
        }
        /*Filtro*/
        public int FiltroTipo
        {
            get { return _filtroTipo; }
            set { _filtroTipo = value; }
        }
        public int FiltroAnhio
        {
            get { return _filtroAnhio; }
            set { _filtroAnhio = value; }
        }
        public int FiltroMes
        {
            get { return _filtroMes; }
            set { _filtroMes = value; }
        }
        public int FiltroCmbCentro
        {
            get { return _filtroCmbCentro; }
            set { _filtroCmbCentro = value; }
        }
        public string FiltroCuenta1
        {
            get { return _filtroCuenta1; }
            set { _filtroCuenta1 = value; }
        }
        public string FiltroCuenta2
        {
            get { return _filtroCuenta2; }
            set { _filtroCuenta2 = value; }
        }
        public string FiltroCuenta3
        {
            get { return _filtroCuenta3; }
            set { _filtroCuenta3 = value; }
        }               
    }
}
