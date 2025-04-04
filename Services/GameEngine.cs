using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Data;
using W9_assignment_template.Models;

namespace W9_assignment_template.Services;

public class GameEngine
{
    private readonly GameContext _context;

    public GameEngine(GameContext context)
    {
        _context = context;
    }

    public void DisplayRooms()
    {
        var rooms = _context.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void DisplayCharacters()
    {
        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    public void AddRoom()
    {
        Console.Write("Enter room name: ");
        var name = Console.ReadLine();

        Console.Write("Enter room description: ");
        var description = Console.ReadLine();

        var room = new Room
        {
            Name = name,
            Description = description
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        Console.WriteLine($"Room '{name}' added to the game.");
    }

    public void AddCharacter()
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine();

        Console.Write("Enter character level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter room ID for the character: ");
        var roomId = int.Parse(Console.ReadLine());

        var room = _context.Rooms.Find(roomId);
        if (room == null)
        {
            Console.WriteLine("Room not found.");
            return;
        }

        var character = new Character
        {
            Name = name,
            Level = level,
            RoomId = roomId
        };

        _context.Characters.Add(character);
        _context.SaveChanges();

        // Debug statement to confirm character addition
        // Added by Copilot as AddCharacter and FindCharacter were not working prior
        var addedCharacter = _context.Characters.Find(character.Id);
        if (addedCharacter != null)
        {
            Console.WriteLine($"Character '{name}' added to room '{room.Name}'.");
        }
        else
        {
            Console.WriteLine("Failed to add character.");
        }
    }


    public void FindCharacter()
    {
        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        var character = _context.Characters
            .Include(c => c.Room)
            .FirstOrDefault(c => c.Name == name);

        if (character == null)
        {
            Console.WriteLine("Character not found.");
        }
        else
        {
            Console.WriteLine($"Character '{character.Name}' found in room '{character.Room.Name}' with level {character.Level}.");
        }
    }
}
