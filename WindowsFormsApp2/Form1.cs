using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RollD20WindowsForm;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public List<Dice> RollResult;
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        public void DiceRollsFormFiller()
        {
            foreach(Dice die in RollResult)
            {
                chart1.Series["NumberOfDiceRolls"].Points.Add(die.Result); 
            }
            chart1.Titles.Add("Number Of Dice Rolls Per Number");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
