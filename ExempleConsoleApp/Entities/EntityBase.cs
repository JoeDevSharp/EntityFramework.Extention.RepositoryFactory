using JoeDevSharp.RepositoryFactory.EntityFramework.Interfaces;

namespace ExempleConsoleApp.Entities
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
