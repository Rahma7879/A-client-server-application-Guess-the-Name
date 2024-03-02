namespace clientTest2
{
    partial class Game
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.PowderBlue;
            label1.Font = new Font("Gabriola", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkMagenta;
            label1.Location = new Point(377, 24);
            label1.Name = "label1";
            label1.Size = new Size(67, 45);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.PowderBlue;
            label2.Font = new Font("Gabriola", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.DarkMagenta;
            label2.Location = new Point(524, 23);
            label2.Name = "label2";
            label2.Size = new Size(104, 45);
            label2.TabIndex = 2;
            label2.Text = "TURN OF:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Gabriola", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(634, 27);
            label3.Name = "label3";
            label3.Size = new Size(0, 39);
            label3.TabIndex = 3;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Screenshot_2024_02_18_195439;
            ClientSize = new Size(800, 463);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Game";
            Text = "game";
            Load += game_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
    }
}