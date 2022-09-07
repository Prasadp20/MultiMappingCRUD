using System.Collections.Generic;
using System.Security.Cryptography;
using Dapper;
using Double_List_CRUD.Context;
using Double_List_CRUD.DTO;
using Double_List_CRUD.Model;
using Double_List_CRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Double_List_CRUD.Repositories
{
    public class CompanyRepository : ICpmpanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCompany(CreateCompanyDto comp)
        {
            int result = 0;
            var qry = "INSERT INTO Company (cName, cAddress) VALUES (@cName, @cAddress) " +
                      " SELECT CAST(SCOPE_IDENTITY() as int) ";

            var parameter = new DynamicParameters();
            parameter.Add("cName", comp.cName);
            parameter.Add("cAddress", comp.cAddress);

            using(var connection = _context.CreateConnection())
            {
                result = await connection.QuerySingleAsync<int>(qry, parameter);
                if(result != 0)
                {
                    var result1 = await AddEmployee(comp.employee, result);
                }
                return result;
            }
        }

        private async Task<int> AddEmployee(List<Employee> employees, int cId)
        {
            int result = 0;
            using (var connection = _context.CreateConnection())
            {
                if(employees.Count > 0)
                {
                    foreach(Employee employee in employees)
                    {
                        employee.cId = cId;
                        var qry = " INSERT INTO Employee (cId,empName,empSalary) " +
                                  " VALUES (@cId,@empName,@empSalary) ";

                        var result1 = await connection.ExecuteAsync(qry, employee);

                        result = result1 + result1;
                    }
                }
                return result;
            }
        }

        public async Task<int> DeleteCompany(int cId)
        {
            int result = 0;
            var qry = "DELETE FROM Company WHERE cId = @cId" +
                " DELETE FROM Employee WHERE cId = @cId";

            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteAsync(qry, new { cId });
                return result;
            }
        }

        public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
        {
            List<Company> companies = new List<Company>();
            var qry1 = "SELECT * FROM Company";
            using(var connection = _context.CreateConnection())
            {
                var res = await connection.QueryAsync<Company>(qry1);
                companies = res.ToList();
                foreach(var company in companies)
                {
                    var qry2 = "SELECT * FROM Employee WHERE cId = @cId";

                    var obj = await connection.QueryAsync<Employee>(qry2);
                    company.employee = obj.ToList();
                }
                return companies;
            }
        }

        public async Task<Company> GetCompanyEmployeesMultipleResults(int cId)
        {
            var qry = " SELECT * FROM Company WHERE cId = @cId " +
                      " SELECT * FROM Employee WHERE cId = @cId ";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(qry, new { cId }))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if (company != null)
                    company.employee = (await multi.ReadAsync<Employee>()).ToList();
                return company;
            }
        }

        public async Task<int> UpdateCompany(UpdateCompanyDto comp)
        {
            int result = 0;
            var qry = "UPDATE Company SET cAddress = @cAddress WHERE cId = @cId";

            using(var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteAsync(qry, comp);
                if(result != 0)
                {
                    result = await connection.ExecuteAsync(@"DELETE FROM Employee WHERE cId = @cId", new { cId = comp.cId });
                    var result1 = await AddEmployee(comp.employee, comp.cId);
                }
                return result;
            }
        }
    }
}
