#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.Xml;
using System.IO;

/// <summary>
/// This script automatically modifies the `Package.appxmanifest` file after building a Universal Windows Platform (UWP) application in Unity.
/// Specifically, it replaces the `musicLibrary` capability with `documentsLibrary` in the manifest file.
/// 
/// In Unity's Build Settings for UWP, the `documentsLibrary` capability does not appear in the available options for enabling capabilities. 
/// This script handles this limitation by replacing the `musicLibrary` capability with `documentsLibrary` in the `Package.appxmanifest` file.
/// 
/// **IMPORTANT**: Before running this script, ensure that the capability you intend to replace (e.g., `musicLibrary`) is enabled in the Unity Build Settings.
/// This script assumes that the capability you wish to replace is already included in the manifest. The script will then replace it with the specified new capability.
/// 
/// The script is configured to replace `musicLibrary` with `documentsLibrary` in this example, but it can be adapted to replace other
/// known folder capabilities as needed. To use this script for different capabilities, modify the `oldCapabilityName` and
/// `newCapabilityName` parameters in the `ChangeCapabilityName` method.
/// </summary>
public class AddCapabilityToXml : IPostprocessBuildWithReport
{
    /// <summary>
    /// Defines the order in which the callback is invoked. Lower numbers are called earlier.
    /// </summary>
    public int callbackOrder { get { return 1; } }

    /// <summary>
    /// This method is triggered after the build process is complete. It checks if the build target is Universal Windows Platform (WSAPlayer),
    /// and if so, it locates and modifies the `Package.appxmanifest` file by replacing the specified capability.
    /// </summary>
    /// <param name="report">The report containing information about the completed build.</param>
    public void OnPostprocessBuild(BuildReport report)
    {
        // Only proceed if the build target is Universal Windows Platform (WSAPlayer).
        if (report.summary.platform == BuildTarget.WSAPlayer)
        {
            // **IMPORTANT** Define the path to the Package.appxmanifest file. "______" -> (Add your project name (Package name))
            string path = Path.Combine(report.summary.outputPath, "______", "Package.appxmanifest");

            // Check if the manifest file exists at the specified path.
            if (File.Exists(path))
            {
                // Replace the 'musicLibrary' capability with 'documentsLibrary'.
                ChangeCapabilityName(path, "musicLibrary", "documentsLibrary");
            }
        }
    }

    /// <summary>
    /// Modifies the specified capability in the manifest file by replacing it with a new capability.
    /// </summary>
    /// <param name="manifestPath">The full path to the `Package.appxmanifest` file.</param>
    /// <param name="oldCapabilityName">The name of the capability to be replaced.</param>
    /// <param name="newCapabilityName">The name of the new capability to be added.</param>
    private void ChangeCapabilityName(string manifestPath, string oldCapabilityName, string newCapabilityName)
    {
        // Load the manifest file into an XML document.
        XmlDocument doc = new XmlDocument();
        doc.Load(manifestPath);

        // Create a namespace manager to handle the XML namespaces used in the manifest.
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("uap", "http://schemas.microsoft.com/appx/manifest/uap/windows10");

        // Select the capability node that matches the old capability name.
        XmlNode capabilityNode = doc.SelectSingleNode("//uap:Capability[@Name='" + oldCapabilityName + "']", nsmgr);

        // If the node is found, update its name attribute to the new capability name.
        if (capabilityNode != null)
        {
            capabilityNode.Attributes["Name"].Value = newCapabilityName;
        }

        // Save the modified manifest file.
        doc.Save(manifestPath);
    }
}
#endif
