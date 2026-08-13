
namespace LivelockDemo;

public class Program
{
	public static async Task Main()
	{
		Console.WriteLine("=== HALLWAY LIVELOCK DEMO ===");
		Console.WriteLine("Both people keep moving, but neither gets past the other.\n");

		var hallway = new Hallway();
		var alice = new Walker("Alice", 0);
		var bob = new Walker("Bob", 0);

		using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
		using var decisionPoint = new Barrier(2);

		var aliceTask = alice.TryToPassAsync(hallway, bob, decisionPoint, cts.Token);
		var bobTask = bob.TryToPassAsync(hallway, alice, decisionPoint, cts.Token);

		try
		{
			await Task.WhenAll(aliceTask, bobTask);
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("\nTime is up. They were active the whole time, but nobody passed.");
		}
	}
}

public class Hallway
{
	public bool AreOnSameSide(Walker first, Walker second)
	{
		lock (this)
		{
			return first.Side == second.Side;
		}
	}

	public void SwitchSide(Walker walker)
	{
		lock (this)
		{
			walker.Side = walker.Side == 0 ? 1 : 0;
		}
	}
}

public class Walker
{
	public string Name { get; }
	public int Side { get; set; }

	public Walker(string name, int side)
	{
		Name = name;
		Side = side;
	}

	public async Task TryToPassAsync(
		Hallway hallway,
		Walker other,
		Barrier decisionPoint,
		CancellationToken token)
	{
		await Task.Yield();

		while (true)
		{
			token.ThrowIfCancellationRequested();

			var blocked = hallway.AreOnSameSide(this, other);
			decisionPoint.SignalAndWait(token);

			if (!blocked)
			{
				Console.WriteLine($"{Name}: We are on different sides. I can pass!");
				return;
			}

			hallway.SwitchSide(this);
			Console.WriteLine($"{Name}: I move to the other side to let {other.Name} pass.");

			await Task.Delay(200, token);
		}
	}
}