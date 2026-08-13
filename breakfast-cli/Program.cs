using System;
using System.Threading.Tasks;

namespace AsyncBreakfast
{
    // These classes are intentionally empty for the purpose of this example. They are simply marker classes for the purpose of demonstration, contain no properties, and serve no other purpose.
    internal class HashBrown { }
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }
    internal class Breakfast { }

    class Program
    {
        static async Task Main(string[] args)
        {
            var coffee = PourCoffee();
            Console.WriteLine("Coffee is ready");
            Breakfast breakfast = await MakeBreakfastWithAsyncAwait();
            
            var juice = PourJuice();
            Console.WriteLine("Breakfast is ready!");  

        }

        static async Task<Breakfast> MakeBreakfastWithAsyncAwait()
        {
            var eggs = await FryEggsAsync(2);
            Console.WriteLine("Eggs ready, starting bacon...");

            var bacon = await FryBaconAsync(3);
            Console.WriteLine("Bacon ready, starting toast...");

            var toast = await ToastBreadAsync(2);
            Console.WriteLine("Toast ready, applying butter...");

            var butteredToast = await ApplyButterAsync(toast);
            Console.WriteLine("Butter applied, applying jam...");

            var finalToast = await ApplyJamAsync(butteredToast);
            Console.WriteLine("Breakfast completed with async/await!");
            return new Breakfast();
        }

        private static Juice PourJuice()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast)
        {
            Console.WriteLine("Putting jam on the toast");
        }

        private static void ApplyButter(Toast toast)
        {
            Console.WriteLine("Putting butter on the toast");
        }

        private static async Task<Toast> ApplyButterAsync(Toast toast)
        {
            Console.WriteLine("Putting butter on the toast");
            await Task.Delay(500);

            return toast;
        }

        private static async Task<Toast> ApplyJamAsync(Toast toast)
        {
            Console.WriteLine("Putting jam on the toast");
            await Task.Delay(500);

            return toast;
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(2000);
            await Task.Delay(1000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }


        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}