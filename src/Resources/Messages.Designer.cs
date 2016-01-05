using System;
using System.Collections.Generic;

namespace GXPowerCommands.Resources
{
	/// <summary>
	/// A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	// This class was auto-generated by the StronglyTypedResourceBuilder
	// class via a tool like ResGen or Visual Studio.
	// To add or remove a member, edit your .ResX file then rerun ResGen
	// with the /str option, or rebuild your VS project.
	public static class Messages
	{
		private static global::Artech.Common.Localization.CachingResourceManager resourceMan;

		private static global::System.Globalization.CultureInfo resourceCulture;

		/// <summary>
		/// Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (global::System.Object.ReferenceEquals(resourceMan, null)) 
				{
					global::Artech.Common.Localization.CachingResourceManager temp = new global::Artech.Common.Localization.CachingResourceManager("GXPowerCommands.Resources.Messages", typeof(Messages).Assembly);
					resourceMan = temp;
				}

				return resourceMan;
			}
		}

		/// <summary>
		/// Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class.
		/// </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Globalization.CultureInfo Culture 
		{
			get { return resourceCulture; }
			set { resourceCulture = value; }
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Are you sure?'
		/// </summary>
		public static System.String AreYouSure
		{
			get
			{
				return (System.String)ResourceManager.GetString("AreYouSure", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Build folder objects'
		/// </summary>
		public static System.String BuildFolder
		{
			get
			{
				return (System.String)ResourceManager.GetString("BuildFolder", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Command prompt here'
		/// </summary>
		public static System.String CommandPrompt
		{
			get
			{
				return (System.String)ResourceManager.GetString("CommandPrompt", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'This command will try to delete every object in the selected folder and the fodler itself. Are you sure you want to proceed?'
		/// </summary>
		public static System.String EmptyDeleteWarning
		{
			get
			{
				return (System.String)ResourceManager.GetString("EmptyDeleteWarning", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Windows Explorer here'
		/// </summary>
		public static System.String Explore
		{
			get
			{
				return (System.String)ResourceManager.GetString("Explore", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Locate in folder view'
		/// </summary>
		public static System.String FolderView
		{
			get
			{
				return (System.String)ResourceManager.GetString("FolderView", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Genexus Power Commands'
		/// </summary>
		public static System.String GXPowerCommands
		{
			get
			{
				return (System.String)ResourceManager.GetString("GXPowerCommands", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Empty and delete folder'
		/// </summary>
		public static System.String HardDelete
		{
			get
			{
				return (System.String)ResourceManager.GetString("HardDelete", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Say Hello!'
		/// </summary>
		public static System.String HelloWorld
		{
			get
			{
				return (System.String)ResourceManager.GetString("HelloWorld", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Directory &apos;{0}&apos; does not exist yet. Make sure you build your KB in this envirobnment at least once.'
		/// </summary>
		public static System.String InvalidDir
		{
			get
			{
				return (System.String)ResourceManager.GetString("InvalidDir", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the string resource corresponding to 'Directory &apos;{0}&apos; does not exist yet. Make sure you build your KB in this envirobnment at least once.', fomatted using the specified arguments.
		/// </summary>
		public static string FormatInvalidDir(object arg0)
		{
			return FormatInvalidDir(Culture, arg0);
		}

		/// <summary>
		/// Returns the string resource corresponding to 'Directory &apos;{0}&apos; does not exist yet. Make sure you build your KB in this envirobnment at least once.', fomatted using the specified arguments.
		/// </summary>
		public static string FormatInvalidDir(IFormatProvider provider, object arg0)
		{
			return String.Format(provider, InvalidDir, arg0);
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Power Commands'
		/// </summary>
		public static System.String PowerCommands
		{
			get
			{
				return (System.String)ResourceManager.GetString("PowerCommands", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Rebuild folder objects'
		/// </summary>
		public static System.String RebuildFolder
		{
			get
			{
				return (System.String)ResourceManager.GetString("RebuildFolder", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Rebuild and run'
		/// </summary>
		public static System.String RebuildRun
		{
			get
			{
				return (System.String)ResourceManager.GetString("RebuildRun", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Remove unused variables'
		/// </summary>
		public static System.String RemoveVariables
		{
			get
			{
				return (System.String)ResourceManager.GetString("RemoveVariables", resourceCulture);
			}
		}

		/// <summary>
		/// Returns the System.String resource corresponding to 'Run as is'
		/// </summary>
		public static System.String RunAsIs
		{
			get
			{
				return (System.String)ResourceManager.GetString("RunAsIs", resourceCulture);
			}
		}
	}
}