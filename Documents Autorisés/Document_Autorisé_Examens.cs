//------------------------------ Examen Produit/ALTERNANCE --------------------------------------------------//

//-------------------------- Entity Framework Core -------------------------------------------------//
public  class Produit
    {
        [DataType(DataType.DateTime, ErrorMessage = "Date invalid")]
        public DateTime DateProd { get; set; }

        public string Destination { get; set; }

        public string Nom { get; set; }

        public double Price { get; set; }

        public int ProduitId { get; set; }

        public virtual IList<Fournisseur> Fournisseurs { get; set; }

        public int CategorieFK { get; set; }

        [ForeignKey("CategorieFK")]
        public virtual Categorie Categorie { get; set; }

    }
	 public  class Fournisseur
    {
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        [Key]
        public int Identifiant { get; set; }
        public bool isApproved { get; set; }

        [MaxLength(12)]
        [MinLength(3)]
        public string Nom { get; set; }

        public virtual IList<Produit> Produits { get; set;}

    }
	  public class Categorie
    {
        public int Id { get; set; } 
        public string Nom { get; set; }

        public virtual IList<Produit> ProduitList { get; set;}
    }
	public  class Biologique: Produit
    {
        public string Composition { get; set; }

    }
	 public class Chimique: Produit
    {
        public string NomLab { get; set; }
        public string Ville { get; set; }

    }
	
	//Configuration 
	//les dbsets
        public DbSet<Exemple> Exemples { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }

        public DbSet<Chimique> Chimiques { get; set; }

        public DbSet<Biologique> Biologiques { get; set; }

        public DbSet<Categorie> Categories { get; set; }
		
		//Discriminator
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExempleConfiguration());
            modelBuilder.ApplyConfiguration(new ProduitConfig());

            //...................
            //tpt 
            //tph => config
            /* modelBuilder.Entity<Produit>()
                 .HasMany(p => p.Fournisseurs)
                 .WithMany(p => p.Produits)
                 .UsingEntity(p => p.ToTable("Facture"));*/

/*
Configurer la colonne qui représente le Discriminator de la hiérarchie d'héritage par
défaut (TPH) entre Produit, Chimique et Biologique afin qu’elle soit nommée
TypeProduit de type char et qui prend la valeur C si le type du Produit est Chimique,
la valeur B si le type de Produit est Biologique et la valeur P sinon.
*/
            modelBuilder.Entity<Produit>()
                .HasDiscriminator<string>("TypeProduit")
                .HasValue<Chimique>("C")
                .HasValue<Biologique>("B")
                .HasValue<Produit>("P");
        }
		
		/*
		Implémenter une pré-convention qui mappe toutes les propriétés de type String dans des
colonnes de longueur maximale 50 caractères.
		*/
		
		//50 Caractères
		 protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(50);
        }

//Produit Configuration
public class ProduitConfig : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
			/*
			Configurer la relation Many To Many entre Fournisseur et Produit en précisant
Facture comme étant le nom de la table associative.
			*/
            // ManyToMany Configuration
            builder.HasMany(p => p.Fournisseurs)
                .WithMany(f => f.Produits)
                .UsingEntity(e => e.ToTable("Facture"));
        }
    }
	
//---------------------------------- Services ------------------------------------------------//
/*
une méthode qui permet de retourner les fournisseurs d’une catégorie passée en
paramètre.(1pt)
*/
//Get Fournisseur By Categorie
 public List<Fournisseur> getFournissseursCategorie(Categorie categorie)
        {
            List<Produit> lp = new List<Produit>();

            List<Fournisseur> lf = lp.Where(p => p.Categorie == categorie)
                .SelectMany(f => f.Fournisseurs) 
                .Distinct()
                .ToList();  
                

         return lf;

        }
	/*
	Une méthode qui permet de retourner la moyenne des prix des produits biologiques d’une
catégorie donnée.
	*/
   //retourner la moyenne des prix des produits biologiques d’une catégorie donnée.
 public double getMoyPrixProduit(Categorie categorie)
        {


            //return GetAll().OfType<Biologique>().Where(p => p.Categorie.Equals(categorie))
            //    .Average(p => p.Price);

            return GetMany(p => p.Categorie.Equals(categorie)).OfType<Biologique>()
                .Average(p => p.Price);
        }

/*
	
	Une méthode qui permet de retourner les cinq premiers produits chimiques qui ont un prix
supérieur au prix donné.*/
        public IEnumerable<Chimique> getProduitPrices()
        {

            return GetAll().OfType<Chimique>()
                .OrderByDescending(c => c.Price).Take(5);    
                

        }
		
//------------------------------------- Partie WEB ----------------------------------------------//
 public class ProduitController : Controller
    {
       

        IProduitService ps { get; set; }
        ICategorieService cs { get; set; }


        public ProduitController(IProduitService ps, ICategorieService cs)
        {
            this.ps = ps;
            this.cs = cs;
        }
		
		/*
		Rediriger vers une vue Index qui liste l’ensemble des produits.
		Ajouter un bloc de recherche à la vue Index, qui permet de filtrer les produits par Nom.
		*/
        // GET: ProduitController
        public ActionResult Index(string? nom)
        {
            if (nom == null)
            {
                return View(ps.GetAll());
            }
            return View(ps.GetMany(p=> p.Nom.Equals(nom)));
        }

        // GET: ProduitController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

/*
Réaliser une vue qui permet de créer un Produit comme le montre la figure suivante, tout
en choisissant la Categorie à partir d’une liste déroulante
*/
        // GET: ProduitController/Create
        public ActionResult Create()
        {
            ViewBag.CategorieFK = new SelectList(cs.GetAll(), "Id", "Nom");
            return View();
        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: ProduitController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProduitController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
//HTML 
//SEARCH BY NOM
<form asp-action="index">
    <fieldset>
        <legend> Recherche</legend>
        Saisir nom :
        <input type="text" name="nom" />
        <input type="submit" value="Serach" />
    </fieldset>
</form>


//----------------------------------------- Examen SIM --------------------------------------------------------------//
//----------------------------------- Entity Framework Core ---------------------------------------------------//
 public class Banque
    {
        [Key]
        public int Code { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="invalid")]

        //[EmailAddress]
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public virtual IList<Compte> Comptes { get; set; }
    }
	
	 public class Compte
    {
        [Key]
        public string NumeroCompte { get; set; }
        public string Proprietaire { get; set; }
        public double Solde { get; set; }
        public TypeCompte Type { get; set; }
        public DateTime Date { get; set; }

        public int BanqueFk { get; set; }
        [ForeignKey("BanqueFk")]
        public virtual Banque Banque { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
	/*
	3. En utilisant les configurations FluentAPI :
a. Configurer la relation entre Compte, DAB et Transaction en précisant
NumeroCompteFk et DABFk comme des clés étrangères.
	*/
	
	
	public class DAB
    {

        public string DABId { get; set; }
        public string Localisation { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
	
	public class Transaction
    {
        public DateTime Date { get; set; }
        public double Montant { get; set; }
        public string CompteFk { get; set; }
        public string DabFK { get; set; }
        public virtual Compte Compte { get; set; }
        public virtual DAB DAB { get; set; }
    }
	
	 public class TransactionRetrait : Transaction
    {
        public bool AutreAgence { get; set; }
    }
	public class TransactionTransfert:Transaction
    {
        public string NumeroCompte { get; set; }
    }
	
	 public enum TypeCompte
    {
        Epargne,Courant

    }
	
//Configuration
public DbSet<Banque> Banques { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<TransactionRetrait> TransactionRetraits { get; set; }
        public DbSet<TransactionTransfert> TransactionTransferts { get; set; }
		
 protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            
            //TPT
            modelBuilder.Entity<TransactionRetrait>()
                .ToTable("TransactionRetraits");

                 modelBuilder.Entity<TransactionTransfert>()
                .ToTable("TransactionTransferts");
        }
        // appliquer une condition sur les prop de type string
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {


            configurationBuilder.Properties<string>().HaveMaxLength(50);
        }
	
/*
b. Configurer la classe Transaction comme porteuse de données entre Compte
et Banque avec une clé primaire composée de trois propriétés
NumeroCompteFk, DABFk et Date . (0.75 pt)
c. Configurer l’héritage en appliquant l’approche TPT (Table Per Type). (1pt)
*/	
//TransactionConfiguration
 public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
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

            //3-b  // Clé primaire composée (porteuse)
            builder.HasKey(p => new
            {

                p.DabFK,
                p.CompteFk,
                p.Date
            });
        }
    }
	
//-------------------------------------- Services --------------------------------------------------------//

/*
Une méthode qui permet de retourner la liste des transactions s’effectuant à un
compte Épargne à une date donnée.
*/

public IEnumerable<Transaction> GetTransactions(Compte c, DateTime startDate)
        {
            return GetMany(p => p.Compte.Type == 0 && p.Date.Equals(startDate));
        }

/*
6. Une méthode qui permet de retourner le nombre total des transactions d’un compte
passé en paramètre pendant la semaine en cours. (1.5pt)
*/
//NBR Transaction dans une semaine (7 jours) (week)
        public int nbrTransaction(Compte compte)
        {
            return compte.Transactions
                .Where(p => (DateTime.Now - p.Date).TotalDays < 7)
                .Count();
        }
		
		/*
		Une méthode qui permet de retourner le montant total des transactions de transfert
effectuées d'un dab passé en paramètre. (1pts)
		*/
		   public double SommeMontant(DAB dab)
        {
            //return GetMany(p => p.DAB.Equals(dab)).OfType<TransactionTransfert>()
            //   .Sum(p => p.Montant);

            //  return dab.Transactions.OfType<TransactionTransfert>().Sum(p => p.Montant);

            var query = from a in //GetAll().OfType<TransactionTransfert>()
                           // where a.DAB.Equals(dab)
                            dab.Transactions
                        select a.Montant;
            return query.Sum();
        }
		
	//------------------------------------- Partie WEB -------------------------------------------------//
	
	 public class CompteController : Controller
    {
        IServiceCompte serviceCompte;
        IServiceBanque serviceBanque;

        public CompteController(IServiceCompte serviceCompte, IServiceBanque serviceBanque)
        {
            this.serviceCompte = serviceCompte;
            this.serviceBanque = serviceBanque;
        }

//Rediriger vers une vue Index qui liste l’ensemble des comptes
/*
Ajouter un bloc de recherche à la vue Index, qui permet de filtrer les comptes par
Type.
*/
        // GET: CompteController
        public ActionResult Index(TypeCompte? type)
        {
            if(type==null)
            return View(serviceCompte.GetAll());
            return View(serviceCompte.GetMany(p => p.Type.Equals(type)));

        }

        // GET: CompteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

/*
Réaliser une vue qui permet de créer un Compte comme le montre la figure suivante,
tout en choisissant la Banque à partir d’une liste déroulante
*/
        // GET: CompteController/Create
        public ActionResult Create()
        {
            ViewBag.BanqueList = new SelectList(serviceBanque.GetAll(), "Code", "Nom");
            return View();
        }

        // POST: CompteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compte c)
        {
            try
            {
                serviceCompte.Add(c);
                serviceCompte.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	//BanqueController
	public class BanqueController : Controller
    {
        IServiceBanque sb;

        public BanqueController(IServiceBanque sb)
        {
            this.sb = sb;
        }

        // GET: BanqueController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BanqueController/Details/5
        public ActionResult Details(int id)
        {
            return View(sb.GetById(id));
        }

        // GET: BanqueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BanqueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

/*
Ajouter un lien "Modifier Banque” à la vue Index, qui doit diriger vers une vue
Modifier qui modifie les données relatives à chaque Banque affectée au compte.
*/
        // GET: BanqueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(sb.GetById(id));
        }

//MODIFIER
        // POST: BanqueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: BanqueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BanqueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

//INDEX Compte
@model IEnumerable<E.ApplicationCore.Domain.Compte>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>
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

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.NumeroCompte)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Proprietaire)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Solde)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Type)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Date)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.BanqueFk)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.NumeroCompte)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Proprietaire)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Solde)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Type)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Date)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Banque.Nom)
            </td>
             <td>
                @Html.ActionLink(linkText: "Modifier Banque", actionName: "Edit", controllerName: "Banque", routeValues: new { id = item.BanqueFk }, htmlAttributes: null) |
            </td>
           @* <td>
                @Html.ActionLink("Edit", "Edit", new {  id=item.BanqueFk  }) |
                @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
            </td>*@
        </tr>
}
    </tbody>
</table>

//Edit Banque
@model E.ApplicationCore.Domain.Banque

@{
    ViewData["Title"] = "Edit";
}

<h1>Edit</h1>

<h4>Banque</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Edit">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Code" class="control-label"></label>
                <input asp-for="Code" class="form-control" />
                <span asp-validation-for="Code" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Email" class="control-label"></label>
                <input asp-for="Email" class="form-control" />
                <span asp-validation-for="Email" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Nom" class="control-label"></label>
                <input asp-for="Nom" class="form-control" />
                <span asp-validation-for="Nom" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Rue" class="control-label"></label>
                <input asp-for="Rue" class="form-control" />
                <span asp-validation-for="Rue" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Ville" class="control-label"></label>
                <input asp-for="Ville" class="form-control" />
                <span asp-validation-for="Ville" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Save" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}

//----------------------------------- Examen SE ------------------------------------------------------//
//----------------------------------- Entity Framework Core ------------------------------------------------------//
/*
Dans la classe Admission :
▪ La propriété DateAdmission doit être une date valide.
▪ La propriété MotifAdmission doit être Multiligne et affiché «Le motif de
l’admission »
*/
  public class Admission
    {
        [DataType(DataType.Date)]
        public DateTime DateAdmission { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Le motif de l'admission")]
        public string MotifAdmission { get; set; }
        public int NbJours { get; set; }

        public int ChambreFK { get; set; }
        public virtual Chambre Chambre { get; set; }

        public int PatientFk { get; set; }
        public virtual Patient Patient { get; set; }


    }
	//Type détenu
	 //Owned ==> installer le package Entity Framework Core 
    [Owned]
    public class NomComplet
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

    }
	
	
	//Configuration
	/*
	En utilisant les configurations FluentAPI, configurer la relation 1..* entre Clinique et
Chambre en précisant CliniqueFK comme clé étrangère.
	*/
	public class ChambreConfig : IEntityTypeConfiguration<Chambre>
    {
        public void Configure(EntityTypeBuilder<Chambre> builder)
        {
            builder.HasOne(t => t.Clinique)
                .WithMany(t=> t.Chambres)
                .HasForeignKey(t=> t.CliniqueFK)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
	/*
	Faites le nécessaire pour représenter la relation entre Patient, Chambre et Admission. (1
pt)
	*/
 public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.HasOne(a => a.Patient)
                .WithMany(a=> a.Admissions)
                .HasForeignKey(a => a.PatientFk)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(a => a.Chambre)
                .WithMany(a=> a.Admissions)
                .HasForeignKey(a=> a.ChambreFK)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(p => new
            {

                p.PatientFk,
                p.ChambreFK,
                p.DateAdmission
            });

        }
		
//-------------------------------------- SERVICES ----------------------------------------------------//
/*
Retourner le pourcentage des chambres simples d’une clinique passée en paramètre.
*/

public class ChambreService : Service<Chambre>, IChambreService
    {
        public ChambreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public float GetPourcentagesSimples(Clinique clinique)
        {
            return clinique.Chambres.Where(t => t.TypeChambre == TypeChambre.Simple).Count()
                / clinique.Chambres.Count()*100;
                
        }
    }
	/*
	Retourner les noms complets des patients ayant occupé une chambre donnée à partir d’une
date donnée.
	*/
	
	  public IEnumerable<NomComplet> getOccupant(Chambre c, DateTime date)
        {
            return c.Admissions.Where(t => DateTime.Compare(t.DateAdmission, date) > 0)
                .Select(t => t.Patient.NomComplet);
                
        }
		
		/*
		
		Retourner la recette d’une clinique donnée pendant une année passée en paramètre
		*/
		  public double RecetteClinique(Clinique c, int annee)
        {
            return GetMany(t => t.DateAdmission.Year == annee && t.Chambre.CliniqueFK == c.CliniqueId)
                .Sum(t=> t.Chambre.Prix * t.NbJours);
        }
		
//-------------------------------------- Partie WEB ------------------------------------------------------------//
public class AdmissionController : Controller
    {
        IAdmissionService ads { get; set; }
        IPatientService ps { get; set; }

        IChambreService cs { get; set; }
        public AdmissionController(IAdmissionService ads, IPatientService ps, IChambreService cs)
        {
            this.ads = ads;
            this.ps = ps;
            this.cs = cs;
        }

       
/*
Créer la vue Index qui permet de lister les admissions ordonnées par date
d’admission
*/
        // GET: AdmissionController
        public ActionResult Index()
        {
            return View(ads.GetAll().OrderByDescending(t=> t.DateAdmission) );
        }

        // GET: AdmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View(ps.GetById(id));
        }

/*
Créer la vue Create admission qui permet d’affecter un patient à une chambre
*Ajouter deux listes déroulantes dynamiques pour le choix du patient ainsi que la chambre.
*/
        // GET: AdmissionController/Create
        public ActionResult Create()
        {
            ViewBag.ChambreFK =
                new SelectList(cs.GetAll(), "NumeroChambre", "NumeroChambre");

            ViewBag.PatientFk =
               new SelectList(ps.GetAll(), "NumDossier", "CIN");

            return View();
        }

        // POST: AdmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            try
            {
                ads.Add(admission);
                ads.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdmissionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdmissionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	
	//PatientController
	public class PatientController : Controller
    {
        IPatientService ps { get; set; }

        public PatientController(IPatientService ps)
        {
            this.ps = ps;
        }

        // GET: PatientController
        public ActionResult Index()
        {
            return View();
        }
		/*
		Le lien Details permet d’afficher les détails du Patient en question
		*/

        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            return View(ps.GetById(id) );
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	
	//HTML
	//CREATE Admission: 2 Lists Deroulants
	 <div class="form-group">
                <label asp-for="ChambreFK" class="control-label"></label>
                <select asp-for="ChambreFK" class="form-control" asp-items="ViewBag.ChambreFK"></select>

                <span asp-validation-for="ChambreFK" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="PatientFk" class="control-label"></label>
                <select asp-for="PatientFk" class="form-control" asp-items="ViewBag.PatientFk"></select>
                <span asp-validation-for="PatientFk" class="text-danger"></span>
            </div>
			
	//INDEX Admission (Redirection à Details Patient)
	@model IEnumerable<Examen.ApplicationCore.Domain.Admission>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.DateAdmission)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.MotifAdmission)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.NbJours)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.ChambreFK)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.PatientFk)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.DateAdmission)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.MotifAdmission)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.NbJours)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ChambreFK)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.PatientFk)
            </td>
            <td>
                    @Html.ActionLink(linkText: "Details Patient", actionName: "Details", controllerName: "Patient", routeValues: new { id = item.PatientFk }, htmlAttributes: null) |
                </td>
            @*<td>
                @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
            </td>*@
        </tr>
}
    </tbody>
</table>

//------------------------------------------ Examen Uber -----------------------------------------------------------//
//------------------------------------------ Entity Framework Core -----------------------------------------------------------//
//Meme que les autres examens
//------------------------------------------ SERVICES -----------------------------------------------------------//
/*
Une méthode qui retourne la voiture la plus demandée.
*/
public class VoitureService : Service<Voiture>, IVoitureService 
    {
        public VoitureService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Voiture la plus demandée
        public Voiture LaPlusDemandee()
        {
            return GetMany(t=>t.Couleur.Equals("Rouge")).OrderByDescending(t=>t.Courses.Count() ).FirstOrDefault();
        }
    }
	/*
	Une méthode qui retourne la liste des courses payées d’un chauffeur à une date
donnée, en utilisant la propriété Date du type DateTime.
	*/
public IEnumerable<Course> getCoursesPayee(Chauffeur c, DateTime date)
        {
            return GetMany(t => t.Etat == Etat.Payee && t.Voiture == c.Voiture && t.DateCourse == date);
        }
		
		/*
		Une méthode qui retourne le bénéfice total d’un chauffeur des courses payées à une
date donnée, en utilisant la propriété Date du type DateTime.
		*/
		 public double BeneficeTotal( Chauffeur c,DateTime date)
        {
            return getCoursesPayee(c, date).Sum(t => t.Montant * c.TauxBenefice);
        }
//------------------------------------------ Partie WEB -----------------------------------------------------------//

//Course Controller
public class CourseController : Controller
    {
        IServiceCourse serviceCourse;
        IServiceClient serviceClient;
        IServiceVoiture serviceVoiture;
        IServiceChauffeur serviceChauffeur;

        public CourseController(IServiceCourse serviceCourse, IServiceClient serviceClient, IServiceVoiture serviceVoiture, IServiceChauffeur serviceChauffeur)
        {
            this.serviceCourse = serviceCourse;
            this.serviceClient = serviceClient;
            this.serviceVoiture = serviceVoiture;
            this.serviceChauffeur = serviceChauffeur;
        }




        // GET: CourseController
        public ActionResult Index()
        {
            return View(serviceCourse.GetAll());
        }
		/*
		Créer et tester une vue qui permet d'afficher les courses payées d’un chauffeur à la
date d’aujourd’hui, en utilisant les services déjà implémentés dans la partie II. (2 pts).
NB. L’identifiant du chauffeur est passé en paramètre dans l’URL
(localhost:port/Course/ListByChauffeur/2).
		*/
        public ActionResult ListByChauffeur(int id)
        {
            return View(serviceCourse.GetPayedCourses(serviceChauffeur.GetById(id), DateTime.Now));
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

/*
Créer et tester la vue “Create” qui permet d’ajouter une course dans la base de
données.
*/
        // GET: CourseController/Create
        public ActionResult Create()
        {
            ViewBag.Clients = new SelectList(serviceClient.GetAll(), "CIN", "CIN");
            ViewBag.Voitures = new SelectList(serviceVoiture.GetAll(), "NumMat", "NumMat");

            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
         
            
                serviceCourse.Add(course);
                serviceCourse.Commit();
                return RedirectToAction(nameof(Index));
         
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	
	//HTML
	//CREATE
	//Meme que les autres examens
	//Index
	//Meme que les autres examens
	//List Chauffeurs
	@model IEnumerable<Examen.ApplicationCore.Domain.Course>

@{
    ViewData["Title"] = "ListByChauffeur";
}

<h1>ListByChauffeur</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.DateCourse)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.LieuDepart)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.VoitureId)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.ClientId)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.LieuArrivee)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Etat)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.DateCourse)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.LieuDepart)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.VoitureId)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ClientId)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.LieuArrivee)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Etat)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
            </td>
        </tr>
}
    </tbody>
</table>

//------------------------------------------ Examen Vaccin -----------------------------------------------------------//
//------------------------------------------ Entity Framework Code -----------------------------------------------------------//
//Meme que les autres examens
//------------------------------------------ SERVICES -----------------------------------------------------------//
/*
Implémenter la méthode qui permet de retourner “Valide” si un vaccin est disponible et
valide, sinon elle retourne “Nonvalide”. (1pt)
● Le vaccin est valide si “DateValidite” est supérieure ou égale à la date d’aujourd’hui.
● Le vaccin est disponible si la quantité est supérieure à zéro.
*/
 public class VaccinService : Service<Vaccin>, IVaccinService
    {
        public VaccinService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public string Validite(Vaccin v)
        {
            if (v.DateValidite >= DateTime.Now && v.Quantite > 0)
                return "Valide";
            else return "Invalide";
        }
    }
	/*
	Implémenter la méthode qui permet de retourner la liste des citoyens vaccinés (ayant au
moins un rendez-vous) groupée par Ville.
	*/
	public class CitoyenService : Service<Citoyen>, ICitoyenService
    {
        public CitoyenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IGrouping<string, IEnumerable<Citoyen>> GetCitoyensVaccinés()
        {
            return (IGrouping<string, IEnumerable<Citoyen>>)GetMany(c => c.rendezVous.Count >= 0).GroupBy(c => c.Adresse.Ville);
        }
    }
	/*
	Implémenter une méthode qui vérifie, pour une date de vaccination passée en paramètre, si
la capacité d’un centre de vaccination donné permet d’accueillir des nouveaux citoyens,
sachant que:
La méthode retourne “true” si le centre de vaccination peut accueillir le nombre des citoyens
inscrits , sinon la méthode retourne “false”.
	*/
	
	public class CentreVaccinationService : Service<CentreVaccination>, ICentreVaccinationService
    {
        public CentreVaccinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool Capacite(DateTime Date, CentreVaccination centre)
        {
            int TotalCitoyens = 0;
            foreach (Vaccin v in centre.vaccins)
            {
                TotalCitoyens = TotalCitoyens + v.rendezVous.ToList().Count(r => r.DateVaccination == Date);
            }
            if (centre.Capacite >= TotalCitoyens)
                return true;
            return false;
        }
    }
//------------------------------------------ Partie WEB -----------------------------------------------------------//
//Meme que les autres examens

//------------------------------------------ Examen TWIN 2022 -----------------------------------------------------------//
//------------------------------------------ Entity Framework Core -----------------------------------------------------------//
//Meme que les autres examens
//------------------------------------------ SERVICES -----------------------------------------------------------//
/*
Une méthode qui retourne le livre le plus emprunté.
*/
public class LivreService : Service<Livre>, ILivreService
    {
        public LivreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public Livre GetMostPrete()
        {
            return GetAll().OrderByDescending(e => e.PretLivres.Count).FirstOrDefault();
        }
    }

public class PretLivreService : Service<PretLivre>, IPretLivreService
    {
        public PretLivreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
		/*
		Une méthode qui retourne les livres empruntés entre deux dates (passées en
paramètres).
		*/
        public IEnumerable<Livre> GetLivresPretes(DateTime debut, DateTime fin)
        {
            return GetMany(e => e.DateDebut >= debut && e.DateFin <= fin).Select(e => e.Livre);
        }
		/*
		Une méthode qui retourne la liste des catégories des livres empruntés par des
abonnés ayant un statut passé en paramètre.
		*/
        public IEnumerable<Categorie> GetCategoriesLivresPretes(Statut statut)
        {
            return GetMany(e => e.Abonne.Statut == statut).
                Select(e => e.Livre.Categorie).GroupBy(e => e.Code).Select(e => e.First());
        }
    }
//------------------------------------------ Partie WEB -----------------------------------------------------------//

 public class PretLivreController : Controller
    {
        private readonly IPretLivreService pretLivreService;
        private readonly IService<Abonne> abonneService;
        private readonly IService<Livre> livreService;

        public PretLivreController(IPretLivreService pretLivreService, IAbonneService abonneService, ILivreService livreService)
        {
            this.pretLivreService = pretLivreService;
            this.abonneService = abonneService;
            this.livreService = livreService;
        }
        // GET: PretLivreController
        public ActionResult Index()
        {
            return View(pretLivreService.GetAll().ToList());
        }
/*
Ajouter à la vue “Index” créé précédemment une zone de recherche par date
de début et date de fin.
*/
ActionResult Index(DateTime? dd, DateTime? df)
        {
            if (dd == null || df== null)
            {
                return View(ps.GetAll());

            }
            return View(ps.GetMany(p => p.DateDebut.Equals(dd) || p.DateFin.Equals(df)) );

        }
		/*OU*/
        [HttpPost]
        public ActionResult Index(DateTime? dateDebut, DateTime? dateFin)
        {
            IEnumerable<PretLivre> list;

            if (dateDebut != null && dateFin != null)
            {
                list = pretLivreService.GetMany(e => e.DateDebut >= dateDebut.Value && e.DateFin <= dateFin.Value).ToList();
            }
            else if (dateDebut != null)
            {
                list = pretLivreService.GetMany(e => e.DateDebut >= dateDebut.Value).ToList();
            }
            else if (dateFin != null)
            {
                list = pretLivreService.GetMany(e => e.DateFin <= dateFin.Value).ToList();
            }
            else
            {
                list = pretLivreService.GetAll().ToList();
            }

            return View(list);
        }

        // GET: PretLivreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

/*
Créer et tester la vue “Create” qui permet d’ajouter un prêt dans la base de
données.
*/
        // GET: PretLivreController/Create
        public ActionResult Create()
        {
            ViewBag.Abonnes = new SelectList(abonneService.GetAll(), "Id", "Nom");
            ViewBag.Livres = new SelectList(livreService.GetAll(), "LivreId", "Titre");
            return View();
        }

        // POST: PretLivreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PretLivre pretLivre)
        {
            try
            {
                pretLivreService.Add(pretLivre);
                pretLivreService.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PretLivreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PretLivreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PretLivreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PretLivreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	
	//HTML
	//INDEX EMPRUNT
	@model IEnumerable<Examen.ApplicationCore.Domain.PretLivre>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>
//Recheche entre 2 dates
<form asp-action="Index">
    <span>Date Debut: @Html.TextBox("dateDebut")</span>
    <span>Date Fin: @Html.TextBox("dateFin")</span>
    <input type="submit" value="Rechercher" />
</form>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.DateDebut)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.DateFin)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Abonne)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Livre)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.DateDebut)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.DateFin)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Abonne.Nom)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Livre.Titre)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) |
                    @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                    @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
                </td>
            </tr>
        }
    </tbody>
</table>



//----------------------------------------- Examen NIDS 2023 ------------------------------------------//
//----------------------------------------- Entity Framework Core ------------------------------------------//
public class Beneficiary
    {
        //prop+2tab
		// 8 Chiffres
        [Key]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CIN must be an 8-digit number.")]
        public int CIN { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be 8 digits.")]
        public virtual Contact Contact { get; set; }
        public string Description { get; set; }



        //prop de navigation: virtual

    }
	
	 [Owned]
    public class Contact
    {
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be 8 digits.")]
        public string Phone { get; set; }
        
        public string Adress { get; set; }

        public string Email { get; set; }
    }
	
	 public class Kafala
        {
            //prop+2tab
            [Required]

            public DateTime StartDate { get; set; }

            [Required]

            public DateTime EndDate { get; set; }
       
        //Type Monnaie
            [DataType(DataType.Currency)]
            public int Amount { get; set; }

            public virtual int BeneficiaryFk { get; set; }
            public virtual int DonatorFk { get; set; }

            [ForeignKey("BeneficiaryFk")]
            public virtual Beneficiary Beneficiary { get; set; }

            [ForeignKey("DonatorFk")]
            public virtual Donator Donator { get; set; }

      
            //prop de navigation: virtual

        }
//----------------------------------------- SERVICES ------------------------------------------//
/*
Renvoyer la liste des beneficiaries profitant d'une "Kafala" jusqu'a l'instant
*/
public class KafalaService : Service<Kafala>, IKafalaService
    {
        public KafalaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public List<Beneficiary> GetBeneficiariesWithKafala()
        {
            {
                // Assuming you have a list of beneficiaries and kafalas
                List<Beneficiary> beneficiaries = new List<Beneficiary>();
                List<Kafala> kafalas = new List<Kafala>();

                // Get the current moment
                DateTime now = DateTime.Now;

                // Query to filter beneficiaries with kafala based on the current moment
                var result = from beneficiary in beneficiaries
                             join kafala in kafalas on beneficiary.CIN equals kafala.BeneficiaryFk
                             where kafala.StartDate <= now && kafala.EndDate >= now
                             select beneficiary;

                return result.ToList();
            }


        }
    }
	/*
	Renvoyer la liste des dons pour une periode passée en paramètre
	*/
	public class DonationService : Service<Donation>, IDonationService
    {
        public DonationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public decimal GetTotalDonationsForPastPeriod(DateTime startDate, DateTime endDate)
        {
            // Assuming you have a list of donations
            List<Donation> donations = new List<Donation>();

            // Filter donations based on the specified period
            var filteredDonations = donations.Where(d => d.Date >= startDate && d.Date <= endDate);

            // Calculate the total amount of donations
            decimal totalDonations = filteredDonations.Sum(d => d.Amount);

            return totalDonations;
        }

    }
	/*
	Renvoyer la liste des donateurs qui peuvent etre sollicités pour une "Kafala". Ce sont des donateurs
	actifs mais qui n'ont aucune "Kafala" en cours
	*/
	public class DonatorService : Service<Donator>, IDonatorService
    {
        public DonatorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public IEnumerable<Donator> GetAvailableDonators()
        {
            var donators = GetAll();
            var kafalas = GetAll();

            var availableDonators = donators
                .Where(d => !kafalas.Any(k => k.Id == d.Id))
                .ToList();

            return availableDonators;
        }
    }
//----------------------------------------- Partie WEB ------------------------------------------//

//Kafala Controller
 public class KafalaController : Controller
    {
        public IKafalaService kfs;
        public IBeneficiaryService bs;
        public IDonatorService ds;
        public KafalaController(IKafalaService kfs, IBeneficiaryService bs , IDonatorService ds) {
         this.kfs = kfs;
         this.bs = bs;  
         this.ds = ds;
        }
        // GET: KafalaController
        public ActionResult Index()
        {
            return View(kfs.GetAll());
        }

        // GET: KafalaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
/*CREATE*/
        // GET: KafalaController/Create
        public ActionResult Create()
        {
            ViewBag.BenfList = new SelectList(bs.GetAll(), "CIN", "Name");
            ViewBag.DonList = new SelectList(ds.GetAll(), "Id", "Name");

            return View();
        }

        // POST: KafalaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kafala kafala)
        {
            try
            {
                kfs.Add(kafala);
                kfs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KafalaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KafalaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KafalaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KafalaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

//Donator Controller
 public class DonatorController : Controller
    {
        public IDonatorService ds;

           public DonatorController (IDonatorService ds)
        {
                this.ds = ds;
        }
        // GET: DonatorController
        public ActionResult Index()
        {
            return View(ds.GetAll());
        }

        // GET: DonatorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DonatorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonatorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DonatorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonatorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonatorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
	
	//HTML
	//CREATE Kafala ===> Meme que les autres examens
	//Index Kafala ===> Meme que les autres examens
	//Index Donator (Redirection)
	@model IEnumerable<Examen.ApplicationCore.Domain.Donator>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Id)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Profession)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Id)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Profession)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                    @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })|
				
                    @Html.ActionLink(linkText: "Kafala" , actionName :"Index" , controllerName:"Kafala",routeValues
                :new {  id=item }) |
            </td>
        </tr>
}
    </tbody>
</table>

