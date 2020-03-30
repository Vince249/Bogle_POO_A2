using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD9___TD10_Bogle
{
    //public pour test unitaire

    /// <summary>
    /// Classe définissant un joueur, ce dernier est caractérisé par son nom son score et la liste des mots qu'il a trouvé
    /// </summary>
    public class Joueur
    {
        //champs
        private string nom;
        private int score;
        private List<string> listeMotsTrouvés;
        //on fait une liste car on ne connait pas à l'avance le nombre de mots que va trouver le joueur

        //constructeur
        /// <summary>
        /// un joueur est crée à partir de son nom
        /// </summary>
        /// <param name="nom"> nom du joueur </param>
        public Joueur(string nom)
        {
            this.nom = nom;
            listeMotsTrouvés = new List<string>();
        }

        //propriétés
        /// <summary>
        /// pouvoir obtenir le nom du joueur plus tard dans le programme
        /// </summary>
        public string Nom
        {
            get
            {
                return this.nom;
            }
        }

        /// <summary>
        /// pouvoir obtenir et modifier le score du joueur plus tard dans le programme
        /// </summary>
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }

        /// <summary>
        /// pouvoir obtenir la liste des mots trouvés par le joueur plus tard dans le programme
        /// </summary>
        public List<string> ListeMotsTrouvés
        {
            get
            {
                return listeMotsTrouvés;
            }
        }

        //méthodes
        /// <summary>
        /// Savoir si le mot rentré en paramètre se trouve dans la liste des mots trouvés par le joueur
        /// </summary>
        /// <param name="mot"> ici le mot sera le mot tapé par un joueur dans le jeu </param>
        /// <returns> on retourne vrai si le mot appartient à la liste ou faux sinon </returns>
        public bool Contain(string mot)
        {
            bool MotAppartientAListe = false;
            if (listeMotsTrouvés.Contains(mot)) // pour savoir si le mot appartient à la liste (".Contains" permet cela)
            // si je n'avais pas utilisé cela, j'aurai pu faire une boucle for 
            {
                MotAppartientAListe = true;
            }
            return MotAppartientAListe;
        }

        /// <summary>
        /// Sert à ajouter un mot à la liste de mots trouvés par le joueur
        /// </summary>
        /// <param name="mot"> ici le mot sera le mot tapé par un joueur dans le jeu </param>
        public void Add_Mot(string mot)
        {
            listeMotsTrouvés.Add(mot); // on ajoute le mot trouvé dans la liste de mots trouvés
        }

        /// <summary>
        /// Va nous servir dans la méthode TooString après pour afficher les mots de la liste des mots trouvés par le joueur
        /// </summary>
        /// <returns> on retourne la liste des mots trouvés par le joueur </returns>
        public string AfficherListeMotsTrouvés() // pour pouvoir afficher tous les mots trouvé par le joueur dans la méthode "TooString"
        {
            string reponse = "";
            if (listeMotsTrouvés.Count != 0) //si la liste n'est pas nulle (que le joueur a trouvé des mots)
            {
                //on retourne la liste des mots
                for (int i = 0; i < listeMotsTrouvés.Count - 1; i++) // on s'arrête à l'avant dernier index
                {
                    reponse = reponse + listeMotsTrouvés[i] + " / ";
                }
                reponse = reponse + listeMotsTrouvés[listeMotsTrouvés.Count - 1]; // on affiche le dernier élément mais sans le "/"
            }
            //si la liste est nulle on ne retournera que : ""
            return reponse;
        }

        /// <summary>
        /// Sert à afficher tout ce qui caractérise un joueur
        /// </summary>
        /// <returns> on retourne donc un string contenant toutes les caractéristique d'un joueur </returns>
        public string TooString()
        {
            string s = "";
            if (listeMotsTrouvés.Count == 0)
            {
                s = this.nom + " possède " + this.score + " points et n'a trouvé aucun mot";
            }
            else
            {
                s = this.nom + " possède " + this.score + " points et a trouvé les mots suivants : " + AfficherListeMotsTrouvés();
            }
            return s;
        }
    }
}
