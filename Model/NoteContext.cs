using Microsoft.EntityFrameworkCore;

namespace note_app_api;

public class NoteContext : DbContext
{
    public NoteContext(DbContextOptions<NoteContext> options) :
        base(options)
    {

    }

    public DbSet<Note> Note => Set<Note>();
}