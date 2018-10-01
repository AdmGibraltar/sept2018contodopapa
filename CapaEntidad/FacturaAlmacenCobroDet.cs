using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaAlmacenCobroDet
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

        private int _Id_FacDet;
        public int Id_FacDet
        {
            get { return _Id_FacDet; }
            set { _Id_FacDet = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private string _Fac_Tipo;
        public string Fac_Tipo
        {
            get { return _Fac_Tipo; }
            set { _Fac_Tipo = value; }
        }

        private string _Fac_TipoStr;
        public string Fac_TipoStr
        {
            get { return _Fac_TipoStr; }
            set { _Fac_TipoStr = value; }
        }

        private int _Fac_Doc;
        public int Fac_Doc
        {
            get { return _Fac_Doc; }
            set { _Fac_Doc = value; }
        }

        private DateTime _Fac_Fecha;
        public DateTime Fac_Fecha
        {
            get { return _Fac_Fecha; }
            set { _Fac_Fecha = value; }
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

        private double _Fac_Importe;
        public double Fac_Importe
        {
            get { return _Fac_Importe; }
            set { _Fac_Importe = value; }
        }

        private int _Fac_EnviarA;
        public int Fac_EnviarA
        {
            get { return _Fac_EnviarA; }
            set { _Fac_EnviarA = value; }
        }

        private string _Fac_EnviarAStr;
        
        public string Fac_EnviarAStr
        {
            get { return _Fac_EnviarAStr; }
            set { _Fac_EnviarAStr = value; }
        }

        private bool _Fac_Confirmado;
        

        public bool Fac_Confirmado
        {
            get { return _Fac_Confirmado; }
            set { _Fac_Confirmado = value; }
        }


        private bool _Fac_Seleccionado;

        public bool Fac_Seleccionado
        {
            get { return _Fac_Seleccionado; }
            set { _Fac_Seleccionado = value; }
        }

        public string Cte_DiasRevision { get; set; }
    }
}
