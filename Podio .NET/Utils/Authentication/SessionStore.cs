using System.Web;

namespace PodioAPI.Utils
{
    class SessionStore: IAuthStore
    {
        public PodioOAuth Get()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["PodioOAuth"] != null)
                return HttpContext.Current.Session["PodioOAuth"] as PodioOAuth;
            else
                return new PodioOAuth();
        }

        public void Set(PodioOAuth podioOAuth)
        {
            HttpContext.Current.Session["PodioOAuth"] = podioOAuth;
        }
        public void Distroy()
        {
            HttpContext.Current.Session["PodioOAuth"] = null;
        }
    }
}
