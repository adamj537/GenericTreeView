using System;

namespace GenericTreeView
{
	[Serializable]
	public class AssemblyItem : NamedObjectItem
	{
		public AssemblyItem()
		  : this("Assembly")
		{
		}

		public AssemblyItem(string sName)
		  : base(sName)
		{
			Components = new ContainerItem<ComponentItem>("Components");
		}

		[TreeNode]
		public ContainerItem<ComponentItem> Components { get; set; }
	}
}
