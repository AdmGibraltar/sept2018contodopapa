using System;

namespace CapaEntidad
{
    public class ProductoDet
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Ped;
        private int _Id_Prd;
        private DateTime _Ped_Fecha;
        private int _Id_Ter;
        private int _Id_Cte;
        private string _Cte_NomComercial;
        private bool _Cte_Credito;
        private string _Cte_CreditoStr;
        private int _Ped_Ord;
        private int _Ped_Disp;
        private int _Ped_Asignar;
        private int _Ped_Faltante;
        private int _Prd_InvFinal;
        private int _Prd_Disp;
        private int _Id_U;
        private int _Id_PedDet;

        public int Id_PedDet
        {
            get { return _Id_PedDet; }
            set { _Id_PedDet = value; }
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
        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public DateTime Ped_Fecha
        {
            get { return _Ped_Fecha; }
            set { _Ped_Fecha = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public bool Cte_Credito
        {
            get { return _Cte_Credito; }
            set { _Cte_Credito = value; }
        }
        public string Cte_CreditoStr
        {
            get { return _Cte_CreditoStr; }
            set { _Cte_CreditoStr = value; }
        }
        public int Ped_Ord
        {
            get { return _Ped_Ord; }
            set { _Ped_Ord = value; }
        }
        public int Ped_Disp
        {
            get { return _Ped_Disp; }
            set { _Ped_Disp = value; }
        }
        public int Ped_Asignar
        {
            get { return _Ped_Asignar; }
            set { _Ped_Asignar = value; }
        }
        public int Ped_Faltante
        {
            get { return _Ped_Faltante; }
            set { _Ped_Faltante = value; }
        }
        public int Prd_InvFinal
        {
            get { return _Prd_InvFinal; }
            set { _Prd_InvFinal = value; }
        }
        public int Prd_Disp
        {
            get { return _Prd_Disp; }
            set { _Prd_Disp = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
    }
}
