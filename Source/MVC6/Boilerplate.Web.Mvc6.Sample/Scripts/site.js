// Its good practice to wrap your JavaScript in an immediately-invoked function expression (IIFE) 
// (See https://en.wikipedia.org/wiki/Immediately-invoked_function_expression) to stop your functions being added to 
// the global scope (See https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions).

// framework - The namespace under which all your code should live if you want it to be globally available. As an 
//             example a calculator object has been created under the framework namespace below. Note that due to the
//             '||' we are using when passing in the namespace at the bottom of this file, you can repeat the pattern
//             in as many files as you want. The calculator would be better off in a calculator.js file for example.
//             (See http://elijahmanor.com/angry-birds-of-javascript-red-bird-iife/)
//         $ - The jQuery object. We can't assume that '$' sign is the main jQuery object or being used for something
//             else. It is good practice to pass in the full 'jQuery' object and assign it to the dollar sign here.
//    window - The window object could have been changed during the application lifetime, to guard against this it is 
//             good practice to hold onto a copy.
//  document - Same as above.
// undefined - We have an undefined parameter but we do not pass in a value for it. This ensures that the undefined 
//             keyword does actually mean undefined and has not been reassigned (Yes, that is possible in JavaScript).
(function (framework, $, window, document, undefined) {
    // Enable strict mode for JavaScript (See https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Strict_mode).
    "use strict";

    // Add your code here.

    // The DOM ready handler (See https://learn.jquery.com/using-jquery-core/document-ready/).
    $(function () {
        // Add your code here if you want to access the DOM.
    });

    // This is an example of creating a Calculator object under the framework namespace. The Calculator is using the 
    // Revealing Prototype Pattern. You can use the Calculator using the 'new' keyword e.g. var c = new Calculator();
    // (See http://weblogs.asp.net/dwahlin/techniques-strategies-and-patterns-for-structuring-javascript-code-revealing-prototype-pattern)
    framework.Calculator = function (throwOnDivideByZero) {
        // Calculator constructor. You can pass parameters to the Calculator and/or create private variables like here.
        this.throwOnDivideByZero = throwOnDivideByZero === undefined ? true : throwOnDivideByZero;
    };
    framework.Calculator.prototype = (function () {

        // The 'self' variable ensures that the 'this' keyword references the Calculator and not the calling function.
        // (See https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/this)
        var self = this;

        function add(a, b) {
            return a + b;
        }

        function subtract(a, b) {
            return a - b;
        }

        function multiply(a, b) {
            return a * b;
        }

        function divide(a, b) {
            checkDivideByZero(b);
            return a / b;
        }

        // This private function is not exposed to those who want to use the Calculator.
        function checkDivideByZero(x) {
            if ((x === 0) && self.throwOnDivideByZero) {
                throw new Error("Divide by Zero.");
            }
        }

        return {
            add: add,
            subtract: subtract,
            multiply: multiply,
            divide: divide
        };
    }());

})(window.framework = window.framework || {}, window.jQuery, window, document);