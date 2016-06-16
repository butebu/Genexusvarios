using System.Reflection;
using System.Runtime.InteropServices;
using Artech.Architecture.Common.Packages;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Power commands for common Genexus tasks")]
[assembly: AssemblyDescription("Power commands for common Genexus tasks")]
[assembly: AssemblyConfiguration("Just copy the assembly under the Packages folder of your GeneXus installation")]
[assembly: AssemblyCompany("@sebagomez")]
[assembly: AssemblyProduct("GeneXus Power Commands")]
[assembly: AssemblyCopyright("Copyright © @sebagomez 2011 - 2016")]
[assembly: AssemblyTrademark("™ GeneXus Power Commands is an unregistered trademark of @sebagomez")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
//[assembly: Guid("45038ef8-d8fb-4732-8c8c-d782402216f2")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

#if SALTO
[assembly: AssemblyVersion("4.2.0.*")]
[assembly: PackageCompatibility(Version = 96640)]
#else
[assembly: AssemblyVersion("3.0.0.*")]
[assembly: PackageCompatibility(Version = 86463)]
#endif
[assembly: PackageAttribute(typeof(GXPowerCommands.Package), Author = "@sebagomez", IsUIPackage = true)]
