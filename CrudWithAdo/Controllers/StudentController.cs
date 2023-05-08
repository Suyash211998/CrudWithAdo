using CrudWithAdo.DAL;
using CrudWithAdo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithAdo.Controllers
{
    public class StudentController : Controller
    {
        private Student_DAL dal;

        public StudentController(Student_DAL dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> getStudents = new List<Student>();

            try
            {
                getStudents = dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(getStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Student model)
        {
            if(!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Invalid";
            }
            bool result = dal.Insert(model);
            if (!result)
            {
                TempData["errorMessage"] = "Not inserted";
                return View();
            }
            return RedirectToAction("Index");
            
        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
            try
            {
                Student student = dal.GetById(id);
                if (student == null)
                {
                    TempData["errorMessage"] = $"studnet mot available with id :{id}";
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Inavid";
                    return View();
                }
                bool success = dal.Update(model);
                if (!success)
                {
                    TempData["errorMessage"] = "Not inserted";
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }


    }
}
