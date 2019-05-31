using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandscapeSprinklerDesigner
{
	/// <summary>
	/// Debounces 
	/// </summary>
	class Debouncer
	{
		private CancellationTokenSource cts = null;
		private int delay = 1000;

		/// <summary>
		/// Initialize the <paramref name="delay"/> with a different delay value
		/// </summary>
		/// <param name="delay">Delay applied to the <see cref="Debouncer"/></param>
		public Debouncer(int delay = 1000)
		{
			this.delay = delay;
		}

		/// <summary>
		/// Debounces the <paramref name="work"/> by delaying the call to <paramref name="work"/> for 
		/// <see cref="delay"/>
		/// </summary>
		/// <param name="work"></param>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task DebounceAsync(Action<CancellationToken> work)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			cts?.Cancel();
			cts = new CancellationTokenSource();

			var task = Task.Run(async () =>
			{
				await Task.Delay(this.delay, cts.Token);

				if (!cts.Token.IsCancellationRequested)
				{
					work(cts.Token);
				}
			}, cts.Token);
		}
	}
}