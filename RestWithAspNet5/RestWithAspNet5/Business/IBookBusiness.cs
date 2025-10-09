using RestWithASPNET.Model;
using RestWithAspNet5.Model;

namespace RestWithAspNet5.Business
{
    public interface IBookBusiness
    {
        Book Create(Book person);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book person);
        void Delete(long id);

    }
}
