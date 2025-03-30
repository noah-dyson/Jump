using System;

namespace CS_Coursework;

public class Level {
    private string _name;
    public string Name { get { return _name; } }
    private TimeSpan _bestTime;
    public TimeSpan BestTime { get { return _bestTime; } set { _bestTime = value; } }
    private int[,] _ids;
    public int[,] Ids { get { return _ids; } set { _ids = value; } }
    private string _filePath;
    public string FilePath { get { return _filePath; } }

    public Level(string name, TimeSpan bestTime, int[,] ids, string filePath = "") {
        _name = name;
        _bestTime = bestTime;
        _ids = ids;
        _filePath = filePath;
    }
}