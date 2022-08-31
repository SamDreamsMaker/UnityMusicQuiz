using System;

namespace MusicQuiz
{
    [System.Serializable]
    public class Choice
    {
        public string artist;
        public string title;

        public string getTextData() {
            return artist + Environment.NewLine + title;
        }
    }
}