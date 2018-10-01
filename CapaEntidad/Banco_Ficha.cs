using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable()]
    public class Banco_Ficha
    {
        private int _Pag_Ficha;
        private int _Id_Ban;
        private string _Ban_Nombre;
        private DateTime _Pag_Fecha;
        private double _Pag_Importe;
        private string _Ban_Cuenta;

        public int Pag_Ficha 
        {
            get { return _Pag_Ficha; }
            set { _Pag_Ficha = value; }
        }
        public int Id_Ban
        {
            get { return _Id_Ban; }
            set { _Id_Ban = value; }
        }
        public string Ban_Nombre
        {
            get { return _Ban_Nombre; }
            set { _Ban_Nombre = value; }
        }
        public DateTime Pag_Fecha
        {
            get { return _Pag_Fecha; }
            set { _Pag_Fecha = value; }
        }
        public double Pag_Importe
        {
            get { return _Pag_Importe; }
            set { _Pag_Importe = value; }
        }
        public string Ban_Cuenta
        {
            get { return _Ban_Cuenta; }
            set { _Ban_Cuenta = value; }
        }
    }
}
