using ejemplolab1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
using ejemplolab1.Controllers;

namespace ejemplolab1.DBContest
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public List<Players> Players = new List<Players>();
        public List<string> Ids = new List<string>();
        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {

            get
            {

                if (Instance == null)
                {
                    lock (syncRoot)
                    {

                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }


    }
}