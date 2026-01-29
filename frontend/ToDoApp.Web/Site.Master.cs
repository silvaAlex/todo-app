using System;
using System.Web.UI;
using ToDoApp.Web.Helpers;

namespace ToDoApp.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthSession.Logout();
            Response.Redirect("~/");
        }
    }
}