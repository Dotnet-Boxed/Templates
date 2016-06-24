namespace Framework.UI.Input
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// This class allows delegating the commanding logic to methods passed as parameters,
    /// and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    public sealed class AsyncDelegateCommand : AsyncCommand
    {
        #region Fields

        private readonly Func<Task> execute;
        private readonly Func<bool> canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public AsyncDelegateCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncDelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public AsyncDelegateCommand(Func<Task> execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Command Members

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute()
        {
            if (this.canExecute != null)
            {
                return this.canExecute();
            }

            return true;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override Task Execute()
        {
            Task task = null;

            if (this.execute == null)
            {
                task = Task.FromResult<object>(null);
            }
            else
            {
                task = this.execute();
            }

            return task;
        }

        #endregion
    }
}