using Microsoft.EntityFrameworkCore;

namespace RecipeOrganizer.Data
{
	public class UnitOfWork : IDisposable
	{
		private readonly DbContext _context;
		private bool _disposed;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public void BeginTransaction()
		{
			_context.Database.BeginTransaction();
		}

		public void Commit()
		{
			_context.Database.CommitTransaction();
		}

		public void Rollback()
		{
			_context.Database.RollbackTransaction();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
				_disposed = true;
			}
		}
	}
}
