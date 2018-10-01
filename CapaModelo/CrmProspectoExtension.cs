using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaModelo
{
    public partial class CrmProspecto
    {
        public string Cte_NomComercial
        {
            get;
            set;
        }

        public string Cte_Contacto
        {
            get;
            set;
        }

        public string Cte_Email
        {
            get;
            set;
        }

        public string Cte_Calle
        {
            get;
            set;
        }

        public string Cte_Telefono
        {
            get;
            set;
        }

        public string Cte_Rfc
        {
            get;
            set;
        }

        public int[] Territorios
        {
            get;
            set;
        }

        public List<ProspectoTerritorioViewPOST> TerritoriosAsociados
        {
            get;
            set;
        }

        public class ProspectoTerritorioViewPOST
        {
            public int Id_Ter
            {
                get;
                set;
            }

            public double VPO
            {
                get;
                set;
            }
        }

        /// <summary>
        /// Esta propiedad se utiliza para devolver los territorios asociados con el prospecto cuando es requerido en las vistas.
        /// </summary>
        public List<CatTerritorio> TerritoriosDeProspectoSerializable
        {
            get;
            set;
        }
    }
}
