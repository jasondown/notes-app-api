using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace note_app_api;

public record Note
{
    [Column("id")]
    public int Id { get; init; } = default!;

    [Column("title")]
    public string? Title { get; set; } = default;
    [Column("content")]
    public string? Content { get; set; } = default!;

    public static async Task<IEnumerable<Note>> GetNotesAsync(NoteContext ctx) 
    { 
        return await ctx.Note.ToListAsync(); 
    }

    public static async Task<Note> AddNoteAsync(NoteContext ctx, Note note)
    {
        ctx.Note.Add(note);
        await ctx.SaveChangesAsync();
        return note;
    }

    public static async Task<Note?> UpdateNoteAsync(NoteContext ctx, int id, Note newNote)
    {
        var note = await ctx.Note.FindAsync(id);
        if (note is null) {
            return null;
        }

        note.Title = newNote.Title;
        note.Content = newNote.Content;
        
        await ctx.SaveChangesAsync();
        return note;
    }

    public static async Task DeleteNoteAsync(NoteContext ctx, int id)
    {
        var note = await ctx.Note.FindAsync(id);
        if (note is not null) {
            ctx.Note.Remove(note);
            await ctx.SaveChangesAsync();
        }
    }
}