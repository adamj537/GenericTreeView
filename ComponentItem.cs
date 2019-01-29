using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShaderEditor
{
  [Serializable]
  public class ComponentItem : NamedObjectItem
  {
    public ComponentItem()
      : base( "Component")
    {

    }

    public ComponentItem(string sName)
      : base( sName)
    {
    }
  }
}
