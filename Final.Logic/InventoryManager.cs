namespace Final.Logic
{
    public class InventoryManager
    {
        private List<string> ThingsIHave;
        private int maxSlots;

        public InventoryManager(int maxSlots = 10)
        {
            this.maxSlots = maxSlots;
            ThingsIHave   = new List<string>();
        }

        public List<string> GetItems()
        {
            return new List<string>(ThingsIHave);
        }

        public bool IsFull()
        {
            return ThingsIHave.Count >= maxSlots;
        }

        public bool HasItem(string item)
        {
            return ThingsIHave.Contains(item);
        }

        public bool PickUpItem(string item)
        {
            if (IsFull())
            {
                Console.WriteLine("Your pockets are full, you can't pick that up");
                return false;
            }

            ThingsIHave.Add(item);
            return true;
        }

        public bool UseItem(string item)
        {
            if (!HasItem(item))
                return false;

            ThingsIHave.Remove(item);
            return true;
        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            Console.WriteLine("  ║           I N V E N T O R Y          ║");
            Console.WriteLine("  ╠══════════════════════════════════════╣");

            if (ThingsIHave.Count == 0)
            {
                Console.WriteLine("  ║                                      ║");
                Console.WriteLine("  ║        Your inventory is empty!      ║");
                Console.WriteLine("  ║                                      ║");
            }
            else
            {
                for (int i = 0; i < maxSlots; i++)
                {
                    if (i < ThingsIHave.Count)
                    {
                        string item = ThingsIHave[i];
                        string slot = $"  {i + 1}.  {item}";
                        
                        slot = slot.PadRight(42);
                        Console.WriteLine($"{slot}║");
                    }
                    else
                    {
                        Console.WriteLine("  ║   [ empty slot ]                     ║");
                    }
                }
            }

            Console.WriteLine("  ╠══════════════════════════════════════╣");
            Console.WriteLine($"  ║   Slots used: {ThingsIHave.Count}/{maxSlots}                   ║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("          Press any key to close inventory...");
            Console.ReadKey(true);
        }
    }
}