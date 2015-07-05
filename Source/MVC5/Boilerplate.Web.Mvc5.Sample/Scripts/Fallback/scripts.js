// Performs tests to see if a script was loaded successfully, if the script failed to load, appends a new script tag 
// pointing to the local copy of the script and then waits for it to load before performing the next check.
(function (fallbacks) {

    var check = function (fallbacks, i) {
        if (i < fallbacks.length) {
            var fallback = fallbacks[i];
            if (fallback.test()) {
                check(fallbacks, i + 1);
            }
            else {
                var script = document.createElement("script");
                script.onload = function () {
                    check(fallbacks, i + 1);
                }
                script.src = fallback.path;
                document.getElementsByTagName("body")[0].appendChild(script);
            }
        }
    }
    check(fallbacks, 0);

})([
    { test: function () { return window.Modernizr; }, path: "/bundles/modernizr" },
    { test: function () { return window.jQuery; }, path: "/bundles/jquery" },
    { test: function () { return $.validator; }, path: "/bundles/jqueryval" },
    { test: function () { return $.validator.unobtrusive; }, path: "/bundles/jqueryvalunobtrusive" },
    { test: function () { return $.fn.modal; }, path: "/bundles/bootstrap" }
]);