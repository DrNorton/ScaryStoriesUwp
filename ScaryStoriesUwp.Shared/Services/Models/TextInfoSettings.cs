namespace ScaryStoriesUwp.Shared.Services.Models
{
    public class TextInfoSettings
    {
        private double _size;
        private double _lineHeight;
        private string _font;

        public double Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public double LineHeight
        {
            get { return _lineHeight; }
            set { _lineHeight = value; }
        }
        public string Font
        {
            get { return _font; }
            set { _font = value; }
        }

    }
}
