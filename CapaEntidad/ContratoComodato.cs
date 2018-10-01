using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ContratoComodato
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
        private int _Id_Cco;
        public int Id_Cco
        {
            get { return _Id_Cco; }
            set { _Id_Cco = value; }
        }
        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private string _Rem_Estatus;
        public string Rem_Estatus
        {
            get { return _Rem_Estatus; }
            set { _Rem_Estatus = value; }
        }

        public string Rem_EstatusStr
        {
            get 
            {
                switch (Rem_Estatus.ToUpper())
                { 
                    case "B":
                        return "Baja";
                    case "I":
                        return "Impreso";
                    case "C":
                        return "Captura";
                    case "N":
                        return "Entregado";
                    case "E":
                        return "Embarque";
                }
                return string.Empty;
            }
        }

        //private int _Cco_AcFolio;
        //public int Cco_AcFolio
        //{
        //    get { return _Cco_AcFolio; }
        //    set { _Cco_AcFolio = value; }
        //}

        //private int _Cco_CoFolio;
        //public int Cco_CoFolio
        //{
        //    get { return _Cco_CoFolio; }
        //    set { _Cco_CoFolio = value; }
        //}

        private DateTime _Cco_Fecha;
        public DateTime Cco_Fecha
        {
            get { return _Cco_Fecha; }
            set { _Cco_Fecha = value; }
        }

        private int? _Id_U;
        public int? Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private int? _Id_Rik;
        public int? Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
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

        //private double _Cco_Subtotal;
        //public double Cco_Subtotal
        //{
        //    get { return _Cco_Subtotal; }
        //    set { _Cco_Subtotal = value; }
        //}

        //private double _Cco_Iva;
        //public double Cco_Iva
        //{
        //    get { return _Cco_Iva; }
        //    set { _Cco_Iva = value; }
        //}

        //private double _Cco_Total;
        //public double Cco_Total
        //{
        //    get { return _Cco_Total; }
        //    set { _Cco_Total = value; }
        //}

        private ContratoComodatoDetalle _ContratoComodatoDetalle;
        public ContratoComodatoDetalle ContratoComodatoDetalle
        {
            get { return _ContratoComodatoDetalle; }
            set { _ContratoComodatoDetalle = value; }
        }

        private List<ContratoComodatoDetalle> _ListaContratoComodatoDetalle;
        private int _Id_Prd;

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        private string _Prd_Descripcion;
        private int _Contrato;

        public int Contrato
        {
            get { return _Contrato; }
            set { _Contrato = value; }
        }
        private int _Saldo;

        public int Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }
        private int _Cantidad;
        private DateTime? _Cco_FechaIni;

        public DateTime? Cco_FechaIni
        {
            get { return _Cco_FechaIni; }
            set { _Cco_FechaIni = value; }
        }
        private DateTime? _Cco_FechaFin;

        public DateTime? Cco_FechaFin
        {
            get { return _Cco_FechaFin; }
            set { _Cco_FechaFin = value; }
        }

        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        public List<ContratoComodatoDetalle> ListaContratoComodatoDetalle
        {
            get { return _ListaContratoComodatoDetalle; }
            set { _ListaContratoComodatoDetalle = value; }
        }
    }
}
