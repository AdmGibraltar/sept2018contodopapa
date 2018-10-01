using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConsecutivoFE
    {
        private int _Id;
        private string _NombreAcuse;
        private string _FolioSAT;
        private int _Año;
        private string _RazonSocial;
        private string _NumRazonSocial;
        private double? _UltimoFolio;
        private int _RangoInicial;
        private int _RangoFinal;
        private DateTime _RangoFecha;
        private int _TipoMovimiento;
        private string _FolioAprovacion;
        private int _Empresa;
        private int _CentroDistribucion;
        private bool _Estatus;
        private string _EstatusStr;
        public string TipoMovimientoOld;



        public object Id
        {
            get { return _Id; }
            set { _Id = Convert.ToInt32(value); }
        }
        public object NombreAcuse
        {
            get { return _NombreAcuse; }
            set { _NombreAcuse = Convert.ToString(value); }
        }
        public object FolioSAT
        {
            get { return _FolioSAT; }
            set { _FolioSAT = value.ToString(); }
        }
        public object Año
        {
            get { return _Año; }
            set { _Año = Convert.ToInt32(value); }
        }
        public object RazonSocial
        {
            get { return _RazonSocial; }
            set { _RazonSocial = Convert.ToString(value); }
        }
        public object NumRazonSocial
        {
            get { return _NumRazonSocial; }
            set { _NumRazonSocial = value.ToString(); }
        }
        public double? UltimoFolio
        {
            get { return _UltimoFolio; }
            set { _UltimoFolio = value; }
        }
        public object RangoInicial
        {
            get { return _RangoInicial; }
            set { _RangoInicial = Convert.ToInt32(value); }
        }
        public object RangoFinal
        {
            get { return _RangoFinal; }
            set { _RangoFinal = Convert.ToInt32(value); }
        }
        public object RangoFecha
        {
            get { return _RangoFecha; }
            set { _RangoFecha = Convert.ToDateTime(value); }
        }
        public object TipoMovimiento
        {
            get { return _TipoMovimiento; }
            set { _TipoMovimiento = Convert.ToInt32(value); }
        }
        public object FolioAprovacion
        {
            get { return _FolioAprovacion; }
            set { _FolioAprovacion = value.ToString(); }
        }
        public object Empresa
        {
            get { return _Empresa; }
            set { _Empresa = Convert.ToInt32(value); }
        }
        public object CentroDistribucion
        {
            get { return _CentroDistribucion; }
            set { _CentroDistribucion = Convert.ToInt32(value); }
        }
        public object Estatus
        {
            get { return _Estatus; }
            set { _Estatus = Convert.ToBoolean(value); }
        }
        public object EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = Convert.ToString(value); }
        }
    }
}
