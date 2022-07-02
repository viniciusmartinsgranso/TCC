using System.Drawing;

namespace Xispirito.Models
{
    public static class ModalityColor
    {
        private static Color Online = Color.FromArgb(75, 209, 142);
        private static Color InPerson = Color.FromArgb(240, 145, 22);
        private static Color Hybrid = Color.FromArgb(138, 37, 177);

        public static Color GetModalityColor(string modality)
        {
            Color color = Color.White;
            switch (modality)
            {
                case "Híbrido":
                    color = Hybrid;
                    break;
                case "Presencial":
                    color = InPerson;
                    break;
                default:
                    color = Online;
                    break;
            }
            return color;
        }
    }
}