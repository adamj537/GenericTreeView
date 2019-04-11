using System;
using System.Drawing;

namespace GenericTreeView
{
	[Serializable]
	public class ImageBackgroundItem : NamedObjectItem, IBackgroundItem
	{
		public ImageBackgroundItem(string sName)
		  : base(sName)
		{
		}

		public Image Image { get; set; }
	}
}
