using Double_List_CRUD.DTO;
using Double_List_CRUD.Model;
using Double_List_CRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Double_List_CRUD.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICpmpanyRepository _repo;
        public CompanyController(ICpmpanyRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var obj = await _repo.GetCompaniesEmployeesMultipleMapping();
                return Ok(obj);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{cId}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int cId)
        {
            try
            {
                var obj = await _repo.GetCompanyEmployeesMultipleResults(cId);
                if(obj == null)
                    return NotFound();

                return Ok(obj);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto comp)
        {
            try
            {
                var obj = await _repo.CreateCompany(comp);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{cId}")]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyDto comp)
        {
            try
            {
                var res = await _repo.UpdateCompany(comp);
                if (res == 0)
                    return NotFound();
                else
                    return Ok("Data Updated");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(int cId)
        {
            try
            {
                var obj = await _repo.GetCompanyEmployeesMultipleResults(cId);
                if (obj == null)
                    return NotFound();

                await _repo.DeleteCompany(cId);
                return Ok("Data Deleted");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
