<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAsociacionProspectoTerritorio.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.UCAsociacionProspectoTerritorio" %>
<script type="text/html" id="<%=Id %>">
    <div class="panel panel-default" data-id="id">
        <div class="panel-heading">
            <h4>
                Asociar Territorio
            </h4>
        </div>
        <div class="panel-body">
            <form id="frmDetallesAsociarTerritorio" class="form-inline">
                <div class="form-group">
                    <label for="selDetallesAsociarTerritorio_Id_Ter">
                        Territorio
                    </label>
                    <select class="selectpicker form-control" data-width="auto" id="selDetallesAsociarTerritorio_Id_Ter">
                                                                
                    </select>
                </div>
                <div class="form-group">
                    <label>
                        VPO
                    </label>
                    <input type="text" id="txtDetallesAsociarTerritorio_Potencial" class="form-control" style="width: 50%;" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                </div>
                <button class="btn btn-primary" type="button" id="btnDetallesAsociarTerritorio_Asociar" onclick="btnDetallesAsociarTerritorio_Asociar$click(this)">Asociar Territorio</button>
            </form>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>
                Territorios Asociados
            </h4>
        </div>
        <div class="panel-body">
            <table id="tblDetallesTerritoriosAsociados" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>
                            Clave
                        </th>
                        <th>
                            Nombre de Territorio
                        </th>
                        <th>
                            VPO
                        </th>
                        <th style="text-align: center;">
                            Editar
                        </th>
                        <th style="text-align: center;">
                            Retirar
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
</script>