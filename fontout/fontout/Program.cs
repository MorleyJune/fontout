using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace fontout
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("準備OK? y/n ");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
                if (cki.Key.ToString() == "n")
                {
                    Environment.Exit(0);
                }
            } while (cki.Key.ToString() == "y");
            Console.WriteLine("\n");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string[] files = Directory.GetFiles(@"./", "*.exa");
            Encoding sjisEnc = Encoding.GetEncoding("shift_jis");
            if ( files.Length == 0 )
            {
                Console.WriteLine("該当ファイルが見つかりません。\n何かキーを押して終了");
            }
            else
            {
                string[] lines = new string[files.Length];
                string line; int i = 0;
                foreach( var file in files)
                {
                    StreamReader stream = new StreamReader(@file, sjisEnc);
                    while ((line = stream.ReadLine()) != null)
                    {
                        if(line.Contains("font"))
                            lines[i] = line;
                    }
                    i++;
                }
                string prefix = "./";
                string suffix = ".exa";
                string font = "font=";
                for(i = 0; i < files.Length; i++)
                {
                    files[i] = Regex.Replace(files[i], prefix, "");
                    files[i] = Regex.Replace(files[i], suffix, "");
                    lines[i] = Regex.Replace(lines[i], font, "");
                }
                StreamWriter writer = new StreamWriter("./exaList.csv", false, sjisEnc);
                for( i = 0; i < files.Length; i++)
                {
                    Console.WriteLine(files[i] + " " + lines[i]);
                    writer.WriteLine(files[i] + "," + lines[i]);
                }
                writer.Close();
                Console.WriteLine("書き込みが終わりました。\n何かキーを押して終了");
            }
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
