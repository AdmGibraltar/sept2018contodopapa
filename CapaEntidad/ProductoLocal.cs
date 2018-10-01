//Autor: Oscar Casillas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProductoLocal
    {
        private int _Id_Det;
        private int _Id_Prd;
        private string _Descripcion;
        private double _Costo;
        private string _FechaAut;
        private bool _Autorizado;
        private bool _CompraEnfocada;
        private int _Autorizo;
        private bool _Rechazado;
        private string _Estatus;

        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        public bool Rechazado
        {
            get { return _Rechazado; }
            set { _Rechazado = value; }
        }
        public int Id_Det
        {
            get { return _Id_Det; }
            set { _Id_Det = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public double Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }
        public string FechaAut
        {
            get { return _FechaAut; }
            set { _FechaAut = value; }
        }
        public bool Autorizado
        {
            get { return _Autorizado; }
            set { _Autorizado = value; }
        }
        public bool CompraEnfocada
        {
            get { return _CompraEnfocada; }
            set { _CompraEnfocada = value; }
        }
        public int Autorizo
        {
            get { return _Autorizo; }
            set { _Autorizo = value; }
        }        
    }
}
