using System.IO;

namespace Converter.MeshFormat.Writer;

public interface IMeshFormatWriter
{
	string Tag { get; }

	void WriteToStream(Mesh mesh, Stream outputStream);
}
