using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Server {
    public static class ServerConfig {
        public static string FileServerPath =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.Replace("\\","/")+"/Content";
        public static string FileUploadPath = 
            Directory.GetCurrentDirectory().Replace("\\","/")+"/App_Data/Files";
    }
}
