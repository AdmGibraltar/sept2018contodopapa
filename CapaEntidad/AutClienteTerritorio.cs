using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AutClienteTerritorio
    {
        #region Variables
        private int id_Cd;
        private int id_Emp;
        private int id_Cliente;
        private string nom_Cliente;
        private int id_Territorio;
        private string nom_Territorio;
        private double dimension;
        private double pesos;
        private double potencial;
        private double manodeobra;
        private double gastosTerritorio;
        private double fletespagadosporcliente;
        private double porcentaje;
        private bool activo;
        private bool nuevo;
        private string comentarios;
        private int id_Solicitud;

        


         #endregion

        #region Metodos
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
         public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }
        public int Id_Cliente
        {
            get { return id_Cliente; }
            set { id_Cliente = value; }
        }

        public string Nom_Cliente
        {
            get { return nom_Cliente; }
            set { nom_Cliente = value; }
        }

        public int Id_Territorio
        {
            get { return id_Territorio; }
            set { id_Territorio = value; }
        }

        public string Nom_Territorio
        {
            get { return nom_Territorio; }
            set { nom_Territorio = value; }
        }

        public double Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }

        public double Pesos
        {
            get { return pesos; }
            set { pesos = value; }
        }

        public double Potencial
        {
            get { return potencial; }
            set { potencial = value; }
        }

        public double Manodeobra
        {
            get { return manodeobra; }
            set { manodeobra = value; }
        }

        public double GastosTerritorio
        {
            get { return gastosTerritorio; }
            set { gastosTerritorio = value; }
        }

        public double Fletespagadosporcliente
        {
            get { return fletespagadosporcliente; }
            set { fletespagadosporcliente = value; }
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

        public bool Nuevo
        {
            get { return nuevo; }
            set { nuevo = value; }
        }

        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }

        public int Id_Solicitud
        {
            get { return id_Solicitud; }
            set { id_Solicitud = value; }
        }

        #endregion
    }
}
