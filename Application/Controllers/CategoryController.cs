using Application.Data;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        
        //Get method applied
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); // check all the categories and retrieving database
            return View(objCategoryList);
        }


        // Create action method by default
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
                TempData["success"] = "Category created Successfully"; // temp data gives notification message like created, deleted
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
        //Update method
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            //Default Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); 
                _db.SaveChanges();
                TempData["success"] = "Category updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        //Delete(get) Method will check Id
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) //Delete Name Cant be same bcoz parameter will also be same for get and post
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}

