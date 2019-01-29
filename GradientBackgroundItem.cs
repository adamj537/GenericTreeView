using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShaderEditor
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

    [TreeNodeAttribute]
    public Color Color1 { get; set; }

    [TreeNodeAttribute]
    public Color Color2 { get; set; }
  }
}
