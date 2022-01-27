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
    [Route("Purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private ApplicationDbContext _ctx = null;
        public PurchaseController(ApplicationDbContext context)
        {
            _ctx = context;
        }


        // GET: api/Values/GetBranches
        [HttpGet, Route("GetPurchases")]
        public async Task<object> GetPurchase()
        {
            List<Purchase> purchase = null;
            try
            {
                using (_ctx)
                {
                    purchase = await _ctx.Purchases.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return purchase;
        }

        [HttpGet, Route("GetPurchase/{id}")]
        public async Task<Purchase> GetPurchase(int id)
        {
            Purchase purchase = null;
            try
            {
                using (_ctx)
                {
                    purchase = await _ctx.Purchases.FirstOrDefaultAsync(b => b.ID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return purchase;
        }

        // POST api/Values/PostBranch
        [HttpPost, Route("AddPurchase")]
        public async Task<object> AddPurchase(Purchase purchase)
        {
            object result = null; string message = "";
            if (purchase == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Purchases.Add(purchase);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // PUT api/Values/PutBranch/5
        [HttpPut, Route("UpdatePurchase")]
        public async Task<object> UpdatePurchase(Purchase purchase)
        {
            object result = null; string message = "";
            if (purchase == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Purchases.FirstOrDefault(x => x.ID == purchase.ID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.Name = purchase.Name;
                        entityUpdate.Price = purchase.Price;
                        entityUpdate.Quantity = purchase.Quantity;

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
        [HttpDelete, Route("DeletePurchase")]
        public async Task<object> DeletePurchase(Purchase purchase)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Purchases.SingleOrDefault(x => x.ID == purchase.ID);
                    if (idToRemove != null)
                    {
                        _ctx.Purchases.Remove(idToRemove);
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