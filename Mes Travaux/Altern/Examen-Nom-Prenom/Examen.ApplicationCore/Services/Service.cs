using Examen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
        }

        public void Add(T entity)
        {
            //_unitOfWork.Repository<T>().Add(entity);
            _repository.Add(entity);
        }

       

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            _repository.Delete(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _repository.Get(where);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(params object[] keyValues)
        {
            return _repository.GetById(keyValues);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
        public void Commit()
        {
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
