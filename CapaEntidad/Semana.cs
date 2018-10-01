using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Semana
    {
        int _Id_Emp;
        int _Id_Sem;
        int _Id_Cal;
        int _Cal_Año;
        DateTime _Sem_FechaIni;
        DateTime _Sem_FechaFin;
        bool _Sem_Activo;
        private int _Id_Cd;
        private DateTime _Sem_FechaAct;
        private string _Periodo;

        ///     GVI

        private string _Rango;

        public string Rango
        {
            get { return _Rango; }
            set { _Rango = value; }
        }

        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }

        public DateTime Sem_FechaAct
        {
            get { return _Sem_FechaAct; }
            set { _Sem_FechaAct = value; }
        }



        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }




        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Sem
        {
            get { return _Id_Sem; }
            set { _Id_Sem = value; }
        }

        public int Id_Cal
        {
            get { return _Id_Cal; }
            set { _Id_Cal = value; }
        }

        public int Cal_Año
        {
            get { return _Cal_Año; }
            set { _Cal_Año = value; }
        }

        public DateTime Sem_FechaIni
        {
            get { return _Sem_FechaIni; }
            set { _Sem_FechaIni = value; }
        }

        public DateTime Sem_FechaFin
        {
            get { return _Sem_FechaFin; }
            set { _Sem_FechaFin = value; }
        }

        public bool Sem_Activo
        {
            get { return _Sem_Activo; }
            set { _Sem_Activo = value; }
        }


        public int Mes { get; set; }

        public int Id_SemxMes { get; set; }


    }
}
