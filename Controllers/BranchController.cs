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
    [Route("Branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private ApplicationDbContext _ctx = null;
        public BranchController(ApplicationDbContext context)
        {
            _ctx = context;
        }


        // GET: api/Values/GetBranches
        [HttpGet, Route("GetBranches")]
        public async Task<object> GetBranch()
        {
            List<Branch> branch = null;
            try
            {
                using (_ctx)
                {
                    branch = await _ctx.Branches.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return branch;
        }

        [HttpGet, Route("GetBranch/{id}")]
        public async Task<Branch> GetBranch(int id)
        {
            Branch branch = null;
            try
            {
                using (_ctx)
                {
                    branch = await _ctx.Branches.FirstOrDefaultAsync(b => b.BranchID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return branch;
        }

        // POST api/Values/PostBranch
        [HttpPost, Route("AddBranch")]
        public async Task<object> AddBranch(Branch branch)
        {
            object result = null; string message = "";
            if (branch == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Branches.Add(branch);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // PUT api/Values/PutBranch/5
        [HttpPut, Route("UpdateBranch")]
        public async Task<object> UpdateBranch(Branch branch)
        {
            object result = null; string message = "";
            if (branch == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Branches.FirstOrDefault(x => x.BranchID == branch.BranchID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.BranchName = branch.BranchName;
                        entityUpdate.BranchLocation = branch.BranchLocation;
                        entityUpdate.Division = branch.Division;

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

        // DELETE api/Values/DeleteBranchById/5
        [HttpDelete, Route("DeleteBranch")]
        public async Task<object> DeleteBranch(Branch branch)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Branches.SingleOrDefault(x => x.BranchID == branch.BranchID);
                    if (idToRemove != null)
                    {
                        _ctx.Branches.Remove(idToRemove);
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
    }
}