using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace clientTest2
{

    public partial class welcomePage : Form
    {
        string[] Info;
        bool flag;
        Thread GoCreateRoom;
        Thread GoWelcome1;
        NetworkStream stream;
        BinaryReader Br;
        BinaryWriter Bw;
        string name;
        string[] rooms;
        Thread goroom;
        string[] RoomInfo;
        int SelectedRoomId;
        public string oopname { get; set; }
        public string ownername { get; set; }
        public string rword { get; set; }
        public string cata { get; set; }

        public welcomePage(NetworkStream streamClient, string nameClient)
        {
            InitializeComponent();
            stream = streamClient;
            Bw = new BinaryWriter(streamClient);
            Br = new BinaryReader(streamClient);
            name = nameClient;
            getRoomsinfo();
        }

        private void getRooms_Click(object sender, EventArgs e)
        {
            getRoomsinfo();

        }
        private void getRoomsinfo()
        {
            try
            {
                Bw.Write("1"); // Request to get rooms from server
                listBox1.Items.Clear();


                string roomsData = Br.ReadString();


                string[] rooms = roomsData.Split(';');


                foreach (string roomData in rooms)
                {

                    RoomInfo = roomData.Split(',');
                    listBox1.Items.Add("Number of players: " + RoomInfo[3] + " players: " + RoomInfo[1] + " VS " + RoomInfo[2] + " category: " + RoomInfo[4] + " RoomID: " + RoomInfo[0]); 


                }
            }
            catch
            {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (listBox1.SelectedItem != null)
                {

                    string selectedItem = listBox1.SelectedItem.ToString();


                    string[] currroomInfo = selectedItem.Split(' ');

                    string numPlayersStr = currroomInfo[3].Trim();

                    if (numPlayersStr == "2")
                    {

                        btnJoin.Enabled = false;

                    }
                    else
                    {

                        btnJoin.Enabled = true;
                    }
                }

            }
            catch { }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            GoCreateRoom = new Thread(openCreateRoom);

            Close();
            GoCreateRoom.Start();

        }
        void openCreateRoom()
        {
            Application.Run(new RoomCreation(stream, name));
        }

        private void welcomePage_Load(object sender, EventArgs e)
        {

        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string[] currroomInfo = selectedItem.Split(' ');

            string roomid = currroomInfo[11].Trim();

            SelectedRoomId = int.Parse(roomid);
            Bw.Write("3," + SelectedRoomId);
            flag = true;

            while (flag)
            {
                if (stream.DataAvailable)
                {
                    string[] state;
                    state=Br.ReadString().Split(',');
                    switch (state[0])
                    {
                        case "OK":

                           // Info = Br.ReadString().Split(',');
                            //  RoomID = int.Parse(Info[0]);
                            ownername = state[1];
                            rword = state[2];
                            cata = state[3];
                            flag = false;
                            goroom = new Thread(Goroom);
                            Close();
                            goroom.Start();
                            break;
                        case "NO":
                            GoWelcome1 = new Thread(openWelcome1);
                            GoWelcome1.Start();
                            Close();
                            
                            break;
                    }

                }
            }
            

        }
        void openWelcome1()
        {
            Application.Run(new welcomePage(stream, Name));
        }
        void Goroom()
        {
            Application.Run(new Game(stream, ownername, SelectedRoomId, rword, cata, name));
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string[] currroomInfo = selectedItem.Split(' ');

            string roomid = currroomInfo[11].Trim();

            SelectedRoomId = int.Parse(roomid);
            Bw.Write("WATCH," + SelectedRoomId);

            flag = true;

            while (flag)
            {
                if (stream.DataAvailable)
                {
                    Info = Br.ReadString().Split(',');
                    //  RoomID = int.Parse(Info[0]);
                    ownername = Info[0];
                    rword = Info[1];
                    cata = Info[2];
                    name = Info[3];
                    flag = false;
                }
            }
            goroom = new Thread(Goroom);
            Close();
            goroom.Start();
        }

        //Game(NetworkStream streamClient, string ownerClient, int roomid, string rword,string Catagory,string oppClient)
        // client.Bw.Write($"{room.Id},{room.Owner.Name},{room.Word},{room.Category}");
    }

}
