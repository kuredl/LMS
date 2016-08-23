using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections;
using LMS_Server.Models;
using LMS_Server.DAL;

namespace LMS_Server.Controllers {
    public class FileController : ApiController {
        //FolderRepository _folderRepo = new FolderRepository();

        [Authorize]
        public HttpResponseMessage GetTestFile() {
            HttpResponseMessage result = null;
            var localFilePath = ServerConfig.FileUploadPath+"/learning.jpg";

            if (!File.Exists(localFilePath)) {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else {
                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "item.jpg";
            }

            return result;
        }


        [Authorize]
        public IHttpActionResult GetFolders() {
            var db = new ApplicationContext();
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user == null) {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            var folders = db.Folders.First(f => f.ID == user.RootFolderID);

            if (folders != null) {
                return Ok(folders);
            } else {
                return NotFound();
            }
            
        }

        [Authorize]
        public HttpResponseMessage GetFile() {
            int id = 2;
            HttpRequestMessage request = this.Request;
            var db = new ApplicationContext();
            var user = db.Users.Find(User.Identity.GetUserId());
            var file = db.Files.Find(id);
            if (file == null){
                NotFound();
            }
            string filename = "";
                filename = file.Name + "." + file.FileExtension;
            

            HttpResponseMessage result = null;
            Console.WriteLine(ServerConfig.FileUploadPath+"/"+file.Path);
            var localFilePath = ServerConfig.FileUploadPath +"/"+ file.Path;

            if (!File.Exists(localFilePath)) {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            } else {
                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = filename;
            }

            return result;
        }
        
        [HttpPost,Authorize]
        public Task<HttpResponseMessage> PostFile() {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            

            string root = ServerConfig.FileUploadPath;
            var provider = new MultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {
                    return new HttpResponseMessage() {
                        Content = new StringContent("File uploaded.")
                    };
                }
            );
            using (var repo = new AuthRepository()) {                
                IEnumerable<string> values;
                request.Headers.TryGetValues("filename", out values);
                var filename = values.FirstOrDefault();
                var user = repo.ctx.Users.Find(User.Identity.GetUserId());
                string _name = filename.Split('.')[0];
                string _ext = filename.Split('.')[1];
                user.MyFolder.MyFiles.Add(new FileItem {
                    Name = _name,
                    FileExtension = _ext,
                    Path = provider.GetLocalFileName(request.Content.Headers)
                });
                repo.ctx.SaveChanges();
                Console.WriteLine("Saved file: " + filename + " from user: " + user.UserName);
            }
            return task;
        }
    }
}