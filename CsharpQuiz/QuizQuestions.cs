namespace CsharpQuiz;

public class QuizQuestions
{
    public static List<Question> Questions = new();
    
    public static void CreateQuestions()
        {
            Questions = new List<Question>
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
                    Title = $"Quel sera le résultat de cette variable ? \n\n{AnsiColors.Purple} var result = 2 / 3;{AnsiColors.Reset}",
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
                    Title = $"Quelle est la sortie de l'expression suivante : \n\n{AnsiColors.Purple} var calcul = 5 + Math.BigMule(3, 2);{AnsiColors.Reset}",
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

    public static int Count()
    {
        return Questions.Count;
    }
}