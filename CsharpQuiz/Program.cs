using static System.Int32;

namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static string _reset = "\u001B[0m";
        private static string _black = "\u001B[30m";
        private static string _red = "\u001B[31m";
        private static string _green = "\u001B[32m";
        private static string _yellow = "\u001B[33m";
        private static string _blue = "\u001B[34m";
        private static string _purple = "\u001B[35m";
        private static string _cyan = "\u001B[36m";
        private static string _white = "\u001B[37m";
            
        private static string _blackBackground = "\u001B[40m";
        private static string _redBackground = "\u001B[41m";
        private static string _greenBackground = "\u001B[42m";
        private static string _yellowBackground = "\u001B[43m";
        private static string _blueBackground = "\u001B[44m";
        private static string _purpleBackground = "\u001B[45m";
        private static string _cyanBackground = "\u001B[46m";
        private static string _whiteBackground = "\u001B[47m";
        
        private static List<Question> _questions = new();
        
        private static void Main(string[] args)
        {
            CreateQuestions();

            Console.Clear();
            Console.WriteLine($"Bienvenue au Quiz C# !");
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
                Console.WriteLine($"{_red}Erreur console (null){_reset}");
                return;
            }
                
            if (userStringInput.ToLower().Equals("n"))
            {
                Console.WriteLine("Au revoir");
                return;
            }
            
            ShowRecap(0);
        }

        private static bool AskQuestion(Question question, int id)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{_yellow} Question {id} sur {_questions.Count}{_reset}");
            Console.WriteLine($"{_cyan} {question.Title}{_reset}");
            Console.WriteLine();

            var i = 1;
            foreach (var resp in question.Responses!)
            {
                Console.WriteLine($" {i}. {_cyan}{resp}{_reset}");
                i++;
            }
            
            Console.WriteLine();

            do
            {
                Console.Write($"Votre réponse : {_purple}");

                var userStringInput = Console.ReadLine();
                
                Console.Write($"{_reset}");

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
                    Console.WriteLine($"{_red}Réponse invalide (doit entre 1 et {question.Responses.Length}){_reset}");
                }
                else
                {
                    question.UserResponse = userIntInput;
                    break;
                }
            } while (true);

            return true;
        }

        private static void ShowRecap(int index)
        {
            while (true)
            {
                var question = _questions[index];
                var id = index + 1;

                Console.Clear();

                Console.WriteLine($"(RÉCAPITULATIF) QUESTION {id} / {_questions.Count}");
                Console.WriteLine();
                Console.WriteLine($"{question.Title}");
                Console.WriteLine();

                var i = 1;
                foreach (var resp in question.Responses!)
                {
                    Console.Write(i == question.ValidResponse ? $" ✅ {_green}" : $" ❌ {_red}");

                    Console.Write($" {i}. {resp}{_reset}");

                    Console.Write(i == question.UserResponse ? $"{_blue} <-- Votre réponse {_reset}" : "");
                    Console.WriteLine();
                    i++;
                }

                Console.WriteLine();

                Console.WriteLine(question.Recap);

                Console.WriteLine("\nUtilisez les flèches <- -> pour naviguer dans le récapitulatif.");

                var consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (index <= 0)
                        {
                            index = 0;
                            continue;
                        }

                        index -= 1;
                        continue;

                    case ConsoleKey.RightArrow:
                        if (index + 1 >= _questions.Count)
                            End();
                        else
                        {
                            index += 1;
                            continue;
                        }
                        break;

                    default:
                        continue;
                }

                break;
            }
        }

        private static void End()
        {
            Console.Clear();
            Console.WriteLine($"Fin du récap ! Pour rappel votre score était de /{_questions.Count}. Au revoir !");
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
                    Recap = "Le Boolean est la bonne réponse, car les autres proposes des chaînes de caractères (String), un seul caractère (char), ou un nombre (int)."
                },
                new()
                {
                    Title = "Quelle est la bonne façon d'écrire quelque chose dans la console ?",
                    Responses = new []
                    {
                        "Console.Write(“Hello world!”);",
                        "Console.out.WriteLine(“Hello world!”);",
                        "Console.print(“Hello world!”);",
                        "Console.WriteLine(`Hello world!`);"
                    },
                    ValidResponse = 1,
                    Recap = "La première est la bonne réponse. Même si la 4ème réponse peut sembler bonne, elle utilise les mauvais guillemets pour la chaîne de caractères."
                },
                 new()
                {
                    Title = "Lequel des éléments suivants n’est pas un type de variable valide dans le Framework .Net?",
                    Responses = new []
                    {
                        "double",
                        "int",
                        "var",
                        "mime"
                    },
                    ValidResponse = 4,
                    Recap = "Le “mime” est une bonne réponse. En effet, ce n’est pas une variable, mais un protocole d’adresses mails, ce qui n’a rien à voir."
                },
                 new()
                {
                    Title = "Lequel de ces mots permettent de définir le début d’une boucle ?",
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
                    Title = $"Quel sera le résultat de cette variable ? \n\n{_blue} var result = 2 / 3;{_reset}",
                    Responses = new []
                    {
                        "result de type double",
                        "result de type decimal",
                        "result de type float",
                        "result de type int"
                    },
                    ValidResponse = 4,
                    Recap = "La 4ème réponse est la bonne. Même si le résultat possède une virgule, tant que le calcul n’est pas effectué et directement écrit, la variable prendra comme type : int."
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
                    Recap = "La première réponse est la bonne. ReadLine() est une méthode qui lit entièrement la dernière ligne entrée. ReadKey() obtient le caractère suivant ou la touche de fonction sur laquelle l'utilisateur a appuyé."
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
                    Recap = "La bonne réponse est la première. Seule la virgule présente dans “int[,]” permet de représenter un tableau multidimensionnel."
                },
                 new()
                {
                    Title = "Quelle est la différence entre les opérateurs == et Equals() en C#?",
                    Responses = new []
                    {
                        "L'opérateur == compare les références, Equals() compare seulement le contenu",
                        "L'opérateur == compare le contenu, Equals() compare les références",
                        "L'opérateur == compare les références, Equals() compare les références",
                        "L'opérateur == compare le contenu, Equals() compare le contenu"
                    },
                    ValidResponse = 1,
                    Recap = "La bonne réponse est la première. La méthode .equals() est préférablement à utiliser pour comparer la valeur de deux Strings tandis que l'opérateur « == » les compare par référence. Également, lorsqu'il s'agit de deux Integers, il est préférable d’utiliser « == »."
                },
                 new()
                {
                    Title = "le point d'entrée du programme est une méthode :",
                    Responses = new []
                    {
                        "static void Main()",
                        "void Main(string[] args)",
                        "static void main(string{} args)",
                        "static void Main(string[] args) "
                    },
                    ValidResponse = 4,
                    Recap = "La première réponse est la 4ème. En effet, la première n’a pas d’arguments, la deuxième n’est pas statique, et la troisième a son tableau d'argument mal orthographié."
                },
                 new()
                {
                    Title = "Quelle est la sortie de l'expression suivante : \n\nvar calcul = 5 + Math.BigMule(3, 2);",
                    Responses = new []
                    {
                        "16",
                        "11",
                        "13",
                        "10"
                    },
                    ValidResponse = 3,
                    Recap = "La 3ème réponse est la bonne, en effet Math.BigMule(3, 2) est une opération de multiple, exactement comme “3 x 2”."
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