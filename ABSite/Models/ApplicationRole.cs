using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABSite.Models
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
    }
}