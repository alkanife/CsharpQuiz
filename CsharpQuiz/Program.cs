using static System.Int32;

namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static List<Question> _questions = new();
        
        private static void Main(string[] args)
        {
            CreateQuestions();
            
            Console.WriteLine("Bienvenue au Quiz C# !");
            Console.WriteLine($"Il y a {_questions.Count} questions");
            Console.WriteLine("Êtes-vous prêt ? (entrer)");
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

            var score = _questions.Count(question => question.GoodResponse);

            Console.WriteLine($"Votre score: {score}/{_questions.Count}");
            
            Console.WriteLine("Voulez-vous avoir un récap approfondi ? (Y/n)");
        }

        private static bool AskQuestion(Question question, int id)
        {
            Console.Clear();
            Console.WriteLine($"Question numéro {id}:");
            Console.WriteLine();
            Console.WriteLine($"{question.Title}");

            int i = 1;
            foreach (var resp in question.Responses)
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
                    if (userIntInput == question.ValidResponse)
                        question.GoodResponse = true;
                    
                    Console.WriteLine($"Réponse: {userIntInput}");
                    Console.ReadLine();
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
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
                    Responses = new string[4]
                    {
                        "Réponse de test (vraie)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)",
                        "Réponse de test (fausse)"
                    },
                    ValidResponse = 1,
                    Recap = "Récap de test !"
                }
            };
        }
    }

    internal class Question
    {
        // Titre de la question
        public string? Title { get; set; }
        // Réponses possibles
        public string[]? Responses { get; set; }
        // La réponse valide
        public int ValidResponse { get; set; }
        // Le récap si la réponse n'est pas bonne 
        public string? Recap { get; set; }

        public bool GoodResponse { get; set; } = false;

    }
}