using System;

namespace GenericTreeView
{
	[Serializable]
	public class ProjectItem : NamedObjectItem
	{
		public ProjectItem(string sName)
		  : base(sName)
		{
			Assemblies = new ContainerItem<AssemblyItem>("Assemblies");
			Backgrounds = new ContainerItem<BackgroundItem>("Backgrounds");
			MaterialSetsItem = new ContainerItem<ContainerItem<MaterialItem>>("Material Sets");
		}

		[TreeNode]
		public ContainerItem<AssemblyItem> Assemblies { get; set; }

		[TreeNode]
		public ContainerItem<BackgroundItem> Backgrounds { get; set; }

		[TreeNode(Hide = true)]
		public ContainerItem<ContainerItem<MaterialItem>> MaterialSetsItem { get; set; }
	}
}
