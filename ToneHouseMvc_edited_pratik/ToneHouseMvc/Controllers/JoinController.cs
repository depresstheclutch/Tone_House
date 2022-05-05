using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToneHouseMvc.Models;

namespace ToneHouseMvc.Controllers
{
    public class JoinController : Controller
    {
        db_TheToneHouseEntities2 db = new db_TheToneHouseEntities2();
        tb_SignUp tb_SignUp = new tb_SignUp();
        JoinModel JoinModel = new JoinModel();
        //----------------------------------------------Join Method Starts------------------------------------------------------
        #region Personal Details Join Method
        /// <summary>
        /// Model level Validation
        /// </summary>
        /// <returns></returns>
        public ActionResult Join()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Join(JoinModel JoinModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // List<tb_PersonalDetails> pdobj = new List<tb_PersonalDetails>();
                    //pdobj.Add(personalDetailsModel.UserId);
                    //personalDetails.UserId = Convert.ToInt32(PersoanlDetailsModel.UserId);
                    //tb_SignUp.SignUp_Id = 4;
                    tb_SignUp.FirstName = JoinModel.Firstname;
                    tb_SignUp.MiddleName = JoinModel.Middlename;
                    tb_SignUp.LastName = JoinModel.Lastname;
                    tb_SignUp.Email = JoinModel.Email;
                    tb_SignUp.Password = JoinModel.Password;

                    // personalDetails.tb_SignUp = Convert.ToInt32(personalDetailsModel.UserId);

                    // var i=personalDetailsModel.UserId;

                    db.tb_SignUp.Add(tb_SignUp);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");

                }
                else
                {
                    return View();
                }

            }
            catch (SqlException exsql) {
                return Content(exsql.Message);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
        #endregion 
        //----------------------------------------------Join Method Ends--------------------------------------------------------
    }
}


