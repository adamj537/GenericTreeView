using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GenericTreeView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Inspiration:  https://www.codeproject.com/Articles/23297/Generic-TreeView

			// This a sample object model

			// main project item
			ProjectItem projectItem = new ProjectItem("Test Project");

			// create some assemblies
			AssemblyItem assemblyItem1 = new AssemblyItem("Assembly 1");
			AssemblyItem assemblyItem2 = new AssemblyItem("Assembly 2");
			AssemblyItem assemblyItem3 = new AssemblyItem("Assembly 3");

			// add some components to the assemblies
			assemblyItem1.Components.Add(new ComponentItem("Component 1"));
			assemblyItem1.Components.Add(new ComponentItem("Component 2"));
			assemblyItem1.Components.Add(new ComponentItem("Component 3"));
			assemblyItem2.Components.Add(new ComponentItem("Component 4"));
			assemblyItem2.Components.Add(new ComponentItem("Component 5"));
			assemblyItem3.Components.Add(new ComponentItem("Component 6"));

			// add the assemblies to the project item
			projectItem.Assemblies.Add(assemblyItem1);
			projectItem.Assemblies.Add(assemblyItem2);
			projectItem.Assemblies.Add(assemblyItem3);

			// add a background to the backgrounds of the main project item
			projectItem.Backgrounds.Add(new GradientBackgroundItem("Cool Blue", Color.Beige, Color.Aqua));

			// add project item to tree...
			m_ProjectTree.Populate<TreeNodeAttribute>(projectItem, "Name");

			// Binary format
			BinaryFormatter bf = new BinaryFormatter();
			Stream fileStream = new FileStream(@"C:\\Users\\ajohnson\\Desktop\\test.dat", FileMode.Create, FileAccess.Write, FileShare.None);

			bf.Serialize(fileStream, projectItem);

			fileStream.Close();
		}

	}
}
