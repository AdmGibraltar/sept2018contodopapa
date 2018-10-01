﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AutPrecioEspecial
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ape;
        DateTime _Ape_Fecha;
        string _Ape_Estatus;
        int _Id_Cte;
        string _Cte_NomComercial;
        int? _Folio1;
        int? _Folio2;
        DateTime? _Fecha1;
        DateTime? _Fecha2;
        string _Estatus;
        int _Vencido;
        string _VencidoStr;
        DateTime? _CalendarioFin;
        private string _Ape_EstatusStr;
        int _Id_CteFiltro1;
        int _Id_CteFiltro2;
        int _Solicitud;

        public int Id_CteFiltro1
        {
            get { return _Id_CteFiltro1; }
            set { _Id_CteFiltro1 = value; }
        }
        public int Id_CteFiltro2
        {
            get { return _Id_CteFiltro2; }
            set { _Id_CteFiltro2 = value; }
        }
        public int Solicitud
        {
            get { return _Solicitud; }
            set { _Solicitud = value; }
        }
        public string Ape_EstatusStr
        {
            get { return _Ape_EstatusStr; }
            set { _Ape_EstatusStr = value; }
        }
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
        public int Id_Ape
        {
            get { return _Id_Ape; }
            set { _Id_Ape = value; }
        }
        public DateTime Ape_Fecha
        {
            get { return _Ape_Fecha; }
            set { _Ape_Fecha = value; }
        }
        public string Ape_Estatus
        {
            get { return _Ape_Estatus; }
            set { _Ape_Estatus = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public int? Folio1
        {
            get { return _Folio1; }
            set { _Folio1 = value; }
        }
        public int? Folio2
        {
            get { return _Folio2; }
            set { _Folio2 = value; }
        }
        public DateTime? Fecha1
        {
            get { return _Fecha1; }
            set { _Fecha1 = value; }
        }
        public DateTime? Fecha2
        {
            get { return _Fecha2; }
            set { _Fecha2 = value; }
        }
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public int Vencido
        {
            get { return _Vencido; }
            set { _Vencido = value; }
        }
        public string VencidoStr
        {
            get { return _VencidoStr; }
            set { _VencidoStr = value; }
        }
        public DateTime? CalendarioFin
        {
            get { return _CalendarioFin; }
            set { _CalendarioFin = value; }
        }
    }
}
