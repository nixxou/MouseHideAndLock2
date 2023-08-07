namespace MouseHideAndLock2
{
	partial class Form_Config
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
			this.buttonSave = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Emulator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HideCursor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ShowCursoronMove = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LockCursor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(585, 654);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(123, 70);
			this.buttonSave.TabIndex = 9;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emulator,
            this.HideCursor,
            this.ShowCursoronMove,
            this.LockCursor});
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(696, 621);
			this.dataGridView1.TabIndex = 8;
			// 
			// Emulator
			// 
			this.Emulator.Frozen = true;
			this.Emulator.HeaderText = "Emulator";
			this.Emulator.Name = "Emulator";
			this.Emulator.ReadOnly = true;
			this.Emulator.Width = 500;
			// 
			// HideCursor
			// 
			this.HideCursor.HeaderText = "Hide Cursor";
			this.HideCursor.Name = "HideCursor";
			this.HideCursor.Width = 50;
			// 
			// ShowCursoronMove
			// 
			this.ShowCursoronMove.HeaderText = "Show Cursor on Move";
			this.ShowCursoronMove.Name = "ShowCursoronMove";
			this.ShowCursoronMove.Width = 50;
			// 
			// LockCursor
			// 
			this.LockCursor.HeaderText = "Lock Cursor";
			this.LockCursor.Name = "LockCursor";
			this.LockCursor.Width = 50;
			// 
			// Form_Config
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(731, 743);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form_Config";
			this.Text = "Form_Config";
			this.Load += new System.EventHandler(this.Form_Config_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Emulator;
		private System.Windows.Forms.DataGridViewTextBoxColumn HideCursor;
		private System.Windows.Forms.DataGridViewTextBoxColumn ShowCursoronMove;
		private System.Windows.Forms.DataGridViewTextBoxColumn LockCursor;
	}
}