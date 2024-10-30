using MCC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MCC.Data
{
    public class UserDBContext : IdentityDbContext<User> //poderia usar o identity user aqui mas quero ele mas customizado 
    {
        public UserDBContext(DbContextOptions<UserDBContext>opts) : base(opts)
        { 

        }

    
    }
}
