using backend.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.tests.InMemory;

public class InMemoryDatabase
{
  static public Persistence Initialize(string tableName)
  {
    var options = new DbContextOptionsBuilder<Persistence>()
    .UseInMemoryDatabase(databaseName: tableName)
    .Options;
    var persistence = new Persistence(options);
    return persistence;
  }
}