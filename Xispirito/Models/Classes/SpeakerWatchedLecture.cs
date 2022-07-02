namespace Xispirito.Models
{
    public class SpeakerWatchedLecture
    {
        private Speaker Speaker { get; set; }
        private Lecture Lecture { get; set; }

        public SpeakerWatchedLecture()
        {

        }

        public SpeakerWatchedLecture(Speaker speaker, Lecture lecture)
        {
            Speaker = speaker;
            Lecture = lecture;
        }

        public Speaker GetSpeaker()
        {
            return Speaker;
        }

        public Lecture GetLecture()
        {
            return Lecture;
        }
    }
}