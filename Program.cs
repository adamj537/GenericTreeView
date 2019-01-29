using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace ShaderEditor
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {

      /*

       project
       * workspace
       * materials
       * views
       * environment
       *  lights
       *  reflection map
       * configurations
       * measurements
       * 
       
       */


      AssemblyItem assemblyItem1 = new AssemblyItem("Assembly 1");
      AssemblyItem assemblyItem2 = new AssemblyItem("Assembly 2");
      AssemblyItem assemblyItem3 = new AssemblyItem("Assembly 3");

      assemblyItem1.Components.Add(new ComponentItem("Component 1"));
      assemblyItem1.Components.Add(new ComponentItem("Component 2"));
      assemblyItem1.Components.Add(new ComponentItem("Component 3"));
      assemblyItem2.Components.Add(new ComponentItem("Component 4"));
      assemblyItem2.Components.Add(new ComponentItem("Component 5"));
      assemblyItem3.Components.Add(new ComponentItem("Component 6"));

      ProjectItem projectItem = new ProjectItem("Test Project");

      projectItem.Assemblies.Add(assemblyItem1);
      projectItem.Assemblies.Add(assemblyItem2);
      projectItem.Assemblies.Add(assemblyItem3);

      // Binary format
      BinaryFormatter bf = new BinaryFormatter();
      Stream fileStream = new FileStream(@"C:\\Users\\ajohnson\\Desktop\\test.dat", FileMode.Create, FileAccess.Write, FileShare.None);

      bf.Serialize(fileStream, projectItem);

      fileStream.Close();

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
