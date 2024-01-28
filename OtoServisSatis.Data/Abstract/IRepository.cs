using System.Linq.Expressions; //expressions ı ekledik

namespace OtoServisSatis.Data.Abstract
{
    // temel crud işlemlerini yapacak repository yapımız
    public interface IRepository<T> where T : class //tüm veritabanı classları için çalışabilmesi için generic <T> yapıyoruz
    {
        //aşağıdakiler normal senkron metotlarımız
        List<T> GetAll(); //burada tüm kayıtlar listelenir
        List<T> GetAll(Expression<Func<T,bool>> expression); //bu şekilde filtreleyip de getirebiliriz expression ile
        
        T  Get(Expression<Func<T,bool>> expression); 

        T Find(int  id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        int Save(); //veritabanına değişiklikleri işleyeceğimiz metot imzası


        //.NET core dan daha hızlı işlem yapmamızı sağlayan asenkron metotlar
        //asenkron metotların çalışması için task içine almamız gerekiyor
        Task<T> FindAsync(int id);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task<int> SaveAsync();

    }
}
