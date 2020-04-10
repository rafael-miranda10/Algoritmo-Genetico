using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_Genetico_Simples
{
    class Program
    {

        private static int NUMERO_CIDADES = 15;
        private static int NUMERO_POPULACAO;
        static void Main(string[] args)
        {
            bool FLAG_POPULACAO_VALIDA = false;

            // matriz de adjacencia
            int[,] _MAPA = {
            {0, 29, 82, 46, 68, 52, 72, 42, 51, 55, 29, 74, 23, 72, 46},
            {29, 0, 55, 46, 42, 43, 43, 23, 23, 31, 41, 51, 11, 52, 21},
            {82, 55, 0, 68, 46, 55, 23, 43, 41, 29, 79, 21, 64, 31, 51},
            {46, 46, 68, 0, 82, 15, 72, 31, 62, 42, 21, 51, 51, 43, 64},
            {68, 42, 46, 82, 0, 74, 23, 52, 21, 46, 82, 58, 46, 65, 23},
            {52, 43, 55, 15, 74, 0, 61, 23, 55, 31, 33, 37, 51, 29, 59},
            {72, 43, 23, 72, 23, 61, 0, 42, 23, 31, 77, 37, 51, 46, 33},
            {42, 23, 43, 31, 52, 23, 42, 0, 33, 15, 37, 33, 33, 31, 37},
            {51, 23, 41, 62, 21, 55, 23, 33, 0, 29, 62, 46, 29, 51, 11},
            {55, 31, 29, 42, 46, 31, 31, 15, 29, 0, 51, 21, 41, 23, 37},
            {29, 41, 79, 21, 82, 33, 77, 37, 62, 51, 0, 65, 42, 59, 61},
            {74, 51, 21, 51, 58, 37, 37, 33, 46, 21, 65, 0, 61, 11, 55},
            {23, 11, 64, 51, 46, 51, 51, 33, 29, 41, 42, 61, 0, 62, 23},
            {72, 52, 31, 43, 65, 29, 46, 31, 51, 23, 59, 11, 62, 0, 59},
            {46, 21, 51, 64, 23, 59, 33, 37, 11, 37, 61, 55, 23, 59, 0}};

            string[] _CIDADES = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };


            capiturarQuantidadePupulacao();


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Definições Iniciais
            bool MOSTRARITERACOES = true;
            int POPULACOESRUIM = (NUMERO_POPULACAO / 2);
            int NUMERO_ITERACOES = 3000;//3000
            int[,] POPULACAO = new int[NUMERO_POPULACAO, NUMERO_CIDADES];
            int[] FITNESS = new int[NUMERO_POPULACAO];

            //Chamada do método para criar uma população
            gerarPopulacaoAleatoriamente(POPULACAO);
            calcularFitness(POPULACAO, FITNESS, _MAPA);

            //imprime os estados iniciais (população)
            Console.Clear();
            Console.WriteLine("\n*** ESTADOS INICIAIS *** \n");
            imprimir(POPULACAO, FITNESS, _CIDADES);

            //chama o método ordenar os estados deixando os melhores no topo
            ordenar(POPULACAO, FITNESS);

            //imprime os estados (população de forma ordenado)
            if (MOSTRARITERACOES)
            {
                Console.WriteLine("\n*** ESTADOS ORDENADOS *** \n");
                imprimir(POPULACAO, FITNESS, _CIDADES);
                MOSTRARITERACOES = false;

            }

            //executa os ciclos até atingir as iterações, nesse caso até 3000 iterações
            for (int i = 0; i < NUMERO_ITERACOES; i++)
            {
                renovarPopulacao(POPULACAO,FITNESS,POPULACOESRUIM);
                calcularFitness(POPULACAO, FITNESS, _MAPA);
                ordenar(POPULACAO, FITNESS);
                if (MOSTRARITERACOES)
                {
                    Console.WriteLine($"\n*** ITERAÇÃO: {i+1} *** \n");
                    imprimir(POPULACAO, FITNESS, _CIDADES);
                }
            }

            stopWatch.Stop();

            //mostrando o melhor Fitness encontrado
            melhorFitness(POPULACAO,FITNESS,_CIDADES);

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("\nTempo de execução: " + elapsedTime);

            Console.ReadKey();






            #region Métodos/Funções
            void capiturarQuantidadePupulacao()
            {
                //Verifica se o valor de NUMERO_POPULACAO é uma entrada válida (> 1).
                while (!FLAG_POPULACAO_VALIDA)
                {
                    Console.Clear();
                    Console.WriteLine("INFORME A QUANTIDADE DE ESTADOS (POPULAÇÃO) A SEREM GERADOS: ");
                    int.TryParse(Console.ReadLine(), out NUMERO_POPULACAO);

                    if (NUMERO_POPULACAO > 1)
                    {
                        FLAG_POPULACAO_VALIDA = true;
                    }
                    else
                    {
                        Console.WriteLine($"O ESTADO (POPULACAO): {NUMERO_POPULACAO} NÃO É UMA ENTRADA VÁLIDA! PRESSIONE UM TECLA PARA CONTINUAR...");
                        Console.ReadKey();
                    }

                }

            }

            /**
             * Método para gerar de forma aleatoria todos os estados, conforme número de população indicado.
             * Parametros: populacao matriz de estado
             * retorno: matriz com os estados gerados
             */
            void gerarPopulacaoAleatoriamente(int[,] populacao)
            {
                //Cria um vetor para armazenar as cidades que serão geradas
                int[] p_temp = new int[NUMERO_CIDADES];
                bool pop_valida;

                for (int i = 0; i < NUMERO_POPULACAO; i++)
                {
                    pop_valida = false;
                    while (!pop_valida)
                    {
                        pop_valida = true;
                        p_temp = resetPopulacao(); // deixa o estado com o valor inicial de -1;

                        //gerando a população
                        for (int k = 0; k < NUMERO_CIDADES; k++)
                        {
                            p_temp[k] = valorValidoNaPopulacao(p_temp);
                        }
                        pop_valida = populacaoValido(p_temp, populacao);
                    }
                    // populacao[i] = p_temp;
                    preenchePopulacao(populacao, p_temp, i);
                }
            }

            void preenchePopulacao(int[,] populacao, int[] temp, int index)
            {
                for (int k = 0; k < NUMERO_CIDADES; k++)
                {
                    populacao[index, k] = temp[k];
                }
            }

            /**
            * Método para colocar as posições da população com o valor inicial -1
            * Retorno:  um vetor iniciado com valor -1
            */
            int[] resetPopulacao()
            {
                int[] aux = new int[NUMERO_CIDADES];
                for (int i = 0; i < NUMERO_CIDADES; i++)
                {
                    aux[i] = -1;
                }
                return aux;
            }

            /**
            * Método que seleciona a cidade e garante que não vai existir cidade repetida no estado
            * parametros: p_tmp recebe o vetor com os estados (populacao)
            * retorno: inteiro que representa a cidade
            */
            int valorValidoNaPopulacao(int[] p_tmp)
            {
                int pop_temp;
                bool valido;
                do
                {
                    pop_temp = new Random().Next(NUMERO_CIDADES);
                    valido = true;
                    for (int i = 0; i < NUMERO_CIDADES; i++)
                    {
                        if (p_tmp[i] == pop_temp)
                        {
                            valido = false;
                        }
                    }

                } while (!valido);
                return pop_temp;
            }

            /**
            * Metodo que garante que no curzamento nao vai existir valores repitidos
            * @param valor um inteiro que representa a distacia da cidade
            * @param p_tmp vetor com um estado
            * @return valor booleano true se nao exitir e falso se existir
            */
            bool valorValidoNaPopulacaoBool(int valor, int[] p_tmp)
            {
                int pop_temp = valor;
                bool valido;

                valido = true;
                for (int ii = 0; ii < NUMERO_CIDADES; ii++)
                {
                    if (p_tmp[ii] == pop_temp)
                    {
                        valido = false;
                    }
                }

                return valido;
            }

            /**
             * Método para verficar se o estado não esta igual aos estados armazenados na população
             * parametro: p_tmp estado corrente
             * parametro: populacao estados armazenados
             * retorno: valor booleano true se não for igual e false se for igual
             */
            bool populacaoValido(int[] p_tmp, int[,] populacao)
            {
                bool pop_valida = true;
                for (int i = 0; i < NUMERO_POPULACAO; i++)
                {
                    int n_iguais = 0;
                    for (int k = 0; k < NUMERO_CIDADES; k++)
                    {
                        if (p_tmp[k] == populacao[i, k])
                        {
                            n_iguais++;
                        }
                    }
                    if (n_iguais == NUMERO_CIDADES)
                    {
                        pop_valida = false;
                    }
                }
                return pop_valida;
            }

            /**
             * Método que calcula o Fitness de cada população
             * parametro: populacao matriz com todos os estados
             * parametro: fitness vetor com o melhorFitness de fitness de cada estado
             * parametro: mapa matriz com o mapa das cidades
             */
            void calcularFitness(int[,] populacao, int[] fitness, int[,] mapa)
            {
                int i, k;
                //calculando o melçhor fitness
                for (i = 0; i < NUMERO_POPULACAO; i++)
                {
                    int restTmp = 0;
                    for (k = 0; k < NUMERO_CIDADES - 1; k++)
                    {
                        restTmp += mapa[populacao[i, k], populacao[i, k + 1]];
                    }
                    restTmp += mapa[populacao[i, 0], populacao[i, k]];
                    fitness[i] = restTmp;
                }
            }

            /**
             * metodo para imprimir os valores da população, cidade e o fitness de cada estado
             * parametro: populacao matriz de estados
             * parametro: fitness vetor com valor de distancia da rota de cada estado
             * parametro: cidades  vetor com nome das cidades
             */
            void imprimir(int[,] populacao, int[] fitness, string[] cidades)
            {
                int i, k;
                for (i = 0; i < NUMERO_POPULACAO; i++)
                {
                    for (k = 0; k < NUMERO_CIDADES; k++)
                    {
                        Console.Write($"{cidades[populacao[i, k]]} => ");
                    }
                    Console.Write($"{cidades[populacao[i, 0]]} ");
                    Console.WriteLine($"FITNESS: {fitness[i]}");
                }
            }

            /**
            * Método que ordena as população, compara cada fitness deixando os melhores no topo, ocorrendo a seleção das melhores.
            * parametro: populacao
            * parametro: fitness
            */
            void ordenar(int[,] populacao, int[] fitness)
            {
                //ordenando
                int i, k;
                for (i = 0; i < NUMERO_POPULACAO; i++)
                {
                    for (k = i; k < NUMERO_POPULACAO; k++)
                    {
                        if (fitness[i] > fitness[k])
                        {
                            int vTmp;
                            int[] vvTmp = new int[10];
                            vTmp = fitness[i];
                            fitness[i] = fitness[k];
                            fitness[k] = vTmp;

                            //vvTmp = populacao[i];
                            vvTmp = pegarLinhaMatriz(i, populacao);
                            //populacao[i] = populacao[k];
                            colocarlinhaMatriz(i, populacao, pegarLinhaMatriz(k, populacao));
                            // populacao[k] = vvTmp;
                            colocarlinhaMatriz(k, populacao, vvTmp);
                        }
                    }
                }
            }

            int[] pegarLinhaMatriz(int linha, int[,] populacao)
            {
                int[] aux = new int[NUMERO_CIDADES];
                for (int i = 0; i < NUMERO_CIDADES; i++)
                {
                    aux[i] = populacao[linha, i];
                }
                return aux;
            }

            void colocarlinhaMatriz(int linha, int[,] populacao, int[] vet)
            {
                for (int i = 0; i < NUMERO_CIDADES; i++)
                {
                    populacao[linha, i] = vet[i];
                }
            }

            /**
            * Método que seleciona 2 pais faz o cruzamento e a mutação
            * Parametro: populacao matriz com estados
            * Parametro: fitness vetor com distacia de cada estado
            * Parametro: populacoesRuim valor de corta para os estados ruins
            */
            void renovarPopulacao(int[,] populacao, int[] fitness, float populacaoRuim)
            {
                int inicioExcluidos = (int)POPULACOESRUIM;
                int i, k;

                for (i = inicioExcluidos; i < NUMERO_POPULACAO; i++)
                {
                    bool valido = false;
                    while (!valido)
                    {
                        int[] p_tmp = resetPopulacao();

                        //pegando 2 pais aleatoriamente
                        int pail1, pai2;

                        pail1 = new Random().Next(inicioExcluidos);
                        do
                        {
                            pai2 = new Random().Next(inicioExcluidos);
                        } while ((pail1 == pai2) && (fitness[pail1] != fitness[pai2]));

                        //pegando 6 caracteristicas do pai 1 aleatoriamente
                        for (k = 0; k< 6; k++)
                        {
                            int pos = new Random().Next(NUMERO_CIDADES);
                            p_tmp[pos] = populacao[pail1,pos];
                        }

                        // pegando restante do pai 2
                        for (k = 0; k < 8; k++)
                        {
                            int pos = new Random().Next(NUMERO_CIDADES);
                            if (p_tmp[pos] == -1)
                            {
                                if (valorValidoNaPopulacaoBool(populacao[pai2,pos],p_tmp))
                                {
                                    p_tmp[pos] = populacao[pai2,pos];
                                }
                            }
                        }

                        // preenchendo o restante com aleatorios (Mutação)
                        for (k = 0; k < NUMERO_CIDADES; k++)
                        {
                            if(p_tmp[k] == -1)
                            {
                                int pop_temp = valorValidoNaPopulacao(p_tmp);
                                p_tmp[k] = pop_temp;
                            }
                        }

                        // verificando se é valido
                        valido = populacaoValido(p_tmp, populacao);
                        if (valido)
                        {
                            colocarlinhaMatriz(i,populacao,p_tmp);
                        }

                    }
                }
            }

            /**
             * método que imprime resultado final com o melhor estado
             * Parametro: populacao matriz com os estados
             * Parametro: fitness vetor com a distancia de cada rota
             * Parametro: cidades vetor com nome das cidades
             */

            void melhorFitness(int[,] populacao, int[] fitness, string[] cidades)
            {
                int i =0, k;

                Console.WriteLine("\n*** MELHOR CAMINHO ENCONTRADO *** \n");
                for (k =0; k<NUMERO_CIDADES; k++)
                {
                    Console.Write($"{cidades[populacao[i,k]]} => ");
                }

                Console.Write($"{cidades[populacao[i, 0]]}  ");
                Console.WriteLine($"FITNESS: {fitness[i]}");
            }

            #endregion
        }
    }
}
