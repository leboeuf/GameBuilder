using TestGame1.Properties;

namespace TestGame1.Tools
{
    public static class ResourceTool
    {
        internal static string GetLabel(string key)
        {
            return Labels.ResourceManager.GetString(key);
        }
    }
}
