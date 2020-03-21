﻿using System;
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
			Components = new NamedList<ComponentItem>("Components");
		}

		public string Name { get; set; }

		[TreeNode]
		public NamedList<ComponentItem> Components { get; set; }
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

	[Serializable]
	public class ProjectItem : INamedObject
	{
		public ProjectItem(string name)
		{
			Name = name;
		}

		public string Name { get; set; }

		[TreeNode]
		public NamedList<AssemblyItem> Assemblies { get; set; } = new NamedList<AssemblyItem>("Assemblies")
		{
			new AssemblyItem("Assembly 1")
			{
				Components = new NamedList<ComponentItem>("Components")
				{
					new ComponentItem("Component 1"),
					new ComponentItem("Component 2"),
					new ComponentItem("Component 3"),
				}
			},
			new AssemblyItem("Assembly 2")
			{
				Components = new NamedList<ComponentItem>("Components")
				{
					new ComponentItem("Component 4"),
					new ComponentItem("Component 5"),
				}
			},
			new AssemblyItem("Assembly 3")
			{
				Components = new NamedList<ComponentItem>("Components")
				{
					new ComponentItem("Component 6")
				}
			}
		};

		[TreeNode]
		public NamedList<BackgroundItem> Backgrounds { get; set; } = new NamedList<BackgroundItem>("Backgrounds")
		{
			new GradientBackgroundItem("Cool Blue", Color.Beige, Color.Aqua)
		};

		[TreeNode]
		public NamedList<NamedList<MaterialItem>> MaterialSetsItem { get; set; } = new NamedList<NamedList<MaterialItem>>("Material Sets");
	}
}
