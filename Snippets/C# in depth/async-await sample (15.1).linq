<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

void Main()
{
	Application.Run(new AsyncForm());
}

class AsyncForm : Form
{
	Label label;
	Button button;
	
	public AsyncForm()
	{
		label = new Label { Location = new Point(10, 20),
							Text = "Length"};
		button = new Button { Location = new Point(10,50),
							  Text = "Click"};
							  button.Click += DisplayWebSiteLength;
							  AutoSize = true;
							  Controls.Add(label);
							  Controls.Add(button);
	}
	
	async void DisplayWebSiteLength(object sender, EventArgs e)
	{
		label.Text = "Fetching ...";
		using (HttpClient client = new HttpClient())
		{
			string text =
				await client.GetStringAsync("http://ya.ru");
			label.Text = text.Length.ToString();
		}
	}
}
