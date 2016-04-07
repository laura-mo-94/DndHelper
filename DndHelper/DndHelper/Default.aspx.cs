using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DndHelper
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster master = Page.Master as SiteMaster;

            if (!String.IsNullOrEmpty(Context.User.Identity.Name))
            {
                IdentityHelper.RedirectToReturnUrl(master.getURL("/Account/UserPage.aspx", 0, Context.User.Identity.Name), Response);
            }
        }
    }
}