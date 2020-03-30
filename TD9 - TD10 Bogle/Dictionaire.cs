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
    /// classe définissant le dictionnaire que l'on va utiliser pour valider les mots (si les mots saisis par les joueurs appartiennent au dictionnaire)
    /// </summary>
    public class Dictionaire // pour séparer les mots dans tableau peut etre faire un "Split" avec comme séparateur les chiffres
    {
        //champs
        private string[][] tableauMotsPossibles; // on fait un tableau car le nombre de mots possibles est invariable

        //constructeur
        /// <summary>
        /// le dictionnaire est crée à partir d'un fichier, "MotsPossibles.txt"
        /// </summary>
        /// <param name="filename"> le nom du fichier </param>
        public Dictionaire(string filename) // on va remplir le dictionnaire avec les mots du fichier
        {
            //initialisation
            StreamReader flux = null;
            string line = "";
            string[] tableauFichier = new string[28]; //car il y a 28 lignes dans le fichier
            int i = 0;

            //boucle try/catch
            try
            {
                flux = new StreamReader(filename); // ouverture du flux vers le fichier choisit
                while ((line = flux.ReadLine()) != null) // tant qu'on n'est pas à la fin du fichier, on continue de le lire)
                {
                    tableauFichier[i] = line; // on "télécharge" le fichier en le mettant dans un tableau
                    i++;
                }

            }
            catch (Exception e) // si on trouve pas le fichier par exemple
            {
                Console.WriteLine(e.Message);
            }
            finally //dans tous les cas
            {
                if (flux != null) // si le flux est ouvert 
                {
                    flux.Close(); // on le ferme
                }
            }

            //rassembler groupe de mots par taille
            i = 0; // on remet "i" à 0
            int j = 0;
            string[] tableauGroupeDeMots = new string[14]; // car il y a 14 lignes contenant des mots

            for (i = 0; i < tableauFichier.Length - 1; i = i + 2) // on incrémente pour sauter la ligne contenant les nombres
            {
                tableauGroupeDeMots[j] = tableauFichier[i + 1]; // on remplit le tableauGroupeDeMots avec juste les mots en enlevant les nombres du tableauFichier
                //le "i+1" saute les lignes contenant les nombres
                j++;
            }
            //maintenant "tableauGroupeDeMots" contient seulement les mots
            //j'ai donc à chaque index tous les mots de même longueur --> index 0 : longueur mot = 2,...


            // on va désormais "découper" chaque index de "tableauGroupeDeMots" pour n'avoir que des mots séparés et non un grand string
            char[] separateur = { ' ' }; // car la commande "Split" demande un tableau
            i = 0;
            j = 0;
            string[][] tableauFinal = new string[14][]; //il y a 14 groueps de mots

            while (i < tableauGroupeDeMots.Length) //on parcourt tout le tableau pour séparer chaque index
            {
                string[] tableauMotsSéparés = tableauGroupeDeMots[i].Split(separateur); // on sépare les mots

                tableauFinal[i] = tableauMotsSéparés; // on remplit chaque index de tableauFinal avec un tableau --> celui des motsSeparés
                i++;
            }

            this.tableauMotsPossibles = tableauFinal;
        }

        //propriété
        /// <summary>
        /// Pour pouvoir accéder à tous les mots contenus dans le dictionnaire
        /// </summary>
        public string[][] TableauMotsPossibles
        {
            get
            {
                return this.tableauMotsPossibles;
            }
        }

        //méthodes
        /// <summary>
        /// pour pouvoir afficher tous les mots du dictionnaire
        /// </summary>
        /// <returns></returns>
        public string TooooString()
        {
            string reponse = "";
            for (int i = 0; i < this.tableauMotsPossibles.Length ; i++)
            // ici i = 2 pour tests
            {
                for (int j = 0; j < tableauMotsPossibles[i].Length; j++)
                {
                    reponse = reponse + tableauMotsPossibles[i][j] + " ";
                }
                reponse = reponse + "\n" + "\n"; // on saute 2 lignes pour que l'affichage soit plus propre
            }
            return reponse;
        }

        /// <summary>
        /// pour savoir si le mot saisi par un joueur se trouve dans le dictionnaire
        /// </summary>
        /// <param name="mot"> mot saisi </param>
        /// <param name="debut"> index du début de la recherche (0) </param>
        /// <param name="fin"> index de la fin de la recherche (dico.TableauMotsPossibles[mot.Length - 2].Length --> représente le dernier index du groupe de mot de même taille que le mot saisi par un joueur) </param>
        /// <param name="index"> index du mot (lettre) </param>
        /// <returns> on retourne vrai si le mot se trouve dans le dictionnaire ou faux sinon</returns>
        public bool RechDicoRecursif(string mot, int debut, int fin, int index = 0)
        {
            int milieu = (debut + fin) / 2;

            string MotDuMilieu = tableauMotsPossibles[mot.Length - 2][milieu]; // mot du milieu dans l'index contenant tous les mots de la même taille que "mot"
            //on fait mot.Lentgh-2 car les mots du premier groupe de mots ont une taille de 2 et la première ligne du tableauMotsPossibles est la ligne 0
            //il y a donc un décalage  de 2 entre la taille des premiers mots et la première ligne

            if(debut > fin)
            {
                return false;
            }

            if (mot == MotDuMilieu) // on trouve direct
            {
                return true;
            }
            else
            {
                if (mot[index] < MotDuMilieu[index]) // si le mot est avant
                {
                    return RechDicoRecursif(mot, debut, milieu - 1);
                }
                if (mot[index] > MotDuMilieu[index]) // si le mot est après
                {
                    return RechDicoRecursif(mot, milieu + 1, fin);
                }
                else
                {
                    return RechDicoRecursif(mot, debut, fin, index + 1); // on recommence en comparant la lettre suivante de "mot" et "motDuMilieu"
                } 
            }
        }
    }   
}