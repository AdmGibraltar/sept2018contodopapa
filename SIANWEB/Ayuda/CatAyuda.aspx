<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatAyuda.aspx.cs" Inherits="SIANWEB.CatAyuda.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style type="text/css">
    body
    {
        color:#000000;
        background-color:#FFFFFF;
        background-image:url('Background Image');
        background-repeat:no-repeat;
        }
    a { color:#0000FF;}
    a:visited { color:#800080;}
    a:hover { color:#008000; }
    a:active { color:#FF0000; }
    
    #btnGrabar
    {
        width: 64px;
        margin-left: 0px;
    }
    #btnCancelar
    {
        width: 83px;
    }
    
</style>
</head>
    <form id="form1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientLoad(editor, args) {
            $telerik.$(editor.get_contentArea()).on("drop", function (e) {
                var origEvent = e.originalEvent;
                if (!origEvent || !origEvent.dataTransfer || !origEvent.dataTransfer.items) {
                    return;
                }
                var item = origEvent.dataTransfer.items[0];
                if (!item || item.type.indexOf("image") == -1) {
                    return;
                }
                var blob = item.getAsFile();
                var URLObj = window.URL || window.webkitURL;
                var source = URLObj.createObjectURL(blob);
                var image = new Image();
                image.src = source;
                $telerik.$(image).load(function (ev) {
                    var reader = new FileReader();
                    reader.onload = function (event) {
                        if (image.src != event.target.result) {
                            image.src = event.target.result;
                            var selection = editor.getSelection();
                            var range = editor.getDomRange();
                            if (range) {
                                selection.insertNode(image, range);
                            } else {
                                $telerik.$(editor.get_contentArea()).prepend(image);
                            }
                        }
                    };
                    reader.readAsDataURL(blob);
                });
            });
        }

        //disable right mouse click Script 
        document.onmousedown = "if (event.button==2) return false";
        document.oncontextmenu = new Function("return false");

    </script>
    </telerik:RadCodeBlock>
    <div runat="server" id="divPrincipal">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />

    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="900px">
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" width="350px">&nbsp;Modulo: <b><%=OpcionMenu%></b> (<i><%=IdOpcionMenu%></i>)&nbsp;</td>
            <td style="text-align: left" >&nbsp;]<i><%=path%></i>[&nbsp;
                <asp:HiddenField ID="HF_IdPag" runat="server" />
                <asp:HiddenField ID="HF_DescPag" runat="server" />
                <asp:HiddenField ID="HF_Path" runat="server" />
                <asp:HiddenField ID="HF_New" runat="server" />
            </td>
            <td style="font-weight: bold" align="right">&nbsp;
                <asp:Button ID="btnGrabar" runat="server" Text="Grabar"  OnClick="btnGrabar_Click" />&nbsp;&nbsp;
                <button id="btnCancelar" value="Cancelar" runat="server" onclick="javascript:window.close();">Cancelar</button>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <telerik:RadEditor runat="server" ID="RadEditor1"  Width="980px" 
                    Height="550px" EditModes="Design, Preview" AllowScripts="True" 
                    EnableResize="False" OnClientLoad="OnClientLoad"
                    >
                    <tools>
                        <telerik:EditorToolGroup Tag="MainToolbar">
                            <telerik:EditorTool Name="Print" ShortCut="CTRL+P" />
                            <telerik:EditorTool Name="FindAndReplace" ShortCut="CTRL+F" />
                            <telerik:EditorTool Name="SelectAll" ShortCut="CTRL+A" />
                            <telerik:EditorTool Name="Cut" />
                            <telerik:EditorTool Name="Copy" ShortCut="CTRL+C" />
                            <telerik:EditorTool Name="Paste" ShortCut="CTRL+V" />
                            <telerik:EditorToolStrip Name="PasteStrip">
                            </telerik:EditorToolStrip>
                            <telerik:EditorSeparator />
                            <telerik:EditorSplitButton Name="Undo">
                            </telerik:EditorSplitButton>
                            <telerik:EditorSplitButton Name="Redo">
                            </telerik:EditorSplitButton>
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup Tag="InsertToolbar">
                            <telerik:EditorTool Name="ImageManager" ShortCut="CTRL+G" />
                            <telerik:EditorTool Name="DocumentManager" />
                            <telerik:EditorTool Name="MediaManager" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Superscript" />
                            <telerik:EditorTool Name="Subscript" />
                            <telerik:EditorTool Name="InsertParagraph" />
                            <telerik:EditorTool Name="InsertGroupbox" />
                            <telerik:EditorTool Name="InsertHorizontalRule" />
                            <telerik:EditorTool Name="InsertDate" />
                            <telerik:EditorTool Name="InsertTime" />
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="FormatCodeBlock" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorDropDown Name="FormatBlock">
                            </telerik:EditorDropDown>
                            <telerik:EditorDropDown Name="FontName">
                            </telerik:EditorDropDown>
                            <telerik:EditorDropDown Name="RealFontSize">
                            </telerik:EditorDropDown>
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Bold" ShortCut="CTRL+B" />
                            <telerik:EditorTool Name="Italic" ShortCut="CTRL+I" />
                            <telerik:EditorTool Name="Underline" ShortCut="CTRL+U" />
                            <telerik:EditorTool Name="StrikeThrough" />
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="JustifyLeft" />
                            <telerik:EditorTool Name="JustifyCenter" />
                            <telerik:EditorTool Name="JustifyRight" />
                            <telerik:EditorTool Name="JustifyFull" />
                            <telerik:EditorTool Name="JustifyNone" />
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="Indent" />
                            <telerik:EditorTool Name="Outdent" />
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="InsertOrderedList" />
                            <telerik:EditorTool Name="InsertUnorderedList" />
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="ToggleTableBorder" />
                            <telerik:EditorTool Name="XhtmlValidator" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorSplitButton Name="ForeColor">
                            </telerik:EditorSplitButton>
                            <telerik:EditorSplitButton Name="BackColor">
                            </telerik:EditorSplitButton>
                            <telerik:EditorToolStrip Name="FormatStripper">
                            </telerik:EditorToolStrip>
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup Tag="DropdownToolbar">
                            <telerik:EditorSplitButton Name="InsertSymbol">
                            </telerik:EditorSplitButton>
                            <telerik:EditorToolStrip Name="InsertTable">
                            </telerik:EditorToolStrip>
                            <telerik:EditorDropDown Name="InsertCustomLink">
                            </telerik:EditorDropDown>
                            <telerik:EditorSeparator />
                            <telerik:EditorTool Name="ConvertToLower" />
                            <telerik:EditorTool Name="ConvertToUpper" />
                            <telerik:EditorSeparator />
                            <telerik:EditorDropDown Name="Zoom">
                            </telerik:EditorDropDown>
                            <telerik:EditorTool Name="AboutDialog" />
                        </telerik:EditorToolGroup>
                    </tools>
                
                    
                    <Content>
                        <span style="font-family: Verdana; font-size:12px">​</span>
                    </Content>
                    
            </telerik:RadEditor>

            </td>
        </tr>
    </table>
 <!-- 
 <ImageManager ViewPaths="~/Ayuda/capturas" UploadPaths="~/Ayuda/capturas" />
<tools>
    <telerik:EditorToolGroup Tag="MainToolbar">
        <telerik:EditorTool Name="FindAndReplace" />
        <telerik:EditorSeparator />
        <telerik:EditorSplitButton Name="Undo">
        </telerik:EditorSplitButton>
        <telerik:EditorSplitButton Name="Redo">
        </telerik:EditorSplitButton>
        <telerik:EditorSeparator />
        <telerik:EditorTool Name="Cut" />
        <telerik:EditorTool Name="Copy" />
        <telerik:EditorTool Name="Paste" ShortCut="CTRL+V" />
    </telerik:EditorToolGroup>
    <telerik:EditorToolGroup Tag="Formatting">
        <telerik:EditorTool Name="Bold" />
        <telerik:EditorTool Name="Italic" />
        <telerik:EditorTool Name="Underline" />
        <telerik:EditorSeparator />
        <telerik:EditorSplitButton Name="ForeColor">
        </telerik:EditorSplitButton>
        <telerik:EditorSplitButton Name="BackColor">
        </telerik:EditorSplitButton>
        <telerik:EditorSeparator />
        <telerik:EditorDropDown Name="FontName">
        </telerik:EditorDropDown>
        <telerik:EditorDropDown Name="RealFontSize">
        </telerik:EditorDropDown>
    </telerik:EditorToolGroup>
</tools>
 
 -->
    </div>
    </form>
</html>

