using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servertest
{

    //public class client
    //{
    //    public NetworkStream Stream { get; set; }
    //    public BinaryWriter Bw { get; set; }
    //    public BinaryReader Br { get; set; }
    //    public string Name { get; set; }
    //    public Thread ThreadClient { get; set; }
    //    public bool FlagClientConnected { set; get; }
    //    public client(NetworkStream streamCons, string nameCons)
    //    {
    //        FlagClientConnected = true;
    //        Stream = streamCons;
    //        Br = new BinaryReader(streamCons);
    //        Bw = new BinaryWriter(streamCons);
    //        Name = nameCons;
    //        ThreadClient = new Thread(() => ListenNewMsg(this));
    //        ThreadClient.Start();
    //    }

    //    void ListenNewMsg(client client)
    //    {
    //        while (FlagClientConnected)
    //        {
    //            if (client.Stream.DataAvailable)
    //            {
    //                string[] ClientMsg = client.Br.ReadString().Split(',');
    //                int RoomId;
    //                switch (ClientMsg[0])
    //                {
    //                    case "1":
    //                        string Rooms = "";
    //                        foreach (Room item in Room.Rooms.Values)
    //                        {
    //                            Rooms += item.ToString();
    //                        }

    //                        client.Bw.Write(Rooms);
    //                        break;

    //                    case "2":
    //                        //catch id of room
    //                        Room room = new Room(client, ClientMsg[1], ClientMsg[2]);
    //                        client.Bw.Write(room.Id.ToString() + ',' + room.Word);
    //                        break; // <- This was missing
    //                }
    //            }
    //        }
    //    }

    //  }
    public class Client
    {
        public NetworkStream Stream { get; set; }
        public BinaryWriter Bw { get; set; }
        public BinaryReader Br { get; set; }
        public string Name { get; set; }
        public Thread ThreadClient { get; set; }
        public bool FlagClientConnected { get; set; }
        private bool isPlayer1Turn = true;

        public Client(NetworkStream streamCons, BinaryWriter bw, BinaryReader br, string nameCons)
        {
            FlagClientConnected = true;
            Stream = streamCons;
            Br = br;
            Bw = bw;
            Name = nameCons;
            ThreadClient = new Thread(() => ListenNewMsg(this));
            ThreadClient.Start();
        }

        void ListenNewMsg(Client client)
        {
            while (FlagClientConnected)
            {
                if (client.Stream.DataAvailable)
                {
                    string[] ClientMsg = client.Br.ReadString().Split(',');
                    switch (ClientMsg[0])
                    {
                        case "0":   //when i need a specific room info
                            sendRoomInfo(client, ClientMsg[1]);    //ex:send("0,roomid")=>get the info especially req[5]"WORD"
                            break;
                        case "1":
                            SendRoomsList(client);
                            break;
                        case "2":
                            CreateRoom(client, ClientMsg[1]);
                            break;
                        case "3":
                           int RoomId = int.Parse(ClientMsg[1]);
                            if (Room.Rooms.ContainsKey(RoomId))
                            {
                                if (Room.Rooms[RoomId].Opponent == null)
                                {
                                    Room.Rooms[RoomId].Opponent = client;
                                    
                                    Room.Rooms[RoomId].Owner.Bw.Write($"R,{client.Name} : wants to play with you,{client}");
                                    sendRoomInfo(client, ClientMsg[1]);
                                    Room.Rooms[RoomId].Opponent.Name = Name;
                                    Room.Rooms[RoomId].NumOfPlayers++;
                                    
                                    client.Bw.Write("dimm");
                                }
                                else
                                {
                                    client.Bw.Write("Refresh and Try again");
                                }
                            }
                            else
                            {
                                client.Bw.Write("Room does not exist anymore!! Please Refresh and try agian");
                            }

                            break;
                        //case "3":
                        //    int RoomId = int.Parse(ClientMsg[1]);
                        //    Room.Rooms[RoomId].Opponent = client;   ///important to be before send fun.
                        //    sendRoomInfo(client, ClientMsg[1]);

                        //    // Room.Rooms[1].Opponent = new Client(Stream, Bw, Br, Name);
                        //    Room.Rooms[RoomId].Opponent.Name = Name;
                        //    Room.Rooms[RoomId].NumOfPlayers++;
                        //    client.Bw.Write("dimm");
                        //    break;
                        case "WATCH":
                            int RoomId2 = int.Parse(ClientMsg[1]);
                            sendRoomInfo(client, ClientMsg[1]);
                            client.Bw.Write("dimm");
                            client.Bw.Write($"CATCH,{Room.Rooms[RoomId2].CurrentWord},{Room.Rooms[RoomId2].CurrentTurn}");
                            Room.Rooms[RoomId2].WatcherId++;
                            Room.Rooms[RoomId2].Watchers.Add(Room.Rooms[RoomId2].WatcherId, client);
                            break;
                        case "4":  
                            string clickedLetter = ClientMsg[1];
                            int roomid = int.Parse(ClientMsg[2]);
                            // Send a message to the other client to dim the corresponding button
                            // Opponent.Bw.Write($"5,{clickedLetter}");
                            Room.Rooms[roomid].SendMessageToBothClients($"5,{clickedLetter}");
                            Room.Rooms[roomid].Owner.Bw.Write($"oppname,{Room.Rooms[roomid].Opponent.Name}");

                            if (Room.Rooms.ContainsKey(roomid))
                            {
                                //  Room room = Room[roomid];
                                string randomWord = Room.Rooms[roomid].Word;

                                if (randomWord.Contains(clickedLetter))
                                {

                                    // Broadcast to both players that the letter is correct
                                    Room.Rooms[roomid].SendMessageToBothClients($"6,{clickedLetter}");
                                    foreach(Client watcher in Room.Rooms[roomid].Watchers.Values)
                                    {
                                        watcher.Bw.Write($"WATCH1,{clickedLetter}");
                                    }
                                    //string now = Br.ReadString(); 
                                    //int roomid1 = int.Parse(ClientMsg[2]);
                                    //Room.Rooms[roomid1].CurrentWord = ClientMsg[1];
                                    //if (Room.Rooms[roomid].IsWordFullyGuessed())
                                    //{
                                    //    // Send a message to both players indicating that someone has won
                                    //    Room.Rooms[roomid].SendMessageToBothClients("END,You Win!");
                                    //}
                                }
                                else
                                {
                                    // The clicked letter is not in the word, hide the button for the player who clicked it
                                    // client.Bw.Write($"7,{clickedLetter}");
                                    Room.Rooms[roomid].SendMessageToBothClients($"7,{clickedLetter}");

                                    foreach (Client watcher in Room.Rooms[roomid].Watchers.Values)
                                    {
                                        watcher.Bw.Write($"WATCH2,{clickedLetter}");
                                    }
                                }
                            }
                             break;
                        //case "state":
                        //    int roomid1 = int.Parse(ClientMsg[2]);
                        //    Room.Rooms[roomid1].CurrentWord = ClientMsg[1];
                        //    break;
                        case "updateWord":
                            int roomIdToUpdate = int.Parse(ClientMsg[2]);
                            string updatedWord = ClientMsg[1];
                            string updateturn = ClientMsg[3];
                            Room.Rooms[roomIdToUpdate].CurrentWord = updatedWord;
                            Room.Rooms[roomIdToUpdate].CurrentTurn = updateturn;
                            break;
                        case "ENDGAME":
                            string winner;
                            string losser;
                            int roomid1 = int.Parse(ClientMsg[1]);
                            string p1 = Room.Rooms[roomid1].Owner.Name;
                            string p2 = Room.Rooms[roomid1].Opponent.Name;
                            if (Room.Rooms[roomid1].Owner.Name == client.Name)
                            {
                                winner = Room.Rooms[roomid1].Owner.Name;
                                losser = Room.Rooms[roomid1].Opponent.Name;
                                Room.Rooms[roomid1].Owner.Bw.Write($"WIN,{winner}");
                                Room.Rooms[roomid1].Opponent.Bw.Write($"LOSE,{losser}");
                            }
                            else
                            {
                                winner = Room.Rooms[roomid1].Opponent.Name;
                                losser = Room.Rooms[roomid1].Owner.Name;
                                Room.Rooms[roomid1].Opponent.Bw.Write($"WIN,{winner}");
                                Room.Rooms[roomid1].Owner.Bw.Write($"LOSE,{losser}");
                            }
                            writeScore(roomid1,p1,p2,winner);
                            break;
                        case "REMOVE":
                            Room.Rooms.Remove(int.Parse(ClientMsg[1]));
                            break;
                        case "CONFIRM":
                            int roomid2 = int.Parse(ClientMsg[1]);
                            Room.Rooms[roomid2].Opponent.Bw.Write($"OK,{Room.Rooms[roomid2].Owner.Name},{Room.Rooms[roomid2].Word},{Room.Rooms[roomid2].Category}");
                            Room.Rooms[roomid2].Opponent.Bw.Write("dimm");
                            break;
                        case "REJECT":
                            int roomid3 = int.Parse(ClientMsg[1]);
                            Room.Rooms[roomid3].Opponent.Bw.Write("NO");
                            Room.Rooms[roomid3].Opponent = null;
                            Room.Rooms[roomid3].NumOfPlayers = 1;
                            break;
                    }
                }
            }
        }


        void SendRoomsList(Client client)
        {
            string Rooms = "";
            foreach (Room item in Room.Rooms.Values)
            {
                Rooms += $"{item.Id},{item.Owner.Name},{(item.Opponent != null ? item.Opponent.Name : "")},{item.NumOfPlayers},{item.Category},{item.Word};";
            } 
            client.Bw.Write(Rooms);
        }

        void CreateRoom(Client client, string category)
        {
            Room room = new Room(client, category);
       //     room.Opponent.Name = null;
            client.Bw.Write($"{room.Id},{room.Owner.Name},{room.Word},{room.Category}");
        }

        public static void sendRoomInfo(Client client, string sendedid)
        {
            int id = int.Parse(sendedid);
            foreach (var room in Room.Rooms.Values)
            {
                if (room.Id == id)
                {
                    client.Bw.Write($"{room.Owner.Name},{room.Word},{room.Category},{room.Opponent.Name}"); 
                }
            }
        }

        public static void writeScore(int roomId,string p1,string p2, string winnerName)
        {
            string filePath = "D:\\servertest+play22\\servertest\\servertest\\score.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                   
                    writer.WriteLine($"Room ID: {roomId}, {p1} VS {p2} , Winner: {winnerName}");
                }

                
                Debug.WriteLine("Game result saved to score.txt");
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                Console.WriteLine($"An error occurred while writing to {filePath}: {ex.Message}");
            }
        }
    }

}
