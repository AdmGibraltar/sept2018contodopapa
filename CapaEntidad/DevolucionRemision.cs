using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DevolucionRemision
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

        private int _Id_DevRem;
        public int Id_DevRem
        {
            get { return _Id_DevRem; }
            set { _Id_DevRem = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }



        private int _Cant;
        public int Cant
        {
            get { return _Cant; }
            set { _Cant = value; }
        }


        private int _Es_Referencia;
        public int Es_Referencia
        {
            get { return _Es_Referencia; }
            set { _Es_Referencia = value; }
        }


        private string _DevRem_UNombre;
        public string DevRem_UNombre
        {
            get { return _DevRem_UNombre; }
            set { _DevRem_UNombre = value; }
        }

        private string _Id_Prd;
        public string Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }


        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }


        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }



        private string _DevRem_CteNombre;
        public string DevRem_CteNombre
        {
            get { return _DevRem_CteNombre; }
            set { _DevRem_CteNombre = value; }
        }


        private int _Id_Es;
        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }


        private int _Id_Fac;
        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }


        private int _Id_Tm;
        public int Id_Tm
        {
            get { return _Id_Tm; }
            set { _Id_Tm = value; }
        }



        private string _DevRem_TmNombre;
        public string DevRem_TmNombre
        {
            get { return _DevRem_TmNombre; }
            set { _DevRem_TmNombre = value; }
        }

        private DateTime _DevRem_Fecha;
        public DateTime DevRem_Fecha
        {
            get { return _DevRem_Fecha; }
            set { _DevRem_Fecha = value; }
        }

        private int _Folio1;
        public int Folio1
        {
            get { return _Folio1; }
            set { _Folio1 = value; }
        }

        private int _Folio2;
        public int Folio2
        {
            get { return _Folio2; }
            set { _Folio2 = value; }
        }

        private DateTime? _Fecha1;
        public DateTime? Fecha1
        {
            get { return _Fecha1; }
            set { _Fecha1 = value; }
        }

        private DateTime? _Fecha2;
        public DateTime? Fecha2
        {
            get { return _Fecha2; }
            set { _Fecha2 = value; }
        }




        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }


        private string _EstatusStr;
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }

        private string _NumEntradas;
        public string NumEntradas
        {
            get { return _NumEntradas; }
            set { _NumEntradas = value; }
        }

        private int _Id_CteFiltro1;
        public int Id_CteFiltro1
        {
            get { return _Id_CteFiltro1; }
            set { _Id_CteFiltro1 = value; }
        }

        private int _Id_CteFiltro2;
        public int Id_CteFiltro2
        {
            get { return _Id_CteFiltro2; }
            set { _Id_CteFiltro2 = value; }
        }

        private int _Id_TmInv;

        public int Id_TmInv
        {
            get { return _Id_TmInv; }
            set { _Id_TmInv = value; }
        }


        private int _Es_NatInv;
        public int Es_NatInv
        {
            get { return _Es_NatInv; }
            set { _Es_NatInv = value; }
        }

        private string _DevRem_Tipo;
        public string DevRem_Tipo
        {
            get { return _DevRem_Tipo; }
            set { _DevRem_Tipo = value; }
        }


        private List<DevolucionRemisionDet> _DevolucionRemisionDet;
        public List<DevolucionRemisionDet> ListDevolucionRemisionDet
        {
            get { return _DevolucionRemisionDet; }
            set { _DevolucionRemisionDet = value; }
        }


        public List<EntradaSalida> _ListEntradaSalida;
        public List<EntradaSalida> ListEntradaSalida
        {
            get { return _ListEntradaSalida; }
            set { _ListEntradaSalida = value; }
        }


        public int Id_TG { get; set; }





    }
}
