// Performs tests to see if a stylesheet was loaded successfully, if the stylesheet failed to load, appends a new 
// link tag pointing to the local copy of the stylesheet before performing the next check.
(function (document) {
    "use strict";

    var fallbacks = [
        {
            // metaName - The name of the meta tag that the test is performed on. The meta tag must have a class from the
            //            relevant stylesheet on it so it is styled and a test can be performed against it. E.g. for 
            //            font awesome the <meta name="x-font-awesome-stylesheet-fallback-test" class="fa"> meta tag is
            //            added. The 'fa' class causes the font awesome style to be applied to it.
            metaName: "x-font-awesome-stylesheet-fallback-test",
            // test - The test to perform against the meta tag. Checks to see if the Font awesome styles loaded 
            //        successfully by checking that the font-family of the meta tag is 'FontAwesome'.
            test: function (meta) { return meta.style.fontFamily === "FontAwesome"; },
            // href - The URL to the fallback stylesheet.
            href: "/Content/fa"
        }
    ];

    var metas = document.getElementsByTagName("meta");

    for (var i = 0; i < fallbacks.length; ++i) {
        var fallback = fallbacks[i];

        for (var j = 0; j < metas.length; ++j) {
            var meta = metas[j];
            if (meta.getAttribute("name") === fallback.metaName) {
                if (!fallback.test(meta)) {
                    var link = document.createElement("link");
                    link.href = fallback.href;
                    link.rel = "stylesheet";
                    document.getElementsByTagName("head")[0].appendChild(link);
                }
                break;
            }
        }

    }
    
})(document);