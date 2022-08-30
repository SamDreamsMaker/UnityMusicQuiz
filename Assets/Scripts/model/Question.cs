namespace MusicQuiz
{
    [System.Serializable]
    public class Question
    {
        public string id;
        public int answerIndex;
        public Choice[] choices;
    }
}
