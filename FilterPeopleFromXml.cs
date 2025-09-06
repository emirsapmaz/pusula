using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace pusula
{
    public static class FilterPeopleFromXml
    {
        /*
        public static void Main()
        {
            string data = "<People><Person><Name>Selim</Name><Age>32</Age><Department>IT</Department><Salary>5500</Salary><HireDate>2018-08-05</HireDate></Person></People>";
            Console.WriteLine(FilterPeopleFromXmlFunction(data));
        }*/
        public static string FilterPeopleFromXmlFunction(string xmlData)
        {
            if (xmlData == null || xmlData.Length == 0) 
            {
                var emptyJson = new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                };

                return JsonSerializer.Serialize(emptyJson);
            }

            List<string> names = new List<string>();
            List<int> salaries = new List<int>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);//load the xmldata into xmlDoc

            XmlNodeList personNodes = xmlDoc.SelectNodes("//Person");//access the persons

            foreach (XmlNode person in personNodes)//get each data from person to do filering
            {
                string name = person.SelectSingleNode("Name")?.InnerText;
                int age = int.Parse(person.SelectSingleNode("Age")?.InnerText ?? "0");
                string department = person.SelectSingleNode("Department")?.InnerText;
                int salary = int.Parse(person.SelectSingleNode("Salary")?.InnerText ?? "0");
                DateTime hireDate = DateTime.Parse(person.SelectSingleNode("HireDate")?.InnerText ?? "1900-01-01");

                //check conditions
                if (age > 30 && department == "IT" && salary > 5000 && hireDate < new DateTime(2019, 1, 1))
                {
                    names.Add(name);
                    salaries.Add(salary);
                }
            }

            names.Sort(); //sort by alphabetical

            //initialize the variables
            int count = salaries.Count;
            int totalSalary = count > 0 ? salaries.Sum() : 0;
            int maxSalary = count > 0 ? salaries.Max() : 0;
            int avgSalary = count > 0 ? (int)salaries.Average() : 0;

            var jsonObject = new//get an object to return as json
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = avgSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(jsonObject);
        }

    }
}
