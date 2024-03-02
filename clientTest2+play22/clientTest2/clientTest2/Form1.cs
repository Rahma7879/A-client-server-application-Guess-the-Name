using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace clientTest2
{
    public partial class Form1 : Form
    {
        string Name;
        TcpClient client;
        NetworkStream stream;
        Thread receiveThread;
        Thread GoWelcome;
        string randomWord = "ABCD";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            client = new TcpClient("127.0.0.1", 9001);
            Name = userName.Text;
            stream = client.GetStream();
            Name = userName.Text;
            new BinaryWriter(stream).Write(Name);
            GoWelcome = new Thread(openWelcome);
            Close();
            GoWelcome.Start();
        }

        void openWelcome()
        {
            Application.Run(new welcomePage(stream, Name));
        }
    }
}