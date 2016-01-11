using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Helper;
using Artech.Architecture.UI.Framework.Services;
using Artech.Common.Framework.Commands;
using Artech.Genexus.Common.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abstracta.Packages.GeneraWebxml
{
    class CommandManager : CommandDelegator
    {
        public CommandManager()
        {
            AddCommand(CommandKeys.GeneraWebxmlCommand, new ExecHandler(ExecGeneraWebxmlCommand), new QueryHandler(QueryGeneraWebxmlCommand));
        }

        // This is where you implement whatever you want to do
        // when this command is invoked
        public bool ExecGeneraWebxmlCommand(CommandData cmdData)
        {
            DateTime inicio = DateTime.Now;
            IKBService kbserv = UIServices.KB;

            if (kbserv != null && kbserv.CurrentKB != null)
            {
                try
                {
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.Filter = "web.xml file (web.xml)|";
                    SFD.FileName = "web.xml";
                    DialogResult Res = SFD.ShowDialog();
                    if (Res == DialogResult.OK)
                    {
                        string filename = SFD.FileName;
                        if (!filename.ToLower().EndsWith("xml"))
                        {
                            filename = filename + ".xml";
                        }
                        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                            "<web-app xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                            "xmlns=\"http://java.sun.com/xml/ns/javaee\" " +
                            "xmlns:web=\"http://java.sun.com/xml/ns/j2ee/web-app_2_4.xsd\" " +
                            "xsi:schemaLocation=\"http://java.sun.com/xml/ns/j2ee http://java.sun.com/xml/ns/j2ee/web-app_2_4.xsd\" " +
                            "version=\"2.4\">\n" +
                            "<listener><listener-class>com.genexus.webpanels.ServletEventListener</listener-class></listener>\n" +
                            "<listener><listener-class>com.genexus.webpanels.SessionEventListener</listener-class></listener>\n";

                        KBModel model = kbserv.CurrentKB.WorkingEnvironment.DesignModel;
                        string servletTemplate = "<servlet-mapping><servlet-name>{objName}</servlet-name><url-pattern>/{objName}</url-pattern> </servlet-mapping> \n" +
                                "<servlet> <servlet-name>{objName}</servlet-name><servlet-class>{objName}</servlet-class> </servlet>\n";
                        foreach (KBObject obj in Transaction.GetAll(model))
                        {
                            string servlet = servletTemplate.Replace("{objName}", obj.Name);
                            xml += servlet;
                        }
                        foreach (KBObject obj in WebPanel.GetAll(model))
                        {
                            string servlet = servletTemplate.Replace("{objName}", obj.Name);
                            xml += servlet;
                        }
                        xml += "</web-app>";
                        File.WriteAllText(filename, xml);
                        MessageBox.Show("Archivo generado en " + SFD.FileName);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error while exporting the KB");
                }
            }

            return true;
        }

        private bool QueryGeneraWebxmlCommand(CommandData cmdData, ref CommandStatus status)
        {
            // This is where you have a chance to modify the status of
            // menu / toolbar items.
            status.State = CommandState.Disabled;

            IKBService kbserv = UIServices.KB;
            if (kbserv != null && kbserv.CurrentKB != null)
            {
                status.State = CommandState.Enabled;
            }

            // return true to indicate you already resolved the command status;
            // otherwise the framework will try with its next registered
            // command target
            return true;
        }
    }
}
