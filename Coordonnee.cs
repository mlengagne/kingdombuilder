using System;

class Coordonnee : Tuple<int, int>
{
    private int lvl = -1;
    public Coordonnee(int x, int y)
        : base(x, y)
    { }

    public int X
    {
        get { return Item1; }
    }
    public int Y
    {
        get { return Item2; }
    }

    public int Lvl
    {
        get { return lvl; }
        set { lvl = value; }
    }
}