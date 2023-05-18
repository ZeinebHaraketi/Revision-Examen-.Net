using Examen.ApplicationCore.Interfaces;
using Examen.Interfaces;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Examen.ApplicationCore.Services
{
    public class TransactionService : Service<Transaction>, IServiceTransactionn
    {
        public TransactionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double getMontant(Domain.DAB dAB)
        {
            var montant = from t in dAB.Transactions select t.Montant;
            return montant.Sum();
        }

        public int totalTransactionCompte(Domain.Compte compte)
        {
            return compte.Transactions
                .Where(t => (DateTime.Now - t.Date ).TotalDays > 7)
                .Count();
        }




        public void Add(Domain.Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Domain.Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Domain.Transaction, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Domain.Transaction Get(Expression<Func<Domain.Transaction, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Transaction> GetMany(Expression<Func<Domain.Transaction, bool>> where)
        {
            throw new NotImplementedException();
        }

       

        public void Update(Domain.Transaction entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Domain.Transaction> IService<Domain.Transaction>.GetAll()
        {
            throw new NotImplementedException();
        }

        Domain.Transaction IService<Domain.Transaction>.GetById(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

       
    }
}
