using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

class VScrollBar : Object
{
    private int[] mousepos;
    private bool mousedown;
    private int pos;
    private float m_hauteurBarreDefilement;
    private float m_conteurScrollBar;
    private float minGripSize;
    private float m_hauteur, m_largeur;
    private float m_positionX, m_positionY;
    private RessourceGraphique Ins = null;
    private RenderWindow mFenetre = null;
    private RectangleShape grip = null;
    private RectangleShape track = null;
    private List<BoutonTexte> m_boutons = null;
    private string mValBonus = null;
    private Camera mCamera = null;
    private bool mEtatEcouteurs = false;


    private void MouseScroll(object sender, MouseWheelEventArgs e)
    {
        pos -= e.Delta;
    }
    private void MouseHover(object sender, MouseMoveEventArgs e)
    {
        
            Vector2f Coord;
            if (mCamera != null)
                Coord = mFenetre.ConvertCoords(new Vector2i(e.X, e.Y), mCamera);
            else
                Coord = new Vector2f((float)e.X, (float)e.Y);

            mousepos[1] = mousepos[0];
            mousepos[0] = (int)Coord.Y;
            if (mousedown)
            {
                float gripSize = CalculateGripSize();
                float gripPos = CalculateScrollSize() * CalculateScrollRatio();
                if (Coord.Y >= gripPos && Coord.Y <= gripPos + gripSize)
                    pos += mousepos[0] - mousepos[1];
            }

            for (int i = 0; i < m_boutons.Count; i++)
            {
                m_boutons[i].Survoler((int)Coord.X, (int)Coord.Y);
            }
        
    }

    private void MousePressed(object sender, MouseButtonEventArgs e)
    {
        Vector2f Coord;
        if (mCamera != null)
            Coord = mFenetre.ConvertCoords(new Vector2i(e.X, e.Y), mCamera);
        else
            Coord = new Vector2f((float)e.X, (float)e.Y);

        switch (e.Button)
        {
            case Mouse.Button.Left:
                for (int i = 0; i < m_boutons.Count; i++)
                {
                    //m_boutons[i].SurvolerAppuyer((int)Coord.X, (int)Coord.Y);
                    if (m_boutons[i].Appuyer((int)Coord.X, (int)Coord.Y) == (int)TypeEffetBouton.APPUYER)
                        mValBonus = m_boutons[i].ValNom;
                }
                mousedown = false;
                break;
            case Mouse.Button.Right:
                float mouseRatio = mousepos[0] / m_hauteur;
                pos = (int)(mouseRatio * m_conteurScrollBar);
                mousedown = true;
                break;
            default:
                break;
        }
    }

    private float CalculateGripRatio()
    {
        if (m_conteurScrollBar == 0.0f)
            return 0.0f;
        return m_hauteurBarreDefilement / m_conteurScrollBar;
    }

    private float CalculateGripSize()
    {
        float tmp = m_hauteur * CalculateGripRatio();
        return tmp < minGripSize ? minGripSize : tmp;
    }

    private float CalculateScrollSize()
    {
        return m_hauteur - CalculateGripSize();
    }

    private float CalculateScrollRatio()
    {
        return pos / m_conteurScrollBar;
    }

    private void ValidatePos()
    {
        if (pos < 0) pos = 0;
        float gripPos = CalculateScrollSize() * CalculateScrollRatio();
        while (gripPos > CalculateScrollSize())
        {
            pos--;
            gripPos = CalculateScrollSize() * CalculateScrollRatio();
        }
    }



    public VScrollBar()
    {
        mValBonus = "NDEF";
        m_boutons = new List<BoutonTexte>();
        m_hauteur = VideoMode.DesktopMode.Height;
        m_largeur = VideoMode.DesktopMode.Width * 0.1f;
        m_positionX = VideoMode.DesktopMode.Width - m_largeur;
        m_positionY = 0;
        mousepos = new int[2];
        m_hauteurBarreDefilement = 100;
        m_conteurScrollBar = 200;
        minGripSize = 5;
        pos = 0;
        mousepos[0] = 0;
        mousepos[1] = 0;
        mousedown = false;
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        int gripSize = (int)CalculateGripSize();
        gripSize = gripSize < minGripSize ? (int)minGripSize : (int)gripSize;
        float gripPos = CalculateScrollSize() * CalculateScrollRatio();

        grip = new RectangleShape();
        grip.Position = new Vector2f(m_positionX + 2, m_positionY + gripPos);
        grip.Size = new Vector2f(m_largeur, m_positionY + gripPos + gripSize);
        grip.FillColor = new Color(0, 0, 0);
        grip.OutlineThickness = 1.0f;
        grip.OutlineColor = new Color(0, 0, 0);
        track = new RectangleShape();
        track.Position = new Vector2f(m_positionX + 1, m_positionY);
        track.Size = new Vector2f(/*pX + */m_largeur, /*pY +*/ m_hauteur);
        track.FillColor = new Color(0, 0, 0);
        track.OutlineThickness = 1.0f;
        track.OutlineColor = new Color(70, 70, 70);
    }

    public void Positionner()
    {
        m_positionX = (mCamera.Center.X + mCamera.Size.X / 2) - m_largeur;
        m_positionY = (mCamera.Center.Y - mCamera.Size.Y / 2);

        m_hauteur = mCamera.Size.Y;
        m_largeur = mCamera.Size.X * 0.1f;

        track.Position = new Vector2f(m_positionX + 1, m_positionY);
        track.Size = new Vector2f(m_largeur, m_hauteur);
        float gripPos = CalculateScrollSize() * CalculateScrollRatio();
        grip.Position = new Vector2f(m_positionX + 2, m_positionY + gripPos);
        int gripSize = (int)CalculateGripSize();
        grip.Size = new Vector2f(m_largeur, gripSize);
    }

    public string Bonus
    {
        get { return mValBonus; }
        set { mValBonus = value; }
    }

    public void ActiverEcouteurs()
    {
        if (mEtatEcouteurs == false)
        {
            mEtatEcouteurs = true;
            Ins.Fenetre.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(MousePressed);
            Ins.Fenetre.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseHover);
            Ins.Fenetre.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(MouseScroll);
        }
    }

    public void DesactiverEcouteurs()
    {
        if (mEtatEcouteurs == true)
        {
            mEtatEcouteurs = false;
            Ins.Fenetre.MouseMoved -= new EventHandler<MouseMoveEventArgs>(MouseHover);
            Ins.Fenetre.MouseButtonPressed -= new EventHandler<MouseButtonEventArgs>(MousePressed);
            Ins.Fenetre.MouseWheelMoved -= new EventHandler<MouseWheelEventArgs>(MouseScroll);
        }
    }

    public void SupprimerBoutons()
    {
        m_boutons.Clear();
    }

    public void AjouterBouton(BoutonTexte bouton)
    {
        m_boutons.Add(bouton);
    }

    public Camera Camera
    {
        get { return mCamera; }
        set { mCamera = value; }
    }

    public void Dessiner()
    {
        ValidatePos();
        mFenetre.Draw(track);
        mFenetre.Draw(grip);
        for (int i = 0; i < m_boutons.Count; i++)
        {
            m_boutons[i].Taille = new Vector2f(mCamera.Size.X * 0.1f, mCamera.Size.Y * 0.1f);
            m_boutons[i].Positionner(grip.Position.X, grip.Position.Y + m_boutons[i].TailleY * i, mCamera.Size.X, mCamera.Size.Y);
            m_boutons[i].Dessiner();
        }
    }
}

