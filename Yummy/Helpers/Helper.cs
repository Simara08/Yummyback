using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Yummy.Helpers
{
    public static class Helper
    {
        public static string GetPath(string root, params string[] folders)
        {
            var resultpath = root;
            foreach (var folder in folders)
            {
                resultpath = Path.Combine(resultpath, folder);
            }
            return resultpath;
        }
    }
}
