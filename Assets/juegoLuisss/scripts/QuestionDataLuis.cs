public class QuestionDataLuis
{
    public string infoText;
    public string questionText;
    public string[] answers;
    public int correctIndex;

    public QuestionDataLuis(string infoText, string questionText, string[] answers, int correctIndex)
    {
        this.infoText = infoText;
        this.questionText = questionText;
        this.answers = answers;
        this.correctIndex = correctIndex;
    }
}
