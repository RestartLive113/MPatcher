using System.IO;

namespace Converter.MeshFormat.Reader;

public interface IMeshFormatReader
{
	string Tag { get; }

	Mesh ReadFromStream(Stream inputStream);
}
