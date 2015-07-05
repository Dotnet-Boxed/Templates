// Looks for the <meta name="x-font-awesome-stylesheet-fallback-test" class="fa"> meta tag and checks to see if the 
// Font awesome styles loaded successfully by checking that the font-family of the meta tag is 'FontAwesome'. If the 
// style failed to load, append another link tag pointing to the local copy of font awesome.
(function () {
    var metas = document.getElementsByTagName('meta');
    for (i = 0; i < metas.length; ++i) {
        var meta = metas[i];
        if (meta.getAttribute("name") == "x-font-awesome-stylesheet-fallback-test") {
            if (meta.style.fontFamily !== "FontAwesome") {
                document.write('<link href="/css/font-awesome.css" rel="stylesheet"/>');
            }
            break;
        }
    }
})();