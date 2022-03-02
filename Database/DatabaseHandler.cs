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

        private Dictionary<Collection, Queue<IDatabaseItem>> itemQueue = new Dictionary<Collection, Queue<IDatabaseItem>>();

        private bool DBConnected = false;

        public DatabaseHandler(bool cleanDB = false)
        {
            this.dbPath = testPath;
            activeDatabase = this;

            if (cleanDB)
                TryDeleteOldDB();

            if (EstablishDBConnection(out LiteDatabase database))
            {
                var col = database.GetCollection<ZFInfo>("testColl");

                for (int i = 0; i < 10; i++)
                {
                    aaaa(i, col);
                }

                //database.Checkpoint();
            }
        }

        private void aaaa(int idd, ILiteCollection<ZFInfo> col)
        {
            string id = DateTime.Now.ToString("s:fff");
            var info = new ZFInfo
            {
                md5 = id
            };
            col.Insert(idd, info);
            //Thread.Sleep(1);
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

        private bool EstablishDBConnection(out LiteDatabase database)
        {
            try
            {
                //LiteDB.Engine.LiteEngine liteEngine = new LiteDB.Engine.LiteEngine(dbPath);   
                //database = new LiteDatabase(liteEngine, mapper: null, disposeOnClose: false);  
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
            //QueueItemToAdd(Collection.ZoneFiles, metadata);
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
                            md5 = id
                        };

                        col.Insert(info.md5, info);
                    }
                }
        }

        public void testad()
        {
            if (EstablishDBConnection(out LiteDatabase database))
                using (database)
                {
                    var col = database.GetCollection<ZFInfo>("testColl");

                    string id = DateTime.Now.ToString();
                    var info = new ZFInfo
                    {
                        md5 = id
                    };
                    col.Insert(info);
                }
        }

        private void QueueItemToAdd(Collection collection, IDatabaseItem item)
        {
            lock (itemQueue)
            {
                if (itemQueue.ContainsKey(collection))
                    itemQueue[collection].Enqueue(item);
                else
                {
                    Queue<IDatabaseItem> queue = new Queue<IDatabaseItem>();
                    queue.Enqueue(item);
                    itemQueue.Add(collection, queue);

                    //UpdateDB();
                }
            }
        }

        private void UpdateDB()
        {
            
        }

        private void ProccessChanges()
        {
            if (EstablishDBConnection(out LiteDatabase database))
                using (database)
                {
                }
        }

        private void AddQueuedItems(LiteDatabase database)
        {
        }

        public enum Collection
        {
            ZoneFiles,
            testColl
        }

        public class ZFInfo : IDatabaseItem
        { 
            public string md5 { get; set; }
        }
    }
}
