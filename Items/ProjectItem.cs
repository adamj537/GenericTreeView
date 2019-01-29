using System;

namespace ShaderEditor
{
	[Serializable]
	public class ProjectItem : NamedObjectItem
	{
		public ProjectItem(string sName)
		  : base(sName)
		{
			Assemblies = new ContainerItem<AssemblyItem>("Assemblies");
			Backgrounds = new ContainerItem<IBackgroundItem>("Backgrounds");
			MaterialSetsItem = new ContainerItem<ContainerItem<MaterialItem>>("Material Sets");
		}

		[TreeNodeAttribute]
		public ContainerItem<AssemblyItem> Assemblies { get; set; }

		[TreeNodeAttribute]
		public ContainerItem<IBackgroundItem> Backgrounds { get; set; }

		[TreeNodeAttribute(Hide = true)]
		public ContainerItem<ContainerItem<MaterialItem>> MaterialSetsItem { get; set; }
	}
}
