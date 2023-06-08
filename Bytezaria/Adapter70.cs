using Bytezaria0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bytezaria
{
    public class AdapterLine70 : Bytezaria0.ILine
    {
        private Bytezaria7.Line _line { get; set; }

        public AdapterLine70(Bytezaria7.Line line)
        {
            _line = line;
        }

        public string GetCommonName()
        {
            string name = "";
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "CommonName")
                {
                    name = (string)item.Item2;
                }
            }
            return name;
        }

        public int GetNumberDec()
        {
            int num = 0;
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "NumberDec")
                {
                    num = (int)item.Item2;
                }
            }
            return num;
        }

        public string GetNumberHex()
        {
            string num = "";
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "NumberHex")
                {
                    num = (string)item.Item2;
                }
            }
            return num;
        }

        public List<Stop> GetStops()
        {
            List<Bytezaria0.Stop> stops = new List<Bytezaria0.Stop>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "Stops")
                {
                    stops = (List<Bytezaria0.Stop>)item.Item2;
                }
            }
            return stops;
        }

        public List<Vehicle> GetVehicles()
        {
            List<Bytezaria0.Vehicle> vehicles = new List<Vehicle>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "Vehicles")
                {
                    vehicles = (List<Bytezaria0.Vehicle>)item.Item2;
                }
            }
            return vehicles;
        }

        public void SetCommonName(string s)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _line.LineList)
            {
                if(item.Item1 == "CommonName")
                {
                    List.Add(new Tuple<string, object>("CommonName", s));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _line.LineList = new List<Tuple<string, object>>(List);
        }

        public void SetNumberDec(int a)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "NumberDec")
                {
                    List.Add(new Tuple<string, object>("NumberDec", a));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _line.LineList = new List<Tuple<string, object>>(List);
        }

        public void SetNumberHex(string s)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "NumberHex")
                {
                    List.Add(new Tuple<string, object>("NumberHex", s));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _line.LineList = new List<Tuple<string, object>>(List);
        }

        public void SetStops(List<Stop> stops)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "Stops")
                {
                    List.Add(new Tuple<string, object>("Stops", stops));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _line.LineList = new List<Tuple<string, object>>(List);
        }

        public void SetVehicles(List<Vehicle> vehciles)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _line.LineList)
            {
                if (item.Item1 == "Vehicles")
                {
                    List.Add(new Tuple<string, object>("Vehicles", vehciles));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _line.LineList = new List<Tuple<string, object>>(List);
        }

        public override string ToString()
        {
            string s = $"Line: \n NumberHex: {this.GetNumberHex()} \n NumberDec: {this.GetNumberDec()} \n CommonName: {this.GetCommonName()} \n with Stops:";

            if (this.GetStops() != null)
            {
                foreach (var stop in GetStops())
                {
                    s += "  " + stop.ToString();
                }
            }

            s += "\n with Vehicles:";

            if (this.GetVehicles() != null)
            {
                foreach (var vehicle in GetVehicles())
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }
    }

    public class AdapterDriver70 : Bytezaria0.IDriver
    {
        private Bytezaria7.Driver _driver;

        public AdapterDriver70(Bytezaria7.Driver driver)
        {
            _driver = driver;
        }

        public List<Bytezaria0.Vehicle> GetVehicles()
        {
            List<Bytezaria0.Vehicle> vehicles = new List<Vehicle>();
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Vehicles")
                {
                    vehicles = (List<Bytezaria0.Vehicle>)item.Item2;
                }
            }
            return vehicles;
        }

        public int GetSeniority()
        {
            int sen = 0;
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Seniority")
                {
                    sen = (int)item.Item2;
                }
            }
            return sen;
        }

        public string GetName()
        {
            string name = "";
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Name")
                {
                    name = (string)item.Item2;
                }
            }
            return name;
        }

        public string GetSurname()
        {
            string surname = "";
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Surname")
                {
                    surname = (string)item.Item2;
                }
            }
            return surname;
        }
        public override string ToString()
        {
            string s = $"Driver: \n Name: {this.GetName()} \n Surname: {this.GetSurname()} \n Seniority: {this.GetSeniority()} \n with Vehicles:";

            if (this.GetVehicles() != null)
            {
                foreach (var vehicle in this.GetVehicles())
                {
                    s += "  " + vehicle.ToString();
                }
            }

            return s;
        }

        public void SetVehicles(List<Vehicle> vehicles)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Vehicles")
                {
                    List.Add(new Tuple<string, object>("Vehicles", vehicles));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _driver.DriverList = new List<Tuple<string, object>>(List);
        }

        public void SetSeniority(int seniority)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Seniority")
                {
                    List.Add(new Tuple<string, object>("Seniority", seniority));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _driver.DriverList = new List<Tuple<string, object>>(List);
        }

        public void SetName(string name)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Name")
                {
                    List.Add(new Tuple<string, object>("Name", name));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _driver.DriverList = new List<Tuple<string, object>>(List);
        }

        public void SetSurname(string surname)
        {
            List<Tuple<string, Object>> List = new List<Tuple<string, object>>();
            foreach (var item in _driver.DriverList)
            {
                if (item.Item1 == "Surname")
                {
                    List.Add(new Tuple<string, object>("Surname", surname));
                }
                else
                {
                    List.Add(new Tuple<string, object>(item.Item1, item.Item2));
                }
            }
            _driver.DriverList = new List<Tuple<string, object>>(List);
        }
    }
    public class AdapterStop70 : Bytezaria0.IStop
    {
        Bytezaria7.Stop _stop;
        public AdapterStop70(Bytezaria7.Stop stop)
        {
            _stop = stop;
        }

        public int GetId()
        {
            int id = 0;
            foreach (var item in _stop.StopList)
            {
                if (item.Item1 == "Id")
                {
                    id = (int)item.Item2;
                }
            }
            return id;
        }

        public List<int> GetLines()
        {
            List<int> lines = new List<int>();
            foreach (var item in _stop.StopList)
            {
                if (item.Item1 == "Lines")
                {
                    lines = (List<int>)item.Item2;
                }
            }
            return lines;
        }

        public string GetName()
        {
            string name = "";
            foreach (var item in _stop.StopList)
            {
                if (item.Item1 == "Name")
                {
                    name = (string)item.Item2;
                }
            }
            return name;
        }

        Stop.type IStop.GetType()
        {
            Bytezaria0.Stop.type t = Bytezaria0.Stop.type.other;
            foreach (var item in _stop.StopList)
            {
                if (item.Item1 == "Type")
                {
                    t = (Bytezaria0.Stop.type)item.Item2;
                }
            }
            return t;
        }
        public override string ToString()
        {
            string s = $"Stop: \n Id: {this.GetId()} \n Name: {this.GetName()} \n Type: {this.GetType()} \n with Lines:";

            if (this.GetLines() != null)
            {
                foreach (var line in this.GetLines())
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }

        public void SetId(int id)
        {
            throw new NotImplementedException();
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetType(Stop.type type)
        {
            throw new NotImplementedException();
        }

        public void SetLines(List<int> lines)
        {
            throw new NotImplementedException();
        }
    }

    public class AdapterVehicle70 : Bytezaria0.IVehicle
    {
        Bytezaria7.Vehicle _vehicle;
        public AdapterVehicle70(Bytezaria7.Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public int GetId()
        {
            int id = 0;
            foreach (var item in _vehicle.VehicleList)
            {
                if (item.Item1 == "Id")
                {
                    id = (int)item.Item2;
                }
            }
            return id;
        }

        public void SetId(int id)
        {
            throw new NotImplementedException();
        }
    }
    public class AdapterBytebus70 : Bytezaria0.IBytebus
    {
        Bytezaria7.Bytebus _bus;
        public AdapterBytebus70(Bytezaria7.Bytebus bus)
        {
            _bus = bus;
        }

        public Bytebus.engineClass GetEngineClass()
        {
            Bytezaria0.Bytebus.engineClass engine = Bytezaria0.Bytebus.engineClass.Byte5;
            foreach (var item in _bus.BytebusList)
            {
                if (item.Item1 == "EngineClass")
                {
                    engine = (Bytezaria0.Bytebus.engineClass)item.Item2;
                }
            }
            return engine;
        }

        public int GetId()
        {
            int id = 0;
            foreach (var item in _bus.VehicleList)
            {
                if (item.Item1 == "Id")
                {
                    id = (int)item.Item2;
                }
            }
            return id;
        }

        public List<Line> GetLines()
        {
            List<Bytezaria0.Line> lines = new List<Bytezaria0.Line>();
            foreach (var item in _bus.VehicleList)
            {
                if (item.Item1 == "Id")
                {
                    lines = (List<Bytezaria0.Line>)item.Item2;
                }
            }
            return lines;
        }

        public void SetEngineClass(Bytebus.engineClass engine)
        {
            throw new NotImplementedException();
        }

        public void SetId(int id)
        {
            throw new NotImplementedException();
        }

        public void SetLines(List<Line> lines)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string s = $"Bytebus: \n EngineClass: {this.GetEngineClass()} \n with Lines:";

            if (this.GetLines() != null)
            {
                foreach (var line in this.GetLines())
                {
                    s += "  " + line.ToString();
                }
            }

            return s;
        }
    }
    public class AdapterTram70 : Bytezaria0.ITram
    {
        Bytezaria7.Tram _tram;
        public AdapterTram70(Bytezaria7.Tram tram)
        {
            _tram = tram;
        }

        public int GetCarsNumber()
        {
            int num = 0;
            foreach (var item in _tram.VehicleList)
            {
                if (item.Item1 == "CarsNumber")
                {
                    num = (int)item.Item2;
                }
            }
            return num;
        }

        public int GetId()
        {
            int id = 0;
            foreach (var item in _tram.VehicleList)
            {
                if (item.Item1 == "Id")
                {
                    id = (int)item.Item2;
                }
            }
            return id;
        }

        public Line GetLine()
        {
            Bytezaria0.Line line = default;
            foreach (var item in _tram.TramList)
            {
                if (item.Item1 == "Line")
                {
                    line = (Bytezaria0.Line)item.Item2;
                }
            }
            return line;
        }

        public void SetCarsNumber(int carsNumber)
        {
            throw new NotImplementedException();
        }

        public void SetId(int id)
        {
            throw new NotImplementedException();
        }

        public void SetLine(Line line)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string s = $"Tram: \n CarsNuber: {this.GetCarsNumber()} \n with Line:";

            if (this.GetLine() != null)
            {
                s += "  " + this.GetLine().ToString();
            }

            return s;
        }
    }

}