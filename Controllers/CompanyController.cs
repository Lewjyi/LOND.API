using LOND.API.Models;
using LOND.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LOND.API.Controllers
{
    public class CompanyController : Controller
    {

        public IRepository<CompanyObject, int> _companyRepository;  
        public CompanyController(IRepository<CompanyObject, int> companyRepository)
        {
            _companyRepository = companyRepository;
        }   
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCompanyProfile(CompanyObject companyProfileObject)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.InsertAsync(companyProfileObject);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);  
            }
        }

        
    }
}
