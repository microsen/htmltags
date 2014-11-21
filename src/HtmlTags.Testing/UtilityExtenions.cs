namespace HtmlTags.Testing
{
    public static class UtilityExtenions
    {
        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
    }
}