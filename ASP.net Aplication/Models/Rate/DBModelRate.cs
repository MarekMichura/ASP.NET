﻿using ASP.net_Aplication.Extends;
using ASP.net_Aplication.Models.Identity;
using ASP.net_Aplication.Models.Image;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.net_Aplication.Models.Rate {
    public class DBModelRate {
        [Required(ErrorMessage = "Wystąpił problem")]
        public String ImageID { get; set; }

        [Required(ErrorMessage = "Musisz być zalogowany")]
        public String UserID { get; set; }

        [DefaultValue(false)]
        [Required(ErrorMessage = "Niepoprawna ocena")]
        public Boolean RateValue { get; set; }

        public DBModelAccount User;

        public DBModelImage Image;

        public static void ModelCreate(ModelBuilder builder) {
            builder.Entity<DBModelRate>()
                .HasKey(a => new { a.UserID, a.ImageID });

            foreach (DBModelRate entity in StaticData.rates) {
                builder.Entity<DBModelRate>().HasData(entity);
            }

            builder.Entity<DBModelRate>()
                .HasOne(a => a.User)
                .WithMany(a => a.Rates)
                .HasForeignKey(a => a.UserID);

            builder.Entity<DBModelRate>()
                .HasOne(a => a.Image)
                .WithMany(a => a.Rates)
                .HasForeignKey(a => a.ImageID);
        }
    }
}
