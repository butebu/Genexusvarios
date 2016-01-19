using Artech.Common.Framework.Commands;

namespace GXPowerCommands
{
	public class CommandKeys
	{
		static CommandKeys()
		{
			OpenWindowsAppCommand = new CommandKey(Package.guid, "OpenWindowsApp");
			OpenAndroidAppCommand = new CommandKey(Package.guid, "OpenAndroidApp");
			OpeniOSAppCommand = new CommandKey(Package.guid, "OpeniOSApp");
		}

		private static CommandKey helloCmd = new CommandKey(Package.guid, "HelloWorld");
		public static CommandKey HelloCommand
		{
			get { return helloCmd; }
		}

		private static CommandKey hardDCmd = new CommandKey(Package.guid, "HardDelete");
		public static CommandKey HardDeleteCommand
		{
			get { return hardDCmd; }
		}

		private static CommandKey buildFCmd = new CommandKey(Package.guid, "BuildFolder");
		public static CommandKey BuildFolderCommand
		{
			get { return buildFCmd; }
		}

		private static CommandKey rbuildFCmd = new CommandKey(Package.guid, "RebuildFolder");
		public static CommandKey RebuildFolderCommand
		{
			get { return rbuildFCmd; }
		}

		private static CommandKey promptCmd = new CommandKey(Package.guid, "CommandPromptKB");
		public static CommandKey PromptCommandKB
		{
			get { return promptCmd; }
		}

		private static CommandKey exploreCmd = new CommandKey(Package.guid, "ExploreKB");
		public static CommandKey ExploreCommandKB
		{
			get { return exploreCmd; }
		}

		private static CommandKey promptCmdEnv = new CommandKey(Package.guid, "CommandPromptEnv");
		public static CommandKey PromptCommandEnv
		{
			get { return promptCmdEnv; }
		}

		private static CommandKey exploreCmdEnv = new CommandKey(Package.guid, "ExploreEnv");
		public static CommandKey ExploreCommandEnv
		{
			get { return exploreCmdEnv; }
		}

		private static CommandKey rebuildRunCmd = new CommandKey(Package.guid, "RebuildRun");
		public static CommandKey RebuildRunCommand
		{
			get { return rebuildRunCmd; }
		}

		private static CommandKey runAsIsCmd = new CommandKey(Package.guid, "RunAsIs");
		public static CommandKey RunAsIsCommand
		{
			get { return runAsIsCmd; }
		}

		private static CommandKey removeVariablesCmd = new CommandKey(Package.guid, "RemoveVariables");
		public static CommandKey RemoveVariablesCommand
		{
			get { return removeVariablesCmd; }
		}

		public static CommandKey OpenWindowsAppCommand { get; }
		public static CommandKey OpenAndroidAppCommand { get; }
		public static CommandKey OpeniOSAppCommand { get; }
	}
}
