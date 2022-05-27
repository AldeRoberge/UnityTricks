using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtility
{
    /// <summary>
    /// https://gist.github.com/mattyellen/d63f1f557d08f7254345bff77bfdc8b3
    /// Allows to convert Unity 'AsyncOperations' to 'async/await' Tasks.
    ///
    /// <code>
    /// var getRequest = UnityWebRequest.Get("http://www.google.com");
    /// await getRequest.SendWebRequest();
    /// var result = getRequest.downloadHandler.text;
    /// </code>
    ///
    /// Note : This is a temporary solution until Unity implements a proper async/await support.
    /// It might be already available in the most recent Unity version.
    /// </summary>
    public static class UnityAsyncOperationAwaiter
    {
        public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<object>();
            asyncOp.completed += obj => { tcs.SetResult(null); };
            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}