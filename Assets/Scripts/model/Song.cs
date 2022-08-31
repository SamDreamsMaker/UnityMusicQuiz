using System;

namespace MusicQuiz
{
    [System.Serializable]
    public class Song
    {
        public int id;
        public string title;
        public string artiste;
        public string picture;
        public string sample;

        public string getTextData() {
            return artist + Environment.NewLine + title;
        }
    }
}