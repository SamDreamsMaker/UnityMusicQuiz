using System;

namespace MusicQuiz
{
    [System.Serializable]
    public class Song
    {
        public int id;
        public string title;
        public string artist;
        public string picture;
        public string sample;
        private const string ARTIST_PREFIX = "Artist : ";
        private const string TITLE_PREFIX = "Title : ";

        public string getTextData() {
            return ARTIST_PREFIX + artist + Environment.NewLine + TITLE_PREFIX + title;
        }
    }
}