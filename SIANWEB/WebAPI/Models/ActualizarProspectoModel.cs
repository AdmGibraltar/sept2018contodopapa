using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaModelo;

namespace SIANWEB.WebAPI.Models
{
    public class ActualizarProspectoModel
    {
        public string idCrmProspecto
        {
            get;
            set;
        }

        public string hdnId_Cte
        {
            get;
            set;
        }

        public string hdnId_Rik
        {
            get;
            set;
        }

        public string hdnId_CrmTipoCliente
        {
            get;
            set;
        }

        public string txtNombre
        {
            get;
            set;
        }

        public string txtContacto
        {
            get;
            set;
        }

        public string txtEmail
        {
            get;
            set;
        }

        public string txtCalle
        {
            get;
            set;
        }

        public string txtTelefono
        {
            get;
            set;
        }

        public string RFC
        {
            get;
            set;
        }

        public int[] Territorios
        {
            get;
            set;
        }

        public int? TerritorioTemporal
        {
            get;
            set;
        }

        public List<CrmProspecto.ProspectoTerritorioViewPOST> TerritoriosAsociados
        {
            get;
            set;
        }
    }
}