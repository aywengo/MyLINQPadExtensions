<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

async Task Main()
{
	string[] uris =
	{
		"http://ya.ru",
		"http://google.com",
		"http://linqpad.net",
	};
	Console.WriteLine("Main Thread #{0}", Thread.CurrentThread.ManagedThreadId);
	int result = await GetTotalLength (uris);
	result.Dump();	
	Console.WriteLine("Main Thread  after end #{0}", Thread.CurrentThread.ManagedThreadId);
}

async Task<int> GetTotalLength (string[] uris)
{
	Console.WriteLine("Runned task in await mode with ID #{0}", Thread.CurrentThread.ManagedThreadId);
	"Working...".Dump();
	int totalLength = 0;
	foreach (string uri in uris)
	{		
		Console.WriteLine("Task #{0} started download URI {1}", Thread.CurrentThread.ManagedThreadId, uri);
		string html = await new WebClient().DownloadStringTaskAsync (new Uri (uri));
		Console.WriteLine("Task #{0} finished download URI {1}", Thread.CurrentThread.ManagedThreadId, uri);
		totalLength += html.Length;
	}
	Console.WriteLine("End task in await mode with ID #{0}", Thread.CurrentThread.ManagedThreadId);
	return totalLength; 
}