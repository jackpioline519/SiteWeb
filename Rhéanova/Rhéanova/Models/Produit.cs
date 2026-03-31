namespace AquariumShop.Models;

public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string DescriptionCourte { get; set; } = string.Empty;
    public string DescriptionLongue { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Categorie { get; set; } = string.Empty;
    public bool EnStock { get; set; }
    public bool ProduitVedette { get; set; }
    public string Reference { get; set; } = string.Empty;
}
