using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace GenericTreeView
{
	public class AttributeTree : TreeView
	{
		/// <summary>
		/// Populate a new tree from an object, and recurse through any IEnumerable properties
		/// </summary>
		/// <typeparam name="TAttribute">Attribute that signifies that the property should be added to the tree</typeparam>
		/// <param name="item">root item from your object model</param>
		/// <param name="nameProperty">name of the property to call to get the text for the treenode</param>
		public void Populate<TAttribute>(object item, string nameProperty) where TAttribute : Attribute
		{
			BeginUpdate();

			Nodes.Clear();

			Populate<TAttribute>(item, null, nameProperty);

			EndUpdate();
		}

		/// <summary>
		/// Add a new tree object to the tree, and recurse through any IEnumerable properties
		/// </summary>
		/// <typeparam name="TAttribute">Attribute that signifies that the property should be added to the tree</typeparam>
		/// <param name="item">root item from your object model</param>
		/// <param name="parent">parent tree node</param>
		/// <param name="nameProperty">name of the property to call to get the text for the treenode</param>
		public void Populate<TAttribute>(object item, TreeNode parent, string nameProperty) where TAttribute : Attribute
		{
			BeginUpdate();

			TreeNode treeNode = Add<TAttribute>(item, parent, nameProperty);

			if (treeNode != null)
			{
				Nodes.Add(treeNode);
			}

			EndUpdate();
		}

		/// <summary>
		/// Create a new tree node from an object.
		/// </summary>
		/// <typeparam name="TAttribute"></typeparam>
		/// <param name="parent">parent node to attach this new node to</param>
		/// <param name="nameProperty">name of the property to call to get the text for the treenode</param>
		/// <returns></returns>
		private TreeNode Add<TAttribute>(object item, TreeNode parent, string nameProperty) where TAttribute : Attribute
		{
			TreeNode treeNode = null;

			// Check that the item is valid.
			if (item != null)
			{
				// See if we can get the item's name property.
				PropertyInfo namePropertyInfo = item.GetType().GetProperty(nameProperty);

				// Check that the item's name property is valid.
				if (namePropertyInfo != null)
				{
					// Create a new tree node and store a reference to the actual object as the Tag property.
					treeNode = new TreeNode(namePropertyInfo.GetValue(item, null).ToString())
					{
						Tag = item
					};

					// If there's a valid parent, add the new treenode to the parent.
					if (parent != null)
					{
						parent.Nodes.Add(treeNode);
					}

					// Is item a array or container of objects? i.e. if it implements IEnumerable we can enumerate over
					// those objects to see if they can be added to the tree.
					IEnumerable enumerableObject = item as IEnumerable;

					if (enumerableObject != null)
					{
						foreach (object itemInEnumerable in enumerableObject)
						{
							Add<TAttribute>(itemInEnumerable, treeNode, nameProperty);
						}
					}

					// Get the object's properties.
					PropertyInfo[] propertyInfos = item.GetType().GetProperties();

					// See if there are any that have the attribute TreeNodeAttribute assigned to it.
					foreach (PropertyInfo propertyInfo in propertyInfos)
					{
						// Check all attribs available on the property
						object[] attribs = propertyInfo.GetCustomAttributes(false);

						foreach (object a in attribs)
						{
							if (a is TAttribute ta)
							{
								// Try and add the return value of the property to the tree.
								// If the property returns an object that is not an instance
								// of INamedObjectItem then it will be null, which is
								// caught at the begining of this method.
								Add<TAttribute>(propertyInfo.GetValue(item, null), treeNode, nameProperty);
							}
						}
					}
				}
			}

			return treeNode;
		}

	}
}
