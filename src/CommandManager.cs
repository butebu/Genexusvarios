using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Artech.Architecture.Common.Helpers;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.Common.Services;
using Artech.Architecture.UI.Framework.Controls;
using Artech.Architecture.UI.Framework.Helper;
using Artech.Architecture.UI.Framework.Services;
using Artech.Common.Framework.Commands;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Entities;
using Artech.Genexus.Common.Parts;
using Artech.Genexus.Common.Services;
using Artech.Udm.Framework;
using GXPowerCommands.Resources;

namespace GXPowerCommands
{
	public class CommandManager : CommandDelegator
	{
		readonly string WINDOWS = "Windows";
		readonly string ANDROID = "Android";
		readonly string IOS = "iOS";

		public CommandManager()
		{
			AddCommand(CommandKeys.HelloCommand, ExecSayHello, QuerySayHello);
			AddCommand(CommandKeys.HardDeleteCommand, ExecuteHardDelete, QueryIsFolderSelected);
			AddCommand(CommandKeys.BuildFolderCommand, ExecuteBuildFolder, QueryIsFolderSelected);
			AddCommand(CommandKeys.RebuildFolderCommand, ExecuteRebuildFolder, QueryIsFolderSelected);
			AddCommand(CommandKeys.PromptCommandKB, ExecutePromptInKBFolder, QueryIsKBSelected);
			AddCommand(CommandKeys.ExploreCommandKB, ExecuteExploreKBFolder, QueryIsKBSelected);
			AddCommand(CommandKeys.PromptCommandEnv, ExecutePromptInEnvFolder, QueryIsEnvSelected);
			AddCommand(CommandKeys.ExploreCommandEnv, ExecuteExploreEnvFolder, QueryIsEnvSelected);
			AddCommand(CommandKeys.RebuildRunCommand, ExecuteRebuildAndRun, QueryIsMainObjectSelected);
			AddCommand(CommandKeys.RunAsIsCommand, ExecuteRunAsIs, QueryIsMainObjectSelected);
			AddCommand(CommandKeys.RemoveVariablesCommand, ExecuteRemoveVariables, QueryIsObjectSelected);

			AddCommand(CommandKeys.OpenWindowsAppCommand, ExecuteOpenWindowsApp, QueryOpenWindowsApp);
			AddCommand(CommandKeys.OpenAndroidAppCommand, ExecuteOpenAndroidApp, QueryOpenAndroidApp);
			AddCommand(CommandKeys.OpeniOSAppCommand, ExecuteOpeniOSApp, QueryOpeniOSApp);
		}

		public bool ExecSayHello(CommandData data)
		{
			MessageBox.Show(string.Format("Hello {0}", Environment.UserName));
			return true;
		}

		private bool QuerySayHello(CommandData data, ref CommandStatus status)
		{
#if DEBUG
			status.State = CommandState.Enabled;
#else
			status.State = CommandState.Invisible;
#endif
			return true;
		}

		private bool QueryIsObjectSelected(CommandData data, ref CommandStatus status)
		{
			status.State = CommandState.Disabled;
			KBObject obj = null;
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				if (!IsObjectSelected(data, false, out obj))
					return true;

				data.Context = obj;
				status.State = CommandState.Enabled;
			}
			return true;
		}

		public bool ExecuteRemoveVariables(CommandData data)
		{
			KBObject obj = data.Context as KBObject;
			if (obj == null)
				return true;

			foreach (KBObjectPart p in obj.Parts)
			{
				if (p is VariablesPart)
				{
					VariablesPart vars = p as VariablesPart;
					foreach (var variable in vars.Variables.Where(v => !v.IsStandard))
					{
						CommonServices.Output.AddLine(string.Format("var {0} will be deleted", variable.Name));
					}
					break;
				}
			}

			return true;
		}


		public bool ExecuteHardDelete(CommandData data)
		{
			if (MessageBox.Show(Resources.Messages.EmptyDeleteWarning, Resources.Messages.AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
				return true;

			IModelTree tree = data.Context as IModelTree;
			if (tree == null)
				return true;

			bool success = false;
			try
			{
				CommonServices.Output.StartSection(Resources.Messages.HardDelete);

				List<KBObject> parents = new List<KBObject>();
				foreach (KBObject obj in tree.SelectedObjects)
					parents.Add(obj);

				DeleteObjectAndReferences(UIServices.KB.CurrentModel, parents);

				success = true;
			}
			catch (Exception ex)
			{
				CommonServices.Output.AddErrorLine(ex.Message);
			}
			finally
			{
				CommonServices.Output.EndSection(Resources.Messages.HardDelete, success);
			}

			return true;
		}

		private void DeleteObjectAndReferences(KBModel model, List<KBObject> parents)
		{
			parents.ForEach(p => parents.AddRange(model.Objects.GetAllChildren(p)));
			KBObject.Delete(parents);
		}

		private bool QueryIsFolderSelected(CommandData data, ref CommandStatus status)
		{
			status.State = CommandState.Disabled;
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				IModelTree tree = data.Context as IModelTree;
				if (tree == null || !(tree.SelectedObject is KBObject))
					return true;

				foreach (KBObject obj in tree.SelectedObjects)
					if (obj.Type != typeof(Folder).GUID)
						return true;

				status.State = CommandState.Enabled;
			}
			return true;
		}

		public bool ExecuteBuildFolder(CommandData data)
		{
			IModelTree tree = data.Context as IModelTree;
			if (tree == null)
				return true;

			bool success = false;
			try
			{
				CommonServices.Output.StartSection(Resources.Messages.BuildFolder);

				List<EntityKey> keys = GetAllObjectsKeys(tree.SelectedObjects.OfType<KBObject>());

				if (keys == null)
					return true;

				GenexusUIServices.Build.Build(keys);

				success = true;
			}
			catch (Exception ex)
			{
				CommonServices.Output.AddErrorLine(ex.Message);
			}
			finally
			{
				CommonServices.Output.EndSection(Resources.Messages.BuildFolder, success);
			}

			return true;
		}

		public bool ExecuteRebuildFolder(CommandData data)
		{
			IModelTree tree = data.Context as IModelTree;
			if (tree == null)
				return true;

			bool success = false;
			try
			{
				CommonServices.Output.StartSection(Resources.Messages.BuildFolder);

				List<EntityKey> keys = GetAllObjectsKeys(tree.SelectedObjects.OfType<KBObject>());

				if (keys == null || keys.Count == 0)
					return true;

				keys.ForEach(k => GenexusUIServices.Build.Rebuild(k));

				success = true;
			}
			catch (Exception ex)
			{
				CommonServices.Output.AddErrorLine(ex.Message);
			}
			finally
			{
				CommonServices.Output.EndSection(Resources.Messages.BuildFolder, success);
			}

			return true;
		}

		List<EntityKey> GetAllObjectsKeys(IEnumerable<KBObject> objects)
		{
			List<KBObject> parents = new List<KBObject>(objects);
			if (parents.Count == 0)
				return null;

			List<EntityKey> keys = new List<EntityKey>();
			parents.ForEach(o => keys.AddRange(UIServices.KB.CurrentModel.Objects.GetAllChildren(o).Select(ch => ch.Key)));

			if (keys.Count == 0)
				return null;
			keys.RemoveAll(k => k.Type == typeof(Folder).GUID);

			return keys;
		}

		public bool ExecutePromptInKBFolder(CommandData data)
		{
			ExecuteCommandPrompt(UIServices.KB.CurrentKB.Location);

			return true;
		}

		private bool QueryIsKBSelected(CommandData data, ref CommandStatus status)
		{
			status.State = CommandState.Disabled;
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				Artech.Common.Framework.Selection.Selection selection = data.Context as Artech.Common.Framework.Selection.Selection;
				if (selection == null)
					return true;

				Artech.Architecture.Common.Objects.KBProperties props = selection.SelectedObject as Artech.Architecture.Common.Objects.KBProperties;
				if (props == null)
					return true;

				status.State = CommandState.Enabled;
			}
			return true;
		}

		public bool ExecuteExploreKBFolder(CommandData data)
		{
			ExecuteExplorer(UIServices.KB.CurrentKB.Location);

			return true;
		}

		private bool QueryIsEnvSelected(CommandData data, ref CommandStatus status)
		{
			status.State = CommandState.Disabled;
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				Artech.Common.Framework.Selection.Selection selection = data.Context as Artech.Common.Framework.Selection.Selection;
				if (selection == null)
					return true;

				KBModel env = selection.SelectedObject as KBModel;
				if (env == null)
					return true;
				if (env.Type != ModelType.Prototype)
				{
					status.State = CommandState.Invisible;
					return true;
				}
				data.Context = env;

				status.State = CommandState.Enabled;
			}
			return true;
		}

		public bool ExecutePromptInEnvFolder(CommandData data)
		{
			KBModel env = data.Context as KBModel;
			if (env == null || env.Type != ModelType.Prototype)
				return true;

			ExecuteCommandPrompt(Path.Combine(UIServices.KB.CurrentKB.Location, env.TargetPath + "\\web"));

			return true;
		}

		public bool ExecuteExploreEnvFolder(CommandData data)
		{
			KBModel env = data.Context as KBModel;
			if (env == null || env.Type != ModelType.Prototype)
				return true;

			ExecuteExplorer(Path.Combine(UIServices.KB.CurrentKB.Location, env.TargetPath + "\\web"));

			return true;
		}

		void ExecuteCommandPrompt(string path)
		{
			if (!DirectoryExists(path))
				return;

			ProcessStartInfo info = new ProcessStartInfo("cmd");
			info.WorkingDirectory = path;
			info.UseShellExecute = true;
			Process.Start(info);
		}

		void ExecuteExplorer(string path)
		{
			if (!DirectoryExists(path))
				return;

			ProcessStartInfo info = new ProcessStartInfo("explorer.exe");
			info.Arguments = path;
			Process p = Process.Start(info);
		}

		bool DirectoryExists(string path)
		{
			if (!Directory.Exists(path))
			{
				MessageBox.Show(Messages.FormatInvalidDir(path), Messages.GXPowerCommands);
				return false;
			}

			return true;
		}

		private bool QueryIsMainObjectSelected(CommandData data, ref CommandStatus status)
		{
			status.State = CommandState.Disabled;
			KBObject obj = null;
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				if (!IsObjectSelected(data, true, out obj))
					return true;

				data.Context = obj;
				status.State = CommandState.Enabled;
			}
			return true;
		}

		public bool ExecuteRebuildAndRun(CommandData data)
		{
			KBObject obj = data.Context as KBObject;
			if (obj == null)
				return true;

			List<EntityKey> keys = new List<EntityKey>();
			keys.Add(obj.Key);

			GenexusUIServices.Build.Rebuild(obj.Key);

			BackgroundWorker worker = new BackgroundWorker();
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
			worker.DoWork += new DoWorkEventHandler(worker_DoWork);

			worker.RunWorkerAsync(obj);

			return true;
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			KBObject obj = e.Argument as KBObject;
			if (obj == null)
				return;

			e.Result = obj;

			while (GenexusUIServices.Build.IsBuilding)
				Thread.Sleep(500);
		}

		void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			KBObject obj = e.Result as KBObject;
			if (obj == null)
				return;

			GenexusBLServices.Run.Execute(UIServices.KB.CurrentModel, obj.Key);
		}

		public bool ExecuteRunAsIs(CommandData data)
		{
			KBObject obj = data.Context as KBObject;
			if (obj == null)
				return true;

			GenexusBLServices.Run.Execute(UIServices.KB.CurrentModel, obj.Key);

			return true;
		}

		private bool ExecuteOpenWindowsApp(CommandData cmdData)
		{
			KBObject obj = cmdData.Context as KBObject;
			if (obj == null)
				return true;

			ExecuteExplorer(GetObjectPath(obj, WINDOWS));

			return true;
		}

		private bool QueryOpenWindowsApp(CommandData cmdData, ref CommandStatus status)
		{
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				KBObject obj;
				if (!IsObjectSelected(cmdData, true, out obj))
					return true;

				if (IsOkToShow(obj, Properties.SMARTDEVICE.GenerateWindows))
				{
					cmdData.Context = obj;
					status.State = CommandState.Enabled;
				}
			}
			return true;
		}

		private bool ExecuteOpenAndroidApp(CommandData cmdData)
		{
			KBObject obj = cmdData.Context as KBObject;
			if (obj == null)
				return true;

			ExecuteExplorer(GetObjectPath(obj, ANDROID));

			return true;
		}

		private bool QueryOpenAndroidApp(CommandData cmdData, ref CommandStatus status)
		{
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				KBObject obj;
				if (!IsObjectSelected(cmdData, true, out obj))
					return true;

				if (IsOkToShow(obj, Properties.SMARTDEVICE.GenerateAndroid))
				{
					cmdData.Context = obj;
					status.State = CommandState.Enabled;
				}
			}
			return true;
		}

		private bool ExecuteOpeniOSApp(CommandData cmdData)
		{
			KBObject obj = cmdData.Context as KBObject;
			if (obj == null)
				return true;

			ExecuteExplorer(GetObjectPath(obj, IOS));

			return true;
		}

		private bool QueryOpeniOSApp(CommandData cmdData, ref CommandStatus status)
		{
			if (UIServices.KB != null && UIServices.KB.CurrentKB != null)
			{
				KBObject obj;
				if (!IsObjectSelected(cmdData, true, out obj))
					return true;

				if (IsOkToShow(obj, Properties.SMARTDEVICE.GenerateIos))
				{
					cmdData.Context = obj;
					status.State = CommandState.Enabled;
				}
			}
			return true;
		}

		bool IsOkToShow(KBObject obj, string generator)
		{
			if (obj == null)
				return false;

			if (obj.Type != ObjClass.SDPanel && obj.Type != ObjClass.Dashboard)
				return false;

			GxEnvironment environment = UIServices.KB.WorkingEnvironment.TargetModel.GetAs<GxModel>().Environments.FirstOrDefault(env => env.Generator == (int)GeneratorType.SmartDevices);

			return environment != null && environment.Properties.GetPropertyValue<bool>(generator);
		}

		bool IsObjectSelected(CommandData data, bool mustBeMain, out KBObject obj)
		{
			obj = null;
			if (data.Context == null)
				return false;

			obj = KBObjectSelectionHelper.TryGetOnlyOneKBObjectFrom(data.Context);
			if (obj != null && mustBeMain)
				return obj.GetPropertyValue<bool>("IsMain");

			return true;
		}

		string GetObjectPath(KBObject obj, string platform)
		{
			return Path.Combine(Path.Combine(Path.Combine(Path.Combine(UIServices.KB.CurrentKB.Location, UIServices.KB.CurrentModel.Environment.TargetModel.TargetPath), "mobile"), platform), obj.Name);
		}
	}

}
