using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Zone.Database
{
    public class DatabaseHandler
    {
        string testPath = @"D:\TestSite\TestDB\test.db";

        public string dbPath { get; private set; }

        LiteDatabase db;

        public DatabaseHandler(string dbPath)
        {
            this.dbPath = dbPath;
            db = new LiteDatabase(dbPath);
        }

        public void testadd()
        {
            var col = db.GetCollection<FileBlock>("File");

            for (int i = 0; i < 5; i++)
            {
                string id = i + "" + i * i * i * i;
                var block = new FileBlock
                {
                    fileID = id,
                    tags = new List<string>(),
                };

                col.Insert(block.fileID, block);
            }
        }

        public class FileBlock
        {
            public string fileID { get; set; }
            public List<string> tags { get; set; }
        }
    }
}
