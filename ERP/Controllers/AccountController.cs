using BLogic;
using BObject;
using ERP.CustomAuthentication;
using ERP.DataAccess;
using ERP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ERP.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            LogOut();
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            //restart code Uttaran

            UserWiseCompany uwc = new UserWiseCompany();
            uwc.UserId = 0;
            var Companylist = service.GetUserWiseCompanyList<UserWiseCompany>(uwc, "usp_UserWiseCompany");
            ViewBag.CM_ID = new SelectList(Companylist, "CM_ID", "name");
            //end
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginView, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.Email, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.Email, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList()
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.Email, DateTime.Now, DateTime.Now.AddMinutes(5), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);

                        if (user.UserId != 0)
                        {
                            Service service = new Service();
                            UserWiseCompany uwc = new UserWiseCompany();
                            uwc.Email = loginView.Email;
                            uwc.Password = loginView.Password;
                            List<UserWiseCompany> lstCompany = new List<UserWiseCompany>();
                            var role = service.GetGlobalSelectOne<UserRoleDetails>("UserRoles", "UserId", user.UserId);
                            var Companylist = service.GetUserWiseCompanyList<UserWiseCompany>(uwc, "usp_UserWiseCompany");
                            //ViewBag.CM_ID = new SelectList(Companylist, "CM_ID", "name");
                            lstCompany = Companylist;
                            UserRoleDetails users = new UserRoleDetails();
                            users.LstCompany = Companylist;
                            if (role != null)
                            {
                                users.RoleId = role.RoleId;
                                users.RoleId = role.RoleId;
                                users.CM_ID = Convert.ToInt64(fc["CM_ID"]);

                            }
                            else
                            {
                                users.RoleId = 0;
                            }
                            users.UserId = user.UserId;
                            users.FirstName = user.FirstName;
                            users.LastName = user.LastName;
                            System.Web.HttpContext.Current.Session["UserModel"] = users;
                        }
                    }
                    string url = fc["ReturnUrl"];
                    if (Url.IsLocalUrl(url))
                    {
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
            return View(loginView);
        }
        public ActionResult PasswordOnChngCompanyList(UserWiseCompany uwc)
        {
            try
            {
                var Companylist = service.GetUserWiseCompanyList<UserWiseCompany>(uwc, "usp_UserWiseCompany");
                return Json(Companylist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
            var Companylist = "";
            return Json(Companylist, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;
            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(registrationView);
                }
                //Save User Data
                using (AuthenticationDB dbContext = new AuthenticationDB())
                {
                    registrationView.ActivationCode = Guid.NewGuid();
                    var user = new User()
                    {
                        Username = registrationView.Username,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = registrationView.Password,
                        ActivationCode = registrationView.ActivationCode,
                    };
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
                //Verification Email
                VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^ Please Check Your Email & Active The Account.";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }
        [HttpGet]
        public ActionResult ActivationAccount(string id)
        {
            bool statusAccount = false;
            using (AuthenticationDB dbContext = new DataAccess.AuthenticationDB())
            {
                var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

                if (userAccount != null)
                {
                    userAccount.IsActive = true;
                    dbContext.SaveChanges();
                    statusAccount = true;
                    var role = dbContext.Roles.Where(r => r.RegNewUser.Equals(true)).Select(x => x.RoleId).FirstOrDefault();
                    UserRole TEntity = new UserRole();
                    TEntity.UserId = userAccount.UserId;
                    TEntity.RoleId = role;
                    TEntity.ProjectId = 1;
                    TEntity.OppType = "Insert";
                    var result = service.InsUpUserRole(TEntity, "usp_UserRole");
                }
                else
                {
                    ViewBag.Message = "Something Wrong !!";
                }

            }
            ViewBag.Status = statusAccount;
            return View();
        }
        public ActionResult LogOut()
        {
            
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddMinutes(-5);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut(); 
            Session["UserModel"] = null;
            return RedirectToAction("Login", "Account", null);
        }
        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            string subject = "Activation Account !";
            string body = "<br/>Your account has been created successfully.<br/> ^_^ Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";
            Utils.sendEmail(email, subject, body, false, false);
        }
    }
}