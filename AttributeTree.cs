using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace ShaderEditor
{
  public class AttributeTree : TreeView
  {
    /// <summary>
    /// Populate a new tree from an object, and recurse through any IEnumerable properties
    /// </summary>
    /// <typeparam name="TAttribute">Attribute that signifies that the property should be added to the tree</typeparam>
    /// <param name="item">root item from your object model</param>
    /// <param name="sPropertyForText">Name of the property to call to get the text for the treenode</param>
    public void Populate<TAttribute>(object item, string sPropertyForText)
      where TAttribute : Attribute
    {
      BeginUpdate();

      Nodes.Clear();

      Populate<TAttribute>(item, null, sPropertyForText);

      EndUpdate();
    }

    /// <summary>
    /// Add a new tree object to the tree, and recurse through any IEnumerable properties
    /// We could of sorted the nodes, but decided that it's not the ideal place to do that here, the order is taken
    /// from the order they are listed in the class file.
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TAttribute">Attribute that signifies that the property should be added to the tree</typeparam>
    /// <param name="item">root item from your object model</param>
    /// <param name="tnParent">Parent tree node</param>
    /// <param name="sPropertyForText">Name of the property to call to get the text for the treenode</param>
    public void Populate<TAttribute>(object item, TreeNode tnParent, string sPropertyForText)
      where TAttribute : Attribute
    {
      BeginUpdate();

      TreeNode tn = Add<TAttribute>(item, tnParent, sPropertyForText);

      if (null != tn)
      {
        Nodes.Add(tn);
      }

      EndUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="item"></param>
    /// <param name="treeNodeParent"></param>
    /// <param name="sPropertyForText"></param>
    /// <returns></returns>
    private TreeNode Add<TAttribute>(object item, TreeNode treeNodeParent, string sPropertyForText)
      where TAttribute : Attribute
    {
      if (null != item)
      {
        // See if we can get the property sPropertyForText from the item
        PropertyInfo propertyInfoForText = item.GetType().GetProperty(sPropertyForText);

        // if we can't access the property then it might not be a valid object, or not a valid property so return.
        if (null == propertyInfoForText)
        {
          return null;
        }

        // Create a new tree node
        TreeNode treeNode = new TreeNode(propertyInfoForText.GetValue(item, null).ToString());

        // Store a reference to the actual object as the TreeNode's Tag property
        treeNode.Tag = item;

        // if there's a valid parent add the new treenode (tn) to the parent
        if (null != treeNodeParent)
        {
          treeNodeParent.Nodes.Add(treeNode);
        }

        // Is item a array or container of objects? i.e. if it implements IEnumerable we can enumerate over
        // those objects to see if they can be added to the tree.
        IEnumerable enumerableObject = item as IEnumerable;

        if (null != enumerableObject)
        {
          foreach (object itemInEnumerable in enumerableObject)
          {
            Add<TAttribute>(itemInEnumerable, treeNode, sPropertyForText);
          }
        }

        // Get the object's properties and see if there are any that have the attribute TreeNodeAttribute assigned to it
        PropertyInfo[] propertyInfos = item.GetType().GetProperties();

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
          // Check all attribs available on the property
          object[] attribs = propertyInfo.GetCustomAttributes(false);

          foreach (TAttribute treeNodeAttribute in attribs)
          {
            // Try and add the return value of the property to the tree,
            // if the property returns an object that is not an instance of INamedObjectItem then it will be null which is
            // caught at the begining of this method.
            Add<TAttribute>(propertyInfo.GetValue(item, null), treeNode, sPropertyForText);
          }
        }

        return treeNode;
      }

      return null;
    }

  }
}
