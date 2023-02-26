using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            var result = GetEmployeesWithSalaryOver50000(context);
            Console.WriteLine(result);
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
            .Select(e => new { e.EmployeeId, e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary })
            .OrderBy(e => e.EmployeeId)
            .ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            //var employeesInfo = context.Employees
            //        .Where(x => x.Salary > 50000)
            //        .Select(x => new { x.FirstName, x.Salary })
            //        .OrderBy(x => x.FirstName)
            //        .ToList();
            //
            //StringBuilder sb = new StringBuilder();
            //foreach (var e in employeesInfo)
            //{
            //    sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            //}

            //return sb.ToString().TrimEnd();

             var employeesInfo = context.Employees
                .Where(x => x.Salary > 50000)
                .Select(x => new { x.FirstName, x.Salary})
                .OrderBy(x => x.FirstName)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var e in employeesInfo)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}