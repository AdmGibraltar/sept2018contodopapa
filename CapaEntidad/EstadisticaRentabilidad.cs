using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EstadisticaRentabilidad
    {
            int _Anio;
            int _Mes;
            int _Id_Emp;
            int _Id_Cd_Ver;
            int _Id_Cte;
            int _Id_Ter;
            double _CtaCobrarPorc;
            double _InvDiasCant;
            double _InvConsigDiasCant;
            double _UtilidadRemanentePorc;
            double _CtaCobrar;
            double _InvDias;
            double _InvComodatoOtrosProdCant;
            double _InvComodatoOtrosProd;
            double _InvConsigDias;
            double _FinanProv;
            double _InvActivosNetosOPN;
            double _InvTotalActivos;
            double _InvActivosFijos;
            double _UtilidadRemanente;
            double _UafirActivos;
            double _CostoCapital;
            double _VentaNetaMon;
            double _CostoMaterialMon;
            double _FleteMon;
            double _ManoObraMon;
            double _UtilidadMon;
            double _CostoServEquipoMon;
            double _AmortizacionMon;
            double _ComisionRepMon;
            double _ContribucionGastosFijosOtrosMon;
            double _ContribucionGastosFijosPapelMon;
            double _UafirMensualMon;
            double _CargoUCSMon;
            double _FletesPagadosMon;
            double _OtrosGastosVariablesMon;
            double _GastosVariablesMon;
            double _UafirAnualMon;
            double _CostoCapitalMon;
            double _UtilidadRemanenteMon;
            double _UafirDespuesImpMon;
            double _ISRyPTU;
            double _ISRyPTUMon;
            double _GastosVariablesPorc;
            double _OtrosGastosVariablesPorc;
            double _FletesPagadosPorc;
            double _CargoUCSPorc;
            double _UafirMensualPorc;
            double _ContribucionGastosFijosPapelPorc;
            double _ContribucionGastosFijosOtrosPorc;
            double _AmortizacionPorc;
            double _CostoServEquipoPorc;
            double _ComisionRepPorc;
            double _UtilidadPorc;
            double _ManoObraPorc;
            double _FletePorc;
            double _FletePorc2;
            double _CostoMaterialPorc;
            double _UtilidadMarginalMon;
            double _UtilidadMarginalPorc;
            double _UtilidadBruta;

            public int Anio
            {
                get { return _Anio; }
                set { _Anio = value; }
            }
            public int Mes
            {
                get { return _Mes; }
                set { _Mes = value; }
            }
            public int Id_Emp
            {
                get { return _Id_Emp; }
                set { _Id_Emp = value; }
            }
            public int Id_Cd_Ver
            {
                get { return _Id_Cd_Ver; }
                set { _Id_Cd_Ver = value; }
            }
            public int Id_Cte
            {
                get { return _Id_Cte; }
                set { _Id_Cte = value; }
            }
            public int Id_Ter
            {
                get { return _Id_Ter; }
                set { _Id_Ter = value; }
            }
            public double CtaCobrarPorc
            {
                get { return _CtaCobrarPorc; }
                set { _CtaCobrarPorc = value; }
            }
            public double InvDiasCant
            {
                get { return _InvDiasCant; }
                set { _InvDiasCant = value; }
            }
            public double InvConsigDiasCant
            {
                get { return _InvConsigDiasCant; }
                set { _InvConsigDiasCant = value; }
            }
            public double UtilidadRemanentePorc
            {
                get { return _UtilidadRemanentePorc; }
                set { _UtilidadRemanentePorc = value; }
            }
            public double CtaCobrar
            {
                get { return _CtaCobrar; }
                set { _CtaCobrar = value; }
            }
            public double InvDias
            {
                get { return _InvDias; }
                set { _InvDias = value; }
            }
            public double InvComodatoOtrosProdCant
            {
                get { return _InvComodatoOtrosProdCant; }
                set { _InvComodatoOtrosProdCant = value; }
            }
            public double InvComodatoOtrosProd
            {
                get { return _InvComodatoOtrosProd; }
                set { _InvComodatoOtrosProd = value; }
            }
            public double InvConsigDias
            {
                get { return _InvConsigDias; }
                set { _InvConsigDias = value; }
            }
            public double FinanProv
            {
                get { return _FinanProv; }
                set { _FinanProv = value; }
            }
            public double InvActivosNetosOPN
            {
                get { return _InvActivosNetosOPN; }
                set { _InvActivosNetosOPN = value; }
            }
            public double InvTotalActivos
            {
                get { return _InvTotalActivos; }
                set { _InvTotalActivos = value; }
            }
            public double InvActivosFijos
            {
                get { return _InvActivosFijos; }
                set { _InvActivosFijos = value; }
            }
            public double UtilidadRemanente
            {
                get { return _UtilidadRemanente; }
                set { _UtilidadRemanente = value; }
            }
            public double UafirActivos
            {
                get { return _UafirActivos; }
                set { _UafirActivos = value; }
            }
            public double CostoCapital
            {
                get { return _CostoCapital; }
                set { _CostoCapital = value; }
            }
            public double VentaNetaMon
            {
                get { return _VentaNetaMon; }
                set { _VentaNetaMon = value; }
            }
            public double CostoMaterialMon
            {
                get { return _CostoMaterialMon; }
                set { _CostoMaterialMon = value; }
            }
            public double FleteMon
            {
                get { return _FleteMon; }
                set { _FleteMon = value; }
            }
            public double ManoObraMon
            {
                get { return _ManoObraMon; }
                set { _ManoObraMon = value; }
            }
            public double UtilidadMon
            {
                get { return _UtilidadMon; }
                set { _UtilidadMon = value; }
            }
            public double CostoServEquipoMon
            {
                get { return _CostoServEquipoMon; }
                set { _CostoServEquipoMon = value; }
            }
            public double AmortizacionMon
            {
                get { return _AmortizacionMon; }
                set { _AmortizacionMon = value; }
            }
            public double ComisionRepMon
            {
                get { return _ComisionRepMon; }
                set { _ComisionRepMon = value; }
            }
            public double ContribucionGastosFijosOtrosMon
            {
                get { return _ContribucionGastosFijosOtrosMon; }
                set { _ContribucionGastosFijosOtrosMon = value; }
            }
            public double ContribucionGastosFijosPapelMon
            {
                get { return _ContribucionGastosFijosPapelMon; }
                set { _ContribucionGastosFijosPapelMon = value; }
            }
            public double UafirMensualMon
            {
                get { return _UafirMensualMon; }
                set { _UafirMensualMon = value; }
            }
            public double CargoUCSMon
            {
                get { return _CargoUCSMon; }
                set { _CargoUCSMon = value; }
            }
            public double FletesPagadosMon
            {
                get { return _FletesPagadosMon; }
                set { _FletesPagadosMon = value; }
            }
            public double OtrosGastosVariablesMon
            {
                get { return _OtrosGastosVariablesMon; }
                set { _OtrosGastosVariablesMon = value; }
            }
            public double GastosVariablesMon
            {
                get { return _GastosVariablesMon; }
                set { _GastosVariablesMon = value; }
            }
            public double UafirAnualMon
            {
                get { return _UafirAnualMon; }
                set { _UafirAnualMon = value; }
            }
            public double CostoCapitalMon
            {
                get { return _CostoCapitalMon; }
                set { _CostoCapitalMon = value; }
            }
            public double UtilidadRemanenteMon
            {
                get { return _UtilidadRemanenteMon; }
                set { _UtilidadRemanenteMon = value; }
            }
            public double UafirDespuesImpMon
            {
                get { return _UafirDespuesImpMon; }
                set { _UafirDespuesImpMon = value; }
            }
            public double ISRyPTU
            {
                get { return _ISRyPTU; }
                set { _ISRyPTU = value; }
            }
            public double ISRyPTUMon
            {
                get { return _ISRyPTUMon; }
                set { _ISRyPTUMon = value; }
            }
            public double GastosVariablesPorc
            {
                get { return _GastosVariablesPorc; }
                set { _GastosVariablesPorc = value; }
            }
            public double OtrosGastosVariablesPorc
            {
                get { return _OtrosGastosVariablesPorc; }
                set { _OtrosGastosVariablesPorc = value; }
            }
            public double FletesPagadosPorc
            {
                get { return _FletesPagadosPorc; }
                set { _FletesPagadosPorc = value; }
            }
            public double CargoUCSPorc
            {
                get { return _CargoUCSPorc; }
                set { _CargoUCSPorc = value; }
            }
            public double UafirMensualPorc
            {
                get { return _UafirMensualPorc; }
                set { _UafirMensualPorc = value; }
            }
            public double ContribucionGastosFijosPapelPorc
            {
                get { return _ContribucionGastosFijosPapelPorc; }
                set { _ContribucionGastosFijosPapelPorc = value; }
            }
            public double ContribucionGastosFijosOtrosPorc
            {
                get { return _ContribucionGastosFijosOtrosPorc; }
                set { _ContribucionGastosFijosOtrosPorc = value; }
            }
            public double AmortizacionPorc
            {
                get { return _AmortizacionPorc; }
                set { _AmortizacionPorc = value; }
            }
            public double CostoServEquipoPorc
            {
                get { return _CostoServEquipoPorc; }
                set { _CostoServEquipoPorc = value; }
            }
            public double ComisionRepPorc
            {
                get { return _ComisionRepPorc; }
                set { _ComisionRepPorc = value; }
            }
            public double UtilidadPorc
            {
                get { return _UtilidadPorc; }
                set { _UtilidadPorc = value; }
            }
            public double ManoObraPorc
            {
                get { return _ManoObraPorc; }
                set { _ManoObraPorc = value; }
            }
            public double FletePorc
            {
                get { return _FletePorc; }
                set { _FletePorc = value; }
            }
            public double FletePorc2
            {
                get { return _FletePorc2; }
                set { _FletePorc2 = value; }
            }
            public double CostoMaterialPorc
            {
                get { return _CostoMaterialPorc; }
                set { _CostoMaterialPorc = value; }
            }
            public double UtilidadMarginalMon
            {
                get { return _UtilidadMarginalMon; }
                set { _UtilidadMarginalMon = value; }
            }
            public double UtilidadMarginalPorc
            {
                get { return _UtilidadMarginalPorc; }
                set { _UtilidadMarginalPorc = value; }
            }
            public double UtilidadBruta
            {
                get { return _UtilidadBruta; }
                set { _UtilidadBruta = value; }
            }

    }
}
