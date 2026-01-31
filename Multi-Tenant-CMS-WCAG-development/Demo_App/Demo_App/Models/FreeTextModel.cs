namespace Demo_App.Models
{
    public class FreeTextModel
    {
        public string? Text { get; set; } = "";

        public FreeTextModel(string Text)
        {
            this.Text = Text;
        }
    }
}
