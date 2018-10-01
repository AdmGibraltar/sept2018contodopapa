using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Alertas
    {
        private string _GUID;
        public string GUID
        {
            get { return _GUID; }
            set { _GUID = value; }
        }

        private string _Etapa;
        public string Etapa
        {
            get { return _Etapa; }
            set { _Etapa = value; }
        }

        private string _EtapaStr;
        public string EtapaStr
        {
            get { return _EtapaStr; }
            set { _EtapaStr = value; }
        }

        private double? _Dias;
        public double? Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }

        private int _EnviarA;
        public int EnviarA
        {
            get { return _EnviarA; }
            set { _EnviarA = value; }
        }

        private string _EnviarAStr;
        public string EnviarAStr
        {
            get { return _EnviarAStr; }
            set { _EnviarAStr = value; }
        }

        private bool _SuspenderCredito;
        public bool SuspenderCredito
        {
            get { return _SuspenderCredito; }
            set { _SuspenderCredito = value; }
        }

        private string _SuspenderCreditoStr;
        public string SuspenderCreditoStr
        {
            get { return _SuspenderCreditoStr; }
            set { _SuspenderCreditoStr = value; }
        }
    }
}
