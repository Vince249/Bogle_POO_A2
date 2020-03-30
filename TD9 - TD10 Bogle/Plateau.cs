using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD9___TD10_Bogle
{
    //public pour test unitaire
    /// <summary>
    /// classe définissant le plateau sur lequel une manche du jeu va se dérouler 
    /// </summary>
    public class Plateau 
    {
        //champs
        private Dé[,] matricePlateau;

        //constructeur
        /// <summary>
        /// la plateau est crée à partir d'un fichier, "Des.txt"
        /// </summary>
        /// <param name="filename"> nom du fichier </param>
        public Plateau(string filename)
        {
            matricePlateau = new Dé[4, 4];

            StreamReader flux = null;
            string line = "";
            string[] ensembleLettreDé = new string[16]; // il y a 16 dés
            int i = 0;
            int j = 0;

            //boucle try/catch pour lire le fichier
            try
            {
                flux = new StreamReader(filename);
                while((line = flux.ReadLine()) != null) //on lit le fichier tant que l'on n'est pas à la fin
                {
                    ensembleLettreDé[i] = line; //pareil que dans le constructeur de dictionnaire --> on "télécharge" 
                                                //le fichier dans un tableau que l'on peut manipuler
                    i++;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (flux != null)
                {
                    flux.Close(); // le flux devrait être fermé mais on le ferme au cas où il ne le soit pas
                }
            }

            char[][] tableauLettrePossiblePourDés = new char[16][]; //car 16 dés
            for (i = 0; i<ensembleLettreDé.Length; i++)
            {
                tableauLettrePossiblePourDés[i] = new char[6]; // car on a 6 faces par dé
                int positionLettre = 0;

                for (j = 0; positionLettre < 6 ; j++)
                {
                    if (char.IsLetter(ensembleLettreDé[i][j]) == true) // si le caractère de l'index de "ensembleLettreDé" est une lettre
                    {
                        tableauLettrePossiblePourDés[i][positionLettre] = ensembleLettreDé[i][j];
                        // on extrait le caractère (qui est une lettre) dans un autre tableau
                        positionLettre++;
                    }
                }
            }
            //on a le tableau contenant que les lettres --> on n'a pas utilisé le "Split" car cela posait des restictions au niveau du type de tableau


            for (i = 0; i < matricePlateau.GetLength(0); i++)
            {
                for (j = 0; j < matricePlateau.GetLength(1); j++)
                {
                    this.matricePlateau[i, j] = new Dé(tableauLettrePossiblePourDés[i]); 
                    // a chaque position de matricePlateau, on a un dé --> on a donc les 16 dés répartis sur chaque position de matricePlateau
                }
            }
            // a ce stade on a donc une matrice pour laquelle chaque position contient un dé
        }

        //propriétés
        /// <summary>
        /// pour pouvoir accéder à la matrice décrivant en réalité le plateau
        /// </summary>
        public Dé[,] MatricePlateau
        {
            get
            {
                return this.matricePlateau;
            }
        }

        //méthodes
        /// <summary>
        /// pour afficher le plateau dans son intégralité
        /// </summary>
        /// <returns> on retourne une matrice 4x4 affichant toutes les lettres contenues sur le plateau </returns>
        public string ToooooString()
        {
            string reponse = "";
            for (int i = 0; i < matricePlateau.GetLength(0); i++)
            {
                for (int j = 0; j < matricePlateau.GetLength(1); j++)
                {
                    reponse = reponse + matricePlateau[i, j].Lettre + " ";
                }
                reponse = reponse + "\n";
            }
            return reponse;
        }

        /// <summary>
        /// pour lancé les dés contenues sur chaque case du plateau (chaque case est composé d'un dé)
        /// </summary>
        public void LancéDéSurPlateau()
        {
            Random generator = new Random();
            for (int i = 0; i < this.matricePlateau.GetLength(0); i++)
            {
                for (int j = 0; j < this.matricePlateau.GetLength(1); j++)
                {
                    this.matricePlateau[i, j].Lance(generator); // on lance le dé
                }
            }
            //on a donc la matrice du plateau final avec des lettres aléatoires
        }
        
        /// <summary>
        /// pour savoir si le mot se trouve sur le plateau en respectant les règles d'adjacence
        /// </summary>
        /// <param name="mot"> mot saisi par un joueur </param>
        /// <returns> on retourne vrai si le mot est sur le plateau et respecte les règles d'adjacense ou faux sinon </returns>
        public bool Test_Plateau(char[] mot)
        {
            //initialisation des variables
            bool reponse = false;
            int indexMot = 0;
            int ligneLettre = 0;
            int colonneLettre = 0;
            int ligneLettreAdjacente = 0;
            int colonneLettreAdjacente = 0;
            int memoLigne = 0;
            int memoColonne = 0;
            int valeurDeColonneLettreAdjacente = 0;
            int compteur = 0;

            char[,] marquerPassageMot = new char[4, 4]; //on crée une matrice pour marquer le passage du mot dans le plateau
                                                        //nous allons donc pouvoir manipuler celle-ci sans problèmes
            for (int i = 0; i < matricePlateau.GetLength(0); i++)
            {
                for (int j = 0; j < matricePlateau.GetLength(1); j++)
                {
                    marquerPassageMot[i, j] = matricePlateau[i, j].Lettre;
                    //on initialise "marquerPassageMot" avec les mêmes valeurs que "matricePlateau"
                }
            }


            //détection du mot
            for (int i = 0; i < matricePlateau.GetLength(0) && reponse == false; i++) // on s'arrête si on a trouvé le mot ou si on arrive a la derniere ligne
            {
                ligneLettre = i;

                for (int j = 0; j < matricePlateau.GetLength(1) && reponse == false; j++) // même idée pour les colonnes
                {
                    colonneLettre = j;

                    if (matricePlateau[i,j].Lettre == mot[indexMot]) //si on réussi à avoir la premiere lettre
                    {

                        while (reponse == false && indexMot >= 0)
                        //boucle pour revenir sur la lettre précédente 
                        //--> tant que reponse fausse ou que l'index recherché existe (est supérieur à 0)
                        {
                            
                            while (reponse == false && compteur < 9)
                            //on continue jusqu'à ce qu'on arrive à la fin du mot ou 
                            //jusqu'à ce que ce compteur arrive à 8 (nombre max de case à regarder autour d'une cellule = 8+1 = 9 car premiere cellule compteur = 0)
                            {

                                //on parcourt la matrice des lettres adjacentes à la case où l'on se trouve
                                for (ligneLettreAdjacente = memoLigne; 
                                ligneLettreAdjacente < LettresAdjacentes(ligneLettre, colonneLettre, marquerPassageMot).GetLength(0) && indexMot < mot.Length - 1; 
                                ligneLettreAdjacente++)
                                {
                                    //valeur de d'initialisation de "colonneLettreAdjacente" dans le for
                                    if (ligneLettreAdjacente == memoLigne)
                                    {
                                        valeurDeColonneLettreAdjacente = memoColonne; 
                                        //utile lorsque l'on va reboucler après (quand on s'est trompé de chemin)
                                    }
                                    else
                                    {
                                        valeurDeColonneLettreAdjacente = 0;
                                    }

                                    for (colonneLettreAdjacente = valeurDeColonneLettreAdjacente; 
                                    colonneLettreAdjacente < LettresAdjacentes(ligneLettre, colonneLettre, marquerPassageMot).GetLength(1) && indexMot < mot.Length - 1;
                                    colonneLettreAdjacente++)
                                    {

                                        if (LettresAdjacentes(ligneLettre, colonneLettre, marquerPassageMot)[ligneLettreAdjacente, colonneLettreAdjacente]
                                        == mot[indexMot + 1])
                                        //si on trouve la lettre suivante du mot parmi les lettres adjacentes
                                        {
                                            if (indexMot + 1 == mot.Length - 1) //si on arrive à la fin du mot c'est que le mot est sur le plateau
                                            {
                                                reponse = true;
                                            }
                                            else
                                            {
                                                marquerPassageMot[ligneLettre, colonneLettre] = '*';
                                                //on enlève la lettre de la cellule où l'on est pour éviter que l'on "repasse" desssus (règle du jeu)


                                                //on passe à la lettre suivante dans la matrice

                                                if (ligneLettreAdjacente == 0) 
                                                //si la prochaine lettre du mot se trouve sur la ligne 0 de "LettreAdjacentes", alors on ira se positionner
                                                //une ligne au dessus (donc une ligne de moins) que celle où l'on est actuellement
                                                {
                                                    ligneLettre--;
                                                }
                                                if (ligneLettreAdjacente == 2) // même principe
                                                {
                                                    ligneLettre++;
                                                }
                                                if (colonneLettreAdjacente == 0)
                                                {
                                                    colonneLettre--;
                                                }
                                                if (colonneLettreAdjacente == 2)
                                                {
                                                    colonneLettre++;
                                                }
                                                //on passe à la lettre suivante dans le mot
                                                indexMot++;

                                                //on note les positions de "ligneLettreAdjacente" et "colonneLettreAdjacente" pour pouvoir s'en resservir
                                                memoLigne = ligneLettreAdjacente;
                                                memoColonne = colonneLettreAdjacente;

                                                //on les remet à leur valeur initial pour que lorsque l'on va changer de lettre, 
                                                //on regarde bien TOUTES les cellules adjacentes
                                                ligneLettreAdjacente = 0;
                                                colonneLettreAdjacente = -1; //car la boucle "for" va l'incrémenter donc il sera égale à 0 lorsqu'on va recommencer l'itération
                                                compteur = 0; //on remet à 0 car on va changer de case
                                            }
                                        }
                                        else
                                        {
                                            compteur++; //pour pouvoir sortir du "while" à un moment
                                        }
                                    }
                                }
                            }

                            if (reponse == false) //si on n'arrive pas à la fin du mot
                            {
                                //on a emprunté un mauvais chemin, il faut retourner en arrière

                                indexMot--; //on va se remettre à chercher la lettre de l'index précédent du mot

                                //on remet la lettre car en revenant en arrière, on pourra être amené à l'utiliser plus tard
                                marquerPassageMot[ligneLettre, colonneLettre] = matricePlateau[ligneLettre, colonneLettre].Lettre; 

                                compteur = 0; //on remet le compteur à 0 si on re-rentre dans la double boucle "for"

                                //on revient sur la position précédente --> même systeme que pour passer à la lettre suivante mais à l'inverse du coup
                                //c'est ici que l'on réutilise les valeurs de "ligneLettreAdjacente" et "colonneLettreAdjacente" que l'on a mémorisé juste avant
                                if (memoLigne == 0)
                                {
                                    ligneLettre++;
                                }
                                if (memoLigne == 2)
                                {
                                    ligneLettre--;
                                }
                                if (memoColonne == 0)
                                {
                                    colonneLettre++;
                                }
                                if (memoColonne == 2)
                                {
                                    colonneLettre--;
                                }

                                //on va continuer d'explorer les cellules aux alentours de la cellule à laquelle on était avant de se tromper de chemin
                                //on va donc recommencer à regarder à partir de (la case d'où l'on vient de revenir) +1
                                //pour éviter de reprendre ce mauvais chemin
                                memoLigne = ChangerValeurDeMemoLigne(memoLigne, memoColonne);
                                memoColonne = ChangerValeurDeMemoColonne(memoLigne, memoColonne);
                            }
                        }
                    }
                    //sinon on parcourt le reste de la matrice pour trouver la première lettre autre part
                    //et si elle n'y est pas c'est que le mot n'est pas sur le plateau
                    //donc la valeur de reponse restera false

                    indexMot = 0; //on recherche donc une nouvelle fois la première lettre du mot

                    //on réinitialise tout avec leur valeur initial
                    memoLigne = 0; 
                    memoColonne = 0;
                    ligneLettre = i;
                    colonneLettre = j;


                    //on remet "marquerPassageMot" à sa valeur d'initialisation car on va tenter de trouver un autre chemin pour le mot
                    //au cas où l'on soit rentrer dans la boucle ci-dessus et que "marquerPassageMot" soit partiellement remplis
                    for (int k = 0; k < matricePlateau.GetLength(0); k++)
                    {
                        for (int l = 0; l < matricePlateau.GetLength(1); l++)
                        {
                            marquerPassageMot[k,l] = matricePlateau[k,l].Lettre;
                            //on initialise "marquerPassageMot" avec les mêmes valeurs que "matricePlateau"
                        }
                    }
                }
            }
            
            return reponse;
        }

        /// <summary>
        /// pour connaitre les lettres adjacentes à la cellule où l'on se trouve
        /// </summary>
        /// <param name="ligneMatrice"> ligne de la cellule à laquelle on se trouve </param>
        /// <param name="colonneMatrice"> colonne de la cellule à laquelle on se trouve </param>
        /// <param name="matrice"> matrice "marquéPassageMot" pour respecter le fait qu'on ne puisse pas réutiliser une case sur laquelle on est déjà passé </param>
        /// <returns> on retourne une matrice 3x3 (car il y a 8 lettres adjacentes + celle où l'on se trouve) composée des lettres adjacentes </returns>
        public char[,] LettresAdjacentes(int ligneMatrice, int colonneMatrice, char[,] matrice)
        //on va faire un tableau pour obtenir toutes les lettres des cellules adjacentes à celle à laquelle on est
        {
            char[,] lettresAdjacentes = new char[3, 3];
            // car il y a 8 possibilités max de cellules adjacentes (si on est dans le milieu du plateau) sans compter la cellule à laquelle on est

            int a = 0;
            int b = 0;

            //on se place sur la ligne du dessus (et on finira sur la ligne du dessous) de la cellule où l'on souhaite regarder les cellules adjacentes
            int i = ligneMatrice - 1; 
            int j = colonneMatrice - 1; //idem pour la colonne

            //on va remplir la matrice de LettresAdjacentes
            for (a = 0; a < lettresAdjacentes.GetLength(0); a++)
            {
                j = colonneMatrice - 1;

                for (b = 0; b < lettresAdjacentes.GetLength(1); b++)
                {
                    //on effectue un try/catch car si l'index se trouve en dehors du tableau, on ne lui affecte pas une lettre mais une valeur arbitraire
                    //on utilisera le symbole '*' pour reconnaitre les cellules par lesquelles il est impossible de passer 
                    try
                    {
                        if (i == ligneMatrice && j == colonneMatrice)
                        {
                            lettresAdjacentes[a, b] = '*';
                            //on est à l'endroit où l'on veut connaitre les cellules voisines donc on lui assigne la valeur impossible
                        }
                        else
                        {
                            lettresAdjacentes[a, b] = matrice[i, j];
                            //sinon on remplit la matrice "lettresAdjacentes" avec les lettres des cellules voisines
                        }
                    }
                    catch (IndexOutOfRangeException) // si on se trouve en dehors des limites du tableau
                    {
                        lettresAdjacentes[a, b] = '*'; //impossible d'y accéder
                    }
                    j++;
                }
                i++;
            }
            //nous aurons donc une matrice contenant toutes les lettres adjacentes à la cellule désirée et d'autres caractères dans les autres cellules          
            return lettresAdjacentes;
        }

        /// <summary>
        /// pour regarder à partir de (la case d'où l'on vient de revenir)+1 --> utile lorsque l'on revient sur nos pas car on s'est trompé de chemin pour trouvé le mot
        /// </summary>
        /// <param name="memoLigne"> ligne de la cellule où l'on se trouvait précédemment (avant de revenir sur nos pas) </param>
        /// <param name="memoColonne"> colonne de la cellule où l'on se trouvait précédemment (avant de revenir sur nos pas) </param> 
        /// <returns> on retourne la ligne de la cellule que l'on va explorer à la prochaine boucle (dans la méthode Test_Plateau) </returns>
        public int ChangerValeurDeMemoLigne(int memoLigne, int memoColonne) //pour regarder à partir de(la case d'où l'on vient de revenir) +1
        {
            if (memoColonne == 2) //si la case d'où l'on vient de revenir était à la dernière colonne
            {
                memoLigne = memoLigne + 1; //il faut changer de ligne, en l'occurence augmenter la ligne
            }
            return memoLigne;
        }

        /// <summary>
        /// pour regarder à partir de (la case d'où l'on vient de revenir)+1 --> utile lorsque l'on revient sur nos pas car on s'est trompé de chemin pour trouvé le mot
        /// </summary>
        /// <param name="memoLigne"> ligne de la cellule où l'on se trouvait précédemment (avant de revenir sur nos pas) </param>
        /// <param name="memoColonne"> colonne de la cellule où l'on se trouvait précédemment (avant de revenir sur nos pas) </param>
        /// <returns> on retourne la colonne de la cellule que l'on va explorer à la prochaine boucle (dans la méthode Test_Plateau) </returns>
        public int ChangerValeurDeMemoColonne(int memoLigne, int memoColonne) //pour regarder à partir de(la case d'où l'on vient de revenir) +1
        {
            if (memoColonne == 2) //si la case d'où l'on vient de revenir était à la dernière colonne
            {
                memoColonne = 0; //on va changer de ligne (comme dit au dessus) ET revenir à la première colonne
            }
            else
            {
                memoColonne++; //sinon on reste sur la même ligne mais on change de colonne, en l'occurence ici on l'incrémente
            }
            return memoColonne;
        }
    }
}
