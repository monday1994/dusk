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

          //  usersRepo.Add(new User("TestUser", 1.0, 1.0));

            activitiesRepo.Add(new Activity("Rynek", DateTime.Now, TypeOfPlace.CLUB));

            groupsRepo.Add(new Group("Zyciowe"));

          //  placesRepo.Add(new Place("Prozak", 1.0, 1.0, "testPhotoLink"));               
        }
    }
}

