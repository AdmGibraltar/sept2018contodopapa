using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PedidoDet
    {
        private int _Id_Ter;
        private int _Id_Prd;
        private string _Prd_Desc;
        private int _Prd_Ord;
        private int _Prd_OrdDisp;
        private int _Prd_Asig;
        private int _Prd_Faltante;
        private int _Prd_Existencia;
        private int _Prd_Disponible;
        private int _Id_PedDet;
        private int _Id_Emp;
        private int _Ped_CantF;

        public int Ped_CantF
        {
            get { return _Ped_CantF; }
            set { _Ped_CantF = value; }
        }
        private int _Ped_CantR;

        public int Ped_CantR
        {
            get { return _Ped_CantR; }
            set { _Ped_CantR = value; }
        }

 
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        private int _Id_Ped;
        private string _Ter_Descripcion;

        public string Ter_Descripcion
        {
            get { return _Ter_Descripcion; }
            set { _Ter_Descripcion = value; }
        }
        private int _Original;

        public int Original
        {
            get { return _Original; }
            set { _Original = value; }
        }
        private int _Cancelado;

        public int Cancelado
        {
            get { return _Cancelado; }
            set { _Cancelado = value; }
        }
        private int _Pendiente;

        public int Pendiente
        {
            get { return _Pendiente; }
            set { _Pendiente = value; }
        }
        private int _Final;

        public int Final
        {
            get { return _Final; }
            set { _Final = value; }
        }

        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }

        public int Id_PedDet
        {
            get { return _Id_PedDet; }
            set { _Id_PedDet = value; }
        }

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public string Prd_Desc
        {
            get { return _Prd_Desc; }
            set { _Prd_Desc = value; }
        }
        public int Prd_Ord
        {
            get { return _Prd_Ord; }
            set { _Prd_Ord = value; }
        }
        public int Prd_OrdDisp
        {
            get { return _Prd_OrdDisp; }
            set { _Prd_OrdDisp = value; }
        }
        public int Prd_Asig
        {
            get { return _Prd_Asig; }
            set { _Prd_Asig = value; }
        }
        public int Prd_Faltante
        {
            get { return _Prd_Faltante; }
            set { _Prd_Faltante = value; }
        }
        public int Prd_Existencia
        {
            get { return _Prd_Existencia; }
            set { _Prd_Existencia = value; }
        }
        public int Prd_Disponible
        {
            get { return _Prd_Disponible; }
            set { _Prd_Disponible = value; }
        }

        private int _prd_NoConf;
        public int Prd_NoConf
        {
            get { return _prd_NoConf; }
            set { _prd_NoConf = value; }
        }

        private int _prd_NoEnc;
        public int Prd_NoEnc
        {
            get { return _prd_NoEnc; }
            set { _prd_NoEnc = value; }
        }

        private int _prd_PorcentajeAsignado;
        public int Prd_PorcentajeAsignado
        {
            get { return _prd_PorcentajeAsignado; }
            set { _prd_PorcentajeAsignado = value; }
        }
    }
}
