using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nkuli.Models;
using Microsoft.EntityFrameworkCore;

namespace Nkuli.Controllers
{
    public class StudentController : Controller
    {

        private readonly NkuliContext _context;


        public StudentController(NkuliContext _context) {
            this._context = _context;
        }

        // GET: StudentController
  
        public List<Student> SortStudent(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.ShoolNameSortParm = sortOrder == "SchoolName" ? "SchoolName_desc" : "SchoolName";
            ViewBag.PhoneNumberSortParm = sortOrder == "Id" ? "Id_desc" : "Id";


            var searchList = from s in _context.Student
                             select s;

            switch (sortOrder)
            {
                case "Name_desc":
                    searchList = searchList.OrderByDescending(s => s.Name);
                    break;
                case "Surname":
                    searchList = searchList.OrderBy(s => s.Surname);
                    break;
                case "Surname_desc":
                    searchList = searchList.OrderByDescending(s => s.Surname);
                    break;
                case "SchoolName":
                    searchList = searchList.OrderBy(s => s.SchoolName);
                    break;
                case "SchoolName_desc":
                    searchList = searchList.OrderByDescending(s => s.SchoolName);
                    break;
                case "Id":
                    searchList = searchList.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    searchList = searchList.OrderByDescending(s => s.Id);
                    break;
                default:
                    searchList = searchList.OrderBy(s => s.Id);
                    break;
            }

            return searchList.ToList();
        }
        public async Task<List<Student>> SearchStudentAsync(string searchValue, string searchColunm)
        {
            var searchList = await _context.Student.ToListAsync();

            if (searchColunm == "Name")
            {
                searchList = searchList.Where(student => student.Name == searchValue).ToList();
            }
            else if (searchColunm == "Surname")
            {

                searchList = searchList.Where(student => student.Surname == searchValue).ToList();
            }
            else if (searchColunm == "Id")
            {

                searchList = searchList.Where(student => student.Id.ToString() == searchValue).ToList();
            }
            else if (searchColunm == "SchoolName")
            {
                searchList = searchList.Where(student => student.SchoolName.ToString() == searchValue).ToList();
            }
        
            return searchList;
        }

        public async Task<ViewResult> IndexAsync(string searchValue, string searchColunm, string sortOrder)
        {

            List<Student> searchList = await SearchStudentAsync(searchValue, searchColunm);

            if (searchValue != null && searchColunm != null)
            {
                return View(searchList);
            }

            searchList = SortStudent(sortOrder);

            return View(searchList);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {

            var student = _context.Student.Find(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            try
            {
                Student student = new Student();
                student.Name = collection["Name"];
                student.Surname = collection["Surname"];
                student.SchoolName = collection["SchoolName"];
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {

            var student = _context.Student.Find(id);

            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var student = _context.Student.Find(id);
                student.Name = collection["Name"];
                student.Surname = collection["Surname"];
                student.SchoolName = collection["SchoolName"];
               
                _context.Update(student);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = _context.Student.Find(id);

            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var student = _context.Student.Find(id);
                student.Name = collection["Name"];
                student.Surname = collection["Surname"];
                student.SchoolName = collection["SchoolName"];
                _context.Remove(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
