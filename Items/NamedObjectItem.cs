using System;

namespace ShaderEditor
{
	[Serializable]
	public class NamedObjectItem : INamedObjectItem, IObjectItem
	{
		public NamedObjectItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}
