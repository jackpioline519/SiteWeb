using System.Text.Json;
using AquariumShop.Models;

namespace AquariumShop.Services;

public class ProduitService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ProduitService> _logger;
    private List<Produit>? _cache;

    public ProduitService(IWebHostEnvironment environment, ILogger<ProduitService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task<List<Produit>> GetAllAsync()
    {
        if (_cache is not null)
            return _cache;

        var filePath = Path.Combine(_environment.ContentRootPath, "Data", "produits.json");

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("Le fichier produits.json est introuvable : {FilePath}", filePath);
            return new List<Produit>();
        }

        await using var stream = File.OpenRead(filePath);

        var produits = await JsonSerializer.DeserializeAsync<List<Produit>>(stream,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        _cache = produits ?? new List<Produit>();
        return _cache;
    }

    public async Task<List<Produit>> GetByCategorieAsync(string categorie)
    {
        var produits = await GetAllAsync();

        return produits
            .Where(p => string.Equals(p.Categorie, categorie, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Nom)
            .ToList();
    }

    public async Task<List<Produit>> GetVedettesAsync()
    {
        var produits = await GetAllAsync();

        return produits
            .Where(p => p.ProduitVedette)
            .OrderBy(p => p.Nom)
            .ToList();
    }

    public async Task<Produit?> GetByIdAsync(int id)
    {
        var produits = await GetAllAsync();
        return produits.FirstOrDefault(p => p.Id == id);
    }
}
