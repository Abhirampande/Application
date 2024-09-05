using Application.Data;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); // check all the categories and retrieving database
            return View(objCategoryList);
        }
        // Get action method by default
        public IActionResult Create()
        {
            return View();
        }
        //post method
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Server side validation as default not using custom Validation
            //Client side vlidation using Validation Partial
            //Custom Validation Updated
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name.");
            }

            //Default Validation
            if (ModelState.IsValid)// check the regqired validation of model class
            {
                _db.Categories.Add(obj); //keeps the record of all changes
                _db.SaveChanges(); //it will create the category
                return RedirectToAction("Index");// to go to a different controller, write controller name
            }
            return View();
        }
        //For update And Delete Method
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) //if id =null or zero its not valid
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);// it only works for primary key
            //Some other ways to find Our record
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);//for id for name type u=>u.Name=="" or.contains in place of id
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);//pasiing categories to the view to publish categories
        }
        //post method
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
           
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name.");
            }

            //Default Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); 
                _db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View();
            }
        }
}
