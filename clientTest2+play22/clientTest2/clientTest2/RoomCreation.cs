using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientTest2
{
    public partial class RoomCreation : Form
    {
        NetworkStream stream;
        BinaryReader Br;
        BinaryWriter Bw;
        string[] IdWord;
        string name;
        bool flag;
        Thread goroom;

        // Thread gogame;

        public static int RoomID
        { set; get; }
        public static String RoomOwnerName
        { set; get; }
        public static string RWord { set; get; }
        public static String Cata { set; get; }
        public RoomCreation(NetworkStream streamClient, string nameClient)
        {
            InitializeComponent();
            stream = streamClient;
            Bw = new BinaryWriter(stream);
            Br = new BinaryReader(stream);
            name = nameClient;
        }
        private void CreateRoom_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;


        }

        private void btnRoomCreation_Click(object sender, EventArgs e)
        {
            try
            {
                //request to create room
                Bw.Write("2," + comboBox2.SelectedItem + "," + name);
                flag = true;

                while (flag)
                {
                    if (stream.DataAvailable)
                    {
                        IdWord = Br.ReadString().Split(',');
                        RoomID = int.Parse(IdWord[0]);
                        RoomOwnerName = IdWord[1];
                        RWord = IdWord[2];
                        Cata = IdWord[3];
                        flag = false;
                    }
                }


                goroom = new Thread(Goroom);
                Close();
                goroom.Start();
            }
            catch
            {

            }

        }
        void Goroom()
        {
            Application.Run(new Game(stream, name, RoomID, RWord, Cata, ""));
        }


    }
}
