using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class TeacherController : Controller
    {
        //GET: Teacher
      public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //GET: /Teacher/Show/(id)
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        //GET: /Teacher/DeleteConfirm/(id)
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New

        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string employeeNumber, DateTime HireDate, decimal Salary)
        {
            Debug.WriteLine("I have accessed the create method");

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.employeeNumber = employeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");


        }

        //GET : /Teacher/Update/id
        /// <summary>
        /// this method is a GET request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        //POST: /Tecaher/Update/{id}
        /// <summary>
        /// This method is a POST request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="employeeNumber"></param>
        /// <param name="HireDate"></param>
        /// <param name="Salary"></param>
        /// <returns></returns>
        /// 
         [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string employeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.employeeNumber = employeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);


            return RedirectToAction("Show/" + id);
        }


    }
}
