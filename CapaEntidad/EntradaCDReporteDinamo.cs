using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class EntradaCDReporteDinamo
    {   
        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }                 
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        
        private string _CDI;
        public string CDI
        {
            get { return _CDI; }
            set { _CDI = value; }
        }

        private bool _EsZona;
        public bool EsZona
        {
            get { return _EsZona; }
            set { _EsZona = value; }
        }

        private string _Cd_Nombre;
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre= value; }
        }

        private string _ZonaNombre;
        public string ZonaNombre
        {
            get { return _ZonaNombre; }
            set { _ZonaNombre = value; }
        }

        private Int32 _CD_Zona;
        public Int32  CD_Zona
        {
            get { return _CD_Zona; }
            set { _CD_Zona = value; }
        }

        private string _Zona;
        public string Zona
        {
            get { return _Zona; }
            set { _Zona = value; }
        }


        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik= value; }
        }

        private string _Rik_Nombre;
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre= value; }
        }
        
        private decimal _ProyectosPromocion_Importe;
        public decimal ProyectosPromocion_Importe
        {
            get { return _ProyectosPromocion_Importe; }
            set { _ProyectosPromocion_Importe = value; }
        }

        private decimal _ProyectosPromocion_Cumplimiento;
        public decimal ProyectosPromocion_Cumplimiento
        {
            get { return _ProyectosPromocion_Cumplimiento; }
            set { _ProyectosPromocion_Cumplimiento = value; }
        }

        private decimal _ProyectosPromocion_Num;
        public decimal ProyectosPromocion_Num
        {
            get { return _ProyectosPromocion_Num; }
            set { _ProyectosPromocion_Num = value; }
        }

        private decimal _ProyectosIngresadosNumeroProyectos;
        public decimal ProyectosIngresadosNumeroProyectos
        {
            get { return _ProyectosIngresadosNumeroProyectos; }
            set { _ProyectosIngresadosNumeroProyectos = value; }
        }

        private int _ProyectosIngresados_Num;
        private decimal _ProyectosIngresados_Importe;
        public int ProyectosIngresados_Num
        {
            get { return _ProyectosIngresados_Num; }
            set { _ProyectosIngresados_Num = value; }
        }
        public decimal ProyectosIngresados_Importe
        {
            get { return _ProyectosIngresados_Importe; }
            set { _ProyectosIngresados_Importe = value; }
        }

        private decimal _ProyectosIngresadosImporteProyecto;
        public decimal ProyectosIngresadosImporteProyecto
        {
            get { return _ProyectosIngresadosImporteProyecto; }
            set { _ProyectosIngresadosImporteProyecto = value; }
        }

        private decimal _ProyectosPromocionMontoProyecto;
        public decimal ProyectosPromocionMontoProyecto
        {
            get { return _ProyectosPromocionMontoProyecto; }
            set { _ProyectosPromocionMontoProyecto= value; }
        }

        private decimal _ProyectosPromocion_Monto;
        
        private decimal _ProyectosPromocion_Plantilla;

        public decimal ProyectosPromocion_Monto
        {
            get { return _ProyectosPromocion_Monto; }
            set { _ProyectosPromocion_Monto = value; }
        }        
                      
        public decimal ProyectosPromocion_Plantilla
        {
            get { return _ProyectosPromocion_Plantilla; }
            set { _ProyectosPromocion_Plantilla = value; }
        }
                
        private decimal _ProyectosPromocionCumplimiento;
        public decimal ProyectosPromocionCumplimiento
        {
            get { return _ProyectosPromocionCumplimiento; }
            set { _ProyectosPromocionCumplimiento= value; }
        }

        private decimal _CierreMonto;
        public decimal CierreMonto
        {
            get { return _CierreMonto; }
            set { _CierreMonto = value; }
        }

        private decimal _CierreCumplimiento;
        public decimal CierreCumplimiento
        {
            get { return _CierreCumplimiento; }
            set { _CierreCumplimiento = value; }
        }

        private int _CanceladoNumProy;
        public int CanceladoNumProy
        {
            get { return _CanceladoNumProy; }
            set { _CanceladoNumProy = value; }
        }

        private decimal _CanceladoImporteProy;
        public decimal CanceladoImporteProy
        {
            get { return _CanceladoImporteProy; }
            set { _CanceladoImporteProy = value; }
        }
        
        private decimal _EntradasFrecuencia;
        public decimal EntradasFrecuencia
        {
            get { return _EntradasFrecuencia; }
            set { _EntradasFrecuencia= value; }
        }

        private int _Indice;
        internal int Indice
        {
            get { return _Indice; }
            set { _Indice = value; }
        }



        
        private double _CuotaCumplimientoMontoProyecto;
        public double CuotaCumplimientoMontoProyecto
        {
            get { return _CuotaCumplimientoMontoProyecto; }
            set { _CuotaCumplimientoMontoProyecto = value; }
        }

        private double _CuotaCumplimientoMontoCierre;
        public double CuotaCumplimientoMontoCierre
        {
            get { return _CuotaCumplimientoMontoCierre; }
            set { _CuotaCumplimientoMontoCierre = value; }
        }

        private decimal _ProyectosAnalisisMontoProyecto;
        public decimal ProyectosAnalisisMontoProyecto
        {
            get { return _ProyectosAnalisisMontoProyecto; }
            set { _ProyectosAnalisisMontoProyecto = value; }
        }

        private decimal _ProyectosPresentacionMontoProyecto;
        public decimal ProyectosPresentacionMontoProyecto
        {
            get { return _ProyectosPresentacionMontoProyecto; }
            set { _ProyectosPresentacionMontoProyecto = value; }
        }

        private decimal _ProyectosNegociacionMontoProyecto;
        public decimal ProyectosNegociacionMontoProyecto
        {
            get { return _ProyectosNegociacionMontoProyecto; }
            set { _ProyectosNegociacionMontoProyecto = value; }
        }

        private decimal _ProyectosCierre_Monto;
        private decimal _ProyectosCierre_Cumplimiento;
        private decimal _ProyectosCierre_Plantilla;
        public decimal ProyectosCierre_Monto
        {
            get { return _ProyectosCierre_Monto; }
            set { _ProyectosCierre_Monto = value; }
        }        
        public decimal ProyectosCierre_Cumplimiento
        {
            get { return _ProyectosCierre_Cumplimiento; }
            set { _ProyectosCierre_Cumplimiento = value; }
        }
        public decimal ProyectosCierre_Plantilla
        {
            get { return _ProyectosCierre_Plantilla; }
            set { _ProyectosCierre_Plantilla = value; }
        }

        private decimal _ProyectosCancelados_Num;
        private decimal _ProyectosCancelados_Importe;
        public decimal ProyectosCancelados_Num
        {
            get { return _ProyectosCancelados_Num; }
            set { _ProyectosCancelados_Num = value; }
        }        
        public decimal ProyectosCancelados_Importe
        {
            get { return _ProyectosCancelados_Importe; }
            set { _ProyectosCancelados_Importe = value; }
        }      
        
        private decimal _ProyectosAnalisisCumplimientoProyecto;
        public decimal ProyectosAnalisisCumplimientoProyecto
        {
            get { return _ProyectosAnalisisCumplimientoProyecto; }
            set { _ProyectosAnalisisCumplimientoProyecto = value; }
        }

        private decimal _ProyectosPresentacionCumplimientoProyecto;
        public decimal ProyectosPresentacionCumplimientoProyecto
        {
            get { return _ProyectosPresentacionCumplimientoProyecto; }
            set { _ProyectosPresentacionCumplimientoProyecto = value; }
        }

        private decimal _ProyectosNegociacionCumplimientoProyecto;
        public decimal ProyectosNegociacionCumplimientoProyecto
        {
            get { return _ProyectosNegociacionCumplimientoProyecto; }
            set { _ProyectosNegociacionCumplimientoProyecto = value; }
        }

        public string StyleCdNombre
        {
            get
            {
                if (EsZona)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    sb.Append("text-align: center;");
                    return sb.ToString();
                }
                return "";
            }
        }

        public string StyleProyectosIngresadosNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleProyectosIngresadosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }

                return sb.ToString();
            }
        }

        public string StyleProyectosPromocionMontoProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleProyectosPromocionCumplimientoProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }

                if (ProyectosPromocion_Cumplimiento <= .5M)
                {
                    sb.Append("background-color: red;");
                }
                else if (ProyectosPromocion_Cumplimiento >= .51M && ProyectosPromocion_Cumplimiento <= .7M)
                {
                    sb.Append("background-color: yellow;");
                }
                else
                {
                    sb.Append("background-color: green;");
                }

                return sb.ToString();
            }
        }
        
        public string StyleCierreMontoCierre
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleCierreCumplimiento
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }

                if (ProyectosCierre_Cumplimiento <= .5M)
                {
                    sb.Append("background-color: red;");
                }
                else if (ProyectosCierre_Cumplimiento >= .51M && ProyectosCierre_Cumplimiento <= .7M)
                {
                    sb.Append("background-color: yellow;");
                }
                else
                {
                    sb.Append("background-color: green;");
                }

                return sb.ToString();
            }
        }

        public string StyleCanceladosNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleCanceladosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleEntradasFrecuencia
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleProyectosPromocionPlantilla
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleCierrePlantilla
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }


    }

}
