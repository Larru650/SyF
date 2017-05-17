using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Models
{
    public class SyFUser : IdentityUser
    {
        public DateTime FirstRecipe { get; set; }
    }
}
