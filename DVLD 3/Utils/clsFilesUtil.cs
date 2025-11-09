using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_3.Utils
{
    public static class clsFilesUtil
    {
        public static bool CreateGlobalInformationsFilesAndDirectoresIfNotExist(string DirectoryPath,string FilePath)
        {
            bool AreFilesBeCreated = false;

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
                AreFilesBeCreated = true;
            }

            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
                AreFilesBeCreated = true;
            }

            return AreFilesBeCreated;
        }

        public static void ClearFile(string FilePath)
        {
            File.WriteAllText(FilePath, string.Empty);
        }
    }
}
