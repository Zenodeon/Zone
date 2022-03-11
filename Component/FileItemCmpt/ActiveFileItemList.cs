using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;
using System.Text;
using Zone.View;

namespace Zone.Component.FileItemCmpt
{
    public class ActiveFileItemList<T> : BindingList<T> where T : ContentPresenter
    {
        private SortedList<int, FileItem> pairs = new SortedList<int, FileItem>();

        public void Initialize()
        {
            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;
        }

        public void Add(FileItem item)
        {
            int id = item.id;
            pairs.Add(id, item);

            int i = pairs.IndexOfKey(id);
            this.Insert(i, (T)item.frame);
        }

        public void Remove(FileItem item)
        {
            pairs.Remove(item.id);
            base.Remove((T)item.frame);
        }

        public void Add(List<FileItem> itemList)
        {
            this.RaiseListChangedEvents = false;

            foreach (FileItem item in itemList)
                Add(item);

            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public void Remove(List<FileItem> itemList)
        {
            this.RaiseListChangedEvents = false;

            foreach (FileItem item in itemList)
                Remove(item);

            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public new void Clear()
        {
            pairs.Clear();
            base.Clear();
        }


        [Obsolete] public new void Add(T item) { }
        [Obsolete] public new void Remove(T item) { }
    }
}
