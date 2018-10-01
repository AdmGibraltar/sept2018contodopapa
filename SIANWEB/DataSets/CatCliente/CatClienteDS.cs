namespace SIANWEB.DataSets.CatCliente {

    using System.Linq;
    using System.Collections.Generic;
    
    public partial class CatClienteDS {
        public bool ExisteGarantiaEnTerritorio(string Id_CteDet, string Id_TG)
        {
            var garantias = (from dr in this.CatClienteDetGarantia.AsEnumerable()
                             where dr["Id_TG"].ToString().CompareTo(Id_TG) == 0
                             && dr["Id_CteDet"].ToString().CompareTo(Id_CteDet) == 0
                             select 1).ToList();
            return garantias.Count > 0;
        }

        public List<string> ObtenerGarantiasDeTerritorio(string Id_CteDet)
        {
            var garantias = (from dr in this.CatClienteDetGarantia.AsEnumerable()
                             where dr["Id_CteDet"].ToString().CompareTo(Id_CteDet) == 0
                             select dr["Id_TG"].ToString()).ToList();
            return garantias;
        }
    }
}
