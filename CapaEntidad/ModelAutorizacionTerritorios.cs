using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ModelAutorizacionTerritorios
    {
        public long IdAutorizacion { get; set; }
        public int IdRepresentante { get; set; }
        public long ClaveTerritorio { get; set; }
        public string Territorio { get; set; }
        public bool Activo { get; set; }
        public int Estatus { get; set; }
        public long IdUSolicita { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public long IdUAutoriza { get; set; }
        public DateTime FechaAutorizo { get; set; }
        public string NombreRepresentante { get; set; }
        public string TerritorioCambio { get; set; }
        public string NombreSolicitante { get; set; }
        public string NombreRepresentanteActual { get; set; }
        public string NombreAprobador { get; set; }
    }
}
