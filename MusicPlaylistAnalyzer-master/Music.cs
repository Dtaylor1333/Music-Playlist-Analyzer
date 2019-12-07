using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MUSIC
{
    public class Songinfo
    {
        public string Name;
        public string Artist;
        public string Album;
        public string Genre;
        public int Size;
        public int Time;
        public int Year;
        public int Plays;

        public Songinfo(string data, string data1, string data2, string data3, int data4, int data5, int data6, int data7)
        {
            Name = data;
            Artist = data1;
            Album = data2;
            Genre = data3;
            Size = data4;
            Time = data5;
            Year = data6;
            Plays = data7;
        }
        override public string ToString()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}," +
                " Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string exam = null;
            int x;
            List<Songinfo> New = new List<Songinfo>();
            try
            {
                if (File.Exists($"SampleMusicPlaylist.txt") == false)
                {
                    Console.WriteLine("SampleMusicPlaylist text File does not exist!Erorr");
                }
                else
                {
                    StreamReader de = new StreamReader($"SampleMusicPlaylist.txt");
                    x = 0;
                    string statment = de.ReadLine();
                    while ((statment = de.ReadLine()) != null)
                    {
                        x += 1;
                        try
                        {
                            string[] words = statment.Split('\t');

                            if (words.Length < 8)
                            {
                                Console.Write("Record doesnʼt have the correct amount of characters");
                                Console.WriteLine($"Row {x} contains {words.Length}  values. You need 8 characters.");

                                break;
                            }
                            else
                            {
                                Songinfo data = new Songinfo((words[0]), (words[1]), (words[2]), (words[3]), Int32.Parse(words[4]),
                                    Int32.Parse(words[5]), Int32.Parse(words[6]), Int32.Parse(words[7]));

                                New.Add(data);
                            }
                        }
                        catch
                        {
                            Console.Write("Error from reading lines from playlist data file!");

                            break;
                        }
                    }
                    de.Close();
                }
            }
            catch (Exception d)
            {
                Console.WriteLine("Playlist data file canʼt be opened!");
            }
            try
            {
                Songinfo[] songs = New.ToArray();
                using (StreamWriter write = new StreamWriter("MusicPlaylistReport.txt"))
                {
                    Console.WriteLine("|=====================|\n Music Playlsit Report\n|=====================|");
                    Console.WriteLine("==========");
                    var Songs200 = from song in songs where song.Plays >= 200 select song;
                    exam += "\nSongs that received 200 or more plays:\n \n";
                    foreach (Songinfo song in Songs200)
                    {
                        exam += song + "\n";
                    }
                    Console.WriteLine("==========");
                    var GenreAlternative = from song in songs where song.Genre == "Alternative" select song;
                    x = 0;
                    foreach (Songinfo song in GenreAlternative)
                    {
                        x++;
                    }
                    exam += $"\nSongs are in the playlist with the Genre of Alternative: {x}\n \n";
                    Console.WriteLine("==========");
                    var HipHopRap = from song in songs where song.Genre == "Hip-Hop/Rap" select song;
                    x = 0;
                    foreach (Songinfo song in HipHopRap)
                    {
                        x++;
                    }
                   exam += $"Number of songs Hip-Hop/Rap: {x}\n \n";
                    Console.WriteLine("==========");
                    var AlbumFishbowl = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
                    exam += "Songs from the album Welcome to the Fishbowl:\n \n";
                    foreach (Songinfo song in AlbumFishbowl)
                    {
                        exam += song + "\n";
                    }
                    Console.WriteLine("==========");
                    var Songs1970 = from song in songs where song.Year < 1970 select song;
                    exam += "\nSongs from before 1970:\n \n";
                    foreach (Songinfo song in Songs1970)
                    {
                        exam += song + "\n";
                    }
                    Console.WriteLine("==========");
                    var Names85 = from song in songs where song.Name.Length > 85 select song.Name;
                    exam += "\nSong names longer than 85 characters:\n \n";
                    foreach (string name in Names85)
                    {
                        exam += name + "\n";
                    }
                    Console.WriteLine("==========");
                    var longestSong = from song in songs orderby song.Time descending select song;
                    exam += "\nLongest song:\n \n";
                    exam += longestSong.First();
                    write.Write(exam);
                    write.Close();
                    Console.WriteLine("==========");
                }
                Console.WriteLine("Music Playlist Report file has be created...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Report file canʼt be opened or written to...");
            }
            Console.ReadLine();
        }
    }
}
