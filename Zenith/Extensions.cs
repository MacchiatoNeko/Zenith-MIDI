using System.Threading.Tasks;

namespace Zenith
{
    public static class Extensions
    {
        public static async Task Await(this Task task)
        {
            if (task != null) await task;
        }

        public static async Task Await(this CancellableTask task)
        {
            if (task != null) await task.Task;
        }
    }
}
