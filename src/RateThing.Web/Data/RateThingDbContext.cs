﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RateThing.Web.Data
{
    public class RateThingDbContext : IdentityDbContext
    {
        private readonly string _connectionString;

        public RateThingDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public RateThingDbContext(DbContextOptions<RateThingDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*if (_loggerFactory != null)
                {
                    optionsBuilder = optionsBuilder.UseLoggerFactory(_loggerFactory);
                }*/
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UsePostgresConventions();
        }

        //public DbSet<type> thing { get; set; }
    }
}
