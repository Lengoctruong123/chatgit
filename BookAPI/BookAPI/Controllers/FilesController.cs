using BookAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnviroment;
        public FilesController(IWebHostEnvironment webHostEnviromen)
        {
            _webHostEnviroment = webHostEnviromen;
        }
        [HttpGet]
        public string Get(string a)
        {
            return a;
        }
        [HttpPost]
        public string FilePost([FromForm] Image objFile)
        {
            if (objFile.files.Length > 0) 
            {
                if (!System.IO.Directory.Exists(_webHostEnviroment.WebRootPath + "\\uploads\\"))
                {
                    Directory.CreateDirectory(_webHostEnviroment.WebRootPath + "\\uploads\\");
                }

                using (FileStream fileStream = System.IO.File.Create(_webHostEnviroment.WebRootPath + "\\uploads\\" + objFile.files.FileName))
                {
                    objFile.files.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\upload\\" + objFile.files.FileName;
                }


            }
            else
            {
                return "Fail";
            }
        }


    }
}
