namespace Task1.Models
{
    public class Calculator
    {
        public double Add(double x, double y) => x + y;
        public double Sub(double x, double y) => x - y;
        public double Mul(double x, double y) => x * y;
        public DivideResult Div(double x, double y)
        {
            if (y == 0)
            {
                return DivideResult.ErrorResult();
            }
            else
            {
                return DivideResult.SuccessResult(x / y);
            }
        }
    }

    public class DivideResult
    {
        public string Error { get; set; }
        public bool IsValid { get; set; }
        public double Result { get; set; }
        public DivideResult(double result, bool isValid, string error)
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
            return new DivideResult(0, false, "Cannot be divided by zero");
        }
    }
}
