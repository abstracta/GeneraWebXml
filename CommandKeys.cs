using Artech.Common.Framework.Commands;


namespace Abstracta.Packages.GeneraWebxml
{
    public class CommandKeys
    {
        private static CommandKey generaWebxmlCommand = new CommandKey(Package.guid, "GeneraWebxmlCommand");

        public static CommandKey GeneraWebxmlCommand { get { return generaWebxmlCommand; } }
    }
}
