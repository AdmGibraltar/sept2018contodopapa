using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AutorizaOrdenCom
    {
        private int _Id_OrdCompra;

        public int Id_OrdCompra
        {
            get { return _Id_OrdCompra; }
            set { _Id_OrdCompra = value; }
        }

        private int _Id_Prd;

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        private string _Prd_Nom;

        public string Prd_Nom
        {
            get { return _Prd_Nom; }
            set { _Prd_Nom = value; }
        }
        private string _Prd_Presentacion;

        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        private int _Vta1;

        public int Vta1
        {
            get { return _Vta1; }
            set { _Vta1 = value; }
        }
        private int _Vta2;

        public int Vta2
        {
            get { return _Vta2; }
            set { _Vta2 = value; }
        }
        private int _Vta3;

        public int Vta3
        {
            get { return _Vta3; }
            set { _Vta3 = value; }
        }
        private int _Vta0;

        public int Vta0
        {
            get { return _Vta0; }
            set { _Vta0 = value; }
        }
        private int _Promedio;

        public int Promedio
        {
            get { return _Promedio; }
            set { _Promedio = value; }
        }
        private int _Existencia;

        public int Existencia
        {
            get { return _Existencia; }
            set { _Existencia = value; }
        }
        private int _Maximo;

        public int Maximo
        {
            get { return _Maximo; }
            set { _Maximo = value; }
        }
        private int _Ordenado;

        public int Ordenado
        {
            get { return _Ordenado; }
            set { _Ordenado = value; }
        }
        private int _Autorizacion;

        public int Autorizacion
        {
            get { return _Autorizacion; }
            set { _Autorizacion = value; }
        }

        private int _Id_U;

        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        private int _Pendiente;

        public int Pendiente
        {
            get { return _Pendiente; }
            set { _Pendiente = value; }
        }
    }
}
