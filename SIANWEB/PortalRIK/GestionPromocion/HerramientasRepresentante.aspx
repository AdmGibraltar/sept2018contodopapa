<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true"
    CodeBehind="HerramientasRepresentante.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.HerramientasRepresentante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="<%=Page.ResolveUrl("~/css/docs.min.css")%>" />
    <style>
        .fixed
        {
            position: fixed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div>
        <div class="row">
            <div class="col-md-9">
            </div>
            <div class="col-md-3">
                <nav class="bs-docs-sidebar hidden-print hidden-xs hidden-sm">
                    <ul class="nav bs-docs-sidenav">
                        <li>
                            <a href="#collapseExample" class="dropdown-toggle" data-toggle="collapse" role="button" aria-controls="collapseExample" aria-expanded="false">UEN Industrial</a>
                            <ul id="collapseExample" class="collapse nav">
                                <li>
                                    <a href="#b">Sub caegoria 1</a>
                                </li>
                                <li>
                                    <a href="#c">Sub categoria 2</a>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <a href="#c">UEN Industrial 2</a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript">
        (function ($) {
            var $window = $(window);
            var $body = $(document.body);
            var $sideBar = $('.bs-docs-sidebar');
            var navHeight = $('.navbar').outerHeight(true) + 10;

            $body.scrollspy({
                target: '.bs-docs-sidebar',
                offset: navHeight
            });

            $window.on('resize', function () {
                $body.scrollspy('refresh');
                // We were resized. Check the position of the nav box
                $sideBar.affix('checkPosition');
            });

            $window.on('load', function () {
                $body.scrollspy('refresh');
                $sideBar.affix({
                    offset: {
                        top: function () {
                            var offsetTop = $sideBar.offset().top;
                            var sideBarMargin = parseInt($sideBar.children(0).css('margin-top'), 10);
                            var navOuterHeight = $('.navbar').height();

                            // We can cache the height of the header (hence the this.top=)
                            // This function will never be called again.
                            return (this.top = offsetTop - navOuterHeight - sideBarMargin);
                        },
                        bottom: function () {
                            // We can't cache the height of the footer, since it could change
                            // when the window is resized. This function will be called every
                            // time the window is scrolled or resized
                            return 0;
                        }
                    }
                });
                setTimeout(function () {
                    // Check the position of the nav box ASAP
                    $sideBar.affix('checkPosition');
                }, 10);
                setTimeout(function () {
                    // Check it again after a while (required for IE)
                    $sideBar.affix('checkPosition');
                }, 100);
            });
        })(jQuery);
    </script>
</asp:Content>
