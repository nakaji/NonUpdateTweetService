﻿using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuts.Entity.Migrations;

namespace Nuts.Entity.Test
{
    [TestClass]
    public class AppDbContextTest
    {
        [TestMethod]
        public void データベースを初期化する()
        {
            // Arrange
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
            var db = new AppDbContext();

            // Act
            var result = db.Users.ToListAsync().Result;
        }

        [TestMethod]
        public void マイグレーションするように初期化する()
        {
            // Arrange

            // Act
            DatabaseInitializer.MigrateDatabaseToLatestVersion();
            var db = new AppDbContext();

            // Assert
            var result = db.Users.ToListAsync().Result;

        }
    }
}
