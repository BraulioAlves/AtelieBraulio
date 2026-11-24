using AtelieBraulio.Data;
using Microsoft.EntityFrameworkCore;

public class Quadro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Descricao { get; set; } = "";
    public string Imagem { get; set; } = "";
    public string Preco { get; set; } = "";
}

public class GaleriaService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly string _senhaHash;

    public GaleriaService(IDbContextFactory<AppDbContext> factory, IConfiguration config)
    {
        _factory = factory;
        _senhaHash = config["Admin:SenhaHash"]!;

        // Se o banco estiver vazio, coloca seus 3 quadros lá dentro 1 vez só
        using var db = _factory.CreateDbContext();
        if (!db.Quadros.Any())
        {
            db.Quadros.AddRange(
                new Quadro { Titulo = "Natureza Morta - Vinho", Descricao = "Uvas e vinho em óleo sobre tela", Imagem = "images/vinho-vermelho.jpeg", Preco = "R$ 1.200,00" },
                new Quadro { Titulo = "Paisagem - Ipê amarelo", Descricao = "Ipê amarelo de Minas Gerais", Imagem = "images/ipe-paisagem.jpeg", Preco = "R$ 1.500,00" },
                new Quadro { Titulo = "Natureza Morta - Flores", Descricao = "Flores do campo", Imagem = "images/vinho-azul.jpeg", Preco = "R$ 900,00" }
            );
            db.SaveChanges();
        }
    }

    public List<Quadro> Quadros
    {
        get
        {
            using var db = _factory.CreateDbContext();
            return db.Quadros.ToList();
        }
    }

    public bool EstaLogado { get; set; } = false;

    public bool VerificarSenha(string senhaDigitada)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(senhaDigitada);
        var hashDigitado = Convert.ToHexString(sha.ComputeHash(bytes));
        return hashDigitado == _senhaHash;
    }

    public void AdicionarQuadro(Quadro novo)
    {
        using var db = _factory.CreateDbContext();
        db.Quadros.Add(novo);
        db.SaveChanges();
    }


    public void RemoverQuadro(Quadro q)
    {
        using var db = _factory.CreateDbContext();
        db.Quadros.Remove(q);
        db.SaveChanges();
        Quadros.Remove(q);
    }

    public void AtualizarPreco(Quadro q)
    {
        using var db = _factory.CreateDbContext();
        db.Quadros.Update(q);
        db.SaveChanges();
    }

}