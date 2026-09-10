string nome;
string classe;
int nivel;
int experiencia;
int novaExperiencia;
int novoNivel;
int opcao;
int vidaBase = 100;
int danoBase = 10;

Random random = new Random(); // Função para gerar números aleatórios

string PerguntarNome()
{
    Console.Write("Digite o nome do personagem: ");
    return Console.ReadLine();
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

int CalcularNivel(int experiencia)
{
    return experiencia / 500 + 1;
}

int CalcularVida(int nivel, int vidaBase)
{
    return vidaBase + nivel * 10;
}

int CalcularDanoBatalha(int nivel, int danoBase)
{
    danoBase += nivel;
    int danoMaximo = 20 + nivel * 2;
    return random.Next(danoBase, danoMaximo + 1); // Gera um número aleatório entre o dano base e o dano máximo
}

int CalcularVidaInimigo(int nivel)
{
    return 50 + nivel * 5;
}

int CalcularDanoInimigo(int nivel)
{
    int danoBase = 5 + nivel;
    int danoMaximo = 15 + nivel * 2;
    return random.Next(danoBase, danoMaximo + 1); // Gera um número aleatório entre o dano base e o dano máximo
}

int VerificarOpcao(string tipo)
{
    int opcao;
    bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao); // Verifica se a entrada é um número inteiro

    while (!opcaoValida || (opcao != 1 && opcao != 2 && opcao != 3)) // Verifica se a opção é válida
    {
        if (tipo == "explorar")
        {
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.WriteLine("1. Explorar");
            Console.WriteLine("2. Informações do Personagem");
            Console.WriteLine("3. Sair");
        }
        else if (tipo == "batalha")
        {
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Defender");
            Console.WriteLine("3. Fugir");
        }
        else if (tipo == "classe")
        {
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.WriteLine("1. Guerreiro");
            Console.WriteLine("2. Mago");
            Console.WriteLine("3. Arqueiro");
        }
        opcaoValida = int.TryParse(Console.ReadLine(), out opcao);
    }

    return opcao;
}

string PerguntarClasse()
{
    Console.WriteLine("Escolha a classe do personagem: ");
    Console.WriteLine("1. Guerreiro");
    Console.WriteLine("2. Mago");
    Console.WriteLine("3. Arqueiro");
    return VerificarOpcao("classe") switch
    {
        1 => "Guerreiro",
        2 => "Mago",
        3 => "Arqueiro",
        _ => throw new ArgumentException("Classe inválida")
    };
}

Console.WriteLine("================================");
Console.WriteLine("         Programador RPG         ");
Console.WriteLine("================================");

nome = PerguntarNome();
classe = PerguntarClasse();
experiencia = PerguntarExperiencia();
nivel = CalcularNivel(experiencia);

if (classe == "Guerreiro")
{
    vidaBase = 120;
    danoBase = 15;
}
else if (classe == "Mago")
{
    vidaBase = 80;
    danoBase = 20;
}
else if (classe == "Arqueiro")
{
    vidaBase = 100;
    danoBase = 12;
}

string FichaPersonagem(string nome, string classe, int nivel, int vidaBase, int danoBase, int experiencia)
{
    return $"Nome: {nome}\nClasse: {classe}\nNível: {nivel}\nVida: {CalcularVida(nivel, vidaBase)}\nDano: {danoBase + nivel}\nExperiência atual: {experiencia}";
}

Console.WriteLine(FichaPersonagem(nome, classe, nivel, vidaBase, danoBase, experiencia));

Console.WriteLine("================================");
Console.WriteLine("            Aventura            ");
Console.WriteLine("================================");

do
{
    Console.WriteLine("Escolha uma ação:");
    Console.WriteLine("1. Explorar");
    Console.WriteLine("2. Informações do Personagem");
    Console.WriteLine("3. Sair");
    opcao = VerificarOpcao("explorar");

    if (opcao == 1)
    {
        novaExperiencia = 0;

        int chance = random.Next(1, 101); // Gera um número aleatório entre 1 e 100
        if (chance <= 70) // 70% de chance de encontrar inimigo
        {
            Console.WriteLine($"Um Goblin lv {nivel - 1} apareceu!");
            int vidaJogador = CalcularVida(nivel, vidaBase);
            int vidaInimigo = CalcularVidaInimigo(nivel);

            do
            {
                Console.WriteLine($"Vida do jogador: {vidaJogador}");
                Console.WriteLine($"Vida do Goblin: {vidaInimigo}");
                Console.WriteLine("Escolha uma ação:");
                Console.WriteLine("1. Atacar");
                Console.WriteLine("2. Defender");
                Console.WriteLine("3. Fugir");
                int acao = VerificarOpcao("batalha");
                if (acao == 1)
                {
                    int dadoD20 = random.Next(1, 21); 
                    int dadoInimigo = random.Next(1, 21);

                    if (dadoD20 >= 9)
                    {
                        int dano = CalcularDanoBatalha(nivel, danoBase); // Gera um número aleatório entre 10 e 20
                        Console.WriteLine($"Você atacou o Goblin e causou {dano} de dano!");
                        vidaInimigo -= dano; // O Goblin perde vida com base no dano causado

                        if (vidaInimigo <= 0)
                        {
                            Console.WriteLine("Você derrotou o Goblin!");
                            novaExperiencia = random.Next(200, 251); // Ganha experiência aleatória entre 200 e 250
                            Console.WriteLine($"Você ganhou {novaExperiencia} de experiência!");
                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Você errou o ataque!");
                    }

                    if (dadoInimigo >= 12)
                    {
                        int danoInimigo = CalcularDanoInimigo(nivel); // Gera um número aleatório entre 5 e 15
                        Console.WriteLine($"O Goblin atacou você e causou {danoInimigo} de dano!");
                        vidaJogador -= danoInimigo;
                    }
                    else if (dadoInimigo < 12)
                    {
                        Console.WriteLine("O Goblin errou o ataque!");
                    }
                                        
                    if (vidaJogador <= 0)
                    {
                        Console.WriteLine("Você foi derrotado pelo Goblin!");
                        novaExperiencia = 0; // Não ganha experiência ao ser derrotado
                        break;
                    }

                }
                else if (acao == 2)
                {
                    int dadoD20 = random.Next(1, 21); 
                    int dadoInimigo = random.Next(1, 21);
                    if (dadoD20 >= 13)
                    {
                        Console.WriteLine($"Você defendeu o ataque do Goblin com sucesso!");

                    }
                    else if (dadoInimigo >= 15)
                    {
                        int danoInimigo = CalcularDanoInimigo(nivel); // Gera um número aleatório entre 5 e 15
                        Console.WriteLine($"Você não conseguiu defender o ataque do Goblin e recebeu {danoInimigo} de dano!");
                        vidaJogador -= danoInimigo;
                    }
                    else if (dadoInimigo <= 14)
                    {
                        Console.WriteLine("O Goblin errou o ataque!");
                    }
                                        
                    if (vidaJogador <= 0)
                    {
                        Console.WriteLine("Você foi derrotado pelo Goblin!");
                        novaExperiencia = 0; // Não ganha experiência ao ser derrotado
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Você fugiu da batalha!");
                    novaExperiencia = 0; // Não ganha experiência ao fugir
                    break;
                }
            } while (vidaJogador > 0 && vidaInimigo > 0);
        }

        else
        {
            Console.WriteLine("Você não encontrou nenhum Goblin.");
        }

        experiencia += novaExperiencia; // Atualiza a experiência do personagem com a experiência ganha na exploração
        novoNivel = CalcularNivel(experiencia); // Calcula o novo nível com base na experiência atualizada

        if (novoNivel > nivel)
        {
            Console.WriteLine($"Parabéns! Você subiu de nível! Novo nível: {novoNivel}");
            Console.WriteLine($"Experiência necessária para o próximo nível: {500 - (experiencia % 500)}");
            nivel = novoNivel;
        }
        else
        {
            Console.WriteLine($"Você não subiu de nível. Nível atual: {nivel}");
            Console.WriteLine($"Experiência necessária para o próximo nível: {500 - (experiencia % 500)}");
        }
    }
    else if (opcao == 2)
    {
        Console.WriteLine(FichaPersonagem(nome, classe, nivel, vidaBase, danoBase, experiencia));
    }
    else
    {
        Console.WriteLine("Saindo do jogo...");
        break;
    }
} while (opcao != 3);