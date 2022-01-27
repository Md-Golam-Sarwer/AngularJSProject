using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee.Data;
using Employee.Models;

namespace Employee.Controllers
{
    [Route("Employer")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;

        public EmployerController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // GET: api/Employer
        [HttpGet, Route("GetEmployers")]
        public async Task<object> GetEmployer()
        {
            List<Employer>employer = null;
            try
            {
                using (_ctx)
                {
                    employer = await _ctx.Employers.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return employer;
        }

        // GET: api/Employer/5
        [HttpGet, Route("GetEmployer/{id}")]
        public async Task<Employer> GetEmployer(int id)
        {
            Employer employer = null;
            try
            {
                using (_ctx)
                {
                    employer = await _ctx.Employers.FirstOrDefaultAsync(b => b.EmployeeID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return employer;
        }

        // PUT: api/Employer/5
        [HttpPut, Route("PutEmployer")]
        public async Task<object> PutEmployer(Employer employer)
        {
            object result = null; string message = "";
            if (employer == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Employers.FirstOrDefault(x => x.EmployeeID == employer.EmployeeID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.EmployeeName = employer.EmployeeName;
                        entityUpdate.Salary = employer.Salary;
                        entityUpdate.ContactAddress = employer.ContactAddress;
       
                        entityUpdate.DOB = employer.DOB;
                        entityUpdate.IsActive = employer.IsActive;
                 
                        entityUpdate.MobileNo = employer.MobileNo;

                        entityUpdate.BranchID = employer.BranchID;

                        await _ctx.SaveChangesAsync();
                    }
                    message = "Entry Updated";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Entry Update Failed!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }

        // POST: api/Employer
        [HttpPost, Route("AddEmployer")]
        public async Task<object> PostEmployer(Employer employer)
        {
            object result = null; string message = "";
            if (employer == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Employers.Add(employer);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // DELETE: api/Employer/5
        [HttpDelete, Route("DeleteEmployer")]
        public async Task<object> DeleteOrderBook(Employer employer)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Employers.SingleOrDefault(x => x.EmployeeID == employer.EmployeeID);
                    if (idToRemove != null)
                    {
                        _ctx.Employers.Remove(idToRemove);
                        await _ctx.SaveChangesAsync();
                    }
                    message = "Deleted Successfully";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Error on Deleting!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }

        private bool EmployerExists(int? id)
        {
            return _ctx.Employers.Any(e => e.EmployeeID == id);
        }
    }

    
}