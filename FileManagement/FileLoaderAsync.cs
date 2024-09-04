using System;
using UnityEngine;

#if ENABLE_WINMD_SUPPORT
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

/// <summary>
/// Provides functionality to load files from a specified folder structure within the Documents Library.
/// This class is only included if WinRT support is enabled.
/// </summary>
public class FileLoaderAsync : MonoBehaviour
{
#if ENABLE_WINMD_SUPPORT
    /// <summary>
    /// Asynchronously loads a file from a specified folder structure within the Documents Library.
    /// </summary>
    /// <param name="folders">An array of folder names, where the first element is the main folder and the subsequent elements are subfolders within that folder.</param>
    /// <param name="fileName">The name of the file to be loaded from the final folder.</param>
    /// <returns>A <see cref="Task{String}"/> representing the asynchronous operation. The task result contains the content of the file as a string.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="folders"/> is null, empty, or <paramref name="fileName"/> is null or empty.</exception>
    /// <exception cref="Exception">Thrown if an error occurs while accessing the folder or file, such as if the file does not exist or there are permission issues.</exception>
    public async Task<string> LoadFileFromFolder(string[] folders, string fileName)
    {
        // Validate parameters to ensure they are not null or empty
        if (folders == null || folders.Length == 0)
        {
            throw new ArgumentException("The folders array cannot be null or empty.", nameof(folders));
        }
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("The file name cannot be null or empty.", nameof(fileName));
        }

        try
        {
            // Access the Documents Library folder
            StorageFolder documentsFolder = KnownFolders.DocumentsLibrary;
            StorageFolder currentFolder = await documentsFolder.GetFolderAsync(folders[0]);

            // Navigate through each folder in the array
            for (int i = 1; i < folders.Length; i++)
            {
                currentFolder = await currentFolder.GetFolderAsync(folders[i]);
            }

            // Retrieve the file and read its content
            StorageFile file = await currentFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(file);
        }
        catch (Exception ex)
        {
            // Log the error and rethrow it
            Debug.LogError($"Error loading file: {ex.Message}");
            throw;
        }
    }
#endif
}
