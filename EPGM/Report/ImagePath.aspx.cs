using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPGM.Report
{
    public partial class ImagePath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrl1.Text= Request.QueryString["Name"].ToString();
            img1.ImageUrl = Request.QueryString["ImagePath"].ToString();
        }
    }
}