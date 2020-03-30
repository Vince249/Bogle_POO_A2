using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD9___TD10_Bogle
{
    /// <summary>
    /// Classe où le jeu est mis en place
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //déclaration et création du dictionnaire et du plateau
            Dictionaire dico = new Dictionaire("MotsPossibles.txt");
            Plateau plateau = new Plateau("Des.txt"); //il ne reste plus qu'à lancer les dés

            Console.WriteLine("Bonjour, vous voici dans le jeu du Bogle \n");

            //On crée les joueurs
            Console.WriteLine("Veuillez entrer le nom du joueur 1");
            string nomJoueur1 = Console.ReadLine();
            Joueur joueur1 = new Joueur(nomJoueur1);

            Console.WriteLine("Veuillez entrer le nom du joueur 2");
            string nomJoueur2 = Console.ReadLine();
            Joueur joueur2 = new Joueur(nomJoueur2);

            Console.WriteLine("\n");


            //Début du jeu
            TimeSpan débutPartie = new TimeSpan();
            débutPartie = DateTime.Now.TimeOfDay; //donne l'heure actuelle
            TimeSpan finPartie = new TimeSpan();
            finPartie = débutPartie + TimeSpan.FromMinutes(6); //la fin de la partie aura lieu à l'heure actuelle + 6minutes
            int créationPlateau = 0;

            while (DateTime.Now.TimeOfDay <= finPartie) //on compare systématiquement la fin avec l'heure en cours
            {
                TimeSpan débutMancheJoueur1 = new TimeSpan();
                débutMancheJoueur1 = DateTime.Now.TimeOfDay;

                Console.WriteLine("A votre tour " + joueur1.Nom + "\n");

                while (DateTime.Now.TimeOfDay <= débutMancheJoueur1 + TimeSpan.FromMinutes(1)) //joueur1
                {
                    if (créationPlateau == 0) //on ne fait cela qu'une fois pour la manche du joueur
                    {
                        plateau.LancéDéSurPlateau(); //on lance les dés du plateau
                        créationPlateau++;
                    }

                    Console.WriteLine(plateau.ToooooString()); //on affiche le plateau
                    Console.WriteLine("ATTENTION IL VOUS RESTE " + (débutMancheJoueur1 + TimeSpan.FromMinutes(1) - DateTime.Now.TimeOfDay));
                    Console.WriteLine("Pour le bon déroulement du jeu, veuillez appuyer sur entrée si vous ne trouvez pas de mots au bout d'un certain temps \n");
                    Console.WriteLine("Veuillez saisir un mot appartenant au tableau en respectant les règles du Bogle");
                    string motSaisi = Console.ReadLine().ToUpper(); //on le met en majuscule car tous les mots du dico le sont
                    Console.WriteLine("");

                    //variables nécessaires à l'utilisation des méthodes suivantes
                    int debut = 0;
                    int fin = 0;
                    if (motSaisi.Length > 2) //si la taille du mot est supérieur à 2 donc si elle est inférieur ou égale à 2 on aura fin = 0
                    {
                        fin = dico.TableauMotsPossibles[motSaisi.Length - 2].Length; // dernier index du groupe de mots de même longueur de "mot"
                    }
                    char[] motSaisiTableau = new char[motSaisi.Length];
                    for (int i = 0; i < motSaisi.Length; i++)
                    {
                        motSaisiTableau[i] = motSaisi[i];
                    }

                    if (motSaisi.Length >= 3 && dico.RechDicoRecursif(motSaisi, debut, fin) == true
                    && plateau.Test_Plateau(motSaisiTableau) == true && joueur1.Contain(motSaisi) == false)
                    //si la taille du mot est supérieur ou égale à 3, qu'il appartient au dictionnaire, 
                    //qu'il se trouve sur le plateau et qu'il n'appartient pas à la liste des mots déjà trouvé par le joueur
                    {
                        Console.WriteLine("Oui ! Continuez comme cela \n");
                        joueur1.Add_Mot(motSaisi); //on ajoute le mot à la liste

                        //On ajoute des points au score du joueur1
                        if (motSaisi.Length == 3)
                        {
                            joueur1.Score = joueur1.Score + 2;
                        }
                        if (motSaisi.Length == 4)
                        {
                            joueur1.Score = joueur1.Score + 3;
                        }
                        if (motSaisi.Length == 5)
                        {
                            joueur1.Score = joueur1.Score + 4;
                        }
                        if (motSaisi.Length == 6)
                        {
                            joueur1.Score = joueur1.Score + 5;
                        }
                        if (motSaisi.Length >= 7)
                        {
                            joueur1.Score = joueur1.Score + 11;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ce mot n'appartient pas au plateau ou au dictionnaire ou vous l'avez déjà trouvé, réésayez ! \n");
                    }
                }
                Console.WriteLine(joueur1.TooString() + "\n"); // on affiche les caractéristiques du joueur1
                créationPlateau = 0; //on le remet à 0



                TimeSpan débutMancheJoueur2 = new TimeSpan();
                débutMancheJoueur2 = DateTime.Now.TimeOfDay;

                Console.WriteLine("A votre tour " + joueur2.Nom + "\n");

                while (DateTime.Now.TimeOfDay <= débutMancheJoueur2 + TimeSpan.FromMinutes(1)) //joueur2
                {
                    if (créationPlateau == 0) //on ne fait cela qu'une fois pour la manche du joueur
                    {
                        plateau.LancéDéSurPlateau(); //on lance les dés du plateau
                        créationPlateau++;
                    }

                    Console.WriteLine(plateau.ToooooString()); //on affiche le plateau
                    Console.WriteLine("ATTENTION IL VOUS RESTE " + (débutMancheJoueur2 + TimeSpan.FromMinutes(1) - DateTime.Now.TimeOfDay));
                    Console.WriteLine("Pour le bon déroulement du jeu, veuillez appuyer sur entrée si vous ne trouvez pas de mots au bout d'un certain temps \n");
                    Console.WriteLine("Veuillez saisir un mot appartenant au tableau en respectant les règles du Bogle");
                    string motSaisi = Console.ReadLine().ToUpper(); //on le met en majuscule car tous les mots du dico le sont
                    Console.WriteLine("");

                    //variables nécessaires à l'utilisation des méthodes suivantes
                    int debut = 0;
                    int fin = 0;
                    if (motSaisi.Length > 2) //si la taille du mot est supérieur à 2 donc si elle est inférieur ou égale à 2 on aura fin = 0
                    {
                        fin = dico.TableauMotsPossibles[motSaisi.Length - 2].Length; // dernier index du groupe de mots de même longueur de "mot"
                    }
                    char[] motSaisiTableau = new char[motSaisi.Length];
                    for (int i = 0; i < motSaisi.Length; i++)
                    {
                        motSaisiTableau[i] = motSaisi[i];
                    }

                    if (motSaisi.Length >= 3 && dico.RechDicoRecursif(motSaisi, debut, fin) == true
                    && plateau.Test_Plateau(motSaisiTableau) == true && joueur2.Contain(motSaisi) == false)
                    //si la taille du mot est supérieur ou égale à 3, qu'il appartient au dictionnaire, 
                    //qu'il se trouve sur le plateau et qu'il n'appartient pas à la liste des mots déjà trouvé par le joueur
                    {
                        Console.WriteLine("Oui ! Continuez comme cela \n");
                        joueur2.Add_Mot(motSaisi); //on ajoute le mot à la liste

                        //On ajoute des points au score du joueur1
                        if (motSaisi.Length == 3)
                        {
                            joueur2.Score = joueur2.Score + 2;
                        }
                        if (motSaisi.Length == 4)
                        {
                            joueur2.Score = joueur2.Score + 3;
                        }
                        if (motSaisi.Length == 5)
                        {
                            joueur2.Score = joueur2.Score + 4;
                        }
                        if (motSaisi.Length == 6)
                        {
                            joueur2.Score = joueur2.Score + 5;
                        }
                        if (motSaisi.Length >= 7)
                        {
                            joueur2.Score = joueur2.Score + 11;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ce mot n'appartient pas au plateau ou au dictionnaire ou vous l'avez déjà trouvé, réésayez ! \n");
                    }
                }
                Console.WriteLine(joueur2.TooString() + "\n"); // on affiche les caractéristiques du joueur1
                créationPlateau = 0; //on le remet à 0
            }

            //Fin de la partie
            Console.WriteLine("\n \n \n"); //on saute des lignes pour distinguer la fin de la partie
            Console.WriteLine(joueur1.Nom + " possède " + joueur1.Score + " points");
            Console.WriteLine(joueur2.Nom + " possède " + joueur2.Score + " points");
            Console.WriteLine("");

            if (joueur1.Score > joueur2.Score)
            {
                Console.WriteLine(joueur1.Nom + " a gagné la partie !");
            }
            if (joueur2.Score > joueur1.Score)
            {
                Console.WriteLine(joueur2.Nom + " a gagné la partie !");
            }
            if (joueur1.Score == joueur2.Score)
            {
                Console.WriteLine("Il y a égalité entre " + joueur1.Nom + " et " + joueur2.Nom);
            }

            Console.ReadKey();
        }
    }
}
