using WebsiteDemo.Models;

namespace WebsiteDemo.Services
{
    public class SharedService
    {
        private int _fontSize = 14;
        private int _fontSizeHeading = 20;
        private int _fontSizeH1 = 36;
        public event Action OnChange;
        public bool IsDarkMode { get; private set; }

        public int FontSize
        {
            get => _fontSize;
            private set
            {
                _fontSize = Math.Clamp(value, 10, 24);
                NotifyStateChanged();
            }
        }

        public int FontSizeHead
        {
            get => _fontSizeHeading;
            private set
            {
                _fontSizeHeading = Math.Clamp(value, 10, 24);
                NotifyStateChanged();
            }
        }

        public int FontSizeH1
        {
            get => _fontSizeH1;
            private set
            {
                _fontSizeH1 = Math.Clamp(value, 30, 45);
                NotifyStateChanged();
            }
        }

        public void AdjustFontSize(int change)
        {
            FontSize += change;
            FontSizeHead += change;
            FontSizeH1 += change;
        }

        public void ResetFontSize()
        {
            FontSize = 16;
            FontSizeHead = 20;
            FontSizeH1 = 36;
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
    public class HonorSharedService
    {
        public Honoree honoree = new Honoree();
    }
}
