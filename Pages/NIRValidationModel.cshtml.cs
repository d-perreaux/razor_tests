using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.IO;

namespace RazorPagesMovie.Pages;

public class NIRValidationModel : PageModel
{
    [BindProperty]
    public NIR NIR { get; set; }

    public string ValidationResult { get; set; }

    public void OnGet()
    {
        NIR = new NIR();
    }

    public IActionResult OnPost()
    {
        ValidationResult = ValidateNIR(NIR.Numero);
        return Page();
    }

    private string ValidateNIR(string numero)
{
    // Vérifier si le numéro NIR contient exactement 13 chiffres
    if (numero.Length != 13 || !numero.All(char.IsDigit))
    {
        return "Format NIR incorrect. Le NIR doit contenir exactement 13 chiffres.";
    }

    // Extraire les parties du NIR
    string sexe = numero.Substring(0, 1);
    Console.WriteLine(sexe);
    string anneeNaissance = numero.Substring(1, 2);
    Console.WriteLine(anneeNaissance);
    string moisNaissance = numero.Substring(3, 2);
    Console.WriteLine(moisNaissance);

    // Vérifier si le sexe est valide (1 pour les hommes, 2 pour les femmes)
    if (sexe != "1" && sexe != "2")
    {
        return "Format NIR incorrect. Le chiffre de sexe doit être 1 ou 2.";
    }

    // Vérifier si l'année de naissance est valide (entre 00 et 99)
    if (!int.TryParse(anneeNaissance, out int annee) || annee < 0 || annee > 99)
    {
        return "Format NIR incorrect. L'année de naissance doit être comprise entre 00 et 99.";
    }

    // Vérifier si le mois de naissance est valide (entre 01 et 12)
    if (!int.TryParse(moisNaissance, out int mois) || mois < 1 || mois > 12)
    {
        return "Format NIR incorrect. Le mois de naissance doit être compris entre 01 et 12.";
    }

    // Si toutes les vérifications passent, le NIR est valide
    return "Format NIR valide.";
}

}

public class NIR
{
    public string Numero { get; set; }
}