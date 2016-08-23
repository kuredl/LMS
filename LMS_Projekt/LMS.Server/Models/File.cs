using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_Server.Models
{
    public class FileItem
    {
        [Key]
        public int ID       { get; set; }
        [JsonIgnore]
        public string Path  { get; set; }
        public string Name  { get; set; }
        public string FileExtension { get; set; }

        //Todo: add cryptation later
        public string Key { get; set; }

        public virtual List<ApplicationUser> Owners { get; set; }

        [ForeignKey("MyFolder")]
        public int? FolderKey { get; set; }
        [JsonIgnore]
        public virtual Folder MyFolder { get; set; }

    }
    public class Folder 
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }                

        public virtual List<FileItem> MyFiles { get; set; }
        public virtual List<Folder> MySubFolders { get; set; }

        public Folder() {
            MySubFolders = new List<Folder>();
            MyFiles = new List<FileItem>();
        }
    }
}