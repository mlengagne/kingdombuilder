using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;

class KingdomBuilder : IDisposable
{
    private RenderWindow m_ecran = null;
    private Stopwatch m_chronometre = null;

    public KingdomBuilder()
    {
        m_ecran = new RenderWindow(new VideoMode(Constante.LARGEUR_FENETRE, Constante.HAUTEUR_FENETRE), "Kingdom Builder", Styles.None | Styles.Titlebar | Styles.Close);
        m_ecran.SetVerticalSyncEnabled(true);
        m_ecran.Closed += new EventHandler(Fermer);

        m_chronometre = new Stopwatch();
        m_chronometre.Start();
    }

    /// <summary>
    /// Boucle de jeu.
    /// Limité à 60 images par seconde.
    /// </summary>
    public void Lancer()
    {
        while (m_ecran.IsOpen())
        {
            m_ecran.DispatchEvents();

            MiseAJour();

            m_ecran.Clear();
            Dessiner();
            m_ecran.Display();
        }
    }

    /// <summary>
    /// Toutes les appelles aux méthodes de mises à jours des objets s'effectueront ici.
    /// </summary>
    private void MiseAJour()
    {
        //Exemple : Personnage.MiseAJour();
    }

    /// <summary>
    /// Toutes les appelles aux méthodes de dessins des objets s'effectueront ici.
    /// </summary>
    private void Dessiner()
    {
        //Exemple : Personnage.Dessiner(m_ecran);
        //Exemple : Plateau.Dessiner(m_ecran);
    }

    private void Fermer(object sender, EventArgs e)
    {
        RenderWindow ecran = (RenderWindow)sender;
        ecran.Close();
    }

    /// <summary>
    /// m_ecran (RenderWindow) implémentant l'interface IDisposable, l'analyse de code C# requiert que la classe Kingdom Builder l'implémente également 
    /// et que la méthode Dispose soit définie.
    /// </summary>
    public void Dispose()
    {
        m_ecran.Dispose();
    }
}