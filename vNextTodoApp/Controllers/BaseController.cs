using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace vNextTodoApp
{
    public class BaseController : Controller
    {
        [Activate]
        protected IModelBindersProvider ModelBindersProvider { get; set; }
        [Activate]
        public IModelMetadataProvider ModelMetadataProvider { get; set; }
        [Activate]
        public IEnumerable<IModelValidatorProvider> ValidatorProviders { get; set; }
        [Activate]
        public IEnumerable<IValueProviderFactory> ValueProviderFactories { get; set; }
        private CompositeModelBinder _modelBinder;
        private CompositeValueProvider _compositeValueProvider;
        private bool _modelBinderInitialized;
        private object _modelBinderInitLocker = new object();

        public BaseController()
        {

        }

        protected Task<bool> TryUpdateModelAsync<TModel>(TModel model)
        {
            LazyInitializer.EnsureInitialized(ref _modelBinder, ref _modelBinderInitialized, ref _modelBinderInitLocker, () =>
            {
                var factoryContext = new ValueProviderFactoryContext(Context, ActionContext.RouteData.Values);
                _compositeValueProvider = new CompositeValueProvider(ValueProviderFactories.Select(vpf => vpf.GetValueProvider(factoryContext)).Where(x => x != null));
                return new CompositeModelBinder(ModelBindersProvider);
            });

            var modelMetadata = ModelMetadataProvider.GetMetadataForType(
                       modelAccessor: null,
                       modelType: model.GetType());

            var bindingContext = new ModelBindingContext
            {
                MetadataProvider = ModelMetadataProvider,
                ModelMetadata = modelMetadata,
                Model = model,
                ModelState = ModelState,
                ValidatorProviders = ValidatorProviders,
                ModelBinder = _modelBinder,
                HttpContext = Context,
                ValueProvider = _compositeValueProvider
            };

            return _modelBinder.BindModelAsync(bindingContext);
        }
    }
}