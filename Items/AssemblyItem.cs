using System;

namespace ShaderEditor
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

		[TreeNodeAttribute]
		public ContainerItem<ComponentItem> Components { get; set; }
	}
}
