using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Client
{
    public class AppState
    {
        private IJSRuntime JSRuntime;
        public AppState(IJSRuntime r)
        {
            JSRuntime = r;
        }

        public async Task Store<T>(string key, T obj)
        {
            var objStr = JsonSerializer.Serialize(obj);
            await JSRuntime.InvokeAsync<object>("window.localStorage.setItem", new [] { key, objStr });
        }
        public async Task<T> Retrieve<T>(string key)
        {
            var str = await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", new[] { key });
            if (str == null) return default(T);
            try
            {
                T obj = JsonSerializer.Deserialize<T>(str);
                return obj;
            } catch (Exception)
            {
                return default(T);
            }
        }
    }
}
