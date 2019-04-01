using SFML.Graphics;
using SFML.Window;
using System;

class BoiteInfo
{
    private RectangleShape m_boite = null;
    private Text m_infoJoueur = null;
    private Text m_infoTerrain = null;
    private Text m_infoMaison = null;

    public BoiteInfo(Camera camera)
    {
        m_boite = new RectangleShape(new Vector2f(camera.Size.X * 0.12f, camera.Size.Y * 0.1f));
        m_boite.Position = new Vector2f(0.0f, 0.0f);
        m_boite.FillColor = new Color(0, 0, 0, 200);
        m_boite.OutlineThickness = 5.0f;
        m_boite.OutlineColor = new Color(70, 70, 70, 200);

        m_infoJoueur = new Text();
        m_infoJoueur.Font = Font.DefaultFont;
        m_infoJoueur.Color = Color.White;
        m_infoJoueur.Position = new Vector2f(5.0f, 5.0f);

        m_infoTerrain = new Text();
        m_infoTerrain.Font = Font.DefaultFont;
        m_infoTerrain.Color = Color.White;
        m_infoTerrain.Position = new Vector2f(5.0f, m_infoJoueur.Position.Y + m_infoJoueur.GetGlobalBounds().Height + 10.0f);

        m_infoMaison = new Text();
        m_infoMaison.Font = Font.DefaultFont;
        m_infoMaison.Color = Color.White;
        m_infoMaison.Position = new Vector2f(5.0f, m_infoTerrain.Position.Y + m_infoTerrain.GetGlobalBounds().Height + 10.0f);
    }

    private Color RecupererCouleur(Couleur couleur)
    {
        switch (couleur)
        {
            case Couleur.BLANC:
                return Color.White;
            case Couleur.BLEU:
                return Color.Blue;
            case Couleur.NOIR:
                return Color.Black;
            case Couleur.ROUGE:
                return Color.Red;
            default:
                return Color.Magenta;
        }
    }

    public void ModifierTextes(Couleur couleur, TypeTerrain type, Int16 nbMaisons)
    {
        m_infoJoueur.DisplayedString = "Joueur " + couleur.ToString();
        m_infoJoueur.Color = RecupererCouleur(couleur);

        m_infoTerrain.DisplayedString = "Terrain " + type.ToString();
        m_infoTerrain.Color = RecupererCouleur(couleur);

        m_infoMaison.DisplayedString = "Maison x" + nbMaisons.ToString();
        m_infoMaison.Color = RecupererCouleur(couleur);

        if (couleur == Couleur.NOIR)
            m_boite.FillColor = new Color(0, 0, 0, 0);
        else
            m_boite.FillColor = new Color(0, 0, 0, 200);
    }

    public void PositionnerBoite(Camera camera)
    {
        m_boite.Position = new Vector2f(camera.Center.X - (camera.Size.X / 2.0f), camera.Center.Y - (camera.Size.Y / 2.0f));
        m_boite.Size = new Vector2f(camera.Size.X * 0.15f, camera.Size.Y * 0.12f);
        m_boite.OutlineThickness = 5.0f * camera.Size.X / VideoMode.DesktopMode.Width ;

        m_infoJoueur.Scale = new Vector2f(1.0f * (camera.Size.X / 1920.0f), 1.0f * (camera.Size.Y / 1080.0f));
        m_infoJoueur.Position = new Vector2f(m_boite.Position.X + (m_boite.Size.X / 2 - ((m_infoJoueur.GetGlobalBounds().Width * (camera.Size.X / 1920.0f)) / 2)), m_boite.Position.Y + (m_boite.Size.Y / 4.0f) - m_infoJoueur.GetGlobalBounds().Height * (camera.Size.Y / 1080.0f));

        m_infoTerrain.Scale = new Vector2f(1.0f * (camera.Size.X / 1920.0f), 1.0f * (camera.Size.Y / 1080.0f));
        m_infoTerrain.Position = new Vector2f(m_boite.Position.X + (m_boite.Size.X / 2 - ((m_infoJoueur.GetGlobalBounds().Width * (camera.Size.X / 1920.0f)) / 2)), m_boite.Position.Y + (m_boite.Size.Y / 2 - (m_infoJoueur.GetGlobalBounds().Height * (camera.Size.Y / 1080.0f))));

        m_infoMaison.Scale = new Vector2f(1.0f * (camera.Size.X / 1920.0f), 1.0f * (camera.Size.Y / 1080.0f));
        m_infoMaison.Position = new Vector2f(m_boite.Position.X + (m_boite.Size.X / 2 - ((m_infoJoueur.GetGlobalBounds().Width * (camera.Size.X / 1920.0f)) / 2)), m_boite.Position.Y + (m_boite.Size.Y / 1.35f) - m_infoJoueur.GetGlobalBounds().Height * (camera.Size.Y / 1080.0f));
    }

    public void Dessiner()
    {
        RessourceGraphique.Instance.Fenetre.Draw(m_boite);
        RessourceGraphique.Instance.Fenetre.Draw(m_infoJoueur);
        RessourceGraphique.Instance.Fenetre.Draw(m_infoTerrain);
        RessourceGraphique.Instance.Fenetre.Draw(m_infoMaison);
    }
}
