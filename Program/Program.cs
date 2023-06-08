using Bytezaria;
using Bytezaria0;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            //---------------------Commands--------------------------------------------//
            Console.WriteLine("-----------------Commands-------------------------------------");

            
            DataForCommands.FillLines();
            DataForCommands.FillStops();
            DataForCommands.FillDrivers();

           
            DataForCommands.FillDictionary();
            DataForCommands.FillCollections();

            var commands = new Dictionary<string, ICommand>
            {
                { "list", null },
                { "find", null },
                { "add", null }
            };


            //Algorithm.Print(collections["line"], L => true, true);

            string command = "";
            Console.WriteLine("Commands: list <class>, find <class> <requirements>," +
            " add <class> base|secondary, edit <class> <requirements>, export <filename> <XML|plaintext>, load <filename>, exit");

            // queue of commands
            ArrayList queue = new ArrayList();
            ArrayList executedCommands = new ArrayList();
            ArrayList undoCommands = new ArrayList();

            while (true)
            {
                command = Console.ReadLine();
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
                        var currCmd = new Bytezaria.ListCommand(command);
                        currCmd.Execute();
                        executedCommands.Add(currCmd);
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
                        var currCmd = new Bytezaria.FindCommand(cmdFind, cmd[1], command);
                        currCmd.Execute();
                        executedCommands.Add(currCmd);
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
                        Console.WriteLine("Bledne uzycie...");
                        continue;
                    }

                    try
                    {
                        var currCmd = new Bytezaria.AddCommand(cmd[1], cmd[2], command);
                        currCmd.GetData();
                        currCmd.Execute();
                        executedCommands.Add(currCmd);
                    }
                    catch
                    {
                        Console.WriteLine("Bledne dane...");
                    }

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
                        var currCmd = new Bytezaria.EditCommand(cmd[1], command, cmdFind);
                        currCmd.GetData();
                        currCmd.Execute();
                        executedCommands.Add(currCmd);
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
                        var currCmd = (new Bytezaria.DeleteCommand(cmd[1], command, cmdFind));
                        currCmd.Execute();
                        executedCommands.Add(currCmd);
                    }
                    catch
                    {
                        Console.WriteLine("Bad format...");
                    }
                }
                else if (cmd.Length >= 2 && cmd[0].ToLower() == "queue")
                {
                    var queueCommand = new Bytezaria.QueueCommand(queue, cmd[1], command);
                    queueCommand.Execute();
                }
                else if(cmd[0].ToLower() == "undo")
                {
                    var cmdToUndo = (ICommand)executedCommands[executedCommands.Count - 1];
                    executedCommands.RemoveAt(executedCommands.Count - 1);
                    cmdToUndo.Undo();
                    undoCommands.Add(cmdToUndo);
                }
                else if(cmd[0].ToLower() == "redo")
                {
                    if(undoCommands.Count == 0)
                    {
                        continue;
                    }
                    var cmdToRedo = (ICommand)undoCommands[undoCommands.Count - 1];
                    undoCommands.RemoveAt(undoCommands.Count - 1);
                    cmdToRedo.Execute();
                    executedCommands.Add(cmdToRedo);
                }
                else if (cmd.Length >= 2 && cmd[0].ToLower() == "load")
                {
                    var currCmd = new Bytezaria.LoadCommand(executedCommands, cmd[1], command);
                    currCmd.Execute();
                }
                else if (cmd.Length >= 2 && cmd[0].ToLower() == "export")
                {
                    var currCmd = new Bytezaria.ExportCommand(executedCommands, cmd[1], command);
                    currCmd.Execute();
                }
                else if (cmd[0].ToLower() == "history")
                {
                    foreach (var execCmd in executedCommands)
                    {
                        Console.WriteLine(execCmd.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Bad command...\n");
                }
            }

            Console.WriteLine("---------------------------------------------------------------");

            Console.ReadKey(); 
        }

        public static void Search(Bytezaria0.IDriver[] Idrivers, List<Bytezaria0.Line> lines)
        {
            List<List<Bytezaria0.Vehicle>> vecs = new List<List<Bytezaria0.Vehicle>>();
            foreach (var item in Idrivers)
            {
                if (item.GetSeniority() >= 10)
                {
                    vecs.Add(item.GetVehicles());
                }
            }
            List<Bytezaria0.Line> output = new List<Bytezaria0.Line>();
            foreach (var l in lines)
            {
                foreach (var vv in vecs)
                {
                    foreach (var v in vv)
                    {
                        if (l.Vehicles.Contains(v))
                        {
                            if (output.Contains(l) == false)
                                output.Add(l);
                        }
                    }
                }
            }
            foreach (var l in output)
            {
                Console.WriteLine("Line " + l.CommonName + ", " + l.NumberDec);
            }
        }
    }
}
