using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using EmployeeLib.Model;
namespace EmployeeLib
{
    public class Employees 
    {
        private readonly string csvInput;
        public List<Employee> _employeeList;
        public bool CheckIfSalariesAreValid;
        public bool ValidateEmployeesWithMoreThanOneManager;
        public bool ValidateOneCEO;
        public bool validateNonEmployeeManagers;
        public bool validateCircularReference;
        //Employee Constructor accepts string csc 
        public Employees(string csv)
        {
            csvInput = csv;
            _employeeList = new List<Employee>();
            //Load from CSV to List
            LoadEmployees();
            //Check for valid salaries
            CheckIfSalariesAreValid = ValidateSalaries(_employeeList);
            //Checl for employees with more than one manager
            ValidateEmployeesWithMoreThanOneManager = ValidateEmployees(_employeeList);
            //Validate CEO
            ValidateOneCEO = ValidateCEO(_employeeList);
            //Validate non employee managers
            validateNonEmployeeManagers = ValidateNonEmployeeManagers(_employeeList);
            //Check circular reference
            validateCircularReference = ValidateCircularReference(_employeeList);
        }
        
        //Sets employees list
        private void LoadEmployees()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
            var csvMapper = new MappingHelper();
            CsvParser<Employee> csvParser = new CsvParser<Employee>(csvParserOptions, csvMapper);
            _employeeList = csvParser
                .ReadFromString(csvReaderOptions, csvInput)
                .Select(x => x.Result).ToList();

        }
        // Validate Salaries
        private bool ValidateSalaries(List<Employee> employeeList)
        {
            try
            {
                int salary = 0;
                var isAllInts = employeeList.All(x => int.TryParse(x.Salary.ToString(), out salary));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Validate Non employee managers
        private bool ValidateNonEmployeeManagers(List<Employee> employeeList)
        {
            try
            {
                if (employeeList.Where(e => string.IsNullOrEmpty(e.ManagerId) && string.IsNullOrEmpty(e.Id)).Any())
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Validate Employee with more than one manager
        private bool ValidateEmployees(List<Employee> employeeList)
        {
            try
            {
                if (employeeList.GroupBy(i => i.Id).Where(e => e.Count() > 1).Where(e => !string.IsNullOrEmpty(e.FirstOrDefault().ManagerId)).Any())
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // validations for single CEO
        private  bool ValidateCEO(List<Employee> employeeList)
        {
            try
            {
                if (employeeList.GroupBy(i => i.Id).Where(e => e.Count() > 1).Where(e => string.IsNullOrEmpty(e.FirstOrDefault().ManagerId)).Any())
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // validations for single CEO
        private bool ValidateCircularReference(List<Employee> employeeList)
        {
            bool circulaRefFound=false;
            try
            {
                foreach (var item in employeeList)
                {
                    if (_employeeList.Any(r => r.Id == item.ManagerId && r.ManagerId == item.Id))
                    {
                        circulaRefFound = true;
                        break;
                    };
                }
                return circulaRefFound;
            }
            catch (Exception ex)
            {
                //catch errors
                return false;
            }
        }

        // validations for single CEO
        public long ManagerSalaryBudget(string managerId)
        {
            try
            {
                var salaryBudget = 0;

                var hgd = _employeeList.Where(f => f.ManagerId == managerId).ToList();
                //Get employees reporting directly
                salaryBudget += _employeeList.Where(e => e.ManagerId== managerId).Sum(s => s.Salary);
                //Get employees reporting indirectly
                foreach (var item in _employeeList.Where(f => f.ManagerId == managerId).ToList())
                {
                    var Found = _employeeList.Where(f => f.ManagerId == item.Id).Sum(s=>s.Salary);
                    if (Found > 0) {

                        salaryBudget += Found;

                    }

                }

                return salaryBudget;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private class MappingHelper : CsvMapping<Employee>
        {
            public MappingHelper()
                : base()
            {
                MapProperty(0, x => x.Id);
                MapProperty(1, x => x.ManagerId);
                MapProperty(2, x => x.Salary);
            }

        }

    }
}
