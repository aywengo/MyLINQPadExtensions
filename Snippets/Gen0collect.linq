<Query Kind="Program" />

void Main()
{
	int gen0 = 0;
	while(true)
	{
		A a = new A();
		
		int gen0count = GC.GetGeneration(0);
		if (gen0 != gen0count)		
		{
			Console.WriteLine("Gen0 collection triggered!!!");
			
			gen0 = gen0count;
		}
	}
}

class A
{
	private static int s_count;
	private int m_count;
	
	public A()
	{
		m_count = s_count++;
		//Console.WriteLine("{0} was created", m_count);
	}
	
	~A()
	{
		//Console.WriteLine("{0} was removed", m_count);
	}
}