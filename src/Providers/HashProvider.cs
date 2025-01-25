using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace backend.src.Providers;

public interface IHashProvider
{
  string HashPassword(string password);
  bool VerifyPassword(string password, string hashedPassword);
}

public class HashProvider 
{
  private static readonly byte[] Key = System.Text.Encoding.UTF8.GetBytes("e5266e9f-6aa0-4282-84f8-4657e2762050!");
  private static readonly KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
  private static readonly int IterationCount = 10000;
  private static readonly int NumBytesRequested = 256 / 8;
  static public string HashPassword(string password)
  {

    var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
      password: password,
      salt:Key,
      prf: Prf,
      iterationCount: IterationCount,
      numBytesRequested: NumBytesRequested
    ));

    return hashed;
  }

  public static bool VerifyPassword(string password, string hashedPassword)
  {
    var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
      password: password,
      salt:Key,
      prf: Prf,
      iterationCount: IterationCount,
      numBytesRequested: NumBytesRequested
    ));

    return hashed.Equals(hashedPassword);
  }
}