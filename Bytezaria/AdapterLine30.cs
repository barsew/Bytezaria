using Bytezaria0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bytezaria
{
    public class AdapterLine30 : Bytezaria0.ILine
    {
        private Bytezaria3.Line _line;

        public AdapterLine30(Bytezaria3.Line line)
        {
            _line = line;
        }

        public string GetCommonName()
        {
            throw new NotImplementedException();
        }

        public int GetNumberDec()
        {
            throw new NotImplementedException();
        }

        public string GetNumberHex()
        {
            throw new NotImplementedException();
        }

        public List<Stop> GetStops()
        {
            throw new NotImplementedException();
        }

        public List<Vehicle> GetVehicles()
        {
            throw new NotImplementedException();
        }

        public void SetCommonName(string s)
        {
            throw new NotImplementedException();
        }

        public void SetNumberDec(int a)
        {
            throw new NotImplementedException();
        }

        public void SetNumberHex(string s)
        {
            throw new NotImplementedException();
        }

        public void SetStops(List<Stop> stops)
        {
            throw new NotImplementedException();
        }

        public void SetVehicles(List<Vehicle> vehciles)
        {
            throw new NotImplementedException();
        }

    }
    public class AdapterStop30: Bytezaria0.IStop
    {
        private Bytezaria3.Stop _stop;

        public AdapterStop30(Bytezaria3.Stop stop)
        {
            _stop = stop;
        }

        public int GetId()
        {
            throw new NotImplementedException();
        }

        public List<int> GetLines()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void SetId(int id)
        {
            throw new NotImplementedException();
        }

        public void SetLines(List<int> lines)
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

        Stop.type IStop.GetType()
        {
            throw new NotImplementedException();
        }

    }
}
