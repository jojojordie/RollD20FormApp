using System;

public class Dice
{
    public int NumberOfSides;
    public int Result;
    static Random rnd = new Random();

    public void DiceRoller()
    {
        int RollerResult = rnd.Next(1, NumberOfSides + 1);
        Result = RollerResult;
    }
}