using ExempleConsoleApp.Entities;
using EntityFramework.Extention.RepositoryFactory.Core;

namespace ExempleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new RepositoryFactory<AppContext>();
            var userRepository = factory.CreateRepository<User>();

            var exist = userRepository.Exists(u => u.Name == "John Doe");

            if (exist is false)
            {
                userRepository.Add(new User { Name = "John Doe", Email = "" });
                userRepository.Save();
            }

            User? user = userRepository.Find(u => u.Name == "John Doe");

            if (user is null)
                return;
            
            user.Name = "John Doe Updated";
            userRepository.Update(user);
            userRepository.Save();

            Console.WriteLine($"User exists: {exist}");
        }
    }
}
