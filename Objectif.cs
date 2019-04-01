using System.Collections.Generic;
using System;
static class Objectif
{
	/**
	*	Algorithme de l'explorateur:
	*	1 point par ligne horizontale 
	*	qui poss�de une maison de couleur c
	*/
	public static int Explorateur(Plateau p, Couleur c)
	{
		int score=0,num_case=1;
		bool trouver_maison=false;
		for(byte i=1;i<21;i++)
		{
			while(!trouver_maison && num_case<21)
			{
				if(p[new Coordonnee(num_case,i)].Couleur==c)
				{
                    trouver_maison=true;
                    score++;
                }
				else
					num_case++;
			}
            trouver_maison = false;
            num_case = 1;
		}
        return score;
	}
	
	/**
	*	Algorithme de l'ouvrier:
	*	1 point par maison de couleur c 
	*	� c�t� d'un lieu sp�cial
	*	de coordonne� co (chateau, case bonus)
	*	
	*	la m�thode ne teste qu'un seul lieu,
	*	elle est appel�e dans une boucle qui teste 
	*	tous les lieux sp�ciaux
	*/
    public static int Ouvrier(Plateau p, Tuple<Coordonnee, Terrain> co, Couleur c)
	{
		int score=0;
        List<Coordonnee> voisins = p.Adjacent(co.Item1);
        foreach (Coordonnee voisin in voisins)
        {
            if (p[voisin].Couleur == c)
                score++;
        }
		//score obtenu par le joueur autour de ce lieu
		return score;
	}
	
	/**
	*	Algorithme du chevalier:
	*	2 points par maison pour la ligne horizontale
	*	poss�dant le plus de maison de couleur c
	*/
    public static int Chevalier(Plateau p, Couleur c)
	{
		int max_m=0,nb_m_ligne=0;
		for(byte i=1;i<21;i++)
		{
			for(byte j=1;j<21;j++)
			{
                if (p[new Coordonnee(j,i)].Couleur == c)
					nb_m_ligne++;
			}
            max_m = Math.Max(nb_m_ligne, max_m);
			nb_m_ligne=0;
		}
		return (max_m*2);
	}
	
	
	
	/**
		Algorithme du mineur:
		1 point par maison � c�t�
		d'au moins une case montagne
	*/
    public static int Mineur(Plateau p, List<Coordonnee> maisons)
	{
		int score=0;
        List<Tuple<Coordonnee,Terrain>> tmp=new List<Tuple<Coordonnee,Terrain>>();

        foreach (Coordonnee maison in maisons)
        {
            Tuple<Coordonnee,Terrain> tmpMaison=new Tuple<Coordonnee,Terrain>(maison,p[maison]);
            tmp.Add(tmpMaison);
        }
        foreach (Tuple<Coordonnee,Terrain> t in tmp)
		{
			if(FonctionConst.IsNear(p,t,TypeTerrain.MONTAGNE))
            {
				score++;
            }
		}
		return score;
	}
	
	/**
		Algorithme du pecheur:
		1 point par maison � c�t�
		d'au moins une case eau
	*/
    public static int Pecheur(Plateau p, List<Coordonnee> maisons)
	{
		int score=0;
        List<Tuple<Coordonnee, Terrain>> tmp = new List<Tuple<Coordonnee, Terrain>>();

        foreach (Coordonnee maison in maisons)
        {
            Tuple<Coordonnee, Terrain> tmpMaison = new Tuple<Coordonnee, Terrain>(maison, p[maison]);
            tmp.Add(tmpMaison);
        }
        foreach (Tuple<Coordonnee, Terrain> maison in tmp)
		{
            if (FonctionConst.IsNear(p, maison, TypeTerrain.EAU))
            {
                score++;
            }
		}
		return score;
	}

    /*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="_case"></param>
    /// <param name="speciales"></param>
    /// <returns></returns>
    public static Coordonnee Commer�ant(Plateau p,Coordonnee _case,List<KeyValuePair<Coordonnee,Terrain>> speciales)
    {
        List<Coordonnee> voisins = p.Adjacent(_case);
        List<Coordonnee> tmpVoisins = voisins;
        Coordonnee c;
        foreach (Coordonnee v in voisins)
        {
            if (p[v].Couleur == Couleur.AUCUNE)
            {
                tmpVoisins.Remove(v);
            }else if(v.Lvl == _case.Lvl || v.Lvl==(_case.Lvl-1))
            {
                tmpVoisins.Remove(v);
            }
            else
                v.Lvl=(_case.Lvl+1);
        }
        if(tmpVoisins.Count==0)
        {
            return null;
        }
        else
        {
            foreach(Coordonnee v in tmpVoisins)
            {
                KeyValuePair<Coordonnee,Terrain> case_voisin=new KeyValuePair<Coordonnee,Terrain>(v,p[v]);
                if(speciales.Contains(case_voisin))
                {
                    return v;
                }
                else
                {
                    Coordonnee coord=new Coordonnee(v.Item1,v.Item2);
                    c=Commer�ant(p,v,speciales);
                    if (c == null && c == voisins[voisins.Count - 1])
                    {
                        return null;
                    }
                    if (c == v)
                    {
                        return v;
                    }
                }
            }
            return null;
        }
    }

    public static int Marchand(Plateau p, List<KeyValuePair<Coordonnee, Terrain>> speciales)
    {
        int score = 0;
        List<KeyValuePair<Coordonnee, Terrain>> casesSpeciales = new List<KeyValuePair<Coordonnee, Terrain>>(speciales);
        foreach (KeyValuePair<Coordonnee, Terrain> _case in speciales)
        {
            Coordonnee coord = Commer�ant(p, _case.Key, speciales);
            if (coord != null && casesSpeciales.Contains(_case))
            {
                score += 4;
                casesSpeciales.Remove(_case);
            }
        }
        return score;
    }*/
}