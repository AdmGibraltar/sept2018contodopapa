using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteDetContacto
    {
        int _Id;
        public int Id 
        {
            get { return _Id; }
            set { _Id = value; }
        }

        int _Id_Emp;
        int _Id_Cd;
        int _Id_Cte;
        int _Id_CteDet;
        int _Id_Ter;
        int _Id_Seg;        
        string _Nombre;
        string _Puesto;
        string _Cumpleanios;
        string _Correo;
        string _Direccion1;
        string _Direccion2;
        string _TelNegocio;
        string _TelCasa;
        int _Id_Consecutivo;

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
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_CteDet
        {
            get { return _Id_CteDet; }
            set { _Id_CteDet = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg= value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Puesto
        {
            get { return _Puesto; }
            set { _Puesto= value; }
        }
        public string Cumpleanios
        {
            get { return _Cumpleanios; }
            set { _Cumpleanios= value; }
        }
        public string Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }
        public string Direccion1
        {
            get { return _Direccion1; }
            set { _Direccion1 = value; }
        }
        public string Direccion2
        {
            get { return _Direccion2; }
            set { _Direccion2 = value; }
        }
        public string TelNegocio
        {
            get { return _TelNegocio; }
            set { _TelNegocio = value; }
        }
        public string TelCasa
        {
            get { return _TelCasa; }
            set { _TelCasa = value; }
        }

        public int Id_Consecutivo
        {
            get { return _Id_Consecutivo; }
            set { _Id_Consecutivo = value; }
        }
    }
}
