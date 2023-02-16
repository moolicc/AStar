// See https://aka.ms/new-console-template for more information
using Pathing;

Console.WriteLine("Hello, World!");


var iniParser = new IniParser.Parser.IniDataParser();
var ini = iniParser.Parse(File.ReadAllText("cities.ini"));

var goal = ini.Global["goal"]!;
var start = ini.Global["start"]!;


int heuristic(string referenceNode, string goalNode)
    => int.Parse(ini[referenceNode]["h"]);

IEnumerable<(string Neighbor, int Cost)> expand(string sourceNode)
{
    foreach (var value in ini[sourceNode])
    {
        if (value.KeyName == "h")
        {
            continue;
        }
        yield return (value.KeyName, int.Parse(value.Value));
    }
}

var astar = new AStar<string>(heuristic, expand, start, goal);


astar.Step();
astar.Step();
astar.Step();
astar.Step();
astar.Step();
astar.Step();
astar.Step();
astar.Step();