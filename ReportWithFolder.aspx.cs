using ReportsRender.rs2005;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportsRender
{
    public partial class ReportWithFolder : System.Web.UI.Page
    {
        // TODO: CREAR CLASE APARTE PARA QUE TODOS HEREDEN DE ELLA

        private string folder = "/";  // Ahora mismo ninguno
        private string report;
        private string reportName;   // Nombre
        private string reportFolder;   // Ruta
                                       // private string reportParam;  // Param

        protected void Page_Load(object sender, EventArgs e)
        {
            reportName = !string.IsNullOrEmpty(ReportName) ? ReportName : reportName;
            reportFolder = !string.IsNullOrEmpty(ReportFolder) ? ReportFolder : reportFolder;
            if (!IsPostBack)   // Si carga por primera vez, si no se esta respondiendo a una solicitud hecha
            {
                reportName = !string.IsNullOrEmpty(Request.QueryString["Nombre"]) ? Request.QueryString["Nombre"] : reportName;
                reportFolder = !string.IsNullOrEmpty(Request.QueryString["Folder"]) ? Request.QueryString["Folder"] : reportFolder;
                //reportParam = !string.IsNullOrEmpty(Request.QueryString["Param"]) ? Request.QueryString["Param"] : "";

                if (!string.IsNullOrEmpty(reportName))
                {
                    ddlFolders.Visible = false;
                    ddlReports.Visible = false;
                    ParamTable.Visible = false;
                    if (!string.IsNullOrEmpty(reportFolder))
                    {
                        report = reportFolder + "/" + reportName;
                    }
                    else
                    {
                        report = reportName;
                    }
                    ReportViewer1.ServerReport.ReportPath = report;

                    /// Poniendo Parametros
                    List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                    if (Request.QueryString.Count > 1)
                    {
                        foreach (string item in Request.QueryString)
                        {
                            if (item != "Nombre" && item != "Folder")
                            {
                                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter(item, Request.QueryString[item].ToString()));
                            }
                        }
                        ReportViewer1.ServerReport.SetParameters(paramList);
                    }

                    ReportViewer1.ServerReport.Refresh();
                    LoadReports(reportFolder);
                }
                else
                {
                    if (!string.IsNullOrEmpty(reportFolder))
                    {
                        LoadReports(reportFolder);
                    }
                    else
                    {
                        LoadReports(folder);
                    }
                    ShowReport();
                }
            }
            else // 2 o mas veces que ya entrado
            {
                if (Request.QueryString.Count == 0 && (ddlReports.AutoPostBack || ddlFolders.AutoPostBack))
                {
                    if (ddlFolders.SelectedItem != null)
                    {
                        LoadReports(ddlFolders.SelectedItem.Text);
                        
                        if (ReportViewer1.ServerReport.ReportPath != ddlReports.SelectedItem.Value)
                        {
                            ShowReport();
                        }
                    }
                    else
                    {
                        LoadReports(folder);
                    }
                }
               
            } 


        }


        private ReportingService2005 rs = new ReportingService2005();

        ///
        /// Cargar la lista de reportes en el dropdown
        ///
        protected void LoadReports(string _folder)
        {
            rs.Url = "http://datos/ReportServer/reportservice2005.asmx";
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

            CatalogItem[] items = rs.ListChildren(folder, true);
            if (ddlReports.Items.Count == 0)
            {
                foreach (CatalogItem item in items.Where(x => x.Type == ItemTypeEnum.Folder).ToList())
                {
                    ddlFolders.Items.Add(new ListItem { 
                        Text = item.Path,
                        Selected = item.Path == _folder
                    });;
                }


                // TODO: SI NO EXISTE EL ddlFolders.SelectedItem , entonces seleccionar uno
                if (ddlFolders.SelectedItem != null)
                {
                    foreach (CatalogItem item in items.Where(x => x.Type == ItemTypeEnum.Report && x.Path == ddlFolders.SelectedItem.Text + "/" + x.Name).ToList())
                    {
                        ddlReports.Items.Add(new ListItem(item.Name, item.Path));
                    }
                }
                ddlFolders.Visible = true;
                ddlReports.Visible = true;
                ParamTable.Visible = true;
            }
            else
            {
                if (ddlReports.SelectedItem.Value.Replace(_folder + "/", "") != ddlReports.SelectedItem.Text)
                {
                    ddlReports.Items.Clear();
                    foreach (CatalogItem item in items.Where(x => x.Type == ItemTypeEnum.Report && x.Path == ddlFolders.SelectedItem.Text + "/" + x.Name).ToList())
                    {
                        ddlReports.Items.Add(new ListItem(item.Name, item.Path));
                    }
                }

            }
        }

        // VARIABLES DE SECCION
        protected String ReportName
        {
            get { return (String)Session["ReportName"] ?? ""; }
            set
            {
                Session.Add("ReportName", value);
            }
        }

        // TODO: Verificar esta variable
        protected String ReportFolder
        {
            get { return (String)Session["ReportFolder"] ?? ""; }
            set
            {
                Session.Add("ReportFolder", value);
            }
        }

        /// <summary>
        /// Refrescar la data en el report viewer
        /// </summary>
        protected void ShowReport()
        {
            if (ddlReports.Items.Count != 0)
            {
                ReportName = ddlReports.SelectedItem.Value;
                ReportViewer1.ServerReport.ReportPath = ReportName;
                ReportViewer1.ServerReport.Refresh();
            }
            else
            {
                ReportViewer1.ServerReport.ReportPath = "";
                ReportViewer1.ServerReport.Refresh();
            }


        }


        /// <summary>
        /// Imprimir un Alert con un mensaje.
        /// </summary>
        /// ToDo: Poner esto en una clase que sea extension de System.Web.UI.Page
        /// 
        public void Show(String Message)
        {
            ClientScript.RegisterStartupScript(
               this.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }

        //public override void Dispose()
        //{
        //    base.Dispose();
        //}

    }
}