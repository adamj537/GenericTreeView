using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
