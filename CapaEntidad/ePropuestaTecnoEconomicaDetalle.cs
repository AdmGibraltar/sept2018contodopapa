using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ePropuestaTecnoEconomicaDetalle
    {

        private int _Id_Val;
        private int _Id_Op;
        private int _Id_Prd;
        private int _Id_VapDet;

        public int Id_Val
        {
            get { return _Id_Val; }
            set { _Id_Val = value; }
        }
        public int Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public int Id_VapDet
        {
            get { return _Id_VapDet; }
            set { _Id_VapDet = value; }
        }        
                
        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }        


        private decimal _Vap_Precio;
        private decimal _Prd_Presentacion;
        private decimal _COP_ConsumoMensual;        
        private decimal _ConsumoMensualL;
        private decimal _GastoMensual;
                         
        private decimal _COP_DilucionAntecedente;
        private decimal _COP_DilucionConsecuente;
        private decimal _ConsumoMensualLDiluidos;        
        private decimal _CostoEnUso;

        private decimal _Vap_Cantidad;
        public decimal Vap_Cantidad
        {
            get { return _Vap_Cantidad; }
            set { _Vap_Cantidad = value; }
        }

        public decimal Vap_Precio
        {
            get { return _Vap_Precio; }
            set { _Vap_Precio = value; }
        }
        public decimal Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        public decimal COP_ConsumoMensual
        {
            get { return _COP_ConsumoMensual; }
            set { _COP_ConsumoMensual = value; }
        }
        public decimal ConsumoMensualL
        {
            get { return _ConsumoMensualL; }
            set { _ConsumoMensualL = value; }
        }
        public decimal GastoMensual
        {
            get { return _GastoMensual; }
            set { _GastoMensual = value; }
        }
        public decimal COP_DilucionAntecedente
        {
            get { return _COP_DilucionAntecedente; }
            set { _COP_DilucionAntecedente = value; }
        }
        public decimal COP_DilucionConsecuente
        {
            get { return _COP_DilucionConsecuente; }
            set { _COP_DilucionConsecuente = value; }
        }
        public decimal ConsumoMensualLDiluidos
        {
            get { return _ConsumoMensualLDiluidos; }
            set { _ConsumoMensualLDiluidos = value; }
        }
        public decimal CostoEnUso
        {
            get { return _CostoEnUso; }
            set { _CostoEnUso = value; }
        }        
        
        private string _CPT_ProductoActual;
        private string _CPT_RecursoImagenProductoActual;
        private string _CPT_SituacionActual;
        private string _ProductoKey;
        private string _CPT_RecursoImagenSolucionKey;
        private string _CPT_VentajasKey;

        public string CPT_ProductoActual
        {
            get { return _CPT_ProductoActual; }
            set { _CPT_ProductoActual = value; }
        }
        public string CPT_RecursoImagenProductoActual
        {
            get { return _CPT_RecursoImagenProductoActual; }
            set { _CPT_RecursoImagenProductoActual = value; }
        }
        public string CPT_SituacionActual
        {
            get { return _CPT_SituacionActual; }
            set { _CPT_SituacionActual = value; }
        }
        public string ProductoKey
        {
            get { return _ProductoKey; }
            set { _ProductoKey = value; }
        }
        public string CPT_RecursoImagenSolucionKey
        {
            get { return _CPT_RecursoImagenSolucionKey; }
            set { _CPT_RecursoImagenSolucionKey = value; }
        }
        public string CPT_VentajasKey
        {
            get { return _CPT_VentajasKey; }
            set { _CPT_VentajasKey = value; }
        }
        
        private int _COP_EsQuimico;
        public int COP_EsQuimico
        {
            get { return _COP_EsQuimico; }
            set { _COP_EsQuimico = value; }
        }

        private decimal _Prd_UniEmp;
        public decimal Prd_UniEmp
        {
            get { return _Prd_UniEmp; }
            set { _Prd_UniEmp = value; }
        }

        private int _AplDilucion;
        public int AplDilucion
        {
            get { return _AplDilucion; }
            set { _AplDilucion = value; }
        }                
      
        //
    }
}





