namespace Bimber.Migrations
{
    using Models;
    using Models.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bimber.Models.DAL.BimberDbContext>
    {
        private BimberDbContext db = new BimberDbContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Bimber.Models.DAL.BimberDbContext context)
        {
            Debug.WriteLine("jestem w migrations - seed method");

            User testUser = new User("Francis", 1, 1);

            Preference testUserPreferences = new Preference();

            Place testPlace = new Place("Shine", 1, 1, TypeOfPlace.CLUB);

            PhotoLink testLink = new PhotoLink("testLink.pl");

            Group testGroup = new Group("Friday");

            Activity testActivity = new Activity("Drinking", DateTime.Now);

            //adding relations for user
            testUser.Places.Add(testPlace);
            testUser.Activities.Add(testActivity);
            testUser.Groups.Add(testGroup);
            testUser.Preferences = testUserPreferences;

            //adding preferences relation
            testUserPreferences.User = testUser;
            testUserPreferences.UserId = 1;

            //adding relations for place
            testPlace.Users.Add(testUser);
            testPlace.Activities.Add(testActivity);
            testPlace.PhotoList.Add(testLink);

            //adding relation for photoLink
            testLink.Place = testPlace;

            //adding relations for activity
            testActivity.Users.Add(testUser);
            testActivity.Place = testPlace;

            //adding relation for group
            testGroup.Users.Add(testUser);

            //adding data to each entity
            db.Users.Add(testUser);
            db.Places.Add(testPlace);
            db.Groups.Add(testGroup);
            db.Activities.Add(testActivity);
            db.SaveChanges();
        }
    }
}
