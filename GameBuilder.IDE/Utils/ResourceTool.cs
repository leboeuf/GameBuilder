using System;
using GameBuilder.IDE.Properties;

namespace GameBuilder.IDE.Utils
{
    /// <summary>
    /// Tool used to read from resource files.
    /// </summary>
    public static class ResourceTool
    {
        /// <summary>
        /// Get the label of a command (e.g. menu item).
        /// </summary>
        /// <param name="commandName">The command constant name.</param>
        /// <returns>The text to display for the given command.</returns>
        internal static string GetCommandText(string commandName)
        {
            return MenusLabels.ResourceManager.GetString(string.Format("{0}_Text", commandName.Replace('.', '_')));
        }

        /// <summary>
        /// Get the tooltip text of a command (e.g. menu item).
        /// </summary>
        /// <param name="commandName">The command constant name.</param>
        /// <returns>The tooltip text of the given command.</returns>
        internal static string GetCommandTooltip(string commandName)
        {
            return MenusLabels.ResourceManager.GetString(string.Format("{0}_Tooltip", commandName.Replace('.', '_')));
        }

        /// <summary>
        /// Returns the base URI string of the icons resource folder.
        /// </summary>
        /// <returns>String of the base URI of the icons folder.</returns>
        internal static string GetBaseIconUri()
        {
            return "pack://application:,,,/Resources/Icons";
        }

        /// <summary>
        /// Returns the full name associated to a file extension.
        /// Use "_All" as the extension to get the label for "All supported files".
        /// </summary>
        /// <param name="key">The extension for which to get the name of the file type.</param>
        /// <returns>The full name of the file type.</returns>
        internal static string GetFileTypeLabel(string key)
        {
            throw new NotImplementedException();
            //return FileTypesLabels.ResourceManager.GetString(key);
        }

        /// <summary>
        /// Returns a label from the Resources file.
        /// </summary>
        /// <param name="key">The key of the label.</param>
        /// <returns>The label contents</returns>
        internal static string GetLabel(string key)
        {
            return Resources.ResourceManager.GetString(key);
        }
    }
}
