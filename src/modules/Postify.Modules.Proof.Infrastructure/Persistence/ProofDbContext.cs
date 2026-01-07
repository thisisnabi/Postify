using Microsoft.EntityFrameworkCore;
using Postify.Modules.Proof.Core.Abstractions;
using Postify.Modules.Proof.Core.Entities;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Modules.Proof.Infrastructure.Persistence;
public class ProofDbContext : ModuleDbContext, IProofDbContext
{
    public DbSet<MessageProof> MessageProofs { get; set; }

    protected override string Schema => "proof";

    public ProofDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }
}
