using System;
using System.Collections.Generic;
using System.Drawing;

namespace GenericTreeView
{
	[Serializable]
	public class MaterialItem : NamedObject
	{
		public MaterialItem(string name) : base(name)
		{

		}
	}

	[Serializable]
	public class ImageBackgroundItem : NamedObject
	{
		public ImageBackgroundItem(string name) : base(name)
		{

		}

		public Image Image { get; set; }
	}

	[Serializable]
	public class GradientBackgroundItem : BackgroundItem
	{
		public GradientBackgroundItem(string sName, Color color1, Color color2) : base(sName)
		{
			Color1 = color1;
			Color2 = color2;
		}

		[TreeNode]
		public Color Color1 { get; set; }

		[TreeNode]
		public Color Color2 { get; set; }
	}

	[Serializable]
	public class ComponentItem : NamedObject
	{
		public ComponentItem() : base("Component")
		{

		}

		public ComponentItem(string name) : base(name)
		{

		}
	}

	[Serializable]
	public class AssemblyItem : NamedObject
	{
		public AssemblyItem() : this("Assembly")
		{

		}

		public AssemblyItem(string sName) : base(sName)
		{
			Components = new ContainerItem<ComponentItem>("Components");
		}

		[TreeNode]
		public ContainerItem<ComponentItem> Components { get; set; }
	}

	[Serializable]
	public class BackgroundItem : NamedObject
	{
		public BackgroundItem(string name) : base(name)
		{

		}
	}

	/// <summary>
	/// ContainerItem is a list and also implements INamedObject, so the container can have a name
	/// This will save creating specific classes for each container
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ContainerItem<T> : List<T>, INamedObject
	{
		public ContainerItem(string name)
		{
			Name = name;
		}

		[TreeNode]
		public string Name { get; set; }
	}

	[Serializable]
	public class ProjectItem : NamedObject
	{
		public ProjectItem(string name) : base(name)
		{
			Assemblies = new ContainerItem<AssemblyItem>("Assemblies");
			Backgrounds = new ContainerItem<BackgroundItem>("Backgrounds");
			MaterialSetsItem = new ContainerItem<ContainerItem<MaterialItem>>("Material Sets");
		}

		[TreeNode]
		public ContainerItem<AssemblyItem> Assemblies { get; set; }

		[TreeNode]
		public ContainerItem<BackgroundItem> Backgrounds { get; set; }

		[TreeNode(Hide = true)]
		public ContainerItem<ContainerItem<MaterialItem>> MaterialSetsItem { get; set; }
	}
}
