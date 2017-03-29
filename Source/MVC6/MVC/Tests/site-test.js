// Mocha (https://mochajs.org/) JavaScript tests for the calculator in site.js.
describe("Calculator", function () {
    var calculator;

    before(function () {
        // Runs before all tests in this block.
    });

    after(function () {
        // Runs after all tests in this block.
    });

    beforeEach(function () {
        // Runs before each test in this block.
        calculator = new framework.Calculator();
    });

    afterEach(function () {
        // Runs after each test in this block.
    });

    describe("Add", function () {
        it("Should return 2 when you pass it 1 and 1.", function () {
            assert.strictEqual(calculator.add(1, 1), 2, "2 + 2 failed. This is an optional custom error message.");
        });
    });

    describe("Subtract", function () {
        it("Should return 0 when you pass it 1 and 1.", function () {
            assert.strictEqual(calculator.subtract(1, 1), 0);
        });
    });

    describe("Multiply", function () {
        // it.skip - Skips a specific test case. You can also add this to a 'describe' to skip several test cases.
        it.skip("Should return 4 when you pass it 2 and 2.", function () {
            assert.strictEqual(calculator.multiply(2, 2), 4);
        });
    });

    describe("Divide", function () {
        // it.only - Only runs a specific test case. You can also add this to a 'describe' to run only those test cases.
        // it.only("Should return 2 when you pass it 4 and 2.", function () {
        //     assert.strictEqual(calculator.multiply(4, 2), 2);
        // });
    });

});

// An example of how to test asynchronous code.
describe("setTimeout", function () {
    // A 'done' function delegate must be called to tell Mocha when the test has finished.
    it("should complete after one second.", function (done) {
        setTimeout(function () {
            done();
        },
        1000);
    })
})