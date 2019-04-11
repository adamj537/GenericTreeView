using System;
using System.Drawing;

namespace GenericTreeView
{
	[Serializable]
	public class GradientBackgroundItem : NamedObjectItem, IBackgroundItem
	{
		public GradientBackgroundItem(string sName, Color color1, Color color2)
		  : base(sName)
		{
			Color1 = color1;
			Color2 = color2;
		}

		[TreeNode]
		public Color Color1 { get; set; }

		[TreeNode]
		public Color Color2 { get; set; }
	}
}
