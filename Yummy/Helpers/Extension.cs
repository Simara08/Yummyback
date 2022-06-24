using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Yummy.Helpers
{
    public static class Extension
    {
        public static bool CheckFileSize(this IFormFile file,int kb)
        {
            return file.Length / 1024 <= 200;
        }
        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
        public async static Task<string> SaveFileAsync(this IFormFile file,string root,params string[] folder)
        {
            var filename = Guid.NewGuid().ToString() + file.FileName;
            var resultpath = Path.Combine(Helper.GetPath(root, folder), filename);
            using(FileStream fileStream=new FileStream(resultpath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

                return filename;
        }
    }
}
