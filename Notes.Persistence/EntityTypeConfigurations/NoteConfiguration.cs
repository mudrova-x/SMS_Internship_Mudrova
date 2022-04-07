using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain;


namespace Notes.Persistence.EntityTypeConfigurations
{
    // извлечение всех конфигураций для типа сущности в класс NoteConfiguration
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        // реализуем конфигурацию для типа сущности с помощью EntityTypeBuilder
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            // параметры конфигурации (+ ограничения полей)
            // id - первичный ключ, у Title ограничение по длине
            builder.HasKey(note => note.Id);
            builder.HasIndex(note => note.Id).IsUnique();
            builder.Property(note => note.Title).HasMaxLength(250);


        }
    }
}
