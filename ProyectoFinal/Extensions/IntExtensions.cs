using HashidsNet;

namespace ProyectoFinal.Extensions
{
    public static class IntExtensions
    {
        public const string HashIdsSalt = "9fd34b0eb1f03fff46c95b7fb8f09a25aae309866f2af2004f30da9a2e7ebae9";

        public static string ToHashId(this int number) =>
            GetHasher().Encode(number);

        public static int FromHashId(this string encoded) =>
            GetHasher().Decode(encoded).FirstOrDefault();

        private static Hashids GetHasher() => new(HashIdsSalt, 8);
    }
}
