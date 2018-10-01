using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DbCentro
    {
        public int Id_Emp { get; set; }

        public int Id_Cd { get; set; }

        public string Db_Nombre { get; set; }

        public string Db_CdNombre { get; set; }

        public DateTime? Db_CerradoExtemporaneo { get; set; }
    }  
}
