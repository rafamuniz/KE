using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace KarmicEnergy.Web
{
    [RunInstaller(true)]
    public partial class WebInstaller : System.Configuration.Install.Installer
    {
        public WebInstaller()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            try
            {
                AddConfigurationFileDetails();
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao atualizar o arquivo de configuração da aplicação: " + e.Message);
                base.Rollback(savedState);
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        private void WebInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_AfterRollback(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_BeforeRollback(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {

        }

        private void WebInstaller_Committed(object sender, InstallEventArgs e)
        {
        }

        private void WebInstaller_Committing(object sender, InstallEventArgs e)
        {

        }

        /// <summary>
        /// /SITEID="[SITEID]" /INTERVAL="[INTERVAL]" /PORTNAME="[PORTNAME]" /BAUDRATE="[BAUDRATE]" /PARITY="[PARITY]" /DATABITS="[DATABITS]" /STOPBITS="[STOPBITS]" /READTIMEOUT="[READTIMEOUT]"
        /// </summary>
        private void showParameters()
        {
            StringBuilder sb = new StringBuilder();
            StringDictionary myStringDictionary = this.Context.Parameters;

            if (this.Context.Parameters.Count > 0)
            {
                foreach (string myString in this.Context.Parameters.Keys)
                {
                    sb.AppendFormat("String={0} Value={1}\n", myString, this.Context.Parameters[myString]);
                }
            }
        }

        private void AddConfigurationFileDetails()
        {
            try
            {
                string siteId = Context.Parameters["SITEID"];
                string masterURL = Context.Parameters["MASTERURL"];

                // CONNECTION STRING
                string server = Context.Parameters["SERVER"];
                string databasename = Context.Parameters["DATABASENAME"];
                string username = Context.Parameters["USERNAME"];
                string password = Context.Parameters["PASSWORD"];

                // Get the path to the executable file that is being installed on the target computer  
                string assemblypath = Context.Parameters["assemblypath"];
                string configPath = assemblypath + ".config";

                MessageBox.Show(configPath);

                // Write the path to the app.config file  
                XmlDocument doc = new XmlDocument();
                doc.Load(configPath);

                XmlNode configuration = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "configuration")
                        configuration = node;

                if (configuration != null)
                {
                    // Get the ‘appSettings’ node  
                    XmlNode settingNode = null;
                    XmlNode connectionNode = null;
                    foreach (XmlNode node in configuration.ChildNodes)
                    {
                        if (node.Name == "appSettings")
                            settingNode = node;

                        if (node.Name == "connectionStrings")
                            connectionNode = node;
                    }

                    if (settingNode != null)
                    {
                        //Reassign values in the config file  
                        foreach (XmlNode node in settingNode.ChildNodes)
                        {
                            //MessageBox.Show("node.Value = " + node.Value);
                            if (node.Attributes == null)
                                continue;

                            XmlAttribute attribute = node.Attributes["value"];

                            if (node.Attributes["key"] != null)
                            {
                                switch (node.Attributes["key"].Value)
                                {
                                    case "Site:Id":
                                        attribute.Value = siteId;
                                        break;
                                }
                            }
                        }
                    }

                    if (connectionNode != null)
                    {
                        //Reassign values in the config file  
                        foreach (XmlNode node in connectionNode.ChildNodes)
                        {
                            if (node.Attributes == null)
                                continue;

                            XmlAttribute attribute = node.Attributes["connectionString"];

                            if (node.Attributes["name"] != null &&
                                node.Attributes["name"].Value == "KEConnection")
                            {
                                String configurationString = String.Format("Data Source={0};Initial Catalog={1};User={2};password={3};Integrated Security=false;", server, databasename, username, password);
                                attribute.Value = configurationString;
                            }
                        }
                    }

                    doc.Save(configPath);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
