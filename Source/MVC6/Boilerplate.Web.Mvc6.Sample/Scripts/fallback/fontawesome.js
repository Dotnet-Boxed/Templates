(function ($) {
    var metaTag = $("meta[name=x-font-awesome-stylesheet-fallback-test]");
    if (metaTag.css("font-family") !== "FontAwesome") {
        document.write('<link rel="stylesheet" href="/css/font-awesome.css"/>');
    }
})(jQuery);