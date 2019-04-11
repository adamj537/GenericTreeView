using System;
using System.Drawing;

namespace GenericTreeView
{
	[Serializable]
	public class ImageBackgroundItem : NamedObjectItem
	{
		public ImageBackgroundItem(string sName)
		  : base(sName)
		{
		}

		public Image Image { get; set; }
	}
}
