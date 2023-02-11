using Icarus.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Persistence;

public static class InitialDataForIcarusDBExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Member>().HasData(
        //    Member.Create(Guid.NewGuid(),"Blair","Carager","blair@gmail.com"),
        //    Member.Create(Guid.NewGuid(),"Harry","Porter","harry@gmail.com"));
    }
}
