// Its good practice to wrap your JavaScript in an immediately-invoked function expression (IIFE) 
// (See https://en.wikipedia.org/wiki/Immediately-invoked_function_expression) to stop your functions being added to 
// the global scope (See https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions).
// We can't assume that '$' is jQuery or that the 'window', 'document' or 'undefined' variables have not been 
// reassigned, so we pass them as arguments and ensure their values. Note that we don't add an undefined parameter.
(function ($, window, document) {
    // Enable strict mode for JavaScript 
    // (See https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Strict_mode).
    "use strict";

    // Add your code here.

    // The DOM ready handler (See https://learn.jquery.com/using-jquery-core/document-ready/).
    $(function () {
        // Add your code here if you want to access the DOM.
    });

})(jQuery, window, document, undefined);