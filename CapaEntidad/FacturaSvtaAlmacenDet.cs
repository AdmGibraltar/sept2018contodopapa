using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad 
{
    public class FacturaSvtaAlmacenDet
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

        private int _Id_Fva;
        public int Id_Fva
        {
            get { return _Id_Fva; }
            set { _Id_Fva = value; }
        }

        private int _Id_FvaDet;
        public int Id_FvaDet
        {
            get { return _Id_FvaDet; }
            set { _Id_FvaDet = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private string _Fva_Tipo;
        public string Fva_Tipo
        {
            get { return _Fva_Tipo; }
            set { _Fva_Tipo = value; }
        }

        private string _Fva_TipoStr;
        public string Fva_TipoStr
        {
            get { return _Fva_TipoStr; }
            set { _Fva_TipoStr = value; }
        }

        private int _Fva_Doc;
        public int Fva_Doc
        {
            get { return _Fva_Doc; }
            set { _Fva_Doc = value; }
        }

        private DateTime _Fva_Fecha;
        public DateTime Fva_Fecha
        {
            get { return _Fva_Fecha; }
            set { _Fva_Fecha = value; }
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

        

        private double _Fva_Importe;
        public double Fva_Importe
        {
            get { return _Fva_Importe; }
            set { _Fva_Importe = value; }
        }

        private int _Fva_EnviarA;
        public int Fva_EnviarA
        {
            get { return _Fva_EnviarA; }
            set { _Fva_EnviarA = value; }
        }

        private string _Fva_DiaRev;

        public string Fva_DiaRev
        {
            get { return _Fva_DiaRev; }
            set { _Fva_DiaRev = value; }
        }

        private bool _Fva_Confirmado;
        

        public bool Fva_Confirmado
        {
            get { return _Fva_Confirmado; }
            set { _Fva_Confirmado = value; }
        }


        private bool _Fva_Seleccionado;

        public bool Fva_Seleccionado
        {
            get { return _Fva_Seleccionado; }
            set { _Fva_Seleccionado = value; }
        }

       private string _Fac_EnviarAStr;
       public string Fac_EnviarAStr
       {
           get { return _Fac_EnviarAStr; }
           set { _Fac_EnviarAStr = value; }
        }

    }
}
