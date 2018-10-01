using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AjusteBaseInstaladaDet
    {
        private int _Id_Emp;
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

        private int _Id_Abi;
        public int Id_Abi
        {
            get { return _Id_Abi; }
            set { _Id_Abi = value; }
        }

        private int _Id_AbiDet;
        public int Id_AbiDet
        {
            get { return _Id_AbiDet; }
            set { _Id_AbiDet = value; }
        }

        private int _Abi_Tipo;
        public int Abi_Tipo
        {
            get { return _Abi_Tipo; }
            set { _Abi_Tipo = value; }
        }

        private string _Abi_TipoStr;
        public string Abi_TipoStr
        {
            get { return _Abi_TipoStr; }
            set { _Abi_TipoStr = value; }
        }

        private int? _Id_Ter_Origen;
        public int? Id_Ter_Origen
        {
            get { return _Id_Ter_Origen; }
            set { _Id_Ter_Origen = value; }
        }

        private int? _Id_Cte_Origen;
        public int? Id_Cte_Origen
        {
            get { return _Id_Cte_Origen; }
            set { _Id_Cte_Origen = value; }
        }

        private int? _Id_Prd_Origen;
        public int? Id_Prd_Origen
        {
            get { return _Id_Prd_Origen; }
            set { _Id_Prd_Origen = value; }
        }

        private double? _Abi_CantActual_Origen;
        public double? Abi_CantActual_Origen
        {
            get { return _Abi_CantActual_Origen; }
            set { _Abi_CantActual_Origen = value; }
        }

        private double? _Abi_CantQuitar_Origen;
        public double? Abi_CantQuitar_Origen
        {
            get { return _Abi_CantQuitar_Origen; }
            set { _Abi_CantQuitar_Origen = value; }
        }

        private int? _Id_Ter_Destino;
        public int? Id_Ter_Destino
        {
            get { return _Id_Ter_Destino; }
            set { _Id_Ter_Destino = value; }
        }

        private int? _Id_Cte_Destino;
        public int? Id_Cte_Destino
        {
            get { return _Id_Cte_Destino; }
            set { _Id_Cte_Destino = value; }
        }

        private int? _Id_Prd_Destino;
        public int? Id_Prd_Destino
        {
            get { return _Id_Prd_Destino; }
            set { _Id_Prd_Destino = value; }
        }

        private double? _Abi_CantActual_Destino;
        public double? Abi_CantActual_Destino
        {
            get { return _Abi_CantActual_Destino; }
            set { _Abi_CantActual_Destino = value; }
        }

        private double? _Abi_CantQuitar_Destino;
        public double? Abi_CantQuitar_Destino
        {
            get { return _Abi_CantQuitar_Destino; }
            set { _Abi_CantQuitar_Destino = value; }
        }

        private string _Abi_ExplicacionCaso;
        public string Abi_ExplicacionCaso
        {
            get { return _Abi_ExplicacionCaso; }
            set { _Abi_ExplicacionCaso = value; }
        }

        private string _Abi_Estatus;
        private string _Abi_EstatusStr;

        public string Abi_EstatusStr
        {
            get { return _Abi_EstatusStr; }
            set { _Abi_EstatusStr = value; }
        }
        public string Abi_Estatus
        {
            get { return _Abi_Estatus; }
            set { _Abi_Estatus = value; }
        }
    }
}
