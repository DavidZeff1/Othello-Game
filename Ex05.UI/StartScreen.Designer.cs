namespace Ex05.UI
{
    partial class StartScreen
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
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.PlayAgainstComputerButton = new System.Windows.Forms.Button();
            this.PlayAgainstFriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Location = new System.Drawing.Point(69, 57);
            this.BoardSizeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(415, 50);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "Board Size 6 x 6";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            this.BoardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            // 
            // PlayAgainstComputerButton
            // 
            this.PlayAgainstComputerButton.Location = new System.Drawing.Point(69, 130);
            this.PlayAgainstComputerButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayAgainstComputerButton.Name = "PlayAgainstComputerButton";
            this.PlayAgainstComputerButton.Size = new System.Drawing.Size(187, 46);
            this.PlayAgainstComputerButton.TabIndex = 2;
            this.PlayAgainstComputerButton.Text = "Play Against Computer";
            this.PlayAgainstComputerButton.UseVisualStyleBackColor = true;
            this.PlayAgainstComputerButton.Click += new System.EventHandler(this.boardPlayVsComputerButton_Click);
            // 
            // PlayAgainstFriendButton
            // 
            this.PlayAgainstFriendButton.Location = new System.Drawing.Point(283, 130);
            this.PlayAgainstFriendButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayAgainstFriendButton.Name = "PlayAgainstFriendButton";
            this.PlayAgainstFriendButton.Size = new System.Drawing.Size(201, 46);
            this.PlayAgainstFriendButton.TabIndex = 3;
            this.PlayAgainstFriendButton.Text = "Play Against Your Friend";
            this.PlayAgainstFriendButton.UseVisualStyleBackColor = true;
            this.PlayAgainstFriendButton.Click += new System.EventHandler(this.boardPlayVsFriendButton_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 271);
            this.Controls.Add(this.PlayAgainstFriendButton);
            this.Controls.Add(this.PlayAgainstComputerButton);
            this.Controls.Add(this.BoardSizeButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StartScreen";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button PlayAgainstComputerButton;
        private System.Windows.Forms.Button PlayAgainstFriendButton;
    }
}

