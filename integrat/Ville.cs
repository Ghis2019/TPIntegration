namespace ConsoleApplication2
{
    public class Ville
{
    public string code { get; set; } // Champs non-nullable par défaut

    public string population { get; set; } // Champs non-nullable par défaut

    public string nom { get; set; } // Champs non-nullable par défaut

    // Constructeur par défaut pour initialiser les champs
    public Ville()
    {
        code = "";
        population = "";
        nom = "";
    }
}

}