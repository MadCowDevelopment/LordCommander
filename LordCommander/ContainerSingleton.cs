using System.ComponentModel.Composition.Hosting;

namespace LordCommander
{
    public static class ContainerSingleton
    {
        private static readonly object LockObject = new object();
        private static CompositionContainer _instance;

        public static CompositionContainer Instance
        {
            get { return _instance; }
            set 
            {
                lock (LockObject)
                {
                    if (_instance != null) return;
                    _instance = value;
                }
            }

        }
    }
}