using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

class Jeu
{
    private BoiteInfo m_boiteInfo = null;
    private RessourceGraphique Ins = null;
    private Camera mCamera = null;
    private Plateau mPlateau = null;
    private List<JoueurHumain> mJoueurs = null;
    private JoueurHumain mJoueurCourant = null;
    private RenderWindow mFenetre = null;
    private VScrollBar mScroll = null;
    private PopUp mPopUpBonus = null;
    private Etape mEtape = Etape.PLACER_MAISON;
    private Timer mTimer;
    private bool mContinuer = true;
    private bool mMaisonPlacees = false;
    private bool mSourisActivee = true;
    private int mIndexJoueur = 0;
    private bool bonusDejaUtilise = false;
    private string bonusChoisis = null;
    private int compteurBonus = 0, compteurMaison = 0;
    private Coordonnee coord_origine = null;
    private String[] objectifs = null;
    private static String[] liste_objectifs = { "Explorateur", "Ouvrier", "Chevalier", "Mineur", "Pecheur" };
    private List<KeyValuePair<Coordonnee, Terrain>> mCaseSpeciales = null;


    private void StartTimer()
    {
        mSourisActivee = false; // init mToucheActive
        mTimer = new Timer(250); // un timer qui dure 250 msecondes

        mTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        mTimer.Enabled = true; // activer timer
    }
    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        mSourisActivee = true;
        mTimer.Enabled = false;
    }

    /// <summary>
    /// cree une liste contenant les cases spéciales du plateau 
    /// </summary>
    /// <param name="p"></param>
    private void ListesCasesSpeciales()
    {
        String[] tmpType = { "CHATEAU", "ORACLE", "FERME", "TAVERNE", "TOUR", "PORT", "ENCLOS", "GRANGE", "OASIS" };
        foreach (KeyValuePair<Coordonnee, Terrain> c in mPlateau)
        {
            if (tmpType.Contains(c.Value.Type.ToString()))
            {
                mCaseSpeciales.Add(c);
            }
        }
    }

    private void SupprimerListeBonus()
    {
        mScroll.SupprimerBoutons();
    }

    private void RemplirListeBonus()
    {
        for (int i = 0; i < mJoueurCourant.ListeBonus.Count; i++)
        {
            mScroll.AjouterBouton(new BoutonTexte(0.0f, 0.0f, mJoueurCourant.ListeBonus[i].ToString(), VideoMode.DesktopMode.Width * 0.1f, VideoMode.DesktopMode.Height * 0.1f, Color.Black, Color.White));
        }
    }

    public Jeu()
    {
        mCamera = new Camera();
        m_boiteInfo = new BoiteInfo(mCamera);
        Ins = RessourceGraphique.Instance;
        mFenetre = Ins.Fenetre;
        mFenetre.SetView(mCamera);
        mPlateau = new Plateau();
        mPlateau.Initialiser();

        mJoueurs = new List<JoueurHumain>();
        mJoueurs.Add(new JoueurHumain(Couleur.BLEU));
        mJoueurs.Add(new JoueurHumain(Couleur.BLANC));
        mJoueurs.Add(new JoueurHumain(Couleur.NOIR));
        mJoueurs.Add(new JoueurHumain(Couleur.ROUGE));
        mJoueurCourant = mJoueurs[mIndexJoueur];
        mCaseSpeciales = new List<KeyValuePair<Coordonnee, Terrain>>();

        ListesCasesSpeciales();

        mScroll = new VScrollBar();

        mPopUpBonus = new PopUp("Voulez vous activer vos bonus?");
        mPopUpBonus.Camera = mCamera;
        mScroll.Camera = mCamera;

    }

    /// <summary>
    /// 
    /// </summary>
    public void Lancer()
    {
        mFenetre.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(GestionJeu);

        objectifs = new String[3];
        Random rand = new Random();
        int rnd;
        int i = 0;
        do
        {
            rnd = rand.Next(3);
            if (!objectifs.Contains(liste_objectifs[rnd]))
            {
                objectifs[i] = liste_objectifs[rnd];
                i++;
            }
        } while (i < 3);
        RessourceAudio.Instance.MusiqueMenu.Stop();
        RessourceAudio.Instance.MusiqueJeu.Play();

        while (mContinuer)
        {
            mFenetre.DispatchEvents();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                mContinuer = false;
                break;
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left) && mSourisActivee)
                StartTimer();

            switch (mEtape)
            {

                case Etape.ACTIVER_BONUS:
                    mPopUpBonus.ActiverEcouteurs();

                    if (mPopUpBonus.Reponse == "OUI")
                    {
                        mEtape = Etape.CHOISIR_BONUS;
                        mPopUpBonus.Reponse = "NDEF"; // reset Reponse PopUp
                    }
                    else if (mPopUpBonus.Reponse == "NON")
                    {

                        if (mMaisonPlacees == true)
                        {
                            mEtape = Etape.PIOCHER;
                        }
                        else
                        {
                            mEtape = Etape.PLACER_MAISON;
                        }

                        mPopUpBonus.Reponse = "NDEF"; // reset Reponse PopUp
                    }

                    break;

                case Etape.CHOISIR_BONUS:
                    mScroll.ActiverEcouteurs();
                    mPopUpBonus.DesactiverEcouteurs();
                    break;

                case Etape.PIOCHER:
                    mJoueurCourant.CarteTerrain = mJoueurCourant.Piocher();
                    mEtape = Etape.FIN_TOUR;
                    break;

                case Etape.FIN_TOUR:
                    mMaisonPlacees = false;
                    compteurBonus = 0;
                    SupprimerListeBonus();
                    mJoueurCourant.RecupererBonus(mPlateau);
                    ChangerJoueur();
                    bonusDejaUtilise = false;

                    if (mJoueurCourant.m_listeBonus.Count == 0)
                    {
                        mEtape = Etape.PLACER_MAISON;
                    }
                    else
                    {
                        mEtape = Etape.ACTIVER_BONUS;
                        RemplirListeBonus();
                    }
                    break;
                case Etape.FIN_PARTIE:
                    foreach (JoueurHumain j in mJoueurs)
                    {
                        int score = j.m_maisonsPlacees.Count;

                        foreach (String nomObjectif in objectifs)
                        {
                            if (nomObjectif == "Explorateur")
                            {
                                score += Objectif.Explorateur(mPlateau, j.Couleur);
                            }
                            if (nomObjectif == "Ouvrier")
                            {
                                foreach (KeyValuePair<Coordonnee, Terrain> c in mCaseSpeciales)
                                {
                                    Tuple<Coordonnee, Terrain> c2 = new Tuple<Coordonnee, Terrain>(c.Key, c.Value);
                                    score += Objectif.Ouvrier(mPlateau, c2, j.Couleur);
                                }
                            }
                            if (nomObjectif == "Chevalier")
                                score += Objectif.Chevalier(mPlateau, j.Couleur);
                            if (nomObjectif == "Mineur")
                                score += Objectif.Mineur(mPlateau, j.m_maisonsPlacees);
                            if (nomObjectif == "Pecheur")
                                score += Objectif.Pecheur(mPlateau, j.m_maisonsPlacees);
                        }

                        j.Score = (Int16)score;
                    }
                    mContinuer = false;
                    break;
                default:
                    mScroll.DesactiverEcouteurs();
                    break;
            }
            m_boiteInfo.ModifierTextes(mJoueurCourant.Couleur, mJoueurCourant.CarteTerrain, mJoueurCourant.NbMaisons);
            mCamera.MiseAJour();
            m_boiteInfo.PositionnerBoite(mCamera);
            Dessiner();
        }
        RessourceAudio.Instance.MusiqueJeu.Stop();
        Ins.Fenetre.SetView(Ins.Fenetre.DefaultView);
    }


    private void GestionJeu(object sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left)
        {
            switch (mEtape)
            {
                case Etape.CHOISIR_BONUS:

                    bonusChoisis = mScroll.Bonus;

                    //si il choisit un bonus
                    if (bonusChoisis != "NDEF")
                    {
                        mEtape = Etape.UTILISER_BONUS;
                        mScroll.Bonus = "NDEF";
                        mScroll.DesactiverEcouteurs();
                    }

                    break;

                case Etape.UTILISER_BONUS:
                    compteurBonus++;

                    if (BonusApplicable(bonusChoisis, compteurBonus))
                    {
                        if (PlacerMaison(coord_origine, bonusChoisis))
                        {
                            RessourceAudio.Instance.SonPoserMaison.Play();
                            bonusChoisis = "NDEF";
                            compteurBonus = 0;

                            if (mMaisonPlacees == true)
                                mEtape = Etape.FIN_TOUR;
                            else
                                mEtape = Etape.PLACER_MAISON;

                            bonusDejaUtilise = true;
                        }
                        else
                        {
                            compteurBonus = 0;
                        }
                    }

                    else
                    {
                        coord_origine = Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau);
                    }

                    break;
                case Etape.PLACER_MAISON:
                    if (mJoueurCourant.PlacerMaison(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mPlateau))
                    {
                        RessourceAudio.Instance.SonPoserMaison.Play();
                        if (++compteurMaison == 3)
                        {
                            mMaisonPlacees = true;
                            compteurMaison = 0;

                            if (mJoueurCourant.ListeBonus.Count == 0)
                                mEtape = Etape.PIOCHER;
                            else if (bonusDejaUtilise == true)
                                mEtape = Etape.FIN_TOUR;
                            else
                                mEtape = Etape.ACTIVER_BONUS;
                        }
                        if (mJoueurCourant.NbMaisons == 0)
                        {
                            mEtape = Etape.FIN_PARTIE;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void Dessiner()
    {

        mFenetre.Clear();
        mPlateau.Dessiner(mFenetre);

        if (mEtape == Etape.ACTIVER_BONUS)
        {
            mPopUpBonus.Positionner();
            mPopUpBonus.Dessiner();
        }
        else if (mEtape == Etape.CHOISIR_BONUS)
        {
            mScroll.Positionner();
            mScroll.Dessiner();
        }

        mFenetre.SetView(mCamera);
        m_boiteInfo.Dessiner();
        mFenetre.Display();
    }


    public void ChangerJoueur()
    {
        mIndexJoueur++;
        if (mIndexJoueur == 4)
            mIndexJoueur = 0;

        mJoueurCourant = mJoueurs[mIndexJoueur];


    }

    public bool BonusApplicable(string valBonus, int compteurBonus)
    {
        switch (valBonus)
        {
            case "ORACLE":
                return (compteurBonus == 1);
            case "FERME":
                return (compteurBonus == 1);
            case "TAVERNE":
                return (compteurBonus == 1);
            case "TOUR":
                return (compteurBonus == 1);
            case "PORT":
                return (compteurBonus == 2);
            case "ENCLOS":
                return (compteurBonus == 2);
            case "GRANGE":
                return (compteurBonus == 2);
            case "OASIS":
                return (compteurBonus == 1);
            default:
                return false;
        }
    }

    public bool PlacerMaison(Coordonnee origine, string valBonus)
    {

        switch (valBonus)
        {
            case "ORACLE":
                return Bonus.Oracle(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "FERME":
                return Bonus.Ferme(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "TAVERNE":
                return Bonus.Taverne(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "TOUR":
                return Bonus.Tour(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "PORT":
                return Bonus.Port(origine, Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "ENCLOS":
                return Bonus.Enclos(origine, Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "GRANGE":
                return Bonus.Grange(origine, Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            case "OASIS":
                return Bonus.Oasis(Plateau.Vector2fVersCoordonnee(CoordonneesSouris(), mPlateau), mJoueurCourant, mPlateau);
            default:
                return false;

        }
    }

    public Vector2f CoordonneesSouris()
    {
        Vector2f souris = mFenetre.ConvertCoords(Mouse.GetPosition(), mCamera);
        return souris;
    }
}

