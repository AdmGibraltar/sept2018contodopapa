using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class VentaSemanal
    {
        #region Atributos
        private int id_Ter;
        private string nom_Ter;
        private int id_Cte;
        private string nom_Cte;
        private int id_prd;
        private string nom_Prd;
        private int unidades;
        private float importe;
        private int anio;
        private int semana;
        private string mes;

        #endregion

        #region Metodos

        public int Id_Ter
        {
            get { return id_Ter; }
            set { id_Ter = value; }
        }

        public string Nom_Ter
        {
            get { return nom_Ter; }
            set { nom_Ter = value; }
        }

        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }

        public string Nom_Cte
        {
            get { return nom_Cte; }
            set { nom_Cte = value; }
        }

        public int Id_prd
        {
            get { return id_prd; }
            set { id_prd = value; }
        }

        public string Nom_Prd
        {
            get { return nom_Prd; }
            set { nom_Prd = value; }
        }

        public int Unidades
        {
            get { return unidades; }
            set { unidades = value; }
        }

        public float Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }

        public int Semana
        {
            get { return semana; }
            set { semana = value; }
        }

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        #endregion
    }
}
