namespace Demo_App.Data
{
    public class ImagesGallery
    {
        public List<string> Images = new List<string> ();
    }
    public class Image
    {
        public string Url { get; set; }
        public string AltText { get; set; }
    }
}
