using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaRevisionCobroDet
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

        private int _Id_Frc;
        public int Id_Frc
        {
            get { return _Id_Frc; }
            set { _Id_Frc = value; }
        }

        private int _Id_FrcDet;
        public int Id_FrcDet
        {
            get { return _Id_FrcDet; }
            set { _Id_FrcDet = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private string _Frc_Tipo;
        public string Frc_Tipo
        {
            get { return _Frc_Tipo; }
            set { _Frc_Tipo = value; }
        }

        private string _Frc_TipoStr;
        public string Frc_TipoStr
        {
            get { return _Frc_TipoStr; }
            set { _Frc_TipoStr = value; }
        }

        private int _Frc_Doc;
        public int Frc_Doc
        {
            get { return _Frc_Doc; }
            set { _Frc_Doc = value; }
        }

        private int _Frc_Cheque;
        public int Frc_Cheque
        {
            get { return _Frc_Cheque; }
            set { _Frc_Cheque = value; }
        }


        private int _Frc_Efectivo;
        public int Frc_Efectivo
        {
            get { return _Frc_Efectivo; }
            set { _Frc_Efectivo = value; }
        }

        

        private DateTime _Frc_Fecha;
        public DateTime Frc_Fecha
        {
            get { return _Frc_Fecha; }
            set { _Frc_Fecha = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private double _Frc_Importe;
        public double Frc_Importe
        {
            get { return _Frc_Importe; }
            set { _Frc_Importe = value; }
        }

        private int _Frc_EnviarA;
        public int Frc_EnviarA
        {
            get { return _Frc_EnviarA; }
            set { _Frc_EnviarA = value; }
        }

        private string _Frc_EnviarAStr;
        
        public string Frc_EnviarAStr
        {
            get { return _Frc_EnviarAStr; }
            set { _Frc_EnviarAStr = value; }
        }

        private bool _Frc_Confirmado;
        

        public bool Frc_Confirmado
        {
            get { return _Frc_Confirmado; }
            set { _Frc_Confirmado = value; }
        }


        private bool _Frc_Seleccionado;

        public bool Frc_Seleccionado
        {
            get { return _Frc_Seleccionado; }
            set { _Frc_Seleccionado = value; }
        }
    }
}
