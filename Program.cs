using System;

namespace BINGO
{
    class Partida
    {
        public Jogador[] jogador;
        public Jogador[] ranking;
        public bool ocorrendo;
        int idxJogador = 0; // Índice do vetor de jogadores
        int idxGanhadores = 0; // Índice do ranking começando das primeiras posições
        int idxEliminados; // Índice do ranking começando das ultimas posições
        

        public void QuantJogadores(int quant) // Recebe a quantidade de jogadores pra inicializar os vetores
        {
            jogador = new Jogador[quant];
            ranking = new Jogador[quant];
            idxEliminados = quant - 1;
        }

        public void RetornaRanking() // Imprime cada posição do ranking
        {
            for(int i = 0; i < ranking.Length; i++)
            {
                if(this.ranking[i] == null)
                    Console.WriteLine($"{i + 1}° colocado: indefinido;");
                else
                    Console.WriteLine($"{i+1}° colocado: {this.ranking[i].nome};");
            }
        }

        public void RankingGanhadores(int idxJogador) // Preenche o ranking começando das primeiras posições
        {
            this.ranking[idxGanhadores] = jogador[idxJogador];
            idxGanhadores++;
        }
        public void RankingEliminados(int idxJogador) // Preenche o ranking começando das primeiras posições
        {
            this.ranking[idxEliminados] = jogador[idxJogador];
            idxEliminados--;
        }

        public void RankingRestante(int idxJogador) // Identifica a posição do ranking que está vazia e coloca o ultimo jogador nela
        {
            for (int i = 0; i < ranking.Length; i++)
            {
                if (ranking[i] == null)
                {
                    ranking[i] = jogador[idxJogador];
                    jogador[idxJogador].emjogo = false; 
                }
            }
        }

        public void CriaJogador(string nome, int idade, char sexo, int quantCartelas) // Cria o objeto jogador que por sua vez cria n cartelas
        {
            jogador[idxJogador] = new Jogador();
            jogador[idxJogador].informaJogador(nome, idade, sexo, quantCartelas);
            idxJogador++;
        }

        public int JogadoresEmJogo() // Retorna a quantidade de jogadores que ainda estão em jogo
        {
            int contador = 0;

            for (int i = 0; i < jogador.Length; i++)
            {
                if (jogador[i].emjogo == true)
                {
                    contador++;
                }
            }
            return contador;
        }

        public int SortearNumero(int[] JaSorteados){

            int[] sorteados = JaSorteados; // Vetor que vai receber os números já sorteados;
            int res = 0, count = 0;
            Random rand = new Random();

            int numeroAleatorio = rand.Next(1, sorteados.Length + 1);
            while (Array.IndexOf(sorteados, numeroAleatorio) != -1) { // Verificando se existe ocorrência do numero gerado no vetor de valores sorteados;
                numeroAleatorio = rand.Next(1, sorteados.Length + 1);//Novo número será sorteado enquanto o índice da ocorrência for -1;
            }
            res =  numeroAleatorio;
            return res;
        }
        public bool VerificaLinha(int idxJogador, int idxCartela, int idxsequencia, int[] sorteados) // Identifica qual linha de qual tabela será verificada
        {
            int contador = 0;
            int[,] cartela = this.jogador[idxJogador].getCopias(idxCartela); // Dados da copia para verificação

            for (int i = 0; i < cartela.GetLength(0); i++)
            {
                for (int j = 0; j < cartela.GetLength(1); j++)
                {
                    if(i == idxsequencia)
                    {
                        if (Array.IndexOf(sorteados, cartela[i,j]) != -1){ // Verifica se os números na cópia estão entre os números sorteados
                            contador++;
                        }
                    }
                }
            }

            if(idxsequencia == 2) // caso expecífico das linhas que passam pela posição [2,2]
            {
                if (contador == 4)
                    return true;
                else
                    return false;
            }
            else
            {
                if (contador == 5) // Se todos os números estiverem entre os sorteados retorna 5
                    return true;
                else
                    return false;
            }            
        }
        public bool VerificaColuna(int idxJogador, int idxCartela, int idxsequencia, int[] sorteados)
        {
            int contador = 0;
            int[,] cartela = this.jogador[idxJogador].getCopias(idxCartela);

            for (int i = 0; i < cartela.GetLength(0); i++)
            {
                for (int j = 0; j < cartela.GetLength(1); j++)
                {
                    if (j == idxsequencia)
                    {
                        if (Array.IndexOf(sorteados, cartela[i, j]) != -1)
                        {
                            contador++;
                        }
                    }
                }
            }

            if (idxsequencia == 2)
            {
                if (contador == 4)
                    return true;
                else
                    return false;
            }
            else
            {
                if (contador == 5)
                    return true;
                else
                    return false;
            }
        }
    }
     class Jogador
    {
        public string nome;
        public int idade;
        public char sexo;
        public Cartela[] cartelas;
        public Cartela[] copias;
        public bool emjogo;
        int contador = 1;

        Random rand = new Random();

        public int CartelasEmJogo()
        {
            int contador = 0;

            for(int i = 0; i < cartelas.Length; i++)
            {
                if (cartelas[i].emjogo == true)
                {
                    contador++;
                }
            }
            return contador;
        }


        public int[,] getCopias(int i)
        {
            return this.copias[i].numeros;
        }
        public void informaJogador(string nome, int idade, char sexo, int quantCartelas)
        {
            this.nome = nome;
            this.idade = idade;
            this.sexo = sexo;
            emjogo = true;

            cartelas = new Cartela[quantCartelas];
            copias = new Cartela[cartelas.Length];

            for (int i = 0; i < quantCartelas; i++)
            {
                //Gerar e preencher os dados de cada cartela
                cartelas[i] = new Cartela();
                cartelas[i].novaCartela(quantCartelas, rand);
                cartelas[i].id = contador;
                cartelas[i].emjogo = true;
                contador++;
            
                //Salvando as copias das cartelas criadas anteriormente
                copias[i] = new Cartela();

                for(int j = 0; j < copias[i].numeros.GetLength(0); j++)
                {
                    for(int k = 0; k < copias[i].numeros.GetLength(1); k++)
                    {
                        copias[i].numeros[j, k] = cartelas[i].numeros[j, k];
                    }
                }
            }
        }
    
        public string MarcarCartela(int numero, int indice)
        {
            string retorno = "";
            int contador = 0;
            for (int i = 0; i < cartelas[indice].numeros.GetLength(0); i++) 
            {
                for (int j = 0; j < cartelas[indice].numeros.GetLength(1); j++)
                {
                    if (cartelas[indice].numeros[i, j] == numero)  // Verifica se o número escolhido existe na cartela
                    {
                        retorno = "Número marcado na cartela!";
                        cartelas[indice].numeros[i, j] = 0;

                        copias[indice].numeros[i, j] = numero;
                    }
                    else
                        contador++;
                }
            }

            if(contador == 25) // Verifica se o número é diferente de todos os números da cartela
                retorno = "Esse número não existe nessa cartela!";

            return retorno;
        }

    }
    class Cartela
    {
        public int id;
        public int[,] numeros = new int[5,5];
        public bool emjogo;

        public void novaCartela(int quantidade, Random rand)
        {
            for (int i = 0; i < quantidade; i++){ // Usa a quantidade de cartelas informada pelo jogador
                numeros = PreencheCartela(numeros, rand); // Chama o método de preencher
                emjogo = true;
            }
        }
       private static int[,] PreencheCartela(int[,] numeros, Random rand)
        {
            int[,] NovaCartela = new int[numeros.GetLength(0), numeros.GetLength(1)]; // Matriz que tem os dimensões da cartela passada como parâmetro;
            int[] gerados = new int[numeros.GetLength(0) * numeros.GetLength(1)]; // Vetor que vai receber os valores que foram inseridos na cartela;
            int count = 0;

            for (int i = 0; i < numeros.GetLength(0); i++)
            {
                for (int j = 0; j < numeros.GetLength(1); j++) // Percorrendo pela cartela;
                {
                    if(j == 0){ // Inserindo a coluna B
                        int numeroAleatorio = rand.Next(1, 16);
                        while (Array.IndexOf(gerados, numeroAleatorio) != -1) // Verificando se existe ocorrência do numero gerado no vetor de valores inseridos;
                        {
                            numeroAleatorio = rand.Next(1, 16);
                        }

                        NovaCartela[i,j] = numeroAleatorio;
                        gerados[count] = NovaCartela[i, j];
                        count++;
                    }

                    if (j == 1){ // Inserindo a coluna I
                        int numeroAleatorio = rand.Next(16, 31);
                        while (Array.IndexOf(gerados, numeroAleatorio) != -1)
                        {
                            numeroAleatorio = rand.Next(16, 31);
                        }

                        NovaCartela[i, j] = numeroAleatorio;
                        gerados[count] = NovaCartela[i, j];
                        count++;
                    }
                    if (j == 2 && i !=2) // Inserindo a coluna N (Nessa a posição central [2,2] nao vai receber valor;
                    {
                        int numeroAleatorio = rand.Next(31, 46);
                        while (Array.IndexOf(gerados, numeroAleatorio) != -1)
                        {
                            numeroAleatorio = rand.Next(31, 46);
                        }

                        NovaCartela[i, j] = numeroAleatorio;
                        gerados[count] = NovaCartela[i, j];
                        count++;
                    }

                    if (j == 3) // Inserindo a coluna G
                    {
                        int numeroAleatorio = rand.Next(46, 61);
                        while (Array.IndexOf(gerados, numeroAleatorio) != -1)
                        {
                            numeroAleatorio = rand.Next(46, 61);
                        }

                        NovaCartela[i, j] = numeroAleatorio;
                        gerados[count] = NovaCartela[i, j];
                        count++;
                    }

                    if (j == 4) { // Inserindo a coluna O
                        int numeroAleatorio = rand.Next(61, 76);
                        while (Array.IndexOf(gerados, numeroAleatorio) != -1)
                        {
                            numeroAleatorio = rand.Next(61, 76);
                        }

                        NovaCartela[i, j] = numeroAleatorio;
                        gerados[count] = NovaCartela[i, j];
                        count++;
                    }
                }
            }
            NovaCartela[2, 2] = -1;
            return NovaCartela; 
        }

    }
    internal class Program
    {
        public static void Impressao(int[,] numeros)
        {
            Console.WriteLine("---------------------------------------------");
            for (int i = 0; i < numeros.GetLength(0); i++)
            {
                for (int k = 0; k < numeros.GetLength(1); k++)
                {
                    if (numeros[i, k] < 10 && numeros[i, k] != -1)
                        Console.Write($"{numeros[i, k]} |");
                    else
                        Console.Write($"{numeros[i, k]}|");
                }
                Console.WriteLine();
            }
        }

        public static void ImpressaoLegendada(int[,] numeros)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0  1  2  3  4");
            Console.ResetColor();
            for (int i = 0; i < numeros.GetLength(0); i++)
            {
                for (int k = 0; k < numeros.GetLength(1); k++)
                {
                    if (numeros[i, k] < 10 && numeros[i, k] != -1)
                        Console.Write($"{numeros[i, k]} |");
                    else
                        Console.Write($"{numeros[i, k]}|");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" {i}");
                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            Partida partida = new Partida();
            partida.ocorrendo = true;
            int quantidadeCartelas = 0;

            Console.Write("Informe quantas pessoas vão jogar: ");
            int quantJogador = int.Parse(Console.ReadLine());

            while (quantJogador < 2 || quantJogador > 5)
            {
                Console.Write($"O número de jogadores precisa estar entre 2 e 4. Informe quantas pessoas vão jogar: ");
                quantJogador = int.Parse(Console.ReadLine());
            }
            partida.QuantJogadores(quantJogador);
            
            for(int i = 0; i < quantJogador; i++)
            {
                int quantCartela;
                Console.Write($"Informe o nome do(a) Jogador(a) {i+1}: ");
                string nome = Console.ReadLine();

                Console.Write($"Informe a idade do(a) Jogador(a) {i + 1}: ");
                int idade = int.Parse(Console.ReadLine());

                Console.Write($"Informe o sexo do(a) Jogador(a) {i + 1} (M ou F): ");
                char sexo = char.Parse(Console.ReadLine());

                do{
                    Console.Write($"Informe a quantidade de cartelas do(a) Jogador(a) {i + 1}: ");
                    quantCartela = int.Parse(Console.ReadLine());
                    if (quantCartela > 1 && quantCartela <= 4)
                        quantidadeCartelas+= quantCartela;

                } while (quantCartela < 1 || quantCartela > 4);

                partida.CriaJogador(nome, idade, sexo, quantCartela);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Jogador(a) {i + 1} cadastrado(a)!");
                Console.ResetColor();

                Console.WriteLine("----------------------------------------------");
            }

            int[] sorteados = new int[75]; // Armazena os números sorteados e é usado para garantir que nao repita
            int idxSorteados = 0; // Variável de controle para o vetor de sorteados


            for (int i = 0; i <= 75; i++) { // Iniciar cada rodada
                if (partida.ocorrendo == true) // Verifica se tem jogadores o suficiente pra partida continuar
                {
                    int numero = partida.SortearNumero(sorteados);// Sorteando o número da rodada
                    sorteados[idxSorteados] = numero; // Salvando o número sorteado;
                    idxSorteados++;
                    int jogadoresEmJogo = partida.JogadoresEmJogo(); // Recebe a quantidade de jogadores em jogo
                    Console.Clear();
                    if(jogadoresEmJogo != 1)
                    {
                        Console.Write($"ÍNÍCIO DE RODADA!\nO número sorteado da rodada é {numero}.\n-Enter pra continuar...");
                        Console.ReadLine();
                    }

                    for (int j = 0; j < partida.jogador.Length; j++){ //Percorrendo por cada jogador
                        if (partida.jogador[j].emjogo == true){ // Verificando se o jogador não foi removido
                            if (jogadoresEmJogo == 1) //Verifica se só tem um jogador em jogo
                            {
                                partida.RankingRestante(j); // Se só tiver um jogador em jogo ele é colocado no ranking
                                partida.ocorrendo = false; // Garantia de que não se repetirá
                                break;
                            }
                            for (int k = 0; k < partida.jogador[j].cartelas.Length; k++){//Percorrendo por cada cartela
                                if (partida.jogador[j].cartelas[k].emjogo == true){ // Verificando se a cartela não foi removida
                                    Console.Clear();
                                    Console.Write("Número da rodada: ");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine(numero);
                                    Console.ResetColor();
                                    Console.Write($"Cartela N°{partida.jogador[j].cartelas[k].id} do(a) jogador(a) ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(partida.jogador[j].nome);
                                    Console.ResetColor();
                                    Impressao(partida.jogador[j].cartelas[k].numeros);
                                    int opcao;
                                    Console.Write("O número da rodada existe na sua cartela?\n 1 - Marcar número;\n 2 - Prosseguir;\n - Escolha uma opção: ");
                                    opcao = int.Parse(Console.ReadLine());
                                    if (opcao == 1)
                                    {
                                        Console.Clear();
                                        Impressao(partida.jogador[j].cartelas[k].numeros);
                                        Console.WriteLine("------------------------------------------");
                                        Console.Write("Qual número na cartela você deseja marcar? ");
                                        int num = int.Parse(Console.ReadLine());
                                        int opt;
                                        Console.WriteLine(partida.jogador[j].MarcarCartela(num, k)); // Marcação
                                        Console.WriteLine("Enter pra continuar...");
                                        Console.ReadLine();

                                        if (idxSorteados >= 4) // Depois da quarta rodada já pode ter acontecido BINGO!
                                        {
                                            Console.Write($"1 - Gritar BINGO!\n2 - Prosseguir;\n - Escolha uma opção: ");
                                            opt = int.Parse(Console.ReadLine());

                                            if (opt == 1)
                                            {
                                                int RowCol; // Variável que vai receber se é linha ou coluna
                                                int idxSequencia = 0; // Variavel que vai receber qual linha ou qual coluna
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine($"BINGO!{partida.jogador[j].nome} completou uma fileira!");
                                                Console.ResetColor();
                                                Console.Write("Digite 1 se completou uma linha;\nDigite 2 se completou uma coluna;\n-Escolha uma opção: ");
                                                RowCol = int.Parse(Console.ReadLine());
                                                bool verificacao = true;

                                                if (RowCol == 1)
                                                {
                                                    Console.Clear();
                                                    ImpressaoLegendada(partida.jogador[j].cartelas[k].numeros);
                                                    Console.Write("Informe qual linha você completou segundo a legenda: ");
                                                    idxSequencia = int.Parse(Console.ReadLine());
                                                    verificacao = partida.VerificaLinha(j, k, idxSequencia, sorteados); // Executa a verificação segundo as informações fornecidas
                                                }
                                                else if (RowCol == 2)
                                                {
                                                    Console.Clear();
                                                    ImpressaoLegendada(partida.jogador[j].cartelas[k].numeros); // Impressão da cartela para orientação
                                                    Console.Write("Informe qual coluna você completou segundo a legenda: ");
                                                    idxSequencia = int.Parse(Console.ReadLine());
                                                    verificacao = partida.VerificaColuna(j, k, idxSequencia, sorteados); // verificãção se o bingo realmente aconteceu
                                                }

                                                if (verificacao)
                                                {
                                                    int completou = 0;
                                                    if (completou == 0) // Se foi o primeiro bingo informa que é o vencedor
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"{partida.jogador[j].nome} venceu o jogo!");
                                                        Console.ResetColor();
                                                        partida.RankingGanhadores(j);
                                                        completou++;
                                                        partida.jogador[j].emjogo = false; // Ao ganhar, o jogador sai do ciclo
                                                        //Impressao(partida.jogador[j].getCopias(k));
                                                        Console.WriteLine("Enter pra continuar...");
                                                        Console.ReadLine();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"{partida.jogador[j].nome} é o {completou + 1}° colocado!");
                                                        Console.ResetColor();
                                                        completou++;
                                                        partida.jogador[j].emjogo = false;
                                                        //Impressao(partida.jogador[j].getCopias(k));
                                                        Console.WriteLine("Enter pra continuar...");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                else
                                                {
                                                    partida.jogador[j].cartelas[k].emjogo = false; // Bingo falso o jogador perde a cartela atual
                                                    Console.ForegroundColor = ConsoleColor.Red;

                                                    int cartelasEmJogo = partida.jogador[j].CartelasEmJogo();

                                                    if (cartelasEmJogo == 0) //Era for a ultima cartela do jogador, o jogador sai do clico
                                                {
                                                        Console.WriteLine($"{partida.jogador[j].nome} anunciou Bingo incorretamente e perdeu sua ÚLTIMA cartela e está fora do jogo!");
                                                        partida.jogador[j].emjogo = false;
                                                        partida.RankingEliminados(j); // Ranking de trás pra frente
                                                        jogadoresEmJogo = partida.JogadoresEmJogo();
                                                        if (jogadoresEmJogo == 1)
                                                            break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"{partida.jogador[j].nome} anunciou Bingo incorretamente e perderá uma de suas cartelas!"); // Se não era a ultima ele apenas perde a partela
                                                    }

                                                    Console.ResetColor();
                                                    Console.WriteLine("Enter pra continuar...");
                                                    Console.ReadLine();
                                                    //Impressao(partida.jogador[j].getCopias(k));
                                                }

                                                //partida.RetornaRanking();
                                                //Console.ReadKey();
                                            }
                                        }
                                    }
                                }
                            }
                            jogadoresEmJogo = partida.JogadoresEmJogo();
                            if (jogadoresEmJogo == 1)
                                break;
                        }
                    }
                } else
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Fim da partida! {partida.ranking[0].nome} é o(a) vencedor(a)!");
            Console.ResetColor();
            partida.RetornaRanking();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Dados dos jogadores:");
            for(int i = 0; i < partida.jogador.Length; i++)
            {
                Console.WriteLine($"Nome: {partida.ranking[i].nome}");
                Console.WriteLine($"Sexo: {partida.ranking[i].sexo}");
                Console.WriteLine($"Idade: {partida.ranking[i].idade}");
                Console.WriteLine("------------------------------------------");
            }

            Console.ReadKey();
        } 
    }
}
