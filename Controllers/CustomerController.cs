using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class CustomerController : Controller

    { 
        private readonly MVCContext _context;
        public CustomerController(MVCContext context)
        {
        _context = context;    
        }
        public async Task< IActionResult> Index()
        {


            return View(await _context.Customers.ToListAsync() );
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
       
 public async Task<IActionResult> Create([Bind("Id,Name,Email,Birth")] Customer customer)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Email,Birth")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            return View(customer);
        }

        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id); 
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }


    }
}
