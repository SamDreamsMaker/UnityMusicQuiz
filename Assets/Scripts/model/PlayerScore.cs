using System.Collections;
using System.Collections.Generic;
namespace MusicQuiz
{
    public class PlayerScore
    {
        private int score;
        private Dictionary<int, Answer> answers = new Dictionary<int, Answer>();

        public PlayerScore() {

        }

        public int getScore() {
            return score;
        }
        public void increaseScore() {
            score++;
        }
        public void addAnswer(int questionId, int answerId, bool win) {
            answers.Add(questionId, new Answer(answerId, win));
        }
        public bool hasWin(int id) {
            return answers[id].win;
        }
    }
}