using Microsoft.AspNetCore.Mvc;
using StructuraFlow.Models;

namespace StructuraFlow.Controllers
{
    public class RulesController : Controller
    {
        private readonly StructuralDbContext _context;
        public RulesController(StructuralDbContext context)
        {
            _context = context;
        }

        public IActionResult Rules()
        {
            var config = _context.ValidationRules.FirstOrDefault();
            if (config == null)
            {
                config = new ValidationRule(); // load defaults if nothing in DB
            }
            return View(config);
        }


        [HttpPost]
        public async Task<IActionResult> Update(ValidationRule rules)
        {
            if (!ModelState.IsValid)
            {
                return View("rules", rules); // Return validation errors
            }

            var existing =  _context.ValidationRules.FirstOrDefault();
            if (existing != null)
            {
                _context.ValidationRules.Remove(existing);
            }
            
            _context.ValidationRules.Add(rules);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Rules saved successfully";
            return RedirectToAction("rules", rules);
        }
    }
}
