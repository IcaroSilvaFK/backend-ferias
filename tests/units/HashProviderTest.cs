using backend.src.Providers;
using Xunit;

namespace backend.tests.units;


public class HashProviderTest
{
  [Fact]
  public void ShouldBeInstanceHashProvider()
  {
    var hash = HashProvider.HashPassword("Test");

    Console.WriteLine(hash);

    Assert.NotNull(hash);
    Assert.IsType<string>(hash);
  }

  [Fact]
  public void ShouldBeVerifyPassword()
  {
    var hash = HashProvider.HashPassword("Test");

    var result = HashProvider.VerifyPassword("Test", hash);

    Assert.True(result);
  }
}