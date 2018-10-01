using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// RFH 18 Mayo 2018
//  Entidad para carga de archivos
//

namespace CapaEntidad
{
    public class eCRMArchivosCargados
    {
        private int _IdAchivo;
        public int IdAchivo
        {
            get { return _IdAchivo; }
            set { _IdAchivo = value; }
        }
        private string _NombreArchivo;
        public string NombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }
        private string _Hash;
        public string Hash
        {
            get { return _Hash; }
            set { _Hash = value; }
        }

        private int _IdDocumento;
        public int IdDocumento
        {
            get { return _IdDocumento; }
            set { _IdDocumento = value; }
        }

        private int _IdUser;
        public int IdUser
        {
            get { return _IdUser; }
            set { _IdUser = value; }
        }

        private string _Fecha;
        public string Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private int _Activo;
        public int Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
        private int _IdDocTipo;
        public int IdDocTipo
        {
            get { return _IdDocTipo; }
            set { _IdDocTipo = value; }
        }
        
    }
}
