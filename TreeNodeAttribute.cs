using System;

namespace GenericTreeView
{
	public class TreeNodeAttribute : Attribute
	{
		public TreeNodeAttribute()
		{
			Hide = false;
		}

		public bool Hide { get; set; }
	}
}
