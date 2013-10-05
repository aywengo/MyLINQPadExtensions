<Query Kind="Program" />

void Main()
{
	int count = 3;
	var simpsons = new Abba[count];
	simpsons[0] = new Abba();
	simpsons[1] = new Homer();
	simpsons[2] = new Lisa();
	
	for(int i =0; i < simpsons.Count(); i++)
		((greetable)simpsons[i]).SayHello();	
}

interface greetable
{
	void SayHello();
}

class Abba : greetable
{
	public virtual void SayHello()
	{
		Console.WriteLine("Hi! I am Abba Simpson!");
	}
}

class Homer : Abba, greetable
{
	public new virtual void SayHello()
	{
		Console.WriteLine("Hi! I am Homer Simpson!");
	}
}

class Lisa : Homer, greetable
{
	public override void SayHello()
	{
		Console.WriteLine("Hi! I am Lisa Simpson!");
	}
}