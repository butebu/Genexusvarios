using System;
using System.Runtime.InteropServices;
using Artech.Architecture.Common.Services;
using Artech.Architecture.UI.Framework.Packages;

namespace GXPowerCommands
{
	[Guid("3184D541-7EFB-4beb-8AC8-50E223D3E693")]
	public class Package : AbstractPackageUI
	{
		public static Guid guid = typeof(Package).GUID;

		public override  string Name 
		{
			get { return GXPowerCommands.Resources.Messages.GXPowerCommands; } 
		}

		public override void Initialize(IGxServiceProvider services)
		{
			base.Initialize(services);
			AddCommandTarget(new CommandManager());
		}
	}
}
