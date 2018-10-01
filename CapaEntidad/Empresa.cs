using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Empresa
    {
        private int _Id_Emp;
        private string _Emp_Nombre;
        private string _Emp_Cnx;
        private bool _Verifica1 ;
        private bool _Verifica2 ;
  
        private string _RiFactura;
        private bool _Emp_Activo ;
        private string _Emp_ActivoStr;
        private double? _Emp_GastoLocal;

        public double? Emp_GastoLocal
        {
            get { return _Emp_GastoLocal; }
            set { _Emp_GastoLocal = value; }
        }
        private double? _Emp_GastoAdmin;

        public double? Emp_GastoAdmin
        {
            get { return _Emp_GastoAdmin; }
            set { _Emp_GastoAdmin = value; }
        }
        private double? _Emp_ContribucionPapel;

        public double? Emp_ContribucionPapel
        {
            get { return _Emp_ContribucionPapel; }
            set { _Emp_ContribucionPapel = value; }
        }
        private double? _Emp_ContribucionNoPapel;

        public double? Emp_ContribucionNoPapel
        {
            get { return _Emp_ContribucionNoPapel; }
            set { _Emp_ContribucionNoPapel = value; }
        }
        private double? _Emp_Ucs;

        public double? Emp_Ucs
        {
            get { return _Emp_Ucs; }
            set { _Emp_Ucs = value; }
        }
        private double? _Emp_Isr;

        public double? Emp_Isr
        {
            get { return _Emp_Isr; }
            set { _Emp_Isr = value; }
        }
        private double? _Emp_Inversion;

        public double? Emp_Inversion
        {
            get { return _Emp_Inversion; }
            set { _Emp_Inversion = value; }
        }
        private double? _Emp_Cetes;

        public double? Emp_Cetes
        {
            get { return _Emp_Cetes; }
            set { _Emp_Cetes = value; }
        }
        private double? _Emp_AdicionalCetes;

        public double? Emp_AdicionalCetes
        {
            get { return _Emp_AdicionalCetes; }
            set { _Emp_AdicionalCetes = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public string Emp_Nombre
        {
            get { return _Emp_Nombre.Trim(); }
            set { _Emp_Nombre = value.Trim(); }
        }
        public string Emp_Cnx
        {
            get { return _Emp_Cnx.Trim(); }
            set { _Emp_Cnx = value.Trim(); }
        }
        public bool Verifica1
        {
            get { return _Verifica1; }
            set { _Verifica1 = value; }
        }
        public bool Verifica2
        {
            get { return _Verifica2; }
            set { _Verifica2 = value; }
        }
      
        public string RiFactura
        {
            get { return _RiFactura; }
            set { _RiFactura = value; }
        }
        public bool Emp_Activo
        {
            get { return _Emp_Activo; }
            set { _Emp_Activo = value; }
        }
        public string Emp_ActivoStr
        {
            get { return _Emp_ActivoStr; }
            set { _Emp_ActivoStr = value; }
        }
    }
}
