using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
