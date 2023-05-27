using static System.Int32;

namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static List<Question> _questions = new();
        
        private static void Main(string[] args)
        {
            CreateQuestions();

            Console.Clear();
            Console.WriteLine("Bienvenue au Quiz C# !");
            Console.WriteLine($"Il y a {_questions.Count} questions");
            Console.WriteLine("Taper entrer si vous êtes prêt");
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
                ShowRecap(question, i);
            }
            
            Console.Clear();
            Console.WriteLine($"Fin du récap ! Pour rappel votre score était de {score}/{_questions.Count}. Au revoir !");
        }

        private static void ShowRecap(Question question, int id)
        {
            Console.Clear();
            
            Console.WriteLine("RECAPITULATIF");
            Console.WriteLine($"Question {id} sur {_questions.Count}");
            Console.WriteLine();
            Console.WriteLine($"{question.Title}");
            
            var i = 1;
            foreach (var resp in question.Responses!)
            {
                Console.Write(i == question.ValidResponse ? "[V] " : "[X] ");
                
                Console.Write($"{i}. {resp}");
                
                Console.Write(i == question.UserResponse ? " <-- Votre réponse" : "");
                Console.Write("\n");
                i++;
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
            
            
            Console.WriteLine("\nTaper entrer pour continuer");
            Console.ReadLine();
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
                Console.WriteLine("Entrer réponse");

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
                    Console.WriteLine($"Réponse invalide (entre 1 et {question.Responses.Length})");
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
                    Title = "Question test",
                    Responses = new[]
                    {
                        "Réponse de test (vraie)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)"
                    },
                    ValidResponse = 1,
                    Recap = "Récap de test !"
                },
                new()
                {
                    Title = "Question test",
                    Responses = new []
                    {
                        "Réponse de test (vraie)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)"
                    },
                    ValidResponse = 1,
                    Recap = "Récap de test !"
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