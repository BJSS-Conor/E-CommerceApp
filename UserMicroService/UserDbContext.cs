﻿using Microsoft.EntityFrameworkCore;
using UserMicroService.Models;

namespace UserMicroService
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
