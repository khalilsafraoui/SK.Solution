using Microsoft.JSInterop;

namespace SK.Solution.Shared.Extensions
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

        public static async Task ToastrSuccessWithDelay(this IJSRuntime jSRuntime, string message, int delay = 2000)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "success", message);
            await Task.Delay(delay);
        }

        public static async Task ToastrErrorWithDelay(this IJSRuntime jSRuntime, string message, int delay = 2000)
        {
            await jSRuntime.InvokeVoidAsync("showToastr", "error", message);
            await Task.Delay(delay);
        }

        public static async Task GoogleMap_InitMap(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("initMap");
        }

        public static async Task GoogleMap_AddMarkers(this IJSRuntime jSRuntime, object Locations)
        {
            await jSRuntime.InvokeVoidAsync("addMarkers", Locations);
        }

        public static async Task GoogleMap_DrawPolylines(this IJSRuntime jSRuntime, object Locations)
        {
            await jSRuntime.InvokeVoidAsync("drawPolylines", Locations);
        }

        public static async Task GoogleMap_ClearMarkers(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("clearMarkers");
        }

        public static async Task GoogleMap_ClearPolylines(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("clearPolylines");
        }
    }
}
