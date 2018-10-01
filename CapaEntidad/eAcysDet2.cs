using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eAcysDet2
    {
        private int _Id_Prd;
        private string _Prd_Descripcion;
        private string _Prd_Presentacion;
        
        private double _Acs_Precio;
        private Int32 _Acs_Cantidad;
        private int _Acs_Frecuencia;
        private bool _Acs_Lunes;
        private bool _Acs_Martes;
        private bool _Acs_Miercoles;
        private bool _Acs_Jueves;
        private bool _Acs_Viernes;
        private bool _Acs_Sabado;

        private string _Acs_Documento;
        private string _Acs_ConsigFechaInicio;
        private string _Acs_ConsigFechaFin;
        
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        private string _Uni_Decripcion;
        public string Uni_Descripcion
        {
            get { return _Uni_Decripcion; }
            set { _Uni_Decripcion = value; }
        }

        public double Acs_Precio
        {
            get { return _Acs_Precio; }
            set { _Acs_Precio = value; }
        }
        public Int32 Acs_Cantidad
        {
            get { return _Acs_Cantidad; }
            set { _Acs_Cantidad = value; }
        }
        public int Acs_Frecuencia
        {
            get { return _Acs_Frecuencia; }
            set { _Acs_Frecuencia = value; }
        }
        public bool Acs_Lunes
        {
            get { return _Acs_Lunes; }
            set { _Acs_Lunes = value; }
        }
        public bool Acs_Martes
        {
            get { return _Acs_Martes; }
            set { _Acs_Martes= value; }
        }
        public bool Acs_Miercoles
        {
            get { return _Acs_Miercoles; }
            set { _Acs_Miercoles = value; }
        }
        public bool Acs_Jueves
        {
            get { return _Acs_Jueves; }
            set { _Acs_Jueves = value; }
        }
        public bool Acs_Viernes
        {
            get { return _Acs_Viernes; }
            set { _Acs_Viernes = value; }
        }
        public bool Acs_Sabado
        {
            get { return _Acs_Sabado; }
            set { _Acs_Sabado = value; }
        }

        public string Acs_Documento
        {
            get { return _Acs_Documento; }
            set { _Acs_Documento = value; }
        }
        public string Acs_ConsigFechaInicio
        {
            get { return _Acs_ConsigFechaInicio; }
            set { _Acs_ConsigFechaInicio = value; }
        }
        public string Acs_ConsigFechaFin
        {
            get { return _Acs_ConsigFechaFin; }
            set { _Acs_ConsigFechaFin = value; }
        }

        private Int32 _Acs_CantTotal;
        private Int32 _Acs_UltSCpt;
        private Int32 _Acs_UltACpt;
        private Int32 _Id_TG;

        public Int32 Acs_CantTotal
        {
            get { return _Acs_CantTotal; }
            set { _Acs_CantTotal = value; }
        }
        public Int32 Acs_UltSCpt
        {
            get { return _Acs_UltSCpt; }
            set { _Acs_UltSCpt = value; }
        }
        public Int32 Acs_UltACpt
        {
            get { return _Acs_UltACpt; }
            set { _Acs_UltACpt = value; }
        }
        public Int32 Id_TG
        {
            get { return _Id_TG; }
            set { _Id_TG = value; }
        }
    }
    //
}
