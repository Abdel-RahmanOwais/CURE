namespace CURE.Application.Interfaces.Security
{
    public interface IPasswordHash
    {
        public string Hash(string password);

        public bool Verify(string password, string hash);

    }
}
