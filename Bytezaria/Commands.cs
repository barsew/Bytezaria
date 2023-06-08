using Bytezaria0;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Bytezaria
{
    public static class DataForCommands
    {
        public static Dictionary<(Type, string), Func<object, object>> getters;
        public static Dictionary<(string, string), Action<object, object>> setters;
        public static Dictionary<string, Type> types;
        public static Dictionary<(string, string), Func<object>> objects;
        public static Dictionary<string, List<string>> fields;

        //collections
        public static DoubledLinkedList<object> lines;
        public static DoubledLinkedList<object> stops;
        public static DoubledLinkedList<object> drivers;

        public static Dictionary<string, Bytezaria.ICollection<object>> collections;

        public static void FillDictionary()
        {
            getters = new Dictionary<(Type, string), Func<object, object>>();
            setters = new Dictionary<(string, string), Action<object, object>>();
            types = new Dictionary<string, Type>();
            objects = new Dictionary<(string, string), Func<object>>();
            fields = new Dictionary<string, List<string>>();

            // Line
            getters.Add((typeof(Bytezaria0.ILine), "numberhex"), x => ((Bytezaria0.ILine)x).GetNumberHex());
            getters.Add((typeof(Bytezaria0.ILine), "numberdec"), x => ((Bytezaria0.ILine)x).GetNumberDec());
            getters.Add((typeof(Bytezaria0.ILine), "commonname"), x => ((Bytezaria0.ILine)x).GetCommonName());
            getters.Add((typeof(Bytezaria0.ILine), "stops"), x => ((Bytezaria0.ILine)x).GetStops());
            getters.Add((typeof(Bytezaria0.ILine), "vehicles"), x => ((Bytezaria0.ILine)x).GetVehicles());

            setters.Add(("line", "numberhex"), (x, val) => ((Bytezaria0.ILine)x).SetNumberHex((string)val));
            setters.Add(("line", "numberdec"), (x, val) => ((Bytezaria0.ILine)x).SetNumberDec((int)val));
            setters.Add(("line", "commonname"), (x, val) => ((Bytezaria0.ILine)x).SetCommonName((string)val));
            // Stop
            getters.Add((typeof(Bytezaria0.IStop), "id"), x => ((Bytezaria0.IStop)x).GetId());
            getters.Add((typeof(Bytezaria0.IStop), "name"), x => ((Bytezaria0.IStop)x).GetName());
            getters.Add((typeof(Bytezaria0.IStop), "type"), x => ((Bytezaria0.IStop)x).GetType());
            getters.Add((typeof(Bytezaria0.IStop), "lines"), x => ((Bytezaria0.IStop)x).GetLines());

            setters.Add(("stop", "id"), (x, val) => ((Bytezaria0.IStop)x).SetId((int)val));
            setters.Add(("stop", "name"), (x, val) => ((Bytezaria0.IStop)x).SetName((string)val));
            setters.Add(("stop", "type"), (x, val) => ((Bytezaria0.IStop)x).SetType((Bytezaria0.Stop.type)val));
            // Bytebus
            getters.Add((typeof(Bytezaria0.IBytebus), "engineclass"), x => ((Bytezaria0.IBytebus)x).GetEngineClass());
            getters.Add((typeof(Bytezaria0.IBytebus), "id"), x => ((Bytezaria0.IBytebus)x).GetId());

            setters.Add(("bytebus", "engineclass"), (x, val) => ((Bytezaria0.IBytebus)x).SetEngineClass((Bytezaria0.Bytebus.engineClass)val));
            setters.Add(("bytebus", "id"), (x, val) => ((Bytezaria0.IBytebus)x).SetId((int)val));
            // Tram
            getters.Add((typeof(Bytezaria0.ITram), "carsnumber"), x => ((Bytezaria0.ITram)x).GetCarsNumber());
            getters.Add((typeof(Bytezaria0.ITram), "id"), x => ((Bytezaria0.ITram)x).GetId());

            setters.Add(("tram", "carsnumber"), (x, val) => ((Bytezaria0.ITram)x).SetCarsNumber((int)val));
            setters.Add(("tram", "id"), (x, val) => ((Bytezaria0.ITram)x).SetId((int)val));
            // Driver
            getters.Add((typeof(Bytezaria0.IDriver), "name"), x => ((Bytezaria0.IDriver)x).GetName());
            getters.Add((typeof(Bytezaria0.IDriver), "surname"), x => ((Bytezaria0.IDriver)x).GetSurname());
            getters.Add((typeof(Bytezaria0.IDriver), "seniority"), x => ((Bytezaria0.IDriver)x).GetSeniority());

            setters.Add(("driver", "name"), (x, val) => ((Bytezaria0.IDriver)x).SetName((string)val));
            setters.Add(("driver", "surname"), (x, val) => ((Bytezaria0.IDriver)x).SetSurname((string)val));
            setters.Add(("driver", "seniority"), (x, val) => ((Bytezaria0.IDriver)x).SetSeniority((int)val));
            // types
            types.Add("line", typeof(Bytezaria0.ILine));
            types.Add("stop", typeof(Bytezaria0.IStop));
            types.Add("bytebus", typeof(Bytezaria0.IBytebus));
            types.Add("tram", typeof(Bytezaria0.ITram));
            types.Add("driver", typeof(Bytezaria0.IDriver));

            // Objects
            objects.Add(("line", "base"), () => new Bytezaria0.Line(" ", 0, " ", null, null));
            objects.Add(("stop", "base"), () => new Bytezaria0.Stop(0, " ", Bytezaria0.Stop.type.other, null));
            objects.Add(("driver", "base"), () => new Bytezaria0.Driver(" ", " ", 0, null));
            objects.Add(("tram", "base"), () => new Bytezaria0.Tram(0, 0, null));
            objects.Add(("bytebus", "base"), () => new Bytezaria0.Bytebus(0, null, Bytezaria0.Bytebus.engineClass.gibagaz));

            objects.Add(("line", "secondary"), () => new AdapterLine70(new Bytezaria7.Line(" ", 0, " ", null, null)));
            objects.Add(("stop", "secondary"), () => new AdapterStop70(new Bytezaria7.Stop(0, " ", Bytezaria7.Stop.type.other, null)));
            objects.Add(("driver", "secondary"), () => new AdapterDriver70(new Bytezaria7.Driver(" ", " ", 0, null)));
            objects.Add(("tram", "secondary"), () => new AdapterTram70(new Bytezaria7.Tram(0, 0, null)));
            objects.Add(("bytebus", "secondary"), () => new AdapterBytebus70(new Bytezaria7.Bytebus(0, null, Bytezaria7.Bytebus.engineClass.gibagaz)));

            // Fields
            fields.Add("line", new List<string> { "NumberHex", "NumberDec", "CommonName" });
            fields.Add("stop", new List<string> { "Id", "Name", "Type" });
            fields.Add("driver", new List<string> { "Name", "Surname", "Seniority" });
            fields.Add("tram", new List<string> { "CarsNumber", "Id" });
            fields.Add("bytbus", new List<string> { "EngineClass", "Id" });
        }

        public static void FillDrivers()
        {
            drivers = new DoubledLinkedList<object>();
            drivers.Add(new Bytezaria0.Driver("Robert", "Kubica", 45, null));
            drivers.Add(new AdapterDriver70(new Bytezaria7.Driver("Kajetan", "Kajetanowicz", 55, null)));
        }

        public static void FillStops()
        {
            stops = new DoubledLinkedList<object>();
            List<int> lines = new List<int>
            {
                1,
                2
            };
            stops.Add(new Bytezaria0.Stop(1, "Wolska", Bytezaria0.Stop.type.tram, lines));
            stops.Add(new Bytezaria0.Stop(10, "Politechnika", Bytezaria0.Stop.type.bus, null));
        }

        public static void FillLines()
        {
            lines = new DoubledLinkedList<object>();
            List<Bytezaria0.Stop> stops = new List<Bytezaria0.Stop>
            {
                new Bytezaria0.Stop(1, "Wolska", Bytezaria0.Stop.type.tram, null)
            };

            lines.Add(new Bytezaria0.Line("E10", 1, "Okopowa", stops, null));
            lines.Add(new Bytezaria0.Line("T1", 2, "Wola", null, null));
            lines.Add(new Bytezaria0.Line("Z2", 3, "Opoczno", null, null));
            lines.Add(new AdapterLine70(new Bytezaria7.Line("AAA", 101, "przystanek", null, null)));
        }

        public static void FillCollections()
        {
            collections = new Dictionary<string, ICollection<object>>();

            collections.Add("line", lines);
            collections.Add("stop", stops);
            collections.Add("driver", drivers);
        }
        public static List<(string fieldName, string op, string value)> ExtractCommandFind(string[] cmd)
        {
            List<(string fieldName, string op, string value)> list = new List<(string fieldName, string op, string value)>();

            string op = "";
            for (int i = 2; i < cmd.Length; i++)
            {

                if (cmd[i].Contains("="))
                {
                    op = "=";
                }
                else if (cmd[i].Contains("<"))
                {
                    op = "<";
                }
                else
                {
                    op = ">";
                }
                cmd[i].Replace("\"", string.Empty);
                var current = cmd[i].Split('=', '<', '>');
                var ss = current[1].Split('"', '\\');
                if (ss.Length > 1)
                {
                    list.Add((current[0], op, ss[1]));
                }
                else
                {
                    list.Add((current[0], op, ss[0]));
                }
            }

            return list;
        }
    }

    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    [Serializable]
    public class ListCommand : ICommand
    {
        public string fullCommand;
        public string className;

        public ListCommand(string fullCommand)
        {
            this.fullCommand = fullCommand;
            var tab = fullCommand.Split();
            if(tab.Length > 1)
            {
                className = tab[1];
            }
            
        }
        public ListCommand() { }


        public void Execute()
        {
            var it = DataForCommands.collections[className].GetIterator();
            it.First();
            while (it.HasNext())
            {
                var tmp = it.Current();
                Console.WriteLine(tmp.ToString());
                it.Next();
            }
        }

        public override string ToString()
        {
            return fullCommand;
        }

        public void Undo()
        {
            ;
        }
    }

    [Serializable]
    public class FindCommand : ICommand
    {
        public string fullCommand;
        public List<(string fieldName, string op, string value)> req;
        public string className;

        public FindCommand(List<(string fieldName, string op, string value)> req, string className, string fullCommand)
        {
            this.req = req;
            this.className = className;
            this.fullCommand = fullCommand;
        }

        public FindCommand() { }

        public void Execute()
        {
            bool isPassed = false;

            var it = DataForCommands.collections[className].GetIterator();
            it.First();
            while (it.HasNext())
            {
                var tmp = it.Current();

                isPassed = true;
                foreach (var r in req)
                {
                    if(r.op == "=")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) != int.Parse(r.value))
                            {
                                isPassed = false;
                            }
                        }
                        else if(DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {
                            if ((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) != r.value)
                            {
                                isPassed = false;
                            }
                        }
                    }
                    else if(r.op == "<")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if ((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) >= int.Parse(r.value))
                            {   
                                isPassed = false;
                            }
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {
                            if (String.Compare((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp), r.value) >= 0)
                            {
                                isPassed = false;
                            }
                        }
                    }
                    else if (r.op == ">")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if ((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) <= int.Parse(r.value))
                            {
                                isPassed = false;
                            }
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {
                            if (String.Compare((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp), r.value) <= 0)
                            {
                                isPassed = false;
                            }
                        }

                    }
                }

                if(isPassed)
                {
                    Console.WriteLine(tmp.ToString());
                }

                it.Next();
            }

        }
        public List<object> ExecuteAndReturn()
        {
            List<object> list = new List<object>();
            bool isPassed = false;

            var it = DataForCommands.collections[className].GetIterator();
            it.First();
            while (it.HasNext())
            {
                var tmp = it.Current();

                isPassed = true;
                foreach (var r in req)
                {
                    if (r.op == "=")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if ((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) != int.Parse(r.value))
                            {
                                isPassed = false;
                            }
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {
                            if ((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) != r.value)
                            {
                                isPassed = false;
                            }
                        }
                    }
                    else if (r.op == "<")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if ((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) >= int.Parse(r.value))
                            {
                                isPassed = false;
                            }
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {
                            if (String.Compare((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp), r.value) >= 0)
                            {
                                isPassed = false;
                            }
                        }
                    }
                    else if (r.op == ">")
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(int))
                        {
                            if ((int)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp) <= int.Parse(r.value))
                            {
                                isPassed = false;
                            }
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp).GetType() == typeof(string))
                        {   
                            if (String.Compare((string)DataForCommands.getters[(DataForCommands.types[className.ToLower()], r.fieldName.ToLower())].Invoke(tmp), r.value) <= 0)
                            {   
                                isPassed = false;
                            }
                        }

                    }
                }

                if (isPassed)
                {
                    list.Add(tmp);
                }

                it.Next();
            }
            return list;
        }
        public override string ToString()
        {
            return fullCommand;
        }

        public void Undo()
        {
            ;
        }
    }

    [Serializable]
    public class AddCommand: ICommand
    {
        public string fullCommand;
        public string className;
        public string representation;

        public List<string> dataList;

        public AddCommand(string className, string representation, string fullCommand, List<string> dataList = null)
        {
            this.className = className;
            this.representation = representation;
            this.fullCommand = fullCommand;
            if(dataList != null)
            {
                this.dataList = new List<string>(dataList);
            }
            else
            {
                this.dataList = new List<string>();
            }
        }

        public AddCommand() { }

        public void GetData()
        {
            string s = "[Availabe fields: ";
            try
            {
                foreach (var field in DataForCommands.fields[className.ToLower()])
                {
                    s += field + ", ";
                }
            }
            catch
            {
                Console.WriteLine("Bad class name...");
                return;
            }

            s += "]";
            Console.WriteLine(s);

            string rec = "";


            while (true)
            {
                rec = Console.ReadLine();

                if (rec.ToUpper() == "EXIT")
                {
                    dataList.Add(rec);
                    break;
                }

                if (rec.ToUpper() == "DONE")
                {
                    dataList.Add(rec);
                    break;
                }

                var tab = rec.Split('=');

                if (DataForCommands.setters.ContainsKey((className.ToLower(), tab[0].ToLower())) == false)
                {
                    Console.WriteLine("Field doesn't exist...");
                    continue;
                }

                dataList.Add(rec);

            }
        }

        public void Execute()
        {
            var obj = DataForCommands.objects[(className.ToLower(), representation)].Invoke();

            foreach (string rec in dataList)
            {
                if (rec.ToUpper() == "EXIT")
                {
                    Console.WriteLine("[Adding of object has been stopped]");
                    break;
                }

                if (rec.ToUpper() == "DONE")
                {
                    DataForCommands.collections[className].Add(obj);
                    Console.WriteLine("[Object added]");
                    break;
                }

                var tab = rec.Split('=');

                if (DataForCommands.setters.ContainsKey((className.ToLower(), tab[0].ToLower())) == false)
                {
                    Console.WriteLine("Field doesn't exist...");
                    continue;
                }

                if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], tab[0].ToLower())].Invoke(obj).GetType() == typeof(string))
                {
                    DataForCommands.setters[(className.ToLower(), tab[0].ToLower())].Invoke(obj, tab[1]);
                }
                else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], tab[0].ToLower())].Invoke(obj).GetType() == typeof(int))
                {
                    try
                    {
                        DataForCommands.setters[(className.ToLower(), tab[0].ToLower())].Invoke(obj, int.Parse(tab[1]));
                    }
                    catch
                    {
                        Console.WriteLine("Bad data...");
                        return;
                    }
                }

            }
        }
        public override string ToString()
        {
            string s = fullCommand + "\n";
            foreach (var item in dataList)
            {
                s += item + "\n";
            }
            return s;
        }

        public void Undo()
        {
            List<(string, string, string)> req = new List<(string, string, string)>();
            foreach (var data in dataList)
            {
                if(data.ToLower() == "exit")
                {
                    return;
                }
                if(data.ToLower() == "done")
                {
                    break;
                }
                var x = data.Split('=');
                req.Add((x[0], "=", x[1]));
            }

            var del = new DeleteCommand(className, fullCommand, req);
            del.Execute();
        }
    }

    [Serializable]
    public class EditCommand : ICommand
    {
        public string fullCommand;
        public string className;
        public List<(string fieldName, string op, string value)> req;
        public List<string> dataList;

        private object objToUndo = null;
        private List<(string, object)> dataToUndo;
        public EditCommand(string className, string fullCommand, List<(string fieldName, string op, string value)> req)
        {
            this.className = className;
            this.fullCommand = fullCommand;
            this.req = req;
            this.dataList = new List<string>();
            this.dataToUndo = new List<(string, object)>();
        }

        public EditCommand() { }

        public void Execute()
        {
            var findCommand = new FindCommand(req, className, fullCommand);
            var findOutcome = findCommand.ExecuteAndReturn();

            if (findOutcome.Count == 0)
            {
                Console.WriteLine("No objects that meet the requirements...");
                return;
            }
            else if (findOutcome.Count > 1)
            {
                Console.WriteLine("Attemp of modification more tha one object at the same time...");
                return;
            }

            objToUndo = findOutcome[0];

            var obj = findOutcome[0];
            List<(string, string)> fieldsToChange = new List<(string, string)>();
            foreach (string rec in dataList)
            {

                if (rec.ToUpper() == "EXIT")
                {
                    Console.WriteLine("[Modifying of the object has been stopped, changes hasn't been saved]");
                    break;
                }

                if (rec.ToUpper() == "DONE")
                {
                    foreach (var field in fieldsToChange)
                    {
                        if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(obj).GetType() == typeof(string))
                        {
                            dataToUndo.Add((field.Item1.ToLower(), DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(obj)));
                            DataForCommands.setters[(className.ToLower(), field.Item1.ToLower())].Invoke(obj, field.Item2);
                        }
                        else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(obj).GetType() == typeof(int))
                        {
                            try
                            {
                                dataToUndo.Add((field.Item1.ToLower(), DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(obj)));
                                DataForCommands.setters[(className.ToLower(), field.Item1.ToLower())].Invoke(obj, int.Parse(field.Item2));
                            }
                            catch
                            {
                                Console.WriteLine("Bad data...");
                                break;
                            }
                        }
                    }
                    Console.WriteLine("[Object has been modified]");
                    break;
                }

                var tab = rec.Split('=');

                if (DataForCommands.setters.ContainsKey((className.ToLower(), tab[0].ToLower())) == false)
                {
                    Console.WriteLine("Field doesn't exist...");
                    continue;
                }

                fieldsToChange.Add((tab[0], tab[1]));
            }
        }
        public void GetData()
        {

            string s = "[Availabe fields: ";
            try
            {
                foreach (var field in DataForCommands.fields[className.ToLower()])
                {
                    s += field + ", ";
                }
            }
            catch
            {
                Console.WriteLine("Bad class name...");
                return;
            }

            s += "]";
            Console.WriteLine(s);

            string rec = "";


            while (true)
            {
                rec = Console.ReadLine();

                if (rec.ToUpper() == "EXIT")
                {
                    dataList.Add(rec);
                    break;
                }

                if (rec.ToUpper() == "DONE")
                {
                    dataList.Add(rec);
                    break;
                }

                var tab = rec.Split('=');

                if (DataForCommands.setters.ContainsKey((className.ToLower(), tab[0].ToLower())) == false)
                {
                    Console.WriteLine("Field doesn't exist...");
                    continue;
                }

                dataList.Add(rec);

            }
        }

        public override string ToString()
        {
            string s = fullCommand + "\n";
            foreach (var item in dataList)
            {
                s += item + "\n";
            }
            return s;
        }

        public void Undo()
        {
            if(dataToUndo == null)
            {
                return;
            }
            foreach (var field in dataToUndo)
            {
                if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(objToUndo).GetType() == typeof(string))
                {
                    DataForCommands.setters[(className.ToLower(), field.Item1.ToLower())].Invoke(objToUndo, field.Item2);
                }
                else if (DataForCommands.getters[(DataForCommands.types[className.ToLower()], field.Item1.ToLower())].Invoke(objToUndo).GetType() == typeof(int))
                {
                    try
                    {
                        DataForCommands.setters[(className.ToLower(), field.Item1.ToLower())].Invoke(objToUndo, field.Item2);
                    }
                    catch
                    {
                        Console.WriteLine("Bad data...");
                        break;
                    }
                }
            }
        }
    }

    [Serializable]
    public class DeleteCommand: ICommand
    {
        public string fullCommand;
        public string className;
        public List<(string fieldName, string op, string value)> req;
        private object deletedObj = null;
        public DeleteCommand(string className, string fullCommand, List<(string fieldName, string op, string value)> req)
        {
            this.className = className;
            this.fullCommand = fullCommand;
            this.req = req;
        }

        public DeleteCommand() { }

        public void Execute()
        {
            var findCommand = new FindCommand(req, className, fullCommand);
            var findOutcome = findCommand.ExecuteAndReturn();

            if (findOutcome.Count == 0)
            {
                Console.WriteLine("No objects that meet the requirements...");
                return;
            }
            else if (findOutcome.Count > 1)
            {
                Console.WriteLine("Attemp of deleting more than one object at the same time...");
                return;
            }

            deletedObj = findOutcome[0];
            DataForCommands.collections[className].Remove(findOutcome[0]);
            Console.WriteLine("[Object deleted]");
        }

        public override string ToString()
        {
            return fullCommand;
        }

        public void Undo()
        {
            if (deletedObj != null)
            {
                DataForCommands.collections[className].Add(deletedObj);
            }
        }
    }

    // QueueCommand is unused now
    public class QueueCommand: ICommand
    {
        private string fullCommand;
        private ArrayList commands;
        private string commandText;
        private string fileName;
        private string format;

        public QueueCommand(ArrayList commands, string cmd, string fullCommand)
        {
            this.fullCommand = fullCommand;
            this.commands = commands;
            this.commandText = cmd;

            var tab = fullCommand.Split();

            if(tab.Length == 3)
            {
                fileName = tab[2].ToLower();
                format = "xml";
            }
            else if (tab.Length > 3)
            {
                fileName = tab[2].ToLower();
                if (tab[3].ToLower() == "plaintext")
                {
                    format = "plaintext";
                }
                else
                {
                    format = "xml";
                }
            }
            else
            {
                format = null;
                fileName = null;
            }
           
        }

        public void Execute()
        {
           switch(commandText.ToLower())
            {
                case "print":
                    foreach (var command in commands)
                    {
                        Console.WriteLine(command.ToString());
                    }
                    break;
                case "export":
                    if(format == "plaintext" && fileName != null)
                    {
                        ExportToPlainText();
                    }
                    else if(fileName != null)
                    {
                        ExportToXML();
                    }
                    break;
                case "load":
                    DeserializeFromFile();
                    break;
                case "commit":
                    foreach (var command in commands)
                    {
                        ICommand tmp = (ICommand)command;
                        tmp.Execute();
                    }
                    commands.RemoveRange(0, commands.Count);
                    break;
                case "dismiss":
                    commands.RemoveRange(0, commands.Count);
                    break;
                default:
                    Console.WriteLine("Bad command...");
                    return;
            }
        }

        private void DeserializeFromFile()
        {
            if (fileName.Contains(".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(ListCommand), typeof(FindCommand), typeof(AddCommand), typeof(EditCommand), typeof(DeleteCommand) });
                FileStream fs;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open);
                }
                catch
                {
                    Console.WriteLine("File doesn't exist...");
                    return;
                }
                ArrayList i = (ArrayList)serializer.Deserialize(fs);
                fs.Close();

                commands.AddRange(i);
                Console.WriteLine("[Commands added]");
            }
            else
            {
                string[] lines = File.ReadAllLines(fileName);

                string command = "";
                bool isAddCommandStill = false;
                List<string> dataForAddCommand = null;
                (string, string, string) dataForAddCommandStrings = default;

                ArrayList queue = new ArrayList();

                foreach(var line in lines)
                {
                    if(line == "")
                    {
                        continue;
                    }

                    command = line;

                    if(isAddCommandStill == true)
                    {
                        dataForAddCommand.Add(command);
                        if(command == "exit" || command == "done")
                        {
                            isAddCommandStill = false;
                            try
                            {
                                var newObj = new Bytezaria.AddCommand(dataForAddCommandStrings.Item1, dataForAddCommandStrings.Item2, dataForAddCommandStrings.Item3, dataForAddCommand);
                                queue.Add(newObj);
                            }
                            catch
                            {
                                Console.WriteLine("Bad data...");
                            }
                        }
                        continue;
                    }

                    if (command.ToLower() == "exit")
                    {
                        Console.WriteLine("See you soon!");
                        break;
                    }

                    var cmd = command.Split();
                    if (cmd.Length == 2 && cmd[0].ToLower() == "list")
                    {
                        if (DataForCommands.collections.ContainsKey(cmd[1].ToLower()) == true)
                        {
                            queue.Add(new Bytezaria.ListCommand(command));
                        }
                        else
                        {
                            Console.WriteLine("Bad command...\n");
                        }
                    }
                    else if (cmd.Length > 2 && cmd[0].ToLower() == "find")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.FindCommand(cmdFind, cmd[1], command));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else if (cmd.Length == 3 && cmd[0].ToLower() == "add")
                    {
                        if (cmd[2].ToLower() != "base" && cmd[2].ToLower() != "secondary")
                        {
                            Console.WriteLine("Bad usage...");
                            continue;
                        }

                        isAddCommandStill = true;
                        dataForAddCommand = new List<string>();
                        dataForAddCommandStrings = (cmd[1], cmd[2], command);

                    }
                    else if (cmd.Length >= 2 && cmd[0].ToLower() == "edit")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.EditCommand(cmd[1], command, cmdFind));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else if (cmd.Length > 2 && cmd[0].ToLower() == "delete")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.DeleteCommand(cmd[1], command, cmdFind));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad command...\n");
                    }
                }
                commands.AddRange(queue);
                Console.WriteLine("[Commands added]");
            }
        }

        private void ExportToXML()
        {
            var serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(ListCommand), typeof(FindCommand), typeof(AddCommand), typeof(EditCommand), typeof(DeleteCommand) });
            using (FileStream fs = File.Create(fileName))
            {
                serializer.Serialize(fs, commands);
            }

        }

        private void ExportToPlainText()
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var command in commands)
                {
                    writer.WriteLine(command.ToString());
                }
            }
        }

        public void Undo()
        {
            ;
        }
    }

    public class ExportCommand: ICommand
    {
        private ArrayList executedCoammnds;
        private string fullCommand;
        private string commandText;
        private string fileName;
        private string format;

        public ExportCommand(ArrayList commands, string cmd, string fullCommand)
        {
            this.fullCommand = fullCommand;
            this.executedCoammnds = commands;
            this.commandText = cmd;

            var tab = fullCommand.Split();

            if (tab.Length == 2)
            {
                fileName = tab[1].ToLower();
                format = "xml";
            }
            else if (tab.Length > 2)
            {
                fileName = tab[1].ToLower();
                if (tab[2].ToLower() == "plaintext")
                {
                    format = "plaintext";
                }
                else
                {
                    format = "xml";
                }
            }
            else
            {
                format = null;
                fileName = null;
            }
        }
        private void ExportToXML()
        {
            var serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(ListCommand), typeof(FindCommand), typeof(AddCommand), typeof(EditCommand), typeof(DeleteCommand) });
            using (FileStream fs = File.Create(fileName))
            {
                serializer.Serialize(fs, executedCoammnds);
            }

        }

        private void ExportToPlainText()
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var command in executedCoammnds)
                {
                    writer.WriteLine(command.ToString());
                }
            }
        }

        public void Execute()
        {
            if (format == "plaintext" && fileName != null)
            {
                ExportToPlainText();
            }
            else if (fileName != null)
            {
                ExportToXML();
            }
        }

        public void Undo()
        {
            ;
        }
    }

    public class LoadCommand : ICommand
    {
        private ArrayList executedCoammnds;

        private string fullCommand;
        private ArrayList commandsToExecute;
        private string commandText;
        private string fileName;

        public LoadCommand(ArrayList commands, string cmd, string fullCommand)
        {
            this.fullCommand = fullCommand;
            this.executedCoammnds = commands;
            this.commandText = cmd;
            this.commandsToExecute = new ArrayList();

            var tab = fullCommand.Split();

            if (tab.Length == 2)
            {
                fileName = tab[1].ToLower();
            }
            else
            {
                fileName = null;
            }
        }

        public void Execute()
        {
            if(fileName == null)
            {
                Console.WriteLine("[Wrtie filename...]");
                return;
            }
            DeserializeFromFile();
            foreach (var cmd in commandsToExecute)
            {
                var x = (ICommand)cmd;
                x.Execute();
                executedCoammnds.Add(x);
            }
        }

        public void Undo()
        {
            ;
        }

        private void DeserializeFromFile()
        {
            if (fileName.Contains(".xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(ListCommand), typeof(FindCommand), typeof(AddCommand), typeof(EditCommand), typeof(DeleteCommand) });
                FileStream fs;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open);
                }
                catch
                {
                    Console.WriteLine("File doesn't exist...");
                    return;
                }
                ArrayList i = (ArrayList)serializer.Deserialize(fs);
                fs.Close();

                commandsToExecute.AddRange(i);
                Console.WriteLine("[Commands added]");  
            }
            else
            {
                string[] lines = File.ReadAllLines(fileName);

                string command = "";
                bool isAddCommandStill = false;
                List<string> dataForAddCommand = null;
                (string, string, string) dataForAddCommandStrings = default;

                ArrayList queue = new ArrayList();

                foreach (var line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }

                    command = line;

                    if (isAddCommandStill == true)
                    {
                        dataForAddCommand.Add(command);
                        if (command == "exit" || command == "done")
                        {
                            isAddCommandStill = false;
                            try
                            {
                                var newObj = new Bytezaria.AddCommand(dataForAddCommandStrings.Item1, dataForAddCommandStrings.Item2, dataForAddCommandStrings.Item3, dataForAddCommand);
                                queue.Add(newObj);
                            }
                            catch
                            {
                                Console.WriteLine("Bad data...");
                            }
                        }
                        continue;
                    }

                    if (command.ToLower() == "exit")
                    {
                        Console.WriteLine("See you soon!");
                        break;
                    }

                    var cmd = command.Split();
                    if (cmd.Length == 2 && cmd[0].ToLower() == "list")
                    {
                        if (DataForCommands.collections.ContainsKey(cmd[1].ToLower()) == true)
                        {
                            queue.Add(new Bytezaria.ListCommand(command));
                        }
                        else
                        {
                            Console.WriteLine("Bad command...\n");
                        }
                    }
                    else if (cmd.Length > 2 && cmd[0].ToLower() == "find")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.FindCommand(cmdFind, cmd[1], command));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else if (cmd.Length == 3 && cmd[0].ToLower() == "add")
                    {
                        if (cmd[2].ToLower() != "base" && cmd[2].ToLower() != "secondary")
                        {
                            Console.WriteLine("Bad usage...");
                            continue;
                        }

                        isAddCommandStill = true;
                        dataForAddCommand = new List<string>();
                        dataForAddCommandStrings = (cmd[1], cmd[2], command);

                    }
                    else if (cmd.Length >= 2 && cmd[0].ToLower() == "edit")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.EditCommand(cmd[1], command, cmdFind));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else if (cmd.Length > 2 && cmd[0].ToLower() == "delete")
                    {
                        List<(string, string, string)> cmdFind = new List<(string, string, string)>();
                        try
                        {
                            cmdFind = DataForCommands.ExtractCommandFind(cmd);
                        }
                        catch
                        {
                            Console.WriteLine("Field name doesn't exist!!!");
                            continue;
                        }

                        try
                        {
                            queue.Add(new Bytezaria.DeleteCommand(cmd[1], command, cmdFind));
                        }
                        catch
                        {
                            Console.WriteLine("Bad format...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad command...\n");
                    }
                }
                commandsToExecute.AddRange(queue);
                Console.WriteLine("[Commands added]");
            }

        }
    }
}
