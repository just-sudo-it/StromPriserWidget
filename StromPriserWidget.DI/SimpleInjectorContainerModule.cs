using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StromPriserWidget.DI
{
    public abstract class SimpleInjectorContainerModule
    {
        public virtual void Load(){ }
        public abstract void Load(IContainer container);
    }
}
