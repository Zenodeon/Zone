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

        public void Enqueue(T item)
        {
            lock (queue)
                queue.Enqueue(item);
        }

        public bool TryDequeue(out T t)
        {
            lock (queue)
            {
                if (queue.Count != 0)
                {
                    t = queue.Dequeue();
                    return true;
                }
                else
                {
                    t = default(T);
                    return false;
                }
            }
        }
    }
}
