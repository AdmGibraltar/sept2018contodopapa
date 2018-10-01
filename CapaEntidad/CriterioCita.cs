using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CriterioCita
    {
        private int _Id_CriterioCita;
        private int _Id_Emp;
        private int _Id_Cd; 
        private int _Id_Cliente;
        string _Cliente;
        private int _Id_Frecuencia;
        string _Frecuencia;
        private int _Id_TipoVisita;
        string _TipoVisita;
        private int _Id_RSC;
        string _RSC;
        private DateTime _FechaInicial;
        bool _Activo;
        private int _TienePreRequi;
        private int _Usuario;

        public int Id_CriterioCita
        {
            get { return _Id_CriterioCita; }
            set { _Id_CriterioCita = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public int Id_Cliente
        {
            get { return _Id_Cliente; }
            set { _Id_Cliente = value; }
        }

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        public int Id_Frecuencia
        {
            get { return _Id_Frecuencia; }
            set { _Id_Frecuencia = value; }
        }

        public string Frecuencia
        {
            get { return _Frecuencia; }
            set { _Frecuencia = value; }
        }

        public int Id_TipoVisita
        {
            get { return _Id_TipoVisita; }
            set { _Id_TipoVisita = value; }
        }

        public string TipoVisita
        {
            get { return _TipoVisita; }
            set { _TipoVisita = value; }
        }

        public int Id_RSC
        {
            get { return _Id_RSC; }
            set { _Id_RSC = value; }
        }

        public string RSC
        {
            get { return _RSC; }
            set { _RSC = value; }
        }

        public DateTime FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public int TienePreRequi
        {
            get { return _TienePreRequi; }
            set { _TienePreRequi = value; }
        }

        public int Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
    }
}
