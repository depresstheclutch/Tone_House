using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ToneHouseMvc.Models;


namespace ToneHouseMvc.Controllers
{
    public class LoginController : Controller
    {
        db_TheToneHouseEntities2 db = new db_TheToneHouseEntities2();
        // db_TheToneHouseEntities db = new db_TheToneHouseEntities();
        tb_SignUp tb_SignUp = new tb_SignUp();

        // static List<tb_SignUp> tb_SignUps = new List<tb_SignUp>();
        LoginModel LoginModel = new LoginModel();

        // GET: Login
        //-------------------------------------------------Login Method Starts-----------------------------------------------
        #region Login Method
        /// <summary>
        /// Login Method for the user Starts with proper validation
        /// </summary>
        /// <returns>Returns error messages on entering wrong data and goes to the next page if entered data is correct</returns>
        //Note:
        //For email we are doing all remote, model and controller level validation
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel LoginModel)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginModel.Email))
                {
                    ModelState.AddModelError("Email", "Please enter your Email");
                }
                if (!string.IsNullOrEmpty(LoginModel.Email))
                {
                    string emailreg = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                    Regex re = new Regex(emailreg);
                    if (!re.IsMatch(LoginModel.Email))
                    {

                        ModelState.AddModelError("Email", "Please enter a valid email address");
                    }
                }
                if (string.IsNullOrEmpty(LoginModel.Password))
                {
                    ModelState.AddModelError("Password", "Please enter your Password");
                }

                if (ModelState.IsValid)
                {
                    if (isExist(LoginModel.Email, LoginModel.Password))
                    {

                        Session["Sessionemail"] = LoginModel.Email;

                        //SessionModel sessionModel = new SessionModel();
                        var sessionModel = (from s in db.tb_SignUp
                                            where s.Email == LoginModel.Email
                                            select
                            new { s.SignUp_Id, s.FirstName, s.LastName }).Single();

                        Session["Name"] = sessionModel.FirstName + " " + sessionModel.LastName;
                        Session["SignupId"] = Convert.ToInt32(sessionModel.SignUp_Id);
                        int x = Convert.ToInt32(Session["SignupId"]);


                        if (Session["Sessionemail"].Equals("admin@admin.com"))
                        {
                            return RedirectToAction("Index", "MainAdmin");
                        }
                        else if (Session["Sessionemail"] != null && !Session["Sessionemail"].Equals("admin@admin.com"))
                        {
                            if ((from s in db.tb_PersonalDetails where s.SignUpId == x select s.UserId).Count() != 0)
                            {
                                Session["filled"] = "filled";
                                return RedirectToAction("Dashboard", "PersonalDashboard");
                            }
                            else
                            {
                                Session["filled"] = "not";
                                return RedirectToAction("PersonalDetailsForm", "PersonalDashboard");
                            }

                        }
                        else
                        {
                            return RedirectToAction("Index", "Start");
                        }

                    }
                    else
                    {
                        Session["Sessionemail"] = null;
                        ViewBag.emailerror = "Invalid emailid or password !";
                        return View();
                    }
                }
                else
                {
                    Session["Sessionemail"] = null;
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

       
        public bool isExist(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
                return false;
            var result = (from r in db.tb_SignUp where r.Email == email && r.Password == password select r).ToList();
            if (result.Count != 0)
                return true;
            else
                return false;
        }

        #endregion
        //-------------------------------------------------Login Method Ends-----------------------------------------------


        //-------------------------------------------------Logout method start-----------------------------------------------
        #region Logout Method
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Start");
        }
        #endregion
        //-------------------------------------------------Logout method end-----------------------------------------------
    }
}