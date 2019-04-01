using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

class BoutonTexte : Bouton
{
    private Text m_valNom = null;
    private float m_hauteur, m_largeur;
    private RectangleShape m_Cadre = null;
    private Color m_fondCadre;

    public string ValNom
    {
        get { return m_valNom.DisplayedString; }
    }

    public float TailleY 
    {
        get { return m_Cadre.Size.Y; }

    }

    public Vector2f Taille
    {
        set { m_Cadre.Size = value; }
    }

    public float TailleX
    {

        get { return m_Cadre.Size.X; }

    }

    public void Positionner(float posX, float posY, float largeur, float hauteur)
    {

        PositionX = posX;
        PositionY = posY;
        //m_largeur = largeur ;
        //m_hauteur = hauteur;
        m_Cadre.Position = new Vector2f(posX, posY);
        m_valNom.Scale = new Vector2f(1.0f * (largeur / 1920.0f), 1.0f * (hauteur / 1080.0f));

        m_valNom.Position = new Vector2f(posX + (m_Cadre.Size.X / 2 - ((m_valNom.GetGlobalBounds().Width * (largeur / 1920.0f)) / 2)), posY + (m_Cadre.Size.Y / 2 - (m_valNom.GetGlobalBounds().Height * (hauteur / 1080.0f))));
    }

    public BoutonTexte(float posX, float posY, string nom, float largeur, float hauteur, Color fond, Color police) : base(posX,posY)
    {
        m_largeur = largeur ;
        m_hauteur = hauteur;
        m_Cadre = new RectangleShape();
        m_Cadre.Size = new Vector2f(m_largeur, m_hauteur);
        m_fondCadre = fond;
        m_valNom = new Text();
        m_valNom.DisplayedString = nom;
        m_valNom.Font = Font.DefaultFont;
        m_valNom.Color = police;
        m_valNom.Scale = new Vector2f(1.0f * ((float)VideoMode.DesktopMode.Width / 1920.0f), 1.0f * ((float)VideoMode.DesktopMode.Height / 1080.0f));
    
    }

    public override int Appuyer(int curseurX, int curseurY)
    {
        if (curseurX >= mPositionX && curseurX < mPositionX + m_Cadre.Size.X && curseurY >= mPositionY && curseurY < mPositionY + m_Cadre.Size.Y)
        {
            return (int)TypeEffetBouton.APPUYER;
        }
        else
        {
            return (int)TypeEffetBouton.NORMAL;
        }

    }

    public override void Survoler(int curseurX, int curseurY)
    {
        if (curseurX >= mPositionX && curseurX < mPositionX + m_Cadre.Size.X && curseurY >= mPositionY && curseurY < mPositionY + m_Cadre.Size.Y)
        {
            mAppuie = TypeEffetBouton.HOVER;
        }
        else
        {
            mAppuie = TypeEffetBouton.NORMAL;
        }
    }


    public override void SurvolerAppuyer(int curseurX, int curseurY)
    {
        if (curseurX >= mPositionX && curseurX < mPositionX + m_Cadre.Size.X && curseurY >= mPositionY && curseurY < mPositionY + m_Cadre.Size.Y)
        {
            mAppuie = TypeEffetBouton.APPUYER;
        }
        else
        {
            mAppuie = TypeEffetBouton.NORMAL;
        }
    }



    public override void Dessiner()
    {
        switch (mAppuie)
        {
            case TypeEffetBouton.NORMAL:
                m_Cadre.FillColor = m_fondCadre ;
                break;
            case TypeEffetBouton.HOVER:
                m_Cadre.FillColor = new Color(180, 20, 50);
                break;
            case TypeEffetBouton.APPUYER:
                m_Cadre.FillColor = new Color(90, 180, 10);
                break;
            default:
                break;
        }

        mFenetre.Draw(m_Cadre);
        mFenetre.Draw(m_valNom);
    }
}

