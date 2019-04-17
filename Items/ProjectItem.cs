using System;
using System.Collections.Generic;
using System.Drawing;

namespace GenericTreeView
{
	[Serializable]
	public class MaterialItem : INamedObject
	{
		public MaterialItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}

	[Serializable]
	public class ImageBackgroundItem : INamedObject
	{
		public ImageBackgroundItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }

		public Image Image { get; set; }
	}

	[Serializable]
	public class GradientBackgroundItem : BackgroundItem
	{
		public GradientBackgroundItem(string name, Color color1, Color color2) : base(name)
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
	public class ComponentItem : INamedObject
	{
		public ComponentItem() : this("Component")
		{

		}

		public ComponentItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}

	[Serializable]
	public class AssemblyItem : INamedObject
	{
		public AssemblyItem() : this("Assembly")
		{

		}

		public AssemblyItem(string name)
		{
			Name = name;
			Components = new ContainerItem<ComponentItem>("Components");
		}

		public string Name { get; set; }

		[TreeNode]
		public ContainerItem<ComponentItem> Components { get; set; }
	}

	[Serializable]
	public class BackgroundItem : INamedObject
	{
		public BackgroundItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
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

		public string Name { get; set; }
	}

	[Serializable]
	public class ProjectItem : INamedObject
	{
		public ProjectItem(string name)
		{
			Name = name;
			Assemblies = new ContainerItem<AssemblyItem>("Assemblies");
			Backgrounds = new ContainerItem<BackgroundItem>("Backgrounds");
			MaterialSetsItem = new ContainerItem<ContainerItem<MaterialItem>>("Material Sets");
		}

		public string Name { get; set; }

		[TreeNode]
		public ContainerItem<AssemblyItem> Assemblies { get; set; }

		[TreeNode]
		public ContainerItem<BackgroundItem> Backgrounds { get; set; }

		[TreeNode]
		public ContainerItem<ContainerItem<MaterialItem>> MaterialSetsItem { get; set; }
	}
}
