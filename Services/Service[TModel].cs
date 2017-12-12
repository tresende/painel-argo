using System;
using System.Collections.Generic;
using System.Reflection;
using IBM.FCAGroup.FiatApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IBM.FCAGroup.FiatApp.Services
{
    /// <summary>
    /// Classe para o programador não precisar a todo momento criar nos serviços método como all, get, save etc..
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Services<T> : Service where T : class
    {
        public Services(MainContext context)
            : base(context)
        {
        }

        public Services()
            : base()
        {
        }

        /// <summary>
        /// Retorna todos os regristros
        /// </summary>
        /// <returns>Um item da entidade</returns>
        public IEnumerable<T> All()
        {
            var query = this.context.Set<T>().AsQueryable();
            foreach (var property in this.context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(property.Name);
            }
            return query;
        }

        /// <summary>
        /// Salva um item
        /// </summary>
        /// <param name="item">Item da entidade</param>
        public void Save(T item)
        {
            this.context.Set<T>().Add(item);
            base.Save();
        }

        /// <summary>
        /// Salva vários items
        /// </summary>
        /// <param name="items">Coleção de itens da entidade</param>
        public void Save(IEnumerable<T> items)
        {
            this.context.Set<T>().AddRange(items);
            base.Save();
        }

        public void Update(T item)
        {
            this.context.Set<T>().Attach(item);
            this.context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            base.Save();
        }

        /// <summary>
        /// Deleta um item da entidade
        /// </summary>
        /// <param name="item">Item da entidade</param>
        public void Delete(object id)
        {
            var item = this.Get(id);
            this.context.Set<T>().Remove(item);
            base.Save();
        }

        /// <summary>
        /// Retorna um item pela chave
        /// </summary>
        /// <param name="id">Chave</param>
        /// <returns>Item da entidae</returns>
        public T Get(object id)
        {
            return base.context.Set<T>().Find(id);
        }


        #region Primary Types Validations

        public static bool IsValid(string[] item)
        {
            if (item != null && (item.Count() > 1 || (item.Count() == 1 && item.First() != string.Empty)))
                return true;
            return false;
        }

        public static bool IsValid(int[] item)
        {
            return item != null && (item.Count() > 1 || (item.Count() == 1 && item.First() > 0));
        }

        public static bool IsValid(int?[] item)
        {
            return item != null && (item.Count() > 1 || (item.Count() == 1 && item.First() > 0 && item.FirstOrDefault() != null));
        }

        public static bool IsValid(int item)
        {
            return item > 0;
        }

        public static bool IsValid(int? item)
        {
            return item.HasValue && IsValid(item.Value);
        }

        public static bool IsValid(long[] item)
        {
            return item != null && (item.Count() > 1 || (item.Count() == 1 && item.First() > 0));
        }

        public static bool IsValid(long item)
        {
            return item > 0;
        }

        public static bool IsValid(long? item)
        {
            return item.HasValue && IsValid(item.Value);
        }

        public static bool IsValid(string item)
        {
            return !string.IsNullOrEmpty(item);
        }

        public static bool IsValid(DateTime item)
        {
            return item != DateTime.MinValue;
        }

        public static bool IsValid(DateTime? item)
        {
            return item.HasValue && item != DateTime.MinValue;
        }

        public static bool IsValid(bool? item)
        {
            return item.HasValue && item.Value;
        }

        public static bool IsValid(bool item)
        {
            return item;
        }

        public static bool IsValid(object item)
        {
            return item != null;
        }

        #endregion

    }
}
