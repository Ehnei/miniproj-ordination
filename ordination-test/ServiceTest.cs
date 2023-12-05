using shared;

namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }
    
    [TestMethod]
    public void OpretDagligFastUgyldig()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(2, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            -2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }
    
    [TestMethod]
    public void DagligSkaevDognDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
        Dosis[] doser = new[]
        {
            new Dosis(Util.CreateTimeOnly(TimeOnly.FromDateTime(DateTime.Now)), 5),
            new Dosis(Util.CreateTimeOnly(TimeOnly.FromDateTime(DateTime.Now.AddHours(3))), 4)
        };

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(9, service.GetDagligSkæve().Find(x => x.laegemiddel == lm).doser.Sum(x => x.antal));
    }
    
    [TestMethod]
    public void PNGivDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId,
            8, DateTime.Now, DateTime.Now.AddDays(3));
        Dato d = new Dato();
        d.dato = DateTime.Now.AddDays(4);
        
        Assert.AreEqual(false, service.GetPNs().Find(x => x.antalEnheder == 8).givDosis(d));
    }
    
    [TestMethod]
    public void AnbefaletDosisPerDøgn()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId,
            8, DateTime.Now, DateTime.Now.AddDays(3));
        Dato d = new Dato();
        d.dato = DateTime.Now.AddDays(4);
        
        Assert.AreEqual(false, service.GetPNs().Find(x => x.antalEnheder == 8).givDosis(d));
    }



    [TestMethod]
    public void AnbefaletDosisprDøgnLet()
    {
        Patient patient = service.GetPatienter().Find(x => x.cprnr == "123457-4321");
        Laegemiddel lm = service.GetLaegemidler().Find(l => l.navn == "Acetylsalicylsyre");

        Assert.AreEqual(7, service.GetPatienter().Count());

        service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);
        Assert.AreEqual(2.01, service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId), 0.01);

    }


    [TestMethod]
    public void AnbefaletDosisprDøgnNormal()
    {
        Patient patient = service.GetPatienter().Find(x => x.cprnr == "123456-1234");
        Laegemiddel lm = service.GetLaegemidler().Find(l => l.navn == "Paracetamol");

        Assert.AreEqual(7, service.GetPatienter().Count());

        service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);
        Assert.AreEqual(131.55, service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId), 0.01);

    }



    [TestMethod]
    public void AnbefaletDosisprDøgnTung()
    {

        Patient patient = service.GetPatienter().Find(x => x.cprnr == "123459-4301");
        Laegemiddel lm = service.GetLaegemidler().Find(l => l.navn == "Fucidin");

        Assert.AreEqual(7, service.GetPatienter().Count());

        service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);
        Assert.AreEqual(3.76, service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId), 0.01);

    }










    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}