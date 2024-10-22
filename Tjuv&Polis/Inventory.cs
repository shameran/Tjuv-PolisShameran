using System.Collections.Generic;

public class Inventory
{
    private List<string> items; // Lista som håller inventory 

    public Inventory()
    {
        items = new List<string>();
    }

    // Lägger till ett föremål i inventariet
    public void Add(string item)
    {
        items.Add(item);
    }

    // Tar bort ett slumpmässigt föremål från inventariet
    public string RemoveRandomItem(Random random)
    {
        if (items.Count == 0) return null;

        int index = random.Next(items.Count);// Slumpar ett index
        string item = items[index]; // Hämtar föremålet vid det slumpade indexet
        items.RemoveAt(index);
        return item;
    }

    // Tömmer hela inventariet
    public void Clear()
    {
        items.Clear();
    }

    public List<string> GetItems()
    {
        return new List<string>(items); 
    }
}
