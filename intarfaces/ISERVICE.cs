namespace BooksLib.interfaces;

public interface ISERVICE<T>
{
    void Write(T entity);
    List<T> ReadAll();
    T ReadById(int id);
}
