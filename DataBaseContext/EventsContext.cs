﻿using RestfulApiVisualCode.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulApiVisualCode.DataBaseContext
{
    public class EventsContext : DbContext
    {
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Image> Images => Set<Image>();
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<Event>().HasData
            //        (
            //            new Event { 
            //                        EventId = 1,
            //                        Nameofdevice = "SSL", 
            //                        Nameofasb = 1, 
            //                        Dateofevent = DateTime.Now.Date.ToShortDateString(),
            //                        Isserios = "серьезно", Discribeevent = "ваще коллапс", 
            //                        Fixevent = "все пофиксили", EventCreator="Admin",
            //                        tags = $"{DateTime.Now.Date.ToShortDateString()},ваще коллапс,Admin,все пофиксили,серьезно,1, SSL" 
            //            },
            //            new Event { 
            //                        EventId = 2, 
            //                        Nameofdevice = "Karrera", 
            //                        Nameofasb = 2, 
            //                        Dateofevent = DateTime.Now.Date.ToShortDateString(), 
            //                        Isserios = "не серьезно", 
            //                        Discribeevent = "все сломалось", 
            //                        Fixevent = "это не", 
            //                        EventCreator = "Admin",
            //                        tags = $"{DateTime.Now.Date.ToShortDateString()},все сломалось,Admin,это не,не серьезно,2, Karrera"
            //            },
            //            new Event {
            //                        EventId = 3, 
            //                        Nameofdevice = "микрофон", 
            //                        Nameofasb = 7, 
            //                        Dateofevent = DateTime.Now.Date.ToShortDateString(), 
            //                        Isserios = "не серьезно", 
            //                        Discribeevent = "ваще коллапс", 
            //                        Fixevent = "все пофиксили", 
            //                        EventCreator = "Admin" 
            //            },
            //            new Event {
            //                        EventId = 4, 
            //                        Nameofdevice = "Dalet", 
            //                        Nameofasb = 5, 
            //                        Dateofevent = DateTime.Now.Date.ToShortDateString(), 
            //                        Isserios = "серьезно", 
            //                        Discribeevent = "жесть какая", 
            //                        Fixevent = "это не", 
            //                        EventCreator = "Admin",
            //                        tags = $"{DateTime.Now.Date.ToShortDateString()},жесть какая,Admin,это не,серьезн,5, Dalet"
            //            }
            //        );
            //    modelBuilder.Entity<Page>().HasData
            //        (
            //            new Page {
            //                        PageId = 1, 
            //                        Header = "Видеопульт", 
            //                        Subheader = "Эффекты, окна", 
            //                        Info = "Вот тут написано как делать окна и всякие эффекты", 
            //                        PageCreator = "Admin" 
            //            },
            //            new Page { 
            //                        PageId = 2, 
            //                        Header = "Видеопульт", 
            //                        Subheader = "Разного рода поломки", 
            //                        Info = "Вот тут сбор всяких поломок", 
            //                        PageCreator = "Admin" 
            //            },
            //            new Page { 
            //                        PageId = 3, 
            //                        Header = "Камеры, CCU и OCP", 
            //                        Subheader = "Камеры", 
            //                        Info = "Вот тут написано о великой полезности камер", 
            //                        PageCreator = "Admin" 
            //            },
            //            new Page { 
            //                        PageId = 4, 
            //                        Header = "Звуковое оборудование", 
            //                        Subheader = "Микрофоны", 
            //                        Info = "Вот здесь написано про микрофоны и их особенности", 
            //                        PageCreator = "Admin" 
            //            }
            //        );
            modelBuilder.Entity<Role>().HasData
                (
                    new Role
                    {
                        RoleId = 1,
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        RoleId = 2,
                        RoleName = "Engeneer"
                    }

                );
            modelBuilder.Entity<User>().HasData
                (
                    new User
                    {
                        UserId = 1,
                        Login = "Admin",
                        Password = "Admin",
                        RoleId = 1
                    }
                    
                    );
        }
    }
}