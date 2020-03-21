using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GenericTreeView
{
	public enum VariableType
	{
		[Description("Gas Mix")]
		GasConcentration,
		[Description("Mass Flow")]
		MassFlow,
		[Description("Volume Flow")]
		VolumeFlow,
		Velocity,
		Pressure,
		Temperature
	}

	/// <summary>
	/// Non-measurement related commands that can be executed during a test.
	/// </summary>
	/// <remarks>
	/// These actions can activate a relay, send info to the DUTs, etc.
	/// </remarks>
	public enum Command
	{
		TurnOff,    // Remove power from DUT.
		TurnOn,     // Apply power to DUT.			  
		Default,    // Set factory default settings.
		Range,      // Set range settings.
		Zero,       // Perform zero-calibration.
		Span,       // Perform span-calibration.
	}

	/// <summary>
	/// Configuration for a variable being controlled during a test.
	/// </summary>
	[Serializable]
	public class TestControlledVariable
	{
		[TreeNode, Category("Test Variable"), Description("Type of the variable.")]
		public VariableType VariableType { get; set; }

		[TreeNode, Category("Test Variable"), Description("Error tolerance around setpoints [% full scale].  If exceeded, Stability Time will reset.")]
		public NamedDouble ErrorTolerance { get; set; } = new NamedDouble("Error Tolerance", 25.0);

		[TreeNode, Category("Test Variable"), Description("Tolerated rate of change of setpoints [% full scale / s].  If exceeded, Stability Time will reset.")]
		public NamedDouble RateTolerance { get; set; } = new NamedDouble("Rate Tolerance", 2.0);

		[TreeNode, Category("Test Variable"), Description("Setpoints [% full scale].")]
		public NamedList<double> Setpoints { get; set; } = new NamedList<double>("Setpoints");

		[TreeNode, Category("Test Variable"), Description("Required time to be at setpoint before continuing test.")]
		public NamedTimeSpan StabilityTime { get; set; } = new NamedTimeSpan("Stability Time", new TimeSpan (0, 0, 0));

		[TreeNode, Category("Test Variable"), Description("Timeout before aborting control.")]
		public NamedTimeSpan Timeout { get; set; } = new NamedTimeSpan("Timeout", new TimeSpan(0, 0, 30));

		[TreeNode, Category("Test Component"), Description("Number of samples taken from DUT at each setpoint.")]
		public NamedInt Samples { get; set; } = new NamedInt("Samples", 0);

		[TreeNode, Category("Test Component"), Description("Time to wait between taking samples from DUT/variables.")]
		public NamedTimeSpan Interval { get; set; } = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0));
	}

	/// <summary>
	/// Configuration for a component of a test.
	/// </summary>
	[Serializable]
	public class TestComponent
	{
		#region Constructors

		// Default constructor.
		public TestComponent() { }

		// Initializer with label.
		public TestComponent(string name)
		{
			Name = name;
		}

		#endregion

		[Category("Test Component"), Description("Name for this part of the test.")]
		public string Name { get; set; } = "";

		[TreeNode, Category("Test Component"), Description("Actions to perform on the DUT during this test component.")]
		public NamedList<Command> Commands { get; set; }

		[TreeNode, Category("Test Component"), Description("Controlled variables for this part of the test.")]
		public NamedList<TestControlledVariable> ControlledVariables { get; set; }
	}

	/// <summary>
	/// Tests that may be performed on a DUT
	/// </summary>
	[Serializable]
	public class TestSetting
	{
		#region Constructors

		// Default constructor.
		public TestSetting() { }

		// Initializer with label.
		public TestSetting(string name)
		{
			Name = name;
		}

		#endregion

		[Category("Test Settings"), Description("Name of the test (as it will appear to the operator).")]
		public string Name { get; set; } = "";

		[TreeNode, Category("Test Settings"), Description("Actions performed during the test.")]
		public NamedList<TestComponent> Components { get; set; }

		[TreeNode, Category("Test Settings"), Description("Variables measured (with reference devices) during the test.")]
		public NamedList<VariableType> References { get; set; }
	}

	[Serializable]
	public class TestSettings : INamedObject
	{
		public string Name { get; set; } = "Settings";

		[TreeNode, Category("Test Settings"), Description("Settings describing tests that can be performed.")]
		public NamedList<TestSetting> Tests { get; } = new NamedList<TestSetting>("Tests")
		{
			new TestSetting("Flow Rate Test")
			{
				References = new NamedList<VariableType>("References")
				{
					VariableType.MassFlow,
					VariableType.GasConcentration
				},
				Components = new NamedList<TestComponent>("Components")
				{
					new TestComponent("Purge")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0.0 },
								StabilityTime = new NamedTimeSpan("Stability Time", new TimeSpan(0, 4, 0)),
								Timeout = new NamedTimeSpan("Timeout", new TimeSpan(0, 10, 0)),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						}
					},
					new TestComponent("100 sccm")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 100 },
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 100.0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							},
						},
					},
					new TestComponent("Purge")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0.0 },
								StabilityTime = new NamedTimeSpan("Stability Time", new TimeSpan(0, 4, 0)),
								Timeout = new NamedTimeSpan("Timeout", new TimeSpan(0, 10, 0)),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						}
					},
					new TestComponent("200 sccm")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 100 },
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 200.0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							}
						},
					},
					new TestComponent("Purge")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0.0 },
								StabilityTime = new NamedTimeSpan("StabilityTime", new TimeSpan(0, 4, 0)),
								Timeout = new NamedTimeSpan("Timeout", new TimeSpan(0, 10, 0)),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						}
					},
					new TestComponent("300 sccm")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 100 },
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 300.0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							}
						},
					},
					new TestComponent("Purge")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0.0 },
								StabilityTime = new NamedTimeSpan("Stability Time", new TimeSpan(0, 4, 0)),
								Timeout = new NamedTimeSpan("Timeout", new TimeSpan(0, 10, 0)),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						}
					},
					new TestComponent("400 sccm")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 100 },
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 400.0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							}
						},
					},
					new TestComponent("Purge")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0.0 },
								StabilityTime = new NamedTimeSpan("Stability Time", new TimeSpan(0, 4, 0)),
								Timeout = new NamedTimeSpan("Timeout", new TimeSpan(0, 10, 0)),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						}
					},
					new TestComponent("500 sccm")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 100 },
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 500.0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							}
						},
					},
				}
			},
			new TestSetting("Warm-Up Stability")
			{
				References = new NamedList<VariableType>("References")
				{
					VariableType.MassFlow,
					VariableType.GasConcentration
				},
				Components = new NamedList<TestComponent>("Components")
				{
					// Apply gas for 5 minutes.
					new TestComponent("Apply gas")
					{
						Commands = new NamedList<Command>("Commands") { Command.TurnOff },
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 300.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								StabilityTime = new NamedTimeSpan("Stability Time", new TimeSpan(0, 5, 0)),
								Setpoints = new NamedList<double>("Setpoints") { 25.0 }
							}
						}
					},
					// Measure stability every second for 30 minutes.
					new TestComponent("Measure stability")
					{
						Commands = new NamedList<Command>("Commands") { Command.TurnOn },
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 300.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 25.0 },
								Samples = new NamedInt("Samples", 1800),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 1))
							}
						},
					}
				}
			},
			new TestSetting("Linearity: 1-cycle, 100%")
			{
				References = new NamedList<VariableType>("References")
				{
					VariableType.MassFlow,
					VariableType.GasConcentration
				},
				Components = new NamedList<TestComponent>("Components")
				{
					// Ramp up and down.  Measure gas every 1 second.  Don't wait for stability.
					new TestComponent("Up and Down")
					{
						ControlledVariables = new NamedList<TestControlledVariable>("Controlled Variables")
						{
							new TestControlledVariable()
							{
								VariableType = VariableType.MassFlow,
								Setpoints = new NamedList<double>("Setpoints") { 300.0 }
							},
							new TestControlledVariable()
							{
								VariableType = VariableType.GasConcentration,
								Setpoints = new NamedList<double>("Setpoints") { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0 },
								Samples = new NamedInt("Samples", 240),
								Interval = new NamedTimeSpan("Interval", new TimeSpan(0, 0, 0, 0, 500))
							}
						},
					},
				}
			},
		};
	}
}
