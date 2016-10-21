using System;
using System.Collections.Generic;

namespace QuizEngine.Infrastructure
{
    public class BaseObjectContainer<T> : IDisposable where T: PlatformContext
    {
        #region Members
        private Dictionary<Type, BaseObject<T>> _baseObjectInstances = new Dictionary<Type, BaseObject<T>>();
        private T _context;
        private bool disposed;
        #endregion Members

        #region Constructor
        public BaseObjectContainer(T context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// This method is the generic method used to return a Base object.
        /// </summary>
        /// <typeparam name="TObject">The type is generic and needs to be specified when calling this class.</typeparam>
        /// <returns><see cref="QuizEngine.Infrastructure.BaseObject<T>"/></returns>
        public TObject GetObject<TObject>() where TObject : BaseObject<T>
        {
            Type genericBaseObjectType = typeof(TObject);
            if (_baseObjectInstances.ContainsKey(genericBaseObjectType))
            {
                return (TObject)_baseObjectInstances[genericBaseObjectType];
            }
            TObject baseObject = (TObject)Activator.CreateInstance(genericBaseObjectType, _context);
            _baseObjectInstances.Add(genericBaseObjectType, baseObject);
            return baseObject;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_baseObjectInstances != null)
                {
                    foreach (BaseObject<T> baseObject in _baseObjectInstances.Values)
                    {
                        baseObject.Dispose();
                    }
                    _baseObjectInstances = null;
                }

                if (_context != null)
                    _context = null;
            }
            disposed = true;
        }

        ~BaseObjectContainer()
        {
            Dispose(false);
        }
        #endregion Methods
    }
}
