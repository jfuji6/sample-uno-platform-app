using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace MyUnoApp.Skia.Tizen
{
	class Program
{
	static void Main(string[] args)
	{
		var host = new TizenHost(() => new MyUnoApp.App(), args);
		host.Run();
	}
}
}
