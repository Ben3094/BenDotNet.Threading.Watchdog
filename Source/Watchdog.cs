using System.Threading.Tasks;

namespace System.Threading
{
    public static class Watchdog
    {
        /// <param name="action"></param>
        /// <param name="waitTime">Time allocated to the action to run before the watchdog barks</param>
        /// <returns>true if the watchdog didn't bark, else false</returns>
        public static bool Execute(Action action, TimeSpan waitTime)
        {
            bool result = false;
            AutoResetEvent watchdog = new AutoResetEvent(false);
            Task.Factory.StartNew(() =>
            {
                action.Invoke();
                watchdog.Set();
                result = true;
            });
            watchdog.WaitOne(waitTime);
            return result;
        }
    }
}
