using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteDet
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Cte;
        int _Id_CteDet;
        int _Id_Ter;
        string _Territorio;
        int _Ter_Tipo;
     
        string _Segmento;
        string _Cte_UnidadDim;
        string _Cte_Dimension;
        double _Cte_Pesos;
        double _Cte_Potencial;
        bool _Estatus;
        string _EstatusStr;
        string _DescTer_Tipo;



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
        public string Territorio
        {
            get { return _Territorio; }
            set { _Territorio = value; }
        }

        public int Ter_Tipo
        {
            get { return _Ter_Tipo; }
            set { _Ter_Tipo = value; }
        }

        public string DescTer_Tipo
        {
            get { return _DescTer_Tipo; }
            set { _DescTer_Tipo = value; }
        }


        public int Id_Seg
        {
            get { return Id_Seg; }
            set { Id_Seg = value; }
        }
        public string Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }
        public string Cte_UnidadDim
        {
            get { return _Cte_UnidadDim; }
            set { _Cte_UnidadDim = value; }
        }
        public string Cte_Dimension
        {
            get { return _Cte_Dimension; }
            set { _Cte_Dimension = value; }
        }
        public double Cte_Pesos
        {
            get { return _Cte_Pesos; }
            set { _Cte_Pesos = value; }
        }
        public double Cte_Potencial
        {
            get { return _Cte_Potencial; }
            set { _Cte_Potencial = value; }
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
    }
}
