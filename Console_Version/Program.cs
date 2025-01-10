using System;
using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Factory;
using Business.CoreFiles.Factory.Interfaces;
using Business.Logic._2_Repositories;
using Business.Services;
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
        // Variabel för att lagra den inloggade användaren
        private static BaseUser _loggedInUser = null;

        // Sökväg till JSON-filen som används som databas
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Business\CoreFiles\Databases\JsonFileDb\JsonDb.json");

        /// <summary>
        /// Huvudingången för applikationen.
        /// </summary>
        /// <param name="args">Kommandoradsargument.</param>
        static void Main(string[] args)
        {
            // Skapa en service collection för att registrera tjänster (Dependency Injection)
            var services = new ServiceCollection();

            // Registrera tjänster för Dependency Injection
            services.AddSingleton<IJsonRepository>(provider => new JsonRepository(_filePath, provider.GetService<IExampleUserCreate>()));
            services.AddScoped<IUserRepository, UserRepository>();

            //IUserCreate behövs tas bort om vi ska köra Static factory
            services.AddTransient<IUserCreate, UserCreate>();
            services.AddTransient<UserService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IExampleUserCreate, ExampleUserCreate>();

            // Bygg service provider för att hantera tjänsterna
            var serviceProvider = services.BuildServiceProvider();

            // Hämta UserService från DI-container
            var userService = serviceProvider.GetService<UserService>();

            // Starta huvudmenyn för exempelanvändaren
            ExampleUserFrontPage.Start(serviceProvider);
        }
    }
}
