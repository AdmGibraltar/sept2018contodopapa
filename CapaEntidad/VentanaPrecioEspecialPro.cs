using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class VentanaPrecioEspecialPro
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ape;
        int _Id_ApePro;
        int _Id_Prd;
        int _Id_Mon;
        string _Prd_Descripcion;
        int _Ape_VolVta;
        double _Ape_PreVta;
        DateTime _Ape_FecInicio;
        DateTime _Ape_FecFin;
        double _Ape_PreEsp;
        private string _Mon_Descripcion;
        private string _Ape_Estatus;
        private DateTime _Ape_FecAut;
        private double _Ape_PreAAA;

        public double Ape_PreAAA
        {
            get { return _Ape_PreAAA; }
            set { _Ape_PreAAA = value; }
        }

        public DateTime Ape_FecAut
        {
            get { return _Ape_FecAut; }
            set { _Ape_FecAut = value; }
        }

        public string Ape_Estatus
        {
            get { return _Ape_Estatus; }
            set { _Ape_Estatus = value; }
        }

        public string Mon_Descripcion
        {
            get { return _Mon_Descripcion; }
            set { _Mon_Descripcion = value; }
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
        public int Id_Ape
        {
            get { return _Id_Ape; }
            set { _Id_Ape = value; }
        }
        public int Id_ApePro
        {
            get { return _Id_ApePro; }
            set { _Id_ApePro = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public int Id_Mon
        {
            get { return _Id_Mon; }
            set { _Id_Mon = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        public int Ape_VolVta
        {
            get { return _Ape_VolVta; }
            set { _Ape_VolVta = value; }
        }
        public double Ape_PreVta
        {
            get { return _Ape_PreVta; }
            set { _Ape_PreVta = value; }
        }
        public DateTime Ape_FecInicio
        {
            get { return _Ape_FecInicio; }
            set { _Ape_FecInicio = value; }
        }
        public DateTime Ape_FecFin
        {
            get { return _Ape_FecFin; }
            set { _Ape_FecFin = value; }
        }
        public double Ape_PreEsp
        {
            get { return _Ape_PreEsp; }
            set { _Ape_PreEsp = value; }
        }
    }
}
