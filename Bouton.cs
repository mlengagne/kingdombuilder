using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

abstract class Bouton
{
    protected float mPositionX;
    protected float mPositionY;
    protected RessourceGraphique Ins = null;
    protected TypeEffetBouton mAppuie;
    protected RenderWindow mFenetre = null;

    public float PositionX
    {
        get { return mPositionX; }
        set { mPositionX = value; }
    }

    public float PositionY
    {
        get { return mPositionY; }
        set { mPositionY = value; }
    }

    public Bouton(float posX, float posY)
    {
        mPositionX = posX;
        mPositionY = posY;
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        mAppuie = TypeEffetBouton.NORMAL;

    }
    abstract public int Appuyer(int curseurX, int curseurY);
    abstract public void Survoler(int curseurX, int curseurY);
    abstract public void SurvolerAppuyer(int curseurX, int curseurY);
    abstract public void Dessiner();

}
