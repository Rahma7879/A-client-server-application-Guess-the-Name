//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using servertest;

public class Room
{
    public Client Owner { get; set; }
    public Client Opponent { get; set; }
    public string Category { get; set; }
    public string Level { get; set; }
    public string Word { get; set; }
    public string CurrentWord { get; set; }
    public string CurrentTurn { get; set; }
    public int NumOfPlayers { get; set; }
    public int Id { get; set; }
    public int WatcherId { set; get; }
    public static Dictionary<int, Room> Rooms { get; set; } = new Dictionary<int, Room>();
    public Dictionary<int, Client> Watchers { get; set; } = new Dictionary<int, Client>();

    static int Count = 0;

    public Room(Client ownerCons, string categoryCons)
    {
        Owner = ownerCons;
        Category = categoryCons;
       // Level = levelCons; 
        Id = ++Count;
        NumOfPlayers = 1;
        WatcherId = 0;
        string[] words = File.ReadAllLines("words.txt");
        if (Category == "Players")
        {
            Word = words[new Random().Next(0,3)];
        }
        else if (Category == "Cars")
        {
            Word = words[new Random().Next(3,6)];
        }
        else if (Category == "Colors")
        {
            Word = words[new Random().Next(6,9)];
        }
        //Word = words[new Random().Next(words.Length)];
        CurrentWord = Word;
        CurrentTurn = Owner.Name;
        Rooms.Add(Id, this);

    }
    public void SendMessageToBothClients(string message)
    {
        Owner.Bw.Write(message);
        Opponent.Bw.Write(message);
    }

    public bool IsWordFullyGuessed()//
    {
        return !CurrentWord.Contains("_");
    }
}
