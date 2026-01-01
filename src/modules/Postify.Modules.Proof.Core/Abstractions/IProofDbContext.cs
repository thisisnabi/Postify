using Microsoft.EntityFrameworkCore;
using Postify.Modules.Proof.Core.Entities;

namespace Postify.Modules.Proof.Core.Abstractions;
public interface IProofDbContext
{
    public DbSet<MessageProof> MessageProofs { get; set; }
}
