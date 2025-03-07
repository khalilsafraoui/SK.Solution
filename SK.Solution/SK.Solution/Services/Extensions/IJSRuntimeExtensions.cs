using Microsoft.JSInterop;

namespace SK.Solution.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "error", message);
        }

        public static async Task ToastrSuccessWithDelay(this IJSRuntime jSRuntime, string message,int delay = 2000)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "success", message);
            await Task.Delay(delay);
        }

        public static async Task ToastrErrorWithDelay(this IJSRuntime jSRuntime, string message, int delay = 2000)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "error", message);
            await Task.Delay(delay);
        }
    }
}
