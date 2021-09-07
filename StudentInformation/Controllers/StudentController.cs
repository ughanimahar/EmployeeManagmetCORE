using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformation.Models;

namespace StudentInformation.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _Db;


        public StudentController(StudentContext db)
        {
            _Db = db;
        }
        public IActionResult StudentList()
        {
            try
            {
                //var stdList = _Db.tbl_Student.ToList();
                var stdlist = from a in _Db.tbl_Student
                              join b in _Db.tbl_Dp
                              on a.DpId equals b.Id
                              into dep
                              from b in dep.DefaultIfEmpty()
                              select new Student
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  FName = a.FName,
                                  Email = a.Email,
                                  Mobile = a.Mobile,
                                  Description = a.Description,
                                  DpId = a.DpId,
                                  Department = b == null ? "" : b.DpName
                              };

                return View(stdlist);

            }
            catch (Exception)
            {

                return View();
            }
     
        }

        public IActionResult Create(Student obj)
        {
            //loadDLL();
            //return View(obj);
            loadDLL();
            return View("Create", new Student());
        }
       
        public IActionResult Edit(int id)
        {
            //loadDLL();
            //return View(obj);
            loadDLL();
            return View("Create", _Db.tbl_Student.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student obj)
        {
            try
            {
                 if (obj.Id == 0)
                    {
                        _Db.tbl_Student.Add(obj);
                        await _Db.SaveChangesAsync();

                    }
                    else {
                    _Db.Entry(obj).State = EntityState.Modified;
                    await _Db.SaveChangesAsync();
                }
                    return RedirectToAction("StudentList");
                
              
            }
            catch (Exception ex)
            {

                return RedirectToAction("StudentList");
            }
        }
        public async Task<IActionResult> DeleteStd(int Id)
        {
            try
            {
                var std = await _Db.tbl_Student.FindAsync(Id);
                if (std != null)
                {
                    _Db.tbl_Student.Remove(std);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("StudentList");
            }
            catch (Exception)
            {

                return RedirectToAction("StudentList");
            }
        }

        public void loadDLL()
        {
            try
            {
                List<Departments> dplist = new List<Departments>();
                dplist = _Db.tbl_Dp.ToList();
                dplist.Insert(0, new Departments { Id = 0, DpName = "Please Select" });
                ViewBag.DepList = dplist;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}