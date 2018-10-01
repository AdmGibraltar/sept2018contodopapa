using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RequisitoCita
    {
        private int _Id_CriterioCita;
        private int _IdPreRequisito;
        private int _Secuencia; 
        string _PreRequisito;

        public int Id_CriterioCita
        {
            get { return _Id_CriterioCita; }
            set { _Id_CriterioCita = value; }
        }

        public int IdPreRequisito
        {
            get { return _IdPreRequisito; }
            set { _IdPreRequisito = value; }
        }

        public int Secuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }

        public string PreRequisito
        {
            get { return _PreRequisito; }
            set { _PreRequisito = value; }
        }

    }
}
