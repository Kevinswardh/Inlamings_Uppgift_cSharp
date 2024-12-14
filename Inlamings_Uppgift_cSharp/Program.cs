using System;
using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Factory;
using Business.CoreFiles.Factory.Interfaces;
using Business.Logic._2_Repositories;
using Business.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Business._2_Repositories.JsonRepository.Interface;
using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces.IUser;
using Business.Interfaces.Repositories;
using Business.Logic._1_Services.UserService.Interface;
using Business.Logic._1_Services.UserService;
using Business.CoreFiles.Models.Contacts;


namespace ConsoleApp
{
    class Program
    {
        private static BaseUser _loggedInUser = null;
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json");
        static void Main(string[] args)
        {
            // Skapa en service collection för att registrera tjänster
            var services = new ServiceCollection();


            //Registrerar Services
            services.AddSingleton<IJsonRepository>(provider => new JsonRepository(_filePath, provider.GetService<IExampleUserCreate>()));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserCreate, UserCreate>();
            services.AddTransient<UserService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IExampleUserCreate, ExampleUserCreate>();

            // Bygg service provider
            var serviceProvider = services.BuildServiceProvider();

            // Hämta UserService från DI-container
            var userService = serviceProvider.GetService<UserService>();


            // Huvudmeny
            ExampleUserFrontPage.Start(serviceProvider);
        }

       


    }
}
