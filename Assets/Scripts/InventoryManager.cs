using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventoryItem> inventory = new List<InventoryItem>();

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            inventory.Add(new InventoryItem(
                Random.Range(1, 1000),
                "Item" + i,
                Random.Range(1, 500)));
        }

        // testing code for search by name
        InventoryItem found = LinearSearchByName("Item3");
        Debug.Log(found != null ? "Found " + found.Name + " (ID " + found.ID + ")" : "Not found");

        // testing code for binary search
        InventoryItem byId = BinarySearchByID(inventory[0].ID);
        Debug.Log(byId != null ? "Binary search found " + byId.Name + " (ID " + byId.ID + ")" : "Binary search not found");

        // testing code for sort by value
        QuickSortByValue();
        foreach (var item in inventory)
            Debug.Log(item.Name + " Value: " + item.Value);
    }

    public InventoryItem LinearSearchByName(string itemName)
    {
        foreach (var item in inventory)
        {
            if (item.Name == itemName)
                return item;
        }
        return null;
    }

    public InventoryItem BinarySearchByID(int id)
    {
        inventory.Sort((a, b) => a.ID.CompareTo(b.ID));

        int low = 0;
        int high = inventory.Count - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (inventory[mid].ID == id)
                return inventory[mid];
            else if (inventory[mid].ID < id)
                low = mid + 1;
            else
                high = mid - 1;
        }

        return null;
    }

    // everything below this comment is for QuickSortByValue

    public void QuickSortByValue()
    {
        QuickSort(0, inventory.Count - 1);
    }

    private void QuickSort(int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(low, high);
            QuickSort(low, pivotIndex - 1);
            QuickSort(pivotIndex + 1, high);
        }
    }

    private int Partition(int low, int high)
    {
        int pivot = inventory[high].Value;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (inventory[j].Value <= pivot)
            {
                i++;
                (inventory[i], inventory[j]) = (inventory[j], inventory[i]);
            }
        }

        (inventory[i + 1], inventory[high]) = (inventory[high], inventory[i + 1]);
        return i + 1;
    }
}