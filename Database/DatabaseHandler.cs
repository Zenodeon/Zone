using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Zone.FileInterface;
using System.Threading;

namespace Zone.Database
{
    public class DatabaseHandler
    {
        public static DatabaseHandler activeDatabase;

        string testPath = @"D:\TestSite\TestDB\test.db";

        public string dbPath { get; private set; }

        private LiteDatabase database;

        private bool dbConnected = false;

        public DatabaseHandler(bool cleanDB = false)
        {
            this.dbPath = testPath;
            activeDatabase = this;

            if (cleanDB)
                TryDeleteOldDB();

            EstablishDBConnection();

            //if (EstablishDBConnection(out LiteDatabase database))
            //{
            //    var col = database.GetCollection<ZFInfo>("testColl");

            //    for (int i = 0; i < 10; i++)
            //    {
            //        aaaa(i, col);
            //        database.Checkpoint();
            //    }

            //    database.Checkpoint();
            //}
        }

        public void CloseDB()
        {
            database.Dispose();
            dbConnected = false;
        }

        private void aaaa(int idd, ILiteCollection<ZFInfo> col)
        {
            string id = DateTime.Now.ToString("s:fff");
            var info = new ZFInfo
            {
                md5 = id
            };
            col.Insert(idd, info);
        }

        public DatabaseHandler(string dbPath, bool cleanDB = false)
        {
            this.dbPath = dbPath;
            activeDatabase = this;

            if (cleanDB)
                TryDeleteOldDB();
        }

        private void TryDeleteOldDB()
        {
            if(File.Exists(dbPath))
                File.Delete(dbPath);
        }

        private void EstablishDBConnection()
        {
            try
            {
                database = new LiteDatabase(dbPath);
                dbConnected = true;
            }
            catch
            {
                database = null;
                dbConnected = false;
            }
        }

        public void AddMetadata(ZoneMetadata metadata)
        {
            lock (database)
            {
                var col = database.GetCollection<ZoneMetadata>("ZoneFiles");
                col.Insert(metadata.fileMD5, metadata);
                database.Checkpoint();
            }
        }

        public bool MetadataExists(ZoneMetadata metadata)
        {
            var col = database.GetCollection<ZoneMetadata>("ZoneFiles");
            return col.Exists(Query.EQ("_id", metadata.fileMD5));
        }

        public void testadd()
        {
            var col = database.GetCollection<ZFInfoTestClass>("ZoneFiles");

            for (int i = 0; i < 5; i++)
            {
                string id = i + "" + i * i * i * i;
                var info = new ZFInfoTestClass
                {
                    testField = id
                };

                col.Insert(info.testField, info);
            }
            database.Checkpoint();
        }
        
        public enum Collection
        {
            ZoneFiles,
            testColl
        }

        public class ZFInfoTestClass
        { 
            public string testField { get; set; }
        }
    }
}
