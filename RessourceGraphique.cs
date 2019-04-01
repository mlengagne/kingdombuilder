using System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class RessourceGraphique
{
    private static RessourceGraphique instance = null;
    private RenderWindow m_fenetre = null;
    private Texture m_tilesetTerrain = null;
    private List<Sprite> m_conteneurTerrains = null;
    private Texture m_tilesetMaison = null;
    private Texture m_menu = null;
    private Sprite m_spriteMenu = null;
    private List<Texture> m_boutons = null;
    private List<Sprite> m_sBoutons = null;

    private List<Texture> m_regles = null;
    private List<Sprite> m_sRegles = null;

    private RessourceGraphique()
    {
        m_fenetre = new RenderWindow(VideoMode.DesktopMode, "Kingdom Builder", Styles.Fullscreen);

        m_fenetre.SetVerticalSyncEnabled(true); 
        m_tilesetTerrain = new Texture("sprites/tileset-terrain.png");
        m_tilesetMaison = new Texture("sprites/tileset-maison.png");
        m_conteneurTerrains = new List<Sprite>();
        m_boutons = new List<Texture>();
        m_sBoutons = new List<Sprite>();
        m_regles = new List<Texture>();
         m_sRegles = new List<Sprite>();


        for (int i = 1; i < 10; i++)
        {
            Texture T = new Texture("sprites/bouton/b" + i + ".png");
            m_boutons.Add(T);
            Sprite S = new Sprite(T);
            m_sBoutons.Add(S);
        }
        m_menu = new Texture("sprites/KingdomBuilder.png");
        m_spriteMenu = new Sprite(m_menu);

        for (int i = 1; i < 13; i++)
        {
            Texture t = new Texture("sprites/Regles/r" + i + ".jpg");
            m_regles.Add(t);
            Sprite S = new Sprite(t);
            m_sRegles.Add(S);
        }

        DecoupageTileset(m_tilesetTerrain, m_conteneurTerrains, new Vector2i(128, 128));
    }

    private void DecoupageTileset(Texture texture, List<Sprite> sprites, Vector2i taille)
    {
        Sprite sprite = new Sprite(texture);

        for (Int32 y = 0; y < texture.Size.Y / 128; y++)
        {
            for (Int32 x = 0; x < texture.Size.X / 128; x++)
            {
                sprite.TextureRect = new IntRect(x * taille.X, y * taille.Y, taille.X, taille.Y);
                sprites.Add(new Sprite(sprite));
            }
        }
    }

    public Texture TilesetTerrain
    {
        get { return m_tilesetTerrain; }
    }

    public Texture TilesetMaison
    {
        get { return m_tilesetMaison; }
    }
    
    public Sprite GetSpriteTerrain(TypeTerrain terrain)
    {
        return m_conteneurTerrains[(int)terrain];
    }

    public Sprite GetSpriteRegles(TypeRegles r)
    {
        return m_sRegles[(int)r];
    }

    public Sprite GetSpriteBouton(TypeBouton bouton)
    {
        return m_sBoutons[(int)bouton];
    }

    public Sprite GetSpriteMenu()
    {
        return m_spriteMenu;
    }

    public RenderWindow Fenetre
    {
        get { return m_fenetre; }
        set { m_fenetre = value; }
    }

    public static void Allouer()
    {
        if (instance == null)
            instance = new RessourceGraphique();
    }

    public static RessourceGraphique Instance
    {
        get { return instance; }
    }

    public static IntRect ConvertirIndiceTileset(TypeTerrain tt)
    {
        IntRect rect;

        //on cast la signification du terrain pour l'utiliser comme un int
        int indice = (int)tt;

        //les sprites du tileset font tous 128px de hauteur et largeur
        rect.Width = rect.Height = 128;

        //On initialise Top et Left avant de s'en servir pour trouver la bonne image dans le tileset
        rect.Top = rect.Left = 0;

        /* 
         * Le tileset est formé de ligne de 3 sprites.
         * A chaque fois qu'on dépasse la valeur "3", on change de ligne, on incrémente donc rect.Top.
         * On oublie pas de soustraire a l'indice les 3 valeurs et on reverifie jusqu'a ce qu'on soit sur la bonne ligne.
         * Le reste des soustraction permet de savoir le bon indice sur rect.Width
         */
        while (indice >= 3)
        {
            indice -= 3;
            rect.Top += 128;
        }

        //On applique le reste de la somme sur rect.Left.
        rect.Left = indice * 128;

        return (rect);
    }
}