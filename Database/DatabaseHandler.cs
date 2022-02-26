using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Zone.FileInterface;

namespace Zone.Database
{
    public class DatabaseHandler
    {
        public static DatabaseHandler activeDatabase;

        string testPath = @"D:\TestSite\TestDB\test.db";

        public string dbPath { get; private set; }

        public DatabaseHandler()
        {
            this.dbPath = testPath;
            activeDatabase = this;
        }

        public DatabaseHandler(string dbPath)
        {
            this.dbPath = dbPath;
            activeDatabase = this;
        }

        private bool EstablishDBConnection(out LiteDatabase database)
        {
            try
            {
                database = new LiteDatabase(dbPath);
                return true;
            }
            catch
            {
                database = null;
                return false;
            }
        }

        public void AddMetadata(ZoneMetadata metadata)
        {
            if (EstablishDBConnection(out LiteDatabase database))
                using (database)
                {
                    var col = database.GetCollection<ZoneMetadata>("ZoneFiles");
                    col.Insert(metadata.fileMD5, metadata);
                }
        }

        public void testadd()
        {
            if (EstablishDBConnection(out LiteDatabase database))
                using (database)
                {
                    var col = database.GetCollection<ZFInfo>("ZoneFiles");

                    for (int i = 0; i < 5; i++)
                    {
                        string id = i + "" + i * i * i * i;
                        var info = new ZFInfo
                        {
                            md5 = id,
                            tags = new List<string>(),
                        };

                        col.Insert(info.md5, info);
                    }
                }
        }

        public class ZFInfo //Zone File Info
        {
            public string md5 { get; set; }
            public List<string> tags { get; set; }
        }
    }
}
