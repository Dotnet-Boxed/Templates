export class Calculator {
    private throwOnDivideByZero: boolean;

    public add(a: number, b: number) {
        return a + b;
    }

    public subtract(a: number, b: number) {
        return a - b;
    }

    public multiply(a: number, b: number) {
        return a * b;
    }

    public divide(a: number, b: number) {
        this.checkDivideByZero(b);
        return a / b;
    }

    private checkDivideByZero(x: number) {
        if ((x === 0) && this.throwOnDivideByZero) {
            throw new Error("Divide by Zero.");
        }
    }
}
