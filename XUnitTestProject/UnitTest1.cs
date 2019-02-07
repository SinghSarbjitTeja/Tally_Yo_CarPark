using Carpark_Project.Controllers;
using Carpark_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        Times Times = new Times() { Start = "2019-02-08, 7:57:00 am", End = "2019-02-08, 10:59:00 pm" };
        CalculatePriceController controller = new CalculatePriceController();

        [Fact]
        public void Test_FinalPriceCarpark_Returns_NotNull()
        {
            //Act
            JsonResult res = controller.FinalPriceCarpark(Times) as JsonResult;

            //Assert         
            Assert.NotNull(res);

            try
            {
                Assert.Throws<InvalidOperationException>(() => res);
            }
            catch (AssertActualExpectedException exception)
            {
                Assert.Equal("(No exception was thrown)", exception.Actual);
            }

        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_NoException()
        {
            //Act
            JsonResult res = controller.FinalPriceCarpark(Times) as JsonResult;

            //Assert         
            try
            {
                Assert.Throws<InvalidOperationException>(() => res);
            }
            catch (AssertActualExpectedException exception)
            {
                Assert.Equal("(No exception was thrown)", exception.Actual);
            }

        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_JsonType()
        {
            //Act
            var res = controller.FinalPriceCarpark(Times);

            //Assert         
            Assert.IsType<JsonResult>(res);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_Success()
        {
            //Act
            JsonResult res = controller.FinalPriceCarpark(Times) as JsonResult;

            //Assert         
            Assert.Contains("success", res.Value.ToString());
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_EarlyBird_Results()
        {
            var TimesProvided = new Times() { Start = "2019-02-20, 7:57:00 am", End = "2019-02-20, 10:59:00 pm" };
            var expected = new PriceDTO { Name = "Early Bird", Price = "$13" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(TimesProvided) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_NightRate_Results()
        {
            var TimesProvided = new Times() { Start = "2019-02-20, 7:20:00 pm", End = "2019-02-21, 5:59:00 am" };
            var expected = new PriceDTO
            {
                Name = "Night Rate",
                Price = "$6.50"
            };
          
            //Act
            JsonResult res = controller.FinalPriceCarpark(TimesProvided) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_WeekendRate_Results()
        {
            var TimesProvided = new Times() { Start = "2019-02-16, 1:20:00 am", End = "2019-02-17, 5:59:00 am" };
            var expected = new PriceDTO()
            {
                Name = "Weekend Rate",
                Price = "$10"
            };

            //Act
            JsonResult res = controller.FinalPriceCarpark(TimesProvided) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_StandardRate_Less_Than_Hour()
        {
            var TimesProvided = new Times() { Start = "2019-02-15, 2:20:00 pm", End = "2019-02-15, 2:59:00 pm" };
            var expected = new PriceDTO() { Name = "Standard Rate", Price = "$5" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(TimesProvided) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_StandardRate_Less_Than_Two_Hours()
        {
            var twoHours = new Times() { Start = "2019-02-15, 2:20:00 pm", End = "2019-02-15, 4:10:00 pm" };
            var expected = new PriceDTO() { Name = "Standard Rate", Price = "$10" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(twoHours) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_StandardRate_Less_Than_Three_Hours()
        {
            var threeHours = new Times() { Start = "2019-02-15, 2:20:00 pm", End = "2019-02-15, 4:50:00 pm" };
            var expected = new PriceDTO() { Name = "Standard Rate", Price = "$15" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(threeHours) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }

        [Fact]
        public void Test_FinalPriceCarpark_Returns_StandardRate_More_Than_Three_Hours_Same_Day()
        {
            var moreThanThree = new Times() { Start = "2019-02-15, 2:20:00 pm", End = "2019-02-15, 6:59:00 pm" };
            var expected = new PriceDTO() { Name = "Standard Rate", Price = "$20" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(moreThanThree) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }
        [Fact]
        public void Test_FinalPriceCarpark_Returns_StandardRate_More_Than_Three_Hours_Different_Day()
        {
            var moreThanThreeD = new Times() { Start = "2019-02-15, 1:20:00 am", End = "2019-02-16, 5:59:00 am" };
            var expected = new PriceDTO() { Name = "Standard Rate", Price = "$40" };

            //Act
            JsonResult res = controller.FinalPriceCarpark(moreThanThreeD) as JsonResult;
            var finalResult = JsonConvert.SerializeObject(res);

            //Assert         
            Assert.Contains(expected.Name, finalResult);
            Assert.Contains(expected.Price, finalResult);
        }
    }
}

