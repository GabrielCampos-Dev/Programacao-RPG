string nome;
string classe;
int nivel;
int experiencia;
int novaExperiencia;
int novoNivel;
int opcao;

Random random = new Random(); // Função para gerar números aleatórios

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

int GanharExperiencia()
{
    int experienciaGanha;

    Console.Write("Ganhar experiência: ");
    bool experienciaValida = int.TryParse(Console.ReadLine(), out experienciaGanha);
    while (!experienciaValida || experienciaGanha < 0)
    {
        Console.WriteLine("Experiência inválida. Por favor, digite um número inteiro.");
        Console.Write("Digite a experiência ganha do personagem: ");
        experienciaValida = int.TryParse(Console.ReadLine(), out experienciaGanha);
    }

    return experienciaGanha;
}

int VerificarOpcao(string tipo)
{
    int opcao;
    bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao); // Verifica se a entrada é um número inteiro

    while (!opcaoValida || (opcao != 1 && opcao != 2)) // Verifica se a opção é válida
    {
        if (tipo == "explorar")
        {
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.WriteLine("1. Explorar");
            Console.WriteLine("2. Sair");
        }
        else if (tipo == "batalha")
        {
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Fugir");
        }

        opcaoValida = int.TryParse(Console.ReadLine(), out opcao);
    }

    return opcao;
}

Console.WriteLine("================================");
Console.WriteLine("         Programador RPG         ");
Console.WriteLine("================================");

nome = PerguntarNome();
classe = PerguntarClasse();
experiencia = PerguntarExperiencia();
nivel = CalcularNivel(experiencia);

Console.WriteLine($"Nome: {nome}");
Console.WriteLine($"Classe: {classe}");
Console.WriteLine($"Nível: {nivel}");
Console.WriteLine($"Experiência atual: {experiencia}");
Console.WriteLine(SaudarJogador(nome));

Console.WriteLine("================================");
Console.WriteLine("            Aventura            ");
Console.WriteLine("================================");

do
{
    Console.WriteLine("Escolha uma ação:");
    Console.WriteLine("1. Explorar");
    Console.WriteLine("2. Sair");
    opcao = VerificarOpcao("explorar");

    if (opcao == 1)
    {
        novaExperiencia = 0;

        int chance = random.Next(1, 101); // Gera um número aleatório entre 1 e 100
        if (chance <= 70) // 70% de chance de encontrar inimigo
        {
            Console.WriteLine("Goblin apareceu!");
            int vidaJogador = 100;
            int vidaInimigo = 50;

            do
            {
                Console.WriteLine($"Vida do jogador: {vidaJogador}");
                Console.WriteLine($"Vida do Goblin: {vidaInimigo}");
                Console.WriteLine("Escolha uma ação:");
                Console.WriteLine("1. Atacar");
                Console.WriteLine("2. Fugir");
                int acao = VerificarOpcao("batalha");
                if (acao == 1)
                {
                    int dadoD20 = random.Next(1, 20); 
                    int dadoInimigo = random.Next(1, 20);

                    if (dadoD20 >= 9)
                    {
                        int dano = random.Next(10, 21); // Gera um número aleatório entre 10 e 20
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
                        int danoInimigo = random.Next(5, 16); // Gera um número aleatório entre 5 e 15
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
    else
    {
        Console.WriteLine("Saindo do jogo...");
        break;
    }
} while (opcao != 2);