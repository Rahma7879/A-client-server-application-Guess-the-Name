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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace clientTest2
{
    public partial class Game : Form
    {
        private System.Windows.Forms.Button[] alphabetButtons;

        private System.Windows.Forms.Button btn;
        NetworkStream stream;
        BinaryReader Br;
        BinaryWriter Bw;
        string[] RoomInfo;
        string name;
        bool flag;
        int roomId;
        private bool isPlayer1Turn = true;
        public string player1 { get; set; }
        public string player2 { get; set; }
        public string SelectedWord { get; set; }
        public string cata { get; set; }
        //created
        //NetworkStream StreamCons, string RoomIdCons, string WordCons, string nameCons, string categoryCons, string levelCons, string OpponentNameCons
        public Game(NetworkStream streamClient, string ownerClient, int roomid, string rword, string Catagory, string oppClient)
        {
            InitializeComponent();
            stream = streamClient;
            Bw = new BinaryWriter(streamClient);
            Br = new BinaryReader(streamClient);
            name = ownerClient;
            player1 = name;
            roomId = roomid;
            SelectedWord = rword;
            cata = Catagory;
            player2 = oppClient;
            //requestRoomInfo(roomId);<=
            // getRoomInfo();
            InitializeAlphabetButtons();
            label3.Text = player1;

        }
        //private void InitializeAlphabetButtons()
        //{
        //    alphabetButtons = new System.Windows.Forms.Button[26];

        //    int buttonWidth = 40;
        //    int buttonHeight = 40;
        //    int spacing = 5;
        //    int xOffset = 10;
        //    int yOffset = 100;
        //    int buttonsPerRow = 9; // Adjust the number of buttons per row here
        //    int currentRow = 0;

        //    for (int i = 0; i < 26; i++)
        //    {
        //        char letter = (char)('A' + i);
        //        if (i > 0 && i % buttonsPerRow == 0) // Move to the next row
        //        {
        //            currentRow++;
        //            xOffset = 10;
        //            yOffset = 100 + currentRow * (buttonHeight + spacing);
        //        }
        //        alphabetButtons[i] = new System.Windows.Forms.Button();
        //        alphabetButtons[i].Text = letter.ToString();
        //        alphabetButtons[i].Width = buttonWidth;
        //        alphabetButtons[i].Height = buttonHeight;
        //        alphabetButtons[i].Top = yOffset;
        //        //if (letter >= (char)('R'))
        //        //    alphabetButtons[i].Left = xOffset + (buttonWidth + spacing) * (i - 20);
        //        //else
        //        //    alphabetButtons[i].Left = xOffset + (buttonWidth + spacing) * (i);
        //        xOffset += buttonWidth + spacing;
        //        alphabetButtons[i].Click += AlphabetButton_Click;

        //        this.Controls.Add(alphabetButtons[i]);

        //    }
        //}

        private void InitializeAlphabetButtons()
        {
            alphabetButtons = new System.Windows.Forms.Button[26];

            int buttonWidth = 40;
            int buttonHeight = 40;
            int spacing = 5;
            //int xOffset = 10;
            int yOffset = 100;

            int buttonsPerRow = 9; 
            int currentButton = 0;

            int totalButtonWidth = buttonsPerRow * buttonWidth + (buttonsPerRow - 1) * spacing;

            // Calculate the initial x offset to center the buttons horizontally
            int xOffset = (this.ClientSize.Width - totalButtonWidth) / 2;

            for (int row = 0; row < 3; row++) 
            {
                for (int col = 0; col < buttonsPerRow; col++) 
                {
                    if (currentButton >= 26)
                        break;

                    char letter = (char)('A' + currentButton);
                    alphabetButtons[currentButton] = new System.Windows.Forms.Button();
                    alphabetButtons[currentButton].Text = letter.ToString();
                    alphabetButtons[currentButton].Width = buttonWidth;
                    alphabetButtons[currentButton].Height = buttonHeight;
                    alphabetButtons[currentButton].Top = yOffset + row * (buttonHeight + spacing);
                    alphabetButtons[currentButton].Left = xOffset + col * (buttonWidth + spacing);

                    alphabetButtons[currentButton].Click += AlphabetButton_Click;
                    this.Controls.Add(alphabetButtons[currentButton]);

                    currentButton++;
                }
            }
        }
        private void AlphabetButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            string letter = clickedButton.Text;

            // Send a message to the server 
            Bw.Write($"4,{letter},{roomId}");
        }
        private void UpdateDashLines(char guessedChar)
        {
            for (int i = 0; i < SelectedWord.Length; i++)
            {
                if (SelectedWord[i] == guessedChar)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        label1.Text = label1.Text.Remove(i * 2, 1).Insert(i * 2, guessedChar.ToString());
                    });
                }


                 //Bw.Write($"updateWord,{label1.Text},{roomId}");//
            }

             Bw.Write($"updateWord,{label1.Text},{roomId},{label3.Text}"); //HERE DON"T SEND
            foreach (System.Windows.Forms.Button button in alphabetButtons)
            {
                // If the button is currently hidden, show it
                if (button.Visible)
                {
                    if (label1.Text.Replace(" ", "") == SelectedWord)
                    {
                        Bw.Write($"ENDGAME,{roomId}");
                    }
                }
                break;
            }
        }

        private void UpdateDashLines2(char guessedChar)
        {
            for (int i = 0; i < SelectedWord.Length; i++)
            {
                if (SelectedWord[i] == guessedChar)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        label1.Text = label1.Text.Remove(i * 2, 1).Insert(i * 2, guessedChar.ToString());
                    });
                }


                // Bw.Write($"updateWord,{label1.Text},{roomId}");//
            }
        }




        private void HideButtons(bool x)
        {
            foreach (System.Windows.Forms.Button button in alphabetButtons)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    button.Visible = false;
                });
            }
        }
        private void ShowButtons(bool x)
        {
            foreach (var button in alphabetButtons)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    button.Visible = true;
                });
            }
        }
        private void SwitchTurns()
        {
            isPlayer1Turn = !isPlayer1Turn;
            if (isPlayer1Turn)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label3.Text = player1;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label3.Text = player2;
                });
            }
        
            foreach (System.Windows.Forms.Button button in alphabetButtons)
            {
                // If the button is currently hidden, show it
                if (!button.Visible)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        button.Visible = true;
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        button.Visible = false;
                    });
                }
            }


            //isPlayer1Turn = !isPlayer1Turn; // Switch the turn
            //if (isPlayer1Turn)
            //{
            //    // Enable buttons for player 1 and disable buttons for player 2
            //    ShowButtons(player1 == name);
            //    HideButtons(player1 != name);
            //}
            //else
            //{
            //    // Enable buttons for player 2 and disable buttons for player 1
            //    ShowButtons(player2 == name);
            //    HideButtons(player2 != name);
            //}
            //foreach (System.Windows.Forms.Button button in alphabetButtons)
            //{
            //    if(button.Visible == false)
            //    {
            //        button.Visible = true;
            //    }
            //    else
            //    {
            //        button.Visible = false;
            //    }

            //    if (button.Visible == true)
            //    {
            //        button.Visible = false;
            //    }
            //    else
            //    {
            //        button.Visible = true;
            //    }

            //}
        }

        private void SwitchTurns2()
        {
           // isPlayer1Turn = !isPlayer1Turn;
            if (label3.Text==player2)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label3.Text = player1;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label3.Text = player2;
                });
            }
        }

        private void HandleServerMessages()
        {
            while (true)
            {
                if (stream.DataAvailable)
                {
                    string message = Br.ReadString();
                    // Handle the message received from the server
                    string[] parts = message.Split(',');
                    switch (parts[0])
                    {
                        case "dimm":
                            HideButtons(true);
                            break;
                        case "5":
                            string letterToDim = parts[1];

                            // Dim the button corresponding to the received letter
                            foreach (System.Windows.Forms.Button button in alphabetButtons)
                            {
                                if (button.Text == letterToDim)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        button.Enabled = false;
                                    });
                                    break;
                                }
                            }
                            break;
                        case "6":
                            char guessedChar = parts[1][0];
                            UpdateDashLines(guessedChar);
                            break;
                        case "7":
                            SwitchTurns();
                            break;
                        case "oppname":
                            player2 = parts[1];
                            break;
                        case "WIN":
                            
                            DialogResult status = DialogResult.None;
                          
                            status = MessageBox.Show("Winner!!\n Do you want to play again ?!", "Congratulation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                            //        status = MessageBox.Show("Good luck next time !!\n Do you want to play again ?!", "Try again", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            switch (status)
                            {
                                case DialogResult.Yes:
                                    Bw.Write($"2,{cata}");
                                    break;
                                case DialogResult.No:

                                    Bw.Write($"REMOVE,{roomId}");
                                    Thread goWelcome = new Thread(() => Application.Run(new welcomePage(stream, parts[1])));
                                    goWelcome.SetApartmentState(ApartmentState.STA);
                                    this.Invoke(new Action(() => Close()));
                                    goWelcome.Start();
                                //    this.Invoke(new Action(() => Close()));
                                   
                                    break;
                            }
                            
                            //MessageBox.Show("Congratulations! You Win!");
                            break;
                        case "LOSE":
                            DialogResult status2 = DialogResult.None;
                            status2 = MessageBox.Show("You Lost . Hard Luck !!\n Do you want to play again ?!", "Try again", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            switch (status2)
                            {
                                case DialogResult.Yes:
                                    Bw.Write($"2,{cata}");
                                    break;
                                case DialogResult.No:
                                    // Bw.Write("3");

                                    Thread goWelcome = new Thread(() => Application.Run(new welcomePage(stream, parts[1])));
                                    goWelcome.SetApartmentState(ApartmentState.STA);
                                    this.Invoke(new Action(() => Close()));
                                    goWelcome.Start();
                                  //  this.Invoke(new Action(() => Close()));
                                    
                                    break;
                            }


                            //  MessageBox.Show("Hard luck!");
                            break;
                        case "WATCH1":
                            char guessedChar2 = parts[1][0];
                            UpdateDashLines2(guessedChar2);
                            break;
                        case "WATCH2":
                                SwitchTurns2();
                            break;
                        case "CATCH":
                            string currentw = parts[1];
                            string currentt = parts[2];
                            catchAlpha(currentw,currentt);
                            break;
                        case "R":
                            string Msg = parts[1];
                            
                            DialogResult Result = MessageBox.Show(Msg, "Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            switch (Result)
                            {
                                case DialogResult.Yes:
                                    //play
                                    Bw.Write($"CONFIRM,{roomId},{parts[2]}");
                                    break;
                                case DialogResult.No:
                                    Bw.Write($"REJECT,{roomId},{parts[2]}");
                                    break;
                            }
                            break;
                    }

                }
            }

        }
        //join
        //public Game(NetworkStream streamClient, string nameClient)//,int room_Id)
        //{

        //    InitializeComponent();
        //    stream = streamClient;
        //    Bw = new BinaryWriter(streamClient);
        //    Br = new BinaryReader(streamClient);
        //    player2 = nameClient;
        //    //  roomId=room_Id;
        //    requestRoomInfo(roomId);
        //    getRoomInfo();

        //}

        //function request data from server 
        void requestRoomInfo(int id)
        {
            Bw.Write("3," + roomId);
            //flag = true;
            //while (flag)
            //{
            //    if (stream.DataAvailable)
            //    {

            //        flag = false;
            //    }
            //}


        }

        //function get room data from server
        void getRoomInfo()
        {

            string roomData = Br.ReadString();


            RoomInfo = roomData.Split(',');
            player1 = RoomInfo[0];
        }


        private void game_Load(object sender, EventArgs e)
        {
            InitializeDashLines();
            // label1.Text = player1;
            //if (player1 == name)
            //{

            //    testbrn.Enabled = true;
            //}
            //else
            //{
            //    testbrn.Enabled = false;
            //}
            Task.Run(() => HandleServerMessages());
        }

        private void InitializeDashLines()
        {
            label1.Text = null;
            // Display dash lines representing each character in the selected word
            for (int i = 0; i < SelectedWord.Length; i++)
            {
                label1.Text += "_ ";
            }
        }

        private void catchAlpha(string currentword,string currentturn)
        {
            this.Invoke((MethodInvoker)delegate
            {
                label1.Text = currentword;
                label3.Text = currentturn;
            });
        }
    }
}
