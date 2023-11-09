using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Client.Web;

public static class DatabaseInitialization
{
    public static async Task SeedDatabase(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<RoutePlanningDatabaseContext>();
        await context.Database.EnsureCreatedAsync();

        var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        await using (var unitOfWork = unitOfWorkManager.Initiate())
        {
            await SeedUsers(context);
            await SeedLocationsAndRoutes(context);

            unitOfWork.Commit();
        }
    }

    private static async Task SeedLocationsAndRoutes(RoutePlanningDatabaseContext context)
    {
        // OLD LOCATIONS:
        var berlin = new Location("Berlin");
        await context.AddAsync(berlin);

        var copenhagen = new Location("Copenhagen");
        await context.AddAsync(copenhagen);

        var paris = new Location("Paris");
        await context.AddAsync(paris);

        var warsaw = new Location("Warsaw");
        await context.AddAsync(warsaw);

        // OUR LOCATIONS:
        var tanger = new Location("TANGER");
        await context.AddAsync(tanger);

        var marrakesh = new Location("MARRAKESH");
        await context.AddAsync(marrakesh);

        var tunis = new Location("TUNIS");
        await context.AddAsync(tunis);

        var sahara = new Location("SAHARA");
        await context.AddAsync(sahara);

        var tripoli = new Location("TRIPOLI");
        await context.AddAsync(tripoli);

        var dakar = new Location("DAKAR");
        await context.AddAsync(dakar);

        var sierraleone = new Location("SIERRA LEONE");
        await context.AddAsync(sierraleone);

        var guldkysten = new Location("GULDKYSTEN");
        await context.AddAsync(guldkysten);

        var timbuktu = new Location("TIMBUKTU");
        await context.AddAsync(timbuktu);

        var slavekysten = new Location("SLAVE KYSTEN");
        await context.AddAsync(slavekysten);

        var congo = new Location("CONGO");
        await context.AddAsync(congo);

        var wadai = new Location("WADAI");
        await context.AddAsync(wadai);

        var darfur = new Location("DARFUR");
        await context.AddAsync(darfur);

        var omdurman = new Location("OMDURMAN");
        await context.AddAsync(omdurman);

        var cairo = new Location("CAIRO");
        await context.AddAsync(cairo);

        var suakin = new Location("SUAKIN");
        await context.AddAsync(suakin);

        var bahrelghazal = new Location("BAHREL GHAZAL");
        await context.AddAsync(bahrelghazal);

        var victoriasoen = new Location("VICTORIASØEN");
        await context.AddAsync(victoriasoen);

        var addisabeba = new Location("ADDIS ABEBA");
        await context.AddAsync(addisabeba);

        var kapguardafui = new Location("KAP GUARDAFUI");
        await context.AddAsync(kapguardafui);

        var zanzibar = new Location("ZANZIBAR");
        await context.AddAsync(zanzibar);

        var mocambique = new Location("MOCAMBIQUE");
        await context.AddAsync(mocambique);

        var kabalo = new Location("KABALO");
        await context.AddAsync(kabalo);

        var luanda = new Location("LUANDA");
        await context.AddAsync(luanda);

        var dragebjerget = new Location("DRAGEBJERGET");
        await context.AddAsync(dragebjerget);

        var victoriafaldene = new Location("VICTORIAFALDENE");
        await context.AddAsync(victoriafaldene);

        var hvalbugten = new Location("HVALBUGTEN");
        await context.AddAsync(hvalbugten);

        var kapstaden = new Location("KAPSTADEN");
        await context.AddAsync(kapstaden);

        // OLD CONNECTIONS:
        //CreateTwoWayConnection(berlin, warsaw, 573);
        //CreateTwoWayConnection(berlin, copenhagen, 763);
        //CreateTwoWayConnection(berlin, paris, 1054);
        //CreateTwoWayConnection(copenhagen, paris, 1362);

        // OUR CONNECTIONS:
        CreateTwoWayConnection(tanger, tunis, 5);
        CreateTwoWayConnection(tanger, sahara, 5);
        CreateTwoWayConnection(tanger, marrakesh, 2);
        CreateTwoWayConnection(tunis, tripoli, 3);
        CreateTwoWayConnection(marrakesh, dakar, 8);
        CreateTwoWayConnection(dakar, sierraleone, 4);
        CreateTwoWayConnection(sierraleone, guldkysten, 5);
        CreateTwoWayConnection(guldkysten, timbuktu, 4);
        CreateTwoWayConnection(timbuktu, slavekysten, 5);
        CreateTwoWayConnection(slavekysten, congo, 5);
        CreateTwoWayConnection(slavekysten, wadai, 7);
        CreateTwoWayConnection(wadai, darfur, 4);
        CreateTwoWayConnection(slavekysten, darfur, 7);
        CreateTwoWayConnection(wadai, congo, 6);
        CreateTwoWayConnection(darfur, congo, 6);
        CreateTwoWayConnection(sahara, darfur, 8);
        CreateTwoWayConnection(tripoli, omdurman, 6);
        CreateTwoWayConnection(omdurman, darfur, 3);
        CreateTwoWayConnection(omdurman, cairo, 4);
        CreateTwoWayConnection(darfur, suakin, 4);
        CreateTwoWayConnection(darfur, bahrelghazal, 2);
        CreateTwoWayConnection(bahrelghazal, victoriasoen, 2);
        CreateTwoWayConnection(victoriasoen, addisabeba, 3);
        CreateTwoWayConnection(addisabeba, suakin, 3);
        CreateTwoWayConnection(addisabeba, kapguardafui, 3);
        CreateTwoWayConnection(kapguardafui, zanzibar, 6);
        CreateTwoWayConnection(zanzibar, mocambique, 3);
        CreateTwoWayConnection(zanzibar, victoriasoen, 5);
        CreateTwoWayConnection(victoriasoen, mocambique, 6);
        CreateTwoWayConnection(victoriasoen, kabalo, 4);
        CreateTwoWayConnection(kabalo, luanda, 4);
        CreateTwoWayConnection(luanda, congo, 3);
        CreateTwoWayConnection(luanda, mocambique, 10);
        CreateTwoWayConnection(luanda, victoriafaldene, 11);
        CreateTwoWayConnection(luanda, dragebjerget, 11);
        CreateTwoWayConnection(mocambique, victoriafaldene, 5);
        CreateTwoWayConnection(mocambique, dragebjerget, 4);
        CreateTwoWayConnection(dragebjerget, victoriafaldene, 3);
        CreateTwoWayConnection(victoriafaldene, hvalbugten, 4);
        CreateTwoWayConnection(hvalbugten, kapstaden, 4);
    }

    private static async Task SeedUsers(RoutePlanningDatabaseContext context)
    {
        var alice = new User("alice", User.ComputePasswordHash("alice123!"));
        await context.AddAsync(alice);

        var bob = new User("bob", User.ComputePasswordHash("!CapableStudentCries25"));
        await context.AddAsync(bob);
    }

    private static void CreateTwoWayConnection(Location locationA, Location locationB, int distance)
    {
        locationA.AddConnection(locationB, distance);
        locationB.AddConnection(locationA, distance);
    }
}
