using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using System.Timers;


class Regles
{
    private RessourceGraphique Ins = null;
    private RenderWindow mFenetre = null;
    private bool mContinuer = true;
    private Sprite mRegle = null;
    private short pos = 0;
    private bool mToucheActive = true;
    private Timer mTimer;


    public void Start()
    {
        mToucheActive = false; // init mToucheActive
        mTimer = new Timer(250); // un timer qui dure 250 msecondes

        mTimer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        mTimer.Enabled = true; // activer timer
    }

    public void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        mToucheActive = true;
        mTimer.Enabled = false;
    }


    public Regles()
    { 
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        mRegle = Ins.GetSpriteRegles((TypeRegles) pos);
        mRegle.Position = new Vector2f(0, 0);
        float X = (float)VideoMode.DesktopMode.Width / (float)mRegle.Texture.Size.X;
        float Y = (float)VideoMode.DesktopMode.Height / (float)mRegle.Texture.Size.Y;
        mRegle.Scale = new Vector2f(X, Y);
       
        
    }

    public void Lancer()
    {
       
        while (mContinuer)
        {
            mFenetre.DispatchEvents();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                mContinuer = false;
                break;
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && mToucheActive)
            {
                Start();
                pos--;
                if (pos == -1)
                    pos = 11;
                mRegle = Ins.GetSpriteRegles((TypeRegles)pos);
                mRegle.Position = new Vector2f(0, 0);
                float X = (float)VideoMode.DesktopMode.Width / (float)mRegle.Texture.Size.X;
                float Y = (float)VideoMode.DesktopMode.Height / (float)mRegle.Texture.Size.Y;
                mRegle.Scale = new Vector2f(X, Y);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && mToucheActive)
            {
                Start();
                pos++;
                if (pos == 12)
                    pos = 0;
                   
                mRegle = Ins.GetSpriteRegles((TypeRegles)pos);
                mRegle.Position = new Vector2f(0, 0);
                float X = (float)VideoMode.DesktopMode.Width / (float)mRegle.Texture.Size.X;
                float Y = (float)VideoMode.DesktopMode.Height / (float)mRegle.Texture.Size.Y;
                mRegle.Scale = new Vector2f(X, Y);
            }
            
            Dessiner();
        }
    }

    public void Dessiner()
    {
        mFenetre.Clear();
        mFenetre.Draw(mRegle);
        mFenetre.Display();
    }

}

