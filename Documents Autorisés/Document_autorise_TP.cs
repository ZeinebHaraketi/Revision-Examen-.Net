//--------------------- Entity Framework Core ---------------------------------------------------//

//Chaine de 7 caractères
[StringLength(7)]
public string PassportNumber { get; set; }

//Valider Date 
[DataType(DataType.Date)] // DataType.DateTime ==> enti wou chnowa yotlob 3lik
 [DataType(DataType.DateTime , ErrorMessage ="date invalide")]
//Name of the column table
[Display(Name = "Date of birth")]
public DateTime BirthDate { get; set; }

//Valider numero tel
[RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Invalid Phone Number!")]
[Range(0, 8)] //Ne depasse pas 8 chiffres
public int? TelNumber { get; set; }
//Number entre 1 et 4
 [Range(1,4)]    
public int NbPersonnes { get; set; }

//Valider Email
[DataType(DataType.EmailAddress)]
public string? EmailAddress { get; set; }

//Valider Salaire (float ou double)
 [DataType(DataType.Currency)]
 public float Salary { get; set; }
//Les Lists (non Ordonnées)
public virtual IList<Flight> Flights { get; set; }

//Les Lists (Ordonnées)
public virtual IEnumerable<Flight> Flights { get; set; }

// Class Enumeration 
 public enum PlaneType
    {
        Boing,
        Airbus
    }
 ///////La propriété Password doit être obligatoire et la valeur saisie par l’utilisateur doit être cachée.
 [Required]
 [DataType(DataType.Password)]
 public string Password { get; set; }   
/////La propriété MotifAdmission doit être Multiligne et affiché «Le motif de l’admission »    
[Display(Name = "Le motif de l'admission")]
[DataType(DataType.MultilineText)]
///////le propriete CIN represente un numero de carte identite nationale valide compose de huit chiffres 
[RegularExpression(@"^\d{8}$", ErrorMessage = "Le CIN doit être composé de 8 chiffres.")]
  public string CIN { get; set; }
//Clé primaire
[Key]
public string PassportNumber { get; set; }
//Clé etrangère
public int PlaneFK { get; set; }
[ForeignKey("PlaneFK")]
public virtual Plane Plane { get; set; }

//Relation * * 
 public virtual IList<Fournisseur> Fournisseurs { get; set; }
 public virtual IList<Produit> Produits { get; set; }
 //Relation 1 *  categorie 1 * Produit
  /*dans la classe Produit*/:public virtual Categorie CategorieCategorie { get; set; } 
 /*dans la classe Categorie*/:  public virtual IList<Produit> Produits { get; set; }
//Table Porteuse 
///////dans la classe porteuse on met 
public virtual Compte Compte { get; set; }
public virtual DAB DAB { get; set; }
public string CompteFk { get; set; }
public string DabFK { get; set; }
avec les attributs donne dans l examen
//////dans les deux autres classes 
public virtual IList<Transaction> Transactions { get; set; }

////////Créer le type détenu « NomComplet » qui représente les propriétés Nom et Prénom de la classe Patient
////on va creer une autre classe Nom complet
///1ere methode on ajoute l aanotation owned foug l classe sinon ykharej erreur elli classe ma3endech cle primaire  
[Owned] 
public class NomComplet
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
///et dans la classe patient on remplace les deux proprietes nom et prenom par cette classe
 public NomComplet NomComplet { get; set; }

//2eme methode par fluent api 

//---------- Génération de la base ----------------//

1) AMContext: DbContext ===> qui va implémenter la config de base
2) Installation du package ORM: Entity Framework Core 7.0.3
3) DbSet <Entity> Tables 
4) chaine de connexion ==> onConfiguring (fel AMContext)
5) avant de lancer 2 commandes modifier to examen.infrasturtuce:
   ** 1/ Add-migration nom_migration  (enregistrée modif apportée au code)
   ** 2/ update-database (mettre à jour DB)
6) relations: ** ==> config par l'ORM (automatique) ==> table associative (nom)
             1* ==> config par l'ORM ==> FK(nom)
            Heritage ==> TPH ==> discriminator (string)
                     ==> TPT (table per type)
            Porteuse:


//********************Heritage*********************************//
public class Staff : Passenger{
		public string Function { get; set; }
        public DateTime EmployementDate { get; set; }

	}
	
//Exam.Infrastructure (AM.Infrastructure)
//Configuration
public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {

            // configuration de * * association, renommer la table d'association
            builder.HasMany(f => f.Passengers)
            .WithMany(p => p.Flights)
            .UsingEntity(j => j.ToTable("ReservationFlight"));
			
			  //configuration de one to many 
            builder.HasOne(f => f.Plane)
              .WithMany(p => p.Flights)
              // 2 eme methode 
             .HasForeignKey(f => f.PlaneFK)
              .OnDelete(DeleteBehavior.Cascade); //The values of foreign key properties in dependent entities are set to null when the related principal is deleted
		}
	}
	
	//configuration de la table qui a un [key] et qui a une property capacity 
	 public class PlaneConfiguration : IEntityTypeConfiguration<Plane>
    {
        public void Configure(EntityTypeBuilder<Plane> builder)
        {
            builder.HasKey(p => p.PlaneId);
            builder.ToTable("MyPlanes");
            builder.Property(p => p.Capacity).HasColumnName("PlaneCapacity");  
        }
    }
	
	//Clé primaire Composée
	 public class TicketReservationConfiguration : IEntityTypeConfiguration<ReservationTicket>
    {
        public void Configure(EntityTypeBuilder<ReservationTicket> builder)
        {
           
            builder.HasKey(t => new
            {
                t.PassengerFk,t.TicketFk,t.DateReservation
            }
            );
        }
    }
    //TablePorteuse Fluent API
     {
            //3-a
            // config de 1-* compte et transaction

            builder.HasOne(p => p.Compte)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.CompteFk)
                .OnDelete(DeleteBehavior.Restrict);

            //config de 1 * DAB et Transaction


            builder.HasOne(p => p.DAB)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(p => p.DabFK)
                    .OnDelete(DeleteBehavior.Restrict);
            /////classe porteuse has un cle primaire compose 
            builder.HasKey(p => new
            {

                p.DabFK,
                p.CompteFk,
                p.Date
            });         
     }               
	
	//AMContext (ExamContext)
	//Les DbSets
	 public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ReservationTicket> ReservationTickets { get; set; }
		
	///dans cette methode on doit mettre les configurations elli aamelnehom
	 protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.ApplyConfiguration(new PlaneConfiguration());

            // 2 ème méthode // configuration du type complexe : fullName
            modelBuilder.Entity<Passenger>().OwnsOne(p => p.FullName, full =>
            {
                full.Property(f => f.FirstName).HasColumnName("PassFirstName").HasMaxLength(30);
                full.Property(f => f.LastName).HasColumnName("PassLastName").IsRequired();
            });
			
			//TPH avec des entiers ou avec des string 
              //entiers
			   modelBuilder.Entity<Passenger>().HasDiscriminator<int>("IsTraveller")
               .HasValue<Passenger>(0)
              .HasValue<Traveller>(2)
              .HasValue<Staff>(1);
              //string 
               modelBuilder.Entity<Produit>()
                .HasDiscriminator<string>("TypeProduit")
                .HasValue<Chimique>("c")
                .HasValue<Biologique>("b")
                .HasValue<Produit>("P");
			  
			    //TPT
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Traveller>().ToTable("Travellers");
            // relation 1 * clinique chambre avec foreign key 
             modelBuilder.Entity<Chambre>()
                .HasOne(f => f.Clinique)
                .WithMany(p => p.Chambres)
                .HasForeignKey(p => p.CliniqueFK)
                .OnDelete(DeleteBehavior.Restrict);

		}
		//touutes les propriets de type  date has column name date 
		 protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
             .Properties<DateTime>()
                 .HaveColumnType("date");
        }
	
//------------------------------------- SERVICES --------------------------------------------------//
//Les Interfaces
    public interface IServiceFlight :IService<Flight> {
	//les fonctions 	
	}
	
//Les classes Services
    public class ServiceFlight : Service<Flight>, IServiceFlight {
		
		//Get Flight who has the same destination by FlightDate
		  public List<DateTime> GetFlightDates(string destination)
        {
            List<DateTime> ls = new List<DateTime>();
           
            var query = from f in Flights
                      where
                      f.Destination.Equals(destination)
                      select f.FlightDate;
            return query.ToList();

            //with Lambda expressions
            // IEnumerable<DateTime> reqLambda = Flights.Where(f => f.Destination.Equals(destination)).Select(f => f.FlightDate);
        }
		
		 public void GetFlights(string filterType, string filterValue)
        {
            switch (filterType)
            {
                case "Destination":
                    foreach (Flight f in Flights)
                    {
                        if (f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "FlightDate":
                    foreach (Flight f in Flights)
                    {
                        if (f.FlightDate == DateTime.Parse(filterValue))

                            Console.WriteLine(f);

                    }
                    break;
                case "EffectiveArrival":
                    foreach (Flight f in Flights)
                    {
                        if (f.EffectiveArrival == DateTime.Parse(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
            }

        }

     //Details Flight
      public void ShowFlightDetails(Plane plane)
        {
            var req = from f in Flights
                      where f.Plane == plane
                      select new { f.FlightDate, f.Destination };
            //  var reqLambda = Flights.Where(f => f.Plane == plane).Select(p => new { f.FlightDate, f.Destination });
            foreach (var v in req)
                Console.WriteLine("Flight Date; " + v.FlightDate + " Flight destination: " + v.Destination);
        }
		
		//ProgrammedFlightNumber in a week
        public int ProgrammedFlightNumber(DateTime startDate)
        {
           return Flights.Count(f => DateTime.Compare(f.FlightDate, startDate) > 0 && (f.FlightDate - startDate).TotalDays < 7);
        }
		
		//Durée moyenne
		 public double DurationAverage(string destination)
        {
            return (from f in Flights
                    where f.Destination.Equals(destination)
                    select f.EstimatedDuration).Average();
        }
		
		//Flight Trie (Sort)
		 public IEnumerable<Flight> OrderedDurationFlights()
        {
            var req = from f in Flights
                      orderby f.EstimatedDuration descending
            select f;

            return req;
            //lambda expression
          //  return Flights.OrderByDescending(f => f.EstimatedDuration);

        }
		
		//Les 3 travellers les plus agés
		 public IEnumerable<Traveller> SeniorTravellers(Flight f)
        {

            var oldTravellers = from p in f.Passengers.OfType<Traveller>()
                                orderby p.BirthDate
                                select p;

            var reqLambda = f.Passengers.OfType<Traveller>().OrderBy(p => p.BirthDate).Take(3);
            return oldTravellers.Take(3);
            //if we want to skip 3
            //return oldTravellers.Skip(3);
        }
		
		//Grouped By
		  public IGrouping<string, IEnumerable<Flight>> DestinationGroupedFlights()
        {
            var req = from f in Flights
                      group f by f.Destination;

            //  var reqLambda = Flights.GroupBy(f => f.Destination);

            foreach (var g in req)
            {
                Console.WriteLine("Destination: " + g.Key);
                foreach (var f in g)
                    Console.WriteLine("Décollage: " + f.FlightDate);

            }
            return (IGrouping<string, IEnumerable<Flight>>)req;
        }
		
		 //pour tester la méthode DestinationGroupedFlights
        public void diplay()
        {
            var result =
                (from f in Flights
                 group f by f.Destination);

            foreach (var destination in result)
            {
                Console.WriteLine(" destination =  " + destination.Key); // key = city
                foreach (var flight in destination)
                {
                    Console.WriteLine("Flights Details" + flight.FlightDate);

                }
            }

        }
		
		//PlaneService
		public void DeletePlanes()
        {
            Delete(p => (DateTime.Now - p.ManufactureDate).TotalDays > 365 * 10);
        }

        public IEnumerable<Passenger> GetPassengers(Plane plane)
        {
            return plane.Flights.SelectMany(p => p.Passengers);
        }

        public bool IsAvailablePlane(Flight flight, int n)
        {
          return flight.Plane.Capacity>= flight.Passengers.Count()+n;
        }

        public IEnumerable<Flight> GetFlights(int n)
        {
            return GetAll().SelectMany(p => p.Flights).OrderByDescending(p => p.FlightDate).Take(n);

        }

        //Retourner le pourcentage des chambres simples d’une clinique passée en paramètre.
        //1ere methode 
         double PourcentageChambre(Clinique c);
		return GetMany(p=>p.CliniqueFK==c.CliniqueId && p.TypeChambre==TypeChambre.Simple).Count()/GetMany(p=>p.CliniqueFK.Equals(c.CliniqueId)).Count()*100;
         //2eme methode 
        return c.Chambres.Where(p=>p.TypeChambre==TypeChambre.Simple).Count()/c.Chambres.Count()*100;
        //Retourner les noms complets des patients ayant occupé une chambre donnée à partir d’une date donnée
         public IEnumerable<NomComplet>Occupants(DateTime date , Chambre c);
       return c.Admissions.Where(p =>DateTime.Compare(p.DateAdmission, date) > 0).Select(p => p.Patient.NomComplet);
        /////Retourner la recette d’une clinique donnée pendant une année passée en paramètre
       public double RecetteClinique(Clinique c ,int annee)
        {
            return GetMany(p => p.DateAdmission.Year == annee && p.Chambre.CliniqueFK == c.CliniqueId).Sum(p => p.Chambre.Prix * p.NBJours);

        }

    

	}
//-----------------------------------------WEB-----------------------------------------------------//   
////1//creer un controller 
////
    public class ProduitController : Controller
    {
        IProduitService ps;
        ICategorieService cs;

        public ProduitController(IProduitService ps, ICategorieService cs)
        {
            this.ps = ps;
            this.cs = cs;
        }
     } 
/////view 
     public ActionResult Index()
        {
            return View(sf.GetAll());
        }
//// Methode create 
 public ActionResult Create(Produit produit)
        {
            try
            {
                ps.Add(produit);
                ps.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
////liste deroulante 
///////dynamique
 <select asp-for="BanqueFk" class="form-control"
               
               asp-items="ViewBag.BanqueList"
 ></select>
 et la methode ViewBag.BanqueList implemente dans la methode get de create
  public ActionResult Create()
        {
            ViewBag.BanqueList = new SelectList(sb.GetAll(), "Code", "Nom");
            return View();
        }
////statique
        <select asp-for="Type" class="form-control"
                asp-items="Html.GetEnumSelectList<E.ApplicationCore.Domain.TypeCompte>()"
                ></select> 
////creer une view  
//*Créer la vue Index qui permet de lister les admissions ordonnées par date admission 
 public ActionResult Index()
        {
            return View(sa.GetAll().OrderByDescending(a=>a.DateAdmission));
        }	

/////afficher les details dun patient a partir d une view d'admission
1/creer un controller patient 
2/ methode details
  public ActionResult Details(int id)
        {
            return View(sp.GetById(id));
        }
3/ creer une view details de patient
4/ajouter ce code dans index.html de admission 
 <td>
@Html.ActionLink(linkText: "Details", actionName: "Details", controllerName: "Patient", routeValues: new { id = item.PatientFk }, htmlAttributes: null) |
 </td>
//////recherche 
//**par nom 
 //dans controller
 public ActionResult Index(string? nom)
        {
            if (nom == null)
            {
                return View(ps.GetAll());
            }
            return View(ps.GetMany(p=> p.Nom.Equals(nom)));
        }
///html 
<form asp-action="index">
    <fieldset>
        <legend> Recherche</legend>
        Saisir nom :
        <input type="text" name="nom" />
        <input type="submit" value="Serach" />
    </fieldset>
</form>        
//**par enumeration 
 public ActionResult Index(TypeCompte? type)
        {
            if(type==null)
            return View(serviceCompte.GetAll());
            return View(serviceCompte.GetMany(p => p.Type.Equals(type)));

        }
//html 
<form asp-action="index">
<fieldset>
<legend> Recherche</legend>
Saisir un type:         
<select  
asp-items="Html.GetEnumSelectList<E.ApplicationCore.Domain.TypeCompte>()"
    name="type" >   
</select>
<input type="submit" value="Serach" />
</fieldset>
</form>   
////modifier Banque a partir d une index compte 
1/creer un controller banque 
2/ methode edit post
  public ActionResult Edit(int id, Banque banque)
        {
            try
            {
                sb.Update(banque);
                sb.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
 edit get :
 public ActionResult Edit(int id)
        {
            return View(sb.GetById(id));
        }       
3/ creer une view edit de banque
4/ajouter ce code dans index.html de compte 
<td>
 @Html.ActionLink(linkText: "Modifier Banque", actionName: "Edit", controllerName: "Banque", routeValues: new { id = item.BanqueFk }, htmlAttributes: null) |
 </td>