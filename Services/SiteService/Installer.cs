using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace KarmicEnergy.Service
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
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
                    sb.AppendFormat("String={0} Value= {1}\n", myString,
                    this.Context.Parameters[myString]);
                }
            }
        }

        private void AddConfigurationFileDetails()
        {
            try
            {
                string siteId = Context.Parameters["SITEID"];
                //string interval = Context.Parameters["INTERVAL"];

                string portName = Context.Parameters["PORTNAME"];
                //string baudRate = Context.Parameters["BAUDRATE"];
                //string parity = Context.Parameters["PARITY"];
                //string dataBits = Context.Parameters["DATABITS"];
                //string stopBits = Context.Parameters["STOPBITS"];
                //string readTimeout = Context.Parameters["READTIMEOUT"];

                // CONNECTION STRING
                string server = Context.Parameters["SERVER"];
                string databasename = Context.Parameters["DATABASENAME"];
                string username = Context.Parameters["USERNAME"];
                string password = Context.Parameters["PASSWORD"];

                // Get the path to the executable file that is being installed on the target computer  
                string assemblypath = Context.Parameters["assemblypath"];
                string configPath = assemblypath + ".config";

                // Write the path to the app.config file  
                XmlDocument doc = new XmlDocument();
                doc.Load(configPath);

                //MessageBox.Show(appConfigPath);

                XmlNode configuration = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "configuration")
                        configuration = node;

                if (configuration != null)
                {
                    //MessageBox.Show("configuration != null");  
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
                        //MessageBox.Show("settingNode != null");  
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
                                    //case "Interval":
                                    //    attribute.Value = interval;
                                    //    break;
                                    case "PortName":
                                        attribute.Value = portName;
                                        break;
                                        //case "BaudRate":
                                        //    attribute.Value = baudRate;
                                        //    break;
                                        //case "Parity":
                                        //    attribute.Value = parity;
                                        //    break;
                                        //case "DataBits":
                                        //    attribute.Value = dataBits;
                                        //    break;
                                        //case "StopBits":
                                        //    attribute.Value = stopBits;
                                        //    break;
                                        //case "ReadTimeout":
                                        //    attribute.Value = readTimeout;
                                        //    break;
                                }
                            }
                        }
                    }

                    if (connectionNode != null)
                    {
                        //MessageBox.Show("connectionNode != null");  
                        //Reassign values in the config file  
                        foreach (XmlNode node in connectionNode.ChildNodes)
                        {
                            //MessageBox.Show("node.Value = " + node.Value);
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

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
