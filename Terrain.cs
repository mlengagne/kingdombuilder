using SFML.Graphics;

class Terrain : Sprite
{
    private TypeTerrain m_type;
    private Couleur m_couleur;
    private Sprite m_maison;
    private FloatRect m_corps;

    public Terrain(TypeTerrain type)
    {
        m_type = type;
        m_couleur = Couleur.AUCUNE;
        m_maison = new Sprite(RessourceGraphique.Instance.TilesetMaison);
        m_maison.TextureRect = new IntRect((int)m_couleur * 64, 0, 64, 64);

        this.Texture = RessourceGraphique.Instance.TilesetTerrain;
        this.TextureRect = RessourceGraphique.ConvertirIndiceTileset(type);
    }

    public FloatRect Corps
    {
        get { return m_corps; }
        set { m_corps = value; }
    }

    public Sprite SpriteMaison
    {
        get { return m_maison; }
    }

    public TypeTerrain Type
    {
        get { return m_type; }
        set { m_type = value; }
    }
    //--------------------
    public Couleur Couleur
    {
        get
        {
            return m_couleur;
        }
        set
        {
            m_couleur = value;
            m_maison.TextureRect = new IntRect((int)m_couleur * 64, 0, 64, 64);
        }
    }

    public override string ToString()
    {
        return this.GetGlobalBounds().ToString() + " " + m_type.ToString();
    }
}
