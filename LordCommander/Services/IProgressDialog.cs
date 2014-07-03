using System;
using System.Threading.Tasks;

namespace LordCommander.Services
{
    public interface IProgressDialog
    {
        Task<ProgressDialogResult> ShowProgressDialog(string title, string message, Func<Task> func);
    }
}