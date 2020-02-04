//-----------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.ModelDB
{
  using CommonLayer.Model;
  using FundooCommonLayer.Model;
  using Microsoft.EntityFrameworkCore;
 
  /// <summary>
  /// This is the class for usercontext which implement DbContext.
  /// </summary>
  /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
  public class UserContext : DbContext
  {
    public UserContext(DbContextOptions options)
           : base(options)
    {
    }
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>
    /// The users.
    /// </value>
    public DbSet<UserDetails> Users { get; set; }
    public DbSet<NotesDB> Notes { get; set; }
    public DbSet<LabelModel> label { get; set; }
    public DbSet<Noteslabel> Noteslabels { get; set; }
    public DbSet<CollaborateDb> collaborates { get; set; }
    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserDetails>()
        .HasIndex(user => user.Email)
        .IsUnique();
    }
  }
}
