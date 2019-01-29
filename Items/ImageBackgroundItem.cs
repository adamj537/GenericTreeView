using System;
using System.Drawing;

namespace ShaderEditor
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
