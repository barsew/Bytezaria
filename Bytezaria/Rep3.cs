using Bytezaria0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bytezaria3
{
    public class Line
    {
        public string Id { get; }
        public string Number { get; }
        public string CommonName { get; }
        public List<string> Stops { get; }
        public List<string> Vehicles { get; }

        public Line(string numberHex, int numberDec, string commonName, List<string> stops, List<string> vehicles)
        {
            this.Number = numberHex + "(" + numberDec + ")";
            this.CommonName = commonName;
            this.Stops = stops;
            this.Vehicles = vehicles;
        }
        public void SpecificDisplay()
        {
            var tab = Number.Split('(', ')');
            Console.WriteLine($"Line number {tab[0]} ({tab[1]}) with common name {CommonName}");
            Console.WriteLine("Stops:");
            foreach (var s in Stops)
            {
                Console.WriteLine("...");
            }
            Console.WriteLine("Vehicles:");
            foreach (var v in Vehicles)
            {
                Console.WriteLine("...");
            }
        }
    }
    public class Stop
    {
        public string Id { get; }
        public string Name { get; }

        public type Type { get; }
        public enum type
        {
            bus = 1,
            tram = 2,
            other = 3
        }
        public List<string> Lines { get; }

        public Stop(int id, string name, type type, List<string> lines)
        {
            Name = "(" + id + ")" + name;
            Type = type;
            Lines = lines;
        }

        public void SpecificDisplay(List<Line> lines)
        {
            var tab = Name.Split('(', ')');
            Console.WriteLine($"Stop {tab[1]} with id:{tab[0]} for {Type}");
            foreach (var l in lines)
            {
                if(Lines.Contains(l.Id))
                {
                    Console.WriteLine(l.CommonName);
                }
            }
        }
    }
    public class ByteBus: Vehicle
    {
        public enum engineClass
        {
            Byte5 = 1,
            bise120 = 2,
            gibagaz = 3
        }
        public engineClass EngineClass { get; }
        public List<string> Description { get; }

        public ByteBus(int id, List<string> lines,  engineClass engineClass): base(id.ToString())
        {
            EngineClass= engineClass;
            string s = "";
            foreach (var l in lines)
            {
                s = "#" + Id + "*" + l;
                Description.Add(s);
            }
        }

        public void SpecificDisplay()
        {
            Console.WriteLine($"Bytebus with id {Id} cars");
            string[] s;
            foreach (var x in Description)
            {
                s = x.Split('#', '*');
                Console.WriteLine($"Line {s[1]}");
            }
            
        }
    }
    public class Tram: Vehicle
    {
        public string _line { get; }
        public string Description { get; }

        public Tram(int id, int carsNumber, Line line): base(id.ToString())
        {
            _line = line.Id;
            Description = "#" + Id + "(" + carsNumber + ")";
        }
        public void SpecificDisplay()
        {
            string[] s = Description.Split('#', '(', ')');
            Console.WriteLine($"Tram with id {Id} with {s[1]} cars has line {_line}");
        }
    }
    public class Driver
    {
        public string Identity { get; }
        public int Seniority { get; }
        public List<string> Vehicles { get; }

        public Driver(string name, string surname, int seniority, List<string> vehicles)
        {
            Identity = name + "," + surname;
            Seniority = seniority;
            Vehicles = vehicles;
        }

        public void SpecificDisplay()
        {
            string[] s = Identity.Split(',');
            Console.WriteLine($"Driver {s[0]} {s[1]} with seniority {Seniority}");
            foreach (var v in Vehicles)
            {
                Console.WriteLine(v);
            }

        }
    }
    public abstract class Vehicle
    {
        public string Id { get; }
        public Vehicle(string id)
        {
            Id = id;
        }
    }
}
