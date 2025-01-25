//using MVCFolderStrProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using MVCFolderStrProjectLibrary;


namespace MVCFolderStrProject.Controllers
{
    public class HomeController : Controller
    {

        DataSet ds = new DataSet();
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AllData()
        {
            BALUser balUser = new BALUser();
            List<User> user = await balUser.GetAllUsers();
            return View(user);

        }

        [HttpGet]
        public async Task<ActionResult> AddUser()
        {
            BALUser balUser = new BALUser();
            User user = new User();
            await CountryList();

            ViewBag.States = await StateList(user.CountryId);
            ViewBag.CityName =await CityList(user.StateId);
            return PartialView("AddUser", user);
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(User obj)
        {
            BALUser balUser = new BALUser();
            await balUser.AddUser(obj);
            return RedirectToAction("AllData");
        }

        public async Task CountryList()
        {
            BALUser objCtry = new BALUser();
            ds = await objCtry.Countries();
            List<SelectListItem> CountryList = new List<SelectListItem>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                CountryList.Add(new SelectListItem
                {
                    Text = row["CountryName"].ToString(),
                    Value = row["CountryId"].ToString(),
                });
                ViewBag.CountryName1 = CountryList;
            }

        }


        public async Task<JsonResult> StateList(int CountryId)
        {
            User objUser = new User();
            objUser.CountryId = CountryId;

            BALUser objCtry = new BALUser();
            ds = await objCtry.States(objUser.CountryId);

            List<SelectListItem> StateList = new List<SelectListItem>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                StateList.Add(new SelectListItem
                {
                    Text = row["StateName"].ToString(),
                    Value = row["StateId"].ToString(),
                });
                ViewBag.CountryName = StateList;
            }
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> CityList(int StateId)
        {
            User objUser = new User();
            objUser.StateId = StateId;

            BALUser objCtry = new BALUser();
            ds = await objCtry.Cities(objUser.StateId);

            List<SelectListItem> CityList = new List<SelectListItem>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                CityList.Add(new SelectListItem
                {
                    Text = row["CityName"].ToString(),
                    Value = row["CityId"].ToString(),
                });
                ViewBag.CityName1 = CityList;
            }
            return Json(CityList, JsonRequestBehavior.AllowGet);

        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost, ActionName("Delete")] // Use ActionName to map to Delete
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            BALUser user = new BALUser();
            await user.DeleteUser(id); // Delete the user by ID
            return RedirectToAction("AllData"); // Redirect after deletion
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            BALUser balUser = new BALUser();
            User user =await balUser.GetUserById(id.Value); // Get the user to delete

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user); // Confirm deletion
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {

            BALUser balUser = new BALUser();
            User user =await balUser.GetUserById(id.Value); // Get the user to delete

            if (user == null)
            {
                return HttpNotFound();
            }

            var userViewModel = new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Gender = user.Gender,
                Address = user.Address,
                CountryId = user.CountryId,
                CountryName = user.CountryName,
                StateId = user.StateId,
                StateName = user.StateName,
                CityId = user.CityId,
                CityName = user.CityName,

            };
            await CountryList();
            ViewBag.States =await StateList(user.CountryId);
            ViewBag.CityName =await CityList(user.StateId);


            //CountryList();
            //ViewBag.CityName = new List<SelectListItem>();
            //ViewBag.StateName = new List<SelectListItem>();


            return PartialView("Edit", user);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User obj)
        {
            if (ModelState.IsValid)
            {
                BALUser editU = new BALUser();
                await editU.EditUser(obj);
                return RedirectToAction("AllData");
            }

            return View(obj);
        }

        [HttpGet]
        public async Task<ActionResult> UserDetails(int? id)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            BALUser balUser = new BALUser();
            //User user = balUser.GetUserById(id.Value);

            //if (user == null)
            //{
            //    return PartialView("_Error");
            //}

            //var userViewModel = new User
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Gender = user.Gender,
            //    Address = user.Address,            
            //    CountryName = user.CountryName,              
            //    StateName = user.StateName,              
            //    CityName = user.CityName,
            //};
            //return PartialView("UserDetails", userViewModel);

            var users = await balUser.GetAllUsers(); // Await the asynchronous method
            var user = users.FirstOrDefault(model => model.UserId == id);
            return PartialView("UserDetails", user);
        }
    }
}
