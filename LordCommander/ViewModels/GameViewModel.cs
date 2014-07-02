using System.ComponentModel.Composition;
using Caliburn.Micro;
using LordCommander.Shared;

namespace LordCommander.ViewModels
{
    [Export(typeof(GameViewModel))]
    public class GameViewModel : Screen
    {
        public void Initialize(GameDto gameDto)
        {
        }
    }
}