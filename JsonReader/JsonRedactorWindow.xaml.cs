using JsonReaderLib;
using System.Windows;

namespace JsonRedactor
{
    public partial class JsonRedactorWindow : Window
    {
        private JsonDirectory _dir;

        public JsonRedactorWindow(JsonDirectory selectedDir)
        {
            _dir = selectedDir;

            InitializeComponent();
        }
    }
}
