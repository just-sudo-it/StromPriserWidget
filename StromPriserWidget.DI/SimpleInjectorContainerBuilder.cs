using System;
using System.Collections.Generic;
using System.Text;

namespace StromPriserWidget.DI
{
    public class SimpleInjectorContainerBuilder
    {
        public List<SimpleInjectorContainerModule> _modules;
        /*
        private OmniContainerBuilder()
        {
            _modules = new List<OmniContainerModule>();
        }

        public static OmniContainerBuilder Create()
        {
            return new OmniContainerBuilder();
        }

        public OmniContainerBuilder RegisterModule(OmniContainerModule ninjectModule)
        {
            _modules.Add(ninjectModule);
            return this;
        }

        public OmniContainerBuilder RegisterModules(IEnumerable<OmniContainerModule> ninjectModules)
        {
            _modules.AddRange(ninjectModules);
            return this;
        }

        public void Build()
        {
            OmniContainer.Initialize(_modules);
        }
    }*/
    }
}
