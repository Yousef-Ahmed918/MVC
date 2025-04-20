using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Services.AttachmentServices
{
    public class AttachmentService : IAttechmentService
    {
        List<string> AllowedExtensions = [ ".png", ".jpg", ".jpeg" ];
        //Megabyte=1024 Kilobyte
        //Kilobyte=1024 byte
        //2 Megabyte=2_097_152
        int maxSize = 2_097_152;

       

        public string? Upload(IFormFile file ,string folderName)
        {
            if (file is null) return null;
            //1 - Check Extension(to match the extension I want)
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension)) return null; 

            //2 - Check Size(max 2mb)
            if (file.Length >= maxSize) return null;

            //3 - Get located folder path
            //wwwroot/files/Images
            //wwwroot/files/Videos
            //wwwroot/files/Pdfs
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}"; 
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);//Better to reduce the errors 

            //4 - Make attachment name unique GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
 
            //5 - Get file path
            var filePath=Path.Combine(folderPath, fileName);

            //6 - Create file stream to copy file[unManaged]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            
            //7 - Use stream to copy file
            file.CopyTo(fileStream);

            //8 - Return fileName to store in database
            return fileName;
        }

        public bool Delete(string filePath)
        {

            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }
    }
}
