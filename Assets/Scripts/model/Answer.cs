namespace MusicQuiz
{
    public class Answer
    {
        //id could be useful later
        public int id;
        public bool win;

        public Answer(int anId, bool aWin) {
            id = anId;
            win = aWin;
        }
    }
}