using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad 
{
    public class RemisionSvtaAlmacenDet
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

        private int _Id_Rva;
        public int Id_Rva
        {
            get { return _Id_Rva; }
            set { _Id_Rva = value; }
        }

        private int _Id_RvaDet;
        public int Id_RvaDet
        {
            get { return _Id_RvaDet; }
            set { _Id_RvaDet = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private string _Rva_Tipo;
        public string Rva_Tipo
        {
            get { return _Rva_Tipo; }
            set { _Rva_Tipo = value; }
        }

        private string _Rva_TipoStr;
        public string Rva_TipoStr
        {
            get { return _Rva_TipoStr; }
            set { _Rva_TipoStr = value; }
        }

        private int _Rva_Doc;
        public int Rva_Doc
        {
            get { return _Rva_Doc; }
            set { _Rva_Doc = value; }
        }

        private DateTime _Rva_Fecha;
        public DateTime Rva_Fecha
        {
            get { return _Rva_Fecha; }
            set { _Rva_Fecha = value; }
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

        

        private double _Rva_Importe;
        public double Rva_Importe
        {
            get { return _Rva_Importe; }
            set { _Rva_Importe = value; }
        }

        private int _Rva_EnviarA;
        public int Rva_EnviarA
        {
            get { return _Rva_EnviarA; }
            set { _Rva_EnviarA = value; }
        }

        private string _Rva_DiaRev;

        public string Rva_DiaRev
        {
            get { return _Rva_DiaRev; }
            set { _Rva_DiaRev = value; }
        }

        private bool _Rva_Confirmado;
        

        public bool Rva_Confirmado
        {
            get { return _Rva_Confirmado; }
            set { _Rva_Confirmado = value; }
        }


        private bool _Rva_Seleccionado;

        public bool Rva_Seleccionado
        {
            get { return _Rva_Seleccionado; }
            set { _Rva_Seleccionado = value; }
        }

       private string _Fac_EnviarAStr;
       public string Fac_EnviarAStr
       {
           get { return _Fac_EnviarAStr; }
           set { _Fac_EnviarAStr = value; }
        }

    }
}
