export class Calculator {
    throwOnDivideByZero: boolean;

    add(a: number, b: number) {
        return a + b;
    }

    subtract(a: number, b: number) {
        return a - b;
    }

    multiply(a: number, b: number) {
        return a * b;
    }

    divide(a: number, b: number) {
        this.checkDivideByZero(b);
        return a / b;
    }

    private checkDivideByZero(x: number) {
        if ((x === 0) && this.throwOnDivideByZero) {
            throw new Error("Divide by Zero.");
        }
    }
}