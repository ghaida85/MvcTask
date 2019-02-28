using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTask.Models;
using System.Collections.ObjectModel;
using MvcTask.CustomActionFilter;

namespace MvcTask.Controllers
{

    [LogActionFilter]
    public class StudentController : Controller
    {
        public static IList<Student> studentList = new List<Student>() {
                    new Student(){ StudentId=1, StudentName="John", Age = 18 },
                    new Student(){ StudentId=2, StudentName="Steve", Age = 21 },
                    new Student(){ StudentId=3, StudentName="Bill", Age = 25 },
                    new Student(){ StudentId=4, StudentName="Ram", Age = 20 },
                    new Student(){ StudentId=5, StudentName="Ron", Age = 31 },
                    new Student(){ StudentId=6, StudentName="Chris", Age = 17 },
                    new Student(){ StudentId=7, StudentName="Rob", Age = 19 }
                };
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.TotalStudents = studentList.Count();
            return View(studentList);
        }


        ///////////////////////////////// Create////////////////////////////////
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string name = collection["StudentName"];

            var std = studentList.Where(s => s.StudentName == name).FirstOrDefault();
            if (std != null)
            {
                ModelState.AddModelError(String.Empty, "Student Name already exists");
            }


            if (ModelState.IsValid)
            {
                int age = Convert.ToInt32(collection["Age"]);
                studentList.Add(new Student() { StudentId = 15, StudentName = name, Age = age });

                return RedirectToAction("Index");
            }

            return View();
        }



        //////////////////////////////// Edit ///////////////////////////////////////////


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                Id = 0;

                return RedirectToAction("Create");
            }
            else
            {
                var std = studentList.Where(s => s.StudentId == Id).FirstOrDefault();


                return View(std);
            }
        }



        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            string name = collection["StudentName"];
            int age = Convert.ToInt32(collection["Age"]);

            var sttd = studentList.Where(s => s.StudentId == id).FirstOrDefault();

            var std = studentList.Where(s => s.StudentName == name).FirstOrDefault();
            if (std != null)
            {
                ModelState.AddModelError(String.Empty, "Student Name already exists");
            }

            if (ModelState.IsValid)
            {
                if (id == 0)
                {

                    studentList.Add(new Student() { StudentId = 1, StudentName = name, Age = age });
                }
                else
                {
                    sttd.StudentName = name;
                    sttd.Age = age;
                }



                return RedirectToAction("Index");
            }

            return View(sttd);
        }



        ///////////////////////////////////// Delete ////////////////////////////////////////

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var std = studentList.Where(x => x.StudentId == id).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public ActionResult Delete(int Id, Student x)
        {
            var std = studentList.Where(s => s.StudentId == Id).FirstOrDefault();
            studentList.Remove(std);
            return RedirectToAction("Index");
        }
    }
}