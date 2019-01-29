using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShaderEditor
{
  [Serializable]
  public class NamedObjectItem : INamedObjectItem, IObjectItem
  {
    public NamedObjectItem( string sName)
    {
      Name = sName;
    }
    
    #region INamedObject Members
    public string Name { get; set;}

    #endregion
  }
}
