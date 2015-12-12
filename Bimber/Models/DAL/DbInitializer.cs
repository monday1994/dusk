using Bimber.Models.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL
{
    class DbInitializer : DropCreateDatabaseIfModelChanges<BimberDbContext>
    {
        private SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Integrated Security = True; Connect Timeout = 15; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog = BimberData; Integrated Security = True");

        private BimberDbContext db = new BimberDbContext();

        private ActivitiesRepository activitiesRepo = new ActivitiesRepository();

        private GroupsRepository groupsRepo = new GroupsRepository();

        private PlacesRepository placesRepo = new PlacesRepository();

        private PreferencesRepository preferencesRepo = new PreferencesRepository();

        private UsersRepository usersRepo = new UsersRepository();

        public DbInitializer()
        {            
            conn.Close();
        }             

        protected override void Seed(BimberDbContext context)
        {
            Debug.WriteLine("jestem w db initilizer");

            conn.Open();

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

