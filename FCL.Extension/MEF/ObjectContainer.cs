using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using FCL.Extentsion.Reflection;

namespace FCL.Extentsion.MEF
{
    public static class ObjectContainer
    {
        private static CompositionContainer _container;
        private static object _guard = new object();

        public static CompositionContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Initialize with a CompositionContainer object.
        /// </summary>
        /// <param name="container"></param>
        public static void Initialize(CompositionContainer container)
        {
            lock (_guard)
            {
                EnsureInitializable();
                _container = container;
            }
        }

        /// <summary>
        /// Default Initialization method, the working directory of the executing assembly will be set the export catelog source.
        /// </summary>
        public static void Initialize(bool isThreadSafe = true)
        {
            lock (_guard)
            {
                EnsureInitializable();
                _container = new CompositionContainer(new DirectoryCatalog(AssemblyExtension.GetWorkingDirectory()), isThreadSafe);
            }
        }

        private static void EnsureInitializable()
        {
            if (Container != null)
            {
                throw new InvalidOperationException("Already initialized!");
            }
        }
    }
}
