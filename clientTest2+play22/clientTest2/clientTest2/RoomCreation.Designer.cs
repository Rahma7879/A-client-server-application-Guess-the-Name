namespace clientTest2
{
    partial class RoomCreation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            comboBox2 = new ComboBox();
            label1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnRoomCreation = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Players", "Cars", "Colors" });
            comboBox2.Location = new Point(315, 240);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(163, 23);
            comboBox2.TabIndex = 3;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Gabriola", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Purple;
            label1.Location = new Point(339, 187);
            label1.Name = "label1";
            label1.Size = new Size(116, 47);
            label1.TabIndex = 4;
            label1.Text = "CATEGORIES";
            // 
            // btnRoomCreation
            // 
            btnRoomCreation.BackColor = Color.Transparent;
            btnRoomCreation.BorderRadius = 20;
            btnRoomCreation.CustomizableEdges = customizableEdges1;
            btnRoomCreation.DisabledState.BorderColor = Color.DarkGray;
            btnRoomCreation.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRoomCreation.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRoomCreation.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRoomCreation.FillColor = Color.DarkMagenta;
            btnRoomCreation.Font = new Font("Gabriola", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRoomCreation.ForeColor = Color.White;
            btnRoomCreation.Location = new Point(343, 354);
            btnRoomCreation.Name = "btnRoomCreation";
            btnRoomCreation.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRoomCreation.Size = new Size(112, 37);
            btnRoomCreation.TabIndex = 5;
            btnRoomCreation.Text = "OK";
            btnRoomCreation.Click += btnRoomCreation_Click;
            // 
            // RoomCreation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.category1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRoomCreation);
            Controls.Add(label1);
            Controls.Add(comboBox2);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "RoomCreation";
            Text = "RoomCreation";
            Load += CreateRoom_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBox2;
        private Guna.UI2.WinForms.Guna2HtmlLabel label1;
        private Guna.UI2.WinForms.Guna2Button btnRoomCreation;
    }
}