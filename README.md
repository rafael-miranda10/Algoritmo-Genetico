# Algoritmo-Genetico

uma versão simples que será evoluida ao longo do tempo.

# Introdução
O Problema do Caixeiro Viajante (PCV) é um dos problemas mais intensamente estudados em matemática computacional. Apesar de sua definição singela, o PCV representa até hoje um dos desafios da computação.
Sua origem é creditada a William Rowan Hamilton, que inventou um jogo, cujo objetivo era o de traçar um roteiro através dos vértices de um dodecaedro (vértices equivalem a cidades) que se inicia e termina no mesmo vértice, contudo sem repetir uma visita. O Ciclo Hamiltoniano constitui uma solução para jogos de Hamilton [Golbarg e Luna 1999].
O PCV pode ser definido como um problema de encontrar o roteiro de menor distância ou custo que passa por um conjunto de cidades, sendo cada cidade visitada exatamente uma única vez, regressando ao seu ponto de partida.
Sob a ótica de otimização, o PCV pertence à categoria conhecida como NP-difícil ou NP-hard, o que significa que possui ordem de complexidade exponencial.

# Algoritmo Genético

Os algoritmos genéticos (AGs) têm como característica a otimização global, baseado nos mecanismos de seleção natural e da genética. Eles empregam uma estratégia de busca paralela e estruturada, embora aleatória, levando à busca de pontos de alta aptidão, ou seja, tenha valores relativamente baixos ou altos, pois eles exploram informações históricas para encontar novos pontos de busca onde são melhores resultados. O qual se dá por meio de processos iterativos (geração). Em cada iteração é realizada uma seleção que determina quais indivíduos conseguirão se reproduzir e gerar seus descendentes [Rezende 2005].
Russel e Norvig (2004) explicam em linhas gerais que os AGs, são uma variante da Busca em Feixe Estocástica, porém os estados sucessores são gerados pela combinação de outros dois estados, em vez de serem gerados pela modificação de um único estado.

O AG começa com um conjunto de K estados gerados aleatoriamente, denominado população, e cada estado (indivíduo), é representado por uma cadeia sobre um alfabeto finito e esse estado é avaliado pela função de avaliação (função de fitness). A função de fitness retorna valores dos quais servirão para classificar os melhores estados (denominados como pais). A partir dai acontece um cruzamento entre os pais para gerar os próximos estados (os estados filhos), ficando cada posição do estado filho sujeita a uma mutação, e esses passos ocorrerão por diversas vezes até atingir o objetivo, ou seja, o melhor fitness para o problema [Russel e Norvig 2004].
Para a solução do PCV, utilizamos AG da seguinte forma: primeiramente é gerada uma matriz de adjacência com o mapa que representa a distância entre as 15 cidades. O próximo passo é a geração aleatória dos K estados (população) iniciais. Nesse momento é importante lembrar que é necessário que a quantidade de estados para formar a população seja maior que 1, caso contrário não será possível atender as necessidades que o AG precisa para que os passos de cruzamento e mutação sejam executados perfeitamente. O próximo passo é a geração da população, esta fase cada estado é gerado de forma aleatória, garantindo que não vão existir valores repetidos. Após a geração da população é calculado o valor de fitness, que é a soma das distâncias de cada cidade (ver Figura 2). O próximo passo é a ordenação da população de forma que os melhores fiquem no topo. Os melhores são aqueles que têm o fitness mais baixo, ou seja, nesse contexto a menor distância entre as rotas.

A etapa seguinte é selecionar 50% dos melhores estados e descartar os restantes, dentre estes selecionados como melhores é escolhido de forma aleatória dois estados (pais), de cada pai é selecionado de forma aleatória metade dos genes (cidades), para que seja realizado o cruzamento para gerar os estados filhos. No final, se necessário, ocorre a mutação, completando os genes dos filhos que faltaram, devido aos genes incompatíveis com a solução (ver Figura 3). Este processo se repete por 3.000 iterações e ao final deste processo é exibido o melhor estado, nesse caso a melhor rota.


# Referências
Cunha, C. B.; Bonasser, U. O.; Abrahão, F. T. M. (2002) “Experimentos Computacionais com Heurísticas de Melhorias para o Problema do Caixeiro Viajante” htpp://sites.poli.usp.br/ptr/ptr/docentes/cbcunha/files/2-opt_TSP_Anpet_2002_CBC.pdf Março.

Golbarg, M. C; Luna, H. P. R. (1999) “Otimização Combinatória e Programação Linear”. Rio de Janeiro: Editora Campus.

Malaquias, G. L. M. (2006) “Uso dos Algoritmos Genéticos para a Otimização de Rotas de Distribuição”, Universidade Federal de Uberlândia,.http://repositorio.ufu.br/bitstream/123456789/358/1/UsoAlgoritmosGen%C3%A9ticos.pdf, Março.

Martinelli, D. O. J.; Fernandes, E. L. R.; Medeiros, M. L. (2002) “Problema do Caixeiro Viajante”, Universidade Federal do Mato Grosso do Sul, Campo Grande.

Rezende, S. O. (2005) “Sistemas Inteligentes Fundamentos e Aplicações”, Barueri, SP: ed Manole.

Russel S. J., Norvig P. (2004) “Inteligência Artificial”, 2 edição, Prentice Hall.
