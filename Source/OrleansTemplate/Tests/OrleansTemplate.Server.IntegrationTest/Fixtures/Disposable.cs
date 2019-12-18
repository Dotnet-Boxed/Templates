namespace OrleansTemplate.Server.IntegrationTest
{
    using System;

    /// <summary>
    /// Base class for members implementing <see cref="IDisposable"/>.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable"/> class. Releases unmanaged
        /// resources and performs other clean-up operations before the <see cref="Disposable"/>
        /// is reclaimed by garbage collection. Will run only if the
        /// Dispose method does not get called.
        /// </summary>
        ~Disposable()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Disposable"/> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose all managed and unmanaged resources.
            this.Dispose(true);

            // Take this object off the finalization queue and prevent finalization code for this
            // object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the managed resources implementing <see cref="IDisposable"/>.
        /// </summary>
        protected virtual void DisposeManaged()
        {
        }

        /// <summary>
        /// Disposes the unmanaged resources implementing <see cref="IDisposable"/>.
        /// </summary>
        protected virtual void DisposeUnmanaged()
        {
        }

        /// <summary>
        /// Throws a <see cref="ObjectDisposedException"/> if this instance is disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources, called from the finalizer only.</param>
        /// <remarks>We suppress CA1063 which requires that this method be protected virtual because we want to hide
        /// the internal implementation.</remarks>
#pragma warning disable CA1063 // Implement IDisposable Correctly
        private void Dispose(bool disposing)
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
            // Check to see if Dispose has already been called.
            if (!this.IsDisposed)
            {
                // If disposing managed and unmanaged resources.
                if (disposing)
                {
                    this.DisposeManaged();
                }

                this.DisposeUnmanaged();

                this.IsDisposed = true;
            }
        }
    }
}
