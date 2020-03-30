using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD9___TD10_Bogle
{
    //public pour test unitaire
    /// <summary>
    /// Classe définissant un dé, va nous servir pour tirer une lettre au hasard sur une case du plateau
    /// </summary>
    public class Dé
    {
        //champs
        private char[] ensembleDeLettres; // ensemble de lettre possible pour le dé
        private char lettre;

        //constructeur
        /// <summary>
        /// un dé est crée par un ensemble de lettres
        /// </summary>
        /// <param name="ensembleDeLettres"> tableau contenant un ensemble de lettres, en l'occurence 6 </param>
        public Dé(char[] ensembleDeLettres)
        {
            if (ensembleDeLettres.Length == 6) // s'il y a bien 6 valeurs (donc les 6 lettres)
            {
                this.ensembleDeLettres = ensembleDeLettres;
            }
        }

        //propriétés
        /// <summary>
        ///  pouvoir obtenir la lettre de la face supérieur du dé plus tard dans le programme
        /// </summary>
        public char Lettre // on a besoin de pouvoir y avoir accès pour l'utiliser mais pas de set car on va la tirer au hasard
        {
            get
            {
                return this.lettre;
            }
        }

        //pas besoin de propriété pour le tableau "ensembleDeLettres" car on n'a pas besoin d'y accéder autre part que dans cette classe

        //méthodes
        /// <summary>
        /// sert à lancer le dé
        /// </summary>
        /// <param name="r"> générateur --> va nous fournir un chiffre au hasard entre 0 et 5, nous indiquant un index de "ensembleDeLettre" et nous donnera ainsi la face supérieur du dé tirée au sort </param>
        public void Lance(Random r)
        {
            this.lettre = ensembleDeLettres[r.Next(5)]; 
            //la lettre tiré sera donc la lettre se trouvant à l'index tiré au hasard entre 0 et 5 donnant ainsi les 6 possibilités du dé
        }

        /// <summary>
        ///  Va nous servir dans la méthode ToooString après pour afficher les lettres du tableau d'ensemble de lettre d'un dé (toutes les faces du dé)
        /// </summary>
        /// <returns> string contenant toutes les lettres (la valeur des 6 faces)</returns>
        public string AfficherLettreDé() // pour pouvoir afficher les lettres présentes sur chaque face du dé dans la méthode "ToooString"
        {
            string reponse = "";
            for (int i = 0; i < ensembleDeLettres.Length - 1; i++) // on s'arrête à l'avant dernier index
            {
                reponse = reponse + ensembleDeLettres[i] + " / ";
            }
            reponse = reponse + ensembleDeLettres[ensembleDeLettres.Length-1]; // on affiche le dernier élément mais sans le "/"
            return reponse;
        }

        /// <summary>
        /// Sert à afficher tout ce qui caractérise un dé
        /// </summary>
        /// <returns> on retourne donc un string contenant toutes les caractéristique d'un dé </returns>
        public string ToooString()
        {
            string s = "Ce dé possédait les lettres : " + AfficherLettreDé() + "\n" + "Et la lettre de la face visible est " + this.lettre;
            return s;
        }
    }
}
