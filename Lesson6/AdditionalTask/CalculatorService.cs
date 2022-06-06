using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdditionalTask
{
    public class CalculatorService
    {
        public double Add(double x, double y) => x + y;
        public double Sub(double x, double y) => x - y;
        public double Mul(double x, double y) => x * y;
        public DivideResult Div(double x, double y)
        {

            return y != 0 ? DivideResult.SuccessResult(x / y) : DivideResult.ErrorResult();
        }
    }

    public class DivideResult
    {
        public double Result { get; }
        public bool IsValid { get; }
        public string Error { get; }
        protected DivideResult(double result, bool isValid, string error)
        {
            if (isValid)
            {
                Result = result;
            }
            else
            {
                Error = error;
            }

            IsValid = isValid;
        }

        static public DivideResult SuccessResult(double result)
        {

            return new DivideResult(result, true, null);
        }

        static public DivideResult ErrorResult()
        {
            return new DivideResult(0, false, "Cannot divide by zero!");
        }
    }
}
