//Autor: Oscar Casillas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CapaEntidad
{
   public class CompraLocal
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private string _Cd_Nombre;
        private int _Id_Comp;
        private int _Comp_Solicito;
        private string _Solicito_Nombre;
        private DateTime _FechaSol;
        private string _FechaAut;
        private int _Comp_Autorizo;
        private double _Comp_Descuento;


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
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }
        public int Id_Comp
        {
            get { return _Id_Comp; }
            set { _Id_Comp = value; }
        }
        public int Comp_Solicito
        {
            get { return _Comp_Solicito; }
            set { _Comp_Solicito = value; }
        }
        public string Solicito_Nombre
        {
            get { return _Solicito_Nombre; }
            set { _Solicito_Nombre = value; }
        }
        public DateTime FechaSol
        {
            get { return _FechaSol; }
            set { _FechaSol = value; }
        }
        public string FechaAut
        {
            get { return _FechaAut; }
            set { _FechaAut = value; }
        }
        public int Comp_Autorizo
        {
            get { return _Comp_Autorizo; }
            set { _Comp_Autorizo = value; }
        }
        public double Comp_Descuento
        {
            get { return _Comp_Descuento; }
            set { _Comp_Descuento = value; }
        }

    }
}
