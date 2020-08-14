using EddyNewHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddyNewHome.Controllers
{
    public class MemberController : Controller
    {
        EddyNewHomeEntities1 db = new EddyNewHomeEntities1();
        // GET: Member
        public ActionResult Entry()     //주소창이랑 매핑
        {
            Members member = new Members();
            return View(member);
        }

        [HttpPost]
        public ActionResult Entry(Members member)
        {
            member.EntryDate = DateTime.Now;
            try
            {
                db.Members.Add(member);
                db.SaveChanges();
                ViewBag.Result = "OK";
            }
            catch (Exception ex)
            {
                ViewBag.Result = "FAIL";
                //ViewBag.Result = $"{ex.Message}";
            }
            return View(member);
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<Members> list = db.Members.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Edit(string memberid)
        {
            Members member = db.Members.Where(m => m.MemberID == memberid).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Members member)
        {
            //Members dbMember = db.Members.Where(m => m.MemberID == member.MemberID).FirstOrDefault();
            Members origin = db.Members.Find(member.MemberID);

            try
            {
                origin.MemberName = member.MemberName;
                origin.MemberPWD = member.MemberPWD;
                origin.Email = member.Email;
                origin.Telephone = member.Telephone;

                db.Entry(origin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                ViewBag.Result = "OK";
            }
            catch (Exception)
            {
                ViewBag.Result = "FAIL";
            }

            return View(origin);
        }

        [HttpGet]
        public ActionResult Delete(string memberid)     //멤버아이디 받아서 삭제
        {
            Members member = db.Members.Find(memberid);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        /// <summary>
        /// ajax 중복 체크 메소드
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public JsonResult IDCheck(string memberid)  //중복 아이디 있는지 확인
        {
            string result = string.Empty;
            Members member = db.Members.Find(memberid);
            if (member == null)
                result = "OK";
            else
                result = "FAIL";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}