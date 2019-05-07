using System;
using System.IO;

namespace spam_researcher
{
    class Program
    {
        static string ProgramVersion = "1.0.0.0";
        static string PathCartella = "";
        static string FirstPathCartellaSpam = "";
        static string PathCartellaSpam = "";
        static string SpamCharacter = "";
        static int SpamFilesCounter = 0;
        static bool RemovalAllowed;

        static void Main(string[] args)
        {
            PathCartella = "C:\\Users\\mario\\Documents\\Question Game";
            SpamCharacter = "_";
            RemovalAllowed = false;

            Console.Title = "\"spam-researcher\" version " + ProgramVersion + " by marioglsub";
            Console.WriteLine("Spam files will be searched only in \"" + Path.GetFileName(PathCartella) + "\" folder.");
            Console.WriteLine("");
            Console.WriteLine("The character to be searched is \"" + SpamCharacter + "\" (Placed at the beginning of the name).");
            if (RemovalAllowed)
            {
                Console.WriteLine("");
                Console.WriteLine("########################################### WARNING: REMOVAL IS ALLOWED ###########################################");
            }
            Console.ReadLine();

            if (Directory.Exists(PathCartella))
            {
                PathCartellaSpam = PathCartella + " - Spam";
                FirstPathCartellaSpam = PathCartellaSpam;
                if (!Directory.Exists(PathCartellaSpam))
                {
                    Directory.CreateDirectory(PathCartellaSpam);
                }
                else
                {
                    Console.WriteLine("Spam folder \"" + PathCartellaSpam + "\" already exists.");
                    Console.WriteLine("");
                    Console.WriteLine("You press X (close) button to quit this program and delete the spam folder before launching the program again.");
                    Console.WriteLine("");
                    Console.ReadLine();
                }
                SearchInThisFolder(PathCartella, PathCartellaSpam);
            }
            else
            {
                Console.WriteLine("The \"" + Path.GetFileName(PathCartella) + "\" folder not exists.");
            }
            Console.WriteLine("\nYou press X (close) button to quit this program ...");
            Console.ReadLine();
        }

        static void SearchInThisFolder(string PathCartella, string PathCartellaSpam)
        {
            //Console.WriteLine(PathCartella + " folder exists.");
            string[] directoryEntries = Directory.GetFileSystemEntries(PathCartella);
            int ContatoreDiCartelle = 0;
            int ContatoreDiFiles = 0;
            string newPathCartellaSpam = PathCartellaSpam;
            if (directoryEntries.Length > 0)
            {
                //Console.WriteLine(PathCartella + " folder contains " + directoryEntries.Length.ToString() + " entries."); Console.WriteLine("");
                foreach (string PathDellaCartella in directoryEntries)
                {
                    if (Directory.Exists(PathDellaCartella))
                    {
                        //Console.WriteLine(PathDellaCartella + " folder exists.");
                        //if (PathDellaCartella != FirstPathCartella)
                        {
                            newPathCartellaSpam = PathCartellaSpam + "\\" + Path.GetFileName(PathDellaCartella);
                            if (!Directory.Exists(newPathCartellaSpam))
                            {
                                //Console.WriteLine(newPathCartellaSpam + " folder is created.");
                                Directory.CreateDirectory(newPathCartellaSpam);
                            }
                            //Console.WriteLine("\nYou press an any key to continue or you close this window ...");Console.ReadLine();
                            SearchInThisFolder(PathDellaCartella, newPathCartellaSpam);
                        }
                        ContatoreDiCartelle = ContatoreDiCartelle + 1;
                    }
                    if (File.Exists(PathDellaCartella))
                    {
                        ContatoreDiFiles = ContatoreDiFiles + 1;
                    }
                }
                //Console.WriteLine("");
                if (ContatoreDiFiles > 0)
                {
                    foreach (string PathNameDelFile in directoryEntries)
                    {
                        //Console.WriteLine("-");
                        if (File.Exists(PathNameDelFile) && Path.GetFileName(PathNameDelFile).Substring(0, SpamCharacter.Length) == SpamCharacter)
                        {
                            if (PathCartellaSpam == FirstPathCartellaSpam)
                            {
                                newPathCartellaSpam = FirstPathCartellaSpam;
                            }
                            string PathNameDelFileDaCopiare = newPathCartellaSpam + "\\" + Path.GetFileName(PathNameDelFile);

                            if (!File.Exists(PathNameDelFileDaCopiare))
                            {
                                SpamFilesCounter = SpamFilesCounter + 1;
                                //Console.WriteLine(Path.GetFileName(PathNameDelFile) + " copy in " + PathNameDelFileDaCopiare);
                                File.Copy(PathNameDelFile, PathNameDelFileDaCopiare);
                                Console.WriteLine(PathNameDelFileDaCopiare.Substring(40) + " (" + SpamFilesCounter + ")");
                                if (RemovalAllowed)
                                {
                                    File.Delete(PathNameDelFile);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //Console.WriteLine("The folder \"" + PathCartella + "\" is empty.");
            }
        }
    }
}
