namespace PodioAPI.Utils.Authentication
{
    public interface IAuthStore
    {
        /// <summary>
        /// Get PodioOAuth object from store
        /// </summary>
        /// <returns></returns>
        PodioOAuth Get();

        /// <summary>
        /// Store PodioOAuth object to store
        /// </summary>
        /// <param name="podioOAuth"></param>
        void Set(PodioOAuth podioOAuth);

        /// <summary>
        /// Remove PodioOAuth from store
        /// </summary>
        void Clear();
    }
}
