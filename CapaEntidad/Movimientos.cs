using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Movimientos
    {
        int _Id_Emp;
        int _Id;
        int _Id_Cd;
        string _Nombre;
        int _Tipo;
        int _Naturaleza;
        int _Inverso;
        bool _AfeIVA;
        bool _AfeVta;
        bool _AfeOrdComp;
        int _Afecta;
        bool _Estatus;
        string _EstatusStr;
        int _NatMov;
        bool _ReqReferencia;
        bool _ReqSispropietario;

        public bool ReqSispropietario
        {
            get { return _ReqSispropietario; }
            set { _ReqSispropietario = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public bool ReqReferencia
        {
            get { return _ReqReferencia; }
            set { _ReqReferencia = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int Naturaleza
        {
            get { return _Naturaleza; }
            set { _Naturaleza = value; }
        }
        public int Inverso
        {
            get { return _Inverso; }
            set { _Inverso = value; }
        }
        public bool AfeIVA
        {
            get { return _AfeIVA; }
            set { _AfeIVA = value; }
        }
        public bool AfeVta
        {
            get { return _AfeVta; }
            set { _AfeVta = value; }
        }
        public bool AfeOrdComp
        {
            get { return _AfeOrdComp; }
            set { _AfeOrdComp = value; }
        }
        public int Afecta
        {
            get { return _Afecta; }
            set { _Afecta = value; }
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
        public int NatMov
        {
            get { return _NatMov; }
            set { _NatMov = value; }
        }
    }
}
