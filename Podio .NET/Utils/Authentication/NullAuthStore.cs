namespace PodioAPI.Utils.Authentication
{
    public class NullAuthStore : IAuthStore
    {
        public PodioOAuth Get()
        {
            return new PodioOAuth();
        }

        public void Set(PodioOAuth podioOAuth)
        {
        }

        public void Clear()
        {
        }
    }
}