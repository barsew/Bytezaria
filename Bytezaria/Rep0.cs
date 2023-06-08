using Bytezaria;
using Bytezaria3;
using Bytezaria7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bytezaria0
{
    public interface ILine
    {
        string GetNumberHex();
        void SetNumberHex(string s);
        int GetNumberDec();
        void SetNumberDec(int a);
        string GetCommonName();
        void SetCommonName(string s);
        List<Stop> GetStops();
        void SetStops(List<Stop> stops);
        List<Vehicle> GetVehicles();
        void SetVehicles(List<Vehicle> vehciles);
    }
    public interface IStop
    {
        int GetId();
        void SetId(int id);
        string GetName();
        void SetName(string name);
        Bytezaria0.Stop.type GetType();
        void SetType(Bytezaria0.Stop.type type);
        List<int> GetLines();
        void SetLines(List<int> lines);
    }
    public interface IVehicle
    {
        int GetId();
        void SetId(int id);
    }
    public interface IBytebus: IVehicle
    {
        List<Line> GetLines();
        void SetLines(List<Line> lines);
        Bytezaria0.Bytebus.engineClass GetEngineClass();
        void SetEngineClass(Bytezaria0.Bytebus.engineClass engine);
    }
    public interface ITram: IVehicle
    {
        int GetCarsNumber();
        void SetCarsNumber(int carsNumber);
        Line GetLine();
        void SetLine(Line line);
    }
    public interface IDriver
    {
        List<Vehicle> GetVehicles();
        void SetVehicles(List<Vehicle> vehicles);
        int GetSeniority();
        void SetSeniority(int seniority);
        string GetName();
        void SetName(string name);
        string GetSurname();
        void SetSurname(string surname);
    }

    public class Line : ILine
    {
        public string NumberHex { get; set; }

        public int NumberDec { get; set; }

        public string CommonName { get; set; }

        public List<Stop> Stops { get; set; }

        public List<Vehicle> Vehicles { get; set;  }

        public Line(string numberHex, int numberDec, string commonName, List<Stop> stops, List<Vehicle> vehicles)
        {
            NumberHex = numberHex;
            NumberDec = numberDec;
            CommonName = commonName;
            Stops = stops;
            Vehicles = vehicles;
        }

        public string GetNumberHex()
        {
            return NumberHex;
        }

        public int GetNumberDec()
        {
            return NumberDec;
        }

        public string GetCommonName()
        {
           return CommonName;
        }

        public List<Stop> GetStops()
        {
            return Stops;
        }

        public List<Vehicle> GetVehicles()
        {
            return Vehicles;
        }

        public override string ToString()
        {
            string s = $"Line: \n NumberHex: {NumberHex} \n NumberDec: {NumberDec} \n CommonName: {CommonName} \n with Stops:";

            if (Stops != null)
            {
                foreach (var stop in Stops)
                {
                    s += "  " + stop.ToString();
                }
            }

            s += "\n with Vehicles:";

            if (Vehicles != null)
            {
                foreach (var vehicle in Vehicles)
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }

        public void SetNumberHex(string s)
        {
            NumberHex = s;
        }

        public void SetNumberDec(int a)
        {
            NumberDec = a;
        }

        public void SetCommonName(string s)
        {
            CommonName = s;
        }

        public void SetStops(List<Stop> stops)
        {
            stops = new List<Stop>(stops);
        }

        public void SetVehicles(List<Vehicle> vehciles)
        {
            vehciles = new List<Vehicle>(vehciles);
        }
    }

    public class Stop: IStop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public type Type { get; set; }
        public List<int> Lines { get; set; }

        public int GetId()
        {
           return Id;
        }

        public string GetName()
        {
            return Name;   
        }

        type IStop.GetType()
        {
            return Type;
        }

        public List<int> GetLines()
        {
            return Lines;
        }

        public enum type
        {
            bus = 1,
            tram = 2,
            other = 3
        }

        public Stop(int id, string name, type type, List<int> lines)
        {
            Id = id;
            Lines = lines;
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            string s = $"Stop: \n Id: {Id} \n Name: {Name} \n Type: {Type} \n with Lines:";

            if (Lines != null)
            {
                foreach (var line in Lines)
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(type type)
        {
            Type = type;
        }

        public void SetLines(List<int> lines)
        {
            Lines = new List<int>(lines);
        }
    }

    public abstract class Vehicle: IVehicle
    {
        public int Id { get; set; }
        public Vehicle(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }

    public class Bytebus : Vehicle, IBytebus
    {
        public List<Line> Lines { get; set; }

        public engineClass EngineClass { get; set; }

        public Bytebus(int id, List<Line> lines, engineClass engineClass): base(id)
        {
            Lines = lines;
            EngineClass = engineClass;
        }

        public enum engineClass
        {
            Byte5 = 1,
            bise120 = 2,
            gibagaz = 3
        }

        public List<Line> GetLines()
        {
            return Lines;
        }

        public engineClass GetEngineClass()
        {
            return EngineClass;
        }

        public override string ToString()
        {
            string s = $"Bytebus: \n EngineClass: {EngineClass} \n with Lines:";

            if (Lines != null)
            {
                foreach (var line in Lines)
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }

        public void SetLines(List<Line> lines)
        {
           Lines =  new List<Line>(lines);
        }

        public void SetEngineClass(engineClass engine)
        {
            EngineClass = engine;
        }
    }
    public class Tram : Vehicle, ITram
    {
        public int CarsNumber { get; set; }
        public Line _line { get; set; }

        public Tram(int id, int carsNumber, Line line): base(id)
        {
            CarsNumber = carsNumber;
            _line = line;
        }

        public int GetCarsNumber()
        {
           return CarsNumber;
        }

        public Line GetLine()
        {
            return _line;
        }

        public override string ToString()
        {
            string s = $"Tram: \n CarsNuber: {CarsNumber} \n with Line:";

            if (_line != null)
            {
                s += "  " + _line.ToString();
            }

            return s;
        }

        public void SetCarsNumber(int carsNumber)
        {
            CarsNumber = carsNumber;
        }

        public void SetLine(Line line)
        {
            _line = line;
        }
    }
    public class Driver: IDriver
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Seniority { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public Driver(string name, string surname, int seniority, List<Vehicle> vehicles)
        {
            Name = name;
            Surname = surname;
            Seniority = seniority;
            Vehicles = vehicles;
        }

        public List<Vehicle> GetVehicles()
        {
           return Vehicles;
        }

        public int GetSeniority()
        {
            return Seniority;
        }

        public string GetName()
        {
           return Name;
        }

        public string GetSurname()
        {
            return Surname;
        }

        public override string ToString()
        {
            string s = $"Driver: \n Name: {Name} \n Surname: {Surname} \n Seniority: {Seniority} \n with Vehicles:";

            if (Vehicles != null)
            {
                foreach (var vehicle in Vehicles)
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }

        public void SetVehicles(List<Vehicle> vehicles)
        {
            Vehicles = new List<Vehicle>(vehicles);
        }

        public void SetSeniority(int seniority)
        {
            Seniority = seniority;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetSurname(string surname)
        {
            Surname = surname;
        }
    }

}
