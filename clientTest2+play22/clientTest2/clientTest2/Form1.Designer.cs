namespace clientTest2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            userName = new TextBox();
            label1 = new Label();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnPlay = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // userName
            // 
            userName.Location = new Point(252, 372);
            userName.Name = "userName";
            userName.Size = new Size(252, 23);
            userName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Gabriola", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.ForeColor = Color.DarkMagenta;
            guna2HtmlLabel1.Location = new Point(120, 363);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(126, 41);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Enter your name";
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.Transparent;
            btnPlay.BorderRadius = 15;
            btnPlay.CustomizableEdges = customizableEdges1;
            btnPlay.DisabledState.BorderColor = Color.DarkGray;
            btnPlay.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPlay.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPlay.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPlay.FillColor = Color.DarkMagenta;
            btnPlay.Font = new Font("Georgia", 15.75F, FontStyle.Italic, GraphicsUnit.Point);
            btnPlay.ForeColor = Color.White;
            btnPlay.Location = new Point(335, 401);
            btnPlay.Name = "btnPlay";
            btnPlay.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPlay.Size = new Size(94, 45);
            btnPlay.TabIndex = 5;
            btnPlay.Text = "Play";
            btnPlay.Click += btnPlay_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(734, 447);
            Controls.Add(btnPlay);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(label1);
            Controls.Add(userName);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox userName;
        private Label label1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnPlay;
    }
}