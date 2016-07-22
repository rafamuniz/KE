using System;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.Web.Configuration;
using System.Windows.Forms;

namespace KarmicEnergy.Web
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        private void AddConfigurationFileDetails()
        {
            string configFile = string.Concat(Assembly.GetExecutingAssembly().Location, "Web.config");
            int index = configFile.IndexOf("bin");
            configFile = configFile.Substring(0, index);

            VirtualDirectoryMapping vdm = new VirtualDirectoryMapping(configFile, true);
            WebConfigurationFileMap wcfm = new WebConfigurationFileMap();
            wcfm.VirtualDirectories.Add("/", vdm);

            #region ConnectionStrings

            string server = Context.Parameters["SERVER"];
            string databasename = Context.Parameters["DATABASENAME"];
            string username = Context.Parameters["USERNAME"];
            string password = Context.Parameters["PASSWORD"];

            String dataSource = String.Format("Data Source={0};Initial Catalog={1};User={2};password={3};Integrated Security=false;", server, databasename, username, password);

            Configuration webConfig = WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");
            webConfig.ConnectionStrings.ConnectionStrings.Clear();

            ConnectionStringSettings connectionString = webConfig.ConnectionStrings.ConnectionStrings["KEConnection"];
            connectionString = new ConnectionStringSettings("KEConnection", dataSource, "System.Data.SqlClient");
            webConfig.ConnectionStrings.ConnectionStrings.Add(connectionString);

            #endregion ConnectionStrings

            #region AppSettings
            string siteId = Context.Parameters["SITEID"];
            string masterURL = Context.Parameters["MASTERURL"];

            var settings = webConfig.AppSettings.Settings;

            if (settings["Site:Id"] == null)
            {
                webConfig.AppSettings.Settings.Add("Site:Id", siteId);
            }
            else
            {
                webConfig.AppSettings.Settings["Site:Id"].Value = siteId;
            }

            if (settings["Master:Url"] == null)
            {
                webConfig.AppSettings.Settings.Add("Master:Url", masterURL);
            }
            else
            {
                webConfig.AppSettings.Settings["Master:Url"].Value = masterURL;
            }

            if (settings["Notification:PathTemplate"] == null)
            {
                webConfig.AppSettings.Settings.Add("Notification:PathTemplate", String.Format("{0}\\App_Data\\Notifications", configFile));
            }
            else
            {
                webConfig.AppSettings.Settings["Notification:PathTemplate"].Value = String.Format("{0}\\App_Data\\Notifications", configFile);
            }

            #endregion AppSettings

            webConfig.Save();

            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Installer_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                AddConfigurationFileDetails();
            }
            catch (Exception ex)
            {
                Rollback(e.SavedState);
                MessageBox.Show("AfterInstall: " + ex.Message);
            }
        }
    }
}
