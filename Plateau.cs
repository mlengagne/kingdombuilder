using System.Collections.Generic;
using System.IO;
using System;
using SFML.Graphics;
using SFML.Window;

class Plateau : Dictionary<Coordonnee, Terrain>
{
    public Plateau()
        : base()
    { }

    public void Initialiser()
    {
        List<byte> numeros = NumerosQuadrants();
        byte[,] quadrant1 = new byte[10, 10];
        byte[,] quadrant2 = new byte[10, 10];
        byte[,] quadrant3 = new byte[10, 10];
        byte[,] quadrant4 = new byte[10, 10];

        RemplirQuadrants(numeros, ref quadrant1, ref quadrant2, ref quadrant3, ref quadrant4);
        ReunirQuadrantsEnGrille(ref quadrant1, ref quadrant2, ref quadrant3, ref quadrant4);

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in this)
        {
            terrain.Value.Position = new Vector2f(terrain.Key.Item1 * 128.0f, terrain.Key.Item2 * 128.0f);

            if (terrain.Key.Item2 % 2 == 0)
                terrain.Value.Position -= new Vector2f(0.0f, terrain.Key.Item2 * 32.0f);
            else
                terrain.Value.Position -= new Vector2f(64.0f, terrain.Key.Item2 * 32.0f);

            terrain.Value.SpriteMaison.Position = new Vector2f(terrain.Value.Position.X + 32, terrain.Value.Position.Y + 32);

            terrain.Value.Corps = new FloatRect(terrain.Value.Position.X, terrain.Value.Position.Y, terrain.Value.GetGlobalBounds().Width, terrain.Value.GetGlobalBounds().Height);
        }
    }

    public void Dessiner(RenderWindow fenetre)
    {
        foreach (KeyValuePair<Coordonnee, Terrain> terrain in this)
        {
            fenetre.Draw(terrain.Value);
            fenetre.Draw(terrain.Value.SpriteMaison);
        }
    }

    public static Coordonnee Vector2fVersCoordonnee(Vector2f souris, Plateau plateau)
    {
        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Corps.Contains(souris.X, souris.Y))
                return terrain.Key;

        return null;
    }

    public TypeTerrain RecupererBonus(List<Coordonnee> liste)
    {
        foreach (Coordonnee coordonnee in liste)
        {
            switch (this[coordonnee].Type)
            {
                case TypeTerrain.ORACLE: return TypeTerrain.ORACLE;
                case TypeTerrain.FERME: return TypeTerrain.FERME;
                case TypeTerrain.TAVERNE: return TypeTerrain.TAVERNE;
                case TypeTerrain.TOUR: return TypeTerrain.TOUR;
                case TypeTerrain.PORT: return TypeTerrain.PORT;
                case TypeTerrain.ENCLOS: return TypeTerrain.ENCLOS;
                case TypeTerrain.GRANGE: return TypeTerrain.GRANGE;
                case TypeTerrain.OASIS: return TypeTerrain.OASIS;
                default: continue;
            }
        }

        return TypeTerrain.AUCUN;
    }

    public List<Coordonnee> Adjacent(Coordonnee coordonnee)
    {
        List<Coordonnee> coordonneesAdjacentes = new List<Coordonnee>();

        if (coordonnee.Item2 % 2 != 0) //PAIR
            return AdjacentPair(coordonnee, coordonneesAdjacentes);
        else //IMPAIR
            return AdjacentImpair(coordonnee, coordonneesAdjacentes);
    }

    private List<Coordonnee> AdjacentPair(Coordonnee coordonnee, List<Coordonnee> coordonneesAdjacentes)
    {
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X - 1, coordonnee.Y - 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X, coordonnee.Y - 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X - 1, coordonnee.Y));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X + 1, coordonnee.Y));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X - 1, coordonnee.Y + 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X, coordonnee.Y + 1));

        for (int i = 0; i < coordonneesAdjacentes.Count; i++)
        {
            if (!this.ContainsKey(coordonneesAdjacentes[i]))
            {
                coordonneesAdjacentes.RemoveAt(i);
                i--;
            }
        }

        return coordonneesAdjacentes;
    }

    private List<Coordonnee> AdjacentImpair(Coordonnee coordonnee, List<Coordonnee> coordonneesAdjacentes)
    {
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X, coordonnee.Y - 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X + 1, coordonnee.Y - 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X - 1, coordonnee.Y));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X + 1, coordonnee.Y));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X, coordonnee.Y + 1));
        coordonneesAdjacentes.Add(new Coordonnee(coordonnee.X + 1, coordonnee.Y + 1));

        for (int i = 0; i < coordonneesAdjacentes.Count; i++)
        {
            if (!this.ContainsKey(coordonneesAdjacentes[i]))
            {
                coordonneesAdjacentes.RemoveAt(i);
                i--;
            }
        }

        return coordonneesAdjacentes;
    }

    private List<byte> NumerosQuadrants()
    {
        List<byte> bytes = new List<byte>(4);
        Random random = new Random();

        for (byte i = 0; i < 4; i++)
        {
            bytes.Add(TirerNumero(bytes, random));
        }

        return bytes;
    }

    private byte TirerNumero(List<byte> bytes, Random random)
    {
        byte rand;

        do
        {
            rand = (byte)(random.Next(8) + 1);
        } while (NumeroExisteDeja(bytes, rand));

        return rand;
    }

    private bool NumeroExisteDeja(List<byte> bytes, byte b)
    {
        for (byte i = 0; i < bytes.Count; i++)
            if (bytes[i] == b)
                return true;
        return false;
    }

    private void RemplirQuadrants(List<byte> numeros, ref byte[,] quadrant1, ref byte[,] quadrant2, ref byte[,] quadrant3, ref byte[,] quadrant4)
    {
        FileStream fichier = null;
        StreamReader lecteur = null;
        string chemin = null;

        //for (byte i = 0; i < numeros.Count; i++)
        for (byte i = 0; i < 4 ; i++)
        {
            chemin = "data/quadrant" + (i + 1).ToString() + ".data";
            fichier = File.Open(chemin, FileMode.Open);
            lecteur = new StreamReader(fichier);

            switch (i)
            {
                case 0: quadrant1 = LireFichier(lecteur); break;
                case 1: quadrant2 = LireFichier(lecteur); break;
                case 2: quadrant3 = LireFichier(lecteur); break;
                case 3: quadrant4 = LireFichier(lecteur); break;
            }
            fichier.Close();
        }
    }

    private byte[,] LireFichier(StreamReader lecteur)
    {
        byte[,] quadrant = new byte[10, 10];
        string contenu = lecteur.ReadToEnd();
        string[] contenu_splite = contenu.Split(' ');
        byte cpt = 0;

        for (byte y = 0; y < 10; y++)
            for (byte x = 0; x < 10; x++, cpt++)
                quadrant[x, y] = Byte.Parse(contenu_splite[cpt]);

        lecteur.Close();
        return quadrant;
    }

    private void ReunirQuadrantsEnGrille(ref byte[,] quadrant1, ref byte[,] quadrant2, ref byte[,] quadrant3, ref byte[,] quadrant4)
    {
        RemplirPartie(0, 10, 0, 10, ref quadrant1);
        RemplirPartie(10, 20, 0, 10, ref quadrant2);
        RemplirPartie(0, 10, 10, 20, ref quadrant3);
        RemplirPartie(10, 20, 10, 20, ref quadrant4);
    }

    private void RemplirPartie(byte inf1, byte sup1, byte inf2, byte sup2, ref byte[,] quadrant)
    {
        byte x = 0;
        byte y = 0;
        for (byte i = inf1; i < sup1; i++)
        {
            for (byte j = inf2; j < sup2; j++)
            {
                this.Add(new Coordonnee((byte)(i + 1), (byte)(j + 1)), new Terrain((TypeTerrain)Enum.ToObject(typeof(TypeTerrain), quadrant[x, y])));
                y++;
            }
            x++;
            y = 0;
        }
    }

    public List<Terrain> PositionSouris(Vector2f v)
    {
        List<Terrain> l_t = new List<Terrain>();
        foreach (KeyValuePair<Coordonnee, Terrain> _case in this)
        {
            if (_case.Value.GetGlobalBounds().Contains(v.X, v.Y))
                l_t.Add(_case.Value);
        }
        return l_t;
    }

    public override string ToString()
    {
        string str = "";

        foreach (KeyValuePair<Coordonnee, Terrain> paire in this)
            str += "Coordonnée : " + paire.Key + " => " + paire.Value.ToString() + "\n";

        return str;
    }
}
