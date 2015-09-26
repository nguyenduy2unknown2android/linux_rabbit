using System;
using EasyNetQ;

namespace RabbitMQ
{
	class MainClass
	{
		public static void Main (string[] args)
		{			
			using (var bus = RabbitHutch.CreateBus ("host=localhost;username=guest;password=guest")) {

				var input = "";
				Console.WriteLine ("Enter a message. 'Quit' to quit.");
				while ((input = Console.ReadLine ()) != "Quit") {
					bus.Publish<string> (input, "duynct");
				}
			}
		}
	}
}
