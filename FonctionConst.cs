using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System;

class FonctionConst
{

    /**
    *	fonction IsNear:
    *	determine si un terrain de type t
    *	se trouve à côté d'une coordonnée _case
    */
    public static bool IsNear(Plateau d,Tuple<Coordonnee,Terrain>_case,TypeTerrain t )
    {
	    List<Coordonnee> voisins=d.Adjacent(_case.Item1);
        foreach (Coordonnee voisin in voisins)
			    if(d[voisin].Type==t)
				    return true;
	    return false;
    }

   /* public static List<Tuple<Coordonnee,Terrain>> liste_voisin(Plateau d,Tuple<Coordonnee,Terrain> t)
    {
        List<Tuple<Coordonnee, Terrain>> voisins=new List<Tuple<Coordonnee,Terrain>>();

        Coordonnee c1 = new Coordonnee((byte)(t.Item1.X), (byte)(t.Item1.Y - 1));
        Coordonnee c2 = new Coordonnee((byte)(t.Item1.X + 1), (byte)(t.Item1.Y - 1));
        Coordonnee c3 = new Coordonnee((byte)(t.Item1.X - 1), (byte)(t.Item1.Y));
        Coordonnee c4 = new Coordonnee((byte)(t.Item1.X + 1), (byte)(t.Item1.Y));
        Coordonnee c5 = new Coordonnee((byte)(t.Item1.X), (byte)(t.Item1.Y + 1));
        Coordonnee c6 = new Coordonnee((byte)(t.Item1.X + 1), (byte)(t.Item1.Y + 1));
        if (d.ContainsKey(c1) != false)
        {
            voisins.Add(new Tuple<Coordonnee,Terrain>(c1,d[c1]));
        }
        if (d.ContainsKey(c2) != false)
        {
            voisins.Add(new Tuple<Coordonnee, Terrain>(c2, d[c2]));
        }
        if (d.ContainsKey(c3) != false)
        {
            voisins.Add(new Tuple<Coordonnee, Terrain>(c3, d[c3]));
        }
        if (d.ContainsKey(c4) != false)
        {
            voisins.Add(new Tuple<Coordonnee, Terrain>(c4, d[c4]));
        }
        if (d.ContainsKey(c5) != false)
        {
            voisins.Add(new Tuple<Coordonnee, Terrain>(c5, d[c5]));
        }
        if (d.ContainsKey(c6) != false)
        {
            voisins.Add(new Tuple<Coordonnee, Terrain>(c6, d[c6]));
        }
        return voisins;
    }*/
}