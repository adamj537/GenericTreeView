using System;

namespace GenericTreeView
{
	[Serializable]
	public class NamedObject : INamedObject
	{
		public NamedObject(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}
