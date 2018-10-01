using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaModelo_CC.CuentasCoorporativas
{
    public partial class CatCNac_Estructura
    {
        public string NombreMatriz
        {
            get
            {
                return this.CatCNac_Matriz.Nombre;
            }
        }

        public string AcysNombre
        {
            get
            {
                if (CatCNac_ACYS != null)
                    return this.CatCNac_ACYS.Nombre;
                else
                    return "";
            }
        }

        public string SolicitudEstatus
        {
            get
            {
                var sol = this.CatCNac_Solicitudes.Where(x => x.Estatus < 3 || x.Estatus == 6).OrderByDescending(x => x.Estatus).FirstOrDefault();

                if (sol != null && sol.Estatus < 3)
                    return sol.CatCNac_EstatusSolicitudes.Nombre;
                else
                    return "";
            }
        }

        public Int32 IdEstatus
        {
            get
            {
                var sol = this.CatCNac_Solicitudes.Where(x => x.Estatus < 3 || x.Estatus == 6).OrderByDescending(x => x.Estatus).FirstOrDefault();
               
                if (sol != null && sol.Estatus<3)
                    return sol.CatCNac_EstatusSolicitudes.Id;
                else
                    return 0;
            }
        }


        public string ClienteVinculado
        {
            get
            {
                if (this.Id_Cte != null && this.IdEstatus==2)
                    return Id_Cte.ToString() + " - " + this.CatCliente.Cte_NomComercial;
                else
                    return "";
            }
        }
    }


    public partial class CatClienteDet
    {
        public string Ter_Nombre
        {
            get
            {
                 return this.CatTerritorio.Ter_Nombre;
                //return ""; 
            }

        }
    }


    public partial class CatCNac_Solicitudes
    {
        public string AcysNombre
        {
            get
            {
                if (this.CatCNac_Estructura!=null && this.CatCNac_Estructura.CatCNac_ACYS != null)
                    return this.CatCNac_Estructura.CatCNac_ACYS.Nombre;
                else
                    return "";
            }
        }


        public string TerrNombre
        {
            get
            {
                if (this.CatTerritorio != null)
                    return this.CatTerritorio.Ter_Nombre;
                else
                    return "";
            }
        }

        public string SolicitudEstatus
        {
            get
            {
                if (this.CatCNac_EstatusSolicitudes != null)
                    return this.CatCNac_EstatusSolicitudes.Nombre;
                else
                    return "";

                
            }
        }


        public string ClienteNombre
        {
            get
            {
                if (this.CatCliente != null)
                    return this.CatCliente.Cte_NomCorto;
                else
                    return "";


              
            }
        }

        public string EstructuraNombre
        {
            get
            {
                if (this.CatCNac_Estructura != null)
                    return this.CatCNac_Estructura.NombreNodo;
                else
                    return "";
            }
        }


        public string ACYSNombre
        {
            get
            {
                if (this.CatCNac_Estructura.CatCNac_ACYS != null)
                    return this.CatCNac_Estructura.CatCNac_ACYS.Nombre;
                else
                    return "";
            }
        }



    }


}
