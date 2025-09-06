using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace pusula
{
    public static class FilterEmployees
    {
        /*
        public static void Main()
        {

            var data = new List<(string, int, string, decimal, DateTime)>//testing purpose
            {
                ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
                ("Ayşe", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
                ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
            };

            Console.WriteLine(FilterEmployeesFunction(data));
        }
        */
        public static string FilterEmployeesFunction(IEnumerable<(string Name, int Age, string Department, decimal Salary,
        DateTime HireDate)> employees) // since it uses IEnumerable we should use LINQ filtering
        {
            if (employees == null)
            {
                var emptyJson = new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MinSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                };

                return JsonSerializer.Serialize(emptyJson);
            }
            //filter the employees using LINQ
            var filteredEmployees = employees
                .Where(a => a.Age >= 25 && a.Age <= 40 &&
                    (a.Department == "IT" || a.Department == "Finance") &&
                    a.Salary >= 5000 && a.Salary <= 9000 &&
                    a.HireDate >= new DateTime(2017, 12, 31));
            //in the document it is stated that the hireDate is after 2017 however if we make it like this
            //the last tuple {Elif] would not go through the filtering because actually its date is 31.12.2017
            //which means still in 2017. Therefore to be able to give the same result i am changing the condition
            //from > to >= . For your information. Thanks.


            List<string> names = new List<string>();
            List<decimal> salary = new List<decimal>();
            foreach (var employee in filteredEmployees)
            { 
                salary.Add(employee.Salary);
            }

            names = filteredEmployees.OrderByDescending(x => x.Name.Length)//descend by the length of the name
                .ThenBy(x => x.Name)
                .Select(x => x.Name)//order alphabetically
                .ToList();

            //initialize the tuples
            int count = salary.Count;
            decimal totalSalary = count > 0 ? salary.Sum() : 0;
            decimal maxSalary = count > 0 ? salary.Max() : 0;
            decimal minSalary = count > 0 ? salary.Min() : 0;
            decimal avgSalary = count > 0 ? Math.Round(salary.Average(), 2) : 0;

            var result = new //create the json object
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = avgSalary,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);

        }

    }
}
