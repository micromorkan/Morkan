using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.DBModels;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Web.DBModels.Cliente> Cliente { get; set; }
        public DbSet<Web.DBModels.Servico> Servico { get; set; }
        public DbSet<Web.DBModels.Agencia> Agencia { get; set; }
    }
}
