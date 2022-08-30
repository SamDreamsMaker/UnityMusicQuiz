using UnityEngine;

namespace MusicQuiz
{
    [System.Serializable]
    public static class QuizFactory
    {
        public static Song createSong(string text) {
            return JsonUtility.FromJson<Song>(text);
        }
        public static Question createQuestion(string text) {
            return JsonUtility.FromJson<Question>(text);
        }
        public static Quiz createQuiz(string text) {
            return JsonUtility.FromJson<Quiz>(text);
        }
        public static QuizData createQuizData(string text) {
            return JsonUtility.FromJson<QuizData>("{\"quizs\":" + text + "}");
        }
    }
}
