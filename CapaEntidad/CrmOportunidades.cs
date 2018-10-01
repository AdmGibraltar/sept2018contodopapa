using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CrmOportunidades
    {
        #region Variables
        int id;
        string descripcion;
        int id_Emp;
        int id_Cd;
        int id_Op;
        int id_Cte;
        int id_Ter;
        int id_Seg;
        int id_Usu;
        int iD_Area;
        int id_Apl;
        int id_Sol;
        int id_Uen;
        string productos;
        bool ventaNoRepetitiva;
        string comentarios;
        string analisis;
        string presentacion;
        string negociacion;
        string cierre;
        string fechaModificacion;
        string fechaCotizacion;
        double ventaMensual;
        string fechaCancelacion;
        string cancelacion;
        string competidor;
        string comentariosCancel;
        double montoProyecto;
        int avances;
        int mes;
        int año;
        int id_Causa;
        int estatus;
        
        double valorPotencialT;
        double valorPotencialO;
        DateTime dAnalisis;
        DateTime dPresentacion;
        DateTime dNegociacion;
        DateTime dCierre;
        DateTime dCancelacion;
        DateTime dFechaModificacion;
        DateTime dFechaCotizacion;
        DateTime dFechaCancelacion;
        double porcentaje;
        bool activo;
        int id_Estruc;
        int id_Cam;
        string campania;

        bool tienecampania;

        #endregion
        #region refactorizar
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Campania
        {
            get { return campania; }
            set { campania = value; }
        }

        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }

        // RFH 
        public bool tieneCampania
        {
            get { return tienecampania; }
            set { tienecampania = value; }
        }

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public int Id_Op
        {
            get { return id_Op; }
            set { id_Op = value; }
        }
        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }
        public int Id_Ter
        {
            get { return id_Ter; }
            set { id_Ter = value; }
        }
        public int Id_Seg
        {
            get { return id_Seg; }
            set { id_Seg = value; }
        }
        public int Id_Usu
        {
            get { return id_Usu; }
            set { id_Usu = value; }
        }
        public int ID_Area
        {
            get { return iD_Area; }
            set { iD_Area = value; }
        }
        public int Id_Apl
        {
            get { return id_Apl; }
            set { id_Apl = value; }
        }
        public int Id_Sol
        {
            get { return id_Sol; }
            set { id_Sol = value; }
        }
        public int Id_Uen
        {
            get { return id_Uen; }
            set { id_Uen = value; }
        }
        public string Productos
        {
            get { return productos; }
            set { productos = value; }
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
        public string Analisis
        {
            get { return analisis; }
            set { analisis = value; }
        }
        public string Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }
        public string Negociacion
        {
            get { return negociacion; }
            set { negociacion = value; }
        }
        public string Cierre
        {
            get { return cierre; }
            set { cierre = value; }
        }
        public string FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        public string FechaCotizacion
        {
            get { return fechaCotizacion; }
            set { fechaCotizacion = value; }
        }
        public double VentaMensual
        {
            get { return ventaMensual; }
            set { ventaMensual = value; }
        }
        public string FechaCancelacion
        {
            get { return fechaCancelacion; }
            set { fechaCancelacion = value; }
        }
        public string Cancelacion
        {
            get { return cancelacion; }
            set { cancelacion = value; }
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
        public double MontoProyecto
        {
            get { return montoProyecto; }
            set { montoProyecto = value; }
        }
        public int Avances
        {
            get { return avances; }
            set { avances = value; }
        }
        public int Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        public int Año
        {
            get { return año; }
            set { año = value; }
        }
        public int Id_Causa
        {
            get { return id_Causa; }
            set { id_Causa = value; }
        }
        public int Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        public double ValorPotencialT
        {
            get { return valorPotencialT; }
            set { valorPotencialT = value; }
        }
        public double ValorPotencialO
        {
            get { return valorPotencialO; }
            set { valorPotencialO = value; }
        }
        public DateTime DAnalisis
        {
            get { return dAnalisis; }
            set { dAnalisis = value; }
        }
        public DateTime DPresentacion
        {
            get { return dPresentacion; }
            set { dPresentacion = value; }
        }
        public DateTime DNegociacion
        {
            get { return dNegociacion; }
            set { dNegociacion = value; }
        }
        public DateTime DCierre
        {
            get { return dCierre; }
            set { dCierre = value; }
        }
        public DateTime DCancelacion
        {
            get { return dCancelacion; }
            set { dCancelacion = value; }
        }
        public DateTime DFechaModificacion
        {
            get { return dFechaModificacion; }
            set { dFechaModificacion = value; }
        }
        public DateTime DFechaCotizacion
        {
            get { return dFechaCotizacion; }
            set { dFechaCotizacion = value; }
        }
        public DateTime DFechaCancelacion
        {
            get { return dFechaCancelacion; }
            set { dFechaCancelacion = value; }
        }
        public double Porcentaje
        {
            get { return porcentaje; }
            set { porcentaje = value; }
        }
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        public int Id_Estruc
        {
            get { return id_Estruc; }
            set { id_Estruc = value; }
        }

        public int? Id_Cam { get; set; }
       


        public int[] Aplicaciones
        {
            get;
            set;
        }

        /// <summary>
        /// Colección de las aplicaciones asociadas a la oportunidad. Nota: Las entidades deben de ubicarse en la misma librería.
        /// </summary>
        public Object[] CrmOportunidadesAplicaciones
        {
            get;
            set;
        }
        #endregion
    }
}
