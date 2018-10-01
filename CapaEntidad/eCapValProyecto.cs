using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// RFH 24 Mayo 2018
//  
//


namespace CapaEntidad
{
    public class eCapValProyecto
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Vap;

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
        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }

        private string _Vap_Fecha;
        private int _Id_U;
        private int _Id_Cte;

        public string Vap_Fecha
        {
            get { return _Vap_Fecha; }
            set { _Vap_Fecha = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _CteNombre;
        public string CteNombre { get { return _CteNombre; } set { _CteNombre = value; } }

        private string _Vap_Nota;
        private string _Vap_Estatus;

        public string Vap_Nota
        {
            get { return _Vap_Nota; }
            set { _Vap_Nota = value; }
        }
        public string Vap_Estatus
        {
            get { return _Vap_Estatus; }
            set { _Vap_Estatus = value; }
        }

        private decimal _Vap_UtilidadRemanente;
        private decimal _Vap_ValorPresenteNeto;
        public decimal Vap_UtilidadRemanente
        {
            get { return _Vap_UtilidadRemanente; }
            set { _Vap_UtilidadRemanente = value; }
        }
        public decimal Vap_ValorPresenteNeto
        {
            get { return _Vap_ValorPresenteNeto; }
            set { _Vap_ValorPresenteNeto = value; }
        }

        private int _Vap_Estatus2;
        public int Vap_Estatus2
        {
            get { return _Vap_Estatus2; }
            set { _Vap_Estatus2 = value; }
        }

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private string _RikNombre;
        public string RikNombre { get { return _RikNombre; } set { _RikNombre = value; } }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _MotivoParaAutorizacion;
        public string MotivoParaAutorizacion
        {
            get { return _MotivoParaAutorizacion; }
            set { _MotivoParaAutorizacion = value; }
        }

        // 11 Sep 2018 RFH
        private int _Id_Op;
        public int Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }

        private string _AplNombre;
        public string AplNombre { get { return _AplNombre; } set { _AplNombre = value; } }


        //
    }
}