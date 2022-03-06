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

        public string dbPath { get; private set; }

        private LiteDatabase database;

        private bool dbConnected = false;

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

        public void CloseDB()
        {
            database.Dispose();
            dbConnected = false;
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
  
        public enum Collection
        {
            ZoneFiles,
            ZoneDetails,
            testColl
        }

        #region Test Region

        string testPath = @"D:\TestSite\TestDB\test.db";

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

        private void aaaa(int idd, ILiteCollection<ZFInfoTestClass> col)
        {
            string id = DateTime.Now.ToString("s:fff");
            var info = new ZFInfoTestClass
            {
                testField = id
            };
            col.Insert(idd, info);
        }

        public class ZFInfoTestClass
        {
            public string testField { get; set; }
        }

        #endregion
    }
}
