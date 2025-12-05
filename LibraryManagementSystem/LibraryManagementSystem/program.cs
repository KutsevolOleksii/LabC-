//
//Куцевол Олексій Ігорович ПД-23
//
using System;
using LibraryManagementSystem;

namespace LibraryManagementSystemDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryManager manager = new LibraryManager();

            manager.AddItem(new Book("C# Basics", 2023, "Matvey Glupiy"));
            manager.AddItem(new Book("C# Advanced", 2024, "Leha Umniy"));

            manager.AddItem(new Magazine("C# Monthly", 2023, 42));
            manager.AddItem(new Magazine("C# Weekly", 2024, 15));

            var allItems = manager.GetAllItems();
            Console.WriteLine("--- Всі єлементи бібліотеки ---");
            foreach (var item in allItems)
            {
                Console.WriteLine(item.GetDisplayInfo());
            }

            int searchId = 2;
            var foundItem = manager.GetItemById(searchId);
            if (foundItem != null)
            {
                Console.WriteLine($"\nЗнайдено єлемент з ID {searchId}:");
                Console.WriteLine(foundItem.GetDisplayInfo());
            }
            else
            {
                Console.WriteLine($"\nЄлемент з ID {searchId} не знайдено.");
            }
        }
    }
}
