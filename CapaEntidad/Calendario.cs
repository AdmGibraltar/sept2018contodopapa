using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Calendario
    {
        private int _Id_Emp;
        private int _Id_Cal;
        private int _Cal_Año;
        private int _Cal_Mes;
        private DateTime _Cal_FechaIni;
        private DateTime _Cal_FechaFin;
        private bool _Cal_Actual;
        private bool _Cal_Activo;
        private string _Cal_FechaExtemporaneo;

        public string Cal_FechaExtemporaneo
        {
            get { return _Cal_FechaExtemporaneo; }
            set { _Cal_FechaExtemporaneo = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cal
        {
            get {return  _Id_Cal;}
            set { _Id_Cal = value; }
        }
        public int Cal_Año
        { 
            get {return  _Cal_Año;}
            set {_Cal_Año = value; }
        }
        public int Cal_Mes
        { 
            get {return _Cal_Mes ;}
            set {_Cal_Mes = value; }
        }
        public DateTime Cal_FechaIni
        {
            get { return _Cal_FechaIni; }
            set { _Cal_FechaIni = value; }
        }
        public DateTime Cal_FechaFin
        {
            get { return _Cal_FechaFin; }
            set { _Cal_FechaFin = value; }
        }
        public bool Cal_Actual
        {
            get { return _Cal_Actual; }
            set { _Cal_Actual = value; }
        }
        public bool Cal_Activo
        {
            get { return _Cal_Activo; }
            set { _Cal_Activo = value; }
        }
    }
}
