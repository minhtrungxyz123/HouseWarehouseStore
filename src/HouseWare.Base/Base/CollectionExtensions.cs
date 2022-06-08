﻿using FastMember;
using System.Data;

namespace HouseWare.Base
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> initial, IEnumerable<T> other)
        {
            if (other == null)
                return;

            if (initial is List<T> list)
            {
                list.AddRange(other);
                return;
            }

            foreach (var local in other)
            {
                initial.Add(local);
            }
        }

        public static SyncedCollection<T> AsSynchronized<T>(this ICollection<T> source)
        {
            return AsSynchronized(source, new object());
        }

        public static SyncedCollection<T> AsSynchronized<T>(this ICollection<T> source, object syncRoot)
        {
            if (source is SyncedCollection<T> sc)
            {
                return sc;
            }

            return new SyncedCollection<T>(source, syncRoot);
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count == 0;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data, params string[] members)
        {
            var table = new DataTable();
            using (var reader = ObjectReader.Create(data, members))
            {
                table.Load(reader);
            }
            return table;
        }
    }
}