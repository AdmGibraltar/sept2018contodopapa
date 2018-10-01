using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Key Quimica 
 * 06 Abr 2018
 */


namespace CapaEntidad
{
    public class eCapAcysDet
    {

        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Acs;
        private int _Id_AcsDet;
        private int _Id_Reg;
        private int _Id_Prd;
        private int _Acs_Cantidad;
        private int _Acs_Frecuencia;
        private bool _Acs_Lunes;
        private bool _Acs_Martes;
        private bool _Acs_Miercoles;
        private bool _Acs_Jueves;
        private bool _Acs_Viernes;
        private bool _Acs_Sabado;
        private string _Acs_Documento;
        private double _Acs_Precio;
        private int _Id_Ter;
        private int _Acs_UltSCpt;
        private int _Acs_UltACpt;
        private string _Acs_Modalidad;
        private DateTime _Acs_ConsigFechaInicio;
        private DateTime _Acs_ConsigFechaFin;
        private int _Acs_canTTotal;
        private int _Id_AcsVersion;
        private int _Id_TG;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp= value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd= value; }
        }
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs= value; }
        }
        public int Id_AcsDet
        {
            get { return _Id_AcsDet; }
            set { _Id_AcsDet= value; }
        }
        public int Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg= value; }
        }

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd= value; }
        }
        public int Acs_Cantidad
        {
            get { return _Acs_Cantidad; }
            set { _Acs_Cantidad= value; }
        }
        public int Acs_Frecuencia
        {
            get { return _Acs_Frecuencia; }
            set { _Acs_Frecuencia= value; }
        }
        public bool Acs_Lunes
        {
            get { return _Acs_Lunes; }
            set { _Acs_Lunes= value; }
        }
        public bool Acs_Martes
        {
            get { return _Acs_Martes; }
            set { _Acs_Martes= value; }
        }
        public bool Acs_Miercoles
        {
            get { return _Acs_Miercoles; }
            set { _Acs_Miercoles= value; }
        }
        public bool Acs_Jueves
        {
            get { return _Acs_Jueves; }
            set { _Acs_Jueves= value; }
        }
        public bool Acs_Viernes
        {
            get { return _Acs_Viernes; }
            set { _Acs_Viernes= value; }
        }
        public bool Acs_Sabado
        {
            get { return _Acs_Sabado; }
            set { _Acs_Sabado= value; }
        }
        public string Acs_Documento
        {
            get { return _Acs_Documento; }
            set { _Acs_Documento= value; }
        }

        public double Acs_Precio
        {
            get { return _Acs_Precio; }
            set { _Acs_Precio= value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter= value; }
        }

        public int Acs_UltSCpt
        {
            get { return _Acs_UltSCpt; }
            set { _Acs_UltSCpt= value; }
        }
        public int Acs_UltACpt
        {
            get { return _Acs_UltACpt; }
            set { _Acs_UltACpt= value; }
        }

        public string Acs_Modalidad
        {
            get { return _Acs_Modalidad; }
            set { _Acs_Modalidad= value; }
        }
        public DateTime Acs_ConsigFechaInicio
        {
            get { return _Acs_ConsigFechaInicio; }
            set { _Acs_ConsigFechaInicio= value; }
        }
        public DateTime Acs_ConsigFechaFin
        {
            get { return _Acs_ConsigFechaFin; }
            set { _Acs_ConsigFechaFin = value; }
        }
        public int Acs_canTTotal
        {
            get { return _Acs_canTTotal; }
            set { _Acs_canTTotal = value; }
        }
        public int Id_AcsVersion
        {
            get { return _Id_AcsVersion; }
            set { _Id_AcsVersion = value; }
        }
        public int Id_TG
        {
            get { return _Id_TG; }
            set { _Id_TG = value; }
        }
        //
    }
}
