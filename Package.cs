using Artech.Architecture.Common.Services;
using Artech.Architecture.UI.Framework.Packages;
using System;
using System.Runtime.InteropServices;

namespace Abstracta.Packages.GeneraWebxml
{
    [Guid("e0bb3f35-dd2b-46ee-85e8-60ce46577c12")]
    public class Package : AbstractPackageUI
    {
        public static Guid guid = typeof(Package).GUID;

        public override string Name
        {
            get
            {
                return "GeneraWebxml";
            }
        }

        public override void Initialize(IGxServiceProvider services)
        {
            base.Initialize(services);

            LoadCommandTargets();
        }

        private void LoadCommandTargets()
        {
            AddCommandTarget(new CommandManager());
        }
    }
}

