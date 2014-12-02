using System.Linq;

namespace HtmlTags.Mvc
{
    public static class ElementRequestExtensions
    {
        public static ElementRequest SetType<T>(this ElementRequest request)
        {
            request.Type = typeof(T);
            return request;
        }

        public static bool IsType<T>(this ElementRequest request)
        {
            return request.Type == typeof (T);
        }

        public static bool IsForEditor(this ElementRequest request)
        {
            return request.Intent == TagRequestIntent.Edit;
        }

        public static bool IsForDisplay(this ElementRequest request)
        {
            return request.Intent == TagRequestIntent.Display;
        }

        public static ElementRequest AddData(this ElementRequest request, string key, object value)
        {
            request.AdditionalData[key] = value;
            return request;
        }

        public static bool HasData(this ElementRequest request, params string[] keys)
        {
            return keys.All(x => request.AdditionalData.ContainsKey(x));
        }

        public static T GetData<T>(this ElementRequest request, string key)
        {
            if (!request.HasData(key)) return default(T);
            return (T)request.AdditionalData[key];
        }

        public static object GetData(this ElementRequest request, string key)
        {
            if (!request.HasData(key)) return null;
            return request.AdditionalData[key];
        }

        public static ElementRequest SetValue(this ElementRequest request, object value)
        {
            request.Value = value;
            return request;
        }

        public static T GetValue<T>(this ElementRequest request)
        {
            if (request.Value == null) return default(T);
            if (!request.Value.GetType().IsAssignableFrom(typeof (T))) return default(T);
            return (T)request.Value;
        }

        public static ElementRequest SetName(this ElementRequest request, string name)
        {
            request.Name = name;
            return request;
        }

        public static ElementRequest SetDisplayName(this ElementRequest request, string displayName)
        {
            request.Name = displayName;
            return request;
        }

    }
}