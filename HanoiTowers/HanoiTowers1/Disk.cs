using System;
using System.Windows.Forms;

namespace HanoiTowers1
{
	//the Disk class stores the pole and level of a label representing the disk
	//as well as an object refrence to the label
	//It also holds the constants needed to show a "disk" label correctly
	//in position on a given level and pole and uses these constants
	//in its Move(int newPole, int newLevel) method.
	public class Disk
	{
		const int maxPoles = 3;
		const int poleStart = 228;
		const int poleGap = 180;
		const int deckHeight = 240;
		const int diskHeight = 24;
		
		private int pole;
		private int level;
		private int width;
		public Label thisDisk;
		
	
		public Disk(Label aLabel, int aPole, int aLevel)
		{
			width = aLabel.Width;
			pole = aPole;
			level = aLevel;
			thisDisk = aLabel;
		}

		public void Move(int newPole, int newLevel)
		{
			pole = newPole;
			level = newLevel;
			thisDisk.Hide();
			thisDisk.Left = poleStart + ((pole - 1) * poleGap) - (width / 2);
			thisDisk.Top = deckHeight - (level * diskHeight);
			thisDisk.Show();
		}
	
	}
}
