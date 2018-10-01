using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable()]
    public class PagoDetComplemento
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Pag;
        private int _Id_Cte;
        private int _Id_PagComp;
        private int _Id_PagDet;
        private int? _Cte_Fpago;
        private string _Cte_UsoCFDI;
        private string _Pago_FolioFiscal;
        private object _Pago_Xml;
        private object _Pago_Pdf;
        private string _Pago_Serie;

        public int Id_PagDet
        {
            get { return _Id_PagDet; }
            set { _Id_PagDet = value; }
        }

        public string Pago_Serie
        {
            get { return _Pago_Serie; }
            set { _Pago_Serie = value; }
        }

        public object Pago_Pdf
        {
            get { return _Pago_Pdf; }
            set { _Pago_Pdf = value; }
        }
        public object Pago_Xml
        {
            get { return _Pago_Xml; }
            set { _Pago_Xml = value; }
        }
        public string Pago_FolioFiscal
        {
            get { return _Pago_FolioFiscal; }
            set { _Pago_FolioFiscal = value; }
        }
        public int Id_Pag
        {
            get { return _Id_Pag; }
            set { _Id_Pag = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_PagComp
        {
            get { return _Id_PagComp; }
            set { _Id_PagComp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Cte_UsoCFDI
        {
            get { return _Cte_UsoCFDI; }
            set { _Cte_UsoCFDI = value;}
        }
        public int? Cte_Fpago
        {
            get { return _Cte_Fpago; }
            set { _Cte_Fpago = value; }
        }
    }
}
