using Microsoft.EntityFrameworkCore;
using note_app_api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Logging.AddConsole();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NoteContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseHttpsRedirection();

app.MapSwagger();
app.UseSwaggerUI();

const string notesPath = "/api/notes";

app.MapGet(notesPath, async (NoteContext ctx) =>
{
    return await Note.GetNotesAsync(ctx);
}).WithName("GetNotes");

app.MapPost(notesPath, async (NoteContext ctx, Note note) =>
{
    var result = await Note.AddNoteAsync(ctx, note);
    return Results.Created($"{notesPath}/{result.Id}", result);
});

app.MapPut($"{notesPath}/{{id:int}}", async (NoteContext ctx, int id, Note note) =>
{
    var result = await Note.UpdateNoteAsync(ctx, id, note);
    if (result is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
});

app.MapDelete($"{notesPath}/{{id:int}}", async (NoteContext ctx, int id) =>
{
    await Note.DeleteNoteAsync(ctx, id);
    return Results.NoContent();
});

app.Run();
