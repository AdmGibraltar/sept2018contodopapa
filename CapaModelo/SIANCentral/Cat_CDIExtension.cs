using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaModelo.SIANCentral
{
    public class CentroDistribucionNoEncontrado
        : Exception
    {
        public CentroDistribucionNoEncontrado(int idCd)
            : base(string.Format("El centro de distribución con identificador {0} no fué encontrado", idCd))
        {
        }
    }
}
