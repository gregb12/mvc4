using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class c1Controller : Controller
    {



        private CustomerContext _customerContext;

        public c1Controller(CustomerContext ctx)
        {
            this._customerContext = ctx;
        }
        
        public IActionResult Customers()
        {
            var customers = this._customerContext.Customer.ToList(); ;
            return View(customers);
        }

        public async Task<IActionResult> Edit(int id)
        {
           var customer = await  this._customerContext.Customer.FindAsync(id);

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, first_name,last_name")] Customer customer)
        {
          
            if(id != customer.id)
            {
                return NotFound();
            }
            if(this.ModelState.IsValid)
            {
                this._customerContext.Update(customer);
                try
                {
                    await this._customerContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    if(await (this._customerContext.Customer.FindAsync(id)) == null)
                    {
                        return NotFound();

                    }
                    throw;
                }
                
                return RedirectToAction(nameof(Customers));
            }

            return View(customer); ;

        }
    
         public async Task<IActionResult> Delete(int id) 
        {
            var customer  = await this._customerContext.Customer.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            this._customerContext.Remove(customer);
            await this._customerContext.SaveChangesAsync();
            return RedirectToAction(nameof(Customers));


          
        }

        public IActionResult Add()
        {
            var customer = new Customer { id = 0, first_name = "", last_name = "" };
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("id, first_name,last_name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                this._customerContext.Add(customer);
                await this._customerContext.SaveChangesAsync();
                return RedirectToAction(nameof(Customers));
            }

            return View(customer);
        }


        public async Task<IActionResult> Search(string search)
        {
            if (this._customerContext.Customer ==null)
            {
                return Problem("Entity CustomerContext.Customer is null");
            }
            var customers= from m in this._customerContext.Customer select m;
            if(!String.IsNullOrEmpty(search))
            {
                customers = customers.Where(s => s.first_name.Contains(search));
            }

            return View( await customers.ToListAsync());
        }

    }
}
