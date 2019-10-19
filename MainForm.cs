﻿using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GenericTreeView
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Populate a new tree from an object, and recurse through any IEnumerable properties
		/// </summary>
		/// <typeparam name="TAttribute">Attribute that signifies that the property should be added to the tree</typeparam>
		/// <param name="item">root item from your object model</param>
		/// <param name="nameProperty">name of the property to call to get the text for the treenode</param>
		public void Populate<TAttribute>(object item, string nameProperty) where TAttribute : Attribute
		{
			treeView1.BeginUpdate();

			treeView1.Nodes.Clear();

			Populate<TAttribute>(item, null, nameProperty);

			treeView1.EndUpdate();
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
			treeView1.BeginUpdate();

			TreeNode treeNode = Add<TAttribute>(item, parent, nameProperty);

			if (treeNode != null)
			{
				treeView1.Nodes.Add(treeNode);
			}

			treeView1.EndUpdate();
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
				// See if we can get the item's "name" property.
				string name = item.GetType().GetProperty(nameProperty)?.GetValue(item, null).ToString();

				// If there is no name property, use the name of the item's type instead.
				if (name == null)
				{
					name = item.GetType().Name.ToString();
				}

				// Create a new tree node and store a reference to the actual object as the Tag property.
				treeNode = new TreeNode(name)
				{
					Tag = item
				};

				// If there's a valid parent, add the new treenode to the parent.
				if (parent != null)
				{
					parent.Nodes.Add(treeNode);
				}

				// If the item is enumerable (i.e. an array or container of objects)...
				if (item is IEnumerable enumerableItem)
				{
					// Iterate over each object in the enumeration.
					foreach (object i in enumerableItem)
					{
						Add<TAttribute>(i, treeNode, nameProperty);
					}
				}

				// Get the object's properties.
				PropertyInfo[] propertyInfos = item.GetType().GetProperties();

				// For each property...
				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					// Fetch all attributes available on the property.
					object[] attribs = propertyInfo.GetCustomAttributes(false);

					// For each attribute...
					foreach (object a in attribs)
					{
						// If it is a TreeNodeAttribute...
						if (a is TAttribute ta)
						{
							var property = propertyInfo.GetValue(item, null);

							// Try and add the return value of the property to the tree.
							// If the property returns an object that is not an instance
							// of INamedObjectItem then it will be null, which is
							// caught at the begining of this method.
							Add<TAttribute>(property, treeNode, nameProperty);
						}
					}
				}
			}

			return treeNode;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Inspiration:  https://www.codeproject.com/Articles/23297/Generic-TreeView

			// This a sample object model

			// main project item
			ProjectItem projectItem = new ProjectItem("Test Project");

			// create some assemblies
			AssemblyItem assemblyItem1 = new AssemblyItem("Assembly 1");
			AssemblyItem assemblyItem2 = new AssemblyItem("Assembly 2");
			AssemblyItem assemblyItem3 = new AssemblyItem("Assembly 3");

			// add some components to the assemblies
			assemblyItem1.Components.Add(new ComponentItem("Component 1"));
			assemblyItem1.Components.Add(new ComponentItem("Component 2"));
			assemblyItem1.Components.Add(new ComponentItem("Component 3"));
			assemblyItem2.Components.Add(new ComponentItem("Component 4"));
			assemblyItem2.Components.Add(new ComponentItem("Component 5"));
			assemblyItem3.Components.Add(new ComponentItem("Component 6"));

			// add the assemblies to the project item
			projectItem.Assemblies.Add(assemblyItem1);
			projectItem.Assemblies.Add(assemblyItem2);
			projectItem.Assemblies.Add(assemblyItem3);

			// add a background to the backgrounds of the main project item
			projectItem.Backgrounds.Add(new GradientBackgroundItem("Cool Blue", Color.Beige, Color.Aqua));

			// Add the settings object to the tree view.
			Populate<TreeNodeAttribute>(projectItem, "Name");

			// Binary format
			BinaryFormatter bf = new BinaryFormatter();
			Stream fileStream = new FileStream(@"C:\\Users\\ajohnson\\Desktop\\test.dat", FileMode.Create, FileAccess.Write, FileShare.None);

			bf.Serialize(fileStream, projectItem);

			fileStream.Close();
		}

	}
}
