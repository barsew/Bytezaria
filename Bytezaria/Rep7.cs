using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bytezaria7
{

    public class Line
    {
        public List<Tuple<string, Object>> LineList { get; set; }

        public Line(string numberHex, int numberDec, string commonName, List<Bytezaria0.Stop> stops, List<Bytezaria0.Vehicle> vehicles)
        {
            LineList = new List<Tuple<string, Object>>();

            LineList.Add(Tuple.Create("NumberHex", (Object)numberHex));
            LineList.Add(Tuple.Create("NumberDec", (Object)numberDec));
            LineList.Add(Tuple.Create("CommonName", (Object)commonName));
            LineList.Add(Tuple.Create("Stops", (Object)stops));
            LineList.Add(Tuple.Create("Vehicles", (Object)vehicles));
        }

        public override string ToString()
        {
            string s = $"Line: \n NumberHex: {LineList[0].Item2} \n NumberDec: {LineList[1].Item2} \n CommonName: {LineList[2].Item2} \n with Stops:";

            if (LineList[3].Item2 != null)
            {
                foreach (var stop in (List<object>)LineList[3].Item2)
                {
                    s += "  " + stop.ToString();
                }
            }

            s += "\n with Vehicles:";

            if (LineList[4].Item2 != null)
            {
                foreach (var vehicle in (List<object>)LineList[4].Item2)
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }
    }
    public class Stop
    {

        public enum type
        {
            bus = 1,
            tram = 2,
            other = 3
        }
        public List<Tuple<string, Object>> StopList { get; }

        public Stop(int id, string name, type type, List<Bytezaria0.Line> lines)
        {
            StopList = new List<Tuple<string, Object>>();

            StopList.Add(Tuple.Create("Id", (Object)id));
            StopList.Add(Tuple.Create("Name", (Object)name));
            StopList.Add(Tuple.Create("Type", (Object)type));
            StopList.Add(Tuple.Create("Lines", (Object)lines));
        }
        public override string ToString()
        {
            string s = $"Stop: \n Id: {StopList[0].Item2} \n Name: {StopList[1].Item2} \n Type: {StopList[2].Item2} \n with Lines:";

            if (StopList[3].Item2 != null)
            {
                foreach (var line in (List<object>)StopList[3].Item2)
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }
    }
    public class Bytebus : Vehicle
    {
        public enum engineClass
        {
            Byte5 = 1,
            bise120 = 2,
            gibagaz = 3
        }
        public List<Tuple<string, Object>> BytebusList { get; set; }

        public Bytebus(int id, List<Bytezaria0.Line> lines, engineClass engineClass) : base(id.ToString())
        {
            BytebusList = new List<Tuple<string, Object>>();

            BytebusList.Add(Tuple.Create("Lines", (Object)lines));
            BytebusList.Add(Tuple.Create("EngineClass", (Object)engineClass));
        }
        public override string ToString()
        {
            string s = $"Bytebus: \n EngineClass: {BytebusList[0].Item2} \n with Lines:";

            if (BytebusList[1].Item2 != null)
            {
                foreach (var line in (List<object>)BytebusList[1].Item2)
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }
    }
    public class Tram : Vehicle
    {
        public List<Tuple<string, Object>> TramList { get; set; }

        public Tram(int id, int carsNumber, Line line) : base(id.ToString())
        {
            TramList =  new List<Tuple<string, Object>>();

            TramList.Add(Tuple.Create("CarsNumber", (Object)carsNumber));
            TramList.Add(Tuple.Create("Line", (Object)line));
        }
        public override string ToString()
        {
            string s = $"Tram: \n CarsNuber: {TramList[0].Item2} \n with Line:";

            if (TramList[1].Item2 != null)
            {
                s += "  " + TramList[0].Item2.ToString();
            }

            return s;
        }
    }
    public class Driver
    {

        public List<Tuple<string, Object>> DriverList { get; set; }

        public Driver(string name, string surname, int seniority, List<Bytezaria0.Vehicle> vehicles)
        {
            DriverList = new List<Tuple<string, Object>>();

            DriverList.Add(Tuple.Create("Name", (Object)name));
            DriverList.Add(Tuple.Create("Surname", (Object)surname));
            DriverList.Add(Tuple.Create("Seniority", (Object)seniority));
            DriverList.Add(Tuple.Create("Vehicles", (Object)vehicles));
        }
        public override string ToString()
        {
            string s = $"Driver: \n Name: {DriverList[0].Item2} \n Surname: {DriverList[1].Item2} \n Seniority: {DriverList[2].Item2} \n with Vehicles:";

            if (DriverList[3].Item2 != null)
            {
                foreach (var vehicle in (List<object>)DriverList[3].Item2)
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }

    }
    public abstract class Vehicle
    {
        public List<Tuple<string, Object>> VehicleList { get; set; }

        public Vehicle(string id)
        {
            VehicleList = new List<Tuple<string, Object>>();

            VehicleList.Add(Tuple.Create("Id", (Object)id));
        }
    }

}
