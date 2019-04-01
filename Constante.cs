enum TypeTerrain : int
{
    AUCUN = -1,   //ON ESPERE QUE CA PLANTERA PAS
    PRAIRIE = 0,
    FORET = 1,
    MONTAGNE = 2,
    FLEUR = 3,
    DESERT = 4,
    EAU = 5,
    CANYON = 6,
    CHATEAU = 7,
    ORACLE = 8,
    FERME = 9,
    TAVERNE = 10,
    TOUR = 11,
    PORT = 12,
    ENCLOS = 13,
    GRANGE = 14,
    OASIS = 15
}

enum TypeBouton : int
{
    
    JOUER_HOVER = 0,
    JOUER_APPUYER = 1,
    JOUER_NORMAL = 2,
    QUITTER_NORMAL = 3,
    QUITTER_HOVER = 4,
    QUITTER_APPUYER = 5,
    REGLES_APPUYER = 7,
    REGLES_NORMAL = 8,
    REGLES_HOVER = 6,
    
}

enum TypeRegles : int
{

    ACT_SPE_DEPLACEMETNT,
    ACT_SPE_CONSTRUCT,
    /*OBJECTIF_1,
    OBJECTIF_2,*/
    ARTISANS,
    CHEVALIERS,
    CITOYENS,
    COMMERCANT,
    ERMITES,
    EXPLORATEUR,
    FERMIERS,
    MINEURS,
    PECHEUR,
    SEIGNEURS
}

enum TypeEffetBouton : int
{
    NORMAL,
    HOVER ,
    APPUYER ,
}

enum Couleur : sbyte
{
    AUCUNE = 0,
    BLEU = 1,
    ROUGE = 2,
    NOIR = 3,
    BLANC = 4
}

enum Etape
{
    ACTIVER_BONUS,
    CHOISIR_BONUS,
    UTILISER_BONUS,
    PLACER_MAISON,  
    PIOCHER,
    FIN_TOUR,
    FIN_PARTIE
}

class Constante
{
    public const uint LARGEUR_FENETRE = 800;
    public const uint HAUTEUR_FENETRE = 600;
    public const uint X_PLATEAU_MAX = 20;
    public const uint Y_PLATEAU_MAX = 20;
}