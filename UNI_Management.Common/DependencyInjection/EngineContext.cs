using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UNI_Management.Common.Utility;

namespace UNI_Management.Common.DependencyInjection
{
    public static class EngineContext
    {
        static EngineContext()
        {
            if (StaticContext == null)
                StaticContext = new List<object>();
        }

        public static IList<object> StaticContext { get; set; }

        public static bool IsStaticContext { get; set; }

        public static void AddService(object item)
        {
            if (StaticContext == null)
                StaticContext = new List<object>();
            StaticContext.Insert(StaticContext != null ? StaticContext.Count : 0, item);
        }

        public static T GetService<T>()
        {
            var obj = StaticContext.First(d => d is T);
            return (T)obj;
        }

        public static object GetService(Type t)
        {
            return StaticContext.First(d => t.IsInstanceOfType(d));
        }

        public static T Resolve<T>()
        {
            return (T)HttpHelper.HttpContext.RequestServices.GetRequiredService(typeof(T));
        }

        public static object GetInstance(Type t)
        {
            return HttpHelper.HttpContext.RequestServices.GetRequiredService(t);
        }
    }
}
