using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class InvExcesoInventario
    {
        #region Variables
        int id_Emp;
        int id_Cd;
        int id_Pvd;
        int id_Prd;
        string pvd_Descrip;
        string prd_Descrip;
        double costo;
        double excedente;
        double excedente2;
        double excedente3;
        double monto;
        double monto2;
        double monto3;
       
        /*Filtro*/
        string excesosInventarios;
        string productos;
        string tipos;
        string tipo_proveedor;
        string mostrars;
        string producto;//arreglar producto
        int tipo;
        int tipo_prov;
        int mostrar;
        int exceso;
        int tipoFecha;
        DateTime fecha;
        #endregion

        #region refactorizar
        public string ExcesosInventarios
        {
            get { return excesosInventarios; }
            set { excesosInventarios = value; }
        }
        public string Productos
        {
            get { return productos; }
            set { productos = value; }
        }
        public string Tipos
        {
            get { return tipos; }
            set { tipos = value; }
        }
        public string Tipo_proveedor
        {
            get { return tipo_proveedor; }
            set { tipo_proveedor = value; }
        }
        public string Mostrars
        {
            get { return mostrars; }
            set { mostrars = value; }
        }
        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public int Id_Pvd
        {
            get { return id_Pvd; }
            set { id_Pvd = value; }
        }
        public int Id_Prd
        {
            get { return id_Prd; }
            set { id_Prd = value; }
        }
        public string Pvd_Descrip
        {
            get { return pvd_Descrip; }
            set { pvd_Descrip = value; }
        }
        public string Prd_Descrip
        {
            get { return prd_Descrip; }
            set { prd_Descrip = value; }
        }
        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }
        public double Excedente
        {
            get { return excedente; }
            set { excedente = value; }
        }
        public double Excedente2
        {
            get { return excedente2; }
            set { excedente2 = value; }
        }
        public double Excedente3
        {
            get { return excedente3; }
            set { excedente3 = value; }
        }
        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public double Monto2
        {
            get { return monto2; }
            set { monto2 = value; }
        }   
        public double Monto3
        {
            get { return monto3; }
            set { monto3 = value; }
        }

        /*Filtro*/
        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Tipo_prov
        {
            get { return tipo_prov; }
            set { tipo_prov = value; }
        }
        public int Mostrar
        {
            get { return mostrar; }
            set { mostrar = value; }
        }
        public int Exceso
        {
            get { return exceso; }
            set { exceso = value; }
        }
        public int TipoFecha
        {
            get { return tipoFecha; }
            set { tipoFecha = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        #endregion
    }
}
