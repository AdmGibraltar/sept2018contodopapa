using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CrmPromociones
    {
        #region Variables
        int id;
        int ids;
        string descripcion;
        //filtro1
        int cds;
        int representante;
        int uen;
        int segmento;
        int territorio;
        int area;
        int solucion;
        int aplicacion;
        int estatus;
        //clientes
        int _Id_Cte;
        string _NombreCte;
        int _Id_Ter;
        string _Ter_Nombre;
        int _Id_Uen;
        string _Uen_Descrip;
        //resultados
        double _Cli_VPObservado;
        double _VentaMensual;
        string _Analisis;
        string _Presentacion;
        string _Negociacion;
        string _Cierre;
        string _Cancelacion;
        string _FechaCancelacion;
        int _Avances;
        int cliente;
        string _MesModificacion;
        public string Id_Rik;

        public int Id_Apl
        {
            get;
            set;
        }

        public int Id_Sol
        {
            get;
            set;
        }

        public int Id_Area
        {
            get;
            set;
        }

        public int Id_Seg
        {
            get;
            set;
        }

        public int IdUen
        {
            get;
            set;
        }
        #endregion

        #region Refactorizar
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Ids
        {
            get { return ids; }
            set { ids = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        //filtro1
        public int Cds
        {
            get { return cds; }
            set { cds = value; }
        }
        public int Representante
        {
            get { return representante; }
            set { representante = value; }
        }
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
        //filtro2
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
        public int Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        //clientes
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string NombreCte
        {
            get { return _NombreCte; }
            set { _NombreCte = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        public string Uen_Descrip
        {
            get { return _Uen_Descrip; }
            set { _Uen_Descrip = value; }
        }
        //resultados
        public double Cli_VPObservado
        {
            get { return _Cli_VPObservado; }
            set { _Cli_VPObservado = value; }
        }
        public double VentaMensual
        {
            get { return _VentaMensual; }
            set { _VentaMensual = value; }
        }
        public string Analisis
        {
            get { return _Analisis; }
            set { _Analisis = value; }
        }
        public string Presentacion
        {
            get { return _Presentacion; }
            set { _Presentacion = value; }
        }
        public string Negociacion
        {
            get { return _Negociacion; }
            set { _Negociacion = value; }
        }
        public string Cierre
        {
            get { return _Cierre; }
            set { _Cierre = value; }
        }
        public string Cancelacion
        {
            get { return _Cancelacion; }
            set { _Cancelacion = value; }
        }
        public string FechaCancelacion
        {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = value; }
        }
        public int Avances
        {
            get { return _Avances; }
            set { _Avances = value; }
        }
        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public string MesModificacion
        {
            get { return _MesModificacion; }
            set { _MesModificacion  = value; }
        }

        public int Id_CrmProspecto
        {
            get;
            set;
        }

        public bool? VentaNoRepetitiva
        {
            get;
            set;
        }

        public int[] Aplicaciones
        {
            get;
            set;
        }

        public bool? EnValuacion
        {
            get;
            set;
        }

        public string Seg_Descripcion
        {
            get;
            set;
        }

        public string Area_Descripcion
        {
            get;
            set;
        }

        public string Sol_Descripcion
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

        public string Dim_Descripcion
        {
            get;
            set;
        }

        public double ValorPotencialTeorico
        {
            get;
            set;
        }

        public decimal VentaPromedioMensualEsperada
        {
            get;
            set;
        }

        public int CrmTipoCliente
        {
            get;
            set;
        }

        public object CrmValuacionOportunidades
        {
            get;
            set;
        }

        /// <summary>
        /// Representa la aportación de la utilidad remanente de este proyecto en la valuación asociada.
        /// </summary>
        public decimal? UtilidadRemanente
        {
            get;
            set;
        }

        /// <summary>
        /// Representa la aportación del valor presente neto de este proyecto en la valuación asociada.
        /// </summary>
        public decimal? ValorPresenteNeto
        {
            get;
            set;
        }

        /// <summary>
        /// Indica si el proyecto ha sido cancelado
        /// </summary>
        public bool Cancelado
        {
            get
            {
                return Estatus == 5;
            }
        }

        #endregion
    }
}
