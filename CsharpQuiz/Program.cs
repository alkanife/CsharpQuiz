using static System.Int32;

namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static List<Question> _questions = new();
        
        private static void Main(string[] args)
        {
            CreateQuestions();

            int[][] array;

            Console.Clear();
            Console.WriteLine("Bienvenue au Quiz C# !");
            Console.WriteLine($"Il y a {_questions.Count} questions");
            Console.WriteLine("Taper sur n'importe quelle touche si vous êtes prêt. CTRL+C pour annuler");
            Console.ReadKey();

            var i = 0;
            foreach (var question in _questions)
            {
                i++;
                if (AskQuestion(question, i) == false)
                    return;
            }
            
            Console.Clear();
            
            Console.WriteLine("Vous avez répondu a toutes les questions !");

            var score = _questions.Count(question => question.UserResponse == question.ValidResponse);

            Console.WriteLine($"Votre score: {score}/{_questions.Count}");
            
            Console.WriteLine("Voulez-vous avoir un récap approfondi ? (Y/n)");
            
            var userStringInput = Console.ReadLine();

            if (userStringInput == null)
            {
                Console.WriteLine("Erreur console (null)");
                return;
            }
                
            if (userStringInput.ToLower().Equals("n"))
            {
                Console.WriteLine("Au revoir");
                return;
            }
            
            i = 0;
            foreach (var question in _questions)
            {
                i++;
                Console.Clear();
            
                Console.WriteLine("RECAPITULATIF");
                Console.WriteLine($"Question {i} sur {_questions.Count}");
                Console.WriteLine();
                Console.WriteLine($"{question.Title}");
            
                var j = 1;
                foreach (var resp in question.Responses!)
                {
                    Console.Write(j == question.ValidResponse ? "[V] " : "[X] ");
                
                    Console.Write($"{j}. {resp}");
                
                    Console.Write(j == question.UserResponse ? " <-- Votre réponse" : "");
                    Console.WriteLine();
                    j++;
                }
            
                Console.WriteLine();

                if (question.UserResponse == question.ValidResponse)
                {
                    Console.WriteLine("Vous avez trouvé la bonne réponse, bien joué !");
                }
                else
                {
                    Console.WriteLine(question.Recap);
                }
            
            
                Console.WriteLine("\nTaper n'importe quelle touche pour continuer");
                Console.ReadLine();
            }
            
            Console.Clear();
            Console.WriteLine($"Fin du récap ! Pour rappel votre score était de {score}/{_questions.Count}. Au revoir !");
        }

        private static bool AskQuestion(Question question, int id)
        {
            Console.Clear();
            Console.WriteLine($"Question numéro {id}:");
            Console.WriteLine();
            Console.WriteLine($"{question.Title}");

            var i = 1;
            foreach (var resp in question.Responses!)
            {
                Console.WriteLine($"{i}. {resp}");
                i++;
            }
            
            Console.WriteLine();

            do
            {
                Console.Write("Entrer réponse> ");

                var userStringInput = Console.ReadLine();

                if (userStringInput == null)
                {
                    Console.WriteLine("Erreur console (null)");
                    return false;
                }
                
                if (userStringInput.ToLower().Equals("exit"))
                {
                    Console.WriteLine("Annulé");
                    return false;
                }
                
                var tryParse = TryParse(userStringInput, out var userIntInput);

                if (!tryParse || userIntInput == 0 || userIntInput > question.Responses.Length)
                {
                    Console.WriteLine($"Réponse invalide (doit entre 1 et {question.Responses.Length})");
                }
                else
                {
                    question.UserResponse = userIntInput;
                    break;
                }
            } while (true);

            return true;
        }

        private static void CreateQuestions()
        {
            _questions = new List<Question>
            {
                new()
                {
                    Title = "Quelle variable peut avoir pour valeur true ou false ?",
                    Responses = new[]
                    {
                        "Int",
                        "String",
                        "Boolean",
                        "Char"
                    },
                    ValidResponse = 3,
                    Recap = "Le Boolean est la bonne réponse, car les autres proposes des chaînes de caractères (String), un seul caractère (char), ou un nombre (int).\n"
                },
                new()
                {
                    Title = "Quelle est la bonne façon d'écrire quelque chose dans la console ?\n",
                    Responses = new []
                    {
                        "Console.Write(“Hello world!”);",
                        "Console.out.WriteLine(“Hello world!”);",
                        "Console.print(“Hello world!”);",
                        "Console.WriteLine(`Hello world!`);"
                    },
                    ValidResponse = 1,
                    Recap = "La première est la bonne réponse. Même si la 4ème réponse peut sembler bonne, elle utilise les mauvais guillemets pour la chaîne de caractères.\n"
                },
                 new()
                {
                    Title = "Lequel des éléments suivants n’est pas un type de variable valide dans le Framework .Net?\n\n",
                    Responses = new []
                    {
                        "double",
                        "int",
                        "var",
                        "mime"
                    },
                    ValidResponse = 4,
                    Recap = "Le “mime” est une bonne réponse. En effet, ce n’est pas une variable, mais un protocole d’adresses mails, ce qui n’a rien à voir.\n"
                },
                 new()
                {
                    Title = " Lequel de ces mots permettent de définir le début d’une boucle ?",
                    Responses = new []
                    {
                        "Bool",
                        "Loop",
                        "For",
                        "Whileach"
                    },
                    ValidResponse = 3,
                    Recap = "La 3ème réponse est la bonne. “for” est une structure de contrôle de programmation qui permet de répéter l'exécution d'une séquence d'instructions."
                },
                 new()
                {
                    Title = "Quel sera le résultat de cette variable ? \nvar result = 2 / 3;\n",
                    Responses = new []
                    {
                        "result de type double",
                        "result de type decimal",
                        "result de type float",
                        "result de type int"
                    },
                    ValidResponse = 4,
                    Recap = "La 4ème réponse est la bonne. Même si le résultat possède une virgule, tant que le calcul n’est pas effectué et directement écrit, la variable prendra comme type : int.\n"
                },
                 new()
                {
                    Title = "Quelle est la méthode utilisée pour lire une ligne de texte depuis la console ?",
                    Responses = new []
                    {
                        "Console.ReadLine()",
                        "Console.ReadKey()",
                        "Console.Read()",
                        "Console.Readline()"
                    },
                    ValidResponse = 1,
                    Recap = "La première réponse est la bonne. ReadLine() est une méthode qui lit entièrement la dernière ligne entrée. ReadKey() obtient le caractère suivant ou la touche de fonction sur laquelle l'utilisateur a appuyé.\n"
                },
                 new()
                {
                    Title = "Comment déclare-t-on correctement un tableau à deux dimensions en C# ?",
                    Responses = new []
                    {
                        "int[,] myArray;",
                        "int[][] myArray;",
                        "int[2] myArray; ",
                        "System.Array[2] myArray;"
                    },
                    ValidResponse = 1,
                    Recap = " La bonne réponse est la première. Seule la virgule présente dans “int[,]” permet de représenter un tableau multidimensionnel.\n"
                },
                 new()
                {
                    Title = "Quelle est la différence entre les opérateurs == et Equals() en C#?",
                    Responses = new []
                    {
                        "L'opérateur == compare les références, Equals() compare seulement le contenu",
                        "L'opérateur == compare le contenu, Equals() compare les références",
                        "L'opérateur == compare les références, Equals() compare les références   ",
                        "L'opérateur == compare le contenu, Equals() compare le contenu"
                    },
                    ValidResponse = 1,
                    Recap = " La bonne réponse est la première. La méthode .equals() est préférablement à utiliser pour comparer la valeur de deux Strings tandis que l'opérateur « == » les compare par référence. Également, lorsqu'il s'agit de deux Integers, il est préférable d’utiliser « == ».\n"
                },
                 new()
                {
                    Title = "le point d'entrée du programme est une méthode: \n-static void Main()\n",
                    Responses = new []
                    {
                        "static void Main() ",
                        "void Main(string[] args)   ",
                        "static void main(string{} args)  ",
                       
                        "static void Main(string[] args) "
                    },
                    ValidResponse = 4,
                    Recap = " La première réponse est la 4ème. En effet, la première n’a pas d’arguments, la deuxième n’est pas statique, et la troisième a son tableau d'argument mal orthographié.\n"
                },
                 new()
                {
                    Title = "Quelle est la sortie de l'expression suivante : \nvar calcul = 5 + Math.BigMule(3, 2);\n",
                    Responses = new []
                    {
                        "16",
                        "11",
                        "13",
                        "10"
                    },
                    ValidResponse = 3,
                    Recap = " La 3ème réponse est la bonne, en effet Math.BigMule(3, 2) est une opération de multiple, exactement comme “3 x 2”."
                },
               

            };
        }
    }

    internal class Question
    {
        public string? Title { get; init; }
        public string[]? Responses { get; init; }
        public int ValidResponse { get; init; }
        public int UserResponse { get; set; }
        public string? Recap { get; init; }
    }
}