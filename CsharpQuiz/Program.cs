namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static string? _score;
        private static int _intScore;
        
        private static void Main(string[] args)
        {
            QuizQuestions.CreateQuestions();

            Console.Clear();
            Console.WriteLine(" .d88888b.           d8b                         .d8888b.    888  888   ");
            Console.WriteLine("d88P\" \"Y88b          Y8P                        d88P  Y88b   888  888   ");
            Console.WriteLine("888     888                                     888    888 888888888888 ");
            Console.WriteLine("888     888 888  888 888 88888888 88888888      888          888  888   ");
            Console.WriteLine("888     888 888  888 888    d88P     d88P       888          888  888   ");
            Console.WriteLine("888 Y8b 888 888  888 888   d88P     d88P        888    888 888888888888 ");
            Console.WriteLine("Y88b.Y8b88P Y88b 888 888  d88P     d88P         Y88b  d88P   888  888   ");
            Console.WriteLine(" \"Y888888\"   \"Y88888 888 88888888 88888888       \"Y8888P\"    888  888   ");
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.CyanBright}Bienvenue à ce quiz C# !");
            Console.WriteLine($"{AnsiColors.CyanBright}Vous allez devoir répondre à {AnsiColors.White}{QuizQuestions.Count()} {AnsiColors.CyanBright}questions, chacune vous rapportant 1 point.{AnsiColors.Reset}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.BlackBright}Appuyez sur une touche pour continuer...{AnsiColors.Reset}");
            Console.ReadKey();

            var i = 0;
            foreach (var question in QuizQuestions.Questions)
            {
                i++;
                if (AskQuestion(question, i, 1) == false)
                    return;
            }

            _intScore = QuizQuestions.Questions.Count(question => question.UserResponse == question.ValidResponse);
            _score = _intScore >= QuizQuestions.Count() ? $"{AnsiColors.PurpleBright}PERFECT!{AnsiColors.YellowBright}" : $"{AnsiColors.YellowBoldBright}{_intScore}{AnsiColors.YellowBright}/{QuizQuestions.Count()}";
            ShowScore(true);
        }

        private static void ShowScore(bool yes)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.WhiteBright} ********************************************");
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.YellowBright} Score : {_score}");
            Console.WriteLine(" Temps passé sur le quiz : .");
            Console.WriteLine(" Temps passé en moyenne sur une question : .");
            Console.WriteLine();

            var even = QuizQuestions.Count() / 2;
            var message = $"{AnsiColors.BlueBright} Tout juste la moyenne ! ";

            if (_intScore > even)
                message = $"{AnsiColors.Green} Bravo ! Votre score est au dessus de la moyenne.";
            
            if (_intScore < even)
                message = $"{AnsiColors.RedBright} Vous n'avez pas la moyenne... C'est pas grave ! Et si on recommençait ?";

            if (_intScore >= QuizQuestions.Count())
                message = $"{AnsiColors.Green} Votre score est {AnsiColors.PurpleBoldBright}PARFAIT{AnsiColors.Green} ! Bravo !";
            
            Console.WriteLine(message + AnsiColors.Reset);
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.WhiteBright} ********************************************");
            Console.WriteLine();

            Console.WriteLine($"{AnsiColors.Cyan} Voulez-vous voir le récapitulatif approfondi ?");
            Console.WriteLine();
            Console.Write($"{AnsiColors.WhiteBright} - ");
            Console.Write(yes ? $"{AnsiColors.CyanBold}Oui {AnsiColors.Yellow}<--" : $"{AnsiColors.Cyan}Oui");
            Console.Write($"\n{AnsiColors.WhiteBright} - ");
            Console.Write(yes ? $"{AnsiColors.Cyan}Non" : $"{AnsiColors.CyanBold}Non {AnsiColors.Yellow}<--");
            
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    End();
                    break;
                
                case ConsoleKey.Enter:
                    if (yes)
                        ShowRecap(0);
                    break;
                
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    ShowScore(!yes);
                    break;
            }
        }

        private static void WriteQuestion(Question question, int id, int response)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.YellowBoldBright} Question {id} sur {QuizQuestions.Count()}{AnsiColors.Reset}");
            Console.WriteLine($"{AnsiColors.Cyan} {question.Title}{AnsiColors.Reset}");
            Console.WriteLine();

            var i = 1;
            foreach (var resp in question.Responses!)
            {
                if (response == i)
                {
                    Console.Write($" [{i}] {AnsiColors.CyanBold}{resp}{AnsiColors.Yellow} <--{AnsiColors.Reset}");
                }
                else
                {
                    Console.Write($" {i}. {AnsiColors.Cyan}{resp}{AnsiColors.Reset}");
                }
                Console.WriteLine();
                i++;
            }
            
            Console.WriteLine();
        }

        private static bool AskQuestion(Question question, int id, int response)
        {
            WriteQuestion(question, id, response);

            var consoleKeyInfo = Console.ReadKey();

            int userResponse;

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    End();
                    return false;
                
                case ConsoleKey.Enter:
                    if (response == 0)
                        response = 1;
                    question.UserResponse = response;
                    break;
                
                case ConsoleKey.DownArrow:
                    userResponse = response + 1;

                    if (userResponse <= 0 || userResponse > question.Responses!.Length)
                    {
                        AskQuestion(question, id, 1);
                        break;
                    }
                    
                    AskQuestion(question, id, userResponse);
                    break;
                
                case ConsoleKey.UpArrow:
                    userResponse = response - 1;
                    
                    if (userResponse <= 0 || userResponse > question.Responses!.Length)
                    {
                        AskQuestion(question, id, 1);
                        break;
                    }
                    
                    AskQuestion(question, id, userResponse);
                    break;
            }

            return true;
        }

        private static void ShowRecap(int index)
        {
            while (true)
            {
                var question = QuizQuestions.Questions[index];
                var id = index + 1;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"{AnsiColors.YellowBoldBright} Question {id} sur {QuizQuestions.Count()}{AnsiColors.Reset}");
                Console.WriteLine($"{AnsiColors.Cyan} {question.Title}{AnsiColors.Reset}");
                Console.WriteLine();

                var i = 1;
                foreach (var resp in question.Responses!)
                {
                    Console.Write(i == question.ValidResponse ? $" ✅ {AnsiColors.Green}" : $" ❌ {AnsiColors.Red}");

                    Console.Write($" {i}. {resp}{AnsiColors.Reset}");

                    Console.Write(i == question.UserResponse ? $"{AnsiColors.Cyan} <-- Votre réponse {AnsiColors.Reset}" : "");
                    Console.WriteLine();
                    i++;
                }

                Console.WriteLine("\n");

                Console.WriteLine(AnsiColors.GreenBright + "Explications : " + AnsiColors.Green + question.Recap + AnsiColors.Reset);

                Console.WriteLine($"\n\n{AnsiColors.BlackBright}Utilisez les flèches <- -> pour naviguer dans le récapitulatif.\n" +
                                  "Appuyer sur 'Entrée' passe également à la prochaine explication.\n" +
                                  $"Appuyer sur 'Echap' pour quitter.{AnsiColors.Reset}");

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

                    case ConsoleKey.Enter:
                    case ConsoleKey.RightArrow:
                        if (index + 1 >= QuizQuestions.Count())
                            continue;
                        index += 1;
                        continue;

                    case ConsoleKey.Escape:
                        End();
                        break;

                    default:
                        continue;
                }

                break;
            }
        }

        private static void End()
        {
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.Yellow}Au revoir !{AnsiColors.Reset}");
        }
    }
}