namespace PodioAPI.Utils
{
    public interface IAuthStore
    {
        /// <summary>
        /// Get PodioOAuth object from store
        /// </summary>
        /// <returns></returns>
        PodioOAuth Get();

        /// <summary>
        /// Store PodioOAuth object from store
        /// </summary>
        /// <param name="podioOAuth"></param>
        void Set(PodioOAuth podioOAuth);

        /// <summary>
        /// Remove PodioOAuth from store
        /// </summary>
        void Distroy();
    }
}
