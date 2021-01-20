using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineVoting.Models;


namespace onlineVoting.Controllers
{
    public class AdminController : Controller
    {
        WP_VoteIT_DBEntities1 db = new WP_VoteIT_DBEntities1();
        public ActionResult AdminIndex()
        {
            return View();
        }

        /*------------------------------------------------------USER START-----*/

        public ActionResult Users()
        {
            var queryUsers = db.Voter.ToList();
            return View(queryUsers);
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.qCountry = new SelectList(db.Country, "CountryId", "CountryName");
            ViewBag.qGender = new SelectList(db.Gender, "GenderId", "GenderName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Voter user, string isAdmin)
        {
            Voter user_ =new Voter();
            user_.UserName = user.UserName;
            user_.UserSurname = user.UserSurname;
            user_.UserUsername = user.UserUsername;
            user_.UserEmail= user.UserEmail;
            user_.UserPassword = user.UserPassword;
            user_.UserCountry= user.UserCountry;
            user_.UserGender= user.UserGender;
            user_.UserAge= user.UserAge;
            if (isAdmin == "yes")
            {
                user_.isAdmin = true;
            }
            else
            {
                user_.isAdmin = false;
            }

            db.Voter.Add(user_);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult EditUser(int id)
        {
            var user = db.Voter.Find(id);
            ViewBag.qCountry = new SelectList(db.Country, "CountryId", "CountryName");
            ViewBag.qGender = new SelectList(db.Gender, "GenderId", "GenderName");

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(Voter user, string isAdmin)
        {
            Voter user_ = db.Voter.Where(x => x.UserID== user.UserID).FirstOrDefault();
            user_.UserName = user.UserName;
            user_.UserSurname = user.UserSurname;
            user_.UserUsername = user.UserUsername;
            user_.UserEmail = user.UserEmail;
            user_.UserPassword = user.UserPassword;
            user_.UserCountry = user.UserCountry;
            user_.UserGender = user.UserGender;
            user_.UserAge = user.UserAge;
            if (isAdmin == "yes")
            {
                user_.isAdmin = true;
            }
            else
            {
                user_.isAdmin = false;
            }

            db.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            var user_ = db.Voter.Find(id);
            return View(user_);
        }

        [HttpPost]
        public ActionResult DeleteUser(Voter del)
        {
            var d_user = db.Voter.Find(del.UserID);
            db.Voter.Remove(d_user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        /*------------------------------------------------------USER END-----*/

        /*------------------------------------------------------QUESTION/VOTING START-----*/

        public ActionResult Votings()
        {
            var queryVotings = db.Voting.ToList();
            return View(queryVotings);
        }

        [HttpGet]
        public ActionResult CreateVoting()
        {
            ViewBag.qCategory= new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateVoting(Voting voting)
        {

            Voting voting_ = new Voting();
            voting_.VotingName= voting.VotingName;
            voting_.QuestionLine = voting.QuestionLine;
            voting_.VCategoryID = voting.VCategoryID;
            voting_.CreateDate = DateTime.Now;
            

            db.Voting.Add(voting_);
            db.SaveChanges();
            return RedirectToAction("Votings");
        }
        
        public ActionResult EditVoting(int id)
        {
            var votings = db.Voting.Find(id);
            ViewBag.qCategory= new SelectList(db.Category, "CategoryId", "CategoryName");

            return View(votings);
        }
        
        [HttpPost]
        public ActionResult EditVoting(Voting voting)
        {
            Voting voting_ = db.Voting.Where(x => x.VotingID == voting.VotingID).FirstOrDefault();
            voting_.VotingName = voting.VotingName;
            voting_.QuestionLine = voting.QuestionLine;
            voting_.VCategoryID = voting.VCategoryID;
            voting_.EditDate = DateTime.Now;



            db.SaveChanges();
            return RedirectToAction("Votings");
        }
        
        [HttpGet]
        public ActionResult DeleteVoting(int id)
        {
            var voting_ = db.Voting.Find(id);
            return View(voting_);
        }

        [HttpPost]
        public ActionResult DeleteVoting(Voting voting_)
        {
            var d_voting= db.Voting.Find(voting_.VotingID);
            db.Voting.Remove(d_voting);
            db.SaveChanges();
            return RedirectToAction("Votings");
        }



        /*------------------------------------------------------QUESTION/VOTING END-----*/


        /*------------------------------------------------------CATEGORIES START-----
        /**/
        public ActionResult Categories()
        {
            var queryCategories= db.Category.ToList();
            return View(queryCategories);
        }
        
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {

            Category category_ = new Category();
            category_.CategoryName = category.CategoryName;
            category_.CreateDate = DateTime.Now;


            db.Category.Add(category_);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        
        public ActionResult EditCategory(int id)
        {
            var category = db.Category.Find(id);

            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category category_ = db.Category.Where(x => x.CategoryID == category.CategoryID).FirstOrDefault();
            category_.CategoryName = category.CategoryName;
            category_.EditDate = DateTime.Now;



            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var category_ = db.Category.Find(id);
            return View(category_);
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category category_)
        {
            var d_category = db.Category.Find(category_.CategoryID);
            db.Category.Remove(d_category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }

        public ActionResult VotingStatus()
        {
            var qVotings = db.Voting.ToList();
            return View(qVotings);
        }

        /*------------------------------------------------------CATEGORIES END-----*/

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("StartingLogin", "Home");
        }
    }
}