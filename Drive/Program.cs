using System;
using System.IO;
using System.Windows.Forms;


class Test
{
    public static void Main()
    {
        string sourceDirectory = @"F:\";
        string targetDirectory = @"D:\pdHAck";

        DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
        DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

        


        DriveInfo[] allDrives = DriveInfo.GetDrives();
        DialogResult result;

        foreach (DriveInfo d in allDrives)
        {
            Console.WriteLine("Drive {0}", d.Name);
            Console.WriteLine("  Drive type: {0}", d.DriveType);
            if ( (d.IsReady == true) && (d.DriveType == DriveType.Removable) && (d.AvailableFreeSpace < 32000000000)) 
            {
                result = MessageBox.Show("Do you want to continue?" ,"", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine("Hello User!!");
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);

                    CopyAll(diSource, diTarget);
                }
                else
                {
                    break;
                }
            }
        }
        Console.ReadKey();
    }

    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        if (source.FullName.ToLower() == target.FullName.ToLower())
        {
            return;
        }

        // Check if the target directory exists, if not, create it.
        if (Directory.Exists(target.FullName) == false)
        {
            Directory.CreateDirectory(target.FullName);
        }

        // Copy each file into it's new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            Console.WriteLine(@"Processing {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
}
