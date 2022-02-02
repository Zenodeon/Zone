using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.CustomClass
{
    public class ThreadQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();

        public int Count
        {
            get
            {
                lock (queue)
                    return queue.Count;
            }
        }

        public void Enqueue(T item)
        {
            lock (queue)
                queue.Enqueue(item);
        }

        public T Dequeue()
        {
            lock (queue)
                return queue.Dequeue();
        }
    }
}
