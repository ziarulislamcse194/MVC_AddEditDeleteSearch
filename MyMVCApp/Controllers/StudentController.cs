using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMVCApp.BLL.BLL;
using MyMVCApp.Models.Models;

namespace MyMVCApp.Controllers
{
    public class StudentController : Controller
    {
        StudentManager _studentManager =new StudentManager();
        private Student _student = new Student();

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                if (_studentManager.Add(student))
                {
                    ViewBag.SuccesMsg = "Saved";
                }
                else
                {
                    ViewBag.FailedMsg = "Failed";
                }
            }
            else
            {
                ViewBag.FailedMsg = "VAlidation Failed";
            }
            
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            _student.ID = Convert.ToInt32(Id);
            var strudent = _studentManager.GetByID(_student);
            return View(strudent);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (_studentManager.Update(student))
                {
                    ViewBag.SuccesMsg = "Updated";
                }
                else
                {
                    ViewBag.FailedMsg = "Failed";
                }
            }
            else
            {
                ViewBag.FailedMsg = "VAlidation Failed";
            }

            return View(student);
        }

        [HttpGet]

        public ActionResult Delete(int? id)
        {
            _student.ID = Convert.ToInt32(id);
            var strudent = _studentManager.GetByID(_student);
            return View(strudent);
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            if (ModelState.IsValid)
            {
                if (_studentManager.Delete(student))
                {
                    ViewBag.SuccesMsg = "Deleted";
                }
                else
                {
                    ViewBag.FailedMsg = "Failed";
                }
            }
            else
            {
                ViewBag.FailedMsg = "VAlidation Failed";
            }

            return View(student);
        }


        [HttpGet]

        public ActionResult Show()
        {
            _student.Students = _studentManager.GetAll();
            return View(_student);
        }

        [HttpPost]

        public ActionResult Show(Student student)
        {
            var students = _studentManager.GetAll();
            if (student.Name != null)
            {
                students = students.Where(c => c.Name.ToLower().Contains(student.Name.ToLower())).ToList();
            }

            if (student.Address != null)
            {
                students = students.Where(c => c.Address.ToLower().Contains(student.Address.ToLower())).ToList();
            }

            if (student.Age>0)
            {
                students = students.Where(c => c.Age == student.Age).ToList();
            }

            student.Students = students;
            return View(student);
        }
    }
}