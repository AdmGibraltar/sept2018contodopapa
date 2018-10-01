using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PagoDet
    {
        private int _Mov;
        private int _Id_cd;
        private string _Ref;
        private int _Ficha;
        private string _Cheque;
        private double _Importe;
        private string _MovStr;
        private int _Id_Terr;
        private DateTime _Doc_Fecha;
        private int _Id_Cte;
        private string _Cte_Nombre;
        private int _Pag_Numero;
        private string _Pag_Cheque;
        private string _Doc_Estatus;
        private double _Doc_Importe; 

        public string MovStr
        {
            get { return _MovStr; }
            set { _MovStr = value; }
        }
        public int Id_Terr
        {
            get { return _Id_Terr; }
            set { _Id_Terr = value; }
        }
        public DateTime Doc_Fecha
        {
            get { return _Doc_Fecha; }
            set { _Doc_Fecha = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        public int Pag_Numero
        {
            get { return _Pag_Numero; }
            set { _Pag_Numero = value; }
        }
        private object _Doc_Pagado;
        public object Serie;
        public int Pag_Id_Ter;
        public DateTime Pag_Fac_Fecha;
        public int Pag_Id_cte;
        public string Pag_Cte_Nombre;
        public string Pag_Cheque
        {
            get { return _Pag_Cheque; }
            set { _Pag_Cheque = value; }
        }
        public string Doc_Estatus
        {
            get { return _Doc_Estatus; }
            set { _Doc_Estatus = value; }
        }
        public double Doc_Importe
        {
            get { return _Doc_Importe; }
            set { _Doc_Importe = value; }
        }
        public object Doc_Pagado
        {
            get { return _Doc_Pagado; }
            set { _Doc_Pagado = value; }
        }
        public int Mov
        {
            get { return _Mov; }
            set { _Mov = value; }
        }
        public int Pag_Id_cd
        {
            get { return _Id_cd; }
            set { _Id_cd = value; }
        }
        public string Ref
        {
            get { return _Ref; }
            set { _Ref = value; }
        }
        public int Ficha
        {
            get { return _Ficha; }
            set { _Ficha = value; }
        }
        public string Cheque
        {
            get { return _Cheque; }
            set { _Cheque = value; }
        }
        public double Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }
    }
}
