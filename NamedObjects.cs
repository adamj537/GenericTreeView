using System;
using System.Collections.Generic;

namespace GenericTreeView
{
	/// <summary>
	/// A list which also has a name.
	/// </summary>
	/// <remarks>
	/// This will save creating specific classes for each list.
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class NamedList<T> : List<T>, INamedObject
	{
		#region Constructors

		public NamedList(string name)
		{
			Name = name;
		}

		#endregion

		public string Name { get; set; }
	}

	/// <summary>
	/// A string which also has a name.
	/// </summary>
	public class NamedString : INamedObject
	{
		#region Constructors

		public NamedString(string name)
		{
			Name = name;
		}

		public NamedString(string name, string value)
		{
			Name = name;
			Value = value;
		}

		#endregion

		public string Name { get; set; }

		public string Value { get; set; }
	}

	/// <summary>
	/// An integer which also has a name.
	/// </summary>
	public class NamedInt : INamedObject
	{
		#region Constructors

		public NamedInt(string name)
		{
			Name = name;
		}

		public NamedInt(string name, int value)
		{
			Name = name;
			Value = value;
		}

		#endregion

		public string Name { get; set; }

		public int Value { get; set; }
	}

	/// <summary>
	/// A double which also has a name.
	/// </summary>
	public class NamedDouble : INamedObject
	{
		#region Constructors

		public NamedDouble(string name)
		{
			Name = name;
		}

		public NamedDouble(string name, double value)
		{
			Name = name;
			Value = value;
		}

		#endregion

		public string Name { get; set; }

		public double Value { get; set; }
	}

	/// <summary>
	/// A time span which also has a value.
	/// </summary>
	public class NamedTimeSpan : INamedObject
	{
		#region Constructors

		public NamedTimeSpan(string name)
		{
			Name = name;
		}

		public NamedTimeSpan(string name, TimeSpan value)
		{
			Name = name;
			Value = value;
		}

		#endregion

		public string Name { get; set; }

		public TimeSpan Value { get; set; }
	}
}
