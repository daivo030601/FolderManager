using FolderManager.Application.Common.Interfaces;
using FolderManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FolderManager.Data.Context
{
    public class FolderManagerDbContext : IdentityDbContext<User>, IFolderManagerDbContext
    {
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;
        public FolderManagerDbContext(DbContextOptions<FolderManagerDbContext> options) : base(options) 
        {

        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Folder> Folders { get; set; }

        public FolderManagerDbContext(DbContextOptions<FolderManagerDbContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            CancellationToken cancellationToken = new CancellationToken();
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Note>().HasKey(e => e.Id);
            modelBuilder.Entity<Folder>().HasKey(e => e.Id);

            modelBuilder.Entity<Folder>(e =>
            {
                e.Property(f => f.Name).IsRequired();
            });
            modelBuilder.Entity<Note>(e =>
            {
                e.Property(n => n.Title).IsRequired();
                e.Property(n => n.Content).IsRequired();
            });

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Folder)
                .WithMany(f => f.Notes)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
