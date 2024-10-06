using BooksLib.services;

namespace BooksLib.utils;

public static class ServiceLoader
{
    public static void Register(WebApplicationBuilder builder){
        builder.Services.AddSingleton<BookService>();
        builder.Services.AddSingleton<UserService>();
    }
}
