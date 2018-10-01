using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteProd
    {
        int _Id_Emp;
        int _Id_Cd;
        string _Id_Clp;
        int _Id_Cte;
        int _Id_Prd;
        int _Id_Vap;

        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }
        string _Clp_descripcion;
        string _Clp_unidades;
        DateTime? _Clp_FecUltVta;
       
        double _Clp_Cantidad;
        int _Clp_InvFin;
        int _Clp_Asignado;
        bool _Estatus;
        string _EstatusStr;
        private string _Clp_Presentacion;

        private string _Clp_Presentacion1;
        private double _CantFact;
        private string _Unidades;


        public string Clp_Presentacion1
        {
            get { return _Clp_Presentacion1; }
            set { _Clp_Presentacion1 = value; }
        }
        public string Unidades
        {
            get { return _Unidades; }
            set { _Unidades = value; }
        }
        public double CantFact
        {
            get { return _CantFact; }
            set { _CantFact = value; }
        }

        public string Clp_Presentacion
        {
            get { return _Clp_Presentacion; }
            set { _Clp_Presentacion = value; }
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
        public string Id_Clp
        {
            get { return _Id_Clp; }
            set { _Id_Clp = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public string Clp_descripcion
        {
            get { return _Clp_descripcion; }
            set { _Clp_descripcion = value; }
        }
        public string Clp_unidades
        {
            get { return _Clp_unidades; }
            set { _Clp_unidades = value; }
        }
        public DateTime? Clp_FecUltVta
        {
            get { return _Clp_FecUltVta; }
            set { _Clp_FecUltVta = value; }
        }
        public double Clp_Cantidad
        {
            get { return _Clp_Cantidad; }
            set { _Clp_Cantidad = value; }
        }
        public int Clp_InvFin
        {
            get { return _Clp_InvFin; }
            set { _Clp_InvFin = value; }
        }
        public int Clp_Asignado
        {
            get { return _Clp_Asignado; }
            set { _Clp_Asignado = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
    }
}
