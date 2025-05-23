﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        /// <summary>
        /// This EndPoint To Upload Images
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public async Task<string> UploadImages( IFormFile file)
        {

            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (file == null || file.Length == 0)
            {
                throw new Exception("Please Enter Valid File");
            }

            string newFileURL2 = Guid.NewGuid().ToString() + "" + file.FileName;
            using (var inputFile = new FileStream(Path.Combine(uploadFolder, newFileURL2), FileMode.Create))
            {
                await file.CopyToAsync(inputFile);
            }
            return newFileURL2;
        }
        [HttpPost]
        [Route("UploadMultipleImages")]
        public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                throw new Exception("Please provide at least one file");

            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            List<string> savedFileNames = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedFileNames.Add(fileName); // إضافة الصورة إلى القائمة
                }
            }

            if (savedFileNames.Count == 0)
                throw new Exception("No files were uploaded");

            // تخصيص الصورة الرئيسية (أول صورة في القائمة)
          

            return Ok(savedFileNames); // إرجاع الصورة الرئيسية والصور الإضافية
        }



    }
}



