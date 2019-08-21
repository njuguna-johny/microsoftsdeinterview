using EmployeeLib;
using EmployeeLib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace Employee.UnitTests
{
    [TestClass]
    public class EmployeesTest
    {

        [TestMethod]
        public void TestForSalaryValidIntegers()
        {
            //Passing Test
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
               //Uncomment the line below to fail the text
               // .AppendLine("Employee4,Employee2,5KLI0")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employeeData.CheckIfSalariesAreValid);
        }

        [TestMethod]
        public void TestForEmployeesWithMoreThanOneManager()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
                //Uncomment the line below to fail the text
                //.AppendLine("Employee4,Employee2,500")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employeeData.ValidateEmployeesWithMoreThanOneManager);
        }
        [TestMethod]
        public void TestForValidatingOneCEO()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
               //Uncomment the line below to fail the text
              // .AppendLine("Employee1,,1000")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employeeData.ValidateOneCEO);
        }

        [TestMethod]
        public void ValidateNonEmployeeManagers()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 //Uncomment the line below to fail the text
                 //.AppendLine(",,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employeeData.validateNonEmployeeManagers);
        }

        [TestMethod]
        public void GetSalaryBudget()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               .AppendLine("Employee4,Employee2,500")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.AreEqual(2800,employeeData.ManagerSalaryBudget("Employee1"));
        }

        [TestMethod]
        public void ValidateCircularRefence()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Employee1,,1000")
                 .AppendLine("Employee2,Employee1,800")
               //Uncomment the line below to fail the text
               .AppendLine("Employee1,Employee2,800")
               .AppendLine("Employee4,Employee2,500")
                .AppendLine("Employee6,Employee2,500")
               .AppendLine("Employee3,Employee1,500")
                .AppendLine("Employee5,Employee1,500");
            var employeeData = new Employees(stringBuilder.ToString());
            Assert.IsTrue(employeeData.validateCircularReference);
        }
    }  
    
}
