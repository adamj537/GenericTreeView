using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShaderEditor
{
  /// <summary>
  /// ContainerItem is a list and also implements INamedObjectItem, so the container can have a name
  /// This will save creating specific classes for each container
  /// </summary>
  /// <typeparam name="T"></typeparam>
  [Serializable]
  public class ContainerItem<T> : List<T>, INamedObjectItem
  {
    public ContainerItem(string sName)
    {
      Name = sName;
    }

    [TreeNodeAttribute]
    #region INamedObjectItem Members
    public string Name { get; set;}

    #endregion
  }
}
