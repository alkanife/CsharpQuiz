namespace CsharpQuiz;

public class Question
{
    public string? Title { get; init; }
    public string[]? Responses { get; init; }
    public int ValidResponse { get; init; }
    public int UserResponse { get; set; }
    public string? Recap { get; init; }
    
    public TimeSpan TimeSpan { get; set; }
}