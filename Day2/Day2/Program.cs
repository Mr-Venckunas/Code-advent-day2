using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var fileOutput = new FileReader("day2input.txt").ReadFile();
            var commands = new CommandGenerator(fileOutput).GetCommands();
            var output = new PositionCalculator(commands).MultiplyPositionByDepth();

            Console.WriteLine($"{output}");
            Console.ReadLine();
        }
    }

    public class PositionCalculator
    {
        private readonly List<Command> _commands;
        public PositionCalculator(List<Command> commands)
        {
            _commands = commands;
        }

        public decimal MultiplyPositionByDepth()
        {
            return CalculateHorizontalPosition() * CalculateDepth();
        }

        private int CalculateHorizontalPosition() => _commands.Where(x => x.Direction == Direction.Forward).Sum(x => x.Amount);
        private int CalculateDepth()
        {
            return _commands.Where(x => x.Direction == Direction.Down).Sum(x => x.Amount) - _commands.Where(x => x.Direction == Direction.Up).Sum(x => x.Amount);
        }
    }

    public class CommandGenerator
    {
        private readonly string[] _lines;
        public CommandGenerator(string[] lines)
        {
            _lines = lines;
        }

        public List<Command> GetCommands()
        {
            return _lines.Select(ParseLineIntoCommand).ToList();
        }

        private Command ParseLineIntoCommand(string line)
        {
            var splitLine = line.Split(" ");
            if (!Enum.TryParse<Direction>(splitLine[0], true, out var direction) || !int.TryParse(splitLine[1], out var amount))
                throw new Exception();

            return new Command
            {
                Direction = direction,
                Amount = amount
            };
        }
    }

    public class FileReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public string[] ReadFile()
        {
            return File.ReadAllLines(_path);
        }
    }

    public class Command
    {
        public Direction Direction { get; set; }
        public int Amount { get; set; }
    }

    public enum Direction
    {
        Forward,
        Down,
        Up
    }
}
