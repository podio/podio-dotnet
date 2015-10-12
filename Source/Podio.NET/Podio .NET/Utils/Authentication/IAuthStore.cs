namespace PodioAPI.Utils.Authentication
{
    public interface IAuthStore
    {
        /// <summary>
        ///     Store PodioOAuth object to store
        /// </summary>
        /// <param name="podioOAuth"></param>
        void Set(PodioOAuth podioOAuth);
    }
}