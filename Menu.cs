using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

class Menu
{
    private RenderWindow mFenetre = null;
    private Sprite mSprtMenuPrincipal = null;
    private bool mContinuer;
    private BoutonTexture mJouer = null;
    private BoutonTexture mRegles = null;
    private BoutonTexture mQuitter = null;
    private RessourceGraphique Ins = null;

    public Menu()
    {
        RessourceAudio.Allouer();
        RessourceGraphique.Allouer();
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        mJouer = new BoutonTexture((float)VideoMode.DesktopMode.Width / 2 - (Ins.GetSpriteBouton(TypeBouton.JOUER_NORMAL).Texture.Size.X / 2), (float)VideoMode.DesktopMode.Height / 2, TypeBouton.JOUER_NORMAL, TypeBouton.JOUER_HOVER, TypeBouton.JOUER_APPUYER);
        mRegles = new BoutonTexture((float)VideoMode.DesktopMode.Width / 2 - (Ins.GetSpriteBouton(TypeBouton.REGLES_NORMAL).Texture.Size.X / 2), (float)VideoMode.DesktopMode.Height / 2 + 100, TypeBouton.REGLES_NORMAL, TypeBouton.REGLES_HOVER, TypeBouton.REGLES_APPUYER);
        mQuitter = new BoutonTexture((float)VideoMode.DesktopMode.Width / 2 - (Ins.GetSpriteBouton(TypeBouton.QUITTER_NORMAL).Texture.Size.X / 2), (float)VideoMode.DesktopMode.Height / 2 + 200, TypeBouton.QUITTER_NORMAL, TypeBouton.QUITTER_HOVER, TypeBouton.QUITTER_APPUYER);
        mContinuer = true;
        mSprtMenuPrincipal = Ins.GetSpriteMenu();
        mSprtMenuPrincipal.Position = new Vector2f(0, 0);

        float X = (float)VideoMode.DesktopMode.Width / (float)mSprtMenuPrincipal.Texture.Size.X;
        float Y = (float)VideoMode.DesktopMode.Height / (float)mSprtMenuPrincipal.Texture.Size.Y;
        mSprtMenuPrincipal.Scale = new Vector2f(X, Y);
    }

    public void Survole(object sender, MouseMoveEventArgs e)
    {
        mJouer.Survoler(e.X, e.Y);
        mRegles.Survoler(e.X, e.Y);
        mQuitter.Survoler(e.X, e.Y);
    }

    public void GestionMenu(object sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left)
        {
            if (mJouer.Appuyer(e.X, e.Y) == (int)TypeEffetBouton.APPUYER)
            {
                DesactiverEcouteurs();
                Jeu j = new Jeu();
                j.Lancer();
                RessourceAudio.Instance.MusiqueMenu.Play(); 
                ActiverEcouteurs();
            }

            if (mRegles.Appuyer(Mouse.GetPosition().X, Mouse.GetPosition().Y) == (int)TypeEffetBouton.APPUYER)
            {
                DesactiverEcouteurs();
                Regles r = new Regles();
                r.Lancer();
                ActiverEcouteurs();
            }
        }
    }

    private void ActiverEcouteurs()
    {
        Ins.Fenetre.MouseMoved += new EventHandler<MouseMoveEventArgs>(Survole);
        Ins.Fenetre.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(GestionMenu);
    }

    private void DesactiverEcouteurs()
    {
        Ins.Fenetre.MouseMoved -= new EventHandler<MouseMoveEventArgs>(Survole);
        Ins.Fenetre.MouseButtonReleased -= new EventHandler<MouseButtonEventArgs>(GestionMenu);
    }

    public void Lancer()
    {
        ActiverEcouteurs();
       RessourceAudio.Instance.MusiqueMenu.Play();

        while (mContinuer)
        {
            mFenetre.DispatchEvents();

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                mJouer.SurvolerAppuyer(Mouse.GetPosition().X, Mouse.GetPosition().Y);
                mRegles.SurvolerAppuyer(Mouse.GetPosition().X, Mouse.GetPosition().Y);
                mQuitter.SurvolerAppuyer(Mouse.GetPosition().X, Mouse.GetPosition().Y);


                if (mQuitter.Appuyer(Mouse.GetPosition().X, Mouse.GetPosition().Y) == (int)TypeEffetBouton.APPUYER)
                {
                    mContinuer = false;
                }
            }

            DessinerMenu();
        }
    }

    public void DessinerMenu()
    {
        mFenetre.Clear();
        mFenetre.Draw(mSprtMenuPrincipal);
        mJouer.Dessiner();
        mRegles.Dessiner();
        mQuitter.Dessiner();
        mFenetre.Display();
    }


    static void Main(string[] args)
    {
        Menu m = new Menu();
        m.Lancer();
    }
}


