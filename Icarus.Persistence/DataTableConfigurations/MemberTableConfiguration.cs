using Icarus.Domain.Entities;
using Icarus.Domain.ValueObjects;
using Icarus.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Icarus.Persistence.Configurations;

internal sealed class MemberTableConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(TableNames.Members);

        builder.HasKey(x => x.Id);
        //builder.Property<string>("_passwordHash")
        //    .HasField("_passwordHash")
        //    .HasColumnName("password_hash")
        //    .IsRequired();
    }
}
