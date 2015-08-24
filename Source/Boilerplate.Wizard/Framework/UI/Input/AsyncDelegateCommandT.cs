namespace Framework.UI.Input
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// This class allows delegating the commanding logic to methods passed as parameters,
    /// and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public sealed class AsyncDelegateCommand<T> : AsyncCommand<T>
    {
        #region Fields

        private readonly Func<T, Task> execute;
        private readonly Func<T, bool> canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public AsyncDelegateCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public AsyncDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Command Members

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(T parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute(parameter);
            }

            return true;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public override Task Execute(T parameter)
        {
            Task task = null;

            if (this.execute == null)
            {
                task = Task.FromResult<object>(null);
            }
            else
            {
                task = this.execute(parameter);
            }

            return task;
        }

        #endregion
    }
}
