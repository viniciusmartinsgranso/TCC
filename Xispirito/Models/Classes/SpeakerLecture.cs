namespace Xispirito.Models
{
    public class SpeakerLecture : BaseEntity
    {
        private Speaker Speaker { get; set; }
        private Lecture Lecture { get; set; }

        public SpeakerLecture()
        {

        }

        public SpeakerLecture(Speaker speaker, Lecture lecture)
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