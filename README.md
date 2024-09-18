# FileManagement & BuildTools for Unity UWP (HoloLens)

This repository contains Unity scripts designed for file management in UWP (Universal Windows Platform) applications, specifically targeting **HoloLens**, as well as an automation script for configuring the `Package.appxmanifest` file in UWP projects.

## Repository Contents

- **FileManagement/**
  - **FileSaverAsync.cs**: Class for asynchronously saving files and photos in a folder structure within the UWP documents library on HoloLens.
  - **FileLoaderAsync.cs**: Class for asynchronously loading files from a folder structure within the UWP documents library on HoloLens.

- **BuildTools/**
  - **AddCapabilityToXml.cs**: Script that automatically modifies the `Package.appxmanifest` file after building a UWP application for HoloLens in Unity, replacing the `musicLibrary` capability with `documentsLibrary`.

## Using the Scripts

### FileSaverAsync

The `FileSaverAsync` class provides two main methods for use with HoloLens:

1. **Save Text to a File**  
   Allows saving text to a file within a specified folder path on HoloLens.

   ```csharp
   // Example usage
   #if ENABLE_WINMD_SUPPORT
   FileSaverAsync fileSaver = new FileSaverAsync();
   string[] folders = { "MainFolder", "SubFolder" };
   await fileSaver.SaveFileToFolder(folders, "myFile.txt", "File content", FileSaverAsync.FileOperation.Write);
   #endif
    ```
- **folders**: Array of strings representing the folder path within the documents library.
- **fileName**: Name of the file to save.
- **content**: Content to be written to the file.
- **operation**: Operation to perform: `Write` to overwrite or `Append` to add to the end.

2. **Save a Photo to a Folder**  
   Allows saving an image file to a specified folder path on HoloLens.

   ```csharp
   // Example usage
   #if ENABLE_WINMD_SUPPORT
   FileSaverAsync fileSaver = new FileSaverAsync();
   string[] folders = { "MainFolder", "SubFolder" };
   StorageFile photoFile = await obtenerStorageFileParaLaFoto();
   await fileSaver.SavePhotoToFolder(folders, photoFile);
   #endif
- **folders**: Array of strings representing the folder path within the documents library.
- **photoFileName**: The photo file to save. -

### FileLoaderAsync
The `FileLoaderAsync` class allows loading the content of a file as a string from a folder path within the documents library on HoloLens

  ```csharp
  // Example usage
  #if ENABLE_WINMD_SUPPORT
  FileLoaderAsync fileLoader = new FileLoaderAsync();
  string[] folders = { "MainFolder", "SubFolder" };
  string fileContent = await fileLoader.LoadFileFromFolder(folders, "myFile.txt");
  #endif
  ```
- **folders**: Array of strings representing the folder path within the documents library.
- **fileName**: Name of the file to load.

### AddCapabilityToXml
This script runs automatically after building a UWP project for HoloLens in Unity and modifies the `Package.appxmanifest` file to replace the `musicLibrary` capability with `documentsLibrary`.

**Usage:**

Ensure the `musicLibrary` capability is enabled in the UWP Build Settings in Unity.
After building, the script will automatically replace `musicLibrary` with `documentsLibrary` in the `Package.appxmanifest` file.
This script is particularly useful for HoloLens projects due to a limitation in Unity where documentsLibrary does not appear as an option in the Build Settings.

### Contributors

Special thanks to the following people who contributed to this project:

- [Collaborator's Name](https://github.com/username) 
- [Another Collaborator](https://github.com/username2)

### Founding Sources 

This work received financial support from the OVERSIGHT project (PID2022-138526OB-I00) funded by MICIU/AEI/10.13039/501100011033, UE. 
Mercedes Solla acknowledges the grant RYC2019–026604–I funded by MICIU/AEI/10.13039/501100011033 and by “ESF Investing in your future”.
Jesús Balado acknowledges the grant RYC2022-038100-I funded by MICIU/AEI/10.13039/501100011033 and FSE+.
