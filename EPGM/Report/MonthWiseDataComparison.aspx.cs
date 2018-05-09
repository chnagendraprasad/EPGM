
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI;
using System.Web;
using EPGM.Business;

namespace EPGM.Report
{
    public partial class MonthWiseDataComparison : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Load", "Redirection()", true);
                }
                else
                {
                    BindSSRS();
                    //msg.Attributes.Add("display", "block");
                    msg.Style["display"] = "block";
                }
            }
        }

        protected void BindSSRS()
        {
            try
            {
                string centercode = null;
                if (Request.QueryString["centercode"].ToString() == "")
                {
                    centercode = " ";
                }
                else
                {
                    centercode = Request.QueryString["centercode"].ToString();
                }

                ReportViewer1.ShowParameterPrompts = false;
                ReportViewer1.ShowToolBar = false;
                ReportViewer1.WaitMessageFont.Bold = true;

                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerURL"].ToString());
                ReportViewer1.ServerReport.ReportPath = "/EPGMMonthWiseComparisionChart";

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["RSDomainUserName"].ToString(), ConfigurationManager.AppSettings["RSDomainPassword"].ToString(), ConfigurationManager.AppSettings["RSDomainName"].ToString());
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;

                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.Width = Unit.Percentage(100);
                ReportViewer1.Height = Unit.Percentage(100);

                List<ReportParameter> parmList = new List<ReportParameter>();
                ReportParameterInfoCollection parameters;
                parameters = ReportViewer1.ServerReport.GetParameters();
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters[i].Name.ToLower() == "centertypeid")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(),Request.QueryString["CenterType"], false));
                    }
                    if (parameters[i].Name.ToLower() == "statecode")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["statecode"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "distcode")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["distcode"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "projectcode")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["projectcode"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "sectorcode")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["sectorcode"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "centercode")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), centercode.ToString(), false));
                    }
                }

                ReportViewer1.ServerReport.SetParameters(parmList);
                ReportViewer1.DataBind(); //ReportViewer1.ServerReport.Refresh(); 
 
               // msg.Visible = true; 
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
                lblerror.ForeColor = Color.DarkRed;
            }
        }
    }
}