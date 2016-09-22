﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace move_all
{
    class Program
    {
        static void help()
        {
            Console.WriteLine("move-all folder");
            Console.WriteLine("Move all files and folders from current location to the named folder");
        }

        static int Main(string[] args)
        {
            var options = true;
            var dirs = new List<string>();
            foreach (var arg in args)
            {
                var s = arg;
                if (!options)
                {
                    dirs.Add(s);
                    continue;
                }
                if (s == "")
                    continue;
                if (s == "--")
                {
                    options = false;
                    continue;
                }
                if (Path.DirectorySeparatorChar == '\\' && s[0] == '/')
                    s = "-" + s.Substring(1);
                if (s[0] != '-')
                {
                    dirs.Add(s);
                    continue;
                }
                if (s.StartsWith("--"))
                    s = s.Substring(1);
                switch (s)
                {
                    case "-?":
                    case "-h":
                    case "-help":
                        help();
                        return 0;
                    case "-V":
                    case "-v":
                    case "-version":
                        Console.WriteLine("dir-date {0}", Assembly.GetExecutingAssembly().GetName().Version);
                        return 0;
                    default:
                        Console.WriteLine("{0}: unknown option", arg);
                        return 1;
                }
            }
            if (dirs.Count != 1)
            {
                help();
                return 0;
            }
            var dir = dirs[0];
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("{0}: not an existing folder", dir);
                return 1;
            }
            foreach (string entry in Directory.GetDirectories("."))
                Directory.Move(entry, dir + "/" + Path.GetFileName(entry));
            foreach (string entry in Directory.GetFiles("."))
                Directory.Move(entry, dir + "/" + Path.GetFileName(entry));
            return 0;
        }
    }
}
