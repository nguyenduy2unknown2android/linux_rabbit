using System;

namespace RabbitMQSubscriber
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var worker = new WorkerService ();
			worker.Start ();
		}
	}
}
