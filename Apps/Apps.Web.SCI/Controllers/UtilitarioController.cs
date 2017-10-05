using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Apps.Web.SCI.Models;

namespace Apps.Web.SCI.Controllers
{
    public class UtilitarioController : Controller
    {
        // GET: Utilitario
        public ActionResult Index()
        {
            return View();
        }        

        [HttpGet]
        public ActionResult UploadFile()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("UploadFile");
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            try
            {
                if (UploadFileClient(file1) && UploadFileClient(file2))
                {

                }
                return Json(new
                {
                    message = "Archivos cargados correctamente!!",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    message = "Error no se pudo cargar los archivos!!",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        protected bool UploadFileClient(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string folder = Server.MapPath("~/UploadedFiles");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string path = Path.Combine(folder, fileName);
                    file.SaveAs(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}