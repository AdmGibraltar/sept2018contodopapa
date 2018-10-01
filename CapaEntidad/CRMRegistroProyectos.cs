using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CRMRegistroProyectos
    {
        public CRMRegistroProyectos()
        {
        }

        public CRMRegistroProyectos(CRMRegistroProyectos aCopiar)
        {
            this.Analisis = aCopiar.Analisis;
            this.Aplicacion = aCopiar.Aplicacion;
            this.Aplicaciones = aCopiar.Aplicaciones;
            this.Area = aCopiar.Area;
            this.Cancelacion = aCopiar.Cancelacion;
            this.Cierre = aCopiar.Cierre;
            this.Cliente = aCopiar.Cliente;
            this.Comentarios = aCopiar.Comentarios;
            this.ComentariosCancel = aCopiar.ComentariosCancel;
            this.Competidor = aCopiar.Competidor;
            this.CrmOp_VPM = aCopiar.CrmOp_VPM;
            this.Dim_Cantidad = aCopiar.Dim_Cantidad;
            this.Dim_Id_Seg = aCopiar.Dim_Id_Seg;
            this.Dim_Id_Uen = aCopiar.Dim_Id_Uen;
            this.Estatus = aCopiar.Estatus;
            this.FechaCotizacion = aCopiar.FechaCotizacion;
            this.Id_Cam = aCopiar.Id_Cam;
            this.Id_Causa = aCopiar.Id_Causa;
            this.Id_CrmProspecto = aCopiar.Id_CrmProspecto;
            this.Id_Op = aCopiar.Id_Op;
            this.IdCausa = aCopiar.IdCausa;
            this.IdMax = aCopiar.IdMax;
            this.Negociacion = aCopiar.Negociacion;
            this.Presentacion = aCopiar.Presentacion;
            this.Productos = aCopiar.Productos;
            this.Segmento = aCopiar.Segmento;
            this.Solucion = aCopiar.Solucion;
            this.Territorio = aCopiar.Territorio;
            this.Uen = aCopiar.Uen;
            this.ValorPotencialO = aCopiar.ValorPotencialO;
            this.ValorPotencialT = aCopiar.ValorPotencialT;
            this.VentaNoRepetitiva = aCopiar.VentaNoRepetitiva;
            this.VentaPromedio = aCopiar.VentaPromedio;
            Crm_TipoVenta = aCopiar.Crm_TipoVenta;
        }

        #region variables
        int uen;
        int segmento;
        int territorio;
        int area;
        int solucion;
        int aplicacion;
        int cliente;
        bool ventaNoRepetitiva;
        string comentarios;
        string productos;
        DateTime analisis;
        DateTime presentacion;
        DateTime negociacion;
        DateTime cancelacion;
        DateTime fechaCotizacion;
        double ventaPromedio;
        double valorPotencialO;
        double valorPotencialT;
        int estatus;
        int idMax;
        int idCausa;
        string competidor;
        string comentariosCancel;
        private int? _Id_Op;
        private DateTime _Cierre;
        private int _Id_Causa;
        private int _Id_Cam;
        private int _Id_CrmProspecto;

        public int Id_Causa
        {
            get { return _Id_Causa; }
            set { _Id_Causa = value; }
        }

         public int Id_Cam         
        {
            get { return _Id_Cam; }
            set { _Id_Cam = value; }
        }

        public DateTime Cierre
        {
            get { return _Cierre; }
            set { _Cierre = value; }
        }

        public int? Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }

        public int Id_CrmProspecto
        {
            get
            {
                return _Id_CrmProspecto;
            }
            set
            {
                _Id_CrmProspecto = value;
            }
        }

        public int? Crm_TipoVenta
        {
            get;
            set;
        }

        /// <summary>
        /// Indica si la entrada fué generada desde la versión II de CRM
        /// </summary>
        public bool? CrmOp_OrigenCRMII
        {
            get;
            set;
        }

        #endregion

        #region factorizacion
        public int Uen
        {
            get { return uen; }
            set { uen = value; }
        }
        public int Segmento
        {
            get { return segmento; }
            set { segmento = value; }
        }
        public int Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public int Area
        {
            get { return area; }
            set { area = value; }
        }
        public int Solucion
        {
            get { return solucion; }
            set { solucion = value; }
        }
        public int Aplicacion
        {
            get { return aplicacion; }
            set { aplicacion = value; }
        }
        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public bool VentaNoRepetitiva
        {
            get { return ventaNoRepetitiva; }
            set { ventaNoRepetitiva = value; }
        }
        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }
        public string Productos
        {
            get { return productos; }
            set { productos = value; }
        }
        public DateTime Analisis
        {
            get { return analisis; }
            set { analisis = value; }
        }
        public DateTime Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }
        public DateTime Negociacion
        {
            get { return negociacion; }
            set { negociacion = value; }
        }
        public DateTime Cancelacion
        {
            get { return cancelacion; }
            set { cancelacion = value; }
        }
        public DateTime FechaCotizacion
        {
            get { return fechaCotizacion; }
            set { fechaCotizacion = value; }
        }
        public double VentaPromedio
        {
            get { return ventaPromedio; }
            set { ventaPromedio = value; }
        }
        public double ValorPotencialO
        {
            get { return valorPotencialO; }
            set { valorPotencialO = value; }
        }
        public double ValorPotencialT
        {
            get { return valorPotencialT; }
            set { valorPotencialT = value; }
        }
        public int Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        public int IdMax
        {
            get { return idMax; }
            set { idMax = value; }
        }
        public int IdCausa
        {
            get { return idCausa; }
            set { idCausa = value; }
        }
        public string Competidor
        {
            get { return competidor; }
            set { competidor = value; }
        }
        public string ComentariosCancel
        {
            get { return comentariosCancel; }
            set { comentariosCancel = value; }
        }

        public int[] Aplicaciones
        {
            get;
            set;
        }

        public _Aplicacion[] AplicacionesV2
        {
            get;
            set;
        }

        public int? Dim_Id_Uen
        {
            get;
            set;
        }

        public int? Dim_Id_Seg
        {
            get;
            set;
        }

        public decimal? Dim_Cantidad
        {
            get;
            set;
        }

        public decimal? CrmOp_VPM
        {
            get;
            set;
        }
        #endregion

        public class _Aplicacion
        {
            public int Id_Aplicacion
            {
                get;
                set;
            }

            public decimal VPO
            {
                get;
                set;
            }
        }
    }
}
