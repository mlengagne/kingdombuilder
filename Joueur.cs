using System.Diagnostics;
using System;

using System.Collections.Generic;

abstract class Joueur
{
    public List<TypeTerrain> m_listeBonus = null;
    public List<Coordonnee> m_maisonsPlacees = null;
    protected Couleur m_couleur;
    protected TypeTerrain m_carteTerrain;
    protected short m_score;
    protected short m_nbMaisons;
    private static Random rnd1 = null;

    protected Joueur(Couleur couleur)
    {
        if (rnd1 == null)
            rnd1 = new Random();

        m_listeBonus = new List<TypeTerrain>();
        m_maisonsPlacees = new List<Coordonnee>();
        m_carteTerrain = Piocher();
        m_couleur = couleur;
        m_score = 0;
        m_nbMaisons = 20;
    }

    public short NbMaisons
    {
        get { return m_nbMaisons; }
        set { m_nbMaisons = value; }
    }

    public Couleur Couleur
    {
        get { return m_couleur; }
    }

    public TypeTerrain CarteTerrain
    {
        get { return m_carteTerrain; }
        set { m_carteTerrain = value; }
    }

    public List<TypeTerrain> ListeBonus
    {
        get { return m_listeBonus; }
        set { m_listeBonus = value; }
    }

    public TypeTerrain Piocher()
    {
        int nbre = rnd1.Next(7);
        while (nbre == 2 || nbre == 5)
        {
            nbre = rnd1.Next(7);
        }

        return (TypeTerrain)nbre;
    }

    public void RecupererBonus(Plateau plateau)
    {
        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
        {
            if (terrain.Value.Couleur == m_couleur)
            {
                TypeTerrain bonus = plateau.RecupererBonus(plateau.Adjacent(terrain.Key));
                if (bonus != TypeTerrain.AUCUN && !m_listeBonus.Contains(bonus))
                {
                    m_listeBonus.Add(bonus);
                }
            }
        }
    }

    public short Score
    {
        get { return m_score; }
        set { m_score = value; }
    }

    public bool PlacerMaison(Coordonnee coordonnee, Plateau plateau)
    {
        // Pour savoir si c'est pas a NULL
        if (coordonnee == null)
            return false;

        // Si la coordonnée existe dans le plateau
        if (!plateau.ContainsKey(coordonnee))
            return false;

        // Si c'est bien un terrain de sa carte terrain
        if (plateau[coordonnee].Type != m_carteTerrain)
            return false;

        // Pour verifier si il y a pas déjà une maison dessus.
        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;

        // Pour savoir si il y a encore des maisons de dispo
        if (m_nbMaisons == 0)
            return false;

        // TEST MAISON ADJACENTE

        List<Coordonnee> m_listeTerrainObligatoire = new List<Coordonnee>();
        List<Coordonnee> m_listeTerrainSecondaire = new List<Coordonnee>();
        List<Coordonnee> m_listeTerrainTernaire = new List<Coordonnee>();

        // On parcours notre liste de maison deja placees et on ajoute a notre liste ternaire les terrains ou il est possible de poser une maison  
        foreach (Coordonnee coord in m_maisonsPlacees)
            m_listeTerrainTernaire.AddRange(plateau.Adjacent(coord));

        for (int i = 0; i < m_listeTerrainTernaire.Count; i++)
        {
            if (plateau[m_listeTerrainTernaire[i]].Type != m_carteTerrain || plateau[m_listeTerrainTernaire[i]].Couleur != Couleur.AUCUNE)
            {
                m_listeTerrainTernaire.RemoveAt(i);
                i--;
            }
        }

        // On parcours tout le plateau a la recherche d'adjacences des maisons déjà posées. Il existera des doublons dans la
        // Collection.
        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
        {
            if (terrain.Value.Couleur == m_couleur && terrain.Value.Type == m_carteTerrain)
                m_listeTerrainObligatoire.AddRange(plateau.Adjacent(terrain.Key));
            if (terrain.Value.Type == m_carteTerrain && terrain.Value.Couleur == Couleur.AUCUNE)
                m_listeTerrainSecondaire.Add(terrain.Key);
        }

        // On parcours notre liste d'adjacence et on retire les terrains qui ne sont pas concernés.
        for (int i = 0; i < m_listeTerrainObligatoire.Count; i++)
        {
            if (plateau[m_listeTerrainObligatoire[i]].Type != m_carteTerrain || plateau[m_listeTerrainObligatoire[i]].Couleur != Couleur.AUCUNE)
            {
                m_listeTerrainObligatoire.RemoveAt(i);
                i--;
            }
        }

        if (m_listeTerrainObligatoire.Count == 0)
        {
            if (m_listeTerrainTernaire.Count == 0)
            {
                foreach (Coordonnee coord in m_listeTerrainSecondaire)
                {
                    if (coord.Equals(coordonnee))
                    {
                        plateau[coordonnee].Couleur = m_couleur;
                        m_maisonsPlacees.Add(coordonnee);
                        m_nbMaisons--;

                        return true;
                    }
                }
            }
            else
            {
                foreach (Coordonnee coord in m_listeTerrainTernaire)
                {
                    if (coord.Equals(coordonnee))
                    {
                        plateau[coordonnee].Couleur = m_couleur;
                        m_maisonsPlacees.Add(coordonnee);
                        m_nbMaisons--;

                        return true;
                    }
                }
            }
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {

            foreach (Coordonnee coord in m_listeTerrainTernaire)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = m_couleur;
                    m_maisonsPlacees.Add(coordonnee);
                    m_nbMaisons--;

                    return true;
                }
            }
            
            foreach (Coordonnee coord in m_listeTerrainObligatoire)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = m_couleur;
                    m_maisonsPlacees.Add(coordonnee);
                    m_nbMaisons--;

                    return true;
                }
            }
        }

        return false;
    }

}