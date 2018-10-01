using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Crm_Prospecto
    {

        private int _Id_CrmProspecto;
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Rik;
        private int _Id_Cte;
        private int _Id_CrmTipoCliente;
        private int _Id_Ter_Temporal;
            
        public int Id_CrmProspecto {
            get { return _Id_CrmProspecto; }
            set { _Id_CrmProspecto = value; }
        }

		public int Id_Emp {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public int Id_CrmTipoCliente
        {
            get { return _Id_CrmTipoCliente; }
            set { _Id_CrmTipoCliente = value; }
        }

        public int Id_Ter_Temporal
        {
            get { return _Id_Ter_Temporal; }
            set { _Id_Ter_Temporal = value; }
        }

        //

    }
}
