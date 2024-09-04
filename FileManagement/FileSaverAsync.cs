#if ENABLE_WINMD_SUPPORT
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using UnityEngine;
#endif

using UnityEngine;

/// <summary>
/// Class for handling asynchronous file operations on the UWP (Universal Windows Platform).
/// </summary>
public class FileSaverAsync : MonoBehaviour
{
#if ENABLE_WINMD_SUPPORT

    /// <summary>
    /// Enumeration that defines the possible operations on a file.
    /// </summary>
    public enum FileOperation
    {
        /// <summary>
        /// Write to a file (overwrites existing content).
        /// </summary>
        Write,
        
        /// <summary>
        /// Append content to the end of an existing file.
        /// </summary>
        Append
    }

    /// <summary>
    /// Saves a text file to a specified folder path within the documents library.
    /// <para>
    /// Note: You can save to other known folders (e.g., PicturesLibrary) by modifying the known folder variable 
    /// and updating the `SaveFileToFolder` method accordingly. Ensure that the appropriate capability is added to
    /// your application for accessing these folders.
    /// </para>
    /// </summary>
    /// <param name="folders">Path of folders where the file will be saved, starting from the documents folder.</param>
    /// <param name="fileName">Name of the file to save.</param>
    /// <param name="content">Content to write into the file.</param>
    /// <param name="operation">Operation to perform on the file (write or append).</param>
    /// <returns>An asynchronous task representing the save operation.</returns>
    public async Task SaveFileToFolder(string[] folders, string fileName, string content, FileOperation operation)
    {
        // Get the user's documents folder.
        StorageFolder documentsFolder = KnownFolders.DocumentsLibrary;
        StorageFolder currentFolder = documentsFolder;

        // Navigate or create the specified folder path.
        foreach (string folder in folders)
        {
            currentFolder = await currentFolder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);
        }

        // Create or open the file in the final folder of the path.
        StorageFile file = await currentFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

        // Execute the specified operation on the file.
        switch (operation)
        {
            case FileOperation.Write:
                await FileIO.WriteTextAsync(file, content);
                break;

            case FileOperation.Append:
                await FileIO.AppendTextAsync(file, content + Environment.NewLine);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
        }
    }

    /// <summary>
    /// Saves a photo to a specified folder path within the documents library.
    /// <para>
    /// Note: You can save to other known folders (e.g., PicturesLibrary) by modifying the known folder variable 
    /// and updating the `SavePhotoToFolder` method accordingly. Ensure that the appropriate capability is added to
    /// your application for accessing these folders.
    /// </para>
    /// </summary>
    /// <param name="folders">Path of folders where the photo will be saved, starting from the documents folder.</param>
    /// <param name="photoFileName">File of the photo to be moved.</param>
    /// <returns>An asynchronous task representing the photo save operation.</returns>
    public async Task SavePhotoToFolder(string[] folders, StorageFile photoFileName)
    {
        // Get the user's documents folder.
        StorageFolder documentsFolder = KnownFolders.DocumentsLibrary;
        StorageFolder currentFolder = documentsFolder;

        // Navigate or create the specified folder path.
        foreach (string folder in folders)
        {
            currentFolder = await currentFolder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);
        }

        // Move the photo to the final folder in the path, generating a unique name if a collision occurs.
        await photoFileName.MoveAsync(currentFolder, photoFileName.Name, NameCollisionOption.GenerateUniqueName);
    }
#endif
}
