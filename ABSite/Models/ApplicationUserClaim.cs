﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABSite.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<long> { }
}