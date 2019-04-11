using System;

namespace GenericTreeView
{
	[Serializable]
	public class NamedObjectItem : INamedObjectItem
	{
		public NamedObjectItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}
