using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToneHouseMvc.Models;

//To-do:
//in personal details update signnup id - to be taken after sessions
//sessionid= signupid

namespace ToneHouseMvc.Controllers
{
    public class PersonalDashboardController : Controller

    {
        db_TheToneHouseEntities2 db = new db_TheToneHouseEntities2();
        tb_SignUp tb_SignUp = new tb_SignUp();
        PersoanlDetailsModel PersoanlDetailsModel = new PersoanlDetailsModel();
        tb_PersonalDetails personalDetails = new tb_PersonalDetails();
        tb_Exercise tb_Exercise = new tb_Exercise();
        dynamic mymodel = new ExpandoObject();


        //--------------------------------------------Personal Dashboard Starts-------------------------------------------------------
        #region Personal Dashboard
        /// <summary>
        /// Personal Dashboard is displayed with Photo of the user after entering Personal Details along with age, name, blood group, height, weight, and Calories burnt
        /// </summary>
        /// <returns>Displays the user the necessary details on a carousel</returns>
        public ActionResult Dashboard() {
            try
            {
                List<tb_Exercise> list = db.tb_Exercise.ToList();

                //cardio
                var run1 = (from s in db.tb_Exercise where s.WorkoutId == 1 select s.VideoPath).ToList();
                ViewBag.videolist1 = run1;
                //weightlift
                var run3 = (from s in db.tb_Exercise where s.WorkoutId == 3 select s.VideoPath).ToList();
                ViewBag.videolist3 = run3;
                //highdensity
                var run5 = (from s in db.tb_Exercise where s.WorkoutId == 5 select s.VideoPath).ToList();
                ViewBag.videolist5 = run5;
               

                Session["Exercises"] = new SelectList(list, "ExerciseId", "ExerciseName");
                int signupid = Convert.ToInt32(Session["SignupId"]);
                var userid = (from s in db.tb_PersonalDetails where s.SignUpId == signupid select s.UserId).Single();
                var sum = (from s in db.tb_PlanOpted
                           where s.UserId == signupid
                           select s.Calories).Sum();
                TempData["Sum"] = Convert.ToInt32(sum);
                
                mymodel.excercise = tb_Exercise;
                if (Session["filled"].Equals("filled"))
                {
                    int signupiddashboard = Convert.ToInt32(Session["SignupId"]);

                    var sessionPersonalForm = (from s in db.tb_PersonalDetails
                                               where s.SignUpId == signupiddashboard
                                               select
                               new { s.Age, s.Height, s.Weight,s.BloodGroup, s.PhotoPath }).Single();

                    personalDetails.PhotoPath = sessionPersonalForm.PhotoPath;
                    personalDetails.Weight = Convert.ToDouble(sessionPersonalForm.Weight);
                    personalDetails.Height = Convert.ToDouble(sessionPersonalForm.Height);
                    personalDetails.BloodGroup = sessionPersonalForm.BloodGroup;
                    personalDetails.Age = Convert.ToByte(sessionPersonalForm.Age);
                    
                    mymodel.personal = personalDetails;
                    return View(mymodel);
                }
                else
                {
                    mymodel.personal = Session["personaldetailkey"];
                    return View(mymodel);
                }

            }
            catch (SqlException exsql)
            {
                return Content("SQL Error : " + exsql.Message);
            }
            catch (Exception exception)
            {
                return Content("General Error : " + exception.Message);
            }
        }
        [HttpPost]
        public ActionResult Dashboard(FormCollection form)
        {
            try
            {
                int signupid = Convert.ToInt32(Session["SignupId"]);

                var details=(from s in db.tb_PersonalDetails where s.SignUpId == signupid select new { s.Age, s.Height, s.Weight }).Single();
                ViewBag.age = details.Age;
                ViewBag.height = details.Height;
                ViewBag.weight = details.Weight;
                
                tb_PlanOpted tb_PlanOpted = new tb_PlanOpted();
                
                var userid=(from s in db.tb_PersonalDetails where s.SignUpId == signupid select s.UserId).Single();
                tb_PlanOpted.UserId = userid;
                tb_PlanOpted.ExerciseId = Int32.Parse(form["ddlExercise"]);
                tb_PlanOpted.Time = float.Parse(form["TBTime"]);
                tb_PlanOpted.Distance = float.Parse(form["TBDistance"]);
                tb_PlanOpted.Calories = float.Parse(form["TBCalorie"]);
                tb_PlanOpted.PaymentId = null;
                
                TempData["planopted"] = tb_PlanOpted;
                TempData.Keep();
                return RedirectToAction("Save");
            }
            catch (SqlException exsql)
            {
                return Content(exsql.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
        public ActionResult Save()
        {
            tb_PlanOpted tb_PlanOpted = TempData["planopted"] as tb_PlanOpted;
            return View(tb_PlanOpted);
        }
        [HttpPost]
        [ActionName("Save")]
        public ActionResult SavePost(tb_PlanOpted objPlanOpted)
        {
            try
            {
               
                objPlanOpted.UserId = Convert.ToInt32(Session["SignupId"]);
            if (ModelState.IsValid)
            {
                db.tb_PlanOpted.Add(objPlanOpted);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
                }
                else
                    return View();
               
            }
            catch (SqlException exsql)
            {
                return Content(exsql.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
           
        }

        #endregion
        //------------------------------------------Personal Dashboard Ends------------------------------------------------------------

        //--------------------------------------------Personal Details Form Starts----------------------------------------------------
        #region Personal Details Form
        /// <summary>
        /// The Form to enter the Personal Details of the user
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalDetailsForm() {
            List<string> bldgrp = new List<string>();
            
            bldgrp.Add("A+");
            bldgrp.Add("A-");
            bldgrp.Add("B+");
            bldgrp.Add("B-");
            bldgrp.Add("O+");
            bldgrp.Add("O-");
            bldgrp.Add("AB+");
            bldgrp.Add("AB-");
            ViewBag.bdgp = bldgrp;
            return View();
        }
        [HttpPost]
        public ActionResult PersonalDetailsForm(PersoanlDetailsModel PersoanlDetailsModelobj, HttpPostedFileBase ImageUpload)
        {
            try
            {
                
             List<string> bldgrp = new List<string>();
            bldgrp.Add("A+");
            bldgrp.Add("A-");
            bldgrp.Add("B+");
            bldgrp.Add("B-");
            bldgrp.Add("O+");
            bldgrp.Add("O-");
            bldgrp.Add("AB+");
            bldgrp.Add("AB-");
            ViewBag.bdgp = bldgrp;
            if (ModelState.IsValid)
                {
                       
                        
                            string filename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                            string extension = Path.GetExtension(ImageUpload.FileName);
                            if (extension == ".png" || extension == ".jpg"|| extension == ".jpeg")
                            {
                                filename = filename + extension;
                                PersoanlDetailsModelobj.PhotoPath = "~/stamina/images/" + filename;
                                filename = Path.Combine(Server.MapPath("~/stamina/images/"), filename);
                                ImageUpload.SaveAs(filename);
                        
                                personalDetails.SignUpId = Convert.ToInt32(Session["SignupId"]);
                                personalDetails.Age = Convert.ToByte(PersoanlDetailsModelobj.Age);
                                personalDetails.Height = Math.Round(Convert.ToDouble(PersoanlDetailsModelobj.Height) / 30.48, 2);
                                personalDetails.Weight = Convert.ToDouble(PersoanlDetailsModelobj.Weight);
                                personalDetails.BloodGroup = PersoanlDetailsModelobj.BloodGroup;
                                personalDetails.Gender = PersoanlDetailsModelobj.Gender;
                                personalDetails.PhoneNo = PersoanlDetailsModelobj.PhoneNo;
                                personalDetails.PhotoPath = PersoanlDetailsModelobj.PhotoPath;
                                db.tb_PersonalDetails.Add(personalDetails);

               
                                Session["personaldetailkey"] = personalDetails;

                                db.SaveChanges();
                                return RedirectToAction("Dashboard", mymodel);
                            }
                            else
                            {
                                ViewBag.Msg = "File Extension can be JPG or PNG or JPEG only !";
                                return View();
                            }
                        
            }
            else
                {
                    return View();
                }
            }
            catch (SqlException exsql)
            {
                return Content(exsql.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
      
        public ActionResult DisplayPersonalDetails()
        {
            return View((from i in db.tb_PersonalDetails select i).ToList());
        }
        #endregion
        //------------------------------------------------Personal DetailsForm Ends----------------------------------------------------
    }
}