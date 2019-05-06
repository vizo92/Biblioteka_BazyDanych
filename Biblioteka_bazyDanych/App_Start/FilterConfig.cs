using System.Web;
using System.Web.Mvc;

namespace Biblioteka_bazyDanych
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
