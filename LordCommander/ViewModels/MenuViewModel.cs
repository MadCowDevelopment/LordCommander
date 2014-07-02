using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace LordCommander.ViewModels
{
    [Export(typeof(MenuViewModel))]
    public class MenuViewModel : Screen
    {
        [ImportingConstructor]
        public MenuViewModel()
        {
            
        }
    }
}
