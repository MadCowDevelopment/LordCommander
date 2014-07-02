using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using LordCommander.Client;
using LordCommander.Shared;
using LordCommander.Views;

namespace LordCommander.ViewModels
{
    [Export(typeof(MenuViewModel))]
    public class MenuViewModel : Screen
    {
        private readonly IGameProxy _gameProxy;
        private readonly IProgressDialog _progressDialog;

        [ImportingConstructor]
        public MenuViewModel(IGameProxy gameProxy, IProgressDialog progressDialog)
        {
            _gameProxy = gameProxy;
            _progressDialog = progressDialog;
        }

        public async Task Queue()
        {
            var progress = await _progressDialog.ShowProgressDialog("Queue", "Looking for an opponent...", true);

            var tokenSource = new CancellationTokenSource();
            
            _gameProxy.GameStarted.Take(1).Subscribe(async p =>
            {
                tokenSource.Cancel();
                await progress.CloseAsync();
                RaiseGameStarted(p);
            });

            Task.Factory.StartNew(async () =>
            {
                while (!tokenSource.IsCancellationRequested)
                {
                    if (progress.IsCanceled)
                    {
                        await _gameProxy.LeaveQueue();
                        await progress.CloseAsync();
                        break;
                    }

                    Thread.Sleep(20);
                }
            }, tokenSource.Token);

            await _gameProxy.Queue();
        }

        public event Action<MenuViewModel, GameDto> GameStarted;

        private void RaiseGameStarted(GameDto game)
        {
            var handler = GameStarted;
            if (handler != null) handler(this, game);
        }

        public void Quit()
        {
            Application.Current.Shutdown();
        }
    }
}
