using System;

namespace GenericTreeView
{
	[Serializable]
	public class ComponentItem : NamedObjectItem
	{
		public ComponentItem()
		  : base("Component")
		{

		}

		public ComponentItem(string sName)
		  : base(sName)
		{
		}
	}
}
