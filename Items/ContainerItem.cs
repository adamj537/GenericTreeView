using System;
using System.Collections.Generic;

namespace GenericTreeView
{
	/// <summary>
	/// ContainerItem is a list and also implements INamedObjectItem, so the container can have a name
	/// This will save creating specific classes for each container
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ContainerItem<T> : List<T>, INamedObjectItem
	{
		public ContainerItem(string name)
		{
			Name = name;
		}

		[TreeNode]
		public string Name { get; set; }
	}
}
