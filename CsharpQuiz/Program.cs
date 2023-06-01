namespace CsharpQuiz
{
    internal abstract class Program
    {
        private static bool _examMode;
        private static int _timerSeconds;
        private static Question _currentQuestion;
        private static int _currentQuestionId = -1;
        private static bool _stopTimer = false;
        private static int _userResponse = 1;
        
        private static string? _score;
        private static int _intScore;

        private static DateTime _lastQuestionDateTime;
        private static TimeSpan _totalTime;
        private static TimeSpan _avgTime;
        
        private static void Main(string[] args)
        {
            QuizQuestions.CreateQuestions();

            ChooseExamMode(false);

            NextQuestion(false);
            
            _timerSeconds = 1;
            StartTimer();

            HandleUserChoices();
        }

        private static void HandleUserChoices()
        {
            var consoleKeyInfo = Console.ReadKey();

            if (_stopTimer)
            {
                ShowScore(true);
                return;
            }
            
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    End();
                    break;
                
                case ConsoleKey.Enter:
                    NextQuestion(true);
                    HandleUserChoices();
                    break;
                
                case ConsoleKey.DownArrow:
                    _userResponse++;

                    if (IsNotAValidChoice())
                    {
                        _userResponse = 1;
                        WriteQuestion();
                        HandleUserChoices();
                        break;
                    }
                    
                    WriteQuestion();
                    HandleUserChoices();
                    break;
                
                case ConsoleKey.UpArrow:
                    _userResponse--;
                    
                    if (IsNotAValidChoice())
                    {
                        _userResponse = 1;
                        WriteQuestion();
                        HandleUserChoices();
                        break;
                    }
                    
                    WriteQuestion();
                    HandleUserChoices();
                    break;
                
                default:
                    WriteQuestion();
                    HandleUserChoices();
                    break;
            }
        }
        
        private static bool IsNotAValidChoice()
        {
            return _userResponse <= 0 || _userResponse > _currentQuestion.Responses!.Length;
        }
        
        private static void WriteQuestion()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.YellowBoldBright} Question {(_currentQuestionId+1)} sur {QuizQuestions.Count()}{AnsiColors.Reset}");
            if (_examMode)
            {
                Console.SetCursorPosition(30, 1);
                Console.Write($"{_timerSeconds}s               ");
                Console.Write("\n");
            }
            Console.WriteLine($"{AnsiColors.Cyan} {_currentQuestion.Title}{AnsiColors.Reset}");
            Console.WriteLine();

            var i = 1;
            foreach (var resp in _currentQuestion.Responses!)
            {
                Console.Write(_userResponse == i
                    ? $" [{i}] {AnsiColors.CyanBold}{resp}{AnsiColors.Yellow} <--{AnsiColors.Reset}"
                    : $" {i}. {AnsiColors.Cyan}{resp}{AnsiColors.Reset}");
                Console.WriteLine();
                i++;
            }
            
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.BlackBright}Appuyer sur 'Echap' pour quitter.{AnsiColors.Reset}");
        }

        private static void NextQuestion(bool validate)
        {
            if (validate)
            {
                _currentQuestion.UserResponse = _userResponse;
                _currentQuestion.TimeSpan = DateTime.Now.Subtract(_lastQuestionDateTime);
            }
            
            _timerSeconds = 1;
            _lastQuestionDateTime = DateTime.Now;
            
            _currentQuestionId++;
            if (_currentQuestionId >= QuizQuestions.Questions.Count)
            {
                calculScoreAndTime();
                ShowScore(true);
                return;
            }
            
            _currentQuestion = QuizQuestions.Questions[_currentQuestionId];
                    
            WriteQuestion();
        }
        
        private static void calculScoreAndTime()
        {
            _stopTimer = true;

            // Calcul score
            _intScore = QuizQuestions.Questions.Count(question => question.UserResponse == question.ValidResponse);
            _score = _intScore >= QuizQuestions.Count() ? $"{AnsiColors.PurpleBright}PERFECT!{AnsiColors.YellowBright}" : $"{AnsiColors.YellowBoldBright}{_intScore}{AnsiColors.YellowBright}/{QuizQuestions.Count()}";
            
            var times = new List<int>();
            
            // Calcul temps total @ temps en moyenne
            foreach (var question in QuizQuestions.Questions)
            {
                _totalTime = _totalTime.Add(question.TimeSpan);
                times.Add(question.TimeSpan.Seconds);
            }
            
            var average = times.AsQueryable().Average();
            _avgTime = new TimeSpan(0, 0, 0, (int)average);
            
            ShowScore(true);
        }

        private static async void StartTimer()
        {
            if (!_examMode)
                return;
            
            await Task.Run(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer(1000);

                timer.Elapsed += (sender, eventArgs) =>
                {
                    if (_stopTimer) // J'ai pas trouvé comment arrêté ce timer ducoup j'ai mis ça pour l'instant
                        return;    // (C'est honteux)

                    Console.SetCursorPosition(30, 1);
                    Console.Write($"{_timerSeconds}s               ");

                    if (_timerSeconds > 3)
                    {
                        NextQuestion(true); // pour l'instant on valide la réponse quand on skip
                        return;
                    }
                    
                    _timerSeconds++;
                };
                timer.Start();
                return Task.CompletedTask;
            });
        }

        private static void ChooseExamMode(bool examMode)
        {
            while (true)
            {
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
                Console.WriteLine($"{AnsiColors.Cyan} Choisissez le mode du quiz :");
                Console.WriteLine();

                Console.Write(examMode
                    ? $"{AnsiColors.WhiteBright} - {AnsiColors.Cyan}Mode normal"
                    : $"{AnsiColors.WhiteBright} [-] {AnsiColors.CyanBold}Mode normal {AnsiColors.Yellow}<--");
                Console.Write("\n");
                Console.Write(examMode
                    ? $"{AnsiColors.WhiteBright} [-] {AnsiColors.CyanBold}Mode examen {AnsiColors.Yellow}<--"
                    : $"{AnsiColors.WhiteBright} - {AnsiColors.Cyan}Mode examen");
                

                Console.WriteLine(AnsiColors.White);

                var consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        End();
                        break;

                    case ConsoleKey.Enter:
                        _examMode = examMode;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.UpArrow:
                        examMode = !examMode;
                        continue;

                    default:
                        continue;
                }

                break;
            }
        }

        private static void ShowScore(bool continueToRecap)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine($"{AnsiColors.WhiteBright} ********************************************");
                Console.WriteLine();
                Console.WriteLine($"{AnsiColors.YellowBright} Score : {_score}");
                Console.WriteLine($" Temps passé sur le quiz : {GetSpanFormat(_totalTime)}", _totalTime);
                Console.WriteLine($" Temps passé en moyenne sur une question : {GetSpanFormat(_avgTime)}", _avgTime);
                Console.WriteLine();

                var even = QuizQuestions.Count() / 2;
                var message = $"{AnsiColors.BlueBright} Tout juste la moyenne ! ";

                if (_intScore > even) message = $"{AnsiColors.Green} Bravo ! Votre score est au dessus de la moyenne.";

                if (_intScore < even) message = $"{AnsiColors.RedBright} Vous n'avez pas la moyenne... C'est pas grave ! Et si on recommençait ?";

                if (_intScore >= QuizQuestions.Count()) message = $"{AnsiColors.Green} Votre score est {AnsiColors.PurpleBoldBright}PARFAIT{AnsiColors.Green} ! Bravo !";

                Console.WriteLine(message + AnsiColors.Reset);
                Console.WriteLine();
                Console.WriteLine($"{AnsiColors.WhiteBright} ********************************************");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine($"{AnsiColors.Cyan} Voulez-vous voir le récapitulatif approfondi ?");
                Console.WriteLine();

                Console.Write(continueToRecap
                    ? $"{AnsiColors.WhiteBright} [-] {AnsiColors.CyanBold}Oui {AnsiColors.Yellow}<--"
                    : $"{AnsiColors.WhiteBright} - {AnsiColors.Cyan}Oui");

                Console.Write("\n");
                Console.Write(continueToRecap
                    ? $"{AnsiColors.WhiteBright} - {AnsiColors.Cyan}Non"
                    : $"{AnsiColors.WhiteBright} [-] {AnsiColors.CyanBold}Non {AnsiColors.Yellow}<--");

                Console.WriteLine(AnsiColors.White);

                var consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        End();
                        break;

                    case ConsoleKey.Enter:
                        if (continueToRecap)
                            ShowRecap(0);
                        else
                            End();
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.UpArrow:
                        continueToRecap = !continueToRecap;
                        continue;

                    default:
                        continue;
                }

                break;
            }
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
                Console.WriteLine($"{AnsiColors.YellowBright} Temps passé sur la question : {GetSpanFormat(question.TimeSpan)}{AnsiColors.Reset}", question.TimeSpan);
                Console.WriteLine();
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
                                  "Appuyer sur 'Retour' pour revenir au score.\n" +
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
                    
                    case ConsoleKey.Backspace:
                        ShowScore(true);
                        break;

                    case ConsoleKey.Escape:
                        End();
                        break;

                    default:
                        continue;
                }

                ShowRecap(index);
                break;
            }
        }

        private static void End()
        {
            Console.WriteLine();
            Console.WriteLine($"{AnsiColors.Yellow}Au revoir !{AnsiColors.Reset}");
        }
        
        private static string GetSpanFormat(TimeSpan timeSpan)
        {
            var s = timeSpan.Seconds + " secondes";
            
            if (timeSpan.Minutes >= 1)
                s = "{0:mm\\:ss}";
            
            if (timeSpan.Hours >= 1)
                s = "{0:hh\\:mm\\:ss}";

            if (timeSpan.Days >= 1)
                s = "{0:dd\\.hh\\:mm\\:ss}";

            return s;
        }
    }
}