﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Models
{
    /// <summary>
    /// класс окна событий
    /// </summary>
    public class ActionWindowData
    {
        public string Text { get; set; }

        public string Date { get; set; }

        public ActionWindowData(string text, string date)
        {
            Text = text;
            Date = date;
        }

        public ActionWindowData()
        {

        }
    }
}
