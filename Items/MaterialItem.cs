using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShaderEditor
{
  [Serializable]
  public class MaterialItem : NamedObjectItem
  {
    public MaterialItem(string sName)
      : base(sName)
    {
    }
  }
}
