using System;

namespace MusicQuiz
{
    [System.Serializable]
    public class Choice
    {
        public string artist;
        public string title;

        private const string ARTIST_PREFIX = "Artist : ";
        private const string TITLE_PREFIX = "Title : ";

        public string getTextData() {
            return ARTIST_PREFIX + artist + Environment.NewLine + TITLE_PREFIX + title;
        }
    }
}