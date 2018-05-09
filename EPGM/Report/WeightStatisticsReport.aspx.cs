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
    public partial class WeightStatisticsReport : System.Web.UI.Page
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
                }
            }
        }
        protected void BindSSRS()
        {
            try
            {

                ReportViewer1.ShowParameterPrompts = false;
                ReportViewer1.ShowToolBar = true;
                ReportViewer1.WaitMessageFont.Bold = true;

                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerURL"].ToString());
                ReportViewer1.ServerReport.ReportPath = "/EPGMWeightStatistics";

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
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["centertype"].ToString(), false));
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
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["centercode"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "distname")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["distname"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "projectname")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["projectname"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "sectorname")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["sectorname"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "awcname")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["centername"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "month")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["month"].ToString(), false));
                    }
                    if (parameters[i].Name.ToLower() == "year")
                    {
                        parmList.Add(new ReportParameter(parameters[i].Name.ToString(), Request.QueryString["year"].ToString(), false));
                    }

                }

                ReportViewer1.ServerReport.SetParameters(parmList);
                ReportViewer1.DataBind(); //ReportViewer1.ServerReport.Refresh();   
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
                lblerror.ForeColor = Color.DarkRed;
            }
        }
    }
}