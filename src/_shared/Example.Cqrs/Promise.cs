namespace Example.Cqrs;
    public class PromiseSuccessEventArg : EventArgs
    {
        
    }
    interface IPromise<out T>
    {
        T Result { get; }
        void OnSuccess();
        void OnError();
    }
    
    interface IPromiseAsync<out T>
    {
        T Result { get; }
        Task OnSuccessAsync(CancellationToken cancellationToken);
        Task OnErrorAsync(CancellationToken cancellationToken);
    }
    public class Promise<T> : IPromise<T>
    {
        private Action _onSuccess;
        private Action _onError;

        public EventHandler<PromiseSuccessEventArg> OnSuccessEvent;

        public Promise(T result, Action onSuccess = null, Action onError = null)
        {
            _onSuccess = onSuccess;
            _onError = onError;
            Result = result;
        }

        public T Result { get; }

        public void OnSuccess()
        {
            _onSuccess?.Invoke();
            _onSuccess = null;
            OnSuccessEvent?.Invoke(this, new PromiseSuccessEventArg());
        }

        public void OnError()
        {
            _onError?.Invoke();
            _onError = null;
        }
    }
    
    public class PromiseAsync<T> : IPromiseAsync<T>
    {
        private Func<CancellationToken, Task> _onSuccessAsync;
        private Func<CancellationToken, Task> _onErrorAsync;

        public PromiseAsync(T result, Func<CancellationToken, Task> onSuccessAsync = null,  Func<CancellationToken, Task> onErrorAsync = null)
        {
            _onSuccessAsync = onSuccessAsync;
            _onErrorAsync = onErrorAsync;
            Result = result;
        }

        public T Result { get; }

        public async Task OnSuccessAsync(CancellationToken cancellationToken)
        {
            await _onSuccessAsync?.Invoke(cancellationToken);
            _onSuccessAsync = null;
        }

        public async Task OnErrorAsync(CancellationToken cancellationToken)
        {
            await _onErrorAsync?.Invoke(cancellationToken);
            _onErrorAsync = null;
        }
    }
