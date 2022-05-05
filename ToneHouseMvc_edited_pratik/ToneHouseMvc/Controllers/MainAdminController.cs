using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToneHouseMvc.Models;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Net;

namespace ToneHouseMvc.Controllers
{
    public sealed class MainAdminController : Controller
    {
        // GET: MainAdmin
        db_TheToneHouseEntities2 db = new db_TheToneHouseEntities2();
        PersoanlDetailsModel PersoanlDetailsModel = new PersoanlDetailsModel();
        tb_PersonalDetails personalDetails = new tb_PersonalDetails();

        //--------------------------------------------------------------Admin Index Page-----------------------------------------------
        #region Admin Index Page
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        //------------------------------------------------------------Admin Index Page Ends--------------------------------------------


        //--------------------------------------------------------------Excercise Page Starts------------------------------------------
        #region Admin Excerise Page with CRUD Operations
        /// <summary>
        /// Admin Page with Valid CRUD Operations on Exercise Page
        /// </summary>
        /// <returns>The Exercise is updated, deleted or edited by the Admin</returns>
        public ActionResult DisplayExercise()
        {
           
            return View(db.Sp_DisplayDetails());
        }

        public ActionResult CreateExcercise()
        {
            try
            {

                ViewBag.excercise = from sid in db.tb_Exercise
                                    select sid.ExerciseId;
                ViewBag.workout = from wid in db.tb_Workout
                                  select wid.WorkoutName;
                ViewBag.type = from tid in db.tb_Type
                               select tid.TypeName;
                ViewBag.group = from gid in db.tb_ExerciseGroup
                                select gid.GroupName;
                return View();
            }
            catch (SqlException sqlexception) {
                return Content(sqlexception.Message);
            }
            catch (Exception exception) {
                return Content(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateExcercise(tb_Exercise excercise, HttpPostedFileBase VideoUpload, FormCollection fc)
        {
            try {
                string workoutname = fc["WorkoutName"];
                string typename = fc["TypeName"];
                string groupname = fc["GroupName"];

                var workoutid = (from wid in db.tb_Workout
                                where wid.WorkoutName == workoutname
                                select wid.WorkoutId).Single();

                var typeid = (from tid in db.tb_Type
                             where tid.TypeName == typename
                             select tid.TypeId).Single();

                var groupid = (from gid in db.tb_ExerciseGroup
                              where gid.GroupName == groupname
                              select gid.GroupId).Single();

                excercise.WorkoutId = workoutid;
                excercise.TypeId = typeid;
                excercise.GroupId = groupid;

                string filename = Path.GetFileNameWithoutExtension(VideoUpload.FileName);
            string extension = Path.GetExtension(VideoUpload.FileName);
            filename = filename + extension;
            excercise.VideoPath = "~/Videos/" + filename;
            filename = Path.Combine(Server.MapPath("~/Videos/"), filename);
            VideoUpload.SaveAs(filename);
            //personalDetails.PhotoPath = PersoanlDetailsModel.PhotoPath;
            if (ModelState.IsValid)
            {
                db.tb_Exercise.Add(excercise);
                db.SaveChanges();
            }
            return RedirectToAction("DisplayExercise");
                }
            catch (SqlException sqlexception) {

                return Content("SQL Exception ;"+sqlexception.Message);
                 }
            catch (Exception exception) {
                return Content("General Exception : "+exception.Message);
            }
        }
     

        public ActionResult EditExcercise(int eid)
        {
            try {
                ViewBag.excercise = from sid in db.tb_Exercise
                                    select sid.ExerciseId;
                ViewBag.workout = from wid in db.tb_Workout
                                  select wid.WorkoutName;
                ViewBag.type = from tid in db.tb_Type
                               select tid.TypeName;
                ViewBag.group = from gid in db.tb_ExerciseGroup
                                select gid.GroupName;

                return View(db.tb_Exercise.Find(eid));
                 }
            catch (SqlException sqlexception) {
                return Content(sqlexception.Message);
             }
            catch (Exception exception) {
                return Content(exception.Message);
            }
        }
        [HttpPost]
        public ActionResult EditExcercise(tb_Exercise excercise, HttpPostedFileBase VideoUpload, FormCollection fc)
        {
            try {
                string workoutname = fc["WorkoutName"];
                string typename = fc["TypeName"];
                string groupname = fc["GroupName"];
                

                var workoutid = from wid in db.tb_Workout
                                where wid.WorkoutName == workoutname
                                select wid.WorkoutId;

                var typeid = from tid in db.tb_Type
                             where tid.TypeName == typename
                             select tid.TypeId;

                var groupid = from gid in db.tb_ExerciseGroup
                              where gid.GroupName == groupname
                              select gid.GroupId;

                excercise.WorkoutId = workoutid.SingleOrDefault();
                excercise.TypeId = typeid.SingleOrDefault();
                excercise.GroupId = groupid.SingleOrDefault();
                string filename = Path.GetFileNameWithoutExtension(VideoUpload.FileName);
            string extension = Path.GetExtension(VideoUpload.FileName);
            filename = filename + extension;
            excercise.VideoPath = "~/Videos/" + filename;
            filename = Path.Combine(Server.MapPath("~/Videos/"), filename);
            VideoUpload.SaveAs(filename);
            //personalDetails.PhotoPath = PersoanlDetailsModel.PhotoPath;


            if (ModelState.IsValid)
            {
                db.Entry(excercise).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("DisplayExercise");
             }
            catch (SqlException sqlexception) {
                return Content(sqlexception.Message);
             }
            catch (Exception exception) {
                return Content(exception.Message);
            }
        }
        public ActionResult DeleteExcercise(int id, HttpPostedFileBase ImageUpload)
        {
            try { 
            tb_Exercise exercise = db.tb_Exercise.Find(id);
            db.tb_Exercise.Remove(exercise);
            db.SaveChanges();
            return RedirectToAction("DisplayExercise");
            }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
        #endregion
        //--------------------------------------------------------------Excercise Page Ends--------------------------------------------

        //--------------------------------------------------------------Diet Page Starts-----------------------------------------------
        #region Admin Diet Page with CRUD Operations
        /// <summary>
        /// Admin Page with Valid CRUD Operations on Diet Page
        /// </summary>
        /// <returns>The Diet is updated, deleted or edited by the Admin</returns>
        public ActionResult DisplayDiet()
        {
            try
            {

        
                if (db.Sp_DietDetails() != null)
                {
                    return View(db.Sp_DietDetails());
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex) {
                return Content(ex.Message);
            }
        }
        public ActionResult CreateDiet()
        {
            try {

                ViewBag.diet = from did in db.tb_Diet
                               select did.DietID;


                ViewBag.type = from tid in db.tb_Type
                               select tid.TypeName;
                List<string> time = new List<string>();
            time.Add("Breakfast");
            time.Add("Lunch");
            time.Add("Snacks");
            time.Add("Dinner");
            ViewBag.dishtime = time;
            


            return View();
            }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }

        }
        [HttpPost]
        public ActionResult CreateDiet(tb_Diet diet, HttpPostedFileBase ImageUpload,FormCollection fc)
        {
            try {
                string typename = fc["TypeName"];


                var typeid = from tid in db.tb_Type
                             where tid.TypeName == typename
                             select tid.TypeId;


                diet.TypeID = typeid.SingleOrDefault();

                string filename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string extension = Path.GetExtension(ImageUpload.FileName);
            filename = filename + extension;
            diet.ImagePath = "~/stamina/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/stamina/Images/"), filename);
            ImageUpload.SaveAs(filename);
            //personalDetails.PhotoPath = PersoanlDetailsModel.PhotoPath;
            if (ModelState.IsValid)
            {
                db.tb_Diet.Add(diet);
                db.SaveChanges();
            }
            return RedirectToAction("DisplayDiet");
        }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
             }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }

        }
        public ActionResult EditDiet(int? deid)
        {
            try
            {
                if (deid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    ViewBag.diet = from did in db.tb_Diet
                                   select did.DietID;

                    ViewBag.type = from tid in db.tb_Type
                                   select tid.TypeName;
                    List<string> time = new List<string>();
                    time.Add("Breakfast");
                    time.Add("Lunch");
                    time.Add("Snacks");
                    time.Add("Dinner");
                    ViewBag.dishtime = time;


                    return View(db.tb_Diet.Find(deid));
                }
            }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
        [HttpPost]
        public ActionResult EditDiet(tb_Diet diet, HttpPostedFileBase ImageUpload,FormCollection fc)
        {
            try {
                string typename = fc["TypeName"];


                var typeid = from tid in db.tb_Type
                             where tid.TypeName == typename
                             select tid.TypeId;


                diet.TypeID = typeid.SingleOrDefault();

                string filename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string extension = Path.GetExtension(ImageUpload.FileName);
            filename = filename + extension;
            diet.ImagePath = "~/stamina/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/stamina/Images/"), filename);
            ImageUpload.SaveAs(filename);
            if (ModelState.IsValid)
            {
                db.Entry(diet).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("DisplayDiet");
             }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
             }
            catch (Exception exception)
            {
                return Content(exception.Message);
}
        }
        public ActionResult DeleteDiet(int id)
        {
            try { 
            tb_Diet diet = db.tb_Diet.Find(id);
            db.tb_Diet.Remove(diet);
            db.SaveChanges();
            return RedirectToAction("DisplayDiet");
                 }
            catch (SqlException sqlexception)
            {
                return Content(sqlexception.Message);
             }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
        #endregion
        //--------------------------------------------------------------Diet Page Ends-------------------------------------------------
        // SEARCH implementation
        public ActionResult Search(string searchBy, string search)
        {
            if (searchBy == "FirstName")
            {
                return View(db.tb_SignUp.Where(x => x.FirstName == search || search == null).ToList());
            }
            else
            {
                return View(db.tb_SignUp.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
            }
        }
    }
}