using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace LordCommander.Services
{
    [Export(typeof(IProgressDialog))]
    public class ProgressDialog : IProgressDialog
    {
        private readonly IDialog _dialog;

        [ImportingConstructor]
        public ProgressDialog(IDialog dialog)
        {
            _dialog = dialog;
        }

        public async Task<ProgressDialogResult> ShowProgressDialog(string title, string message, Func<Task> func)
        {
            var progress = await _dialog.ShowProgressDialog(title, message);
            Exception exception = null;
            try
            {
                await func();
            }
            catch (Exception e)
            {
                exception = e;
            }

            await progress.CloseAsync();
            if (exception != null) await _dialog.ShowMessage("Error", exception.ToString());
            return new ProgressDialogResult(exception);
        }
    }
}