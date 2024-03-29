﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrishSlangPuzzle
{
    public class User
    {
        public int userId { get; set; }
        public string name { get; set; }
        public int points { get; set; }
        

        public User()
        {

        }

        public User(int userId, string name, int points)
        {
            this.userId = userId;
            this.name = name;
            this.points = points;
        }

        public override string ToString()
        {
            return String.Format("{0}\t\t\t\t\t{1}", name, points);
        }
    }
}
