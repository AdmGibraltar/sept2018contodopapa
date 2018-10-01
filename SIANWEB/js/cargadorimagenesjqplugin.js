/* File Created: November 28, 2016 */
///<reference path="http://code.jquery.com/jquery-2.1.4.min.js" />
///<reference path="~/js/jquery-ui-1.11.4.custom/jquery-ui.js" />

(function ($) {
    $.widget('sianweb.cargadorimagenes', {
        options: {

        },
        _create: function () {
            $(this.element).loadTemplate('#tplCargadorImagenes');
        }
    });
})(jQuery);