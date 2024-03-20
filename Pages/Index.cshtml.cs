using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.IO;

namespace RazorPagesMovie.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Personne? MyPersonne {get; set;}
    public List<Personne> Personnes {get; set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public async Task OnGet()
    {
        Personnes = await DesAsync();
        foreach(var personne in Personnes){
            Console.WriteLine($"Nom: {personne.Nom}, Ville: {personne.Ville}");
        }

    }

    public async Task<List<Personne>> DesAsync() {
        string filePath = "wwwroot\\data\\personnes.json";
        using FileStream openStream = System.IO.File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<List<Personne>>(openStream);
    }
}

public class Personne {
    public string? Nom {get; set; }
    public string? Ville {get; set;}
    public int Age {get; set;}
}
