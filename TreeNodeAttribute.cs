using System;

namespace ShaderEditor
{
	public class TreeNodeAttribute : Attribute
	{
		public TreeNodeAttribute()
		{
			Hide = false;
		}

		//public TreeNodeAttribute( bool bHide)
		//{
		//  Hide = bHide;
		//}

		public bool Hide { get; set; }
	}
}
