(function ($) {
    $.fn.numeraljs = function (options) {
        var $withNumeralDataAttribute = this.find('[data-numeraljs]');
        return $.each($withNumeralDataAttribute, function (index, element) {
            if ($(element).is('div') || $(element).is('td')) {
                var numeraljsDataAttributeStringValue = $(element).data('numeraljs');
                var parsedObject = $.parseJSON('{' + numeraljsDataAttributeStringValue + '}');
                var elementValue = $(element).text();
                if (typeof (parsedObject.format) != undefined && typeof (parsedObject.format) != 'undefined') {
                    var formattedValue = numeral(elementValue).format(parsedObject.format);
                    $(element).text(formattedValue);
                }
            }
        });
    };
})(jQuery);