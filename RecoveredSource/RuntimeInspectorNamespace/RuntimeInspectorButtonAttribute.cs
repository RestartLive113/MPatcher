using System;

namespace RuntimeInspectorNamespace;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class RuntimeInspectorButtonAttribute : Attribute
{
	private readonly string BrfDnqkMr4jWJXZYaW6BDdw;

	private readonly bool as9iQLo4XUaYl7yUYLLjx6E;

	private readonly ButtonVisibility buttonVisibility_0;

	public string Label => BrfDnqkMr4jWJXZYaW6BDdw;

	public bool IsInitializer => as9iQLo4XUaYl7yUYLLjx6E;

	public ButtonVisibility Visibility => buttonVisibility_0;

	public RuntimeInspectorButtonAttribute(string string_0, bool bool_0, ButtonVisibility buttonVisibility_1)
	{
		BrfDnqkMr4jWJXZYaW6BDdw = string_0;
		as9iQLo4XUaYl7yUYLLjx6E = bool_0;
		buttonVisibility_0 = buttonVisibility_1;
	}
}
