using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const int UsernameMaxLength = 15;
    private const int EmailMaxLength = 32;

    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureId(builder);
        ConfigureUsername(builder);
        ConfigureEmail(builder);
        ConfigurePassword(builder);
        ConfigureDateBirth(builder);
        ConfigureGender(builder);
        ConfigureRole(builder);
        ConfigureActive(builder);
    }

    private static void ConfigureId(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(k => k.Id)
            .HasColumnName("ID");
    }

    private static void ConfigureUsername(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(UsernameMaxLength);
    }

    private static void ConfigureEmail(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(EmailMaxLength);

        builder
            .HasIndex(e => e.Email)
            .IsUnique();
    }

    private static void ConfigurePassword(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(p => p.Password)
            .IsRequired();
    }

    private static void ConfigureDateBirth(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(d => d.DateBirth)
            .IsRequired()
            .HasColumnType("date");
    }

    private static void ConfigureGender(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(g => g.Gender)
            .IsRequired()
            .HasConversion(
                gender => gender.ToString(),
                gender => (Gender)Enum.Parse(typeof(Gender), gender)
            )
            ;
    }

    private static void ConfigureRole(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(r => r.Role)
            .IsRequired()
            .HasConversion(
                role => role.ToString(),
                role => (Role)Enum.Parse(typeof(Role), role)
            );
    }

    private static void ConfigureActive(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(a => a.Active)
            .IsRequired()
            .HasConversion(
                active => active.ToString(),
                active => (Active)Enum.Parse(typeof(Active), active)
            );
    }

}