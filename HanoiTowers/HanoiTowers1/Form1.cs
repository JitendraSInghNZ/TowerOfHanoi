using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HanoiTowers1
{
	/// <summary>
	/// This form lets the user play a game of HanoiTowers.
	/// 4 labels representing disks are shown on the first of three poles. It is possible 
	/// to drag a disk from one pole to another. The rules for a valid move are that
	/// a bigger disk cannot be dropped on top of a smaller one. The aim of the game
	/// is to move the stack of disks to another pole one disk at a time.
	/// Moves made by Dragging are recorder as lines of text in a textBox
	/// It is possible to reset the disks to their original position
	/// It is also possible to replay the moves stored in the textbox
	/// either by stepping through them - the [Step] button
	/// or from a timer - started by the [Animate] button
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Disk[,] disks = new Disk[3,4];
		//array of possible positions of disks over the 3 poles and four levels
		//the array keeps track of where the labels representing the disks are
		//the Disk class stores the pole and level of a label representing the disk
		//as well as an object refrence to the label

		private int targetPole = 0;
		//used to communicate between DragDrop which identifies the pole being dropped on
		//and the MouseDown method for the "disks" which will move a "disk" to a new
		//pole after DragDrop is completed

		private int animateLine = 0;
		//used to say which line in a list of moves is the current move

		private bool isStepping = false;
		//if moves are being made from a list, not by drag and drop
		//isStepping is used to prevent recording the moves made

		private int MoveCount = 0; //count of moves made in a game

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label Disk4;
		private System.Windows.Forms.Label Disk3;
		private System.Windows.Forms.Label Disk2;
		private System.Windows.Forms.Label Pole2;
		private System.Windows.Forms.Label Pole1;
		private System.Windows.Forms.Label Pole3;
		private System.Windows.Forms.Label Disk1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Timers.Timer timer1;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnAnimate;
		private System.Windows.Forms.Button btnStep;
		private System.Windows.Forms.Label lblMoves;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.Disk4 = new System.Windows.Forms.Label();
            this.Disk3 = new System.Windows.Forms.Label();
            this.Disk2 = new System.Windows.Forms.Label();
            this.Disk1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.Pole2 = new System.Windows.Forms.Label();
            this.Pole1 = new System.Windows.Forms.Label();
            this.Pole3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.timer1 = new System.Timers.Timer();
            this.btnStep = new System.Windows.Forms.Button();
            this.lblMoves = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Location = new System.Drawing.Point(120, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 48);
            this.panel1.TabIndex = 0;
            // 
            // Disk4
            // 
            this.Disk4.BackColor = System.Drawing.Color.Lime;
            this.Disk4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk4.Location = new System.Drawing.Point(157, 216);
            this.Disk4.Name = "Disk4";
            this.Disk4.Size = new System.Drawing.Size(144, 24);
            this.Disk4.TabIndex = 5;
            this.Disk4.Click += new System.EventHandler(this.Disk1_Click);
            this.Disk4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk1_MouseDown);
            // 
            // Disk3
            // 
            this.Disk3.BackColor = System.Drawing.Color.Lime;
            this.Disk3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk3.Location = new System.Drawing.Point(173, 192);
            this.Disk3.Name = "Disk3";
            this.Disk3.Size = new System.Drawing.Size(112, 24);
            this.Disk3.TabIndex = 6;
            this.Disk3.Click += new System.EventHandler(this.Disk1_Click);
            this.Disk3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk1_MouseDown);
            // 
            // Disk2
            // 
            this.Disk2.BackColor = System.Drawing.Color.Lime;
            this.Disk2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk2.Location = new System.Drawing.Point(189, 168);
            this.Disk2.Name = "Disk2";
            this.Disk2.Size = new System.Drawing.Size(80, 24);
            this.Disk2.TabIndex = 7;
            this.Disk2.Click += new System.EventHandler(this.Disk1_Click);
            this.Disk2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk1_MouseDown);
            // 
            // Disk1
            // 
            this.Disk1.BackColor = System.Drawing.Color.Lime;
            this.Disk1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk1.Location = new System.Drawing.Point(205, 144);
            this.Disk1.Name = "Disk1";
            this.Disk1.Size = new System.Drawing.Size(48, 24);
            this.Disk1.TabIndex = 8;
            this.Disk1.Click += new System.EventHandler(this.Disk1_Click);
            this.Disk1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk1_MouseDown);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(16, 16);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(104, 32);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Pole2
            // 
            this.Pole2.AllowDrop = true;
            this.Pole2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole2.Location = new System.Drawing.Point(400, 112);
            this.Pole2.Name = "Pole2";
            this.Pole2.Size = new System.Drawing.Size(24, 144);
            this.Pole2.TabIndex = 10;
            this.Pole2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole1_DragDrop);
            this.Pole2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole2_DragEnter);
            // 
            // Pole1
            // 
            this.Pole1.AllowDrop = true;
            this.Pole1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole1.Location = new System.Drawing.Point(216, 112);
            this.Pole1.Name = "Pole1";
            this.Pole1.Size = new System.Drawing.Size(24, 144);
            this.Pole1.TabIndex = 11;
            this.Pole1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole1_DragDrop);
            this.Pole1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole2_DragEnter);
            // 
            // Pole3
            // 
            this.Pole3.AllowDrop = true;
            this.Pole3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole3.Location = new System.Drawing.Point(576, 112);
            this.Pole3.Name = "Pole3";
            this.Pole3.Size = new System.Drawing.Size(24, 144);
            this.Pole3.TabIndex = 13;
            this.Pole3.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole1_DragDrop);
            this.Pole3.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole2_DragEnter);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(280, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 40);
            this.label1.TabIndex = 14;
            this.label1.Text = "Moves:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(728, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 280);
            this.textBox1.TabIndex = 15;
            this.textBox1.Visible = false;
            // 
            // btnAnimate
            // 
            this.btnAnimate.Location = new System.Drawing.Point(16, 112);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(104, 32);
            this.btnAnimate.TabIndex = 16;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.Visible = false;
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(16, 64);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(104, 32);
            this.btnStep.TabIndex = 17;
            this.btnStep.Text = "Step";
            this.btnStep.Visible = false;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // lblMoves
            // 
            this.lblMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblMoves.Location = new System.Drawing.Point(368, 16);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(48, 24);
            this.lblMoves.TabIndex = 18;
            this.lblMoves.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(888, 365);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnAnimate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.Disk1);
            this.Controls.Add(this.Disk2);
            this.Controls.Add(this.Disk3);
            this.Controls.Add(this.Disk4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Pole1);
            this.Controls.Add(this.Pole2);
            this.Controls.Add(this.Pole3);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = " Towers of  Hanoi";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		//put all four disks back in order on first pole
		{
			for (int ipole = 0; ipole < 3; ipole++)
			{
				for (int jlevel = 0; jlevel < 4; jlevel++)
				{
					disks[ipole,jlevel] = null;
				}
			}
			disks[0,3] = new Disk(Disk1,1,4);
			disks[0,2] = new Disk(Disk2,1,3);
			disks[0,1] = new Disk(Disk3,1,2);
			disks[0,0] = new Disk(Disk4,1,1);

			disks[0,0].Move(1,1);
			disks[0,1].Move(1,2);
			disks[0,2].Move(1,3);
			disks[0,3].Move(1,4);
			MoveCount = 0;
			lblMoves.Text = MoveCount.ToString();
			animateLine = 0;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		//create Disk objects matching the "Disk" labels on the first pole
		{
			disks[0,3] = new Disk(Disk1,1,4);
			disks[0,2] = new Disk(Disk2,1,3);
			disks[0,1] = new Disk(Disk3,1,2);
			disks[0,0] = new Disk(Disk4,1,1);
		}

		private bool DropOnPole(int oldPole, int oldLevel, int newPole)
		//Move a disk from its current position to the next available position
		//on the new pole and check that the move is valid
		{
			int newLevel;
			oldPole--;
			oldLevel--;
			newPole--;

			if (disks[newPole, 3] != null)
			{
				MessageBox.Show("Invalid Move - pole is full");
				return false;
			}
			else if (disks[oldPole, oldLevel] == null)
			{
				MessageBox.Show("Invalid Move - no disk at start point");
				return false;
			}

			if (oldLevel < 3)
			{
				if (disks[oldPole, oldLevel +1 ] != null)
				{
					MessageBox.Show("Invalid Move - can only move top disk");
					return false;
				}
			}

			newLevel = 3;
			for (int i = 0; i < 4; i++)
			{
				if (disks[newPole,i] == null)
				{
					newLevel = i;
					break;
				}
			}

			if (newLevel > 0) 
			{
				if ( disks[oldPole, oldLevel].thisDisk.Width > disks[newPole,newLevel - 1].thisDisk.Width)
				{
					MessageBox.Show("Invalid Move - cannot drop bigger disk on smaller");
					return false;
				}
			}
						   
			disks[newPole, newLevel] = disks[oldPole, oldLevel];
			disks[oldPole, oldLevel] = null;
			disks[newPole, newLevel].Move(newPole+1, newLevel+1);
			if (!isStepping)
			{
				string theMove = disks[newPole, newLevel].thisDisk.Name + ','
					+ (newPole +1).ToString() + ',' + (newLevel+1).ToString() +"\r\n";
				textBox1.AppendText(theMove);
				MoveCount++;
				lblMoves.Text = MoveCount.ToString();
			}
			return true;
		}

		private void Disk1_Click(object sender, System.EventArgs e)
		{
		}

		void getPoleAndLevel( out int pole, out int level, Label thislabel)
		//given an object reference to the label representing a disk
		//find its position in the 3x4 disks array of possible positions
		{
			pole = -1;
			level = -1;
			for (int ipole = 0; ipole < 3; ipole++)
			{
				for (int jlevel = 0; jlevel < 4; jlevel++)
				{
					if (disks[ipole,jlevel] != null)
					{
						if (disks[ipole,jlevel].thisDisk == thislabel) 
						{
							pole = ipole + 1;
							level = jlevel + 1;
							break;
						}
					}
				}
				if (pole > -1) break;
			}
		}

		private void Disk1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Label alabel = (sender as Label);
			int pole, level;
			getPoleAndLevel( out pole, out level, alabel);

			DragDropEffects result = alabel.DoDragDrop(alabel,DragDropEffects.All);
			if (result != DragDropEffects.None)
			{
				DropOnPole(pole, level, targetPole);
			}
		}

		private void Pole2_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		//change the cursor to show dropping is allowed
		{
			e.Effect = DragDropEffects.All;
		}

		private void Pole1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		//when a drop happens store the information about which pole was
		//dropped on in the global variable targetPole
		{
			Label alabel = (sender as Label);
			if (alabel == Pole1) targetPole = 1;
			else if (alabel == Pole2) targetPole = 2;
			else if (alabel == Pole3) targetPole = 3;
		}

		private Label getDisk(string DiskName)
		//given a string with the .Name property of a disk
		//return a reference to that disk assuming that only valid names are passed
		{
			
			if (DiskName == "Disk1")
			{
				return Disk1;
			}
			else if (DiskName == "Disk2")
			{
				return Disk2;
			}
			else if (DiskName == "Disk3")
			{
				return Disk3;
			}
			else if (DiskName == "Disk4")
			{
				return Disk4;
			}
			else return Disk4;
		}

		private void btnAnimate_Click(object sender, System.EventArgs e)
		//turn the timer on to begin animation of the moves stored in the textbox
		{
			animateLine = 0;
			isStepping = true; //will prevent adding more moves to the textbox from the replay
			timer1.Enabled = true;
		}

		private void btnStep_Click(object sender, System.EventArgs e)
		//repeat of the moves stored in the textbox one move at a time
		{
			if (animateLine >= textBox1.Lines.Length-1)
			{
				MessageBox.Show("Last available move has been completed");
				return;
			}
			MakeNextMove();
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		//repeat one of the moves stored in the textbox each time the timer fires
		{
			if (!MakeNextMove())
			{
				timer1.Enabled = false;
			}
		}

		private bool MakeNextMove()
		//repeat one of the moves stored in the textbox
		{
			int oldPole, oldLevel, newPole;
			bool MoveOK = false;
			string aMove = textBox1.Lines[animateLine];
			string[] parts = aMove.Split(',');
			Label aDisk = getDisk(parts[0]);
			newPole = Convert.ToInt32(parts[1]);
			getPoleAndLevel( out oldPole, out oldLevel, aDisk);
			isStepping = true; //will prevent adding more moves to the textbox from the replay
			MoveOK = DropOnPole(oldPole, oldLevel, newPole);
			isStepping = false;
			
			if (!MoveOK)
			{
				return false;
			}
			
			animateLine++;
			if (animateLine >= textBox1.Lines.Length)
			{
				return false;
			}
			return true;
		}
	}
	/* sample set of text moves
Disk1,2,1
Disk2,3,1
Disk1,3,2
Disk3,2,1
Disk1,1,2
Disk2,2,2
Disk1,2,3
Disk4,3,1	*/
}
