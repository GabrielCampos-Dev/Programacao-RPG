string nome;
string classe;
int nivel;
int experiencia;

string PerguntarNome()
{
    Console.Write("Digite o nome do personagem: ");
    return Console.ReadLine();
}

string PerguntarClasse()
{
    Console.Write("Digite a classe do personagem: ");
    return Console.ReadLine();
}

int PerguntarNivel()
{
    Console.Write("Digite o nível do personagem: ");
    bool nivelValido = int.TryParse(Console.ReadLine(), out nivel);

    while (!nivelValido || nivel < 1 || nivel > 100)
    {
        Console.WriteLine("Nível inválido. O nível deve estar entre 1 e 100.");
        Console.Write("Digite o nível do personagem: ");
        nivelValido = int.TryParse(Console.ReadLine(), out nivel);
    }
    return nivel;
}

int PerguntarExperiencia()
{
    Console.Write("Digite a experiência atual do personagem: ");
    bool experienciaValida = int.TryParse(Console.ReadLine(), out experiencia);

    while (!experienciaValida || experiencia < 0)
    {
        Console.WriteLine("Experiência inválida. Por favor, digite um número inteiro.");
        Console.Write("Digite a experiência atual do personagem: ");
        experienciaValida = int.TryParse(Console.ReadLine(), out experiencia);
    }
    return experiencia;
}

string SaudarJogador(string nome)
{
    return $"Olá, {nome}! Bem-vindo ao Programador RPG!";
}

Console.WriteLine("================================");
Console.WriteLine("         Programador RPG         ");
Console.WriteLine("================================");

nome = PerguntarNome();
classe = PerguntarClasse();
nivel = PerguntarNivel();
experiencia = PerguntarExperiencia();

Console.WriteLine($"Nome: {nome}");
Console.WriteLine($"Classe: {classe}");
Console.WriteLine($"Nível: {nivel}");
Console.WriteLine($"Experiência: {experiencia}");
Console.WriteLine(SaudarJogador(nome));
