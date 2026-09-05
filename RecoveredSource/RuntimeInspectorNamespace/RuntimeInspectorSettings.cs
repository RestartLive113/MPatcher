using UnityEngine;

namespace RuntimeInspectorNamespace;

[CreateAssetMenu(fileName = "Inspector Settings", menuName = "RuntimeInspector/Settings", order = 111)]
public class RuntimeInspectorSettings : ScriptableObject
{
	[SerializeField]
	private InspectorField[] inspectorField_0;

	[SerializeField]
	private InspectorField[] NaTtVZejlaxWcQlWLKL2bNfg5aRr_AOaWJCxDIiTLVTp;

	[SerializeField]
	private VariableSet[] _yj4yQjgNNhRQWbEoyXEgxXHlVtooNSSl9M1sI9_0024Z9LK;

	[SerializeField]
	private VariableSet[] gdbwQs_65cHHJpZMaYh4l7AgMdAB_OENOoaQc14tkh08;

	public InspectorField[] StandardDrawers => inspectorField_0;

	public InspectorField[] ReferenceDrawers => NaTtVZejlaxWcQlWLKL2bNfg5aRr_AOaWJCxDIiTLVTp;

	public VariableSet[] HiddenVariables
	{
		get
		{
			return _yj4yQjgNNhRQWbEoyXEgxXHlVtooNSSl9M1sI9_0024Z9LK;
		}
		set
		{
			_yj4yQjgNNhRQWbEoyXEgxXHlVtooNSSl9M1sI9_0024Z9LK = value;
		}
	}

	public VariableSet[] ExposedVariables
	{
		get
		{
			return gdbwQs_65cHHJpZMaYh4l7AgMdAB_OENOoaQc14tkh08;
		}
		set
		{
			gdbwQs_65cHHJpZMaYh4l7AgMdAB_OENOoaQc14tkh08 = value;
		}
	}
}
