using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToneHouseMvc.Models;

namespace ToneHouseMvc.Controllers
{
    public class StartController : Controller
    {
        db_TheToneHouseEntities2 db = new db_TheToneHouseEntities2();
        DietContentFetch dietContentFetch = new DietContentFetch();
        tb_Diet tb_Diet = new tb_Diet();
        //making same controller for static pages
        //-------------------------------------------------------Home Page Starts-----------------------------------------------------------
        #region Home Page
        /// <summary>
        /// Includes the home page with static pages
        /// </summary>
        /// <returns></returns>
        // GET: Start
        public ActionResult Index()
        {
            List<tb_ExerciseGroup> GroupList = db.tb_ExerciseGroup.ToList();
            ViewBag.GroupList = new SelectList(GroupList, "GroupId", "GroupName");
            return View();
           
        }
     
        #endregion
        //-------------------------------------------------------Home Page Ends------------------------------------------------------------


    


        //-----------------------------------------------------------Diet Page Starts----------------------------------------------------
        #region Diet Page
        /// <summary>
        /// Displays all the recipes that can be follwed by the user
        /// </summary>
        /// <returns></returns>
        public ActionResult Diet()
        {
        
            try {

              
              var fetchvalues = (from s in db.tb_Diet
                                                    select new { s.DishTime, s.TypeID }).ToList();
                foreach (var item in fetchvalues)
                {
                   dietContentFetch= fetch(item.DishTime, item.TypeID);
                }

                return View(dietContentFetch);
            }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
            }
            catch (Exception exception) {
                //return RedirectToAction("ErrorDiet");
                return Content(exception.Message);
            }
            
        }

        //Common function
        public DietContentFetch fetch(string time,int type) {
            if (time.Equals("BreakFast") && type == 1) {
                dietContentFetch.wgbreakfast = (from s in db.tb_Diet
                                                join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                                where s.DishTime == time && s.TypeID == type
                                                select s).ToList();
               
            }
            if (time.Equals("Lunch") && type == 1)
            {
                dietContentFetch.wglunch = (from s in db.tb_Diet
                                                join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                                where s.DishTime == time && s.TypeID == type
                                                select s).ToList();

            }
            if (time.Equals("Snacks") && type == 1)
            {
                dietContentFetch.wgsnacks = (from s in db.tb_Diet
                                                join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                                where s.DishTime == time && s.TypeID == type
                                                select s).ToList();

            }
            if (time.Equals("Dinner") && type == 1)
            {
                dietContentFetch.wgdinner = (from s in db.tb_Diet
                                                join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                                where s.DishTime == time && s.TypeID == type
                                                select s).ToList();

            }
            if (time.Equals("BreakFast") && type == 2)
            {
                dietContentFetch.wlbreakfast = (from s in db.tb_Diet
                                                join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                                where s.DishTime == time && s.TypeID == type
                                                select s).ToList();

            }
            if (time.Equals("Lunch") && type == 2)
            {
                dietContentFetch.wllunch = (from s in db.tb_Diet
                                            join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                            where s.DishTime == time && s.TypeID == type
                                            select s).ToList();

            }
            if (time.Equals("Snacks") && type == 2)
            {
                dietContentFetch.wlsnacks = (from s in db.tb_Diet
                                             join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                             where s.DishTime == time && s.TypeID == type
                                             select s).ToList();

            }
            if (time.Equals("Dinner") && type == 2)
            {
                dietContentFetch.wldinner = (from s in db.tb_Diet
                                             join sa in db.tb_Type on s.TypeID equals sa.TypeId
                                             where s.DishTime == time && s.TypeID == type
                                             select s).ToList();

            }
            return dietContentFetch;
        }
        #endregion
        //------------------------------------------------------------Diet Page Ends---------------------------------------------------



        //------------------------------------------------------------About Page Starts--------------------------------------------------
        public ActionResult About()
        {
            return View();
        }
        //--------------------------------------------------------------About Page Ends------------------------------------------------
        // AJAX implementation
        public ActionResult Ajax()
        {
            List<tb_ExerciseGroup> GroupList = db.tb_ExerciseGroup.ToList();
            ViewBag.GroupList = new SelectList(GroupList, "GroupId", "GroupName");
            return View();
        }
        public JsonResult GetGroupList(int GroupId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tb_Exercise> ExerciseList = db.tb_Exercise.Where(x => x.GroupId == GroupId).ToList();

            return Json(ExerciseList, JsonRequestBehavior.AllowGet);
        }

    }
}