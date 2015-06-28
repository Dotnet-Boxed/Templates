(function ($) {
    var $span = $('<span class="fa" style="display:none"></span>').appendTo('body');
    if ($span.css('fontFamily') !== 'FontAwesome') {
        $('head').append('<link href="/Content/fa" rel="stylesheet">');
    }
    $span.remove();
})(jQuery);