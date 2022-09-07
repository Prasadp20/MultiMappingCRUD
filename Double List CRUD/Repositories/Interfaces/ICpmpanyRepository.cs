using Double_List_CRUD.DTO;
using Double_List_CRUD.Model;

namespace Double_List_CRUD.Repositories.Interfaces
{
    public interface ICpmpanyRepository
    {
        public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();
        public Task<Company> GetCompanyEmployeesMultipleResults(int cId);
        public Task<int> CreateCompany(CreateCompanyDto comp);
        public Task<int> UpdateCompany(UpdateCompanyDto comp);
        public Task<int> DeleteCompany(int cId);
    }
}
