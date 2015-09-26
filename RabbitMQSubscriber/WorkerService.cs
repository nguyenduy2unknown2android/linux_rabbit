using System;
using System.Threading;
using EasyNetQ;
using System.Threading.Tasks;

namespace RabbitMQSubscriber
{
	public class WorkerService : TopshelfService
	{
		public WorkerService ()
		{
		}

		protected override void DoWork()
		{
			try
			{
				var program = new WorkerService();
				program.DoASubscription();

			}
			catch (Exception exception)
			{
				Console.WriteLine(string.Format(">>>>>>>>> ERROR WITH EXCEPTION {0}", exception.Message));
			}
		}

		private void DoASubscription()
		{
			using (var bus = RabbitHutch.CreateBus ("host=localhost;username=guest;password=guest")) {

				var done = false;

				try
				{
					Console.WriteLine("duynct_subscriber is subscribing to topic 'duynct'");
					bus.SubscribeAsync<string>("duynct_subscriber", str => Task.Factory.StartNew(() =>
						{
							Console.WriteLine(string.Format(">>>>>>>>> PROCESSING: {0}", str));

						}).ContinueWith(Completed =>
							{
								if (Completed.Status == TaskStatus.Faulted)
								{
									Console.WriteLine(string.Format(">>>>>>>>> FAILED {0}", str));
								}
							}, TaskContinuationOptions.OnlyOnFaulted), x => x.WithTopic("duynct"));

				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format(">>>>>>>>> ERROR WITH EXCEPTION {0}", ex.Message));
				}
				SpinWait.SpinUntil(() => done);				
			}
		}
	}
}

