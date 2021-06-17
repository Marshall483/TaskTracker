using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions
{
    public interface IConstructor<TView, TModel>
    {
        public TView ConsructView(TModel model);

        public TModel ConstructModel(TView view);
    }
}
