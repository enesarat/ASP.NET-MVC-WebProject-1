using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineVoting.Models;

namespace onlineVoting.Controllers
{

    public class HomeController : Controller
    {
        WP_VoteIT_DBEntities1 db = new WP_VoteIT_DBEntities1();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult StartingPage()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Vote()
        {
            ViewBag.qCategory = new SelectList(db.Category, "CategoryId", "CategoryName");
            var modelVoting = db.Voting.ToList();
            return View(modelVoting);
        }
        [HttpPost]
        public ActionResult Vote(int Id, string vot_)
        {
            Voting vtg_ = db.Voting.Where(x => x.VotingID == Id).FirstOrDefault();
            
            if (vtg_.YesAmount == null && vtg_.NoAmount == null)
            {
                vtg_.YesAmount = 0;
                vtg_.NoAmount = 0;
                vtg_.quantity = 0;

            }
            Vote us = new Vote();
            
            string user_n = (string)Session["UserName"];
            var oldUser = db.Vote.Where(x =>  x.Voting_Key == Id && x.User_name==user_n).FirstOrDefault();
            if (oldUser == null)
            {

                if (vot_ == "yes")
                {
                    vtg_.YesAmount += 1;
                    vtg_.quantity += 1;
                    us.Voting_Key = Id;
                    us.User_name = (string)Session["UserName"];

                    db.Vote.Add(us);

                }
                else if(vot_ == "no")
                {
                    vtg_.NoAmount += 1;
                    vtg_.quantity += 1;
                    us.Voting_Key = Id;
                    us.User_name = (string)Session["UserName"];

                    db.Vote.Add(us);
                }
                

                db.SaveChanges();
                return RedirectToAction("Vote");
            }
            else
            {
                ViewBag.ErrMsg = "You have already voted for this voting!";
                return RedirectToAction("Vote");
            }
        }

        public ActionResult VotingbyCategories(int id)
        {
            viewModel vm_ = new viewModel();
            vm_.Votings = db.Voting.Where(m => m.VCategoryID == id).ToList(); ;
            vm_.Categories = db.Category.ToList();
            return View(vm_);

        }


        public ActionResult Results()
        {
            viewModel vm_ = new viewModel();
            vm_.Votings = db.Voting.ToList();
            vm_.Categories = db.Category.ToList();

            return View(vm_);
        }

        public ActionResult ResultsbyCategories(int id)
        {
            viewModel vm_ = new viewModel();
            vm_.Votings = db.Voting.Where(m => m.VCategoryID == id).ToList(); ;
            vm_.Categories = db.Category.ToList();
            return View(vm_);

        }
        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult StartingContact()
        {

            return View();
        }


        public ActionResult StartingLogin(string U_Username, string U_Password)
        {

            if (U_Username == null)
            {
                return View();

            }
            else
            {
                var user_model = db.Voter.FirstOrDefault(m => m.UserUsername == U_Username && m.UserPassword == U_Password);
                if (user_model != null)
                {
                    Session["UserName"] = user_model.UserUsername;
                    Session["Password"] = user_model.UserPassword;
                    if (user_model.isAdmin == true)
                    {
                        Session["Admin"] = "Admin";

                        return RedirectToAction("AdminIndex", "Admin");

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "The username or password that you've entered is incorrect.";
                    return View();
                }

            }

        }

        [HttpGet]
        public ActionResult StartingSignUp()
        {
            List<SelectListItem> countries = (from i in db.Country.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.CountryName,
                                                  Value = i.CountryID.ToString()
                                              }).ToList();
            List<SelectListItem> genders = (from i in db.Gender.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.GenderName,
                                                Value = i.GenderID.ToString()
                                            }).ToList();

            ViewBag.cntry = countries;
            ViewBag.gndr = genders;
            return View();
        }

        [HttpPost]
        public ActionResult StartingSignUp(FormCollection form)
        {
            string user = form["Username"].Trim();
            var user_model = db.Voter.FirstOrDefault(m => m.UserUsername == user);
            if (user_model == null)
            {

                Voter model = new Voter();
                model.UserName = form["Name"].Trim();
                model.UserSurname = form["Surname"].Trim();
                model.UserUsername = form["Username"].Trim();
                model.UserEmail = form["Email"].Trim();
                model.UserPassword = form["Password"].Trim();
                model.UserPassword = form["PasswordConfirm"].Trim();
                model.UserCountry = int.Parse(form["Country"].Trim());
                model.UserGender = int.Parse(form["Sex"].Trim());
                model.UserAge = int.Parse(form["Age"].Trim());
                db.Voter.Add(model);
                db.SaveChanges();

                return RedirectToAction("StartingLogin");
            }
            else
            {
                ViewBag.ErrorMessageSU = "The username that you've entered is already registered.";
                return View();
            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("StartingLogin", "Home");
        }
    }
}