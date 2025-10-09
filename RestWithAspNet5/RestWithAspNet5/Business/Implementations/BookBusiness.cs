using System;
using RestWithASPNET.Model;
using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Repository.Generic;

namespace RestWithAspNet5.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book person)
        {
            return _repository.Create(person);
        }

        public Book Update(Book person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
