using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD9___TD10_Bogle
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void JoueurContainTest() //test de la méthode Contain de la classe Joueur
        {
            Joueur joueurTest = new Joueur("Durand");
            
            //on ajoute deux mots à la liste
            joueurTest.Add_Mot("loi");
            joueurTest.Add_Mot("faire");

            string mot = "loi"; //mot que l'on va chercher dans la liste du joueur
            bool resultat = joueurTest.Contain(mot); //bool obtenue pour "mot" pour la méthode Contain
            Assert.AreEqual(true, resultat);

            string mot2 = "ok";
            bool resultat2 = joueurTest.Contain(mot2);
            Assert.AreEqual(false, resultat2);
        }

        [TestMethod]
        public void JoueurAfficherListeMotsTrouvésTest()
        {
            Joueur joueurTest = new Joueur("Durand");
            joueurTest.Add_Mot("loi");
            joueurTest.Add_Mot("faire");

            string resultat = joueurTest.AfficherListeMotsTrouvés();
            string valeurAttendu = "loi / faire";

            Assert.AreEqual(valeurAttendu, resultat);
        }

        [TestMethod]
        public void JoueurTooString()
        {
            Joueur joueurTest = new Joueur("Durand");

            //initialisation du joueur
            joueurTest.Score = 10;
            joueurTest.Add_Mot("loi");

            string resultat = joueurTest.TooString();
            string valeurAttendu = "Durand possède 10 points et a trouvé les mots suivants : loi"; //ce qu'on s'attend à avoir

            Assert.AreEqual(valeurAttendu, resultat);
        }

        [TestMethod]
        public void DictionnaireRechercheDicoTest()
        {
            Dictionaire dico = new Dictionaire("MotsPossibles.txt");

            string mot = "RODERA";
            int debut = 0;
            int fin = dico.TableauMotsPossibles[mot.Length - 2].Length;
            int index = 0;

            bool resultat = dico.RechDicoRecursif(mot, debut, fin, index);
            Console.WriteLine(resultat);

            Assert.AreEqual(true, resultat);
        }

        [TestMethod]
        public void PlateauTestPlateauTest() 
        {
            Plateau plateau = new Plateau("Des.txt");
            plateau.LancéDéSurPlateau();

            char[] mot = new char[] { plateau.MatricePlateau[0,0].Lettre, plateau.MatricePlateau[1, 1].Lettre, plateau.MatricePlateau[1, 2].Lettre };
            //on crée un mot composé de lettres qui sont censés se suivre sur le plateau

            bool resultat = plateau.Test_Plateau(mot);

            Assert.AreEqual(true, resultat);
        }
    }
}
