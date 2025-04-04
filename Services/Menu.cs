using W9_assignment_template.Services;

namespace W9_assignment_template
{
    public class Menu
    {
        private readonly GameEngine _gameEngine;

        public Menu(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display Rooms");
                Console.WriteLine("2. Display Characters");
                Console.WriteLine("3. Add a Room");
                Console.WriteLine("4. Add a Character");
                Console.WriteLine("5. Find a Character");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _gameEngine.DisplayRooms();
                        break;
                    case "2":
                        _gameEngine.DisplayCharacters();
                        break;
                    case "3":
                        _gameEngine.AddRoom();
                        break;
                    case "4":
                        _gameEngine.AddCharacter();
                        break;
                    case "5":
                        _gameEngine.FindCharacter();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
