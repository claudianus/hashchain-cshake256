using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace hashchain_cshake256 {
  internal static class Program {
    private static void Main() {
      Console.WriteLine("Hello World!");

      const int hashchainLength = 1000_000;
      
      //해시체인 시드
      var randomBytes = new byte[1000];
      using var c = new RNGCryptoServiceProvider();
      //시드 생성
      c.GetBytes(randomBytes);
      
      const int outputLength = 25;
      const string functionName = "hello";
      const string customization = "world";
      
      var cShakeDigest =
        new CShakeDigest(256,
          Encoding.ASCII.GetBytes(functionName),
          Encoding.ASCII.GetBytes(customization));
      
      var previousHash = randomBytes;
      for (int i = 0; i < hashchainLength; i++) {
        cShakeDigest.Reset();
        cShakeDigest.BlockUpdate(previousHash, 0, previousHash.Length);
        var hash = new byte[outputLength];
        cShakeDigest.DoFinal(hash, 0, outputLength);
        previousHash = hash;
        // var hashString = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        // Console.WriteLine($"{i} {hashString}");
      }
      
      Console.WriteLine("done");
    }
  }
}