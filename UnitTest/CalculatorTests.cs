using IO.Swagger.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTest
{
    public class CalculatorTests
    {
        [Theory]
        [ClassData(typeof(CalcDataGenerator))]
        public void CalculatorTest(int firstValue, int secondValue, string arithmeticOperation, decimal? result)
        {
            var calculator = new CalculatorApiController();
            var actResult = calculator.CalculatorParam1Param2Post(firstValue, secondValue, arithmeticOperation) as ObjectResult;
            
            Assert.Equal(result, actResult?.Value);
        }

        [Theory]
        [InlineData(0, 0, "/", "can't divide By Zero")]
        public void DivideByZero(int firstValue, int secondValue, string arithmeticOperation, string result) {
            var calculator = new CalculatorApiController();

            var actErrorResult = calculator.CalculatorParam1Param2Post(firstValue, secondValue, arithmeticOperation) as ObjectResult;
            Assert.Equal(result, actErrorResult?.Value);
        }

        [Theory]
        [InlineData(4, 5, " ", "invalid arithmetic operation")]
        public void ArithmeticOperationError(int firstValue, int secondValue, string arithmeticOperation, string result)
        {
            var calculator = new CalculatorApiController();

            var actErrorResult = calculator.CalculatorParam1Param2Post(firstValue, secondValue, arithmeticOperation) as ObjectResult;
            Assert.Equal(result, actErrorResult?.Value);
        }

        public class CalcDataGenerator : TheoryData<int, int, string, decimal?>
        {
            public CalcDataGenerator()
            {
                //add combinations to test CalculatorParam1Param2Post
                Add(5, 3, "+", 8);
                Add(-1, 3, "+", 2);
                Add(0, 0, "+", 0);
                Add(-1, 3, "-", -4);
                Add(0, 0, "-", 0);
                Add(15, 3, "/", 5);
                Add(10, 4, "/", 2.50m);
                Add(1, 3, "*", 3);
                Add(0, 0, "*", 0);
            }
        }

    }
}