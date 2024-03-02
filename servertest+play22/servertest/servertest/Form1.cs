using System.Net.Sockets;
using System.Net;

namespace servertest
{
    public partial class Form1 : Form
    {
        static Thread ThreadServer;

        static TcpListener server;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), 9001);
            server.Start();
            ThreadServer = new Thread(ListenNewClient);
            ThreadServer.Start();
        }


        static void ListenNewClient()
        {
            while (true)
            {
                Socket ClientSocket = server.AcceptSocket();
                NetworkStream Stream = new NetworkStream(ClientSocket);
                BinaryReader Br = new BinaryReader(Stream);
                BinaryWriter Bw = new BinaryWriter(Stream);
                string ClientName = Br.ReadString();
                Console.WriteLine(ClientName);
                Client Client = new Client(Stream, Bw, Br, ClientName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.Stop();
            ThreadServer.Join();
            MessageBox.Show("Server disconnected.");
        }
    }
}