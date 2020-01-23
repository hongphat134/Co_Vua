namespace CoVua
{
    partial class frmChessKing
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
            this.pnlBackGround = new System.Windows.Forms.Panel();
            this.pnllstCB1Remove = new System.Windows.Forms.Panel();
            this.pnllstCB2Remove = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlChessBoard = new System.Windows.Forms.Panel();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnEndGame = new System.Windows.Forms.Button();
            this.pnlBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBackGround
            // 
            this.pnlBackGround.Controls.Add(this.btnEndGame);
            this.pnlBackGround.Controls.Add(this.pnllstCB1Remove);
            this.pnlBackGround.Controls.Add(this.pnllstCB2Remove);
            this.pnlBackGround.Controls.Add(this.label2);
            this.pnlBackGround.Controls.Add(this.label1);
            this.pnlBackGround.Controls.Add(this.pnlChessBoard);
            this.pnlBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackGround.Location = new System.Drawing.Point(0, 0);
            this.pnlBackGround.Name = "pnlBackGround";
            this.pnlBackGround.Size = new System.Drawing.Size(984, 561);
            this.pnlBackGround.TabIndex = 0;
            this.pnlBackGround.Visible = false;
            // 
            // pnllstCB1Remove
            // 
            this.pnllstCB1Remove.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnllstCB1Remove.Location = new System.Drawing.Point(843, 70);
            this.pnllstCB1Remove.Name = "pnllstCB1Remove";
            this.pnllstCB1Remove.Size = new System.Drawing.Size(120, 320);
            this.pnllstCB1Remove.TabIndex = 9;
            // 
            // pnllstCB2Remove
            // 
            this.pnllstCB2Remove.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnllstCB2Remove.Location = new System.Drawing.Point(22, 70);
            this.pnllstCB2Remove.Name = "pnllstCB2Remove";
            this.pnllstCB2Remove.Size = new System.Drawing.Size(120, 320);
            this.pnllstCB2Remove.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(853, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Player 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(42, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Player 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlChessBoard
            // 
            this.pnlChessBoard.Location = new System.Drawing.Point(173, 43);
            this.pnlChessBoard.Name = "pnlChessBoard";
            this.pnlChessBoard.Size = new System.Drawing.Size(640, 480);
            this.pnlChessBoard.TabIndex = 5;
            // 
            // btnStartGame
            // 
            this.btnStartGame.AutoSize = true;
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.Location = new System.Drawing.Point(407, 184);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(159, 61);
            this.btnStartGame.TabIndex = 1;
            this.btnStartGame.Text = "BẮT ĐẦU";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnEndGame
            // 
            this.btnEndGame.AutoSize = true;
            this.btnEndGame.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndGame.Location = new System.Drawing.Point(829, 429);
            this.btnEndGame.Name = "btnEndGame";
            this.btnEndGame.Size = new System.Drawing.Size(144, 46);
            this.btnEndGame.TabIndex = 10;
            this.btnEndGame.Text = "KẾT THÚC";
            this.btnEndGame.UseVisualStyleBackColor = true;
            this.btnEndGame.Visible = false;
            this.btnEndGame.Click += new System.EventHandler(this.btnEndGame_Click);
            // 
            // frmChessKing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.pnlBackGround);
            this.Name = "frmChessKing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cờ Vua";
            this.pnlBackGround.ResumeLayout(false);
            this.pnlBackGround.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackGround;
        private System.Windows.Forms.Panel pnllstCB1Remove;
        private System.Windows.Forms.Panel pnllstCB2Remove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Button btnEndGame;
        private System.Windows.Forms.Button btnStartGame;


    }
}

