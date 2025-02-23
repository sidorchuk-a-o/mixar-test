using System;

namespace AD.Services.Router
{
    public static class DisposableExtensions
    {
        public static void AddTo<T>(this T vm, ViewModel targetVM)
            where T : ViewModel
        {
            if (vm == null) throw new ArgumentNullException("vm");
            if (targetVM == null) throw new ArgumentNullException("targetVM");

            targetVM.Add(relatedVM: vm);
        }

        public static void AddTo(this IDisposable disposable, ViewModel vm)
        {
            vm.Add(disposable);
        }
    }
}