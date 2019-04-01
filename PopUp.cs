using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

class PopUp
{
    private RenderWindow mFenetre = null;
    private RessourceGraphique Ins = null;
    private BoutonTexte mBoutonOui = null;
    private BoutonTexte mBoutonNon = null;
    private string mReponse = null;

    private RectangleShape mCadre = null;
    private Text mQuestion = null;
    private Camera mCamera = null;
    private bool mEtatEcouteurs = false;

    public Camera Camera
    {
        get { return mCamera; }
        set { mCamera = value; }
    }

    private void MouseHover(object sender, MouseMoveEventArgs e)
    {
        
            Vector2f Coord;
            if (mCamera != null)
                Coord = mFenetre.ConvertCoords(new Vector2i(e.X, e.Y), mCamera);
            else
                Coord = new Vector2f((float)e.X, (float)e.Y);

            mBoutonNon.Survoler((int)Coord.X, (int)Coord.Y);
            mBoutonOui.Survoler((int)Coord.X, (int)Coord.Y);
        
    }

    private void MousePressed(object sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left)
        {
            Vector2f Coord;
            if (mCamera != null)
                Coord = mFenetre.ConvertCoords(new Vector2i(e.X, e.Y), mCamera);
            else
                Coord = new Vector2f((float)e.X, (float)e.Y);

            //mBoutonNon.SurvolerAppuyer((int)Coord.X, (int)Coord.Y);
            //mBoutonOui.SurvolerAppuyer((int)Coord.X, (int)Coord.Y);

        }
    }


    private void MouseReleased(object sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left)
        {
            Vector2f Coord;
            if (mCamera != null)
                Coord = mFenetre.ConvertCoords(new Vector2i(e.X, e.Y), mCamera);
            else
                Coord = new Vector2f((float)e.X, (float)e.Y);

            if (mBoutonNon.Appuyer((int)Coord.X, (int)Coord.Y) == (int)TypeEffetBouton.APPUYER)
                mReponse = "NON";
            else if (mBoutonOui.Appuyer((int)Coord.X, (int)Coord.Y) == (int)TypeEffetBouton.APPUYER)
                mReponse = "OUI";

        }
    }


    public void Positionner()
    {
        mCadre.Position = new Vector2f(mCamera.Center.X - mCadre.Size.X / 2, mCamera.Center.Y - mCadre.Size.Y / 2);
        mQuestion.Position = new Vector2f(mCadre.Position.X + (mCadre.Size.X / 2 - (mQuestion.GetGlobalBounds().Width * ((float)VideoMode.DesktopMode.Width / 1920.0f)) / 2), mCadre.Position.Y + (mQuestion.GetGlobalBounds().Height * ((float)VideoMode.DesktopMode.Width / 1920.0f)));
        mBoutonOui.Positionner(mCadre.Position.X + (mCadre.Size.X * 0.25f - mBoutonOui.TailleX / 2), mCadre.Position.Y + (mCadre.Size.Y * 0.75f - mBoutonOui.TailleY / 2), VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
        mBoutonNon.Positionner(mCadre.Position.X + (mCadre.Size.X * 0.75f - mBoutonNon.TailleX / 2), mCadre.Position.Y + (mCadre.Size.Y * 0.75f - mBoutonNon.TailleY / 2), VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);

    }

    public string Reponse
    {
        get { return mReponse; }
        set { mReponse = value; }
    }

    public PopUp(string question)
    {
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        mCadre = new RectangleShape();
        mCadre.Size = new Vector2f(0.3f * VideoMode.DesktopMode.Width, 0.15f * VideoMode.DesktopMode.Height);
        mCadre.Position = new Vector2f(VideoMode.DesktopMode.Width / 2 - mCadre.Size.X / 2, VideoMode.DesktopMode.Height / 2 - mCadre.Size.Y / 2);
        mCadre.FillColor = Color.Black;

        mQuestion = new Text();
        mQuestion.Font = Font.DefaultFont;
        mQuestion.Color = Color.White;
        mQuestion.Scale = new Vector2f(1.0f * ((float)VideoMode.DesktopMode.Width / 1920.0f), 1.0f * ((float)VideoMode.DesktopMode.Height / 1080.0f));
        mQuestion.DisplayedString = question;
        mQuestion.Position = new Vector2f(mCadre.Position.X + (mCadre.Size.X / 2 - (mQuestion.GetGlobalBounds().Width * ((float)VideoMode.DesktopMode.Width / 1920.0f)) / 2), mCadre.Position.Y + (mQuestion.GetGlobalBounds().Height * ((float)VideoMode.DesktopMode.Width / 1920.0f)));


        mBoutonOui = new BoutonTexte(0.0f, 0.0f, "Oui", mCadre.Size.X / 4.0f, mCadre.Size.Y / 4.0f, Color.Blue, Color.Green);
        mBoutonOui.Positionner(mCadre.Position.X + (mCadre.Size.X * 0.25f - mBoutonOui.TailleX / 2), mCadre.Position.Y + (mCadre.Size.Y * 0.75f - mBoutonOui.TailleY / 2), VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
        mBoutonNon = new BoutonTexte(0.0f, 0.0f, "Non", mCadre.Size.X / 4.0f, mCadre.Size.Y / 4.0f, Color.Blue, Color.Green);
        mBoutonNon.Positionner(mCadre.Position.X + (mCadre.Size.X * 0.75f - mBoutonNon.TailleX / 2), mCadre.Position.Y + (mCadre.Size.Y * 0.75f - mBoutonNon.TailleY / 2), VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);

    }

    public void ActiverEcouteurs()
    {
        if (mEtatEcouteurs == false)
        {
            mEtatEcouteurs = true;
            Ins.Fenetre.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseHover);
            Ins.Fenetre.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(MousePressed);
            Ins.Fenetre.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(MouseReleased);
        }
    }

    public void DesactiverEcouteurs()
    {
        if (mEtatEcouteurs == true)
        {
            mEtatEcouteurs = false;
            Ins.Fenetre.MouseMoved -= new EventHandler<MouseMoveEventArgs>(MouseHover);
            Ins.Fenetre.MouseButtonPressed -= new EventHandler<MouseButtonEventArgs>(MousePressed);
            Ins.Fenetre.MouseButtonReleased -= new EventHandler<MouseButtonEventArgs>(MouseReleased);
        }
    }



    public void Dessiner()
    {
        mFenetre.Draw(mCadre);
        mFenetre.Draw(mQuestion);
        mBoutonOui.Dessiner();
        mBoutonNon.Dessiner();
    }


}

