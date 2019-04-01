using System.Collections.Generic;
using System;

/// <summary>
/// Cette classe possède une methode pour chaque bonus du jeu. Leurs noms correspond à leur appelation dans le jeu de plateau.
/// </summary>
static class Bonus
{
    #region CONSTRUIRE

    /// <summary>
    /// Construit un batiment sur une case Terrain adjacente au type de la carte Terrain du joueur.
    /// </summary>
    /// <param name="coordonnee">Coordonnee du terrain à construire la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Oracle(Coordonnee coordonnee, Joueur joueur, Plateau plateau)
    {
        if (coordonnee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.ORACLE))
            return false;

        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;

        if (plateau[coordonnee].Type != joueur.CarteTerrain)
            return false;

        // Recuperation de la liste des terrains adjacents du joueur
        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (plateau[m_listeTerrainPosable[i]].Type != joueur.CarteTerrain)
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }

        // Si aucun terrain n'est disponible (adjacent)
        if (m_listeTerrainPosable.Count == 0)
        {

            plateau[coordonnee].Couleur = joueur.Couleur;
            joueur.NbMaisons--;
            joueur.m_maisonsPlacees.Add(coordonnee);
            return true;
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {
            foreach (Coordonnee coord in m_listeTerrainPosable)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    joueur.NbMaisons--;
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Construit un batiment sur une case Terrain adjacente de type prairie.
    /// </summary>
    /// <param name="coordonnee">Coordonnee du terrain à construire la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Ferme(Coordonnee coordonnee, Joueur joueur, Plateau plateau)
    {
        if (coordonnee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.FERME))
            return false;

        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;

        if (plateau[coordonnee].Type != TypeTerrain.PRAIRIE)
            return false;

        // Recuperation de la liste des terrains adjacents du joueur
        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (plateau[m_listeTerrainPosable[i]].Type != TypeTerrain.PRAIRIE)
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }
        // Si aucun terrain n'est disponible (adjacent)
        if (m_listeTerrainPosable.Count == 0)
        {

            plateau[coordonnee].Couleur = joueur.Couleur;
            joueur.NbMaisons--;
            joueur.m_maisonsPlacees.Add(coordonnee);
            return true;
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {
            foreach (Coordonnee coord in m_listeTerrainPosable)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    joueur.NbMaisons--;
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Construit un batiment sur une case Terrain adjacente de type desert.
    /// </summary>
    /// <param name="coordonnee">Coordonnee du terrain à construire la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Oasis(Coordonnee coordonnee, Joueur joueur, Plateau plateau)
    {
        if (coordonnee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.OASIS))
            return false;

        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;

        if (plateau[coordonnee].Type != TypeTerrain.DESERT)
            return false;

        // Recuperation de la liste des terrains adjacents du joueur
        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (plateau[m_listeTerrainPosable[i]].Type != TypeTerrain.DESERT)
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }
        // Si aucun terrain n'est disponible (adjacent)
        if (m_listeTerrainPosable.Count == 0)
        {

            plateau[coordonnee].Couleur = joueur.Couleur;
            joueur.NbMaisons--;
            joueur.m_maisonsPlacees.Add(coordonnee);
            return true;
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {
            foreach (Coordonnee coord in m_listeTerrainPosable)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    joueur.NbMaisons--;
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Construit un batiment sur une case constructible quelquonque de la bordure du plateau du jeu, adjacent si possible.
    /// </summary>
    /// <param name="coordonnee">Coordonnee du terrain à construire la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Tour(Coordonnee coordonnee, Joueur joueur, Plateau plateau)
    {
        if (coordonnee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.TOUR))
            return false;

        if (!(coordonnee.X == 1 || coordonnee.X == 20 || coordonnee.Y == 1 || coordonnee.Y == 20))
            return false;

        if (plateau[coordonnee].Type != TypeTerrain.PRAIRIE && plateau[coordonnee].Type != TypeTerrain.FORET &&
        plateau[coordonnee].Type != TypeTerrain.FLEUR && plateau[coordonnee].Type != TypeTerrain.DESERT &&
        plateau[coordonnee].Type != TypeTerrain.CANYON)
            return false;

        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;
        // Recuperation de la liste des terrains adjacents du joueur
        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (!(m_listeTerrainPosable[i].X == 1 || m_listeTerrainPosable[i].X == 20 || m_listeTerrainPosable[i].Y == 1 || m_listeTerrainPosable[i].Y == 20))
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }

        // Si aucun terrain n'est disponible (adjacent)
        if (m_listeTerrainPosable.Count == 0)
        {
            plateau[coordonnee].Couleur = joueur.Couleur;
            joueur.NbMaisons--;
            joueur.m_maisonsPlacees.Add(coordonnee);
            return true;
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {
            foreach (Coordonnee coord in m_listeTerrainPosable)
            {
                if (coord.Equals(coordonnee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    joueur.NbMaisons--;
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Construit un batiment au bout d'une ligne droite d'au moins 3 batiments alignés de la même couleur, dans n'importe quelle directions.
    /// </summary>
    /// <remarks>Seul le balayage horizontale est implémenté.</remarks>
    /// <param name="coordonnee">Coordonnee du terrain à construire la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Taverne(Coordonnee coordonnee, Joueur joueur, Plateau plateau)
    {
        // Verification que le clic est bien réalisé à l'interieur du plateau.
        if (coordonnee == null)
            return false;

        // Verification que le joueur possède bien le bonus.
        if (!joueur.ListeBonus.Contains(TypeTerrain.TAVERNE))
            return false;

        // Verification que le joeur a cliqué sur un terrain constructible.
        if (plateau[coordonnee].Type != TypeTerrain.PRAIRIE && plateau[coordonnee].Type != TypeTerrain.FORET &&
        plateau[coordonnee].Type != TypeTerrain.FLEUR && plateau[coordonnee].Type != TypeTerrain.DESERT &&
        plateau[coordonnee].Type != TypeTerrain.CANYON)
            return false;

        // Verification que la case est inoccupée.
        if (plateau[coordonnee].Couleur != Couleur.AUCUNE)
            return false;

        // posable est une Liste qui possède les coordonnées éligible d'utilisation du bonus.
        List<Coordonnee> posables = new List<Coordonnee>();

        // Parcours de toute les cases du plateau. On verifie par 3 balayages : 1 sur l'axe des x, et deux en diagonale.

        // Balayage en X (gauche -> droite)
        posables.AddRange(BalayageHorizontale(plateau, joueur));

        // Balayage en diagonale (gauche -> droite)

        // Balayage en diagonale (droite -> gauche)
        foreach (Coordonnee coord in posables)
        {
            if (coord.Equals(coordonnee))
            {
                plateau[coordonnee].Couleur = joueur.Couleur;
                joueur.NbMaisons--;
                joueur.m_maisonsPlacees.Add(coordonnee);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Balayage horizontal du plateau concernant l'algorithme du bonus "TAVERNE".
    /// </summary>
    /// <param name="plateau">Plateau du jeu</param>
    /// <param name="joueur">Joueur qui utilise le bonus</param>
    /// <returns>Liste de coordonnees éligibles du bonus TAVERNE</returns>
    private static List<Coordonnee> BalayageHorizontale(Plateau plateau, Joueur joueur)
    {
        // On crée la liste des coordonnées posables. C'est cette liste qu'on retournera à la fin.
        List<Coordonnee> terrainPosable = new List<Coordonnee>();

        // On crée une coordonnée origine, pour se souvenir de la première extrémité d'une suite de maison.
        Coordonnee coordOrig = new Coordonnee(-1, -1);

        // Compteur
        int compteurMaison = 0;

        // On parcours chaque lignes du plateau de gauche à droite.
        for (int y = 1; y <= Constante.Y_PLATEAU_MAX; y++)
        {
            for (int x = 1; x <= Constante.X_PLATEAU_MAX; x++)
            {
                Coordonnee coordCourante = new Coordonnee(x, y);
                // Si on trouve une maison du joueur, on teste si ses voisins de droite sont aussi des maisons du joueurs.
                // Au cas où une ligne de 3 maisons ou plus est detectée, les deux extrémitées, si elles sont vide, sont a ajouter a la Liste des terrain posables.

                // Si notre coordOrig est vide
                if (coordOrig.Item1 == -1 && coordOrig.Item2 == -1)
                {
                    // Si on trouve une maison du joueur
                    if (plateau[coordCourante].Couleur == joueur.Couleur)
                    {
                        // On remplit la coordOrig de la coordonnée précédente : il s'agit de l'extrémité d'origine
                        coordOrig = new Coordonnee(coordCourante.Item1 - 1, coordCourante.Item2);
                        // Nous avons une premiere maison dans l'alignement
                        compteurMaison++;
                    }
                }
                else // Si notre coordOrig n'est pas vide
                {
                    // Si on tombe sur une coordonnée où ya pas de maison du joueur
                    if (plateau[coordCourante].Couleur != joueur.Couleur)
                    {
                        // On regarde si on a un compteur supérieure ou égale a 3 maisons alignés
                        if (compteurMaison >= 3)
                        {
                            // On ajoute la coordonnée de l'extrémité d'origine et la coordonnée courante (qui est donc l'extrémité de destination) dans la liste
                            if (plateau.ContainsKey(coordOrig) && plateau[coordOrig].Couleur == Couleur.AUCUNE)
                                terrainPosable.Add(coordOrig);
                            if (plateau.ContainsKey(coordOrig) && plateau[coordCourante].Couleur == Couleur.AUCUNE)
                                terrainPosable.Add(coordCourante);
                        }
                        coordOrig = new Coordonnee(-1, -1);
                        compteurMaison = 0;
                    }
                    else // Si on tombe sur une coordonnée occupée par une maison du joueur
                    {
                        // Une maison supplémentaire dans l'alignement
                        compteurMaison++;
                    }
                }
            }
        }

        return terrainPosable;
    }

    #endregion
    #region DEPLACER

    /// <summary>
    /// Deplace un batiment sur une case Terrain adjacente au type de la carte Terrain du joueur. 
    /// </summary>
    /// <param name="coordonneeDepart">Coordonnées de la maison à déplacer</param>
    /// <param name="coordonneeArrivee">Coordonnées de la case où déplacer la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Grange(Coordonnee coordonneeDepart, Coordonnee coordonneeArrivee, Joueur joueur, Plateau plateau)
    {
        if (coordonneeDepart == null || coordonneeArrivee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.GRANGE))
            return false;

        // Est ce bien une maison au joueur que l'on va déplacer ?
        if (plateau[coordonneeDepart].Couleur != joueur.Couleur)
            return false;

        // Est ce bien une case (destination) qui correspond a la carte terrain du joueur ? 
        if (plateau[coordonneeArrivee].Type != joueur.CarteTerrain)
            return false;

        // La case a t'elle déjà une maison de posée ?
        if (plateau[coordonneeArrivee].Couleur != Couleur.AUCUNE)
            return false;

        // Recuperation de la liste des terrains adjacents du joueur
        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (plateau[m_listeTerrainPosable[i]].Type != joueur.CarteTerrain)
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }

        // Si aucun terrain n'est disponible (adjacent)
        if (m_listeTerrainPosable.Count == 0)
        {
            plateau[coordonneeArrivee].Couleur = joueur.Couleur;
            plateau[coordonneeDepart].Couleur = Couleur.AUCUNE;
            joueur.m_maisonsPlacees.Remove(coordonneeDepart);
            joueur.m_maisonsPlacees.Add(coordonneeArrivee);
            return true;
        }
        else // sinon on voit si la coordonnee de destination correspond a un des terrains adjacents
        {
            foreach (Coordonnee coordonnee in m_listeTerrainPosable)
            {
                if (coordonnee.Equals(coordonneeArrivee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    plateau[coordonneeDepart].Couleur = Couleur.AUCUNE;
                    joueur.m_maisonsPlacees.Remove(coordonneeDepart);
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Déplace 1 batiment sur une case Terrain de type eau, Adjacent si possible.
    /// </summary>
    /// <param name="coordonneeDepart">Coordonnées de la maison à déplacer</param>
    /// <param name="coordonneeArrivee">Coordonnées de la case où déplacer la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Port(Coordonnee coordonneeDepart, Coordonnee coordonneeArrivee, Joueur joueur, Plateau plateau)
    {
        if (coordonneeDepart == null || coordonneeArrivee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.PORT))
            return false;

        if (plateau[coordonneeDepart].Couleur != joueur.Couleur)
            return false;

        if (plateau[coordonneeArrivee].Type != TypeTerrain.EAU)
            return false;

        if (plateau[coordonneeArrivee].Couleur != Couleur.AUCUNE)
            return false;

        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        foreach (KeyValuePair<Coordonnee, Terrain> terrain in plateau)
            if (terrain.Value.Couleur == joueur.Couleur)
                m_listeTerrainPosable.AddRange(plateau.Adjacent(terrain.Key));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (plateau[m_listeTerrainPosable[i]].Type != TypeTerrain.EAU)
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }

        if (m_listeTerrainPosable.Count == 0)
        {
            plateau[coordonneeArrivee].Couleur = joueur.Couleur;
            plateau[coordonneeDepart].Couleur = Couleur.AUCUNE;
            joueur.m_maisonsPlacees.Remove(coordonneeDepart);
            joueur.m_maisonsPlacees.Add(coordonneeArrivee);
            return true;
        }
        else
        {
            foreach (Coordonnee coordonnee in m_listeTerrainPosable)
            {
                if (coordonnee.Equals(coordonneeArrivee))
                {
                    plateau[coordonnee].Couleur = joueur.Couleur;
                    plateau[coordonneeDepart].Couleur = Couleur.AUCUNE;
                    joueur.m_maisonsPlacees.Remove(coordonneeDepart);
                    joueur.m_maisonsPlacees.Add(coordonnee);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Deplace un batiment de deux cases en ligne droite dans n'importe quelle direction, vers une case libre constructible. La case sautée peu être de n'importe quel type. La case d'arrivée ne doit pas obligatoirement être adjacente à un des batiments du joueur.
    /// </summary>
    /// <param name="coordonneeDepart">Coordonnées de la maison à déplacer</param>
    /// <param name="coordonneeArrivee">Coordonnées de la case où déplacer la maison</param>
    /// <param name="joueur">Joueur qui lance le bonus</param>
    /// <param name="plateau">Plateau de jeu</param>
    /// <returns>True si bonus bien activé, false sinon.</returns>
    public static bool Enclos(Coordonnee coordonneeDepart, Coordonnee coordonneeArrivee, Joueur joueur, Plateau plateau)
    {
        if (coordonneeDepart == null || coordonneeArrivee == null)
            return false;

        if (!joueur.ListeBonus.Contains(TypeTerrain.ENCLOS))
            return false;

        if (plateau[coordonneeDepart].Couleur != joueur.Couleur)
            return false;

        if (plateau[coordonneeArrivee].Type != TypeTerrain.PRAIRIE && plateau[coordonneeArrivee].Type != TypeTerrain.FORET &&
    plateau[coordonneeArrivee].Type != TypeTerrain.FLEUR && plateau[coordonneeArrivee].Type != TypeTerrain.DESERT &&
    plateau[coordonneeArrivee].Type != TypeTerrain.CANYON)
            return false;

        if (plateau[coordonneeArrivee].Couleur != Couleur.AUCUNE)
            return false;

        List<Coordonnee> m_listeTerrainPosable = new List<Coordonnee>();

        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X + 2, coordonneeDepart.Y));
        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X - 2, coordonneeDepart.Y));
        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X - 1, coordonneeDepart.Y - 2));
        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X + 1, coordonneeDepart.Y - 2));
        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X - 1, coordonneeDepart.Y + 2));
        m_listeTerrainPosable.Add(new Coordonnee(coordonneeDepart.X + 1, coordonneeDepart.Y + 2));

        for (int i = 0; i < m_listeTerrainPosable.Count; i++)
        {
            if (!plateau.ContainsKey(m_listeTerrainPosable[i]))
            {
                m_listeTerrainPosable.RemoveAt(i);
                i--;
            }
        }

        foreach (Coordonnee coordcourante in m_listeTerrainPosable)
        {
            if (coordcourante.Equals(coordonneeArrivee))
            {
                plateau[coordonneeArrivee].Couleur = joueur.Couleur;
                plateau[coordonneeDepart].Couleur = Couleur.AUCUNE;
                joueur.m_maisonsPlacees.Remove(coordonneeDepart);
                joueur.m_maisonsPlacees.Add(coordonneeArrivee);
                return true;
            }
        }

        return false;

    }

    #endregion
}